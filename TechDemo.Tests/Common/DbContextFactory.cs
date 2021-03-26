using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechDemo.Core.Domain;
using TechDemo.Core.Infrastructure.Persisence;

namespace TechDemo.Tests.Common
{
    internal class DbContextFactory
    {

        public static ApplicationContext Create(bool asNoTracking = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            if (asNoTracking)
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }


            var context = new ApplicationContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            SeedData(context).Wait();
            if (asNoTracking)
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }
            }

            return context;
        }

        public static void Destroy(ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async static Task SeedData(ApplicationContext context)
        {
            var users = Enumerable.Range(1, 5).Select(i =>
                   new User
                   {
                       Name = $"Name{i}",
                       Password = $"password{i}",
                       Email = $"example{i}@example.com",
                       DateOfBirth = DateTime.Parse("2000-08-18T07:22:16.0000000-07:00").AddDays(i),
                       Photo = $"photo_{i}.jpg"
                   }).ToList();

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}

