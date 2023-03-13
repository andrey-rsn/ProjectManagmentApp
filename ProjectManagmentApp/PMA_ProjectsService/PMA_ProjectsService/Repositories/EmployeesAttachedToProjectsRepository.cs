﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Repositories
{
    public class EmployeesAttachedToProjectsRepository : BaseAsyncRepository<EmployeesAttachedToProjectsDTO, EmployeesAttachedToProjectsModel>, IEmployeesAttachedToProjectsRepository
    {
        public EmployeesAttachedToProjectsRepository(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
