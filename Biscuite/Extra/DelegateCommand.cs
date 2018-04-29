using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Biscuite.Extra {
    class DelegateCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        private Action<object> _actionwithParameter { get; }
        private Action _actionWithoutParameter { get; }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (parameter == null)
                _actionWithoutParameter.Invoke();
            else
                _actionwithParameter.Invoke(parameter);
        }

        public DelegateCommand(Action action) {
            _actionWithoutParameter = action;
        }

        public DelegateCommand(Action<object> action) {
            _actionwithParameter = action;
        }
    }
}
