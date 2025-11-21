using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.DTOs;
using ToDoApi.Mapping;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
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
            var tasksFromDb = await _context.Tasks.ToListAsync();

            var allTasksDto = tasksFromDb.Select(task => task.ToGetTaskDTO()).ToList();

            if (allTasksDto.Count == 0)
            {
                return NotFound("No tasks found.");
            }

            return Ok(allTasksDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskDTO>> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound($"No tasks found by id {id}");
            }
            var taskRes = task.ToGetTaskDTO();


            return Ok(taskRes);
        }



        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskDTO createTaskDTO)
        {
            if (createTaskDTO == null)
            {
                return BadRequest("Task data is not fully provided.");
            }

            var task = createTaskDTO.ToEntity();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var resultDto = task.ToGetTaskDTO();

            return CreatedAtAction(nameof(GetTaskById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("/{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO updatedInfo)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound($"Task with id: {id} was not found.");
            }

            existingTask.ApplyUpdate(updatedInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("/{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id);
            if (taskToDelete == null)
            {
                return NoContent();
            }

            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       



    }
}
