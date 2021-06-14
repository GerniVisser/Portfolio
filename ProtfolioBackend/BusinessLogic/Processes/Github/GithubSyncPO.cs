using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProtfolioBackend.BusinessLogic.Objects.Github;
using ProtfolioBackend.Controllers;
using ProtfolioBackend.Models.dto;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using ProtfolioBackend.Extentions;

namespace ProtfolioBackend.BusinessLogic.Processes.Github
{
    public interface IGitHubSync
    {
        Task<IEnumerable<dtoGithubRepo>> getReposData(string user);
        Task<dtoGithubReadMe> getReadMeData(string user, string repo);
        Task<IEnumerable<dtoGithubRepoContent>> getUserDataFromGithub(GithubUser user);
        GithubUser addRemoveRepos(GithubUser user, IEnumerable<dtoGithubRepoContent> githubdtoList);
        GithubUser updateRepoContent(GithubUser user, IEnumerable<dtoGithubRepoContent> githubdtoList);
        Task updateUser(GithubUser user);
        Task updateDB();
    }

    public class GithubSyncPO : BackgroundService, IGitHubSync
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IUsers _users;
        private readonly IServiceScopeFactory _scopeFactory;

        public GithubSyncPO(IHttpClientFactory clientFactory, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
            // Creats instances of IUser for every call as DBContext is a scoped Lifacycle.
            _users = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUsers>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Task running ..");
                    await updateDB();
                    await Task.Delay(1000 * 300, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to run sertvice....");
                    return;
                }
            }
        }

        public async Task<IEnumerable<dtoGithubRepo>> getReposData(string user)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/users/"+user+"/repos");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<dtoGithubRepo> repos = await response.Content.ReadFromJsonAsync<IEnumerable<dtoGithubRepo>>();
                return repos;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task<dtoGithubReadMe> getReadMeData(string user, string repo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/repos/"+user+"/"+repo+"/readme");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                dtoGithubReadMe readme = await response.Content.ReadFromJsonAsync<dtoGithubReadMe>();
                return readme;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        

        public async Task<IEnumerable<dtoGithubRepoContent>> getUserDataFromGithub(GithubUser user)
        {
            IEnumerable<dtoGithubRepo> NewdtoRepos = await getReposData(user.UserName);
            IEnumerable<dtoGithubRepoContent> reposEntity = _mapper.Map<IEnumerable<dtoGithubRepoContent>>(NewdtoRepos);

            IList<dtoGithubRepoContent> finalRepoEntity = new List<dtoGithubRepoContent>();

            foreach (dtoGithubRepoContent repoEntity in reposEntity)
            {
                try
                {
                    var readme = await getReadMeData(user.UserName, repoEntity.Name);

                    // Remove Repos that have no content AKA no README adds unneccasary proccessing to handle them
                    _mapper.Map(readme, repoEntity);
                    finalRepoEntity.Add(repoEntity);
                }
                catch
                {
                }
            }

            return finalRepoEntity;
        }

        public GithubUser addRemoveRepos(GithubUser user, IEnumerable<dtoGithubRepoContent> githubdtoList)
        {
            IEnumerable<int> dbRepos = user.Repo.Select(x => x.GithubId).ToList();
            IEnumerable<int> githubRepos = githubdtoList.Select(x => x.GithubId).ToList();
            // If this check returns true a repo has been added or removed 
            if (dbRepos.Count() != githubRepos.Count())
            {
                IEnumerable<int> addRepoList = githubRepos.Except(dbRepos);
                IEnumerable<int> removeRepoList = dbRepos.Except(githubRepos);

                foreach(var githubID in addRepoList)
                {
                    var map = _mapper.Map<GithubRepo>(githubdtoList.Where(x => x.GithubId == githubID).FirstOrDefault());
                    user.Repo.Add(map);
                }

                foreach (var githubID in removeRepoList)
                {
                    user.Repo.Remove(user.Repo.Where(x => x.GithubId == githubID).FirstOrDefault());
                }
            }
             return user;
        }

        public GithubUser updateRepoContent(GithubUser user, IEnumerable<dtoGithubRepoContent> githubdtoList)
        {
            // Map repo data to dtoGithubContent to exclude Id, Parent ext
            IEnumerable<dtoGithubRepoContentVaribles> githubList = _mapper.Map<IEnumerable<dtoGithubRepoContentVaribles>>(githubdtoList);
            IEnumerable<dtoGithubRepoContentVaribles> dbdtoList = _mapper.Map<IEnumerable<dtoGithubRepoContentVaribles>>(user.Repo);

            // IF true some of the records differ and update needs to happen
            if(!githubList.OrderBy(x => x.GithubId).SequenceEqual(dbdtoList.OrderBy(x => x.GithubId), new ContentComparer()))
            {
                IEnumerable<dtoGithubRepoContentVaribles> reposToUpdate = githubList.Except(dbdtoList, new ContentComparer()).ToList();

                foreach(var repo in reposToUpdate)
                {
                    var dbrepo = user.Repo.Where(x => x.GithubId == repo.GithubId).FirstOrDefault();
                    _mapper.Map(repo, dbrepo);
                }
            }

            return user;
        }

        public async Task updateUser(GithubUser user)
        {
            var data = await getUserDataFromGithub(user);

            var addRemoveData = addRemoveRepos(user, data);
            var updatedContent = updateRepoContent(user, _mapper.Map<IEnumerable<dtoGithubRepoContent>>(addRemoveData.Repo));

            _users.Update(updatedContent);
        }

        public async Task updateDB()
        {
            IEnumerable<GithubUser> users = await _users.GetAllGithubUsersAsync();
            foreach(GithubUser user in users)
            {
                await updateUser(user);
            }

            await _users.SaveAllAsync();
        }

        
    }
}
