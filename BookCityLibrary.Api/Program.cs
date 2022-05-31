using BookCityLibrary.Api.Extensions;
using BookCityLibrary.Api.Infrastructure;
using BookCityLibrary.Api.Middlewares;
using BookCityLibrary.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddInfrastructure();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        conf =>
            conf.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; 
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedService = scope.ServiceProvider;

    try
    {
        var dbContext = scopedService.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await AppDbContextSeed.SeedAsync(dbContext);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookCityLibrary.Api v1"));
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseSwaggerDocumentation();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();