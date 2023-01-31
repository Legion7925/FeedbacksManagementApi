using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper;
using FeedbacksManagementApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly ICasesRepository caseRepository;

        public CasesController(ICasesRepository caseRepository)
        {
            this.caseRepository = caseRepository;
        }

        [HttpGet]
        public IActionResult GetCases()
        {
            try
            {
                return Ok(caseRepository.GetCases());
            }
            catch(AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCase([FromBody]CaseBase @case)
        {
            try
            {
                await caseRepository.AddCase(@case);
                return Ok("مورد جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            try
            {
                return Ok(await caseRepository.GetCaseById(id));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }

        [HttpPut]
        [Route("{caseId}")]
        public async Task<IActionResult> UpdateCase([FromBody]CaseBase @case , [FromRoute] int caseId)
        {
            try
            {
                await caseRepository.UpdateCase(@case, caseId);
                return Ok("ویرایش مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }

        [HttpDelete]
        [Route("{caseId}")]
        public async Task<IActionResult> DeleteCase([FromRoute] int caseId)
        {
            try
            {
                await caseRepository.DeleteCase(caseId);
                return Ok("حذف مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultipleCases([FromBody] int[] caseIds)
        {
            try
            {
                await caseRepository.DeleteMultipleCases(caseIds);
                return Ok("حذف موارد ارسالی با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
        }
    }
}
