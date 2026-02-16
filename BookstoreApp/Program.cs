using BookstoreApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using BookstoreApp.Models;
using BookstoreApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add API Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<AiSummaryService>();



builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BookstoreConnection")));


var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Seed database using JSON file
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookstoreContext>();

    // Make sure database and tables exist
    context.Database.EnsureCreated();

    // Seed only when database is empty
    if (!context.Books.Any())
    {
        // Path to the JSON file (copied to output folder)
        var filePath = Path.Combine(
            app.Environment.ContentRootPath,
            "SeedData",
            "books.json"
        );

        if (File.Exists(filePath))
        {
            // Read JSON file
            var json = File.ReadAllText(filePath);

            // Convert JSON into List<Book>
            var books = JsonSerializer.Deserialize<List<Book>>(json);

            if (books != null && books.Count > 0)
            {
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
