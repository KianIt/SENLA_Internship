using Microsoft.EntityFrameworkCore;

using MediatR;

using WebMvcApi.Models;
using WebMvcApi.DBContexts;
using WebMvcApi.Repositories;

// Builder
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserList"));

// MediatR
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining<Program>());

// Dependencies injection
builder.Services.AddTransient<IRepository<TodoItem>, TodoRepository>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
