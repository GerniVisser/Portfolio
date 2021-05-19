using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.data;
using ProtfolioBackend.Models.Data;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.BusinessLogic.Objects.Github
{
    public class GithubUserBO : IUsers
    {
        private readonly DataContext _context;

        public GithubUserBO(DataContext context)
        {
            _context = context;
        }

        public async Task<GithubUser> GetGithubUserById(int id)
        {
            return await _context.GithubUsers.FindAsync(id);
        }

        public async Task<IEnumerable<GithubUser>> GetAllGithubUsersAsync()
        {
            return await _context.GithubUsers.ToListAsync();
        }

        public void Update(GithubUser repo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
