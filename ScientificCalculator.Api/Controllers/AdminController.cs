using Microsoft.AspNetCore.Mvc;
using ScientificCalculator.Services.Interfaces;
using System;
using ScientificCalculator.Common.DTO;
using ScientificCalculator.DataAccess.Repositories;

namespace ScientificCalculator.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public AdminController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var role = HttpContext.Session.GetString("Role");
        
        //if role is not admin return directly status code 403
        if (role != "Admin")
        {
            return StatusCode(403 , "You are not an admin!");
        }
     
        var users = await _userRepository.GetAllAsync();
        // entity listesi
        // UserDto listesi oluştur
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            IsAdmin = u.IsAdmin
            
        }).ToList();

        return Ok(userDtos);
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        var role = HttpContext.Session.GetString("Role");

        if (role != "Admin")
        {
            return Forbid("You are not an admin!");
        }
        else if (user == null)
        {
            return NotFound("User not found");
        }
        
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
        return NoContent();
    }
    
}