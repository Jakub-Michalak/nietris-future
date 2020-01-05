using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
        public static Player P1 = new Player();

        public PlayPage()
        {
            this.InitializeComponent();

            P1.SetTheme("Neon");
            P1.SetAudioPack("Default");
            P1.SetRotationControls("Default");
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

        private void SetRotationControls(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string controlsName = rb.Tag.ToString();
                switch (controlsName)
                {
                    case "Default":
                        P1.SetRotationControls("Default");
                        break;
                    case "Inverted":
                        P1.SetRotationControls("Inverted");
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
                        P1.SetTheme("Neon");
                        break;
                    case "Eggplant":
                        P1.SetTheme("Eggplant");
                        break;
                    case "Retro":
                        P1.SetTheme("Retro");
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
                        P1.SetAudioPack("Default");
                        break;
                    case "Quake":
                        P1.SetAudioPack("Quake");
                        break;
                    case "Woop":
                        P1.SetAudioPack("Woop");
                        break;
                    case "Waterdrops":
                        P1.SetAudioPack("Waterdrops");
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
