using System;
using System.Collections.Generic;
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
using Windows.System;
using System.Diagnostics;
using Windows.UI.Xaml.Documents;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nieTRIS_future
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatsPage : Page
    {
        static Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

        string textLinesCleared;
        string textTetrisCleared;
        string textEndlessScore;
        string textUltraScore;
        string textSprintTime;

        public StatsPage()
        {
            if (roamingSettings.Values.ContainsKey("linesCleared"))
            {
                textLinesCleared = roamingSettings.Values["linesCleared"].ToString();
                textTetrisCleared = roamingSettings.Values["tetrisCleared"].ToString();
                textEndlessScore = MainPage.P1.getEndlessScore().ToString();
                textUltraScore = MainPage.P1.getUltraScore().ToString();
                textSprintTime = MainPage.P1.getSprintTime().ToString();
            }
            else
            {
                roamingSettings.Values.Add("linesCleared", 0);
                roamingSettings.Values.Add("tetrisCleared", 0);
                textLinesCleared = roamingSettings.Values["linesCleared"].ToString();
                textTetrisCleared = roamingSettings.Values["tetrisCleared"].ToString();
                textEndlessScore = MainPage.P1.getEndlessScore().ToString();
                textUltraScore = MainPage.P1.getUltraScore().ToString();
                textSprintTime = MainPage.P1.getSprintTime().ToString();
            }
            this.InitializeComponent();
        }

        private void NavigateToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            if (e.Key == VirtualKey.Escape) this.Frame.Navigate(typeof(MainPage));

        }
    }
}
