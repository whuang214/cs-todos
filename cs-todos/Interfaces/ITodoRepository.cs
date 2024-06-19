using cs_todos.Models; // Importing the namespace where the TodoItem model is defined

namespace cs_todos.Interfaces
{
    // Define an interface for the Todo repository
    public interface ITodoRepository
    {
        ICollection<TodoItem> GetTodos(); // Declare a method to get all TodoItems
        TodoItem GetTodoById(long id); // Declare a method to get a TodoItem by its ID
        void AddTodoItem(TodoItem item); // Declare a method to add a new TodoItem
        void UpdateTodoItem(TodoItem item); // Declare a method to update an existing TodoItem
        void DeleteTodoItem(long id); // Declare a method to delete a TodoItem by its ID

    }
}

