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
using MySql.Data.MySqlClient;

namespace Application_Schreibtrainer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Datenbankverbindung d1 = new Datenbankverbindung("127.0.0.1", "test", "root", "");
            //d1.Daten("Patrick", "krebs", 11, 100);
            Datenbankverbindung d2 = new Datenbankverbindung(textBoxSIP.Text, textBoxDB.Text, textBoxUID.Text, "");
            int zeit= 3;
            d2.Daten(textBoxName.Text, textBoxKlasse.Text, Convert.ToInt32(textBoxKatalogNR.Text), zeit );

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private List<string> toStringList(string s)
        {
            return s.Split().ToList<string>();

        }
    }
}
