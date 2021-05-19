using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Interfaces
{
    interface IRepos
    {
        Task<GithubRepo> GetGithubRepoById(int id);
        Task<IEnumerable<GithubRepo>> GetAllGithubReposByUserIdAsync(int userId);
        void Update(GithubRepo repo);
        Task<bool> SaveAllAsync();
    }
}
