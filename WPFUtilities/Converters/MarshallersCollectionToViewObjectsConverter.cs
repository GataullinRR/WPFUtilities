using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    [ValueConversion(typeof(ObservableCollection<IViewValueProvider>), typeof(ObservableCollection<object>))]
    public class MarshallersCollectionToViewObjectsConverter : MarkupExtension, IValueConverter
    {
        static readonly MarshallersCollectionToViewObjectsConverter _converter = new MarshallersCollectionToViewObjectsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<IViewValueProvider> && value is INotifyCollectionChanged))
            {
                throw new ArgumentUniformException(ArgumentError.DOWNCAST_NOT_POSSIBLE);
            }

            var source = (IEnumerable<IViewValueProvider>)value;
            var sourceChanged = (INotifyCollectionChanged)value;
            var casted = new TransformingObservableCollection<IViewValueProvider, object>
                (source, sourceChanged, ivvp => ivvp.Value);

            return casted;
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
