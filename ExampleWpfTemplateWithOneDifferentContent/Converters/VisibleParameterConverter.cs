using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

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

 
                if (value is DataTemplate content)
                {
                    var dd = content.LoadContent();

                    if (dd is Grid grid)
                    {

                        foreach (var item in grid.Children)
                        {
                            if (item is FrameworkElement path)
                            {
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
                        }

                    }
                }
                //var res = this.FindVisualChildren<Path>(dd);

                //if (content.DataTemplateKey is Grid grid)
                //{
                //    if (int.TryParse(parameter.ToString(), out int id))
                //    {
                //        foreach (var item in grid.Children)
                //        {
                //            if(item is FrameworkElement path)
                //            {
                //                if (path.Uid.Equals(id))
                //                {
                //                    path.Visibility = Visibility.Visible;
                //                }
                //                else
                //                {
                //                    path.Visibility = Visibility.Collapsed;
                //                }
                //            }
                //        }
                //    }
                //}
            
            return value;
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

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
