using System;
using System.Globalization;
using System.Windows.Data;

namespace InventorySystem.Services
{
    class CheckAddEditProductLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Add Product";
            else
                return "Modify Product";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}