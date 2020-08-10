using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BasicArticles.Server.Data.Category;
using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET: /Category/search?name=title
        [HttpGet("{search}/{name}")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Search(string name)
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

        // GET: /Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategorys()
        {
            try
            {
                return Ok(await repository.GetCategoryList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Category/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(long id)
        {
            try
            {
                var result = await repository.GetCategory(id);

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

        // POST: /Category
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> CreateCategory(CategoryModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var created = await repository.AddCategory(model);

                return CreatedAtAction(nameof(GetCategory), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // PUT: /Category/1
        [HttpPut("{id:long}")]
        public async Task<ActionResult<CategoryModel>> UpdateCategory(long id, CategoryModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("Chase ID mismatch");
                }

                var toUpdate = await repository.GetCategory(id);

                if (toUpdate == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.UpdateCategory(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: /Category/1
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<CategoryModel>> DeleteCategory(long id)
        {
            try
            {
                var toDelete = await repository.GetCategory(id);

                if (toDelete == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.DeleteCategory(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
