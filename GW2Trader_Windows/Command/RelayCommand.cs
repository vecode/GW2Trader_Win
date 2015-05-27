using System;
using System.Windows.Input;

namespace GW2Trader_Windows.Command
{
    public abstract class RelayCommand : ICommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                _canExecuteChanged -= value;
            }
        }

        private event EventHandler _canExecuteChanged;

        public void OnCanExecuteChanged()
        {
            var handler = _canExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}