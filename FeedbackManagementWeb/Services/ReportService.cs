using Domain.Entities;
using FeedbackManagementWeb.Helper;

namespace FeedbackManagementWeb.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Product>> GetProdcuts();
        Task<IEnumerable<Specialty>> GetSpecialties();
    }

    public class ReportService : IReportService
    {
        public async Task<IEnumerable<Product>> GetProdcuts()
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetProductsAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت لیست محصولات");
                //todo log error
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetCustomersAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت مورد های رسیده");
                //todo log error
            }
        }
        public async Task<IEnumerable<Specialty>> GetSpecialties()
        {
            try
            {
                using (var http = new HttpClient())
                {

                    var client = new swaggerClient(AppSettings.ApiBaseUrl, http);
                    var response = await client.GetSpecialtiesAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در دریافت مورد گروه متخصصین");
                //todo log error
            }
        }
    }
}
