using System.Threading.Tasks;
using To_Dooey_Interface.ViewModels;
using Avalonia.Controls;
using To_Dooey_Interface.Views;
using Avalonia;

namespace To_Dooey_Interface.Services
{
    public class DialogService : IDialogService
    {
        public Task<string> ShowAddListDialogAsync()
        {
            // Implement the logic to show a dialog for adding a new list
            // For example, you could use a modal window with a text input field
            // Return the input provided by the user
            return Task.FromResult("New List Name");
        }

        public async Task<(string Description, CompletionStatus Status, string Responsibility)> ShowAddTaskDialogAsync()
        {
            // Here you can interact with the user interface to collect task information,
            // or directly pass default values or empty strings if UI interaction is not necessary.

            // For example:
            string description = "New Task Description";
            CompletionStatus status = CompletionStatus.NotStarted;
            string responsibility = "Person Responsible";

            // Return the task details
            return (description, status, responsibility);
        }

        public Task<string> GetTaskDescriptionAsync()
        {
            // Implement the logic to get the task description from the user asynchronously
            // For example, you could use a modal window with a text input field
            // Return the task description provided by the user
            return Task.FromResult("Task Description");
        }

        public Task<CompletionStatus> GetTaskStatusAsync()
        {
            // Implement the logic to get the task status from the user asynchronously
            // For example, you could use a modal window with a dropdown or radio buttons for selecting the status
            // Return the task status selected by the user
            return Task.FromResult(CompletionStatus.NotStarted);
        }

        public Task<string> GetTaskResponsibilityAsync()
        {
            // Implement the logic to get the task responsibility from the user asynchronously
            // For example, you could use a modal window with a text input field
            // Return the task responsibility provided by the user
            return Task.FromResult("Task Responsibility");
        }
    }
}
