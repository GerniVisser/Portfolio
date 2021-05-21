using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.Models.dto;
using ProtfolioBackend.Models.Data;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ProtfolioBackend.BusinessLogic.Objects.Github
{
    public class GithubUserBO : IUsers
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GithubUserBO(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<dtoGithubUserRepos> GetGithubUserWithReposByUsername(string username)
        {
            return await _context.GithubUsers
                .Where(x => x.UserName == username)
                .ProjectTo<dtoGithubUserRepos>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<GithubUser>> GetAllGithubUsersAsync()
        {
            return await _context.GithubUsers
                .Include(x => x.Repo)
                .ToListAsync();
        }

        public async Task<IEnumerable<dtoGithubUserRepos>> GetAllGithubUsersWithReposAsync()
        {
            return await _context.GithubUsers
                .ProjectTo<dtoGithubUserRepos>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Update(GithubUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
