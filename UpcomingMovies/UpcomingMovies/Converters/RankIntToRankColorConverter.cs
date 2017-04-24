using System;
using System.Globalization;
using Xamarin.Forms;

namespace UpcomingMovies.Converters
{
    class RankIntToRankColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vote = (int)value;
            if (vote > 90)
            {
                return Color.FromHex("#148017");
            }
            else if (vote > 70)
            {
                return Color.FromHex("4eb751");
            }
            else if (vote > 60)
            {
                return Color.FromHex("#84b74e");
            }
            else if (vote > 50)
            {
                return Color.FromHex("#b7b14e");
            }
            else if (vote > 30)
            {
                return Color.FromHex("#b7894e");
            }
            else
            { // really bad movie
                return Color.FromHex("#b74e4e");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
