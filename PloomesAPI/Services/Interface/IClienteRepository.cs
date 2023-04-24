using PloomesAPI.Common;
using PloomesAPI.Model.ViewModel;

namespace PloomesAPI.Services.Interface
{
	public interface IClienteRepository
	{
		Cliente GetClienteByNome(string nome);
		List<Cliente> GetAllClientes();
		Cliente GetByIdCliente(Guid Id);
		Cliente InsertCliente(ClienteViewModel item);
		Cliente UpdateCliente(ClienteViewModel item, Guid Id);
		Cliente UpdateClienteViewModel(Cliente cliente, ClienteViewModel item);
		void DeleteCliente(Guid Id);
	}
}
