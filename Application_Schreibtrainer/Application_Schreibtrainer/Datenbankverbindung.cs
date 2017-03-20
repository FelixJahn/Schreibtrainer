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
        public string Nachname { get; set; }
        public int Jahresalter { get; set; }
        public int Zeit { get; set; }
        //Konstruktor machen der die props einliest und speichert
        //private Methode die einen con string generiert und sich gleich versucht zu verbinden(gleich im konstruktor aufrufen)
        //vererbung machen 
        //methode schreiben mit der man daten in die Datenbank schreibt
        //datenbankentwurf in phpmyadmin

        public Datenbankverbindung(string serverip, string datenbank, string userid, string passwort)
        {
            ServerIp = serverip;
<<<<<<< HEAD
            UserID = userid;
            Datenbank = datenbank;
            Passwort = passwort;


            
            connString = "Server=" + serverip + ";database=" + datenbank + ";uid=" + userid + ";pwd=" + passwort;
=======
            userid = UserID;
            datenbank = Datenbank;
            passwort = Passwort;
            connString = "Server=" + serverip + ";uid=" + userid + ";database=" + datenbank + ";pwd=" + passwort;
>>>>>>> 6080b6f95d51b79bf156f84a0bc35c9626cad8e7
            //"Server:"+serverip + ";uid:" + userid + ";database:" + datenbank + ";pwd:" + passwort;
            
            Verbindung(connString);
        }
        public void Daten(string vorname, string nachname, int jahresalter, int zeit)

        {
            Vorname = vorname;
            Nachname = nachname;
            Jahresalter = jahresalter;
            Zeit = zeit;
            MySqlConnection conn = new MySqlConnection(connString);


            try
            {
                conn.Open();
                var cmd =new MySqlCommand("Insert into testuser values('" + vorname + "','" + nachname + "'," + jahresalter + "," + zeit+")",
                        conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("stophere");

            }
            catch
            {
                MessageBox.Show("Statement ist falsch");
            }
            finally
            {
                conn.Close();
            }
        }
        private void Verbindung(string ConnString)
        {
            connString = ConnString;
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                conn.Open();
                MessageBox.Show("Es funkt");
            }
            catch
            {
                throw new Exception("Funktioniert nicht");
            }
        }
    }
}
