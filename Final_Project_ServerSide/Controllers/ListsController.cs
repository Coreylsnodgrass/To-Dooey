using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;  // Add this to use EF Core functionalities

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<IActionResult> GetTodoLists()
        {
            var lists = await _context.ToDoLists
                                      .Include(l => l.Tasks)  // Ensure to load tasks with lists
                                      .ToListAsync();
            return Ok(lists);
        }

        // POST: api/Lists
        [HttpPost]
        public async Task<IActionResult> PostTodoList([FromBody] CreateListModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.listName))
            {
                return BadRequest("The list name is required.");
            }

            var newList = new ToDoList
            {
                Name = model.listName,
                Tasks = new List<TaskItem>()
            };
            _context.ToDoLists.Add(newList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoLists), new { id = newList.Id }, newList);
        }

        // PUT: api/Lists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id, [FromBody] UpdateListModel model)
        {
            var list = await _context.ToDoLists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.listName))
            {
                return BadRequest("The list name is required and cannot be empty.");
            }

            list.Name = model.listName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Lists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var list = await _context.ToDoLists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.ToDoLists.Remove(list);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Additional methods for tasks would also be updated to use _context
    }

    public class UpdateListModel
    {
        public string listName { get; set; }
    }

    public class CreateListModel
    {
        public string listName { get; set; }
    }
}
