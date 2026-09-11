namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers(); // Add support for API Controllers
        builder.Services.AddAuthorization();

        // OpenAPI / Swagger configuration
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers(); // Maps your Controller routes (ExpensesController, SubscriptionsController, etc.)

        app.Run();
    }
}