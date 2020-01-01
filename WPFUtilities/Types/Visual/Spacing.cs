using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;
using WPFUtilities.Types;

namespace WPFUtilities.Types
{
    public class Spacing
    {
        public static readonly DependencyProperty VerticalProperty =
                DependencyProperty.RegisterAttached("Vertical", typeof(double), 
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                new UIPropertyMetadata(0d, VerticalChangedCallback));
        public static readonly DependencyProperty HorizontalProperty =
                DependencyProperty.RegisterAttached("Horizontal", typeof(double), 
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                new UIPropertyMetadata(0d, HorizontalChangedCallback));

        private static void HorizontalChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            var space = (double)e.NewValue;
            var obj = (DependencyObject)sender;

            MarginSetter.SetMargin(obj, new Thickness(0, 0, space, 0));
            MarginSetter.SetLastItemMargin(obj, new Thickness(0));
        }
        public static double GetHorizontal(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalProperty);
        }
        public static void SetHorizontal(DependencyObject obj, double space)
        {
            obj.SetValue(HorizontalProperty, space);
        }

        private static void VerticalChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            var space = (double)e.NewValue;
            var obj = (DependencyObject)sender;
            MarginSetter.SetMargin(obj, new Thickness(0, 0, 0, space));
            MarginSetter.SetLastItemMargin(obj, new Thickness(0));
        }
        public static double GetVertical(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalProperty);
        }
        public static void SetVertical(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalProperty, value);
        }
    }
}
