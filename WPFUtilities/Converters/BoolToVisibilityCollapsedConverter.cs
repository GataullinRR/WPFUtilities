using System;
using System.Collections.Generic;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;
using System.Windows.Markup;

namespace WPFUtilities.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityCollapsedConverter : MarkupExtension, IValueConverter
    {
        static readonly BoolToVisibilityCollapsedConverter _converter = new BoolToVisibilityCollapsedConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new ArgumentUniformException(ArgumentError.DOWNCAST_NOT_POSSIBLE);
            }

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
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
