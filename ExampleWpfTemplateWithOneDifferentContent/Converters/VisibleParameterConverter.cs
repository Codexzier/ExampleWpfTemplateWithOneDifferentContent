using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ExampleWpfTemplateWithOneDifferentContent.Converters
{
    public class VisibleParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return null;
            }


            if (!(value is DataTemplate content))
            {
                return value;
            }

            if (!(content.LoadContent() is Grid grid))
            {
                return value;
            }

            foreach (var item in grid.Children)
            {
                if (!(item is FrameworkElement path))
                {
                    continue;
                }

                if (path.Uid.Equals(parameter))
                {
                    grid.Children.Remove(path);
                    path.Visibility = Visibility.Visible;
                    return path;
                }
                else
                {
                    path.Visibility = Visibility.Collapsed;
                }
            }

            return grid;
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in this.FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
