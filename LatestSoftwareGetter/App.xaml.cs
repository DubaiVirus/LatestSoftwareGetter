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
            containerRegistry.Register<HomeView>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<HomeView>();
        }
    }
}
