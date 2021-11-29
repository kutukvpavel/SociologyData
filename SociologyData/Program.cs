using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using SociologyData.Views;
using SociologyData.Data;
using System;
using System.IO;
using System.Linq;
using System.Data;

namespace SociologyData
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public static void AppMain(Application app, string[] args)
        {
            if (args.Length == 0) args = new string[] { Path.Combine(Environment.CurrentDirectory, "data") };
            var folderPath = args[0].Trim('"');
            var repo = new Repository();
            JsonProvider.ReadFolder(folderPath, repo);
            if (args.Length > 1)
            {
                //JSON to XLS
                ExcelProvider.WriteFolder(folderPath, repo);
            }
            else
            {
                ExcelProvider.ReadFolder(folderPath, repo);
                foreach (DataTable item in repo.Last().Value.Tables)
                {
                    foreach (DataRow row in item.Rows)
                    {
                        for (int i = 1; i < item.Columns.Count; i++)
                        {
                            if (float.TryParse(row[i].ToString(), out float f))
                            {
                                row[i] = (f * (1 + 1 / 6)).ToString();
                            }
                        }
                    }
                }
                var w = new MainWindow() { ViewModel = new ViewModels.MainWindowViewModel() { Repo = repo } };
                app.Run(w);
            }
        }
    }
}
