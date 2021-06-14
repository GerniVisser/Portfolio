using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.dto
{
    public class dtoGithubRepoContentVaribles
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public int GithubId { get; set; }
    }
}
