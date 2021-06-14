using Microsoft.EntityFrameworkCore;
using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Models.dto
{
    public class dtoGithubRepo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string html_url { get; set; }
    }

}
