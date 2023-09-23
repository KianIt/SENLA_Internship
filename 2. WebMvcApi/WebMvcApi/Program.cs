using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


string? postgreConnectionString = builder.Configuration.GetConnectionString("PostgresConnectionString");


builder.Services.AddControllers();

builder.Services.AddTodoItemsInmemoryContextWithRepository();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddUsersPostgresContextWithRepository(postgreConnectionString!);

builder.Services.AddUsersPostgresConnectionWithVisitorAndRepository(postgreConnectionString!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
