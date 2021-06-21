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
    public class EtapaController : ControllerBase
    {
        private readonly AppDbContext context;
        public EtapaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<EtapaController>
        [HttpGet]
        public IEnumerable<Etapa> Get()
        {
            return context.Etapa.ToList();
        }

        // GET api/<EtapaController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var etapa = context.Etapa.Find(id);
            if (etapa != null)
            {
                return Ok(etapa);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<EtapaController>
        [HttpPost]
        public ActionResult Post([FromBody] Etapa etapa)
        {
            try
            {
                context.Etapa.Add(etapa);
                context.SaveChanges();

                return Ok(etapa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EtapaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Etapa nuevaEtapa)
        {
            var etapaRegistro = context.Etapa.Find(id);
            if (etapaRegistro != null)
            {
                try
                {
                    etapaRegistro.Nombre = nuevaEtapa.Nombre;
                    context.SaveChanges();

                    return Ok(etapaRegistro);
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

        // DELETE api/<EtapaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var etapa = context.Etapa.Find(id);
            if (etapa != null)
            {
                try
                {
                    context.Etapa.Remove(etapa);
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
