﻿using AutoMapper;
using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper;
using FeedbacksManagementApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class CasesRepository : ICasesRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public CasesRepository(FeedbacksDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست مورد های ثبت شده
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Case> GetCases()
        {
            try
            {
                return context.Cases.AsNoTracking().AsEnumerable();
            }
            catch (Exception)
            {
                throw new AppException("خطا در دریافت لیست مورد ها");
            }
        }
        /// <summary>
        /// دریافت یک مورد بر اساس آیدی
        /// </summary>
        /// <param name="caseId">آیدی مورد</param>
        /// <returns></returns>
        public async Task<Case?> GetCaseById(int caseId)
        {
            try
            {
                return await context.Cases.FirstOrDefaultAsync(i => i.Id == caseId);
            }
            catch (Exception)
            {
                throw new AppException("خطا در دریافت مورد");
            }
        }
        /// <summary>
        /// اضافه کردن مورد جدید
        /// </summary>
        /// <param name="feedbackCase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddCase(CaseBase feedbackCase)
        {
            try
            {
                var @case = mapper.Map<Case>(feedbackCase);
                await context.Cases.AddAsync(@case);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("در ثبت مورد جدید مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }
        /// <summary>
        /// ویرایش مورد
        /// </summary>
        /// <param name="feedbackCase"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateCase(CaseBase feedbackCase, int caseId)
        {
            try
            {
                var feedback = await GetCaseById(caseId);
                if (feedback == null)
                {
                    throw new AppException("مورد یافت نشد");
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
                throw new AppException("در ویرایش مورد مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }
        /// <summary>
        /// حذف یک مورد
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteCase(int caseId)
        {
            try
            {
                var feedbackCase = await GetCaseById(caseId);
                if (feedbackCase == null)
                {
                    throw new AppException("مورد یافت نشد");
                }
                context.Cases.Remove(feedbackCase);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("در حذق مورد مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }
        /// <summary>
        /// حذف چندین مورد
        /// </summary>
        /// <param name="caseIds"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteMultipleCases(int[] caseIds)
        {
            try
            {
                await context.Cases.Where(i => caseIds.Contains(i.Id)).ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw new AppException("در حذف موارد مشکلی پیش آمده در صورت تکرار با پشتیبانی تماس بگیرید");
            }
        }
        /// <summary>
        /// ارسال مورد برای پاسخ دهی
        /// </summary>
        /// <param name="feedbackCase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task SubmitForRespond(CaseBase feedbackCase)
        {
            try
            {
                var feedback = mapper.Map<Feedback>(feedbackCase);
                feedback.State = Helper.Enums.FeedbackState.ReadyToSend;
                feedback.SerialNumber = $"{DateTime.Now.Ticks}";
                await context.Feedbacks.AddAsync(feedback);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException("خطا در ارسال برای پاسخ دهی");
            }
        }
    }
}
