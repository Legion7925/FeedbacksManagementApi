using AutoMapper;
using FeedbacksManagementApi.Entities;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<FeedbackBase, Feedback>();
        }
    }
}
