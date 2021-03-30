using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.dto
{
    public class dtoGithubReadMe
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Encoding { get; set; }
    }

}
