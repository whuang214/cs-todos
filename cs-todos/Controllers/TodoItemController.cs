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

        try
        {
            var items = _todoRepository.GetTodos(); // Use the repository to get all todo items
            _logger.LogInformation("Retrieved {Count} todo items", items.Count); // Log the count of retrieved items
            return Ok(items); // Return a 200 OK response with the list of todo items
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching todo items."); // Log the error
            return StatusCode(500, "Internal server error"); // Return a 500 Internal Server Error response
        }
    }

    // Define a GET endpoint for api/todoitem/{id}
    // This method returns a single TodoItem object by ID
    [HttpGet("{id}")] // This attribute specifies the route template with a parameter
    [ProducesResponseType(200, Type = typeof(TodoItem))] // This attribute specifies the response type and status code
    [ProducesResponseType(404)] // This attribute specifies another possible response status code
    public IActionResult GetTodo(long id)
    {
        _logger.LogInformation("GetTodo endpoint called with ID {Id}.", id); // Log that the endpoint was called with an ID

        try
        {
            var item = _todoRepository.GetTodoById(id); // Use the repository to get a todo item by ID
            if (item == null)
            {
                _logger.LogWarning("Todo item with ID {Id} not found.", id); // Log a warning if the item is not found
                return NotFound(new { Message = $"Todo item with ID {id} not found." }); // Return a 404 Not Found response with a custom message
            }
            return Ok(item); // Return a 200 OK response with the todo item
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the todo item."); // Log the error
            return StatusCode(500, "Internal server error"); // Return a 500 Internal Server Error response
        }
    }

    // Define a POST endpoint for api/todoitem
    // This method adds a new TodoItem to the database
    [HttpPost] // This attribute specifies the HTTP method
    [ProducesResponseType(201)] // This attribute specifies the response status code
    [ProducesResponseType(400)] // This attribute specifies another possible response status code
    public IActionResult AddTodoItem([FromBody] TodoItem item) // FromBody attribute binds the request body to the parameter
    {
        _logger.LogInformation("AddTodoItem endpoint called."); // Log that the endpoint was called

        if (item == null)
        {
            _logger.LogWarning("Request body is null."); // Log a warning if the request body is null
            return BadRequest(new { Message = "Request body is null." }); // Return a 400 Bad Request response with a custom message
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model state is invalid."); // Log a warning if the model state is invalid
            return BadRequest(ModelState); // Return a 400 Bad Request response with model validation errors
        }

        try
        {
            _todoRepository.AddTodoItem(item); // Use the repository to add a new todo item
            return CreatedAtAction(nameof(GetTodo), new { id = item.Id }, item); // Return a 201 Created response with the added todo item
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding the todo item."); // Log the error
            return StatusCode(500, "Internal server error"); // Return a 500 Internal Server Error response
        }
    }
}
