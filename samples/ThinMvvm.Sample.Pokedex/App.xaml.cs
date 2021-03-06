using ThinMvvm.Applications;
using ThinMvvm.DependencyInjection;
using ThinMvvm.Windows;
using Windows.UI.Xaml.Controls;

namespace ThinMvvm.Sample.Pokedex
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void ConfigureServices( ServiceCollection services )
        {
            base.ConfigureServices( services );

            services.AddTransient<IPokedex, PokeapiPokedex>();
        }

        protected override void ConfigureViews( ViewBinder<Page> binder )
        {
            binder.Bind<MainViewModel, MainView>();
        }

        protected override WindowsAppConfig ConfigureApp( WindowsAppConfigBuilder builder )
        {
            return builder
                .UseSoftwareBackButton()
                .UseSplashScreen()
                .WithStartupNavigation<MainViewModel>();
        }
    }
}