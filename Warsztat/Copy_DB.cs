//Copy data from old BD to new BD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Warsztat
{
    internal class Copy_DB
    {
        private SQLiteConnection conn;
        private SQLiteCommand cmd;

        public void Create_New_DB()
        {
            conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            conn.Open();
            //Create a new table
            cmd = new SQLiteCommand("CREATE TABLE Dane_New (ID	INTEGER NOT NULL UNIQUE, DataPrzyjecia	TEXT NOT NULL, DataWydania TEXT NOT NULL, Marka	TEXT NOT NULL, Model TEXT NOT NULL, NumerRejestracji	TEXT NOT NULL, RokProdukcji	TEXT NOT NULL, PojemnoscSilnika	TEXT NOT NULL, Przebieg	INTEGER NOT NULL, NumerNadwozia	TEXT NOT NULL, IDSilnika TEXT NOT NULL, Imie TEXT NOT NULL, Nazwisko TEXT NOT NULL, NIP TEXT NOT NULL, Telefon	TEXT NOT NULL, Adres	TEXT NOT NULL,	ZlecenieKlienta TEXT NOT NULL, Diagnostyka	TEXT NOT NULL, Naprawa	TEXT NOT NULL, Koszt_Szacunkowy INTEGER NOT NULL, Koszt_Koncowy INTEGER NOT NULL, TestDrive	TEXT NOT NULL, PozostawioneKluczyki	TEXT NOT NULL, PozostawioneDokumenty TEXT NOT NULL, Zakupione_Czesci TEXT NOT NULL,  PRIMARY KEY(ID AUTOINCREMENT))", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        public void Finish()
        {
            conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            conn.Open();
            //Delete tabele old
            cmd = new SQLiteCommand("DROP TABLE Dane;", conn);
            cmd.ExecuteNonQuery();
            //Rename a Dane_New to Dane
            cmd = new SQLiteCommand("ALTER TABLE Dane_New RENAME TO Dane;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Create_Scheduled_Cars()
        {
            conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            conn.Open();
            //Create a new table
            cmd = new SQLiteCommand("CREATE TABLE Zaplanowane_Samochody (ID INTEGER NOT NULL UNIQUE, DataPrzyjecia TEXT NOT NULL, Model TEXT NOT NULL, Marka TEXT NOT NULL, Imie  TEXT NOT NULL, Nazwisko  TEXT NOT NULL, Zlecenie_Klienta  TEXT NOT NULL, PRIMARY KEY(ID AUTOINCREMENT))", conn);
            cmd.ExecuteNonQuery();
            conn.Close ();
        }
        public void Read_ID_Scheduled_Cars(int ID)
        {        
                conn.Open();
                try
                {
                    //Дані до опису Транспорту
                    if (ID != 0)
                    {
                        cmd = new SQLiteCommand("DELETE FROM Zaplanowane_Samochody WHERE ID =@ID", conn);
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie udało się usunąć dane", "Warsztat");
                }
                conn.Close();
            }
        public void Delete()
        {            
            conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            conn.Open();
            //Delete tabele old
            cmd = new SQLiteCommand("DROP TABLE Dane_New;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}