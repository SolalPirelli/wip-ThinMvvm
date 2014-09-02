﻿// Copyright (c) Solal Pirelli 2014
// See License.txt file for more details

using ThinMvvm.SampleApp.Services;
using ThinMvvm.SampleApp.ViewModels;
using ThinMvvm.SampleApp.WindowsRuntime.Views;
using ThinMvvm.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Phone.UI.Input;

namespace ThinMvvm.SampleApp.WindowsRuntime
{
    public sealed partial class App : AppBase
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void Initialize( LaunchActivatedEventArgs e )
        {
            // simple app, no arguments necessary

            var navigationService = Container.Bind<IWindowsRuntimeNavigationService, WindowsRuntimeNavigationService>();
            Container.Bind<ISettingsStorage, WindowsRuntimeSettingsStorage>();
            Container.Bind<ISettings, Settings>();

            navigationService.Bind<MainViewModel, MainView>();
            navigationService.Bind<AboutViewModel, AboutView>();

            // handle the back button since WP doesn't do it for us
            HardwareButtons.BackPressed += ( _, e2 ) =>
            {
                e2.Handled = true;
                navigationService.NavigateBack();
            };

            navigationService.NavigateTo<MainViewModel, int>( 42 );
        }

        public class X
        {
            public int N { get; set; }
            public X Xx { get; set; }
            public string S { get; set; }
        }
    }
}