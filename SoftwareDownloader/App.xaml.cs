using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using SoftwareDownloader.ViewModels;
using SoftwareDownloader.Views;

namespace SoftwareDownloader
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<HomeView>();
            containerRegistry.Register<SettingsView>();
            containerRegistry.Register<SettingsViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<HomeView>();
        }
    }
}
