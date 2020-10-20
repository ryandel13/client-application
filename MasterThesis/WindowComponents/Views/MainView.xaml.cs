using MasterThesis.Controller;
using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für MainMenu.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private static MainView instance;

        public static MainView getInstance()
        {
            if (instance == null)
            {
                instance = new MainView();
            }
            return instance;
        }

        public MainView()
        {
            InitializeComponent();

            BackgroundImage img1 = new BackgroundImage("abc", BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background));
            BackgroundImage img2 = new BackgroundImage("def", BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background_2));
            BackgroundImage img3 = new BackgroundImage("ghi", BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background_3));
            //ImageSource imgSrc3 = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background_3);
            //ImageSource imgSrc2 = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background_2);
            //ImageSource imgSrc1 = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background);

            BackgroundSelect.Items.Add(img1);
            BackgroundSelect.Items.Add(img2);
            BackgroundSelect.Items.Add(img3);
       
            BackgroundSelect.SelectedIndex = MasterThesis.Properties.Settings.Default.backgroundImage;

            LoadSpotifyDevices();
        }

        private void LoadSpotifyDevices()
        {
            SpotifyController spotifyController = SpotifyController.GetInstance();
            List<SpotifyDevice> devices = spotifyController.GetDevices();

            String selDev = SpotifyController.GetDevice();

            foreach(SpotifyDevice sd in devices)
            {
                DeviceSelect.Items.Add(sd);
                if (sd.DeviceId.Equals(selDev))
                {
                    DeviceSelect.SelectedItem = sd;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewController.ToggleFullscreen();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            MainViewController.SetBackground(((BackgroundImage)cb.SelectedValue).Image);
            MasterThesis.Properties.Settings.Default.backgroundImage = cb.SelectedIndex;
            MasterThesis.Properties.Settings.Default.Save();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Environment.Exit(0); //Bisschen Brutal, tut aber
        }

        private void DeviceSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            SpotifyController.SetDevice(((SpotifyDevice)cb.SelectedItem).DeviceId);
        }
    }
}
