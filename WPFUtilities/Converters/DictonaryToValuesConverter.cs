using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Converters
{
    [ValueConversion(typeof(IDictionary), typeof(object[]))]
    public class DictonaryToValuesConverter : MarkupExtension, IValueConverter
    {
        static readonly DictonaryToValuesConverter _converter = new DictonaryToValuesConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var valueType = value.GetType();
            var isObjectDictonary = value is IDictionary;
            var isGenericDictonary = valueType
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                .SingleOrDefault()
                .NullToFalse();
            if (!(isObjectDictonary || isGenericDictonary))
            {
                throw new ArgumentUniformException(ArgumentError.DOWNCAST_NOT_POSSIBLE);
            }

            object[] values = null;
            if (isObjectDictonary)
            {
                values = ((IDictionary)value).Values.ToGenericEnumerable().ToArray();
            }
            else if (value is IDictionary<string, object> genericDictonary)
            {
                values = genericDictonary.Values.ToArray();
            }
            else
            {
                throw new Exception();
            }

            return values;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("IValueConverter.ConvertBack");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter;
        }
    }
}
