using PloomesAPI.Common;
using PloomesAPI.Model.Context;
using PloomesAPI.Services.Interface;

namespace PloomesAPI.Services.Repository
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly BancoContext _dbContext;

		public ClienteRepository(BancoContext dbContext)  => _dbContext = dbContext;
		public Cliente GetClienteByNome (string nome)
		{
			var cliente = _dbContext.Clientes.FirstOrDefault(f => f.Nome == nome);

			return cliente;
		}
	}
}
