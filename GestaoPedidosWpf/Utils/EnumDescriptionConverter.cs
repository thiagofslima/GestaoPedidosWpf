using System;
using System.Globalization;
using System.Windows.Data;

namespace GestaoPedidosWpf.Utils
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum e)
                return e.GetDescription();
            return value?.ToString() ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
