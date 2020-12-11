using System;
using System.Collections.Generic;
using System.Reflection;
using OmniXaml.Services;
using OmniXaml;
using Tizen.NUI.Binding;

namespace Nui.Vsix.Xaml
{
    class Loader : BasicXamlLoader
    {
        public Loader(IList<Assembly> assemblies) : base(assemblies)
        { }

        public override IStringSourceValueConverter StringSourceValueConverter => new SuperSmartSourceValueConverter(
            new IStringSourceValueConverter[]
            {
                new DirectCompatibilitySourceValueConverter(),
                new AttributeBasedStringValueConverter(Assemblies),
                new ComponentModelTypeConverterBasedSourceValueConverter(),
                new Converter(),
            });

        protected override IXmlTypeResolver XmlTypeResolver => new XmlTypeXmlTypeResolver(new TypeLocator(Assemblies));
    }
}