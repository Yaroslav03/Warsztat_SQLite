//Copy data from old BD to new BD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Warsztat
{
    internal class Copy_DB
    {
        private SQLiteConnection conn;
        private SQLiteCommand cmd;

        public void Update_DB()
        {
            conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            conn.Open();
            //Create a new table
            cmd = new SQLiteCommand("CREATE TABLE Dane_New (ID	INTEGER NOT NULL UNIQUE, DataPrzyjecia	TEXT NOT NULL, DataWydania TEXT NOT NULL, Marka	TEXT NOT NULL, Model TEXT NOT NULL, NumerRejestracji	TEXT NOT NULL, RokProdukcji	TEXT NOT NULL, PojemnoscSilnika	TEXT NOT NULL, Przebieg	INTEGER NOT NULL, NumerNadwozia	TEXT NOT NULL, IDSilnika	TEXT NOT NULL, Imie TEXT NOT NULL, Nazwisko TEXT NOT NULL, NIP TEXT NOT NULL, Telefon	TEXT NOT NULL, Adres	TEXT NOT NULL,	ZlecenieKlienta TEXT NOT NULL, Diagnostyka	TEXT NOT NULL, Naprawa	TEXT NOT NULL, Koszt_Szacunkowy INTEGER NOT NULL, Koszt_Koncowy INTEGER NOT NULL, TestDrive	TEXT NOT NULL, PozostawioneKluczyki	TEXT NOT NULL, PozostawioneDokumenty	TEXT NOT NULL, PRIMARY KEY(ID AUTOINCREMENT))", conn);
            cmd.ExecuteNonQuery();
            //Copy data from old to new table
            cmd = new SQLiteCommand("INSERT INTO Dane_New(ID, DataPrzyjecia, DataWydania, Marka, Model,  NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty)" +
                "VALUES ((SELECT ID FROM Dane),(SELECT DataPrzyjecia From Dane), (''), (SELECT Marka From Dane),(SELECT Model From Dane),(SELECT NumerRejestracji From Dane),(SELECT RokProdukcji From Dane), (SELECT PojemnoscSilnika From Dane),(SELECT Przebieg From Dane),(SELECT NumerNadwozia From Dane),(SELECT IDSilnika From Dane), (SELECT Imie From Dane),(SELECT Nazwisko From Dane),(SELECT NIP From Dane),(SELECT Telefon From Dane),(SELECT Adres From Dane),(SELECT ZlecenieKlienta From Dane),(SELECT Diagnostyka From Dane),(SELECT Naprawa From Dane),(SELECT Koszt From Dane),('0'),(SELECT TestDrive From Dane),(SELECT PozostawioneKluczyki From Dane),(SELECT PozostawioneDokumenty From Dane))", conn);
            cmd.ExecuteNonQuery();
            //Delete tabele old
            cmd = new SQLiteCommand("DROP TABLE Dane;", conn);
            cmd.ExecuteNonQuery();
            //Rename a Dane_New to Dane
            cmd = new SQLiteCommand("ALTER TABLE Dane_New RENAME TO Dane;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}