using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BasicArticles.Server.Data.Comment;
using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository repository;

        public CommentController(ICommentRepository repository)
        {
            this.repository = repository;
        }

        // GET: /Comment/search?name=title
        [HttpGet("{search}/{name}")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> Search(string name)
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

        // GET: /Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetComments()
        {
            try
            {
                return Ok(await repository.GetCommentList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // GET: /Comment/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<CommentModel>> GetComment(long id)
        {
            try
            {
                var result = await repository.GetComment(id);

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

        // POST: /Comment
        [HttpPost]
        public async Task<ActionResult<CommentModel>> CreateComment(CommentModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var created = await repository.AddComment(model);

                return CreatedAtAction(nameof(GetComment), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        // PUT: /Comment/1
        [HttpPut("{id:long}")]
        public async Task<ActionResult<CommentModel>> UpdateComment(long id, CommentModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("Chase ID mismatch");
                }

                var toUpdate = await repository.GetComment(id);

                if (toUpdate == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.UpdateComment(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: /Comment/1
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<CommentModel>> DeleteComment(long id)
        {
            try
            {
                var toDelete = await repository.GetComment(id);

                if (toDelete == null)
                {
                    return NotFound($"Chase with id {id} not found");
                }

                return await repository.DeleteComment(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
