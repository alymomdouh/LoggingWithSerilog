
using Serilog;
using SerilogInDot8.example;

namespace SerilogInDot8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
            try
            {
                Log.Information("starting server.");
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.Console();

                    //1- Configuring via appsettings.json (Recommended)
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration);

                    //2- Configuring via Fluent API
                    loggerConfiguration.MinimumLevel.Warning();
                    loggerConfiguration.WriteTo.Console();
                });
                // Add services to the container.
                builder.Services.AddTransient<IDummyService, DummyService>();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                //This ensures that IDummyService is registered into the DI Container of the application.
                app.MapGet("/", (IDummyService svc) => svc.DoSomething());
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "server terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
