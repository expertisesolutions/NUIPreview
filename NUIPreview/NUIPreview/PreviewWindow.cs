using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Editor;
using System.Diagnostics;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.Applications.Messages;
using Nui.Vsix.Xaml;

namespace NUIPreview
{
    class PreviewWindow : NUIApplication
    {
        private View currentView = null;
        private Timer refresher = new Timer(300);
        private Loader loader;
        private string previousCode = "";

        public PreviewWindow()
        {
            var assem = Assembly.GetExecutingAssembly();
            var assemList = new List<Assembly>();
            if (assem != null)
            {
                assemList.Add(assem);
            }
            var assemblies = from m in assem.GetModules(false) select m.Assembly;
            assemList.Concat<Assembly>(assemblies);
            loader = new Loader(assemList);
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            Window.Instance.KeyEvent += (sender, e) =>
            {
                if (e.Key.KeyPressedName == "Escape")
                {
                    Exit();
                }
            };

            refresher.Tick += (sender, args) =>
            {
                IWpfTextView textView;

                try
                {
                    textView = Global.GetTextView();
                }
                catch (Exception e)
                {
                    return true;
                }

                var language = textView.TextBuffer.ContentType.TypeName;

                if (Loader.SupportedLanguages.Contains(language))
                {
                    var snapshot = textView.TextSnapshot;

                    if (snapshot != snapshot.TextBuffer.CurrentSnapshot)
                    {
                        return true;
                    }

                    if (!textView.Selection.IsEmpty)
                    {
                        return true;
                    }

                    var code = snapshot.GetText();

                    if (code != previousCode)
                    {
                        ShowPreview(code);
                        previousCode = code;
                    }
                }
                return true;
            };

            refresher.Start();
            Window.Instance.BackgroundColor = Color.White;
        }

        protected override void OnTerminate()
        {
            refresher.Stop();
            base.OnTerminate();
        }

        private void ShowPreview(string xaml)
        {
            View view;
            try
            {
                view = (View) loader.Load(xaml);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }

            if (currentView)
            {
                Window.Instance.GetDefaultLayer().Remove(currentView);
            }

            //Window.Instance.WindowSize = view.Size;
            Window.Instance.GetDefaultLayer().Add(view);
            Window.Instance.Show();
            currentView = view;
        }
    }
}
