using Microsoft.EntityFrameworkCore.Update.Internal;
using PloomesAPI.Common;
using PloomesAPI.Model.Context;
using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Services.Repository
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly BancoContext _dbContext;
		private readonly IRepository<Cliente> _repository;

		public ClienteRepository(BancoContext dbContext, IRepository<Cliente> repository) 
		{
			_dbContext = dbContext;
			_repository = repository;
		}

		public List<Cliente> GetAllClientes()
		{
			return _repository.GetAll();
		}

		public Cliente GetByIdCliente(Guid Id)
		{
			return _repository.GetById(Id);
		}

		public Cliente GetClienteByNome (string nome)
		{
			var cliente = _dbContext.Clientes.FirstOrDefault(f => f.Nome == nome);

			return cliente;
		}

		public Cliente InsertCliente(ClienteViewModel item)
		{
			var cliente = new Cliente
			{
				Nome = item.Nome,
				CPF = item.CPF,
				Nascimento = item.Nascimento,
				Endereco = item.Endereco,
				Cidade = item.Cidade,
				Estado = item.Estado,
				Email = item.Email,
			};

			return _repository.Insert(cliente);
		}

		public Cliente UpdateCliente(ClienteViewModel item, Guid Id)
		{
			var cliente = GetByIdCliente(Id);

			return UpdateClienteViewModel(cliente, item);
		}
		public void DeleteCliente(Guid Id)
		{
			_repository.Delete(Id);
		}
		public Cliente UpdateClienteViewModel(Cliente cliente, ClienteViewModel item)
		{
			cliente.Nome = item.Nome;
			cliente.CPF = item.CPF;
			cliente.Nascimento = item.Nascimento;
			cliente.Cidade = item.Cidade;
			cliente.Estado = item.Estado;
			cliente.Email = item.Email;
			cliente.Endereco = item.Endereco;

			_dbContext.SaveChanges();

			return cliente;
		}
	}
}
