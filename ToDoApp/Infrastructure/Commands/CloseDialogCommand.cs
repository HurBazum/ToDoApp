using ToDoApp.Infrastructure.Commands.Base;
using ToDoApp.Views.Windows;

namespace ToDoApp.Infrastructure.Commands
{
    internal class CloseDialogCommand : Command
    {
        public bool? DialogResult { get; set; }

        public override bool CanExecute(object? parameter) => parameter is GoalEditorWindow;

        public override void Execute(object? parameter)
        {
            if(!CanExecute(parameter))
            {
                return;
            }

            var window = (GoalEditorWindow)parameter;
            window.DialogResult = DialogResult;
            window.Close();
        }
    }
}