using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;

namespace PMA_DocumentationService.Repositories.Interfaces
{
    public interface IDocumentRepository : IBaseAsyncRepository<DocumentDTO, Document>
    {
    }
}
