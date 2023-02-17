using AutoMapper;
using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;
using PMA_TasksService.Services.CommentServices;
using PMA_TasksService.Services.UserTaskStatusServices;

namespace PMA_TasksService.Services.UserTaskServices
{
    public class UserTaskViewService : IUserTaskViewService
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly ICommentService _commentService;
        private readonly IUserTaskStatusService _userTaskStatusService;
        private readonly IMapper _mapper;

        public UserTaskViewService(IUserTaskRepository userTaskRepository, ICommentService commentService, IUserTaskStatusService userTaskStatusService, IMapper mapper)
        {
            _userTaskRepository = userTaskRepository;
            _commentService = commentService;
            _userTaskStatusService = userTaskStatusService;
            _mapper = mapper;
        }

        public async Task<UserTaskViewModel> Add(UserTaskViewModel entity)
        {
            var userTask = _mapper.Map<UserTaskDTO>(entity);

            var addResult = await _userTaskRepository.AddAsync(userTask);

            try
            {
                if (addResult != null)
                {
                    if (entity.comments != null && entity.comments.Any())
                    {
                        entity.comments.ToList().ForEach(c => c.associatedTaskId = addResult.taskId);
                        await _commentService.AddRange(entity.comments);
                    }
                    entity.id = addResult.taskId;
                    return entity;
                }
                else
                {
                    throw new Exception("Данные отсутствуют");
                }
                
            }
            catch
            {
                await _userTaskRepository.DeleteAsync(userTask);

                throw;
            }
        }

        public async Task<UserTaskViewModel> Delete(int entityId)
        {
            var userTaskView = await GetById(entityId);

            if (userTaskView != null)
            {
                foreach (var comment in userTaskView.comments)
                {
                    await _commentService.Delete(comment.commentId);
                }

                var userTask = _mapper.Map<UserTaskDTO>(userTaskView);

                await _userTaskRepository.DeleteAsync(userTask);

                return userTaskView;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<IEnumerable<UserTaskViewModel>> GetAll(int limit = 100)
        {
            List<UserTaskViewModel> userTaskViews = new List<UserTaskViewModel>();

            try
            {
                var userTasks = await _userTaskRepository.GetAllAsync(limit);

                if (userTasks.Any())
                {
                    foreach (var userTask in userTasks)
                    {
                        userTaskViews.Add(await GetById(userTask.taskId));
                    }

                    return userTaskViews;
                }

                throw new Exception("Данные не найдены");

            }
            catch
            {
                throw;
            }

        }

        public async Task<UserTaskViewModel> GetById(int entityId)
        {
            var userTaskView = new UserTaskViewModel();

            var userTask = await _userTaskRepository.GetByIdAsync(entityId);

            if(userTask != null)
            {
                var comments = await _commentService.GetByTaskId(userTask.taskId);

                var status = await _userTaskStatusService.GetById(userTask.userTaskStatusId);

                userTaskView = _mapper.Map<UserTaskViewModel>(userTask);

                userTaskView.comments = comments;

                userTaskView.status = status.statusName;

                return userTaskView;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<UserTaskViewModel> Update(UserTaskViewModel entity)
        {
            var userTask = _mapper.Map<UserTaskDTO>(entity);

            await _userTaskRepository.UpdateAsync(userTask);

            foreach(var comment in entity.comments)
            {
                await _commentService.Update(comment);
            }
            
            return entity;
        }

    }
}
