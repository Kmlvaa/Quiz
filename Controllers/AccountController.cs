using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quizz.DTO.AccountDTO;
using Quizz.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Quizz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;

		public AccountController(UserManager<AppUser> manager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
			_userManager = manager;
			_signInManager = signInManager;
			_configuration = configuration;
        }
        [HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

			if(!result.Succeeded) return BadRequest(result.ToString());

			var token = GetToken();

			return Ok(token);
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			var user = new AppUser()
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				UserName = dto.Username,
				Email = dto.Email
			};

			var result = await _userManager.CreateAsync(user, dto.Password);

			if(!result.Succeeded) return BadRequest(result.Errors);
			return Ok();
		}
		private string GetToken()
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

			var token = new JwtSecurityToken(
			issuer: _configuration["Jwt: ValidIssuer"],
			audience: _configuration["Jwt: ValidAudience"],
			expires: DateTime.Now.AddMinutes(5),
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
