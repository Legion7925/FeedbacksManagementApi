using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IExpertRepository
    {
        Task AddExpert(ExpertBase expertBase);
        Task DeleteExpert(int expertId);
        Task<Expert?> GetExpertById(int expertId);
        IEnumerable<Expert> GetExperts();
        Task UpdateExpert(ExpertBase expertBase, int caseId);

    }
}
