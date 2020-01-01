using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Types
{
    public class ActionCommand : ActionCommand<object>
    {
        public static implicit operator ActionCommand(Action action)
        {
            return new ActionCommand(action);
        }

        /// <summary>
        /// View checks this property
        /// </summary>
        public bool IsCanExecute => CanExecute();

        #region ##### CTORS #####

        public ActionCommand(Func<Task> actionAsync)
            : base(actionAsync) { }
        public ActionCommand(
            Func<Task> actionAsync,
            BusyObject busy)
            : base(actionAsync, () => !busy, busy) { }
        public ActionCommand(
            Func<Task> actionAsync,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifyer)
            : base(actionAsync, canExecuteChecker, canExecuteChangedNotifyer) { }
        public ActionCommand(
            Func<Task> actionAsync,
            Func<bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifyer)
            : base(actionAsync, canExecuteChecker, canExecuteChangedNotifyer) { }

        public ActionCommand(Action action)
            : this(o => action(), (o) => true) { }
        ActionCommand(Action<object> action)
            : this(action, (o) => true) { }
        ActionCommand(Action<object> action, Func<bool> canExecuteChecker)
            : this(action, (o) => canExecuteChecker()) { }
        public ActionCommand(Action action, Func<bool> canExecuteChecker)
            : this((o) => action(), (o) => canExecuteChecker()) { }
        ActionCommand(Action action, Func<object, bool> canExecuteChecker)
            : this((o) => action(), canExecuteChecker) { }
        ActionCommand(Action<object> action, Func<object, bool> canExecuteChecker)
            : base(action, canExecuteChecker) { }
        public ActionCommand(Action action, BusyObject busy)
            : this(action, () => !busy.IsBusy, busy) { }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifyer)
            : base(action, canExecuteChecker, canExecuteChangedNotifyer) { }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifyer)
            : base(action, canExecuteChecker, canExecuteChangedNotifyer) { }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifyer,
            string propertyName)
            : base(action, canExecuteChecker, canExecuteChangedNotifyer, propertyName) { }

        #endregion

        protected override void onCanExecuteChanged()
        {
            onPropertyChanged(nameof(IsCanExecute));
            base.onCanExecuteChanged();
        }

        /// <summary>
        /// Executes the command even if <see cref="CanExecute"/> returns False
        /// </summary>
        public void Execute()
        {
            ((ICommand)this).Execute(null);
        }

        /// <summary>
        /// Executes in async mode if it is async AcionCommand, otherwise synchronously
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            Execute();
            if (_asyncActionProcess != null)
            {
                await _asyncActionProcess;
            }
        }

        public void ExecuteIfCanBeExecuted()
        {
            if (CanExecute())
            {
                ((ICommand)this).Execute(null);
            }
        }
        public void ExecuteIfCanOrThrow()
        {
            if (CanExecute())
            {
                ((ICommand)this).Execute(null);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public bool CanExecute()
        {
            return ((ICommand)this).CanExecute(null);
        }
    }

    public class CommandParameter
    {
        public object Value { get; private set; }

        internal CommandParameter(object parameter)
        {
            Value = parameter;
        }
    }
    public class CommandParameter<T> : CommandParameter
    {
        public bool IsSet { get; }
        public new T Value => IsSet ? (T)base.Value : throw new InvalidOperationException("The parameter is not set");

        internal CommandParameter(bool isSet, object parameter)
            : base(parameter)
        {
            IsSet = isSet;
        }
    }

    public class ActionCommand<T> : ICommand, INotifyPropertyChanged
    {
        public static implicit operator ActionCommand<T>(Action<CommandParameter<T>> action)
        {
            return new ActionCommand<T>(action);
        }

        readonly Action<CommandParameter<T>> _action;
        readonly Func<CommandParameter<T>, bool> _canExecuteChecker;
        readonly string _propertyName;
        readonly bool _triggerOnAnyProperty;
        bool _asyncActionIsExecuting;
        bool _IsCanBeExecuted = true;
        protected Task _asyncActionProcess;

        public event EventHandler Executed = delegate { };
        public event EventHandler CanExecuteChanged = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// It is not the same as <see cref="CanExecute(T)"/>. It's just a filter
        /// </summary>
        public bool CanBeExecuted
        {
            get => _IsCanBeExecuted;
            set
            {
                _IsCanBeExecuted = value;
                Update();
            }
        }

        #region ##### CTORS #####

        public ActionCommand(Action action)
            : this(o => action(), (o) => true) { }
        public ActionCommand(
            Func<CommandParameter<T>, Task> actionAsync,
            BusyObject busy)
            : this(actionAsync, () => !busy, busy) { }
        public ActionCommand(Func<Task> actionAsync)
            : this(_ => actionAsync())
        {

        }
        public ActionCommand(Func<CommandParameter<T>, Task> actionAsync)
        {
            _canExecuteChecker = (o) => !_asyncActionIsExecuting;
            _action = async (o) =>
            {
                try
                {
                    _asyncActionIsExecuting = true;
                    Update();
                    _asyncActionProcess = actionAsync(o);
                    await _asyncActionProcess;
                }
                finally
                {
                    _asyncActionProcess = null;
                    _asyncActionIsExecuting = false;
                    Update();
                }
            };
        }
        public ActionCommand(Action<CommandParameter<T>> action)
            : this(action, (o) => true) { }
        public ActionCommand(Action<CommandParameter<T>> action, Func<bool> canExecuteChecker)
            : this(action, (o) => canExecuteChecker()) { }
        public ActionCommand(Action action, Func<bool> canExecuteChecker)
            : this((o) => action(), (o) => canExecuteChecker()) { }
        public ActionCommand(Action action, Func<CommandParameter<T>, bool> canExecuteChecker)
            : this((o) => action(), canExecuteChecker) { }
        public ActionCommand(Action<CommandParameter<T>> action, Func<CommandParameter<T>, bool> canExecuteChecker)
        {
            ThrowUtils.ThrowIf_NullArgument(action, canExecuteChecker);

            _action = action;
            _canExecuteChecker = canExecuteChecker;
        }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifier)
            : this((o) => action(), canExecuteChecker, canExecuteChangedNotifier, null)
        {
            _triggerOnAnyProperty = true;
        }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifier,
            string propertyName)
            : this((o) => action(), canExecuteChecker, canExecuteChangedNotifier, propertyName) { }
        public ActionCommand(
            Action<CommandParameter<T>> action,
            Func<bool> canExecuteChecker,
            INotifyPropertyChanged canExecuteChangedNotifier,
            string propertyName)
            : this(action, canExecuteChecker)
        {
            _propertyName = propertyName;

            canExecuteChangedNotifier.PropertyChanged += CanExecuteChangedNotifier_PropertyChanged;
        }
        public ActionCommand(
            Func<Task> actionAsync,
            Func<bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifier)
            : this(_ => actionAsync(), canExecuteChecker, canExecuteChangedNotifier)
        {

        }
        public ActionCommand(
            Action action,
            Func<bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifier)
            : this(action)
        {
            _triggerOnAnyProperty = true;
            canExecuteChangedNotifier.ForEach(e => e.PropertyChanged += CanExecuteChangedNotifier_PropertyChanged);
            _canExecuteChecker = (o) => canExecuteChecker();
        }
        public ActionCommand(
            Func<CommandParameter<T>, Task> actionAsync,
            Func<bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifier)
        : this(actionAsync, _ => canExecuteChecker(), canExecuteChangedNotifier)
        {

        }
        public ActionCommand(
            Func<CommandParameter<T>, Task> actionAsync,
            Func<CommandParameter<T>, bool> canExecuteChecker,
            params INotifyPropertyChanged[] canExecuteChangedNotifier)
        : this(actionAsync)
        {
            _triggerOnAnyProperty = true;
            canExecuteChangedNotifier.ForEach(e => e.PropertyChanged += CanExecuteChangedNotifier_PropertyChanged);
            _canExecuteChecker = canExecuteChecker;
        }

        #endregion

        void CanExecuteChangedNotifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _propertyName || _triggerOnAnyProperty)
            {
                onCanExecuteChanged();
            }
        }
        protected virtual void onCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(this, EventArgs.Empty);
        }
        protected void onPropertyChanged(string property)
        {
            PropertyChanged.Invoke(this, property);
        }

        public void Execute(T parameter)
        {
            ((ICommand)this).Execute(new CommandParameter<T>(true, parameter));
        }
        public void ExecuteIfCanOrThrow(T parameter)
        {
            if (CanExecute(parameter))
            {
                ((ICommand)this).Execute(new CommandParameter<T>(true, parameter));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public bool CanExecute(T parameter)
        {
            return ((ICommand)this).CanExecute(new CommandParameter<T>(true, parameter));
        }

        void ICommand.Execute(object parameter)
        {
            _action(getParameter(parameter));

            Executed?.Invoke(this);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanBeExecuted && _canExecuteChecker(getParameter(parameter));
        }

        CommandParameter<T> getParameter(object parameter)
        {
            if (parameter is CommandParameter cp)
            {
                return new CommandParameter<T>(true, cp.Value);
            }
            {
                return new CommandParameter<T>(false, default);
            }
        }

        public void Update()
        {
            onCanExecuteChanged();
        }
    }
}
