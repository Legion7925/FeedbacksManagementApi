using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared.Enums;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class CasesRepository : ICasesRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public CasesRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست مورد های ثبت شده
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CaseReport> GetCases(int take, int skip)
        {
            var cases = context.Cases.AsNoTracking().Skip(skip).Take(take)
                .Include(c => c.Customer).Include(c => c.Product)
                .Select(c => new CaseReport
                {
                    CustomerName = c.Customer!.NameAndFamily,
                    Description = c.Description,
                    FkIdCustomer = c.FkIdCustomer,
                    Id = c.Id,
                    ProductName = c.Product!.Name,
                    Resources= c.Resources,
                    Source = c.Source,
                    SourceAddress= c.SourceAddress,
                    Title= c.Title,
                    FkIdProduct = c.FkIdProduct, 
                });
            return cases;
        }
        /// <summary>
        /// تعداد مورد های رسیده
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCasesCount()
        {
            return await context.Cases.CountAsync();
        }
        /// <summary>
        /// دریافت یک مورد بر اساس آیدی
        /// </summary>
        /// <param name="caseId">آیدی مورد</param>
        /// <returns></returns>
        public async Task<CaseReport> GetOneCase(int caseId)
        {
            var @case = await context.Cases.Include(i=> i.Customer).Include(i=>i.Product).FirstOrDefaultAsync(i => i.Id == caseId);
            if (@case == null) throw new AppException("مورد یافت نشد");

            var foundCase = mapper.Map<CaseReport>(@case);
            foundCase.ProductName = @case.Product?.Name ?? "نامشخص";
            foundCase.CustomerName = @case.Customer?.NameAndFamily ?? "نامشخص";

            return foundCase;
        }
        /// <summary>
        /// اضافه کردن مورد جدید
        /// </summary>
        /// <param name="feedbackCase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddCase(CaseBase feedbackCase)
        {
            await ValidateForeignKeys(feedbackCase.FkIdCustomer, feedbackCase.FkIdProduct);
            var @case = mapper.Map<Case>(feedbackCase);
            await context.Cases.AddAsync(@case);
            await context.SaveChangesAsync();
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
            await ValidateForeignKeys(feedbackCase.FkIdCustomer, feedbackCase.FkIdProduct);
            var feedback = await GetCaseById(caseId);

            feedback.Title = feedbackCase.Title;
            feedback.Description = feedbackCase.Description;
            feedback.Resources = feedbackCase.Resources;
            feedback.Source = feedbackCase.Source;
            feedback.SourceAddress = feedbackCase.SourceAddress;

            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف یک مورد
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteCase(int caseId)
        {
            var feedbackCase = await GetCaseById(caseId);
            if (feedbackCase == null)
            {
                throw new AppException("مورد یافت نشد");
            }
            context.Cases.Remove(feedbackCase);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف چندین مورد
        /// </summary>
        /// <param name="caseIds"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteMultipleCases(int[] caseIds)
        {
            await context.Cases.Where(i => caseIds.Contains(i.Id)).ExecuteDeleteAsync();
        }
        /// <summary>
        /// ارسال مورد به جدول فیدبک
        /// </summary>
        /// <param name="feedbackCase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task SubmitForRespond(int caseId)
        {
            var feedbackCase = await GetCaseById(caseId);
            if (feedbackCase == null)
            {
                throw new AppException("مورد یافت نشد");
            }
            var feedback = mapper.Map<Feedback>(feedbackCase);
            feedback.State = FeedbackState.ReadyToSend;
            feedback.SerialNumber = $"{DateTime.Now.Ticks}";
            feedback.Created = DateTime.Now;
            await context.Feedbacks.AddAsync(feedback);
            context.Cases.Remove(feedbackCase);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// اعتبارسنجی وجود آیدی مشتری و محصول
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        private async Task ValidateForeignKeys(int customerId, int productId)
        {
            var customerExist = await context.Customers.AnyAsync(i => i.Id == customerId);
            if (customerExist is not true) throw new AppException("کد مشتری یافت نشد");
            var productExist = await context.Products.AnyAsync(i => i.Id == productId);
            if (productExist is not true) throw new AppException("کد محصول یافت نشد");
        }
        /// <summary>
        /// دریافت یک مورد بدون شامل کردن ارتباطات دیتابیسی این تابع فقط در داخل این  
        /// کلاس برای حذف و ویرایش مورد استفاده قرار میگیرد
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        private async Task<Case> GetCaseById(int caseId)
        {
            var @case = await context.Cases.FirstOrDefaultAsync(i => i.Id == caseId);
            if (@case is null) throw new AppException("مورد یافت نشد");
            return @case;
        }
    }
}
