using System;
using System.Data;
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
                string CommandText = "SELECT ID, DataPrzyjecia, Marka, Model, Imie, Nazwisko, NrTelefonu, Zlecenie_Klienta FROM Zaplanowane_Samochody";
                Scheduled_Cars_DB = new SQLiteDataAdapter(CommandText, sql_conn);
                Scheduled_Cars_DS.Reset();
                Scheduled_Cars_DB.Fill(Scheduled_Cars_DS);
                Scheduled_Cars_DT = Scheduled_Cars_DS.Tables[0];
                form.Scheduled_Cars_View.DataSource = Scheduled_Cars_DT;
                sql_conn.Close();

                form.Scheduled_Cars_View.Columns["ID_Column"].Visible = false;
            }
            catch
            {
                MessageBox.Show("Tabelka już stworzona", "Warsztat");
                Update.Create_Scheduled_Cars();
            }
            //form.Scheduled_Cars_View.Columns["ID_Column"].Visible = false;
        }
        public void Add(Form1 form, int ID)
        {
            sql_conn.Close();
            sql_conn.Open();
            try
            {
                sql_cmd =new SQLiteCommand("INSERT INTO Zaplanowane_Samochody(DataPrzyjecia,Model, Marka,  Imie, Nazwisko, NrTelefonu, Zlecenie_Klienta) " +
                "VALUES(@DataPrzyjecia, @Model, @Marka, @Imie, @Nazwisko,@NrTelefonu, @Zlecenie_Klienta)", sql_conn);
                sql_cmd.Parameters.AddWithValue("@Model", form.Model.Text.Trim());
                sql_cmd.Parameters.AddWithValue("@Marka", form.Marka.Text.Trim());
                sql_cmd.Parameters.AddWithValue("@Imie", form._Name.Text.Trim());
                sql_cmd.Parameters.AddWithValue("@Nazwisko", form.LastName.Text.Trim());
                sql_cmd.Parameters.AddWithValue("@NrTelefonu", form.TelefonKomurkowy.Text);
                sql_cmd.Parameters.AddWithValue("@Zlecenie_Klienta", form.txtZlecenie_Klienta.Text.Trim());
                sql_cmd.Parameters.AddWithValue("@DataPrzyjecia", form.DataPrzyjecia.Text.Trim());
                sql_cmd.ExecuteNonQuery();              

                MessageBox.Show("Samochód  " +form.Marka.Text.Trim()  + " " + form.Model.Text.Trim() + " jest zapłanowany na " + form.DataPrzyjecia.Text.Trim());
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
            sql_conn.Close();
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
        public void ReadData(Form1 form, int ID)
        {
            ID = Convert.ToInt32(form.Scheduled_Cars_View.CurrentRow.Cells["ID_Column"].Value.ToString());

            form.DataPrzyjecia.Text = form.Scheduled_Cars_View.CurrentRow.Cells["DataPrzyjecia_Column"].Value.ToString();
            form.Marka.Text = form.Scheduled_Cars_View.CurrentRow.Cells["Marka_Column"].Value.ToString();
            form.Model.Text = form.Scheduled_Cars_View.CurrentRow.Cells["Model_Column"].Value.ToString();
            form._Name.Text = form.Scheduled_Cars_View.CurrentRow.Cells["Imie_Column"].Value.ToString();
            form.LastName.Text = form.Scheduled_Cars_View.CurrentRow.Cells["Nazwisko_Column"].Value.ToString();
            form.txtZlecenie_Klienta.Text = form.Scheduled_Cars_View.CurrentRow.Cells["Zlecenie_Klienta_Column"].Value.ToString();
            form.TelefonKomurkowy.Text= form.Scheduled_Cars_View.CurrentRow.Cells["NrTelefonu_Column"].Value.ToString();
        }
    }
}
