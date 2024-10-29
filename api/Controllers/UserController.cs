using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute]int id){

        var user = await _userRepository.GetUserById(id);

        if(user != null){
            return Ok(user);
        }

        return NotFound();
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(){

        var users = await _userRepository.GetAllUsers();

        if(users == null){
            return NotFound();
        }
            return Ok(users);

    }

    /// <summary>
    /// Add new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/User
    ///     {
    ///        "id": 1,
    ///        "username": "farmaan",
    ///        "email": "test@mail.com",
    ///        "password": "1234"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody]User user){
        var newUser = await _userRepository.CreateUser(user);

        if(newUser != null){
            return Ok("user added successfully");
        }else{
            
            return NotFound("user already exists");
        }

    }
    /// <summary>
    /// Update existing user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT api/User
    ///     {
    ///        "id": 0,
    ///        "username": "farmaan",
    ///        "email": "test@mail.com",
    ///        "password": "1234"
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody]User user, int id){
        var updatedUser = await _userRepository.UpdateUser(user, id);

        if(updatedUser != null){
            return Ok("user updated successfully");
        }else{
            return NotFound("user does not exists");
        }
    }

    /// <summary>
    /// Delete user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id){
        var userToDelete = await _userRepository.DeleteUser(id);

        return Ok("user deleted successfully");
    }

    /// <summary>
    /// User Login using email and password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/User/login
    ///     {
    ///        "id": 0,
    ///        "username": "string",
    ///        "email": "test@mail.com",
    ///        "password": "1234"
    ///     }
    /// 
    /// </remarks>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] User user){
        var existingUser = await _userRepository.LoginUser(user.Email, user.Password);

        if(existingUser == true){
            return Ok("Login Successful");
        }
        return NotFound("Invalid username or password");
    } 
    }
}