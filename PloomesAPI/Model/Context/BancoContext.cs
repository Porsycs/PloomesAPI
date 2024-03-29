﻿using Microsoft.EntityFrameworkCore;
using PloomesAPI.Common;

namespace PloomesAPI.Model.Context
{
    public class BancoContext : DbContext
    {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Token> Tokens { get; set; }

    }
}
