﻿namespace Yuniversal.Context
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using Glass.Core;
    using OmniXaml;

    public static class Registrator
    {
        public static ISourceValueConverter GetSourceValueConverter()
        {
            var sourceValueConverter = new SourceValueConverter();
            sourceValueConverter.Add(typeof(Thickness), value => ConvertToThickness((string) value.Value));
            sourceValueConverter.Add(typeof(Brush), value => ConvertToSolidBrush((string)value.Value));
            sourceValueConverter.Add(typeof(GridLength), value => GetGridLength((string)value.Value));
            sourceValueConverter.Add(typeof(ImageSource), value => ConvertToBitmap((string)value.Value));
            sourceValueConverter.Add(typeof(bool), value => bool.Parse((string)value.Value));
            sourceValueConverter.Add(typeof(FontFamily), value => new FontFamily((string)value.Value));
            return sourceValueConverter;
        }

        private static object ConvertToBitmap(string value)
        {
            var convertToBitmap = new BitmapImage(new Uri($"ms-appx:///{value}"));
            return convertToBitmap;
        }

        private static GridLength GetGridLength(string s)
        {
            if (s.Contains("Auto"))
                return new GridLength(0, GridUnitType.Auto);

            if (s.EndsWith("*"))
            {
                var value = int.Parse(s.Take(s.Length - 1).AsString());
                return new GridLength(value, GridUnitType.Star);
            }

            return new GridLength(int.Parse(s), GridUnitType.Pixel);
        }

        private static SolidColorBrush ConvertToSolidBrush(string value)
        {
            var values = value
                .Skip(1)
                .AsString()
                .Split(2)
                .Select(i => byte.Parse(i, NumberStyles.HexNumber))
                .Take(4)
                .DefaultIfEmpty((byte)0);
            
            var method = typeof(Color).GetRuntimeMethods().First(info => info.Name == "FromArgb");
            var parameters = values.Cast<object>().ToArray();
            var color = (Color)method.Invoke(null, parameters);
            return new SolidColorBrush(color);
        }

        private static Thickness ConvertToThickness(string value)
        {
            var values = value.Split(',').Select(double.Parse).Take(4).DefaultIfEmpty(double.NaN);

            var ctor = typeof(Thickness).GetConstructors().First(info => info.GetParameters().Length == 4);
            var parameters = values.Cast<object>().ToArray();
            return (Thickness) ctor.Invoke(parameters);
        }        
    }
}