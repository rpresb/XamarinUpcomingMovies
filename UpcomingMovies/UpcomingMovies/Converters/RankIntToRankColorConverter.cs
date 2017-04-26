using System;
using System.Globalization;
using Xamarin.Forms;

namespace UpcomingMovies.Converters
{
    class RankIntToRankColorConverter : IValueConverter
    {
        // Color names provided by http://www.color-blindness.com/color-name-hue/
        private const string FOREST_GREEN = "#148017";
        private const string FRUIT_SALAD = "#4eb751";
        private const string MANTIS = "#84b74e";
        private const string OLIVE_GREEN = "#b7b14e";
        private const string HUSK = "#b7894e";
        private const string CHESTNUT = "#b74e4e";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vote = (int)value;
            if (vote > 90)
            {
                return Color.FromHex(FOREST_GREEN);
            }
            else if (vote > 70)
            {
                return Color.FromHex(FRUIT_SALAD);
            }
            else if (vote > 60)
            {
                return Color.FromHex(MANTIS);
            }
            else if (vote > 50)
            {
                return Color.FromHex(OLIVE_GREEN);
            }
            else if (vote > 30)
            {
                return Color.FromHex(HUSK);
            }
            else
            { // really bad movie
                return Color.FromHex(CHESTNUT);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
