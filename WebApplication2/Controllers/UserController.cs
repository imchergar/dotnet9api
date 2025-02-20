using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.DTO;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private DataContextDapper _dapper;
     public UserController(IConfiguration config)
     {
         _dapper = new DataContextDapper(config);
     }
    
     
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT *
            FROM Users";
        
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }    
    
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(string userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM Users
                WHERE UserId = " + userId.ToString(); 
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }


    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE Users
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Gender = @Gender,
            Active = @Active
        WHERE UserId = @UserId";

        var parameters = new
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Gender = user.Gender,
            Active = user.Active,
            UserId = user.UserId
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok("User updated successfully.");
        } 

        return StatusCode(500, "Failed to update user.");
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser([FromBody] UserDto user)
    {
        if (user == null)
        {
            return BadRequest("User data is required.");
        }

        string sql = @"
        INSERT INTO Users (
            FirstName,
            LastName,
            Email,
            Gender,
            Active
        ) VALUES (
            @FirstName,
            @LastName,
            @Email,
            @Gender,
            @Active
        )";

        var parameters = new
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Gender = user.Gender,
            Active = user.Active
        };

        Console.WriteLine(sql); // Debugging purpose

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok("User added successfully.");
        }

        return StatusCode(500, "Failed to add user.");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = "DELETE FROM Users WHERE UserId = @UserId";

        var parameters = new { UserId = userId };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }

        return StatusCode(500, "Failed to delete user");
    }

    
}