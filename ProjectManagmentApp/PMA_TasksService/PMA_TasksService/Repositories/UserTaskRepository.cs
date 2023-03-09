using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_TasksService.Data;
using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PMA_TasksService.Repositories
{
    public class UserTaskRepository : BaseAsyncRepository<UserTaskDTO, UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
