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
using ProtfolioBackend.Models.data;
using ProtfolioBackend.Models.Data;

namespace ProtfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GithubController : ControllerBase
    {
        private IGitHub _github;
        private readonly DataContext _context;
        private readonly ILogger<GithubController> _logger;

        public GithubController(ILogger<GithubController> logger, IGitHub github, DataContext context)
        {
            _logger = logger;
            _github = github;
            _context = context;
        }

        [Route("Get/{RepoName}")]
        [HttpGet]
        public async Task<ActionResult<dtoGithubReadMe>> Getdb(String RepoName)
        {
            var result = await _context.Github.FirstOrDefaultAsync();
            return result;
        }

    }
}
