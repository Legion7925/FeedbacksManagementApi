using Domain.Entities;
using Domain.Shared.Enums;
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
        IEnumerable<FeedbackReport> GetFeedbackReport(FeedbackReportFilterModel filterModel);
        Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filterModel);
        IEnumerable<FeedbackReport> GetFeedbacks(int take, int skip, FeedbackState state);
        Task SubmitFeedbacksToExpert(SubmitFeedbacksRequestModel submitModel);
        Task UpdateFeedback(FeedbackBase feedbackBase, int feedbackId);
        Task<FeedbackReport> GetOneFeedback(int feedbackId);
        Task<int> GetFeedbacksCount();
        Task RecycleFeedbacks(int[] feedbackIds);
    }
}
