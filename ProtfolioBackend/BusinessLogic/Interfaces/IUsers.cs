using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Interfaces
{
    interface IUsers
    {
        Task<GithubUser> GetGithubUserById(int id);
        Task<IEnumerable<GithubUser>> GetAllGithubUsersAsync();
        void Update(GithubUser user);
        Task<bool> SaveAllAsync();
    }
}
