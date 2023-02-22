using BO;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace PL;

public class IntToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // new NotImplementedException();
        int val = (int)value;
        return val == 0 ? "הוספה" : "עדכון";
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

            return $"pack://application:,,,/PL;component/ImagesProducts/{name}";

      }
        else return null;
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NotBooleanToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservableCollection<BO.OrderItem?> items =(ObservableCollection<BO.OrderItem?>)value;

        //if(items!=null && items.Count!=0)
        if(items.Count != 0)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BooleanToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservableCollection<BO.OrderItem?> items = (ObservableCollection<BO.OrderItem?>)value;

        //if(items!=null && items.Count!=0)
        if (items.Count == 0)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
