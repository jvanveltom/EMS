﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using EMS_System.Resource;
using EMS_System.Util;

namespace EMS_System.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseHandler dbh;
        public MainWindow()
        {
            InitializeComponent();
            dbh = new DatabaseHandler();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Environment.Exit(8);
        }
    }
}
