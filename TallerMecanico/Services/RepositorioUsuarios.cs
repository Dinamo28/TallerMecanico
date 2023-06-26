using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerMecanico.Models;

public interface iRepositorioUsuarios
{
    Task<Usuario> BuscarUsuario(string correo, string password);
}

namespace TallerMecanico.Services
{
    public class RepositorioUsuarios : iRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Usuario> BuscarUsuario(string correo, string password)
        {
            var param = new { correo, password };
            using var connection = new MySqlConnection(this.connectionString);
            return await connection.QueryFirstOrDefaultAsync<Usuario>("CALL BuscaUsuario(@correo, @password);", param);
        }
    }
}
