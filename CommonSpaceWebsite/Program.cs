using CommonSpaceWebsite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(); // Add this
builder.Services.AddHttpContextAccessor(); // Add this

builder.Services.AddDbContext<CommonSpaceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ComSpaceDB"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())//lägger till sample data
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CommonSpaceDbContext>();
    SampleData.Initialize(context);
}

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

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
