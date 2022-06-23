using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1
{
    class SearchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ?
                new SolidColorBrush(Colors.Orange)
                : new SolidColorBrush(Colors.White);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
