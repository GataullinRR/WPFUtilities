using System;
using System.Windows.Markup;

namespace WPFUtilities.Converters
{
    public sealed class Int32Extension : MarkupExtension
    {
        readonly int _value;

        public Int32Extension(int value)
        {
            _value = value;
        }

        public override Object ProvideValue(IServiceProvider sp)
        {
            return _value;
        }
    };
}
