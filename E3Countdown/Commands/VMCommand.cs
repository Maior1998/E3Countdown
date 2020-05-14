using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3Countdown.Commands
{
    /// <summary>
    /// Представляет класс для команд модели MVVM.
    /// </summary>
    public class VMCommand : ICommand
    {
        /// <summary>
        /// Делегат, в который помещается действие, которое должна исполнить команда. Принимает на вход все, что угодно.
        /// </summary>
        private readonly Action<object> execute;

        /// <summary>
        /// Делегат, в который помещается функция проверки, может ли команда быть исполнена в данный момент.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Событие изменения условий, на которых базируется видимость данной команды. Заставит вызваться метод проверки <see cref="CanExecute"/>.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="execute">Функция, которая будет срабатывать при выполнении команды.</param>
        /// <param name="canExecute">Функция, которая будет определять, можно ли выполнить команду в данный момент.</param>
        public VMCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Функция, проверяющая, можно ли выполнить команду в данный момент. Если не указана своя функция проверки <see cref="canExecute"/>, всегда будет возвращать true, иначе вернет то, что записано в пользовательской функции <see cref="canExecute"/>.
        /// </summary>
        /// <param name="parameter">Параметр, который будет передан пользовательской функции, если та существует.</param>
        /// <returns>Булево значение - может ли команда быть исполнена в данный момент.</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Запускает пользовательскую функцию команды (<see cref="execute"/>).
        /// </summary>
        /// <param name="parameter">Параметр, который будет передан пользовательской функции.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
