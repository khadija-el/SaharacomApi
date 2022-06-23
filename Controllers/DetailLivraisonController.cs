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
    public class DetailLivraisonController : ControllerBase
    {
        private readonly SaharaDbContext _saharaDbContext;
        public DetailLivraisonController(SaharaDbContext saharaDbContext)
        {
            _saharaDbContext = saharaDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DetailLivraisonClient>> GetDetailLivraisons ()
        {
            return _saharaDbContext.DetailLivraisonClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.DetailLivraisonClient.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DetailLivraisonClient detail)
        {
            await _saharaDbContext.DetailLivraisonClient.AddAsync(detail);
            await _saharaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, DetailLivraisonClient detail){
            if( id != detail.Id)
               return BadRequest();

            detail.Id = id;
            _saharaDbContext.Entry(detail).State = EntityState.Modified;

             try{
                await _saharaDbContext.SaveChangesAsync();
             }
             catch(DbUpdateConcurrencyException)
             {
                var exist = await _saharaDbContext.DetailLivraisonClient.FindAsync(id);
                if(exist == null)
                   return NotFound();
                else 
                   throw;
             }

             return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var exist = await _saharaDbContext.DetailLivraisonClient.FindAsync(id);
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