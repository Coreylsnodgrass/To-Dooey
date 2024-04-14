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
        public IActionResult PostTodoList([FromBody] CreateListModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.listName))
            {
                // Return a bad request if the model is null or the list name is empty
                return BadRequest("The list name is required.");
            }

            todoLists.Add(model.listName); // Add the list name to your list collection
            return CreatedAtAction(nameof(GetTodoLists), new { listName = model.listName }, model);
        }
        public class CreateListModel
        {
            public string listName { get; set; }
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
