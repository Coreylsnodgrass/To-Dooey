using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        // Mock database
        private static List<string> todoLists = new List<string>();

        // GET: api/Lists
        [HttpGet]
        public IActionResult GetTodoLists()
        {
            return Ok(todoLists);
        }

        // POST: api/Lists
        [HttpPost]
        public IActionResult PostTodoList(string listName)
        {
            todoLists.Add(listName);
            return CreatedAtAction(nameof(PostTodoList), new { listName = listName });
        }

        // PUT: api/Lists/5
        [HttpPut("{id}")]
        public IActionResult PutTodoList(int id, string listName)
        {
            if (id < 0 || id >= todoLists.Count)
            {
                return NotFound();
            }

            todoLists[id] = listName;
            return NoContent();
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoList(int id)
        {
            if (id < 0 || id >= todoLists.Count)
            {
                return NotFound();
            }

            todoLists.RemoveAt(id);
            return NoContent();
        }
    }
}
