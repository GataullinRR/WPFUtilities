using System;
using System.Windows.Markup;
using Vectors;
using WPFUtilities.Types;

namespace WPFUtilities.Converters
{
    public sealed class CommandExtension : MarkupExtension
    {
        readonly CommandParameter _value;

        public CommandExtension(object parameter)
        {
            _value = new CommandParameter(parameter);
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            return _value;
        }
    };
}
