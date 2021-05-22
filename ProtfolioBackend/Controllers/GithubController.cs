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
        private readonly ILogger<GithubController> _logger;
        private readonly DataContext _context;

        public GithubController(ILogger<GithubController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Get/{User}")]
        [HttpGet]
        public async Task<ActionResult<GithubUser>> GetUser(String username)
        {
            var result = await _context.GithubUsers.Where(x => x.UserName == username).FirstOrDefaultAsync();
            return result;
        }

      /*  [Route("Get/{User}")]
        [HttpGet]
        public async Task<ActionResult<GithubRepo>> Getdb(String username, String reponame)
        {
            var user = await _context.GithubUsers.Where(x => x.UserName == username).FirstOrDefaultAsync();
            return result;
        }*/



    }
}
