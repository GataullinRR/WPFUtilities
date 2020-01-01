using System;
using System.Windows.Markup;
using Vectors;

namespace WPFUtilities.Converters
{
    public sealed class IntervalExtension : MarkupExtension
    {
        readonly Interval _value;

        public IntervalExtension(double from, double to)
        {
            _value = new Interval(from, to);
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            return _value;
        }
    };
}
