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
using System.Windows.Controls;

namespace WPFUtilities.Types
{
    public class MarginSetter
    {
        private static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Make sure this is put on a panel
            var panel = sender as Panel;
            if (panel == null)
            {
                return;
            }

            // Avoid duplicate registrations
            panel.Loaded -= OnPanelLoaded;
            panel.Loaded += OnPanelLoaded;

            if (panel.IsLoaded)
            {
                OnPanelLoaded(panel, null);
            }
        }

        private static void OnPanelLoaded(object sender, RoutedEventArgs e)
        {
            var panel = (Panel)sender;

            // Go over the children and set margin for them:
            for (var i = 0; i < panel.Children.Count; i++)
            {
                UIElement child = panel.Children[i];
                var fe = child as FrameworkElement;
                if (fe == null)
                {
                    continue;
                }

                bool isLastItem = i == panel.Children.Count - 1;
                fe.Margin = isLastItem ? GetLastItemMargin(panel) : GetMargin(panel);
            }

            // Хотел сделать добавление отступов с 2х сторон
            //// Go over the children and set margin for them:
            //for (var i = 0; i < panel.Children.Count; i++)
            //{
            //    bool isLastItem = i == panel.Children.Count - 1;
            //    var currentChild = getElement(i);
            //    var nextChild = isLastItem ? null : getElement(i + 1);
            //    if (currentChild == null)
            //    {
            //        continue;
            //    }

            //    var 
            //    currentChild.Margin = isLastItem ? GetLastItemMargin(panel) : GetMargin(panel);


            //    FrameworkElement getElement(int index)
            //    {
            //        UIElement child = panel.Children[i];
            //        return child as FrameworkElement;
            //    }
            //}

            return;

            Thickness GetLastItemMargin(Panel obj)
            {
                return (Thickness)obj.GetValue(LastItemMarginProperty);
            }
            Thickness GetMargin(DependencyObject obj)
            {
                return (Thickness)obj.GetValue(MarginProperty);
            }
        }

        public static void SetLastItemMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(LastItemMarginProperty, value);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter),
                new UIPropertyMetadata(new Thickness(), MarginChangedCallback));

        public static readonly DependencyProperty LastItemMarginProperty =
            DependencyProperty.RegisterAttached("LastItemMargin", typeof(Thickness), typeof(MarginSetter),
                new UIPropertyMetadata(new Thickness(), MarginChangedCallback));
    }
}
