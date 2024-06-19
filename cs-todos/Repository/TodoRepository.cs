using cs_todos.Data; // Importing the namespace where the DataContext is defined
using cs_todos.Interfaces; // Importing the namespace where the ITodoRepository interface is defined
using cs_todos.Models; // Importing the namespace where the TodoItem model is defined
using Microsoft.Extensions.Logging; // Importing the logging library
using System.Collections.Generic; // Importing the collection library
using System.Linq; // Importing LINQ for querying collections

namespace cs_todos.Repository
{
    // Implementing the ITodoRepository interface in the TodoRepository class
    public class TodoRepository : ITodoRepository
    {
        // Private readonly fields to hold references to the DataContext and ILogger
        private readonly DataContext _context;
        private readonly ILogger<TodoRepository> _logger;

        // Constructor that takes DataContext and ILogger as parameters
        public TodoRepository(DataContext context, ILogger<TodoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Implementation of the GetTodos method defined in the ITodoRepository interface
        public ICollection<TodoItem> GetTodos()
        {
            _logger.LogInformation("Fetching todo items from database."); // Log information before fetching data
            var items = _context.TodoItems.ToList(); // Use the DataContext to fetch all TodoItems from the database
            _logger.LogInformation("Retrieved {Count} todo items from database.", items.Count); // Log the count of retrieved items
            return items; // Return the fetched items
        }
    }
}
