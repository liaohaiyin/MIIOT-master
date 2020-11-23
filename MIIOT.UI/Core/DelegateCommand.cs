using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MIIOT.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private Action<object> _action;
        private bool _canExecute;

        public DelegateCommand(Action<object> action) : this(action, true)
        {

        }

        public DelegateCommand(Action<object> action, bool canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute;
        }

        public void Execute(object parameter)
        {
            this._action.Invoke(parameter);
        }
    }
}
