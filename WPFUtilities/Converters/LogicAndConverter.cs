using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class LogicAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.All(v => (bool)v);
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

    [ValueConversion(typeof(Visibility), typeof(Visibility))]
    public class VisibilityLogicAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.All(v => (Visibility)v == Visibility.Visible) 
                ? Visibility.Visible 
                : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
