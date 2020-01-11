using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace nieTRIS_future
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
{

        //public static Frame rootFrame = Window.Current.Content as Frame;
        Game1 _game;


        public GamePage()
    {
            this.InitializeComponent();
            
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);


        }

        private void NavigateToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _game.MiuzikuStoppo();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            _game.Reset();
            _game.Load();
            _game.currentGameState = Game1.GameStates.Game;

            userControl.Focus(FocusState.Programmatic);
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine("cos wcisniete");
            if (e.Key == Windows.System.VirtualKey.GamepadView || e.Key == Windows.System.VirtualKey.Q)
            {
                Debug.WriteLine("dziala");
                _game.currentGameState = Game1.GameStates.End;
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}