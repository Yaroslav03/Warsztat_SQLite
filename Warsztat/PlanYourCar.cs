using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace Warsztat
{
    internal class PlanYourCar
    {
        private SQLiteConnection sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
        SQLiteCommand sql_cmd = new SQLiteCommand();
        SQLiteDataAdapter DB = new SQLiteDataAdapter();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        Warsztat Warsztat = new Warsztat();
        Update Update = new Update();

        
        public void Load (Form1 form)
        {
            try
            {
                SQLiteDataAdapter Scheduled_Cars_DB = new SQLiteDataAdapter();
                DataSet Scheduled_Cars_DS = new DataSet();
                DataTable Scheduled_Cars_DT = new DataTable();
                sql_conn.Open();
                sql_cmd = sql_conn.CreateCommand();
                string CommandText = "SELECT ID, DataPrzyjecia, Marka, Model, Imie, Nazwisko, Zlecenie_Klienta FROM Zaplanowane_Samochody";
                Scheduled_Cars_DB = new SQLiteDataAdapter(CommandText, sql_conn);
                Scheduled_Cars_DS.Reset();
                Scheduled_Cars_DB.Fill(Scheduled_Cars_DS);
                Scheduled_Cars_DT = Scheduled_Cars_DS.Tables[0];
                form.Scheduled_Cars_View.DataSource = Scheduled_Cars_DT;
                sql_conn.Close();
            }
            catch
            {
                MessageBox.Show("Tabelka już stworzona", "Warsztat");
                Update.Create_Scheduled_Cars();
            }
        }
        public void Add(Form1 form, int ID)
        { 
            sql_conn.Open();
            try
            {
                sql_cmd =new SQLiteCommand("INSERT INTO Zaplanowane_Samochody(DataPrzyjecia,Model, Marka,  Imie, Nazwisko, Zlecenie_Klienta) " +
                "VALUES(@DataPrzyjecia, @Model, @Marka, @Imie, @Nazwisko, @Zlecenie_Klienta)",sql_conn);
                sql_cmd.Parameters.AddWithValue("@Model", form.Model.Text);
                sql_cmd.Parameters.AddWithValue("@Marka", form.Marka.Text);
                sql_cmd.Parameters.AddWithValue("@Imie", form._Name.Text);
                sql_cmd.Parameters.AddWithValue("@Nazwisko", form.LastName.Text);
                sql_cmd.Parameters.AddWithValue("@Zlecenie_Klienta", form.txtZlecenie_Klienta.Text);
                sql_cmd.Parameters.AddWithValue("@DataPrzyjecia", form.DataPrzyjecia.Text);
                sql_cmd.ExecuteNonQuery();              

                MessageBox.Show("Samochód  " + form.Model.Text + " " + form.Marka.Text + " jest zapłanowany na " + form.DataPrzyjecia.Text);
                Warsztat.Clear(form, ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sql_conn.Close();
        }
        public void Read_ID_Scheduled_Cars(int ID)
        {
            sql_conn.Open();
            try
            {
                //Дані до опису Транспорту
                if (ID != 0)
                {
                    sql_cmd = new SQLiteCommand("DELETE FROM Zaplanowane_Samochody WHERE ID =@ID", sql_conn);
                    sql_cmd.Parameters.AddWithValue("@ID", ID);
                    sql_cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się usunąć dane", "Warsztat");
            }
            sql_conn.Close();
        }

    }
}
