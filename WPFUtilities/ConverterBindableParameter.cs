using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFUtilities
{
    /// <summary>
    ///     <example>
    ///         <TextBox>
    ///             <TextBox.Text>
    ///                 <wpfAdditions:ConverterBindableParameter Binding="{Binding FirstName}"
    ///                     Converter="{StaticResource TestValueConverter}"
    ///                     ConverterParameterBinding="{Binding ConcatSign}" />
    ///             </TextBox.Text>
    ///         </TextBox>
    ///     </example>
    /// </summary>
    /// https://stackoverflow.com/questions/15309008/binding-converterparameter
    public class ConverterBindableParameter : MarkupExtension
    {
        #region Public Properties

        public Binding Binding { get; set; }

        public IValueConverter Converter { get; set; }

        public Binding ConverterParameterBinding { get; set; }

        #endregion

        #region Overridden Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(Binding);
            multiBinding.Bindings.Add(ConverterParameterBinding);
            var adapter = new MultiValueConverterAdapter
            {
                Converter = Converter
            };
            multiBinding.Converter = adapter;
            return multiBinding.ProvideValue(serviceProvider);
        }

        #endregion
    }

    /// <summary>
    /// https://www.codeproject.com/Articles/459958/Bindable-Converter-Converter-Parameter-and-StringF
    /// </summary>
    [ContentProperty("Converter")]
    public class MultiValueConverterAdapter : IMultiValueConverter
    {
        public IValueConverter Converter { get; set; }

        private object lastParameter;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (Converter == null) return values[0]; // Required for VS design-time
            if (values.Length > 1) lastParameter = values[1];
            return Converter.Convert(values[0], targetType, lastParameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (Converter == null) return new object[] { value }; // Required for VS design-time

            return new object[] { Converter.ConvertBack(value, targetTypes[0], lastParameter, culture) };
        }
    }
}
