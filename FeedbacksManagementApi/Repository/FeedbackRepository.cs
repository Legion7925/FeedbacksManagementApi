using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared.Enums;
using FeedbacksManagementApi.Model;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository;

public class FeedbackRepository : IFeedbackRepository
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
    public IEnumerable<FeedbackReport> GetFeedbacks(int take, int skip , FeedbackState state)
    {
        return context.Feedbacks.AsNoTracking().Where(i=>i.State == state)
            .Skip(skip).Take(take).Include(c => c.Customer).Include(c => c.Product)
                .Select(c => new FeedbackReport
                {
                    CustomerName = c.Customer!.NameAndFamily,
                    Description = c.Description,
                    FkIdCustomer = c.FkIdCustomer,
                    Id = c.Id,
                    ProductName = c.Product!.Name,
                    Resources = c.Resources,
                    Source = c.Source,
                    SourceAddress = c.SourceAddress,
                    Title = c.Title,
                    FkIdProduct = c.FkIdProduct,
                    RespondDate = c.RespondDate,
                    Created = c.Created,
                    Priorty = c.Priorty,
                    Respond = c.Respond,
                    ReferralDate = c.ReferralDate,
                    Similarity = c.Similarity,
                    SerialNumber = c.SerialNumber ,
                    State = c.State,
                });

    }
    /// <summary>
    /// تعداد مورد های رسیده
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetFeedbacksCount()
    {
        return await context.Feedbacks.CountAsync();
    }
    /// <summary>
    /// دریافت یک مورد بر اساس آیدی
    /// </summary>
    /// <param name="feedbackId">آیدی مورد</param>
    /// <returns></returns>
    public async Task<FeedbackReport> GetOneFeedback(int feedbackId)
    {
        var feedback = await context.Feedbacks.Include(i => i.Customer).Include(i => i.Product).FirstOrDefaultAsync(i => i.Id == feedbackId);
        if (feedback == null) throw new AppException("مورد یافت نشد");

        var foundFeedback = mapper.Map<FeedbackReport>(feedback);
        foundFeedback.ProductName = feedback.Product?.Name ?? "نامشخص";
        foundFeedback.CustomerName = feedback.Customer?.NameAndFamily ?? "نامشخص";

        return foundFeedback;
    }
    /// <summary>
    /// گرفتن اطلاعات یک مورد
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task<Feedback?> GetFeedbackById(int feedbackId)
    {
        return await context.Feedbacks.FirstOrDefaultAsync(i => i.Id == feedbackId);
    }
    /// <summary>
    /// اضافه کردن مورد جدید
    /// </summary>
    /// <param name="feedbackBase"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task AddFeedback(FeedbackBase feedbackBase)
    {
        await ValidateForeignKeys(feedbackBase.FkIdCustomer, feedbackBase.FkIdProduct);
        var feedback = mapper.Map<Feedback>(feedbackBase);
        feedback.State = FeedbackState.ReadyToSend;
        feedback.SerialNumber = $"{DateTime.Now.Ticks}";
        feedback.Created = DateTime.Now;
        await context.Feedbacks.AddAsync(feedback);
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// ویرایش مورد
    /// </summary>
    /// <param name="feedbackBase"></param>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task UpdateFeedback(FeedbackBase feedbackBase, int feedbackId)
    {
        await ValidateForeignKeys(feedbackBase.FkIdCustomer, feedbackBase.FkIdProduct);
        var feedback = await GetFeedbackById(feedbackId);
        if (feedback == null) throw new AppException("مورد مورد نظر یافت نشد");
        feedback.Title = feedbackBase.Title;
        feedback.Description = feedbackBase.Description;
        feedback.Resources = feedbackBase.Resources;
        feedback.FkIdCustomer = feedbackBase.FkIdCustomer;
        feedback.Source = feedbackBase.Source;
        feedback.SourceAddress = feedbackBase.SourceAddress;
        feedback.Priorty = feedbackBase.Priorty;
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// تغییر وضعیت موارد ارسالی به وضعیت حذف شده
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task DeleteFeedbacks(int[] feedbackIds)
    {
        if (feedbackIds.Any() is not true)
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.Feedbacks.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Deleted));
    }
    /// <summary>
    /// ارسال یک یا چند مورد به متخصص
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <param name="expertId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task SubmitFeedbacksToExpert(SubmitFeedbacksRequestModel submitModel)
    {
        if (!submitModel.FeedbackIds.Any())
            throw new AppException("لیست موارد برای ارسال به متخصص نمیتواند خالی باشد");

        var expertExist = await context.Experts.AnyAsync(e => e.Id == submitModel.ExpertId);
        if (!expertExist)
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
    /// <summary>
    /// تغییر وضعیت موارد ارسالی به موارد بایگانی
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task ArchiveFeedbacks(int[] feedbackIds)
    {
        if (!feedbackIds.Any())
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.Feedbacks.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Archived));
    }
    /// <summary>
    /// بازیابی موارد
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <returns></returns>
    public async Task RecycleFeedbacks(int[] feedbackIds)
    {
        if (!feedbackIds.Any())
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.Feedbacks.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.ReadyToSend));
    }
    /// <summary>
    /// گزارش از موارد با فیلتر
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public IEnumerable<FeedbackReport> GetFeedbackReport(FeedbackReportFilterModel filterModel)
    {
        var feedbacks = context.Feedbacks.AsNoTracking();
        if (filterModel.ProductId is not 0) feedbacks = feedbacks.Where(i => i.FkIdProduct == filterModel.ProductId);
        if (filterModel.CustomerId is not 0) feedbacks = feedbacks.Where(i => i.FkIdCustomer == filterModel.CustomerId);
        //if (filterModel.ExpertId is not 0) feedbacks = feedbacks.Include(e => e.Experts).Where(i => );
        if (filterModel.Tags is not null && filterModel.Tags.Count is not 0)
            feedbacks = feedbacks.Where(i => i.Tags == filterModel.Tags);
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
        return feedbacks.Skip(filterModel.Skip).Take(filterModel.Take).Select(c => new FeedbackReport
        {
            CustomerName = c.Customer!.NameAndFamily,
            Description = c.Description,
            FkIdCustomer = c.FkIdCustomer,
            Id = c.Id,
            ProductName = c.Product!.Name,
            Resources = c.Resources,
            Source = c.Source,
            SourceAddress = c.SourceAddress,
            Title = c.Title,
            FkIdProduct = c.FkIdProduct,
            RespondDate = c.RespondDate,
            Created = c.Created,
            Priorty = c.Priorty,
            Respond = c.Respond,
            ReferralDate = c.ReferralDate,
            Similarity = c.Similarity,
            SerialNumber = c.SerialNumber,
            State = c.State,
        });
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

}
