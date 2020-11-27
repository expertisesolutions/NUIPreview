using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.Applications.Messages;
using Nui.Vsix.Xaml;

namespace NUIPreview
{
    class PreviewWindow : NUIApplication
    {
        public static readonly string PortName = "NUI Preview";
        public static readonly string AppId = "FakeApp";
        private MessagePort messages = new MessagePort(PortName, true);
        private static PreviewWindow Instance = null;

        private View currentView = null;
        private Loader loader = new Loader(new List<Assembly> { Assembly.GetEntryAssembly() });

        public PreviewWindow()
        {
            Instance = this;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            messages.Listen();
            messages.MessageReceived += MessageCallback;

            Window.Instance.KeyEvent += (sender, e) =>
            {
                if (e.Key.KeyPressedName == "Escape")
                {
                    CloseMe();
                }
            };

            Window.Instance.BackgroundColor = Color.White;
        }

        private static void MessageCallback(object sender, MessageReceivedEventArgs args)
        {
            switch (args.Message.GetItem<string>("message"))
            {
                case "close":
                    Instance.CloseMe();
                    break;
                case "preview":
                    Instance.ShowPreview(args.Message.GetItem<string>("content"));
                    break;
            }
        }

        private void CloseMe()
        {
            Instance = null;
            Exit();
        }

        private void ShowPreview(string xaml)
        {
            View view;
            try
            {
                view = (View) loader.Load(xaml);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return;
            }

            if (currentView)
            {
                Window.Instance.GetDefaultLayer().Remove(currentView);
            }

            Window.Instance.WindowSize = view.Size;
            Window.Instance.GetDefaultLayer().Add(view);
            currentView = view;
        }
    }
}
