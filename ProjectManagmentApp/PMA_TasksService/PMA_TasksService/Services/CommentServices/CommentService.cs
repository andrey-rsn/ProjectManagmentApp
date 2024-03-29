﻿using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDTO> Add(CommentDTO entity)
        {
            return await _commentRepository.AddAsync(entity);
        }

        public async Task<CommentDTO> Delete(int entityId)
        {
            var comment = await GetById(entityId);

            if(comment != null)
            {
                return await _commentRepository.DeleteAsync(comment);
            }

            throw new Exception("Объект не найден");
        }

        public async Task<IEnumerable<CommentDTO>> GetAll(int limit = 100)
        {
            var comments = await _commentRepository.GetAllAsync(limit);

            if (comments.Any())
            {
                return comments;
            }
            throw new Exception("Данные не найдены");
        }

        public async Task<CommentDTO> GetById(int entityId)
        {
            var comment = await _commentRepository.GetByIdAsync(entityId);
            
            if(comment != null )
            {
                return comment;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<CommentDTO> Update(CommentDTO entity)
        {
            return await _commentRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<CommentDTO>> GetByAuthorId(int authorId)
        {
            var comments = await _commentRepository.GetAsync(obj => obj.authorId== authorId);

            if (comments.Any())
            {
                return comments;
            }

            throw new Exception("Данные не найдены");
        }

        public async Task<IEnumerable<CommentDTO>> GetByTaskId(int taskId)
        {
            return await _commentRepository.GetAsync(obj => obj.associatedTaskId == taskId);
        }

        public async Task AddRange(IEnumerable<CommentDTO> comments)
        {
            await _commentRepository.AddRangeAsync(comments);
        }


    }
}
