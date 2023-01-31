﻿using AutoMapper;
using FeedbacksManagementApi.Entities;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CaseBase, Case>();
        }
    }
}
