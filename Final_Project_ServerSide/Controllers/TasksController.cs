﻿using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_ServerSide.Controllers
{
    public class TaskItemDTO
    {
        public string Description { get; set; }
        public CompletionStatus Status { get; set; }
        public string Responsibility { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.TaskItems.ToListAsync();
            return Ok(tasks);
        }
        // GET: api/Tasks/ByList/{listId}
        [HttpGet("ByList/{listId}")]
        public async Task<IActionResult> GetTasksByList(int listId)
        {
            var tasks = await _context.TaskItems.Where(task => task.ToDoListId == listId).ToListAsync();
            return Ok(tasks);
        }

        // POST: api/Tasks/{listId} - Create a task within a specific list
        [HttpPost("{listId}")]
        public async Task<IActionResult> PostTask(int listId, [FromBody] TaskItemDTO taskDTO)
        {
            if (taskDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = await _context.ToDoLists.FindAsync(listId);
            if (list == null)
            {
                return NotFound($"List with ID {listId} not found.");
            }

            var task = new TaskItem
            {
                Description = taskDTO.Description,
                Status = taskDTO.Status,
                Responsibility = taskDTO.Responsibility,
                ToDoListId = listId
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }


        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        // PUT: api/Tasks/{id} - Update a task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItemDTO taskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            task.Description = taskDTO.Description;
            task.Status = taskDTO.Status;
            task.Responsibility = taskDTO.Responsibility;

            _context.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskItems.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tasks/{id} - Delete a task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
