using Microsoft.AspNetCore.Mvc;
using ProtfolioBackend.Controllers;
using ProtfolioBackend.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Processes.Github
{
    public interface IGitHub
    {
        Task<dtoGithubReadMe> getReadMe();
    }

    public class GithubPO : IGitHub
    {
        private readonly IHttpClientFactory _clientFactory;
        dtoGithubReadMe dtoReadMe;

        public GithubPO(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<dtoGithubReadMe> getReadMe()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/repos/Sentdex/GameGAN_code/readme");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                dtoReadMe = await response.Content.ReadFromJsonAsync<dtoGithubReadMe>();
                return dtoReadMe;
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
