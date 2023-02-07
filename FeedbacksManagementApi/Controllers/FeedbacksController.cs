using Domain.Entities;
using Domain.Interfaces;
using FeedbacksManagementApi.Model;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> GetFeedbacks()
        {
            try
            {
                return Ok(feedbackRepository.GetFeedbacks());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در دریافت موارد");
            }
        }

        [HttpGet]
        [Route("feedbackId")]
        public async Task<IActionResult> GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedback = await feedbackRepository.GetFeedbackById(feedbackId);

                if (feedback == null)
                    return NoContent();

                return Ok(feedback);
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در دریافت موارد");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackBase feedbackBase)
        {
            try
            {
                await feedbackRepository.AddFeedback(feedbackBase);
                return Ok("مورد جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در اضافه کردن مورد جدید");
            }
        }

        [HttpPut]
        [Route("feedbackId")]
        public async Task<IActionResult> EditFeedback([FromBody] FeedbackBase feedbackBase, int feedbackId)
        {
            try
            {
                await feedbackRepository.UpdateFeedback(feedbackBase, feedbackId);
                return Ok("ویرایش مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در ویرایش مورد");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedbacks(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.DeleteFeedbacks(feedbackIds);
                return Ok("حذف مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch(Exception)
            {
                return BadRequest("خطا در حذف مورد");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedbacksToExpert([FromBody] SubmitFeedbacksRequestModel submitFeedbackModel)
        {
            try
            {
                await feedbackRepository.SubmitFeedbacksToExpert(submitFeedbackModel);
                return Ok("مورد با موفقیت برای کارشناس ارسال شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در ارسال مورد برای کارشناس");
            }
        }

        [HttpPost] 
        public async Task<IActionResult> ArchiveFeedbacks(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.ArchiveFeedbacks(feedbackIds);
                return Ok("موارد ارسالی با موفقیت بایگانی شدند");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در بایگانی موارد");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<Feedback>> GetFeedbackReports([FromBody]FeedbackReportFilterModel filterModel)
        {
            try
            {
                var report = feedbackRepository.GetFeedbackReport(filterModel);
                return Ok(report);
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در دریافت گزارش");
            }
        }
    }
}
