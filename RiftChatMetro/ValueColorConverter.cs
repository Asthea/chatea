using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RiftChatMetro
{
    public class ValueColorConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double total = 0;
            return total.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
