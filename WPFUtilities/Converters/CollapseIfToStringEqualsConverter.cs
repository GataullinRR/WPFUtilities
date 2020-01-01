using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUtilities.Converters
{
    public class VisibilityInvertingConverter : SmartConverterTemplate<Visibility, Visibility>
    {
        public override Visibility Convert(Visibility value)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return Visibility.Hidden;
                case Visibility.Hidden:
                    return Visibility.Visible;
                case Visibility.Collapsed:
                    return Visibility.Visible;

                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class CollapseIfToStringEqualsConverter : SmartConverterTemplate<object, Visibility, string>
    {
        public override Visibility Convert(object value, string parameter)
        {
            return value?.ToString() == parameter ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
