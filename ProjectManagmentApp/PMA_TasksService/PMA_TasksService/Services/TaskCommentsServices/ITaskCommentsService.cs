﻿using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.TaskCommentsServices
{
    public interface ITaskCommentsService : IBaseManagerService<TaskCommentsDTO>
    {
        Task<IEnumerable<TaskCommentsDTO>> GetByTaskId(int taskId);
    }
}
