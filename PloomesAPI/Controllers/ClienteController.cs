using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Common;
using PloomesAPI.Common.Interface;

namespace PloomesAPI.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteRepository _clienteRepository;

		public ClienteController(IClienteRepository clienteRepository) => _clienteRepository = clienteRepository;


		/// <summary>
		/// Retorna todos os clientes
		/// </summary>
		[HttpGet("GetAll/")]
		public IActionResult GetAllClientes()
		{
				var cliente = _clienteRepository.GetAllClientes();
				return Ok(cliente);
		}

		[HttpGet("GetCliente/{Id}")]
		public IActionResult GetClienteById(Guid Id)
		{
			var cliente = _clienteRepository.GetClienteById(Id);
			return Ok(cliente);
		}

		[HttpPost("Insert/")]
		public IActionResult InsertCliente([FromBody] Cliente cli)
		{
			var cliente = _clienteRepository.InsertCliente(cli);
			return Ok(cliente);
		}

		[HttpPost("Update/")]
		public IActionResult UpdateCliente([FromBody] Cliente cli)
		{
			var cliente = _clienteRepository.UpdateCliente(cli);
			return Ok(cliente);
		}

		[HttpDelete("Delete/{Id}")]
		public IActionResult DeleteCliente(Guid Id)
		{
			_clienteRepository.DeleteCliente(Id);
			return NoContent();
		}
	}
}