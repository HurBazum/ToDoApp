using System.Globalization;
using System.Windows.Data;

namespace ToDoApp.Infrastructure.Converters
{
    class GoalStateConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null) return null;

            bool result = (((bool)value).ToString() == "True");

            return (result == true) ? "Выполнена" : "Выполнить";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
