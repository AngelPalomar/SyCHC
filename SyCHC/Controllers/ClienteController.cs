using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext context;
        public ClienteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return context.Cliente.ToList();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var cliente = context.Cliente.Find(id);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ClienteController>
        [HttpPost]
        public ActionResult Post([FromBody] Cliente cliente)
        {
            try
            {
                context.Cliente.Add(cliente);
                context.SaveChanges();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Cliente nuevoCliente)
        {
            var clienteRegistro = context.Cliente.Find(id);
            if (clienteRegistro != null)
            {
                try
                {
                    clienteRegistro.Nombre = nuevoCliente.Nombre;
                    clienteRegistro.RFC = nuevoCliente.RFC;
                    clienteRegistro.RazonSocial = nuevoCliente.RazonSocial;
                    clienteRegistro.DireccionFiscal = nuevoCliente.DireccionFiscal;

                    context.SaveChanges();

                    return Ok(clienteRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var cliente = context.Cliente.Find(id);
            if (cliente != null)
            {
                try
                {
                    context.Cliente.Remove(cliente);
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
