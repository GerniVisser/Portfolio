using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<dtoGithubReadMe> Github { get; set; }
    }
}
