using Domain.Entities;
using FeedbacksManagementApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFeedbackRepository
    {
        Task AddFeedback(FeedbackBase feedbackbase);
        Task ArchiveFeedbacks(int[] feedbackIds);
        Task DeleteFeedbacks(int[] feedbackIds);
        Task<Feedback?> GetFeedbackById(int feedbackId);
        IEnumerable<Feedback> GetFeedbackReport(FeedbackReportFilterModel filterModel);
        IEnumerable<Feedback> GetFeedbacks();
        Task SubmitFeedbacksToExpert(SubmitFeedbacksRequestModel submitModel);
        Task UpdateFeedback(FeedbackBase feedbackBase, int feedbackId);

    }
}
