using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Converters
{
    [ValueConversion(typeof(bool?), typeof(SolidColorBrush))]
    public abstract class BoolToColorConverter : IValueConverter 
    {
        SolidColorBrush _trueColor, _falseColor;

        protected void Set(SolidColorBrush trueColor, SolidColorBrush falseColor)
        {
            _trueColor = trueColor;
            _falseColor = falseColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool?)value ?? false;
            return v ? _trueColor : _falseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("IValueConverter.ConvertBack");
        }
    }
}
