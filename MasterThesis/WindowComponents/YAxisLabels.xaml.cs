﻿using System;
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
    /// Interaktionslogik für YAxisLabels.xaml
    /// </summary>
    public partial class YAxisLabels : UserControl
    {
        public YAxisLabels()
        {
            InitializeComponent();
        }

        public SolidColorBrush LineColor
        {
            get { return (SolidColorBrush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register("LineColor", typeof(SolidColorBrush), typeof(YAxisLabels), new PropertyMetadata(Brushes.White));

        public double YLocation
        {
            get { return (double)GetValue(YLocationProperty); }
            set { SetValue(YLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YLocationProperty =
            DependencyProperty.Register("YLocation", typeof(double), typeof(YAxisLabels), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(YLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YLabelProperty =
            DependencyProperty.Register("YLabel", typeof(string), typeof(YAxisLabels), new PropertyMetadata("test"));

        public bool YLabelVisible
        {
            get { return (bool)GetValue(YLabelVisibleProperty); }
            set { SetValue(YLabelVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YLabelVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YLabelVisibleProperty =
            DependencyProperty.Register("YLabelVisible", typeof(bool), typeof(YAxisLabels), new PropertyMetadata(true));

    }
}
