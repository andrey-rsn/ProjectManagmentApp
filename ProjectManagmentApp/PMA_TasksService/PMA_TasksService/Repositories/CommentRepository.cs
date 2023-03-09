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
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddRangeAsync(IEnumerable<CommentDTO> comments)
        {
            var commentsModels = _mapper.Map<List<Comment>>(comments);

            commentsModels.ForEach(async comm => await _dbContext.Comments.AddAsync(comm));

            await _dbContext.SaveChangesAsync();
        }
    }
}
