using ProtfolioBackend.Models.Data.Entities;
using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Interfaces
{
    public interface IUsers
    {
        Task<dtoGithubUserRepos> GetGithubUserWithReposByUsername(string username);
        Task<IEnumerable<GithubUser>> GetAllGithubUsersAsync();
        Task<IEnumerable<dtoGithubUserRepos>> GetAllGithubUsersWithReposAsync();
        Task<IEnumerable<dtoGithubRepo>> GetRepoContentAsync(string username);
        Task<dtoGithubRepoContent> GetRepoContentAsync(string username, string reponame);
        void Update(GithubUser user);
        Task<bool> SaveAllAsync();
    }
}
