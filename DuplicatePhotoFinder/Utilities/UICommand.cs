using System;
using System.Windows.Input;

namespace DuplicatePhotoFinder
{
    public class UICommand : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public UICommand(Predicate<object> canExecuteHandler, Action<object> executeHandler)
        {
            CanExecuteDelegate = canExecuteHandler;
            ExecuteDelegate = executeHandler;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate(parameter);
        }
    }
}
