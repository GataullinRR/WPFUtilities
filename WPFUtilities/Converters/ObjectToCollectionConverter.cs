using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using System.Collections.ObjectModel;

namespace WPFUtilities.Converters
{
    [ValueConversion(typeof(object), typeof(ObservableCollection<object>))]
    public class ObjectToCollectionConverter : MarkupExtension, IValueConverter
    {
        static readonly ObjectToCollectionConverter _converter = new ObjectToCollectionConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is object))
            {
                throw new ArgumentUniformException(ArgumentError.DOWNCAST_NOT_POSSIBLE);
            }

            return new ObservableCollection<object>() { value };
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
