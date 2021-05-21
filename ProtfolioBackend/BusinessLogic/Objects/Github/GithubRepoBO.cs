using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.Data;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Objects.Github
{
    public class GithubRepoBO: IRepos
    {
        private readonly DataContext _context;

        public GithubRepoBO(DataContext context)
        {
            _context = context;
        }

        public async Task<GithubRepo> GetGithubRepoByName(string name)
        {
            return await _context.GithubRepos.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<GithubRepo>> GetAllGithubReposByUserIdAsync(int userId)
        {
            return await _context.GithubRepos.Where(x => x.OwnerId == userId).ToListAsync();
        }

        public void Update(GithubRepo repo)
        {
            _context.Entry(repo).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
