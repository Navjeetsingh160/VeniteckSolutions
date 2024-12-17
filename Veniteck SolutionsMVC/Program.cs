using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer(); // Add for API exploration
builder.Services.AddSwaggerGen(); // Add Swagger for API documentation
builder.Services.AddHttpClient(); // Register HttpClient for API requests

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Enable Swagger for development environment
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Veniteck Solutions API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS for security in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Map MVC routes for UserController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=InsertUser}/{id?}");

// Map API routes for UserController
app.MapControllers(); // Ensure to create this extension method

app.Run();
