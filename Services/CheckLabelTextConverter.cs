using System;
using System.Globalization;
using System.Windows.Data;

namespace InventorySystem.Services
{
    // Checks to make sure that when the specific radio button is click, the label will
    // turn from Machine ID to Company Name
    class CheckLabelTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Company Name";
            else
                return "Machine ID";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
