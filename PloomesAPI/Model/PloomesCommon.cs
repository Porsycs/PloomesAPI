namespace PloomesAPI.Common
{
	public class PloomesCommon
	{

		public Guid Id { get; set; } = new Guid();
		public string? CodigoExterno { get; set; }
		public DateTime Inclusao { get; set; } = DateTime.Now;
		public DateTime? Alteracao { get; set; }
		public bool Excluido { get; set; } = false;
	}
}
