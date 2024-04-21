
using System.Text.Json.Serialization;

namespace TodoApi.Models
{

    public enum CompletionStatus
    {
        NotStarted,
        Started,
        InProgress,
        Completed
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public CompletionStatus Status { get; set; }
        public string Responsibility { get; set; }

        // Foreign key to link back to the ToDoList
        public int ToDoListId { get; set; }
        [JsonIgnore]
        public ToDoList ToDoList { get; set; }  // Navigation property
    }


}