using AutoMapper;
using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class CasesRepository
    {
        private readonly FeedbacksDbContext context;

        public CasesRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// دریافت لیست مورد های ثبت شده
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Case> GetCases()
        {
            return context.Cases.AsNoTracking().AsEnumerable();
        }

        public async Task<Case?> GetCaseById(int caseId)
        {
           return await context.Cases.FirstOrDefaultAsync(i => i.Id == caseId);
        }

        public async Task AddCase(Case feedbackCase)
        {
            try
            {
                context.Cases.Add(feedbackCase);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("در ثبت مورد جدید مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }

        public async Task UpdateCase(Case feedbackCase , int caseId)
        {
            try
            {
                var feedback = await GetCaseById(caseId);
                if(feedback == null)
                {
                    throw new AppException("مورد نظر یافت نشد");
                }

                feedback.Title = feedbackCase.Title;
                feedback.Description = feedbackCase.Description;
                feedback.Resources = feedbackCase.Resources;
                feedback.Source = feedbackCase.Source;
                feedback.SourceAddress = feedbackCase.SourceAddress;

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("در ویرایش مورد جدید مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }
    }
}
