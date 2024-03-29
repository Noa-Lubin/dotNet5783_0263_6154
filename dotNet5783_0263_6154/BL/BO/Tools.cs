﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                str += "\n" + item.Name + ": ";
                if (item.GetValue(t, null) is IEnumerable<object>)//Case for IEnumerable property
                {
                    IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
                    string s = String.Join("  " , list);
                    str += s;
                }
                else
                    str += item.GetValue(t, null);
            }
            return str += "\n";
        }

        //public static string ToStringProperty<T>(this T t)
        //{
        //    string str = "";
        //    foreach (PropertyInfo item in t.GetType().GetProperties())
        //        str += "\n" + item.Name +
        //        ": " + item.GetValue(t, null);

        //    return str;
        //}

        //שולההה
        public static string ToStringPropertyyyyy<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                str += "\n" + item.Name + ": ";
                if (item.GetValue(t, null) is IEnumerable<object>)//Case for IEnumerable property
                {
                    //foreach (object obj in item.GetValue(t, null))
                    //{
                    //    str += obj.ToString();
                    //}
                    //IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
                    //string s = String.Join("  ", list);
                    //str += s;
                }
                else
                    str += item.GetValue(t, null);
            }
            return str += "\n";
        }
    }
}
