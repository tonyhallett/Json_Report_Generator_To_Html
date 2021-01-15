using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BrowserControl
{
    public class RelayCommand<T> : ICommand where T : class
    {
        #region Fields 
        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;
        #endregion // Fields 
        #region Constructors 
        public RelayCommand(Action<T> execute) : this(execute, null) { }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter as T);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter) { _execute(parameter as T); }
        #endregion // ICommand Members 
    }
}
