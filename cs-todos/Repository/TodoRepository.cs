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

        // Implementation of the GetTodoById method defined in the ITodoRepository interface
        public TodoItem GetTodoById(long id)
        {
            _logger.LogInformation("Fetching todo item with ID {Id} from database.", id); // Log information before fetching data
            var item = _context.TodoItems.Find(id); // Use the DataContext to find a TodoItem by its ID
            if (item == null)
            {
                _logger.LogWarning("Todo item with ID {Id} not found in database.", id); // Log a warning if the item is not found
            }
            return item; // Return the fetched item (or null if not found)
        }

        // Implementation of the AddTodoItem method defined in the ITodoRepository interface
        public void AddTodoItem(TodoItem item)
        {
            _logger.LogInformation("Adding new todo item to database."); // Log information before adding data
            _context.TodoItems.Add(item); // Use the DataContext to add a new TodoItem
            _context.SaveChanges(); // Save changes to the database
            _logger.LogInformation("New todo item added to database."); // Log information after adding data
        }

        // Implementation of the UpdateTodoItem method defined in the ITodoRepository interface
        public void UpdateTodoItem(TodoItem item)
        {
            _logger.LogInformation("Updating todo item with ID {Id} in database.", item.Id); // Log information before updating data
            _context.TodoItems.Update(item); // Use the DataContext to update an existing TodoItem
            _context.SaveChanges(); // Save changes to the database
            _logger.LogInformation("Todo item with ID {Id} updated in database.", item.Id); // Log information after updating data
        }

        // Implementation of the DeleteTodoItem method defined in the ITodoRepository interface
        public void DeleteTodoItem(long id)
        {
            _logger.LogInformation("Deleting todo item with ID {Id} from database.", id); // Log information before deleting data
            var item = _context.TodoItems.Find(id); // Use the DataContext to find a TodoItem by its ID
            if (item == null)
            {
                _logger.LogWarning("Todo item with ID {Id} not found in database.", id); // Log a warning if the item is not found
                return; // Return early if the item is not found
            }
            _context.TodoItems.Remove(item); // Use the DataContext to remove the TodoItem
            _context.SaveChanges(); // Save changes to the database
            _logger.LogInformation("Todo item with ID {Id} deleted from database.", id); // Log information after deleting data
        }
    }
}
