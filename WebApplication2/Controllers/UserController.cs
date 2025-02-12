using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
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
    public IEnumerable<User> GetUsers(string userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM dotnetapidatabase.User";
        
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
            FROM dotnetapidatabase.User
                WHERE UserId = " + userId.ToString(); //"7"
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }
    
    
}