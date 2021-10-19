using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace toDo.Converters
{
    class ToDoDatumfsformatConverter : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            string datumsFormat = "dddd, dd.MMMM.yyyy";
            // value = erstellZeitpunkt (DateTime)
            DateTime date = (DateTime)value;
            return date.ToString(datumsFormat, CultureInfo.CreateSpecificCulture("de-DE"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
