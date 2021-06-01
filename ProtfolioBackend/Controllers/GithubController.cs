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
using ProtfolioBackend.BusinessLogic.Objects.Github;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.dto.dtoEndpointsEntries;

namespace ProtfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly ILogger<GithubController> _logger;
        private readonly IUsers _users;

        public GithubController(ILogger<GithubController> logger, IUsers users)
        {
            _logger = logger;
            _users = users;
        }

        [Route("Repos/Summary/{username}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dtoGithubRepo>>> GetUserReposSummary(String username)
        {
            var result = await _users.GetRepoContentAsync(username);
            return Ok(result);
        }

        [Route("Repos/RepoData")]
        [HttpGet]
        public async Task<ActionResult<GithubRepo>> GetRepoData([FromBody] dtoSelectRepo selectRepo)
        {
            var result = await _users.GetRepoContentAsync(selectRepo.Username, selectRepo.Reponame);
            return Ok(result);
        }
    }
}
