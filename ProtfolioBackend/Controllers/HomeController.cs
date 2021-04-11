﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtfolioBackend.BusinessLogic.Processes.Github;
using ProtfolioBackend.Models.data;

namespace ProtfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[]")]
    public class HomeController : ControllerBase
    {
        private IGitHub _github;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGitHub github)
        {
            _logger = logger;
            _github = github;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<dtoGithubReadMe>> Get()
        {
            dtoGithubReadMe result = await _github.getReadMe();
            return result;
        }

    }
}
