using cs_todos.Interfaces; // Importing the namespace where the ITodoRepository interface is defined
using cs_todos.Models; // Importing the namespace where the TodoItem model is defined
using Microsoft.AspNetCore.Mvc; // Importing the namespace for ASP.NET Core MVC functionalities
using Microsoft.Extensions.Logging; // Importing the logging library

[Route("api/[controller]")] // This attribute defines the route template for the controller
[ApiController] // This attribute indicates that this is an API controller
public class TodoItemController : ControllerBase
{
    // Private readonly fields to hold references to the ITodoRepository and ILogger
    private readonly ITodoRepository _todoRepository;
    private readonly ILogger<TodoItemController> _logger;

    // Constructor that takes ITodoRepository and ILogger as parameters
    public TodoItemController(ITodoRepository todoRepository, ILogger<TodoItemController> logger)
    {
        _todoRepository = todoRepository;
        _logger = logger;
    }

    // Define a GET endpoint for api/todoitem
    // This method returns a list of TodoItem objects
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoItem>))] // This attribute specifies the response type and status code
    public IActionResult GetTodos()
    {
        _logger.LogInformation("GetTodos endpoint called."); // Log that the endpoint was called

        var items = _todoRepository.GetTodos(); // Use the repository to get all todo items
        _logger.LogInformation("Retrieved {Count} todo items", items.Count); // Log the count of retrieved items

        // Check if the model state is valid
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model state is invalid."); // Log a warning if the model state is invalid
            return BadRequest(ModelState); // Return a 400 Bad Request response
        }

        return Ok(items); // Return a 200 OK response with the list of todo items
    }
}
