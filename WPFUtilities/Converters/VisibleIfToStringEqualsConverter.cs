using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUtilities.Converters
{
    public class VisibleIfToStringEqualsConverter : SmartConverterTemplate<object, Visibility, string>
    {
        public override Visibility Convert(object value, string parameter)
        {
            return value?.ToString() == parameter ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
