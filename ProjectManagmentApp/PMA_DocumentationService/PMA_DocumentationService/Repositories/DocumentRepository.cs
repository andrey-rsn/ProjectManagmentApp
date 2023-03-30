using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_DocumentationService.Data;
using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;
using PMA_DocumentationService.Repositories.Interfaces;

namespace PMA_DocumentationService.Repositories
{
    public class DocumentRepository : BaseAsyncRepository<DocumentDTO, Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
