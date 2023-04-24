using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Common;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Controllers
{
    [ApiController]
	[Route("api/v1/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly IRepository<Cliente> _repository;

		public ClienteController(IRepository<Cliente> repository) => _repository = repository;


		/// <summary>
		/// Retorna todos os clientes
		/// </summary>
		[HttpGet("GetAll/")]
		public IActionResult GetAllClientes()
		{
				var cliente = _repository.GetAll();
				return Ok(cliente);
		}

		[HttpGet("GetCliente/{Id}")]
		public IActionResult GetClienteById(Guid Id)
		{
			var cliente = _repository.GetById(Id);
			return Ok(cliente);
		}

		[HttpPost("Insert/")]
		public IActionResult InsertCliente([FromBody] Cliente cli)
		{
			var cliente = _repository.Insert(cli);
			return Ok(cliente);
		}

		[HttpPut("Update/")]
		public IActionResult UpdateCliente([FromBody] Cliente cli)
		{
			var cliente = _repository.Update(cli);
			return Ok(cliente);
		}

		[HttpDelete("Delete/{Id}")]
		public IActionResult DeleteCliente(Guid Id)
		{
			_repository.Delete(Id);
			return NoContent();
		}
	}
}