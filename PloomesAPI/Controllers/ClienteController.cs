using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Common;
using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services;
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
        private readonly IMongoServices _mongoServices;
        private readonly RabbitMqProducerServices _rabbitMqProducer;

        public ClienteController(IClienteRepository clienteRepository, IMongoServices mongoServices, RabbitMqProducerServices rabbitMqProducer)
        {
            _clienteRepository = clienteRepository;
            _mongoServices = mongoServices;
            _rabbitMqProducer = rabbitMqProducer;
        }


        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<Cliente>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllientes()
        {
            var cliente = _clienteRepository.GetAllClientes();

            if (cliente == null)
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

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [AllowAnonymous]
        [HttpPost("Insert")]
        [ProducesResponseType(200, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult InsertCliente([FromBody] ClienteViewModel cli)
        {
            try
            {
                var cliente = _clienteRepository.InsertCliente(cli);
                return Ok(cliente);
            }
            catch (Exception e)
            {
                _rabbitMqProducer.SendMessage(System.Text.Json.JsonSerializer.Serialize(cli));
                var document = new
                {
                    Sucesso = false, 
                    Titulo = "Erro ao inserir cliente",
                    Mensagem = e.Message
                };
                _mongoServices.Create(MongoLogsViewModel.Log.ClienteLog, document);
                return BadRequest();
            }
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