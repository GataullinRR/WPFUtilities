using MVVMUtilities.Types;
using System;
using System.Linq;
using System.Windows.Markup;
using Utilities.Extensions;

namespace WPFUtilities
{
    public sealed class DoubleValueBinderExtension : MarkupExtension
    {
        readonly ValueBinder<double> _testMode;

        public DoubleValueBinderExtension(string range, int round)
        {
            var values = range
                .Remove("[ ] ( )".Split(" ").ToArray())
                .Split(":")
                .ToArray();
            var from = values[0].ParseToDoubleInvariant();
            var to = values[1].ParseToDoubleInvariant();
            var isFromInclusive = range.StartsWith("[");
            var isToInclusive = range.EndsWith("]");

            _testMode = new ValueBinder<double>(
                ((Func<string, double?>)ParsingEx.TryParseToDoubleInvariant).ToOldTryParse(),
                mv => mv.ToStringInvariant(),
                v => (isFromInclusive ? v >= from : v > from) && (isToInclusive ? v <= to : v < to),
                mv => mv.Round(round));
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            return (ValueBinder<object>)_testMode;
        }
    };
}
