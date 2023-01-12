using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // new NotImplementedException();
            int val = (int)value;
            return val == 0 ? "Add" : "Update";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string name = (string)value;

                //var requestImage = new Image()
                //{
                //    Height = 16,
                //    Width = 16,
                //    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                //};
                string imagename = (string)value;
                return new BitmapImage(new Uri($"/PicturesProduct/{imagename}.jpg", UriKind.Relative));
                //requestImage.Source= new BitmapImage(new Uri($"/PicturesProduct/{name}.jpg"));
                //return requestImage;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



