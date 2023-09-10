using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.DBContexts;
using WebMvcApi.Repositories;
using WebMvcApi.Mediators;
using WebMvcApi.Mediators.Handlers;

// Builder
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserList"));

// Dependencies injection
builder.Services.AddTransient<IRepository<TodoItem>, TodoRepository>();
builder.Services.AddTransient<IMediator, UserMediator>();
builder.Services.AddScoped<GetUsersHandler>();
builder.Services.AddScoped<GetUserHandler>();
builder.Services.AddScoped<AddUserHandler>();


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
