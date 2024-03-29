﻿using AutoMapper;
using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Services.Mapper
{
    public class MapConfig : Profile
    {
        public MapConfig() 
        { 
            CreateMap<Project,ProjectDTO>().ReverseMap();
            CreateMap<CreateProjectRequestModel, ProjectDTO>().ReverseMap();
            CreateMap<EmployeesAttachedToProjectsModel, EmployeesAttachedToProjectsDTO>().ReverseMap();
            CreateMap<ProjectsTasksModel, ProjectsTasksDTO>().ReverseMap();
        }
    }
}
