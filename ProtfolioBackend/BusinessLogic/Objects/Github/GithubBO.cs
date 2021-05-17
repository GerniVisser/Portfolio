using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.data;
using ProtfolioBackend.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Objects.Github
{
    public class GithubBO : IReadMe
    {
        private readonly DataContext _context;

        public GithubBO(DataContext context)
        {
            _context = context;
        }

        public async Task<dtoGithubReadMe> GetRepoReadMeByIdAsync(int id)
        {
            return await _context.GithubRepo.FindAsync(id);
        }

        public async Task<IEnumerable<dtoGithubReadMe>> GetAllRepoReadMeAsync()
        {
            return await _context.GithubRepo.ToListAsync();
        }

        public void Update(dtoGithubReadMe repo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
