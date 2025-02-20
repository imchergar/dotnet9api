using AutoMapper;
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
    
    
    private readonly Mapper _mapper;
    private readonly DataContextEF _entityFramework;

    public UserController(IConfiguration config)
     {
         _dapper = new DataContextDapper(config);

         _entityFramework = new DataContextEF(config);

         _mapper = new Mapper(new MapperConfiguration(cfg =>{
             cfg.CreateMap<UserDto, User>();
             cfg.CreateMap<UserSalary, UserSalary>().ReverseMap();
             cfg.CreateMap<UserJobInfo, UserJobInfo>().ReverseMap();
         }));
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
    public User GetSingleUser(int userId)
    {
        var user = _entityFramework.Users
            .FirstOrDefault(u => u.UserId == userId);

        if (user != null)
        {
            return user;
        }
        
        throw new Exception("Failed to Get User");
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
    public IActionResult AddUser( UserDto user)
    {
        User userDb = _mapper.Map<User>(user);
        
        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        } 

        throw new Exception("Failed to Add User");
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

    [HttpGet("UserSalary/{userId}")]
    public IEnumerable<UserSalary> GetUserSalaryEf(int userId)
    {
        return _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .ToList();
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalaryEf(UserSalary userForInsert)
    {
        _entityFramework.UserSalary.Add(userForInsert);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Adding UserSalary failed on save");
    }


    [HttpPut("UserSalary")]
    public IActionResult PutUserSalaryEf(UserSalary userForUpdate)
    {
        UserSalary? userToUpdate = _entityFramework.UserSalary
            .FirstOrDefault(u => u.UserId == userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Updating UserSalary failed on save");
        }
        throw new Exception("Failed to find UserSalary to Update");
    }


    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEf(int userId)
    {
        UserSalary? userToDelete = _entityFramework.UserSalary
            .FirstOrDefault(u => u.UserId == userId);

        if (userToDelete != null)
        {
            _entityFramework.UserSalary.Remove(userToDelete);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Deleting UserSalary failed on save");
        }
        throw new Exception("Failed to find UserSalary to delete");
    }


    [HttpGet("UserJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetUserJobInfoEf(int userId)
    {
        return _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .ToList();
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEf(UserJobInfo userForInsert)
    {
        _entityFramework.UserJobInfo.Add(userForInsert);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Adding UserJobInfo failed on save");
    }


    [HttpPut("UserJobInfo")]
    public IActionResult PutUserJobInfoEf(UserJobInfo userForUpdate)
    {
        UserJobInfo? userToUpdate = _entityFramework.UserJobInfo
            .FirstOrDefault(u => u.UserId == userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Updating UserJobInfo failed on save");
        }
        throw new Exception("Failed to find UserJobInfo to Update");
    }


    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfoEf(int userId)
    {
        UserJobInfo? userToDelete = _entityFramework.UserJobInfo
            .FirstOrDefault(u => u.UserId == userId);

        if (userToDelete != null)
        {
            _entityFramework.UserJobInfo.Remove(userToDelete);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Deleting UserJobInfo failed on save");
        }
        throw new Exception("Failed to find UserJobInfo to delete");
    }
    
}