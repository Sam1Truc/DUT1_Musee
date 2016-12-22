using Core;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Musée
{
    public class StringToUriConvertisseur : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Media media = value as Media;
            if (media == null) return null;
            string imagesrc = media.Chemin;
            if (string.IsNullOrWhiteSpace(imagesrc)) return null;

            Uri uri = new Uri(string.Format(imagesrc), UriKind.RelativeOrAbsolute);

            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
