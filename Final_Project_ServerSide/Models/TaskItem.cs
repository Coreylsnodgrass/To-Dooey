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
    }

    
}