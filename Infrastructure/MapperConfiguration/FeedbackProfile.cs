using AutoMapper;
using Domain.Entities;

namespace Infrastructure.MapperConfiguration
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<FeedbackBase, Feedback>();
        }
    }
}
