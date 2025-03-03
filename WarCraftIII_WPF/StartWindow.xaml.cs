﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WarCraftIII_WPF
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void DistributionOfPoints_Button_Click(object sender, RoutedEventArgs e)
        {
            new DistributionOfPointsWindow().Show();
            Close();
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ImgMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Inventory_Button_Click(object sender, RoutedEventArgs e)
        {
            new Inventory().Show();
            Close();
        }

        private void AnimButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseEnter((Button)sender);
        }

        private void AnimButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseLeave((Button)sender);
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=o-YBDTqX_ZU") { UseShellExecute = true });
        }
    }
}
