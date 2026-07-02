

using Api.DataAccess;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;



namespace Api;

public class Program
{
    public static void Main(string[] args)
    {

        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
             ?? Environments.Development;

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            EnvironmentName = envName,
        });


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelop", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }


        //Registers all user defined services 
        RegisterServices(builder.Services);

        if(builder.Environment.IsProduction() || builder.Environment.IsStaging())
        {
            
        } else
        {
            builder.Services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=App.db"));
        }


        



        var app = builder.Build();

        InitalizeDatabase(app.Services.CreateScope().ServiceProvider);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

        }

        if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor,
            });
        }


        if (app.Environment.IsDevelopment())
        {
            app.UseCors("AllowDevelop");
        }



        if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
        {
            app.UseHttpsRedirection();
        }


        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }



    private static void InitalizeDatabase(IServiceProvider serviceProvider)
    {
        var DbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DatabaseContext>>();   
        using var  DbContext = DbContextFactory.CreateDbContext();
        DbContext.Database.EnsureCreated();   
    }



    protected static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
    }

}




