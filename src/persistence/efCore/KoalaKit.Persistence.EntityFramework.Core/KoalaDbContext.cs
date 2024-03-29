﻿using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore
{
    public class KoalaDbContext : DbContext
    {
        //TODO: enhance
        private const string DefaultSchema = "dbo";
        private const string DefaultMigrationsHistoryTable = "__EFMigrationsHistory";
        public KoalaDbContext(DbContextOptions options) : base(options) { }
        
        public static string Schema => DefaultSchema;
        public static string MigrationsHistoryTable => DefaultMigrationsHistoryTable;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //if (!string.IsNullOrWhiteSpace(Schema))
            //    modelBuilder.HasDefaultSchema(Schema);
            foreach (var dbEntityProvider in EntityProvidersCollection.List)
                dbEntityProvider.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
