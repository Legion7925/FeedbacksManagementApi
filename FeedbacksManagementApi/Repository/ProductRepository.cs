using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FeedbacksDbContext context;

        public ProductRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetPorducts()
        {
            return context.Products.AsNoTracking().AsEnumerable();
        }
    }
}
