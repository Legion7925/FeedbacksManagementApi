using AutoMapper;
using Domain.Entities;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CaseBase, Case>();

            CreateMap<CaseBase , Feedback>();
        }
    }
}
