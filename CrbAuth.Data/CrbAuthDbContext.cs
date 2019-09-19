using System;
using CrbAuth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrbAuth.Data
{
    public class CrbAuthDbContext : DbContext
    {
        private ILoggerFactory _loggerFactory;
        public CrbAuthDbContext(DbContextOptions<CrbAuthDbContext> options) : base(options)
        {
        }

        public CrbAuthDbContext()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name,
                    LogLevel.Information));
            _loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging(true);
        }
    }
}
