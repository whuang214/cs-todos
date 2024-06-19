using cs_todos.Data; // Importing the namespace where the DataContext is defined
using cs_todos.Interfaces; // Importing the namespace where the ITodoRepository interface is defined
using cs_todos.Repository; // Importing the namespace where the TodoRepository is defined
using Microsoft.EntityFrameworkCore; // Importing the namespace for Entity Framework Core functionalities
using Microsoft.Extensions.Logging; // Importing the logging library

var builder = WebApplication.CreateBuilder(args); // Creating a WebApplication builder with the given arguments

// Add services to the container.
builder.Services.AddControllers(); // Adding controllers to the services collection
builder.Services.AddEndpointsApiExplorer(); // Adding endpoint API explorer for Swagger
builder.Services.AddSwaggerGen(); // Adding Swagger for API documentation

// Register the repository and database context
builder.Services.AddScoped<ITodoRepository, TodoRepository>(); // Registering the ITodoRepository and its implementation TodoRepository
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Registering the DataContext with a SQL Server connection string

// Configure logging
builder.Logging.ClearProviders(); // Clearing existing logging providers
builder.Logging.AddConsole(); // Adding console logging

var app = builder.Build(); // Building the WebApplication

// Seed the database
using (var scope = app.Services.CreateScope()) // Creating a scope for the services
{
    var services = scope.ServiceProvider; // Getting the service provider from the scope
    var logger = services.GetRequiredService<ILogger<Program>>(); // Getting the logger service
    try
    {
        logger.LogInformation("Seeding the database..."); // Logging the start of the seeding process
        SeedData.Initialize(services); // Initializing the seed data
        logger.LogInformation("Database seeding completed."); // Logging the completion of the seeding process
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the database."); // Logging any errors that occur during seeding
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) // Checking if the environment is development
{
    app.UseSwagger(); // Enabling Swagger
    app.UseSwaggerUI(); // Enabling Swagger UI
}

app.UseHttpsRedirection(); // Enabling HTTPS redirection
app.UseAuthorization(); // Enabling authorization

app.MapControllers(); // Mapping the controllers to endpoints
app.Run(); // Running the application
