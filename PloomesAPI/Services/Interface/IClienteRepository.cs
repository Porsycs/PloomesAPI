using PloomesAPI.Common;

namespace PloomesAPI.Services.Interface
{
	public interface IClienteRepository
	{
		Cliente GetClienteByNome(string nome);
		List<Cliente> GetAllClientes();
		Cliente GetByIdCliente(Guid Id);
		Cliente InsertCliente(Cliente item);
		Cliente UpdateCliente(Cliente item);
		void DeleteCliente(Guid Id);
	}
}
