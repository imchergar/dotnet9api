using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;

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
    
     [HttpGet("TestConnection")]
     public DateTime TestConnection()
     {
         return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
     }
     
    
    [HttpGet("GetUsers/{userId}")]
    public string[] GetUsers(string userId)
    {
        string[] responseArray = new string[]
        {
            "value1",
            "value2"
        };
        
        return responseArray;
    }
    
    
}