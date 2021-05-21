using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProtfolioBackend.BusinessLogic.Processes.Github;
using ProtfolioBackend.Models.dto;
using ProtfolioBackend.Models.Data;
using ProtfolioBackend.Models.Data.Entities;

namespace ProtfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GithubController : ControllerBase
    {
        private IGitHub _github;
        private readonly ILogger<GithubController> _logger;

        public GithubController(ILogger<GithubController> logger, IGitHub github)
        {
            _logger = logger;
            _github = github;
        }

       /* [Route("Get/{User}")]
        [HttpGet]
        public async Task<ActionResult<GithubUser>> Getdb(String User)
        {
            var result = await _context.GithubUsers.FirstOrDefaultAsync();
            return result;
        }*/

        [Route("Test")]
        [HttpGet]
        public async Task Getdb1()
        {
            await _github.updateDB();
        }

    }
}
