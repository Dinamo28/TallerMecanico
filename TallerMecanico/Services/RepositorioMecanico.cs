using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerMecanico.Models;

public interface iRepositorioMecanico
{
    Task CrearMecanico(Mecanico mecanico);
    Task<IEnumerable<Mecanico>> MecanicosPorSupervisor(int supervisor);
}

namespace TallerMecanico.Services
{
    public class RepositorioMecanico : iRepositorioMecanico
    {
        private readonly string connectionString;
        public RepositorioMecanico(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<Mecanico>> MecanicosPorSupervisor(int supervisor)
        {
            using var connection = new MySqlConnection(this.connectionString);
            return await connection.QueryAsync<Mecanico>("SELECT * FROM mecanico WHERE Id_supervisor = @supervisor", new { supervisor });

        }
        public async Task CrearMecanico(Mecanico mecanico)
        {
            using var connection = new MySqlConnection(this.connectionString);
            await connection.QueryAsync("INSERT INTO mecanico (nombre, direccion, cp, telefono, rfc, correo, password, Id_supervisor) VALUES (@nombre, @direccion, @cp, @telefono, @rfc, @correo, '12345', 2)", mecanico);

        }
    }
}
