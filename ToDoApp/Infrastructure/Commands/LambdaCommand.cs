using ToDoApp.Infrastructure.Commands.Base;

namespace ToDoApp.Infrastructure.Commands
{
    internal class LambdaCommand(Action<object> execute, Func<object, bool> canExecute) : Command
    {
        public Action<object> _execute = execute;
        public Func<object, bool> _canExecuted = canExecute;

        public override bool CanExecute(object? parameter) => _canExecuted?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => _execute(parameter);
    }
}