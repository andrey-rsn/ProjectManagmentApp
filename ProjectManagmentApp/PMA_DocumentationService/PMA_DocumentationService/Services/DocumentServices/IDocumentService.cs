using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;

namespace PMA_DocumentationService.Services.DocumentServices
{
    public interface IDocumentService
    {
        Task<DocumentDTO> UploadDocument(UploadDoucmentViewModel documentModel);
        Task<IEnumerable<DocumentDTO>> GetAllDocumentsByProjectId(int projectId);
        Task<DocumentDTO> GetDocumentById(int documentId);

        Task<bool> DeleteById(int documentId);
    }
}
