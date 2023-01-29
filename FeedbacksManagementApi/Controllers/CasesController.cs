using FeedbacksManagementApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        public CasesController()
        {
            
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCases()
        //{
        //    return Ok();  
        //}
    }
}
