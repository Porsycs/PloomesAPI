using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Model;
using PloomesAPI.Services.Interface;

namespace PloomesAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly ILoginRepository _loginRepository;

		public AuthController(ILoginRepository loginRepository)
		{
			_loginRepository = loginRepository;
		}

		[HttpPost("Login")]
		public IActionResult Login([FromBody] Usuario usuario)
		{
			if (usuario == null)
				return BadRequest();

			var token = _loginRepository.ValidateCredentials(usuario);
			if(token == null)
				return Unauthorized();

			return Ok(token);
		}

		[HttpPost("Refresh")]
		public IActionResult Refresh([FromBody] Token token)
		{
			if (token == null)
				return BadRequest();

			var tkn = _loginRepository.ValidateCredentials(token);
			if (tkn == null)
				return BadRequest();

			return Ok(token);
		}

		[HttpPost("Revoke")]
		[Authorize("Bearer")]
		public IActionResult Revoke()
		{
			var usuario = User.Identity?.Name;
			var result = _loginRepository.RevokeToken(usuario);

			if(!result)
				return BadRequest();

			return NoContent();
		}
	}
}
