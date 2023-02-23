using FeedbackManagementWeb.Helper;
using FeedbackManagementWeb.Interface;

namespace FeedbackManagementWeb.Services
{
    public class FeedbackService : IFeedbackService
    {
        public async Task<IEnumerable<FeedbackReport>> GetFeedbacks(int take, int skip , FeedbackState state)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetFeedbacksAsync(take,skip ,state);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت مورد ها");
                //todo log error
            }
        }
        public async Task<int> GetFeedbackCount()
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetFeedbacksCountAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت تعداد");
                //todo log error
            }
        }

        public async Task<IEnumerable<FeedbackReport>> GetFeedbackReport(FeedbackReportFilterModel filter)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetFeedbackReportsAsync(filter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت مورد های رسیده");
                //todo log error
            }
        }

        public async Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filter)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetFeedbackReportCountAsync(filter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت تعداد");
                //todo log error
            }
        }

        public async Task AddFeedback(FeedbackBase feedback)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.AddFeedbackAsync(feedback);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در ثبت مورد جدید");
                //todo log error
            }
        }

        public async Task UpdateFeedback(FeedbackBase feedback, int feedbackId)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.FeedbackId2Async(feedbackId, feedback);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در ویرایش مورد");
                //todo log error
            }
        }

        public async Task DeleteFeedbacks(int[] feedbackIds)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.DeleteFeedbacksAsync(feedbackIds);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در حذف مورد");
                //todo log error
            }
        }

        public async Task ArchiveFeedbacks(int[] feedbackIds)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.ArchiveFeedbacksAsync(feedbackIds);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در بایگانی مورد");
                //todo log error
            }
        }

        public async Task RecycleFeedbacks(int[] feedbackIds)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.RecycleFeedbacksAsync(feedbackIds);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در بازیابی مورد");
                //todo log error
            }
        }

        public async Task SubmitFeedbacksToExpert(SubmitFeedbacksRequestModel feedbackRequest)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.SubmitFeedbacksToExpertAsync(feedbackRequest);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در حذف مورد");
                //todo log error
            }
        }
    }
}
