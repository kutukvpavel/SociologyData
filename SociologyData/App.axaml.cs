using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SociologyData.ViewModels;
using SociologyData.Views;
using System.Text;

namespace SociologyData
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
