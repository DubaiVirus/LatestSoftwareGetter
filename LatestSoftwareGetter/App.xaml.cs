using LatestSoftwareGetter.ViewModels;
using LatestSoftwareGetter.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace LatestSoftwareGetter
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
