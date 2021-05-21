﻿using AutoMapper;
using ProtfolioBackend.Models.Data.Entities;
using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GithubUser, dtoGithubUser>();
            CreateMap<GithubUser, dtoGithubUserRepos>();
            CreateMap<dtoGithubUserRepos, GithubUser>();
            CreateMap<dtoGithubRepo, dtoGithubRepoContent>();

            CreateMap<dtoGithubRepoContent, GithubRepo>();
        }
    }
}
