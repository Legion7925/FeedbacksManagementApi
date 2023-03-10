using FeedbackManagementWeb.Helper;
using FeedbackManagementWeb.Interface;

namespace FeedbackManagementWeb.Services
{
    public class CaseService : ICaseService
    {
        public async Task<IEnumerable<CaseReport>> GetCases(int take , int skip)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetCasesAsync(skip , take);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت مورد های رسیده");
                //todo log error
            }
        }

        public async Task<int> GetCasesCount()
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetCasesCountAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در تعداد");
                //todo log error
            }
        }

        public async Task AddCase(CaseBase @case)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.AddCaseAsync(@case);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در ثبت مورد جدید");
                //todo log error
            }
        }

        public async Task UpdateCase(CaseBase @case, int caseId)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.UpdateCaseAsync(caseId, @case);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در ویرایش مورد");
                //todo log error
            }
        }

        public async Task DeleteCases(int[] caseIds)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.DeleteMultipleCasesAsync(caseIds);
                }
            }
            catch (Exception)
            {
                throw new Exception("خطا در حذف مورد");
                //todo log error
            }
        }

        public async Task SubmitCaseForAnswer(int caseId)
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    await client.SubmitCaseForAnswerAsync(caseId);
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
