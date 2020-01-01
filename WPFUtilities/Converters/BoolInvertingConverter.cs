using System;
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
    [ValueConversion(typeof(bool?), typeof(bool?))]
    public class BoolInvertingConverter : MarkupExtension, IValueConverter
    {
        static readonly BoolInvertingConverter _converter = new BoolInvertingConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool?))
            {
                throw new ArgumentUniformException(ArgumentError.DOWNCAST_NOT_POSSIBLE);
            }
            return !(value as bool?);
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
