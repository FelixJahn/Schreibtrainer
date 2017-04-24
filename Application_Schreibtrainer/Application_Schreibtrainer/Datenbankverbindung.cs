using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Application_Schreibtrainer
{
    class Datenbankverbindung
    {
        public string ServerIp { get; set; }
        public string UserID { get; set; }
        public string Datenbank { get; set; }
        public string Passwort { get; set; }

        private MySqlConnection conn { get; set; }
        static string connString { get; set; }

        public string Vorname { get; set; }
        public string Klasse { get; set; }
        public int Katalognummer { get; set; }

        public double Zeit { get; set; }
        
        //Konstruktor machen der die props einliest und speichert
        //private Methode die einen con string generiert und sich gleich versucht zu verbinden(gleich im konstruktor aufrufen)
        //vererbung machen 
        //methode schreiben mit der man daten in die Datenbank schreibt
        //datenbankentwurf in phpmyadmin

        public Datenbankverbindung(string serverip, string datenbank, string userid, string passwort)
        {
            ServerIp = serverip;

            UserID = userid;
            Datenbank = datenbank;
            Passwort = passwort;

                                                                                                          
            connString = $"Server={serverip};uid={userid};database={datenbank};pwd={passwort}";

            Verbindung(connString);
        }
        public void Daten(string vorname, string klasse, int katalognummer, double zeit)

        {
            Vorname = vorname;
            Klasse = klasse;
            Katalognummer = katalognummer;
            Zeit = zeit;
            MySqlConnection conn = new MySqlConnection(connString);


            try
            {
                conn.Open();
                MySqlCommand cmd =new MySqlCommand("Insert into data values(@name, @klasse, @ktlgnr, @zeit, null)", conn);


                cmd.Parameters.AddWithValue("@zeit", zeit);
                cmd.Parameters.AddWithValue("@name", vorname);
                cmd.Parameters.AddWithValue("@klasse", klasse);
                cmd.Parameters.AddWithValue("@ktlgnr", katalognummer);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Datenbankverbindung geschlossen, Daten gesendet!");
            }
        }
        private void Verbindung(string ConnString)
        {
            connString = ConnString;
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                conn.Open();
                MessageBox.Show("Datenbankverbindung Hergestellt");
            }
            catch
            {
                MessageBox.Show("Datenbankverbindung nicht hergestellt!");
            }
        }
    }
}
