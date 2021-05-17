using ProtfolioBackend.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Interfaces
{
    interface IReadMe
    {
        Task<dtoGithubReadMe> GetRepoReadMeByIdAsync(int id);
        Task<IEnumerable<dtoGithubReadMe>> GetAllRepoReadMeAsync();
        void Update(dtoGithubReadMe repo);
        Task<bool> SaveAllAsync();
    }
}
