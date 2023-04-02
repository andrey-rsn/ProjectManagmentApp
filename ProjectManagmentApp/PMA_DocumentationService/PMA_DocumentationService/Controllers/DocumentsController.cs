using Microsoft.AspNetCore.Mvc;
using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;
using PMA_DocumentationService.Services.DocumentServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_DocumentationService.Controllers
{
    [Route("api/v1/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/v1/documents/{documentId}
        [HttpGet("{documentId}")]
        public async Task<ActionResult<DocumentDTO>> GetById(int documentId)
        {
            var document = await _documentService.GetDocumentById(documentId);

            return Ok(document);
        }

        // GET api/v1/documents/byProject/{projectId}
        [HttpGet("byProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<DocumentDTO>>> GetByProjectId(int projectId)
        {
            var documents = await _documentService.GetAllDocumentsByProjectId(projectId);

            if (documents.Any())
            {
                return Ok(documents);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/v1/documents
        [HttpPost]
        public async Task<ActionResult> UploadDocument([FromForm] UploadDoucmentViewModel document)
        {
            var result = await _documentService.UploadDocument(document);

            if(result.Id > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/v1/documents/{documentId}
        [HttpDelete("{documentId}")]
        public async Task<ActionResult> UploadDocument(int documentId)
        {
            var result = await _documentService.DeleteById(documentId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
