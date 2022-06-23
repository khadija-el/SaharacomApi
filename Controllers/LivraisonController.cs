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

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public virtual async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var list = await _saharaDbContext.Set<LivraisonClient>()
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync()
                ;
            int count = await _saharaDbContext.Set<LivraisonClient>().CountAsync();

            return Ok(new { list = list, count = count });
        }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var list = await _saharaDbContext.Set<LivraisonClient>()/*.OrderByName<T>("Id")*/.ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var model = await _saharaDbContext.Set<LivraisonClient>().FindAsync(id);

            // if (model == null)
            // {
            //     return NotFound();
            // }

            return Ok(model);
        }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpPost]
        public virtual async Task<IActionResult> Post(LivraisonClient model)
        {
            await _saharaDbContext.Set<LivraisonClient>().AddAsync(model);

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

        [HttpPost]
        public virtual async Task<IActionResult> PostRange(List<LivraisonClient> models)
        {
            

            await _saharaDbContext.Set<LivraisonClient>().AddRangeAsync(models);
            try
            {
                await _saharaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(models);
        }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] LivraisonClient model)
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

        // [HttpPatch("{id}")]
        // public virtual async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<LivraisonClient> patchDoc)
        // {
        //     var model = await _saharaDbContext.Set<LivraisonClient>().FindAsync(id);

        //     patchDoc.ApplyTo(model, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        //     var res = new ObjectResult(model);

        //     _saharaDbContext.Entry(res.Value as LivraisonClient).State = EntityState.Modified;

        //     try
        //     {
        //         await _saharaDbContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }

        //     return NoContent();
        // }

        // [HttpGet("{name}/{value}")]
        // public virtual async Task<IActionResult> Autocomplete([FromRoute] string name,[FromRoute] string value)
        // {
        //     int i = typeof(LivraisonClient).FullName.LastIndexOf('.');
        //     string tableName = typeof(LivraisonClient).FullName.Substring(i + 1) + "s";

        //     string Name = char.ToUpper(name[0]) + name.Substring(1);

        //     var list = await _saharaDbContext.Set<LivraisonClient>()
        //         .FromSqlRaw(String.Format(@"SELECT * FROM {0} where {1} LIKE '%{2}%'", tableName, name, value.Replace("'", "''")))
        //         .Select(e => new{
        //             id = e.GetType().GetProperty("Id").GetValue(e, null),
        //             name = e.GetType().GetProperty(Name).GetValue(e, null),
        //         })
        //         .Take(10)
        //         .ToListAsync()
        //         ;

        //     return Ok(list);
        // }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await _saharaDbContext.Set<LivraisonClient>().FindAsync(id);
            if (model == null)
            {
                return Ok(false);
            }

            _saharaDbContext.Set<LivraisonClient>().Remove(model);
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
         [HttpGet]
        public async Task<ActionResult<int>> Count()
        {
            return await _saharaDbContext.Set<LivraisonClient>().CountAsync();
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetForSelect()
        {
            // var list0 = (await _saharaDbContext.Set<T>().ToListAsync())
            //     .Select((e, i) => new{
            //         p = e.GetType().GetProperties().Select(s => s.Name),
            //         name = e.GetType().GetProperties()[1].GetValue(e, null),
            //     })
            //     .ToList()
            // ;

             var list = await _saharaDbContext.Set<LivraisonClient>()
                .Select(e => new{
                    id = e.GetType().GetProperty("Id").GetValue(e, null),
                    name = e.GetType().GetProperties().ElementAtOrDefault(1).GetValue(e, null),
                })
                .ToListAsync()
            ;

            return Ok(list);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PutRange(List<LivraisonClient> models)
        {
            if (models.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            _saharaDbContext.Set<LivraisonClient>().UpdateRange(models);

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

        [HttpPost]
        public virtual async Task<IActionResult> DeleteRange(List<LivraisonClient> models)
        {
            if (models.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            _saharaDbContext.Set<LivraisonClient>().RemoveRange(models);

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

        [HttpPost]
        public virtual async Task<IActionResult> DeleteRangeByIds(List<int> ids)
        {
            if (ids.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            // var l =  ids.Select(model => (int)model.GetType().GetProperty("Id").GetValue(model, null)).ToList();
            var l =  ids.Select(id => _saharaDbContext.Set<LivraisonClient>().Find(id)).ToList();

            _saharaDbContext.Set<LivraisonClient>().RemoveRange(l);

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

    }
}