using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BasicArticles.Server.Data.Article;
using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository repository;

        public ArticleController(IArticleRepository repository)
        {
            this.repository = repository;
        }

        // GET: /Article/search?name=title
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<ArticleModel>>> Search(string name)
        {
            try
            {
                var result = await repository.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleModel>>> GetArticles()
        {
            try
            {
                return Ok(await repository.GetArticleList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Article/Category/sports
        [HttpGet("Category/{categoryName}")]
        public async Task<ActionResult<List<ArticleModel>>> GetArticlesByCategory(string categoryName)
        {            
            try
            {
                return Ok(await repository.GetArticleListByCategory(categoryName));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Article/User/
        [HttpGet("User/{user}")]
        public async Task<ActionResult<List<ArticleModel>>> GetArticlesByUser(string user)
        {
            try
            {
                return Ok(await repository.GetArticleListByUser(user));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Article/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ArticleModel>> GetArticle(long id)
        {
            try
            {
                var result = await repository.GetArticle(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // POST: /Article
        [HttpPost]
        public async Task<ActionResult<ArticleModel>> CreateArticle(ArticleModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var created = await repository.AddArticle(model);

                return CreatedAtAction(nameof(GetArticle), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // PUT: /Article/1
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ArticleModel>> UpdateArticle(long id, ArticleModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("Chase ID mismatch");
                }

                var toUpdate = await repository.GetArticle(id);

                if (toUpdate == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.UpdateArticle(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: /Article/1
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ArticleModel>> DeleteArticle(long id)
        {
            try
            {
                var toDelete = await repository.GetArticle(id);

                if (toDelete == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.DeleteArticle(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
