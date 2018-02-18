﻿using eureka_sharpener;
using eureka_sharpener.elements;
using MasterThesis.WindowComponents.Views;
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
    /// Interaktionslogik für ViewMenu.xaml
    /// </summary>
    public partial class ViewMenu : UserControl
    {

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public ViewMenu()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(UpdateServices);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
        }

        private void UpdateServices(object sender, EventArgs e)
        {
            Eureka eureka = new Eureka("localhost", 8761);
            Registry registry = eureka.ReadRegistry();
            Instance instance = registry.FindInstance("poi-service");
            if(instance == null)
            {
                this.NaviBtn.Enabled(false);
            } else
            {
                this.NaviBtn.Enabled(true);
            }
        }

        private void SensorBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(new TemperatureDiagram());
        }

        private void NaviBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(Navigation.getInstance());
        }
    }
}
