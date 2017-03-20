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

        public string vorname { get; set; }
        public string nachname { get; set; }
        public int jahresalter { get; set; }
        public int zeit { get; set; }
        //Konstruktor machen der die props einliest und speichert
        //private Methode die einen con string generiert und sich gleich versucht zu verbinden(gleich im konstruktor aufrufen)
        //vererbung machen 
        //methode schreiben mit der man daten in die Datenbank schreibt
        //datenbankentwurf in phpmyadmin

        public Datenbankverbindung(string serverip, string datenbank, string userid, string passwort)
        {
            ServerIp = serverip;
            userid = UserID;
            datenbank = Datenbank;
            passwort = Passwort;


            
            connString = "Server=" + serverip + ";uid=" + userid + ";database=" + datenbank + ";pwd=" + passwort;
            //"Server:"+serverip + ";uid:" + userid + ";database:" + datenbank + ";pwd:" + passwort;

            Verbindung(connString);
        }
        public void daten(string Vorname, string Nachname, int Jahresalter, int Zeit)

        {
            vorname = Vorname;
            nachname = Nachname;
            jahresalter = Jahresalter;
            zeit = Zeit;
            var cmd = new MySqlCommand("Insert into testuser values('"+Vorname+"','"+Nachname+"',"+Jahresalter+","+Zeit, conn);

        }
        private void Verbindung(string ConnString)
        {
           

            connString = ConnString;

     


            MySqlConnection conn = new MySqlConnection(ConnString);

            try
            {
                conn.Open();
                daten(vorname, nachname, jahresalter, zeit);
                MessageBox.Show("Es funkt");
            }
            catch
            {
                throw new Exception("Funktioniert nicht");
            }
        }
    }
}
