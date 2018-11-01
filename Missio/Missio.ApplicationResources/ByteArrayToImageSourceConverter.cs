using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Missio.ApplicationResources
{
    public class ByteArrayToImageSourceConverter : IValueConverter 
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var array = (byte[]) value;
            var imageSource = ImageSource.FromStream(() => new MemoryStream(array));
            return imageSource;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}