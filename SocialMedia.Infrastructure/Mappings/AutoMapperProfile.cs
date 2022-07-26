using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile    
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
