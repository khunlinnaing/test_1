using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Security.Cryptography;
using System.Text;

namespace StudentsApi.Models;

[EnableCors("Policy")]
public class EmployeeService
{
    // private readonly DbConnected db;
    // public static async Task<IResult> GetAllEmployees(DbConnected db)
    // {
    //     return Results.Ok(await db.Employee.ToListAsync());
    // }

    public static async Task<IResult> GetEmployeeBySchool(string Address, DbConnected db)
    {
        return Results.Ok(await db.Employee.Where(t => t.Address!.ToLower() == Address.ToLower()).ToListAsync());
    }

    public static async Task<IResult> GetEmployeeById(int id, DbConnected db)
    {
        return Results.Ok(await db.Employee.FindAsync(id)
            is Employees employees ? employees : Results.NotFound());
    }

    public static async Task<IResult> InsertEmployee(Employees employees, DbConnected db)
    {
        var email = db.Employee.Any(u => u.Email == employees.Email);
        var phone = db.Employee.Any(u => u.PhoneNumber == employees.PhoneNumber);
        if (email)
        {
            return Results.BadRequest("Email already exit.");
        }
        else if (phone)
        {
            return Results.BadRequest("Phone already exit.");
        }
        var user = new Employees
        {
            FirstName = employees.FirstName,
            LastName = employees.FirstName,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
            Address = employees.Address,
            Profile = employees.Profile,
            Birdthday = employees.Birdthday,
            Password = HashPassword(employees.Password),
            Role = employees.Role
        };
        db.Employee.Add(user);
        await db.SaveChangesAsync();
        return Results.Created($"/Employee/{employees.id}", employees);
    }

    public static async Task<IResult> UpdateEmployee(int id, Employees inputEmployee, DbConnected db)
    {

        var employee = await db.Employee.FindAsync(id);

        if (employee is null) return Results.NotFound();
        var email = db.Employee.FirstOrDefault(u => u.Email == inputEmployee.Email);
        var phone = db.Employee.FirstOrDefault(u => u.PhoneNumber == inputEmployee.PhoneNumber);
        if (email != null && email.id != inputEmployee.id)
        {
            return Results.BadRequest("Email already Exit");
        }
        else if (phone != null && phone.id != inputEmployee.id)
        {
            return Results.BadRequest("Phone already Exit");
        }
        
        employee.FirstName = inputEmployee.FirstName;
        employee.LastName = inputEmployee.LastName;
        employee.Email = inputEmployee.Email;
        employee.PhoneNumber = inputEmployee.PhoneNumber;
        employee.Address = inputEmployee.Address;
        employee.Profile = inputEmployee.Profile;
        employee.Birdthday = inputEmployee.Birdthday;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public static async Task<IResult> DeleteEmployee(int id, DbConnected db)
    {
        if (await db.Employee.FindAsync(id) is Employees employees)
        {
            db.Employee.Remove(employees);
            await db.SaveChangesAsync();
            return Results.Ok(employees);
        }

        return Results.NotFound();
    }
    public static async Task<IResult> GetPagination(HttpContext context, DbConnected db)
    {
        // Get query parameters for pagination
        int pageNumber = int.Parse(context.Request.Query["page"]);
        int pageSize = 10;
        var count = await db.Employee.CountAsync();
        var totalpage = (int)System.Math.Ceiling(count / (double)pageSize);
        var entities = await db.Employee
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        PaginatedData<Employees> result = new PaginatedData<Employees>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalpage,
            Items = entities
        };
        return Results.Json(result);
    }
    
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}