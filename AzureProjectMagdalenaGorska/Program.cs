using AzureProjectMagdalenaGorska.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Cosmos;
using Microsoft.OpenApi.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Azure Project API", Version = "v1" });
});

builder.Services.AddSingleton<IMovieCosmosService>(options =>
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

    return new MovieCosmosService(cosmosClient, dbName, containerName);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Azure Project API V1");
});


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

//var configuration = (IConfiguration)app.Services.GetService(typeof(IConfiguration))!;
//app.MapGet("/", () => $"Hello World! Value: {configuration.GetSection("test").Value}");
//app.MapGet("/db", () => $"Hello Database! Value: {configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("DatabaseName")}");

app.Run();