using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        // Mock database
        private static List<ToDoList> todoLists = new List<ToDoList>();

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
                return BadRequest("The list name is required.");
            }

            var newList = new ToDoList
            {
                Id = todoLists.Any() ? todoLists.Max(l => l.Id) + 1 : 1,
                Name = model.listName,
                Tasks = new List<TaskItem>()
            };
            todoLists.Add(newList);
            return CreatedAtAction(nameof(GetTodoLists), new { id = newList.Id }, newList);
        }

        // PUT: api/Lists/{id}
        [HttpPut("{id}")]
        public IActionResult PutTodoList(int id, [FromBody] UpdateListModel model)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == id);
            if (list == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.listName))
            {
                return BadRequest("The list name is required and cannot be empty.");
            }

            list.Name = model.listName;
            return NoContent();
        }

        // DELETE: api/Lists/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoList(int id)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == id);
            if (list == null)
            {
                return NotFound();
            }

            todoLists.Remove(list);
            return NoContent();
        }
        //-====================================================== Task Stuff=======================================


        // POST: api/Lists/{listId}/tasks
        [HttpPost("{listId}/tasks")]
        public IActionResult AddTask(int listId, [FromBody] TaskItem task)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == listId);
            if (list == null)
            {
                return NotFound("List not found.");
            }
            task.Id = list.Tasks.Any() ? list.Tasks.Max(t => t.Id) + 1 : 1;
            list.Tasks.Add(task);
            return CreatedAtAction("GetTask", new { listId = list.Id, taskId = task.Id }, task);
        }

        // GET: api/Lists/{listId}/tasks/{taskId}
        [HttpGet("{listId}/tasks/{taskId}")]
        public IActionResult GetTask(int listId, int taskId)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == listId);
            if (list == null)
            {
                return NotFound("List not found.");
            }
            var task = list.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return Ok(task);
        }

        [HttpPut("{listId}/tasks/{taskId}")]
        public IActionResult UpdateTask(int listId, int taskId, [FromBody] UpdateTaskModel model)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == listId);
            if (list == null)
            {
                return NotFound("List not found.");
            }
            var task = list.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.Description = model.Description;
            task.Status = model.Status;
            task.Responsibility = model.Responsibility;
            return NoContent();
        }
        // DELETE: api/Lists/{listId}/tasks/{taskId}
        [HttpDelete("{listId}/tasks/{taskId}")]
        public IActionResult DeleteTask(int listId, int taskId)
        {
            var list = todoLists.FirstOrDefault(l => l.Id == listId);
            if (list == null)
            {
                return NotFound("List not found.");
            }

            var task = list.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            list.Tasks.Remove(task);
            return NoContent();
        }

    }

    public class UpdateTaskModel
    {
        public string Description { get; set; }
        public CompletionStatus Status { get; set; }
        public string Responsibility { get; set; }
    }
    public class CreateListModel
    {
        public string listName { get; set; }
    }

    public class UpdateListModel
    {
        public string listName { get; set; }
    }
}
