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
    public class ArticleController : ControllerBase
    {
        private readonly SaharaDbContext _saharaDbContext;
        public ArticleController(SaharaDbContext saharaDbContext)
        {
            _saharaDbContext = saharaDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Article>> GetArticles()
        {
            return _saharaDbContext.Articles;
        }
        [HttpGet("getAll/{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public virtual async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var list = await _saharaDbContext.Articles
                // .Skip(startIndex)
                // .Take(pageSize)
                .ToListAsync()
                ;
            int count = await _saharaDbContext.Articles.CountAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exist = await _saharaDbContext.Articles.FindAsync(id);
            if (exist == null)
                return NotFound();

            else
                return Ok(exist);

        }

        [HttpPost("post")]
        public virtual async Task<IActionResult> Post(Article model)
        {
            await _saharaDbContext.Set<Article>().AddAsync(model);

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

        // [HttpPost]
        // public virtual async Task<IActionResult> PostRange(List<Article> models)
        // {

        //     await _saharaDbContext.Set<Article>().AddRangeAsync(models);
        //     try
        //     {
        //         await _saharaDbContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }

        //     return Ok(models);
        // }


        // [HttpPut]
        // public async Task<ActionResult> Put(int id, Article article)
        // {
        //     if (id != article.id)
        //         return BadRequest();

        //     article.id = id;
        //     _saharaDbContext.Entry(article).State = EntityState.Modified;

        //     try
        //     {
        //         await _saharaDbContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         var exist = await _saharaDbContext.Articles.FindAsync(id);
        //         if (exist == null)
        //             return NotFound();
        //         else
        //             throw;
        //     }

        //     return NoContent();
        // }
         [HttpPut("put/{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] Article model)
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
            var model = await _saharaDbContext.Set<Article>().FindAsync(id);
            if (model == null)
            {
                return Ok(false);
            }

            _saharaDbContext.Set<Article>().Remove(model);
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
    }
}