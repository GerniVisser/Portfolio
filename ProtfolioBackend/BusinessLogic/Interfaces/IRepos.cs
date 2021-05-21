using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Interfaces
{
    public interface IRepos
    {
        Task<GithubRepo> GetGithubRepoByName(string name);
        Task<IEnumerable<GithubRepo>> GetAllGithubReposByUserIdAsync(int userId);
        void Update(GithubRepo repo);
        Task<bool> SaveAllAsync();
    }
}
