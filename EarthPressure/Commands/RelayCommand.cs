using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EarthPressureCalculator.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Func <object, bool > _canExecuteMethod;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }    
        }

        public RelayCommand(Action<object> executeMethod)
        {
            _executeMethod = executeMethod;
            //_canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
            //if(_canExecuteMethod != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public void Execute(object? parameter)
        {
            _executeMethod(parameter);
        }
    }
}
