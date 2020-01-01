using System.Windows;

namespace WPFUtilities.Converters
{
    public class NullToCollapsedConverter : SmartConverterTemplate<object, Visibility>
    {
        public override Visibility Convert(object value)
        {
            return value == null 
                ? Visibility.Collapsed 
                : Visibility.Visible;
        }
    }
}
