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
    public class ClientController : ControllerBase
    {
        private readonly SaharaDbContext _saharaDbContext;
        public ClientController(SaharaDbContext saharaDbContext)
        {
            _saharaDbContext = saharaDbContext;
        }

      [HttpGet("getAll/{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public virtual async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var list = await _saharaDbContext.Clients
                // .Skip(startIndex)
                // .Take(pageSize)
                .ToListAsync()
                ;
            int count = await _saharaDbContext.Clients.CountAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients ()
        {
            return _saharaDbContext.Clients;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.Clients.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            await _saharaDbContext.Clients.AddAsync(client);
            await _saharaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, Client client){
            if( id != client.id)
               return BadRequest();

            client.id = id;
            _saharaDbContext.Entry(client).State = EntityState.Modified;

             try{
                await _saharaDbContext.SaveChangesAsync();
             }
             catch(DbUpdateConcurrencyException)
             {
                var exist = await _saharaDbContext.Clients.FindAsync(id);
                if(exist == null)
                   return NotFound();
                else 
                   throw;
             }

             return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var exist = await _saharaDbContext.Clients.FindAsync(id);
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