using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace toDo.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if(execute == null)
            {
                throw new NullReferenceException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object _)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object _)
        {
            _execute.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
