using TodoApi.Models;

namespace TodoApi.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
