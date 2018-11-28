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
            byte[] array;
            switch (value)
            {
                case byte[] bytes:
                    array = bytes;
                    break;
                case string bytesAsString:
                    array = System.Convert.FromBase64String(bytesAsString);
                    break;
                default:
                    throw new ArgumentException(nameof(value));
            }
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