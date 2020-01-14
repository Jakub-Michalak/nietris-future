using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

using System.Diagnostics;
using Windows.System;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nieTRIS_future
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        bool customizationFocused = false;
        Gamemode selectedMode;
        

        public PlayPage()
        {
            this.InitializeComponent();

            MainPage.P1.SetTheme("Neon");
            MainPage.P1.SetAudioPack("Default");
            MainPage.P1.SetRotationControls("Default");
        }

        private void ModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlipView flipView = (FlipView)sender;
            int position = flipView.SelectedIndex + 1;

            switch (position)
            {
                case 1:
                    selectedMode = new EndlessGM();
                    break;
                case 2:
                    selectedMode = new MarathonGM();
                    break;
                case 3:
                    selectedMode = new SprintGM();
                    break;
            }
            MainPage.P1.SetGamemode(selectedMode);
        }

        private void setFocusToPlayButton(object sender, RoutedEventArgs e)
        {
            PlayButton.Focus(FocusState.Programmatic);
        }

        private void customizationMenuFocused(object sender, RoutedEventArgs e)
        {
            customizationFocused = true;
        }

        private void customizationMenuLostFocus(object sender, RoutedEventArgs e)
        {
            customizationFocused = false;
        }

        private void NavigateToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void NavigateToGame(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        private void NavigateToControls(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ControlsPage));
        }

        private void SetStartingLevel(object sender, RoutedEventArgs e)
        {
            Slider sl = sender as Slider;

            if (sl != null)
            {
                MainPage.P1.SetStartingLevel((int)sl.Value);
            }
        }

        private void SetRotationControls(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string controlsName = rb.Tag.ToString();
                switch (controlsName)
                {
                    case "Default":
                        MainPage.P1.SetRotationControls("Default");
                        break;
                    case "Inverted":
                        MainPage.P1.SetRotationControls("Inverted");
                        break;
                }
            }
        }

        private void SetTheme(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb != null)
            {
                string controlsName = cb.SelectedValue.ToString();
                switch (controlsName)
                {
                    case "Neon":
                        MainPage.P1.SetTheme("Neon");
                        break;
                    case "Eggplant":
                        MainPage.P1.SetTheme("Eggplant");
                        break;
                    case "Retro":
                        MainPage.P1.SetTheme("Retro");
                        break;
                    case "Miku":
                        MainPage.P1.SetTheme("Miku");
                        break;
                }
            }
        }

        private void SetAudioPack(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb != null)
            {
                string controlsName = cb.SelectedValue.ToString();
                switch (controlsName)
                {
                    case "Default":
                        MainPage.P1.SetAudioPack("Default");
                        break;
                    case "Quake":
                        MainPage.P1.SetAudioPack("Quake");
                        break;
                    case "Miku":
                        MainPage.P1.SetAudioPack("Miku");
                        break;
                }
            }
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (customizationFocused == true) customizationFocused = false;
            else if (e.Key == VirtualKey.Escape && customizationFocused == false) this.Frame.Navigate(typeof(MainPage));
        }
    }
}
