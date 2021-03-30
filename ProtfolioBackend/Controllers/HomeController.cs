using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtfolioBackend.Models;
using ProtfolioBackend.Models.dto;

namespace ProtfolioBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        dtoGithubReadMe dtoReadMe;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task getReadMe()
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
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

    }
}
