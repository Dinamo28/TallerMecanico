using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TallerMecanico.Models;

namespace TallerMecanico.Controllers
{
    public class AccionesController : Controller
    {

        private readonly iRepositorioCliente iRepCliente;
        private readonly iRepositorioMecanico iRepMecanico;
        private readonly iRepositorioUsuarios iRepUsuarios;
        
        public AccionesController(iRepositorioCliente iRepCliente, iRepositorioMecanico iRepMecanico, iRepositorioUsuarios iRepUsuarios)
        {
            this.iRepCliente = iRepCliente;
            this.iRepMecanico = iRepMecanico;
            this.iRepUsuarios = iRepUsuarios;

        }

        public async Task<IActionResult> RegistrarCliente()
        {
            return View();
        }
        public async Task<IActionResult> RegistrarMecanico()
        {
            return View();
        }
        public async Task<IActionResult> AsignarTrabajo()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            await this.iRepCliente.CrearCliente(cliente);
            return RedirectToAction("RegistrarCliente");
        }
        [HttpPost]
        public async Task<IActionResult> CrearMecanico(Mecanico mecanico)
        {
            await this.iRepMecanico.CrearMecanico(mecanico);
            return RedirectToAction("RegistrarMecanico");
        }

        [HttpPost]
        public async Task<IActionResult> LoginIn(Usuario entrada)
        {
            Usuario userSession = await this.iRepUsuarios.BuscarUsuario(entrada.correo, entrada.contraseña);
            Console.WriteLine(userSession.correo);
            if (userSession!=null)
            {
                byte[] userData = JsonSerializer.SerializeToUtf8Bytes(userSession);

                // Almacenar el array de bytes en la sesión
                HttpContext.Session.Set("UserSession", userData);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                // Mostrar mensaje de error de inicio de sesión
                ModelState.AddModelError("message", "Credenciales inválidas");
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public async Task<string> CoincidenciaNombre(string nombre)
        {
            Cliente coincidencia = await this.iRepCliente.BuscarClienteCoinidencia(nombre);
            return coincidencia.nombre;
        }

        public async Task<IEnumerable<Mecanico>> ObtenerMecanicos(int supervisor)
        {
            IEnumerable<Mecanico> mecanicos = await this.iRepMecanico.MecanicosPorSupervisor(supervisor);
            return mecanicos;
        }



    }
}
