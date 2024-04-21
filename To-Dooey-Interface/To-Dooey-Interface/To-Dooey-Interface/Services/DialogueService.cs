using System.Threading.Tasks;
using To_Dooey_Interface.ViewModels;
using Avalonia.Controls;
using To_Dooey_Interface.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;

namespace To_Dooey_Interface.Services
{
    public class DialogService : IDialogService
    {

        public async Task<string> ShowAddListDialogAsync()
        {
            // Define the dialog window
            var dialog = new Window
            {
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "New List",
                CanResize = false
            };

            // Create a TextBox for user input
            var textBox = new TextBox
            {
                Width = 280,
                Watermark = "Enter list name..."
            };

            // Create Buttons for OK and Cancel
            var okButton = new Button
            {
                Content = "OK",
                Width = 130,
                Margin = new Thickness(10)
            };
            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 130,
                Margin = new Thickness(10)
            };

            // Setup the OK button click event
            var completionSource = new TaskCompletionSource<string>();
            okButton.Click += (_, _) =>
            {
                completionSource.SetResult(textBox.Text);
                dialog.Close();
            };

            // Setup the Cancel button click event
            cancelButton.Click += (_, _) =>
            {
                completionSource.SetResult(null);
                dialog.Close();
            };

            // Layout the dialog content
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            stackPanel.Children.Add(textBox);
            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            buttonPanel.Children.Add(okButton);
            buttonPanel.Children.Add(cancelButton);
            stackPanel.Children.Add(buttonPanel);

            // Set the content of the dialog window
            dialog.Content = stackPanel;

            // Show the dialog
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Centers the dialog on the main window
                dialog.ShowDialog(desktop.MainWindow);
            }

            // Await the task completion source which gets signaled by button clicks
            return await completionSource.Task;
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
