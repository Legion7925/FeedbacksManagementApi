using Domain.Entities;
using Domain.Interfaces;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                return Ok(customerRepository.GetCustomers());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در دریافت لیست مشتری ها");
            }
        }

    }
}
