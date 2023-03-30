using Microsoft.EntityFrameworkCore;
using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;
using PMA_DocumentationService.Repositories.Interfaces;
using System.IO;

namespace PMA_DocumentationService.Services.DocumentServices
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public DocumentService(IDocumentRepository documentRepository, IWebHostEnvironment appEnvironment)
        {
            _documentRepository = documentRepository;
            _appEnvironment = appEnvironment;
        }

        public async Task<DocumentDTO> GetAllDocumentsById(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);

            return document;
        }

        public async Task<IEnumerable<DocumentDTO>> GetAllDocumentsByProjectId(int projectId)
        {
            var documents = await _documentRepository.GetAsync(t=>t.ProjectId == projectId);

            return documents;
        }

        public async Task<DocumentDTO> UploadDocument(UploadDoucmentViewModel documentModel)
        {
            if (documentModel.UploadedFile != null)
            {

                string path = string.Concat(_appEnvironment.WebRootPath, "/Files/", documentModel.UploadedFile.FileName, new Guid());

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await documentModel.UploadedFile.CopyToAsync(fileStream);
                }

                DocumentDTO uploadedDocument = new DocumentDTO { 
                    DocumentName= documentModel.DocumentName ,
                    DocumentDescription= documentModel.DocumentDescription ,
                    ProjectId= documentModel.ProjectId ,
                    UploadDate= DateTime.UtcNow,
                    FileName= documentModel.UploadedFile.FileName,
                    FileUrl= path
                };

                var result = await _documentRepository.AddAsync(uploadedDocument);

                return result;
            }

            return new DocumentDTO();
        }
    }
}
