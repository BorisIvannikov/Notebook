using System;
using Microsoft.EntityFrameworkCore;
using Notebook.DAL;
using Notebook.DAL.Interfaces;
using Notebook.DAL.Repository;
using Notebook.Service.Implementation;
using Notebook.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.LogTo(Console.WriteLine);
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
