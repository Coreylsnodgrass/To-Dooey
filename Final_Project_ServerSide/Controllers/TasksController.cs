using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Final_Project_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<string> tasks = new List<string>(); // Mock database for tasks

        // GET: api/Tasks
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult PostTask(string taskName)
        {
            tasks.Add(taskName);
            return CreatedAtAction(nameof(PostTask), new { taskName = taskName });
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (id < 0 || id >= tasks.Count)
            {
                return NotFound();
            }

            tasks.RemoveAt(id);
            return NoContent();
        }
    }
}
