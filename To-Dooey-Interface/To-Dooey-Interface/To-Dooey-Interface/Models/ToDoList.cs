// ToDoList.cs
using System.Collections.Generic;


namespace To_Dooey_Interface.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; }

        public ToDoList()
        {
            Tasks = new List<TaskItem>();
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        // ... include other properties for TaskItem here ...
    }
}
