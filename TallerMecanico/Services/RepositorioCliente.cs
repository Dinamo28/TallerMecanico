using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerMecanico.Models;

public interface iRepositorioCliente
{
    Task<Cliente> BuscarClienteCoinidencia(string input);
    Task CrearCliente(Cliente cliente);
}

namespace TallerMecanico.Services
{

    public class RepositorioCliente : iRepositorioCliente
    {

        private readonly string connectionString;
        public RepositorioCliente(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearCliente(Cliente cliente)
        {
            using var connection = new MySqlConnection(this.connectionString);
            await connection.QueryAsync("INSERT INTO cliente (nombre, direccion, cp, telefono, rfc, correo, contraseña) VALUES (@nombre, @direccion, @cp, @telefono, @rfc, @correo, '12345')", cliente);

        }
        public async Task<Cliente> BuscarClienteCoinidencia(String input)
        {
            using var connection = new MySqlConnection(this.connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cliente>("SELECT * FROM cliente WHERE nombre LIKE '%" + input + "%'");

        }

    }
}
