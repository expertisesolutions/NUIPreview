using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Linq;

namespace NUIPreview
{
    internal static class Global
    {
        static public Func<Type, object> GetService = Package.GetGlobalService;

        public static IWpfTextViewHost GetViewHost()
        {
            object holder;
            Guid guidViewHost = DefGuidList.guidIWpfTextViewHost;
            GetUserData().GetData(ref guidViewHost, out holder);
            return (IWpfTextViewHost)holder;
        }

        public static IWpfTextView GetTextView()
        {
            return Global.GetViewHost()?.TextView;
        }

        public static int IndexOfAny(this string text, string[] patterns)
        {
            var match = patterns.Select(x=>new { pos = text.IndexOf(x) })
                                .Where(x=>x.pos != -1)
                                .OrderBy(x=>x.pos)
                                .FirstOrDefault();
            if (match != null)
                return match.pos;
            else
                return -1;
        }

        public static IVsUserData GetUserData()
        {
            int mustHaveFocus = 1;//means true
            IVsTextView currentTextView;
            (GetService(typeof(SVsTextManager)) as IVsTextManager).GetActiveView(mustHaveFocus, null, out currentTextView);

            if (currentTextView is IVsUserData)
                return currentTextView as IVsUserData;
            else
                throw new ApplicationException("No text view is currently open");
        }

        public static DTE2 GetDTE2()
        {
            DTE dte = (DTE)GetService(typeof(DTE));
            DTE2 dte2 = dte as DTE2;

            if (dte2 == null)
            {
                return null;
            }

            return dte2;
        }

        public static string GetAssemblyLocalPathFrom(Type type)
        {
            string codebase = type.Assembly.CodeBase;
            var uri = new Uri(codebase, UriKind.Absolute);
            return uri.LocalPath;
        }
    }
}