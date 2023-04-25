using PloomesAPI.Model.Generic;

namespace PloomesAPI.Model
{
	public class Usuario : PloomesCommon
	{
		public string? UsuarioLogin { get; set; }
		public string? UsuarioPassword { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? ExpireTokenTime { get; set; }
	}
}
