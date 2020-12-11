using System;
using System.Collections.Generic;
using OmniXaml;
using Tizen.NUI;
using Tizen.NUI.Binding;

namespace Nui.Vsix.Xaml
{
    public class Converter : IStringSourceValueConverter
    {
        private readonly Dictionary<Type, TypeConverter> converters;

        public Converter()
        {
            converters = new Dictionary<Type, TypeConverter>();
            converters.Add(typeof(BindableProperty), new BindablePropertyConverter());
            converters.Add(typeof(Binding), new BindingTypeConverter());
            converters.Add(typeof(Color), new ColorTypeConverter());
            converters.Add(typeof(Size2D), new Size2DTypeConverter());
            converters.Add(typeof(Size), new SizeTypeConverter());
        }

        public (bool, object) Convert(string value, Type desiredTargetType, ConvertContext context = null)
        {
            TypeConverter conv;
            if (!converters.TryGetValue(desiredTargetType, out conv))
            {
                return (false, null);
            }

            if (!conv.CanConvertFrom(typeof(string)))
            {
                return (false, null);
            }

            return (true, conv.ConvertFromInvariantString(value));
        }
    }
}