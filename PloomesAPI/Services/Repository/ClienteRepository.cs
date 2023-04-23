using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PloomesAPI.Common.Interface;
using PloomesAPI.Model.Context;

namespace PloomesAPI.Common.Repository
{
    public class ClienteRepository : IClienteRepository
	{
		private readonly BancoContext _dbContext;
		public ClienteRepository(BancoContext dbContext) => _dbContext = dbContext;

		public List<Cliente> GetAllClientes()
		{
			var cliente = _dbContext.Clientes.AsNoTracking().ToList();
			return cliente;
		}

		public Cliente GetClienteById(Guid clienteId)
		{
			var clienteById = _dbContext.Clientes.AsNoTracking().FirstOrDefault(f => f.Id == clienteId);
			return clienteById;
		}

		public Cliente InsertCliente(Cliente cliente)
		{
			try
			{
				_dbContext.Add(cliente);
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw;
			}

			return cliente;
		}

		public Cliente UpdateCliente(Cliente cliente)
		{
			if (!Existe(cliente.Id))
				return new Cliente();

			var result = GetClienteById(cliente.Id);

			if (result != null) {
				try
				{
					_dbContext.Entry(result).CurrentValues.SetValues(cliente);
					_dbContext.SaveChanges();
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			return cliente;
		}
		
		public void DeleteCliente(Guid clienteId)
		{
			try
			{
				var cliente = GetClienteById(clienteId);
				_dbContext.Clientes.Remove(cliente);
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public bool Existe(Guid clienteId)
		{
			var existe = _dbContext.Clientes.AsNoTracking().Any(f => f.Id == clienteId);
			return existe;
		}
	}
}
