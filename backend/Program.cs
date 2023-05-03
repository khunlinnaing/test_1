using backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbConnected>(option => option.UseSqlite(connectionString));
// Add Cors
builder.Services.AddCors(o => o.AddPolicy("Policy", builder => {
  builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
var app = builder.Build();
app.UseCors("Policy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/api/employees", EmployeeService.GetAllEmployees);
app.MapGet("/address/{Address}", EmployeeService.GetEmployeeBySchool);
app.MapGet("/api/employees/{id}", EmployeeService.GetEmployeeById);
app.MapPost("/api/employees", EmployeeService.InsertEmployee);
app.MapPut("/api/employees/{id}", EmployeeService.UpdateEmployee);
app.MapDelete("/api/employees/{id}", EmployeeService.DeleteEmployee);
app.MapGet("/api/employees", EmployeeService.GetPagination);

app.Run();





