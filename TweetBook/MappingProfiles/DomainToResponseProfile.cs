﻿using System.Linq;
using AutoMapper;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;

namespace TweetBook.MappingProfiles;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.Tags, opt => 
                opt.MapFrom(src => src.Tags.Select(x => new TagResponse{Name = x.TagName})));
        CreateMap<Tag, TagResponse>();

    }
}