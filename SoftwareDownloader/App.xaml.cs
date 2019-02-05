using Prism.Ioc;
using Prism.Unity;
using SoftwareDownloader.Serializers;
using SoftwareDownloader.Views;
using System.Windows;
using SoftwareDownloader.ViewModels;

namespace SoftwareDownloader
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<HomeView>();
            containerRegistry.Register<SettingsView>();
            containerRegistry.Register<SettingsViewModel>();
            containerRegistry.Register<HomeViewModel>();
            containerRegistry.Register<IXmlSerializer, XmlSerializer>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<HomeView>();
        }
    }
}
