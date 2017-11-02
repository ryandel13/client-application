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
    /// Interaktionslogik für XAxisLabels.xaml
    /// </summary>
    public partial class XAxisLabels : UserControl
    {
        public XAxisLabels()
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
            DependencyProperty.Register("LineColor", typeof(SolidColorBrush), typeof(XAxisLabels), new PropertyMetadata(Brushes.White));

        public double XLocation
        {
            get { return (double)GetValue(XLocationProperty); }
            set { SetValue(XLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XLocationProperty =
            DependencyProperty.Register("XLocation", typeof(double), typeof(XAxisLabels), new PropertyMetadata(0d));

        public double LabelAngle
        {
            get { return (double)GetValue(LabelAngleProperty); }
            set { SetValue(LabelAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAngleProperty =
            DependencyProperty.Register("LabelAngle",
                typeof(double),
                typeof(XAxisLabels),
                new FrameworkPropertyMetadata(0d,
      new PropertyChangedCallback(OnLabelAngleChanged)));

        public static void OnLabelAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var test = (XAxisLabels)d;
            double value = (double)e.NewValue;

            if (value == 0)
            {
                test.MyLabel.Margin = new Thickness(-25, 0, 0, 0);
                test.MyLabel.TextAlignment = TextAlignment.Center;
            }
            else
            {
                test.MyLabel.Margin = new Thickness(0, 0, 0, 0);
                test.MyLabel.TextAlignment = TextAlignment.Left;
            }

        }

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(XLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XLabelProperty =
            DependencyProperty.Register("XLabel", typeof(string), typeof(XAxisLabels), new PropertyMetadata("", new PropertyChangedCallback(XLabelChange)));

        private static void XLabelChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var MyLabelClass = (XAxisLabels)d;
            if (MyLabelClass.XLabel == "")
            {
                MyLabelClass.XLine.Points[1] = new Point(0, 5);
            }
            else
            {
                MyLabelClass.XLine.Points[1] = new Point(0, 10);
            }
        }

        public bool XLabelVisible
        {
            get { return (bool)GetValue(XLabelVisibleProperty); }
            set { SetValue(XLabelVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XLabelVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XLabelVisibleProperty =
            DependencyProperty.Register("XLabelVisible", typeof(bool), typeof(XAxisLabels), new PropertyMetadata(true));


    }
}
