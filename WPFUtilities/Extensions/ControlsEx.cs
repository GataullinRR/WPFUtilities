using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vectors;

namespace WPFUtilities.Extensions
{
    public static class ControlsEx
    {
        public static BitmapImage ToBitmapImage(this System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static V2 MeasureStringSize(this TextBlock textBlock, string text)
        {
            var formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution());

            return new V2(formattedText.Width, formattedText.Height);
        }

        public static V2 GetActualSize(this FrameworkElement frameworkElement)
        {
            return new V2(frameworkElement.ActualWidth, frameworkElement.ActualHeight);
        }

        public static IEnumerable<T> FindVisualChildrenByName<T>(this DependencyObject parent, string name)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child == null) // Why???
                {
                    continue;
                }
                else
                {
                    string controlName = child.GetValue(Control.NameProperty) as string;
                    if (controlName == name)
                    {
                        yield return child as T;
                    }
                    else
                    {
                        foreach (var c in FindVisualChildrenByName<T>(child, name))
                        {
                            yield return c as T;
                        }
                    }
                }
            }
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
