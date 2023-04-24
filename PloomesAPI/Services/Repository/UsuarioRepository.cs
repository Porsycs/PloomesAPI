using Microsoft.EntityFrameworkCore;
using PloomesAPI.Model;
using PloomesAPI.Model.Context;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace PloomesAPI.Services.Repository
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly BancoContext _dbContext;
		private readonly IRepository<Usuario> _repository;
		public UsuarioRepository(BancoContext dbContext, IRepository<Usuario> repository) 
		{
			_dbContext = dbContext;
			_repository = repository;
		}
		public Usuario ValidaUsuario(Usuario usuario)
		{
			var senha = PasswordHash(usuario.UsuarioPassword, SHA256.Create());
			return _dbContext.Usuarios.FirstOrDefault(f => (f.UsuarioLogin == usuario.UsuarioLogin) && (f.UsuarioPassword == senha));
		}

		public Usuario ValidateCredentials(string login)
		{
			return _dbContext.Usuarios.SingleOrDefault(u => (u.UsuarioLogin == login));
		}

		public Usuario RefreshUsuario(Usuario usuario )
		{
			var usuarioDados = _repository.GetById(usuario.Id);

			if (usuarioDados != null)
			{
				try
				{
					_dbContext.Entry(usuario).CurrentValues.SetValues(usuario);
					_dbContext.SaveChanges();
					return usuario;
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
				return null;
		}

		private string PasswordHash(string usuarioPassword, SHA256 sha)
		{
			Byte[] inputBytes = Encoding.UTF8.GetBytes(usuarioPassword);
			Byte[] hashedBytes = sha.ComputeHash(inputBytes);
			return BitConverter.ToString(hashedBytes);
		}

		public bool RevokeToken (string usuarioLogin)
		{
			var usuario = _dbContext.Usuarios.FirstOrDefault(f => f.UsuarioLogin == usuarioLogin);

			if (usuario == null)
				return false;

			usuario.RefreshToken = null;
			_dbContext.SaveChanges();

			return true;
		}
	}
}
