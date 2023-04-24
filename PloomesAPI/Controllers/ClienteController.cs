using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Common;
using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Controllers
{
    [ApiController]
	[Route("api/v1/[controller]")]
	[Authorize("Bearer")]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteRepository _clienteRepository;

		public ClienteController(IClienteRepository clienteRepository)
		{
			_clienteRepository = clienteRepository;
		}


		/// <summary>
		/// Retorna todos os clientes
		/// </summary>
		[HttpGet("GetAll")]
		[ProducesResponseType(200,Type = typeof(List<Cliente>))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult GetAllientes()
		{
				var cliente = _clienteRepository.GetAllClientes();

				if(cliente == null)
					return NotFound();

				return Ok(cliente);
		}

		[HttpGet("GetClienteById/{Id}")]
		[ProducesResponseType(200, Type = typeof(ClienteViewModel))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult GetClienteById(Guid Id)
		{
			var cliente = _clienteRepository.GetByIdCliente(Id);

			if (cliente == null) 
				return NotFound();

			return Ok(cliente);
		}

		[HttpGet("GetClienteByNome/{Nome}")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult GetClienteByNome(string Nome)
		{
			var cliente = _clienteRepository.GetClienteByNome(Nome);

			if(cliente == null)
				return NotFound();

			return Ok(cliente);
		}

		[HttpPost("Insert")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult InsertCliente([FromBody] ClienteViewModel cli)
		{
			var cliente = _clienteRepository.InsertCliente(cli);
			return Ok(cliente);
		}

		[HttpPut("Update")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult UpdateCliente([FromBody] ClienteViewModel cli, [FromHeader] Guid Id)
		{
			var cliente = _clienteRepository.UpdateCliente(cli, Id);
			return Ok(cliente);
		}

		[HttpDelete("Delete/{Id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult DeleteCliente(Guid Id)
		{
			_clienteRepository.DeleteCliente(Id);
			return NoContent();
		}
	}
}