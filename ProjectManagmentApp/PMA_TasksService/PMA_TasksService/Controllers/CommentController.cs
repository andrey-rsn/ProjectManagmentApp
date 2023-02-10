using Microsoft.AspNetCore.Mvc;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Services.CommentServices;
using System.Net;

namespace PMA_TasksService.Controllers
{
    [Route("api/v1/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/v1/comments?limit={limit}
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<CommentDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAll(int limit)
        {
            try
            {
                var comments = await _commentService.GetAll(limit);

                return Ok(comments);
            }
            catch
            {
                return NoContent();
            }

        }

        // GET api/v1/comments/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<CommentDTO>> GetById(int id)
        {
            try
            {
                var comment = await _commentService.GetById(id);

                return Ok(comment);
            }
            catch 
            {
                return NoContent();
            }
        }

        // GET api/v1/comments/{authorId}
        [HttpGet("byAuthor/{authorId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetByAuthorId(int authorId)
        {
            try
            {
                var comment = await _commentService.GetByAuthorId(authorId);

                return Ok(comment);
            }
            catch
            {
                return NoContent();
            }
        }

        // POST api/v1/comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommentDTO>> Add([FromBody] CommentDTO comment)
        {
            return Ok(await _commentService.Add(comment));
        }

        // PUT api/v1/comments
        [HttpPut]
        [ProducesResponseType(typeof(CommentDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommentDTO>> Update([FromBody] CommentDTO comment)
        {
            return Ok(await _commentService.Update(comment));
        }

        // DELETE api/v1/comments/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommentDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CommentDTO>> Delete(int id)
        {
            try
            {
                var deletedComment = await _commentService.Delete(id);

                return Ok(deletedComment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
