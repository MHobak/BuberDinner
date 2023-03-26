using BuberDinner.Api.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
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
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/errors"); //Middleware that re-executes the request to the error path
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
