

using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Infrastructure.Persistence.Contexts
{
   public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public DbSet<Publications> Publications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api


            //Table Creations

            #region table  
            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Friends>().ToTable("Friends");
            modelBuilder.Entity<Publications>().ToTable("Publications");
            #endregion


            //Configuration primary keys

            #region keys
            modelBuilder.Entity<Comments>().HasKey(e => e.Id);
            modelBuilder.Entity<Friends>().HasKey(e => e.Id);
            modelBuilder.Entity<Publications>().HasKey(e => e.Id);         
            #endregion


            //Relationships

            #region relationships
            modelBuilder.Entity<Comments>()
                .HasOne(a => a.Publications)
                .WithMany(a => a.Comments)
                .HasForeignKey(a => a.PublicationsId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

    }
}
