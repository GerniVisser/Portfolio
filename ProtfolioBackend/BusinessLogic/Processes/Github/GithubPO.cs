﻿using Hangfire;
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

namespace ProtfolioBackend.BusinessLogic.Processes.Github
{
    public interface IGitHub
    {
        Task<IEnumerable<dtoGithubRepo>> getReposData(string user);
        Task<dtoGithubReadMe> getReadMeData(string user, string repo);
        Task<GithubUser> getUserData(GithubUser user);
        Task updateUser(GithubUser user);
        Task updateDB();
    }

    public class GithubPO : IGitHub
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IUsers _users;
        private readonly IRepos _repos;

        public GithubPO(IHttpClientFactory clientFactory, IMapper mapper, IUsers users, IRepos repos)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
            _users = users;
            _repos = repos;
            RecurringJob.AddOrUpdate(() => System.Diagnostics.Debug.WriteLine("HIT"), Cron.Minutely);
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
        

        public async Task<GithubUser> getUserData(GithubUser User)
        {
            IEnumerable<dtoGithubRepo> NewdtoRepos = await getReposData(User.UserName);
            ICollection<dtoGithubRepoContent> reposEntity = _mapper.Map<ICollection<dtoGithubRepoContent>>(NewdtoRepos);

            List<GithubRepo> NewRepos = new List<GithubRepo>();
            IEnumerable<GithubRepo> OldRepos = await _repos.GetAllGithubReposByUserIdAsync(User.Id);

            User.Repo.Clear();

            foreach (dtoGithubRepoContent repoEntity in reposEntity)
            {
                repoEntity.OwnerId = User.Id;

                try
                {
                    dtoGithubReadMe readme = await getReadMeData(User.UserName, repoEntity.Name);

                    repoEntity.Content = readme.Content;
                    repoEntity.Url = readme.Url;

                    
                }
                catch
                {
                    repoEntity.Content = "";
                    repoEntity.Url = "";
                }
                finally
                {
                    GithubRepo newGithubRepo = _mapper.Map<GithubRepo>(repoEntity);
                    User.Repo.Add(newGithubRepo);
                }
            }

            return User;

        }
        public async Task updateUser(GithubUser user)
        {
            _users.Update(await getUserData(user));
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
