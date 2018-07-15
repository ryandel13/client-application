using MasterThesis.Controller;
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

namespace MasterThesis.WindowComponents
{
    /// <summary>
    /// Interaktionslogik für MainFooter.xaml
    /// </summary>
    public partial class MainFooter : UserControl
    {
        public MainFooter()
        {
            InitializeComponent();
        }

        private void PlayBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MediaPlayerController.PlayResume();
        }

        private void NextBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MediaPlayerController.Forward();
        }

        private void MuteBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VolumeController.Mute();
        }
    }
}
