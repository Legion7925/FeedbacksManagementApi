using AutoMapper;
using Domain.Entities;

namespace Infrastructure.MapperConfiguration
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
