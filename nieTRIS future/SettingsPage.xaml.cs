using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.System;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nieTRIS_future
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

        }

        private void NavigateToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SetMusicVolume(object sender, RoutedEventArgs e)
        {
                Slider sl = sender as Slider;

                if (sl != null)
                {
                    MainPage.musicVolume = sl.Value;
                }
        }

        private void SetSfxVolume(object sender, RoutedEventArgs e)
        {
            Slider sl = sender as Slider;

            if (sl != null)
            {
                MainPage.sfxVolume  = sl.Value;
            }
        }

        private void SetMusicSliderValue(object sender, RoutedEventArgs e)
        {
            Slider sl = sender as Slider;

            sl.Value = MainPage.musicVolume;
        }

        private void SetSfxSliderValue(object sender, RoutedEventArgs e)
        {
            Slider sl = sender as Slider;

            sl.Value = MainPage.sfxVolume;
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            if (e.Key == VirtualKey.Escape) this.Frame.Navigate(typeof(MainPage));

        }
    }
}
