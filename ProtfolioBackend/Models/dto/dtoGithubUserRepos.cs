using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.dto
{
    public class dtoGithubUserRepos
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public ICollection<dtoGithubRepoContent> Repo { get; set; }
    }
}
