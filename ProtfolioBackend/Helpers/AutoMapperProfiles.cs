using AutoMapper;
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

            CreateMap<dtoGithubRepoContent, GithubRepo>();
            CreateMap<GithubRepo, dtoGithubRepoContent>();

            CreateMap<dtoGithubReadMe,dtoGithubRepoContent>();

            CreateMap<GithubRepo, dtoGithubRepo>();

            CreateMap<GithubRepo, dtoGithubRepoContentVaribles>();
            CreateMap<dtoGithubRepoContentVaribles, GithubRepo>();

            CreateMap<dtoGithubRepoContent, dtoGithubRepoContentVaribles>();

            CreateMap<dtoGithubRepo, dtoGithubRepoContent>()
                .ForMember(dest => dest.GithubId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.html_url));

        }
    }
}
