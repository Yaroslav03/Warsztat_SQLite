//Copy data from old BD to new BD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace Warsztat
{
    internal class Update
    {
        private  SQLiteConnection conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
        SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter sqlDB = new SQLiteDataAdapter();
        DataSet sqlDS = new DataSet();
        DataTable sqlDT = new DataTable();

        public void Create_New_DB()
        {
            try
            {
                conn.Open();
                //Create a new table
                cmd = new SQLiteCommand("CREATE TABLE Dane_New " +
                    "(ID INTEGER NOT NULL UNIQUE," +
                    " DataPrzyjecia	TEXT NOT NULL," +
                    " DataWydania TEXT NOT NULL, " +
                    "Marka	TEXT NOT NULL, " +
                    "Model TEXT NOT NULL," +
                    " NumerRejestracji TEXT NOT NULL," +
                    " RokProdukcji TEXT NOT NULL," +
                    " PojemnoscSilnika TEXT NOT NULL, " +
                    "Przebieg INTEGER NOT NULL, " +
                    "NumerNadwozia TEXT NOT NULL, " +
                    "IDSilnika TEXT NOT NULL, " +
                    "Imie TEXT NOT NULL," +
                    " Nazwisko TEXT NOT NULL, " +
                    "NIP TEXT NOT NULL, " +
                    "Telefon TEXT NOT NULL, " +
                    "Adres	TEXT NOT NULL,	" +
                    "ZlecenieKlienta TEXT NOT NULL, " +
                    "Diagnostyka TEXT NOT NULL, " +
                    "Naprawa TEXT NOT NULL, " +
                    "Koszt_Szacunkowy INTEGER NOT NULL, " +
                    "Koszt_Koncowy INTEGER NOT NULL, " +
                    "TestDrive TEXT NOT NULL, " +
                    "PozostawioneKluczyki TEXT NOT NULL, " +
                    "PozostawioneDokumenty TEXT NOT NULL, " +
                    "Zakupione_Czesci TEXT NOT NULL,  " +
                    "PRIMARY KEY(ID AUTOINCREMENT))", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Tabelka już dawno stworzona");
            }
        }

        public void Finish()
        {
            conn.Open();
            //Delete tabele old
            DialogResult result = MessageBox.Show( "Jeśli wy skopjowali dane to proszę kliknąć OK a jeśli nie kliknięcie Ok powoduję usunięcia danych bez możliwości przywrócenia",
                "Warsztat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                    cmd = new SQLiteCommand("DROP TABLE Dane;", conn);
                    cmd.ExecuteNonQuery();
                    //Rename a Dane_New to Dane
                    cmd = new SQLiteCommand("ALTER TABLE Dane_New RENAME TO Dane;", conn);
                    cmd.ExecuteNonQuery();
                    Application.Restart();
            }
            else if( result == DialogResult.No)
            {

            }
            conn.Close();
        }
        public void Create_Scheduled_Cars()
        {
            conn.Open();
            //Create a new table
            cmd = new SQLiteCommand("CREATE TABLE Zaplanowane_Samochody " +
                "(ID INTEGER NOT NULL UNIQUE," +
                " DataPrzyjecia TEXT NOT NULL," +
                " Model TEXT NOT NULL, " +
                "Marka TEXT NOT NULL, Imie  " +
                "TEXT NOT NULL, " +
                "Nazwisko  TEXT NOT NULL," +
                " Zlecenie_Klienta TEXT NOT NULL, " +
                "PRIMARY KEY(ID AUTOINCREMENT))", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        public void Delete()
        {
            try
            {
                conn.Open();
                //Delete tabele old
                cmd = new SQLiteCommand("DROP TABLE Dane_New;", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Tabelki nie istnieje", "Warsztat");
            }
        }
        public void Save_data_old(int ID)
        {            
            conn.Open();
            try
            {//new version
               
                cmd.CommandText = "INSERT INTO Dane_New(ID, DataPrzyjecia, DataWydania, Marka, Model,  NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci)" +
                        "VALUES ((SELECT ID FROM Dane),(SELECT DataPrzyjecia From Dane), (SELECT DataWydania FROM Dane), (SELECT Marka From Dane),(SELECT Model From Dane),(SELECT NumerRejestracji From Dane),(SELECT RokProdukcji From Dane), (SELECT PojemnoscSilnika From Dane),(SELECT Przebieg From Dane),(SELECT NumerNadwozia From Dane),(SELECT IDSilnika From Dane), (SELECT Imie From Dane),(SELECT Nazwisko From Dane),(SELECT NIP From Dane),(SELECT Telefon From Dane),(SELECT Adres From Dane),(SELECT ZlecenieKlienta From Dane),(SELECT Diagnostyka From Dane),(SELECT Naprawa From Dane),(SELECT Koszt_Szacunkowy From Dane),(SELECT Koszt_Koncowy FROM Dane),(SELECT TestDrive From Dane),(SELECT PozostawioneKluczyki From Dane),(SELECT PozostawioneDokumenty From Dane), (SELECT Zakupione_Czesci FROM Dane))";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
            }
            catch
            {// old version
                cmd.CommandText = "INSERT INTO Dane_New(ID, DataPrzyjecia, DataWydania, Marka, Model,  NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci)" +
                     "VALUES ((SELECT ID FROM Dane),(SELECT DataPrzyjecia From Dane), (''), (SELECT Marka From Dane),(SELECT Model From Dane),(SELECT NumerRejestracji From Dane),(SELECT RokProdukcji From Dane), (SELECT PojemnoscSilnika From Dane),(SELECT Przebieg From Dane),(SELECT NumerNadwozia From Dane),(SELECT IDSilnika From Dane), (SELECT Imie From Dane),(SELECT Nazwisko From Dane),(SELECT NIP From Dane),(SELECT Telefon From Dane),(SELECT Adres From Dane),(SELECT ZlecenieKlienta From Dane),(SELECT Diagnostyka From Dane),(SELECT Naprawa From Dane),(SELECT Koszt From Dane),(SELECT Koszt From Dane),(SELECT TestDrive From Dane),(SELECT PozostawioneKluczyki From Dane),(SELECT PozostawioneDokumenty From Dane), (''))";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public void Load_To_Copy_NEW_Tabel(Form1 form)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            string CommandText = "select * from Dane_New";
            sqlDB = new SQLiteDataAdapter(CommandText, conn);
            sqlDS.Reset();
            sqlDB.Fill(sqlDS);
            sqlDT = sqlDS.Tables[0];
            form.NEW_Table.DataSource = sqlDT;
            form.NEW_Table.Columns[0].Visible = false;
            conn.Close();
        }
        public void Load_To_Copy_Old_Tabel(Form1 form)
        {
            SQLiteDataAdapter DB = new SQLiteDataAdapter();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            conn.Open();
            cmd = conn.CreateCommand();
            string CommandText = "select * from Dane";
            DB = new SQLiteDataAdapter(CommandText, conn);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            form.OLD_Table.DataSource = DT;
            form.OLD_Table.Columns[0].Visible = false;
            conn.Close();
        }
    }
}
