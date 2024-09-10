using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Service.Services;
using FastEndpoints;
using LibraryManagementSystem.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
// Add services to the container.
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<ILibraryService, LibraryService>();
builder.Services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();

var app = builder.Build();


// Use FastEndpoints
app.UseFastEndpoints();


app.Run();