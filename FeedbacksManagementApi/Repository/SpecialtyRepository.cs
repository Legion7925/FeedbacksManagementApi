using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly FeedbacksDbContext context;

        public SpecialtyRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Specialty> GetSpecialties()
        {
            return context.Specialties.AsNoTracking().AsEnumerable();
        }
    }
}
