using System.Collections;
using System.Windows;

namespace WPFUtilities.Converters
{
    public class CollapseIfEmptyConverter : SmartConverterTemplate<IEnumerable, Visibility>
    {
        public override Visibility Convert(IEnumerable value)
        {
            return value.GetEnumerator().MoveNext() 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }
    }
}
