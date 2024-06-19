using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using cs_todos.Models;

namespace cs_todos.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any TodoItems.
                if (context.TodoItems.Any())
                {
                    return;   // DB has been seeded
                }

                context.TodoItems.AddRange(
                    new TodoItem
                    {
                        Name = "Learn ASP.NET Core",
                        IsComplete = false
                    },
                    new TodoItem
                    {
                        Name = "Build a TODO API",
                        IsComplete = false
                    },
                    new TodoItem
                    {
                        Name = "Explore Entity Framework Core",
                        IsComplete = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
