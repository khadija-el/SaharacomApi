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
        public ActionResult<IEnumerable<Article>> GetArticles ()
        {
            return _saharaDbContext.Articles;
        }
         [HttpGet("getAll/{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public virtual async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var list = await _saharaDbContext.Articles
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync()
                ;
            int count = await _saharaDbContext.Articles.CountAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id){
            var exist = await _saharaDbContext.Articles.FindAsync(id);
            if(exist == null)
            return NotFound();

            else
            return Ok(exist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Article article)
        {
            await _saharaDbContext.Articles.AddAsync(article);
            await _saharaDbContext.SaveChangesAsync();

            return Ok(article);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, Article article){
            if( id != article.id)
               return BadRequest();

            article.id = id;
            _saharaDbContext.Entry(article).State = EntityState.Modified;

             try{
                await _saharaDbContext.SaveChangesAsync();
             }
             catch(DbUpdateConcurrencyException)
             {
                var exist = await _saharaDbContext.Articles.FindAsync(id);
                if(exist == null)
                   return NotFound();
                else 
                   throw;
             }

             return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var exist = await _saharaDbContext.Articles.FindAsync(id);
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