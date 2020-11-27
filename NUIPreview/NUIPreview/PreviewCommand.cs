using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using System.Threading;
using Tizen.Applications.Messages;
using Tizen.Applications;

namespace NUIPreview
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class PreviewCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b073a3c3-0db5-4a65-af18-0ef61ba0bd26");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        private MessagePort previewPort = new MessagePort(PreviewWindow.PortName, true);
        private Thread previewTask = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private PreviewCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static PreviewCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in PreviewCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new PreviewCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Preview();
        }

        private void Preview()
        {
            var textView = Global.GetTextView();
            var language = textView.TextBuffer.ContentType.TypeName;

            if (previewTask != null && !previewTask.IsAlive)
            {
                previewTask.Join();
                previewTask = null;
            }

            if (previewTask == null)
            {
                previewTask = new Thread(this.PreviewTask);
                previewTask.Start();
            }

            if (language != null && language == "Xaml")
            {
                var snapshot = textView.TextSnapshot;

                if (snapshot != snapshot.TextBuffer.CurrentSnapshot)
                {
                    return;
                }

                if (!textView.Selection.IsEmpty)
                {
                    return;
                }

                var code = snapshot.GetText();

                Bundle args = new Bundle();
                args.AddItem("message", "preview");
                args.AddItem("content", code);
                previewPort.Send(args, PreviewWindow.AppId, PreviewWindow.PortName);
            }
        }

        private void PreviewTask()
        {
            var previewWindow = new PreviewWindow();
            string[] args = { "Visual Studio" };
            previewWindow.Run(args);
        }
    }
}
