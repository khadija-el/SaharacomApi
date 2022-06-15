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
    public class LivraisonController : ControllerBase
    {
        private readonly SaharaDbContext _saharaDbContext;
        public LivraisonController(SaharaDbContext saharaDbContext)
        {
            _saharaDbContext = saharaDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Livraison>> GetLivraisons ()
        {
            return _saharaDbContext.Livraisons;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.Livraisons.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Livraison livraison)
        {
            await _saharaDbContext.Livraisons.AddAsync(livraison);
            await _saharaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, Livraison livraison){
            if( id != livraison.id)
               return BadRequest();

            livraison.id = id;
            _saharaDbContext.Entry(livraison).State = EntityState.Modified;

             try{
                await _saharaDbContext.SaveChangesAsync();
             }
             catch(DbUpdateConcurrencyException)
             {
                var exist = await _saharaDbContext.Livraisons.FindAsync(id);
                if(exist == null)
                   return NotFound();
                else 
                   throw;
             }

             return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var exist = await _saharaDbContext.Livraisons.FindAsync(id);
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