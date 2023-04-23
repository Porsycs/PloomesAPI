namespace PloomesAPI.Common.Interface
{
	public interface IClienteRepository
	{
		List<Cliente> GetAllClientes();
		Cliente GetClienteById(Guid clienteId);
		Cliente InsertCliente(Cliente cliente);
		Cliente UpdateCliente(Cliente cliente);
		void DeleteCliente(Guid clienteId);

	}
}
