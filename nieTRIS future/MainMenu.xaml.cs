using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace nieTRIS_future
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            

        }

        private void ButtonGotFocus(object sender, object args)
        {
            (sender as Button).Background = (ImageBrush)Resources["ButtonOn"];
        }

        private void ButtonLostFocus(object sender, object args)
        {
            (sender as Button).Background = (ImageBrush)Resources["ButtonSemi"];
        }


        private void NavigateToPlayPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlayPage));

        }

        private void NavigateToSettingsPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void NavigateToStatsPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StatsPage));
        }

        private void NavigateToCreditsPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreditsPage));
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
