using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public ExpertRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست مورد های ثبت شده
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Expert> GetExperts()
        {
            return context.Experts.AsNoTracking().AsEnumerable();
        }
        /// <summary>
        /// دریافت یک مورد بر اساس آیدی
        /// </summary>
        /// <param name="expertId">آیدی مورد</param>
        /// <returns></returns>
        public async Task<Expert?> GetExpertById(int expertId)
        {

            return await context.Experts.FirstOrDefaultAsync(i => i.Id == expertId);
        }
        /// <summary>
        /// اضافه کردن کارشناس جدید
        /// </summary>
        /// <param name="expertBase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddExpert(ExpertBase expertBase)
        {
            if (expertBase.BirthDate > DateTime.Now)
                throw new AppException("تاریخ تولد وارد شده معتبر نیست");
            await ValidateForeignKeys(expertBase.FkIdExpertise);
            var expert = mapper.Map<Expert>(expertBase);
            expert.Created = DateTime.Now;
            await context.Experts.AddAsync(expert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ویرایش کارشناس
        /// </summary>
        /// <param name="expertBase"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateExpert(ExpertBase expertBase, int caseId)
        {
            await ValidateForeignKeys(expertBase.FkIdExpertise);
            var expert = await GetExpertById(caseId);
            if (expert == null)
            {
                throw new AppException("کارشناس مورد نظر یافت نشد");
            }

            mapper.Map(expertBase, expert);

            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف کارشناس
        /// </summary>
        /// <param name="expertId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteExpert(int expertId)
        {
            var expert = await GetExpertById(expertId);
            if (expert == null)
            {
                throw new AppException("کارشناس مورد نظر یافت نشد");
            }
            context.Experts.Remove(expert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// اعتبارسنجی کلید های خارجی موجودیت
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        private async Task ValidateForeignKeys(int specialtyId)
        {
            var customerExist = await context.Specialties.AnyAsync(i => i.Id == specialtyId);
            if (customerExist is not true) throw new AppException("کد تخصص یافت نشد");
        }
    }
}
