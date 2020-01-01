using System.Collections.Generic;
using System.Linq;

namespace WPFUtilities.Converters
{

    public sealed class Wrapper<T>
    {
        internal Wrapper(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public class WrappingEnumerableConverter : SmartConverterTemplate<IEnumerable<object>, IEnumerable<Wrapper<object>>>
    {
        public override IEnumerable<Wrapper<object>> Convert(IEnumerable<object> value)
        {
            return value.Select(v => new Wrapper<object>(v));
        }
    }
}
