using KoalaKit.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EfCore.Test.Models
{
    internal class TestDbEntityProvider : IDbEntityProvider
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestModelParent>(entity =>
            {
                entity.ToTable("Parents");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.HasMany(p => p.Children).WithOne(p => p.Parent).HasForeignKey(p => p.ParentId).IsRequired();
            });

            modelBuilder.Entity<TestModelChild>(entity =>
            {
                entity.ToTable("Children");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.ParentId).IsRequired();
            });
        }
    }
}
