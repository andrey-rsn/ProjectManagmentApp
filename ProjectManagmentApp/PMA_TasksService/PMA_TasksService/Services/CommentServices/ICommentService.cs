using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.CommentServices
{
    public interface ICommentService : IBaseManagerService<CommentDTO>
    {
        Task<IEnumerable<CommentDTO>> GetByAuthorId(int authorId);
        Task<IEnumerable<CommentDTO>> GetByTaskId(int taskId);
        Task AddRange(IEnumerable<CommentDTO> comments);
    }
}
