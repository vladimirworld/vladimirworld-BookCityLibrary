using BlazorApp.UI.Contracts;
using BlazorApp.UI.Services;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

{
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddHttpClient();

    builder.Services.AddBlazoredToast();

    builder.Services.AddTransient<ApiService>();
    builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
    builder.Services.AddScoped<IAuthorRepository, AuthorService>();
    builder.Services.AddScoped<IBookRepository, BookService>();

    builder.Services.AddOptions();
}

{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}