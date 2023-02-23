namespace FeedbackManagementWeb.Interface
{
    public interface IFeedbackService
    {
        Task AddFeedback(FeedbackBase feedback);
        Task ArchiveFeedbacks(int[] feedbackIds);
        Task DeleteFeedbacks(int[] feedbackIds);
        Task<int> GetFeedbackCount();
        Task<IEnumerable<FeedbackReport>> GetFeedbackReport(FeedbackReportFilterModel filter);
        Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filter);
        Task<IEnumerable<FeedbackReport>> GetFeedbacks(int take, int skip,FeedbackState state);
        Task SubmitFeedbacksToExpert(SubmitFeedbacksRequestModel feedbackRequest);
        Task UpdateFeedback(FeedbackBase feedback, int feedbackId);
        Task RecycleFeedbacks(int[] feedbackIds);
    }
}
