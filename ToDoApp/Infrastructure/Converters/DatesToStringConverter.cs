using System.Globalization;
using System.Windows.Data;

namespace ToDoApp.Infrastructure.Converters
{
    class DatesToStringConverter : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values is null) return null;

            var dateSince = ((DateTime)values[0]).ToString("dd.MM.yyyy");
            var dateTo = ((DateTime)values[1]).ToString("dd.MM.yyyy");

            return $"С {dateSince} до {dateTo}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}