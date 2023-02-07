using AutoMapper;
using Domain.Entities;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class ExpertsProfile : Profile
    {
        public ExpertsProfile()
        {
            CreateMap<ExpertBase, Expert>();
        }
    }
}
