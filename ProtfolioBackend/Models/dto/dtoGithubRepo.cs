using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.data
{
    public class dtoGithubRepo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Encoding { get; set; }
        public dtoGithubUser Owner { get; set; }
        public int OwnerId { get; set; }
    }

}
