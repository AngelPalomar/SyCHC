using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clientes_UsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        public Clientes_UsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<Cliente_UsuarioController>
        [HttpGet]
        public IEnumerable<Clientes_Usuario> Get()
        {
            return context.Clientes_Usuario.ToList();
        }

        // GET api/<Cliente_UsuarioController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var usuarioCliente = context.Clientes_Usuario.Find(id);
            if (usuarioCliente != null)
            {
                return Ok(usuarioCliente);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<Cliente_UsuarioController>/lista-usuarios/5
        [HttpGet("lista-usuarios/{idCliente}")]
        public ActionResult GetListaUsuariosDeCliente(Guid idCliente)
        {
            var usuarioCliente = context.Lista_Usuarios_De_Cliente
                .Where(uc => uc.IdCliente == idCliente);

            if (usuarioCliente != null)
            {
                return Ok(usuarioCliente);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<Cliente_UsuarioController>/lista-usuarios/5
        [HttpGet("info-cliente/{idUsuario}")]
        public ActionResult GetListaClienteDeUsuario(Guid idUsuario)
        {
            var usuarioCliente = context.Lista_Usuarios_De_Cliente
                .Where(uc => uc.IdUsuario == idUsuario);

            if (usuarioCliente != null)
            {
                return Ok(usuarioCliente);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<Cliente_UsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] Clientes_Usuario clientes_Usuario)
        {
            //Busca info del usuario
            var usuarioInfo = context.Usuario.Find(clientes_Usuario.IdUsuario);
            if (usuarioInfo == null)
            {
                return NotFound("Usuario no econtrado");
            }

            //Valida que no exista
            var existeUsuarioCliente = context
                .Clientes_Usuario
                .FirstOrDefault(
                    euc => 
                    euc.IdCliente == clientes_Usuario.IdCliente &&
                    euc.IdUsuario == clientes_Usuario.IdUsuario
                );

            if (existeUsuarioCliente != null)
            {
                return BadRequest("Este usuario ya está asignado a este cliente");
            }

            try
            {
                usuarioInfo.AsignadoTipoUsuario = true;
                context.Clientes_Usuario.Add(clientes_Usuario);
                context.SaveChanges();

                return Ok(clientes_Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Cliente_UsuarioController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Clientes_Usuario nuevoClienteUsuario)
        {
            var cliSuarioRegistro = context.Clientes_Usuario.Find(id);
            if (cliSuarioRegistro != null)
            {
                try
                {
                    cliSuarioRegistro.IdCliente = nuevoClienteUsuario.IdCliente;
                    cliSuarioRegistro.IdUsuario = nuevoClienteUsuario.IdUsuario;
                    context.SaveChanges();

                    return Ok(cliSuarioRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        // DELETE api/<Cliente_UsuarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cliSuarioRegistro = context.Clientes_Usuario.Find(id);

            if (cliSuarioRegistro != null)
            {
                //Busca info del usuario
                var usuarioInfo = context.Usuario.Find(cliSuarioRegistro.IdUsuario);
                if (usuarioInfo == null)
                {
                    return NotFound("Usuario no econtrado");
                }

                try
                {
                    usuarioInfo.AsignadoTipoUsuario = false;
                    context.Clientes_Usuario.Remove(cliSuarioRegistro);
                    context.SaveChanges();

                    return Ok("Usuario eliminado del cliente correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }
    }
}
