using AutoMapper;
using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class FeedbackRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public FeedbackRepository(FeedbacksDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

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

        public async Task AddFeedback([FromBody]FeedbackBase feedbackbase)
        {
            try
            {
                var feedback = mapper.Map<Feedback>(feedbackbase);
                await context.Feedbacks.AddAsync(feedback);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("خطا در اضافه کردن مورد جدید");
            }
        }

        public async Task UpdateFeedback([FromBody]FeedbackBase feedbackBase ,[FromRoute] int feedbackId)
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

            }
            catch (Exception)
            {
                throw new AppException("خطا در ویرایش مورد");
            }
        }
    }
}
