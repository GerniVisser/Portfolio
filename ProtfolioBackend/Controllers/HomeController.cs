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
    public class HomeController : ControllerBase
    {
        private IGitHub _github;
        private readonly DataContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGitHub github, DataContext context)
        {
            _logger = logger;
            _github = github;
            _context = context;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<dtoGithubReadMe>> Get()
        {
            dtoGithubReadMe result = await _github.getReadMe();
            return result;
        }

        [Route("Getdb")]
        [HttpGet]
        public async Task<ActionResult<dtoGithubReadMe>> Getdb()
        {
            var result = await _context.Github.FirstOrDefaultAsync();
            return result;
        }

    }
}
