using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Processes.Github
{
    public interface IGithubRepository
    {
        Task<IEnumerable<dtoGithubRepo>> GetUserReposSummary(string username);
        Task<dtoGithubUserRepos> GetUserRepoContent(string username);
        Task<dtoGithubRepoContent> GetRepoContent(string username, string reponame);
        Task UpdateDB();
    }
    public class GithubDBRepository : IGithubRepository
    {
        private readonly IUsers _users;

        public GithubDBRepository(IUsers users)
        {
            _users = users;
        }

        public async Task<dtoGithubRepoContent> GetRepoContent(string username, string reponame)
        {
            throw new NotImplementedException();
        }

        public Task<dtoGithubUserRepos> GetUserRepoContent(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dtoGithubRepo>> GetUserReposSummary(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDB()
        {
            throw new NotImplementedException();
        }
    }
}
