using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions; 
using VeniteckSolutions.Models;
using VeniteckSolutions.Services;

namespace VeniteckSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertUser([FromBody] UserDTO user)
        {
           
            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Invalid email format.");

            if (!Regex.IsMatch(user.MobileNumber, @"^\d{10,15}$"))
                return BadRequest("Mobile number must be 10-15 digits.");

            if (user.Gender != "M" && user.Gender != "F")
                return BadRequest("Gender must be 'M' or 'F'.");

            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

      
            var result = await _userService.InsertUser(user);
            return result ? Ok("User added successfully") : StatusCode(500, "An error occurred while adding the user");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
           
            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Invalid email format.");

            if (!Regex.IsMatch(user.MobileNumber, @"^\d{10,15}$"))
                return BadRequest("Mobile number must be 10-15 digits.");

            if (user.Gender != "M" && user.Gender != "F")
                return BadRequest("Gender must be 'M' or 'F'.");

           
            if (user.UserId == null)
                return BadRequest("UserId is required for update");

 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 
            var result = await _userService.UpdateUser(user);
            return result ? Ok("User updated successfully") : StatusCode(500, "An error occurred while updating the user");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return users != null ? Ok(users) : NotFound("No users found.");
        }

        // Get User By ID Endpoint
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid User ID");

            var user = await _userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound($"User with ID {id} not found.");
        }
    }

}

