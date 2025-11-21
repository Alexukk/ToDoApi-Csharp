using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Mapping
{
    public static class TaskMapping
    {
        public static GetTaskDTO ToGetTaskDTO(this TaskToDo task)
        {
            return new GetTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            };
        }

        public static TaskToDo ToEntity(this CreateTaskDTO createTaskDTO)
        {
            return new TaskToDo
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void ApplyUpdate(this TaskToDo ExisitngEntity, UpdateTaskDTO task)
        {
            
            ExisitngEntity.Title = task.Title;
            ExisitngEntity.Description = task.Description;

        }



    }
}
