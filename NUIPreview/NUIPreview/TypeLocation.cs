using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using OmniXaml.Services;
using OmniXaml.TypeLocation;

namespace Nui.Vsix.Xaml
{
    class TypeLocator : ITypeDirectory
    {
        private readonly AttributeBasedTypeDirectory inner;
        private readonly ITypeDirectory nuiTypeDirectory;
        public TypeLocator(IList<Assembly> assemblies)
        {
            inner = new AttributeBasedTypeDirectory(assemblies);
            var nuiAssem = Assembly.Load("Tizen.NUI");
            var systemXamlAssem = Assembly.GetAssembly(typeof(System.Xaml.XamlReader));
            var ns = GetNamespacesNUI(new List<Assembly> { nuiAssem });
            ns.Concat<XamlNamespace>(GetNamespacesWPF(new List<Assembly> { systemXamlAssem }));
            nuiTypeDirectory = new TypeDirectory(ns);
        }

        private IEnumerable<XamlNamespace> GetNamespacesNUI(IEnumerable<Assembly> assemblies)
        {
            var routes = from a in assemblies
                         let attributes = a.GetCustomAttributes<Tizen.NUI.XmlnsDefinitionAttribute>()
                         from byNamespace in attributes.GroupBy(arg => arg.XmlNamespace)
                         let name = byNamespace.Key
                         let clrNamespaces = byNamespace.Select(arg => arg.ClrNamespace)
                         let configuredAssemblyWithNamespaces = Route.Assembly(a).WithNamespaces(clrNamespaces.ToArray())
                         select new { Ns = name, configuredAssemblyWithNamespaces };

            var nss = from route in routes
                      group route by route.Ns
                      into g
                      let ns = g.Select(arg => arg.configuredAssemblyWithNamespaces).ToArray()
                      select XamlNamespace.Map(g.Key).With(ns);

            return nss;
        }
        private IEnumerable<XamlNamespace> GetNamespacesWPF(IEnumerable<Assembly> assemblies)
        {
            var routes = from a in assemblies
                         let attributes = a.GetCustomAttributes<System.Windows.Markup.XmlnsDefinitionAttribute>()
                         from byNamespace in attributes.GroupBy(arg => arg.XmlNamespace)
                         let name = byNamespace.Key
                         let clrNamespaces = byNamespace.Select(arg => arg.ClrNamespace)
                         let configuredAssemblyWithNamespaces = Route.Assembly(a).WithNamespaces(clrNamespaces.ToArray())
                         select new { Ns = name, configuredAssemblyWithNamespaces };

            var nss = from route in routes
                      group route by route.Ns
                      into g
                      let ns = g.Select(arg => arg.configuredAssemblyWithNamespaces).ToArray()
                      select XamlNamespace.Map(g.Key).With(ns);

            return nss;
        }

        public Type GetTypeByFullAddress(Address address)
        {
            Type typ;

            try
            {
                typ = inner.GetTypeByFullAddress(address);

                if (typ == null)
                {
                    typ = nuiTypeDirectory.GetTypeByFullAddress(address);
                }
            }
            catch (TypeLocationException)
            {
                typ = nuiTypeDirectory.GetTypeByFullAddress(address);
            }

            return typ;
        }
    }
}