using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Converters
{
    ///// <summary>
    ///// Converts not only between <typeparamref name="TFrom"/> and <typeparamref name="TTo"/>, but between 
    ///// <see cref="IEnumerable{TFrom}"/> and <see cref="IEnumerable{TTo}"/>
    ///// </summary>
    ///// <typeparam name="TFrom"></typeparam>
    ///// <typeparam name="TTo"></typeparam>
    //public abstract class SmartConverterTemplate<TFrom, TTo> : MarkupExtension, IValueConverter
    //{
    //    readonly Dictionary<Type, SmartConverterTemplate<TFrom, TTo>> _instances 
    //        = new Dictionary<Type, SmartConverterTemplate<TFrom, TTo>>();
    //    public override sealed object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        var type = GetType();
    //        if (_instances.Keys.NotContains(type))
    //        {
    //            _instances[type] = (SmartConverterTemplate<TFrom, TTo>)Activator.CreateInstance(type);
    //        }

    //        return _instances[type];
    //    }

    //    public SmartConverterTemplate() { }

    //    public virtual TTo Convert(TFrom value)
    //    {
    //        throw new NotSupportedException("ConverterTemplate.Convert");
    //    }
    //    public virtual TFrom ConvertBack(TTo value)
    //    {
    //        throw new NotSupportedException("ConverterTemplate.ConvertBack");
    //    }

    //    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var acceptsNulls = !typeof(TFrom).IsValueType || 
    //            Nullable.GetUnderlyingType(typeof(TFrom)) != null;
    //        if (value == null || acceptsNulls)
    //        {
    //            return Convert((TFrom)value);
    //        }
    //        else if (value == null)
    //        {
    //            throw new ArgumentException("Value is null, but input type is ValueType");
    //        }
    //        else if (value is TFrom v)
    //        {
    //            return Convert(v);
    //        }
    //        else if (value is IEnumerable<TFrom> sequence)
    //        {
    //            return sequence.Select(Convert);
    //        }
    //        else
    //        {
    //            throw new NotImplementedException("Expected IEnumerable<TFrom> or TFrom");
    //        }
    //    }

    //    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is TTo v)
    //        {
    //            return ConvertBack(v);
    //        }
    //        else if (value is IEnumerable<TTo> sequence)
    //        {
    //            return sequence.Select(ConvertBack);
    //        }
    //        else
    //        {
    //            throw new NotImplementedException("Expected IEnumerable<TTo> or TTo");
    //        }
    //    }
    //}

    /// <summary>
    /// Converts not only between <typeparamref name="TFrom"/> and <typeparamref name="TTo"/>, but between 
    /// <see cref="IEnumerable{TFrom}"/> and <see cref="IEnumerable{TTo}"/>
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    public abstract class SmartConverterTemplate<TFrom, TTo> : SmartConverterTemplate<TFrom, TTo, object>
    {
        public SmartConverterTemplate() { }

        public virtual TTo Convert(TFrom value)
        {
            throw new NotSupportedException("ConverterTemplate.Convert");
        }
        public virtual TFrom ConvertBack(TTo value)
        {
            throw new NotSupportedException("ConverterTemplate.ConvertBack");
        }
        public virtual IEnumerable<TTo> Convert(IEnumerable<TFrom> value)
        {
            return value.Select(v => Convert(v));
        }
        public virtual IEnumerable<TFrom> ConvertBack(IEnumerable<TTo> value)
        {
            return value.Select(v => ConvertBack(v));
        }

        public override sealed IEnumerable<TTo> Convert(IEnumerable<TFrom> value, object parameter)
        {
            return Convert(value);
        }
        public sealed override IEnumerable<TFrom> ConvertBack(IEnumerable<TTo> value, object parameter)
        {
            return ConvertBack(value);
        }
        public sealed override TTo Convert(TFrom value, object parameter)
        {
            return Convert(value);
        }
        public sealed override TFrom ConvertBack(TTo value, object parameter)
        {
            return ConvertBack(value);
        }
    }

    public abstract class SmartConverterTemplate<TFrom, TTo, TParameter> : MarkupExtension, IValueConverter
    {
        readonly Dictionary<Type, SmartConverterTemplate<TFrom, TTo, TParameter>> _instances
            = new Dictionary<Type, SmartConverterTemplate<TFrom, TTo, TParameter>>();
        public override sealed object ProvideValue(IServiceProvider serviceProvider)
        {
            var type = GetType();
            if (_instances.Keys.NotContains(type))
            {
                _instances[type] = (SmartConverterTemplate<TFrom, TTo, TParameter>)Activator.CreateInstance(type);
            }

            return _instances[type];
        }

        public SmartConverterTemplate() { }

        public virtual TTo Convert(TFrom value, TParameter parameter)
        {
            throw new NotSupportedException("ConverterTemplate.Convert");
        }
        public virtual TFrom ConvertBack(TTo value, TParameter parameter)
        {
            throw new NotSupportedException("ConverterTemplate.ConvertBack");
        }
        public virtual IEnumerable<TTo> Convert(IEnumerable<TFrom> value, TParameter parameter)
        {
            return value.Select(v => Convert(v, parameter));
        }
        public virtual IEnumerable<TFrom> ConvertBack(IEnumerable<TTo> value, TParameter parameter)
        {
            return value.Select(v => ConvertBack(v, parameter));
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = (TParameter)parameter;

            var acceptsNulls = !typeof(TFrom).IsValueType ||
                Nullable.GetUnderlyingType(typeof(TFrom)) != null;
            if (value == null || acceptsNulls)
            {
                return Convert((TFrom)value, p);
            }
            else if (value == null)
            {
                throw new ArgumentException("Value is null, but input type is ValueType");
            }
            else if (value is TFrom v)
            {
                return Convert(v, p);
            }
            else if (value is IEnumerable<TFrom> sequence)
            {
                return Convert(sequence, p);
            }
            else
            {
                throw new NotImplementedException("Expected IEnumerable<TFrom> or TFrom");
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = (TParameter)parameter;

            var acceptsNulls = !typeof(TTo).IsValueType ||
                Nullable.GetUnderlyingType(typeof(TTo)) != null;
            if (value == null || acceptsNulls)
            {
                return ConvertBack((TTo)value, p);
            }
            else if (value == null)
            {
                throw new ArgumentException("Value is null, but input type is ValueType");
            }
            else if (value is TTo v)
            {
                return ConvertBack(v, p);
            }
            else if (value is IEnumerable<TTo> sequence)
            {
                return ConvertBack(sequence, p);
            }
            else
            {
                throw new NotImplementedException("Expected IEnumerable<TTo> or TTo");
            }
        }
    }

    public abstract class SmartMultiConverterTemplate<TFrom, TTo, TParameter> : MarkupExtension, IMultiValueConverter
    {
        readonly Dictionary<Type, SmartMultiConverterTemplate<TFrom, TTo, TParameter>> _instances
            = new Dictionary<Type, SmartMultiConverterTemplate<TFrom, TTo, TParameter>>();
        public override sealed object ProvideValue(IServiceProvider serviceProvider)
        {
            var type = GetType();
            if (_instances.Keys.NotContains(type))
            {
                _instances[type] = (SmartMultiConverterTemplate<TFrom, TTo, TParameter>)Activator.CreateInstance(type);
            }

            return _instances[type];
        }

        public SmartMultiConverterTemplate() { }

        public virtual TTo Convert(TFrom[] value, TParameter parameter)
        {
            throw new NotSupportedException("SmartMultiConverterTemplate.Convert");
        }
        public virtual TFrom[] ConvertBack(TTo value, TParameter parameter)
        {
            throw new NotSupportedException("SmartMultiConverterTemplate.ConvertBack");
        }

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var p = (TParameter)parameter;
            if (values == null)
            {
                return Convert(new TFrom[0], p);
            }
            else
            {
                if (values.Any(v => v == DependencyProperty.UnsetValue))
                {
                    return DependencyProperty.UnsetValue;
                }
                else
                {
                    return Convert(values.Cast<TFrom>().ToArray(), p);
                }
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("SmartMultiConverterTemplate.ConvertBack");
        }
    }

    public abstract class ConverterTemplate<TFrom, TTo> : IValueConverter
    {
        public virtual TTo Convert(TFrom value)
        {
            throw new NotSupportedException("ConverterTemplate.Convert");
        }
        public virtual TFrom ConvertBack(TTo value)
        {
            throw new NotSupportedException("ConverterTemplate.ConvertBack");
        }
        public virtual IEnumerable<TTo> Convert(IEnumerable<TFrom> value)
        {
            return value.Select(v => Convert(v));
        }
        public virtual IEnumerable<TFrom> ConvertBack(IEnumerable<TTo> value)
        {
            return value.Select(v => ConvertBack(v));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TTo)value);
        }
    }

    public abstract class ParameterizedConverterTemplate<TFrom, TTo, TParameter> : IValueConverter
    {
        public virtual TTo Convert(TFrom value, TParameter parameter)
        {
            throw new NotSupportedException("ParameterizedConverterTemplate.Convert");
        }
        public virtual TFrom ConvertBack(TTo value, TParameter parameter)
        {
            throw new NotSupportedException("ParameterizedConverterTemplate.ConvertBack");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom)value, (TParameter)parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TTo)value, (TParameter)parameter);
        }
    }
}
