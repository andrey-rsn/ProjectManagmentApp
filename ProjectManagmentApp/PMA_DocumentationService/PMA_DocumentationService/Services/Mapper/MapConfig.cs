using AutoMapper;
using PMA_DocumentationService.Models;
using PMA_DocumentationService.Models.DTOs;

namespace PMA_DocumentationService.Services.Mapper
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<Document, DocumentDTO>().ReverseMap();
        }
    }
}
