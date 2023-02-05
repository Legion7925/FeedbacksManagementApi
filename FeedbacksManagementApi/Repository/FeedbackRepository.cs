using AutoMapper;
using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper;
using FeedbacksManagementApi.Helper.Enums;
using FeedbacksManagementApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class FeedbackRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public FeedbackRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// دریافت لیست موارد
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public IEnumerable<Feedback> GetFeedbacks()
        {
            try
            {
                return context.Feedbacks.AsNoTracking().AsEnumerable();
            }
            catch (Exception)
            {
                throw new AppException("خطا در دریافت لیست مورد ها");
            }
        }
        /// <summary>
        /// گرفتن اطلاعات یک مورد
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task<Feedback?> GetFeedbackById(int feedbackId)
        {
            try
            {
                return await context.Feedbacks.FirstOrDefaultAsync(i => i.Id == feedbackId);
            }
            catch (Exception)
            {
                throw new AppException("خطا در دریافت مورد");
            }
        }
        /// <summary>
        /// اضافه کردن مورد جدید
        /// </summary>
        /// <param name="feedbackbase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddFeedback([FromBody] FeedbackBase feedbackbase)
        {
            try
            {
                var feedback = mapper.Map<Feedback>(feedbackbase);
                feedback.State = FeedbackState.ReadyToSend;
                feedback.SerialNumber = $"{DateTime.Now.Ticks}";
                feedback.Created = DateTime.Now;
                await context.Feedbacks.AddAsync(feedback);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("خطا در اضافه کردن مورد جدید");
            }
        }
        /// <summary>
        /// ویرایش مورد
        /// </summary>
        /// <param name="feedbackBase"></param>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateFeedback([FromBody] FeedbackBase feedbackBase, [FromRoute] int feedbackId)
        {
            try
            {
                var feedback = await GetFeedbackById(feedbackId);
                if (feedback == null) throw new AppException("مورد مورد نظر یافت نشد");
                feedback.Title = feedbackBase.Title;
                feedback.Description = feedbackBase.Description;
                feedback.Resources = feedbackBase.Resources;
                feedback.FkIdCustomer = feedbackBase.FkIdCustomer;
                feedback.Source = feedbackBase.Source;
                feedback.SourceAddress = feedbackBase.SourceAddress;
                feedback.Similarity = feedbackBase.Similarity;
                feedback.Priorty = feedbackBase.Priorty;
                feedback.Respond = feedbackBase.Respond;
            }
            catch (Exception)
            {
                throw new AppException("خطا در ویرایش مورد");
            }
        }
        /// <summary>
        /// تغییر وضعیت موارد ارسالی به وضعیت حذف شده
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteFeedbacks([FromRoute] int[] feedbackIds)
        {
            try
            {
                if (feedbackIds.Any())
                    throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

                await context.Feedbacks.Where(i => feedbackIds.Contains(i.Id))
                    .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Deleted));
            }
            catch (Exception)
            {
                throw new AppException("خطا در حذف مورد");
            }
        }
        /// <summary>
        /// ارسال یک یا چند مورد به متخصص
        /// </summary>
        /// <param name="feedbackIds"></param>
        /// <param name="expertId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task SubmitFeedbacksToExpert([FromBody] SubmitFeedbacksRequestModel submitModel)
        {
            try
            {
                if (!submitModel.FeedbackIds.Any())
                    throw new AppException("لیست موارد برای ارسال به متخصص نمیتواند خالی باشد");

                var expertExist = await context.Experts.AnyAsync(e => e.Id == submitModel.ExpertId);
                if(!expertExist)
                    throw new AppException("کد متخصص یافت نشد");

                await context.Feedbacks
                    .Where(i => submitModel.FeedbackIds.Contains(i.Id))
                    .ExecuteUpdateAsync(f => f
                    .SetProperty(p => p.State, p => FeedbackState.SentToExpert)
                    .SetProperty(p => p.ReferralDate, p => DateTime.Now));

                foreach (var item in submitModel.FeedbackIds)
                {
                    await context.ExpertFeedbacks.AddAsync(new ExpertFeedback
                    {
                        FkIdExpert = submitModel.ExpertId,
                        FkIdFeedback = item,
                        Description = submitModel.Description
                    });
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("خطا در ارسال موارد به متخصص");
            }
        }
        /// <summary>
        /// تغییر وضعیت موارد ارسالی به موارد بایگانی
        /// </summary>
        /// <param name="feedbackIds"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task ArchiveFeedbacks([FromRoute] int[] feedbackIds)
        {
            try
            {
                if (feedbackIds.Any())
                    throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

                await context.Feedbacks.Where(i => feedbackIds.Contains(i.Id))
                    .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Archived));
            }
            catch (Exception)
            {
                throw new AppException("خطا در حذف مورد");
            }
        }
        /// <summary>
        /// گزارش از موارد با فیلتر
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public IEnumerable<Feedback> GetFeedbackReport([FromBody] FeedbackReportFilterModel filterModel)
        {
            var feedbacks = context.Feedbacks.AsNoTracking();
            if (filterModel.ProductId is not 0) feedbacks =  feedbacks.Where(i => i.FkIdProduct == filterModel.ProductId);
            if (filterModel.CustomerId is not 0) feedbacks = feedbacks.Where(i => i.FkIdCustomer == filterModel.CustomerId);
            //if (filterModel.ExpertId is not 0) feedbacks = feedbacks.Include(e => e.Experts).Where(i => );
            if (filterModel.Tags is not null && filterModel.Tags.Count is not 0) 
                feedbacks =  feedbacks.Where(i => i.Tags == filterModel.Tags);
            if (filterModel.Source is not null) feedbacks = feedbacks.Where(i => i.Source == filterModel.Source);
            if (filterModel.Created is not null) feedbacks = feedbacks.Where(i => i.Created == filterModel.Created);
            if (filterModel.ReferralDate is not null) feedbacks = feedbacks.Where(i => i.ReferralDate == filterModel.ReferralDate);
            if (filterModel.RespondDate is not null) feedbacks = feedbacks.Where(i => i.RespondDate == filterModel.RespondDate);
            if (filterModel.Priorty is not null) feedbacks = feedbacks.Where(i => i.Priorty == filterModel.Priorty);
            if (filterModel.State is not null) feedbacks = feedbacks.Where(i => i.State == filterModel.State);
            if (!string.IsNullOrEmpty(filterModel.Description)) feedbacks = feedbacks.Where(i => i.Description == filterModel.Description);
            if (!string.IsNullOrEmpty(filterModel.Respond)) feedbacks = feedbacks.Where(i => i.Respond == filterModel.Respond);
            if (!string.IsNullOrEmpty(filterModel.Title)) feedbacks = feedbacks.Where(i => i.Title == filterModel.Title);
            if (!string.IsNullOrEmpty(filterModel.SerialNumber)) feedbacks = feedbacks.Where(i => i.SerialNumber == filterModel.SerialNumber);
            return feedbacks.AsEnumerable();
        }
    }
}
