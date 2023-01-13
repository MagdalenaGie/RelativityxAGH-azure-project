using Microsoft.EntityFrameworkCore;

namespace AzureProjectMagdalenaGorska
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var accountEndpoint = "https://azureprojectmgaccount.documents.azure.com:443/";
        //    var accountKey = "HRDTuVQsUWMnUe6VEXa7887tifVnsFEtXvfoDPh7nO9p1mMT56Kb8ZupVrJZ2IbZ2EV3DS5vjOogACDb1IqTxw==";
        //    var databaseName = "ToDoList";

        //    optionsBuilder.UseCosmos(accountEndpoint, accountKey, databaseName);
        //}

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
