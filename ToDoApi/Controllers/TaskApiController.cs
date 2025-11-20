using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.DTOs;

namespace ToDoApi.Controllers
{
    [Route("api/tasks/[controller]")]
    [ApiController]
    public class TaskApiController : ControllerBase
    {
        private readonly ToDoContext _context;
        public TaskApiController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<GetTaskDTO>>> GetAllTasks()
        {
            var AllTasks = await _context.Tasks
            .Select(task => new GetTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            })
        .ToListAsync();

            if (AllTasks.Count == 0)
            {
                return NotFound("No tasks found.");
            }

            return Ok(AllTasks);    
        }


        [HttpGet("/{id}")]
        public async Task<ActionResult<GetTaskDTO>> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound($"No tasks found by id {id}");
            }
            var taskRes = new GetTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            }; 

            return Ok(taskRes);
        }


    }
}
