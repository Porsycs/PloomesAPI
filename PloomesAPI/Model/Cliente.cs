using PloomesAPI.Model.Generic;

namespace PloomesAPI.Common
{
    public class Cliente : PloomesCommon
	{
		public string Nome { get; set; }
		public string CPF { get; set; }
		public string Nascimento { get; set; }
		public string Endereco { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string Email { get; set; }
	}
}