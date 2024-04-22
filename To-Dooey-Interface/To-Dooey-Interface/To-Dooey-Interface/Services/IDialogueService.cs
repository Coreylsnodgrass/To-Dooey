using System.Threading.Tasks;
using To_Dooey_Interface.ViewModels;

namespace To_Dooey_Interface.Services
{
    public interface IDialogService
    {
        Task<string> ShowAddListDialogAsync();
        Task<string> ShowUpdateListDialogAsync(string currentName);
        Task<(string Description, CompletionStatus Status, string Responsibility)> ShowAddTaskDialogAsync();
        Task<(string Description, CompletionStatus Status, string Responsibility)> ShowUpdateTaskDialogAsync(TaskItemViewModel currentTask);
        Task<string> GetTaskDescriptionAsync();
        Task<CompletionStatus> GetTaskStatusAsync();
        Task<string> GetTaskResponsibilityAsync();
    }
}
