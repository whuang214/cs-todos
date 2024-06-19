using Microsoft.EntityFrameworkCore; // Importing the Entity Framework Core library
using cs_todos.Models; // Importing the namespace where the TodoItem model is defined

namespace cs_todos.Data
{
    // The DataContext class is derived from DbContext, which is a part of Entity Framework Core
    public class DataContext : DbContext
    {
        // Constructor that takes DbContextOptions and passes it to the base DbContext class
        // DbContextOptions allows configuration of the DataContext, like which database to use
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) // Pass the options into the superclass (DbContext)
        {
        }

        // Define a DbSet property for the TodoItem model
        // DbSet<TodoItem> represents the collection of TodoItems that can be queried from the database
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
