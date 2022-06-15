using saharacomnew.Models;
using saharacomnew.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newsaharacom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvaController : ControllerBase
    {
        private readonly SaharaDbContext _saharaDbContext;
        public TvaController(SaharaDbContext saharaDbContext)
        {
            _saharaDbContext = saharaDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tva>> GetTvas ()
        {
            return _saharaDbContext.Tvas;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.Tvas.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Tva tva)
        {
            await _saharaDbContext.Tvas.AddAsync(tva);
            await _saharaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, Tva tva){
            if( id != tva.id)
               return BadRequest();

            tva.id = id;
            _saharaDbContext.Entry(tva).State = EntityState.Modified;

             try{
                await _saharaDbContext.SaveChangesAsync();
             }
             catch(DbUpdateConcurrencyException)
             {
                var exist = await _saharaDbContext.Tvas.FindAsync(id);
                if(exist == null)
                   return NotFound();
                else 
                   throw;
             }

             return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var exist = await _saharaDbContext.Tvas.FindAsync(id);
            if(exist == null)
            return NotFound();

            else{
            _saharaDbContext.Remove(exist);
            await _saharaDbContext.SaveChangesAsync();

            return Ok();
            }
        }
    }
}