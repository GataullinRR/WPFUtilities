using System.Windows;

namespace WPFUtilities.Converters
{
    public class NotNullToCollapsedConverter : SmartConverterTemplate<object, Visibility>
    {
        public override Visibility Convert(object value)
        {
            return value == null
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
