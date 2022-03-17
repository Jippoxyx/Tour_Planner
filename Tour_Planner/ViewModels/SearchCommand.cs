using System;
using System.Windows.Input;

namespace Tour_Planner
{
    internal class SearchCommand : ICommand
    {

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public SearchCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;

        }

        bool ICommand.CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        void ICommand.Execute(object parameter) => execute?.Invoke(parameter);

    }
}