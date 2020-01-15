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
        string textMarathonScore;
        string textSprintTime;

        public StatsPage()
        {
            if (roamingSettings.Values.ContainsKey("linesCleared"))
            {
                textLinesCleared = roamingSettings.Values["linesCleared"].ToString();
                textTetrisCleared = roamingSettings.Values["tetrisCleared"].ToString();
            }
            else
            {
                roamingSettings.Values.Add("linesCleared", 0);
                roamingSettings.Values.Add("tetrisCleared", 0);
                textLinesCleared = roamingSettings.Values["linesCleared"].ToString();
                textTetrisCleared = roamingSettings.Values["tetrisCleared"].ToString();
            }

            if(roamingSettings.Values.ContainsKey("endlessHighScore"))
            {
                textEndlessScore = roamingSettings.Values["endlessHighScore"].ToString();
            }
            else
            {
                roamingSettings.Values.Add("endlessHighScore", 0);
                textEndlessScore = roamingSettings.Values["endlessHighScore"].ToString();
            }

            if(roamingSettings.Values.ContainsKey("marathonHighScore"))
            {
                textMarathonScore = roamingSettings.Values["marathonHighScore"].ToString();
            }
            else
            {
                roamingSettings.Values.Add("marathonHighScore", 0);
                textMarathonScore = roamingSettings.Values["marathonHighScore"].ToString();
            }

            if(roamingSettings.Values.ContainsKey("sprintBestTime"))
            {
                TimeSpan time = TimeSpan.FromMilliseconds((double)roamingSettings.Values["sprintBestTime"]);
                textSprintTime = $"{time.Minutes}:{time.Seconds}:{time.Milliseconds}";
            }
            else
            {
                roamingSettings.Values.Add("sprintBestTime", 0.0);
                textSprintTime = roamingSettings.Values["sprintBestTime"].ToString();
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
