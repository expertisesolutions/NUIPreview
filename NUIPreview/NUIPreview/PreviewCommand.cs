using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
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
            SetupEnvironment();
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
        }

        private string GetPackagePath() => Path.GetDirectoryName(Global.GetAssemblyLocalPathFrom(typeof(NUIPreviewPackage)));

        private void SetupEnvironment()
        {
            var vsixPath = GetPackagePath();
            var toolkit = Path.Combine(vsixPath, "toolkit");
            var res = Path.Combine(vsixPath, "com.samsung.dali-demo", "res/");

            Environment.SetEnvironmentVariable("FONTCONFIG_FILE", Path.Combine(vsixPath, "fonts.conf"));
            Environment.SetEnvironmentVariable("DALI_APPLICATION_PACKAGE", res);
            Environment.SetEnvironmentVariable("DALI_IMAGE_DIR", Path.Combine(toolkit, "images/"));
            Environment.SetEnvironmentVariable("DALI_SOUND_DIR", Path.Combine(toolkit, "sounds/"));
            Environment.SetEnvironmentVariable("DALI_STYLE_DIR", Path.Combine(toolkit, "styles/"));
            Environment.SetEnvironmentVariable("DALI_STYLE_IMAGE_DIR", Path.Combine(toolkit, "styles", "images/"));
            Environment.SetEnvironmentVariable("DALI_WINDOW_HEIGHT", "800");
            Environment.SetEnvironmentVariable("DALI_WINDOW_WIDTH", "480");
        }

        private void PreviewTask()
        {
            var previewWindow = new PreviewWindow();
            string[] args = { "Visual Studio" };
            previewWindow.Run(args);
        }
    }
}
