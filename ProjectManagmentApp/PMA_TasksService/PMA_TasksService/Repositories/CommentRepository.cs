using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_TasksService.Data;
using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Repositories
{
    public class CommentRepository : BaseAsyncRepository<CommentDTO, Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
