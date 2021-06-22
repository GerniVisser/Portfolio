using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext context)
        {
            if (await context.GithubUsers.AnyAsync()) return;

            var userData = System.IO.File.ReadAllText("Models/Data/SeedData.json");
            var users = JsonSerializer.Deserialize<List<GithubUser>>(userData);

            foreach(var user in users)
            {
                context.GithubUsers.Add(user);
            }

            await context.SaveChangesAsync();
        }   
    }
}
