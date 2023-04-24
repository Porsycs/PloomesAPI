using PloomesAPI.Common;
using PloomesAPI.Model.Context;
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

		public Cliente InsertCliente(Cliente item)
		{
			return _repository.Insert(item);
		}

		public Cliente UpdateCliente(Cliente item)
		{
			return _repository.Update(item);
		}
		public void DeleteCliente(Guid Id)
		{
			_repository.Delete(Id);
		}
	}
}
