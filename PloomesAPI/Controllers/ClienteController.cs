using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Common;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Controllers
{
    [ApiController]
	[Route("api/v1/[controller]")]
	[Authorize("Bearer")]
	public class ClienteController : ControllerBase
	{
		private readonly IRepository<Cliente> _repository;
		private readonly IClienteRepository _clienteRepository;

		public ClienteController(IRepository<Cliente> repository, IClienteRepository clienteRepository)
		{
			_repository = repository;
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
				var cliente = _repository.GetAll();

				if(cliente == null)
					return NotFound();

				return Ok(cliente);
		}

		[HttpGet("GetClienteById/{Id}")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult GetClienteById(Guid Id)
		{
			var cliente = _repository.GetById(Id);

			if (cliente == null) 
				return NotFound();

			return Ok(cliente);
		}

		[HttpGet("GetClienteByNome/{Nome}")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult GetClienteByNome(string nome)
		{
			var cliente = _clienteRepository.GetClienteByNome(nome);

			if(cliente == null)
				return NotFound();

			return Ok(cliente);
		}

		[HttpPost("Insert")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult InsertCliente([FromBody] Cliente cli)
		{
			var cliente = _repository.Insert(cli);
			return Ok(cliente);
		}

		[HttpPut("Update")]
		[ProducesResponseType(200, Type = typeof(Cliente))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult UpdateCliente([FromBody] Cliente cli)
		{
			var cliente = _repository.Update(cli);
			return Ok(cliente);
		}

		[HttpDelete("Delete/{Id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult DeleteCliente(Guid Id)
		{
			_repository.Delete(Id);
			return NoContent();
		}
	}
}