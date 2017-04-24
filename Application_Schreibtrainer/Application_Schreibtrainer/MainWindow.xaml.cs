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
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.IO;
using System.Timers;

namespace Application_Schreibtrainer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    //Dinge die ich noch einfügen will
    //- Text den man schon geschrieben hat verschwindet 
    //Fehlercount
    public partial class MainWindow : Window
    {
        private List<string> ListOfWords;
        Timer aTimer = new Timer(1000);
        int countdown = 60;
        private int index = 0;
        private bool TextChangedtheFirstTime = true;
        private bool TestMode = false;
        private int fehler = 0;
        Datenbankverbindung d;
        private List<string> backuplow;
        public MainWindow()
        {
            InitializeComponent();
            //foreach (string w )

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListOfWords = new List<string>();
            textBoxVorlage.IsReadOnly = true;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Stop();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            decrementCountdown();
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Datenbankverbindung d2 = new Datenbankverbindung(textBoxSIP.Text, textBoxDB.Text, textBoxUID.Text, "");
            //int zeit = 3;
            //d2.Daten(textBoxName.Text, textBoxKlasse.Text, Convert.ToInt32(textBoxKatalogNR.Text), zeit);
            aTimer.Stop();
            fehler = 0;
            countdown = 60;
            TextChangedtheFirstTime = true;
            labelTime.Content = 60;
            listBoxDaten.Items.Clear();
            ListOfWords.Clear();
            foreach (string item in backuplow)
            {
                ListOfWords.Add(item);
            }
            showListOfWords();
        }

        private void decrementCountdown()
        {
            if (countdown >= 1)
            {
                countdown--;
                this.Dispatcher.Invoke(() =>
                {
                    labelTime.Content = Convert.ToString(countdown);
                });

            }
            else
            {
                aTimer.Stop();
                MessageBox.Show("Countdown abgelaufen!", "Ende", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Dispatcher.Invoke(() =>
                {
                    textBoxInput.IsReadOnly = true;
                
                if (TestMode)
                {
                    d.Daten(textBoxName.Text, textBoxKlasse.Text, Convert.ToInt32(textBoxKatalogNR.Text), index);//code optimieren für andere Werte als 60s//
                }
                });
            }
        }
        private List<string> toStringList(string s)
        {
            try
            {
                return s.Split(' ').ToList<string>();
            }
            catch (Exception)
            {
                MessageBox.Show("Leere Datei!");
                return new List<string>();

            }

        }
        private void addToListOfWords(List<string> l)
        {
            foreach (string s in l)
            {
                if (s != null)
                {
                    ListOfWords.Add(s);
                    //MessageBox.Show(s);
                }

            }
        }

        private void btnOFD_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "txt files (*.txt)|*.txt";


            if (ofd1.ShowDialog().HasValue)
            {
                labelFD.Content = ofd1.FileName;
                using (StreamReader r = new StreamReader(ofd1.FileName))
                {
                    addToListOfWords(toStringList(r.ReadLine()));
                }
            }
            showListOfWords();
        }

        private void textBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChangedtheFirstTime)
            {
                if (!(ListOfWords.Count<1))
                {
                    aTimer.Start();
                    TextChangedtheFirstTime = false;
                    backuplow = new List<string>();
                    foreach (string item in ListOfWords)
                    {
                        backuplow.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Text muss vorhanden sein!");
                    textBoxInput.Text = "";
                }

            }
        }
        private void textBoxInput_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                if (ListOfWords.Count<2)
                {
                    foreach (string item in backuplow)
                    {
                        ListOfWords.Add(item);
                        showListOfWords();
                    }
                }
                string s = textBoxInput.Text.Trim();
                if (s == ListOfWords[0])
                {
                    ListOfWords.RemoveAt(0);
                    textBoxVorlage.Text = "";
                    showListOfWords();
                    textBoxInput.Text = "";
                    index++;

                    outputListBox(calculateWPM(Convert.ToDouble(60 - countdown), index), index, fehler);
                }
                else
                {
                    fehler++;
                    outputListBox(calculateWPM(Convert.ToDouble(60 - countdown), index), index, fehler);

                }
            }
        }

        private void buttonTestMode_Click(object sender, RoutedEventArgs e)
        {
            if (TestMode)
            {
                TestMode = false;
                labelTestMode.Content = "Deaktiviert";
            }
            else
            {
                TestMode = true;
                labelTestMode.Content = "Aktiviert";
                if (textBoxDB.Text == "" || textBoxKatalogNR.Text == "" || textBoxKlasse.Text == "" || textBoxName.Text == "" || textBoxSIP.Text == "" || textBoxUID.Text == "")
                {
                    MessageBox.Show("Bitte füllen Sie alle Felder unten aus.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TestMode = false;
                    labelTestMode.Content = "Deaktiviert";
                }
                else
                {
                    d = new Datenbankverbindung(textBoxSIP.Text, textBoxDB.Text, textBoxUID.Text, "");//Password?emirhan fragen ! Ausbessern in database, Emirhan solls testen 
                }

            }
        }

        private void outputListBox(double wpm, int wordsWritten, int f)
        {
            listBoxDaten.Items.Clear();
            listBoxDaten.Items.Add("WPM: " + wpm);
            listBoxDaten.Items.Add("Geschrieben :" + wordsWritten);
            listBoxDaten.Items.Add("Fehler :" + f);

        }

        private double calculateWPM(double time, double words)
        {
            if (time == 0)
            {
                return 60 * words;
            }
            else
            {
                return (60 / time) * words;
            }
        }

        private void showListOfWords()
        {
            foreach (string s in ListOfWords)
            {
                textBoxVorlage.Text += s + " ";
            }
        }
    }
}
