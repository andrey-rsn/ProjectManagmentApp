using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly string _applicationHost;

        public DocumentService(IDocumentRepository documentRepository, IWebHostEnvironment appEnvironment, IOptions<BaseAppHost> appHost)
        {
            _documentRepository = documentRepository;
            _appEnvironment = appEnvironment;
            _applicationHost = appHost.Value.AppHost;
        }

        public async Task<DocumentDTO> GetDocumentById(int documentId)
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

                string path = string.Concat("/UploadedFiles/",Guid.NewGuid(),"_", documentModel.UploadedFile.FileName);

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await documentModel.UploadedFile.CopyToAsync(fileStream);
                }

                var url = string.Concat(_applicationHost, path);

                DocumentDTO uploadedDocument = new DocumentDTO { 
                    DocumentName= documentModel.DocumentName ,
                    DocumentDescription= documentModel.DocumentDescription ,
                    ProjectId= documentModel.ProjectId ,
                    UploadDate= DateTime.UtcNow,
                    FileName= documentModel.UploadedFile.FileName,
                    FileUrl= url
                };

                var result = await _documentRepository.AddAsync(uploadedDocument);

                return result;
            }

            return new DocumentDTO();
        }
    }
}
