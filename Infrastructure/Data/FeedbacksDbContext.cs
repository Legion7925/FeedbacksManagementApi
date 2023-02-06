using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FeedbacksDbContext : DbContext
    {
        public FeedbacksDbContext(DbContextOptions<FeedbacksDbContext> options)
            :base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Expert> Experts { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<User> Users { get;set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UserPages> UserPages { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<ExpertFeedback> ExpertFeedbacks { get; set; }
    }
}
