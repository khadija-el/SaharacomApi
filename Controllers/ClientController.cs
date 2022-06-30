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

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.Clients.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost("post")]
        public virtual async Task<IActionResult> Post(Client model)
        {
            await _saharaDbContext.Set<Client>().AddAsync(model);

            try
            {
                await _saharaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(model);
        }

        [HttpPut("put/{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] Client model)
        {
            _saharaDbContext.Entry(model).State = EntityState.Modified;

            try
            {
                await _saharaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await _saharaDbContext.Clients.FindAsync(id);
            if (model == null)
            {
                return Ok(false);
            }

            _saharaDbContext.Clients.Remove(model);
            try
            {
                await _saharaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(true);
        }

          [HttpGet("GetForSelect")]
          public virtual async Task<IActionResult> GetForSelect()
        {
            var list0 = (await _saharaDbContext.Set<Client>().ToListAsync())
                .Select((e, i) => new{
                    p = e.GetType().GetProperties().Select(s => s.Name),
                    name = e.GetType().GetProperties()[2].GetValue(e, null),
                })
                .ToList()
                
            ;
        
            // var list = await _saharaDbContext.Clients
            //     .Select(e => new{
            //         id = e.GetType().GetProperty("Id").GetValue(e, null),
            //         name = e.GetType().GetProperties().ElementAtOrDefault(1).GetValue(e, null),
            //     })
            //     .ToListAsync() 
            // ;

            return Ok(list0);
        }

    }
}