using AzureProjectMagdalenaGorska.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICarCosmosService>(options =>
{
    string url = builder.Configuration.GetSection("AzureCosmosDbSettings")
    .GetValue<string>("URL");
    string primaryKey = builder.Configuration.GetSection("AzureCosmosDbSettings")
    .GetValue<string>("PrimaryKey");
    string dbName = builder.Configuration.GetSection("AzureCosmosDbSettings")
    .GetValue<string>("DatabaseName");
    string containerName = builder.Configuration.GetSection("AzureCosmosDbSettings")
    .GetValue<string>("ContainerName");

    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );

    return new CarCosmosService(cosmosClient, dbName, containerName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// INITIAL
//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//var configuration = (IConfiguration)app.Services.GetService(typeof(IConfiguration))!;
//app.MapGet("/", () => $"Hello World! Value: {configuration.GetSection("test").Value}");
//app.Run();

//using AzureProjectMagdalenaGorska;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//var builder = WebApplication.CreateBuilder(args);
//var accountEndpoint = "https://azureprojectmgaccount.documents.azure.com:443/";
//var accountKey = "HRDTuVQsUWMnUe6VEXa7887tifVnsFEtXvfoDPh7nO9p1mMT56Kb8ZupVrJZ2IbZ2EV3DS5vjOogACDb1IqTxw==";
//var databaseName = "ToDoList";

//builder.Services.AddDbContext<TodoDb>(opt => opt.UseCosmos(accountEndpoint, accountKey, databaseName));
////builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
////builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//var app = builder.Build();

//app.MapGet("/todoitems", async (TodoDb db) =>
//    await db.Todos.ToListAsync());

//app.MapGet("/todoitems/complete", async (TodoDb db) =>
//    await db.Todos.Where(t => t.IsComplete).ToListAsync());

//app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
//    await db.Todos.FindAsync(id)
//        is Todo todo
//            ? Results.Ok(todo)
//            : Results.NotFound());

//app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
//{
//    db.Todos.Add(todo);
//    await db.SaveChangesAsync();

//    return Results.Created($"/todoitems/{todo.Id}", todo);
//});

//app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);

//    if (todo is null) return Results.NotFound();

//    todo.Name = inputTodo.Name;
//    todo.IsComplete = inputTodo.IsComplete;

//    await db.SaveChangesAsync();

//    return Results.NoContent();
//});

//app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
//{
//    if (await db.Todos.FindAsync(id) is Todo todo)
//    {
//        db.Todos.Remove(todo);
//        await db.SaveChangesAsync();
//        return Results.Ok(todo);
//    }

//    return Results.NotFound();
//});

//app.Run();
