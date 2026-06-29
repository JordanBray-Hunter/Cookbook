

using Microsoft.AspNetCore.HttpOverrides;

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


        var cookiePolicy = builder.Environment.IsProduction() || builder.Environment.IsStaging()
    ? CookieSecurePolicy.Always
    : CookieSecurePolicy.SameAsRequest;


        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
            options.Cookie.Name = "X-CSRF-TOKEN-COOKIE";
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;

            options.Cookie.SecurePolicy = cookiePolicy;
        });


        var app = builder.Build();

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
            app.UseCors("AllowDevlop");
        }



        if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
        {
            app.UseHttpsRedirection();
        }
        app.MapGet("/api/csrf-token", (Microsoft.AspNetCore.Antiforgery.IAntiforgery antiforgery, HttpContext context) =>
{
    var tokens = antiforgery.GetAndStoreTokens(context);
    return Results.Ok(tokens.RequestToken!);
});



        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }




    protected static void RegisterService(IServiceCollection services)
    {

    }

}




