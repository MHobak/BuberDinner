using BuberDinner.Api.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

    // builder.Services.AddControllers(
    //     options => options.Filters.Add<ErrorHandlingFilterAttribute>() //add error handling filter to all controllers
    // );

    builder.Services.AddControllers();
    //Override the default 
    //builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error"); //Middleware that re-executes the request to the error path

    //Minimal api approach to global error handling
    app.Map("/error", (HttpContext httpContext) =>
    {
        Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            
        //this is different, and does not implement the ProblemDetailsFactory
        //but it allows to pass a dictionary with custom properties
        return Results.Problem();
    });

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
