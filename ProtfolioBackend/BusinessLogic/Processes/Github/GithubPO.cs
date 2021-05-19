using Microsoft.AspNetCore.Mvc;
using ProtfolioBackend.Controllers;
using ProtfolioBackend.Models.data;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Processes.Github
{
    public interface IGitHubUser
    {
        Task<GithubUser> getUserData(string user);
    }

    public class GithubPO : IGitHubUser
    {
        private readonly IHttpClientFactory _clientFactory;
        GithubUser user;
        GithubRepo repo;

        public GithubPO(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<GithubRepo> getReadMe(string user, string repo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/repos/"+user+"/"+repo+"/readme");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                this.repo = await response.Content.ReadFromJsonAsync<GithubRepo>();
                return this.repo;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<GithubUser> getUserData(string user)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/users/"+user);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                this.user = await response.Content.ReadFromJsonAsync<GithubUser>();
                return this.user;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task updateDB()
        {

        }
    }
}
