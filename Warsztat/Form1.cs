using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Reflection;
using System.Diagnostics;

namespace Warsztat
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        string ConnectionString = "Data Source=Warsztat.db;Version=3;New=False;Compress=True;";
        public string Jazda;
        public string Kluczyki;
        public string Dokumenty;
        public string nothing;

        int ID = 0;

        Update Update = new Update();
        Interface Interface = new Interface();
        Warsztat Warsztat = new Warsztat();
        PlanYourCar PlanYourCar = new PlanYourCar();
        GeneratePDF GeneratePDF = new GeneratePDF();
        SQLiteConnection sql_conn = new SQLiteConnection();
        SQLiteCommand sql_cmd = new SQLiteCommand();


        public Form1()
        {          
            sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            InitializeComponent();
            Interface.Load_Data_Localize(this);
            Warsztat.Load_DB(this);
            PlanYourCar.Load(this);
            Interface.BodyNumberVerify(this);
            Interface.Verify_Button(this);
            Interface.Zlecenie(this);
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Warsztat.Search(this);
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            Warsztat.Delete(this, ID);
            Warsztat.Load_DB(this);
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            Warsztat.Save(this, ID);
        }

        private void Button_Update_Click(object sender, EventArgs e)
        {
            Warsztat.Update(this, ID);
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Warsztat.Clear(this, ID);
            Button_Save.Enabled = true;
        }

        private void Button_Clear_OP_Click(object sender, EventArgs e)
        {
            Warsztat.ClearPart(this, panel1.Controls);
            YearOfProduction.Text = "";
        }

        private void Button_Clear_Zlecenie_Klienta_Click(object sender, EventArgs e)
        {
            txtZlecenie_Klienta.Text = "";
        }

        private void Button_Clear_Diagnostyka_Click(object sender, EventArgs e)
        {
            txtDiagostyka.Text = "";
        }

        private void Button_Clear_DK_Click(object sender, EventArgs e)
        {
            Warsztat.ClearPart(this, panel3.Controls);
            TelefonKomurkowy.Text = "";
        }

        private void Button_Clear_Naprawa_Click(object sender, EventArgs e)
        {
            txtNaprawa.Text = "";
        }
        private void Button_Clear_Zakupione_Czesci_Click(object sender, EventArgs e)
        {
            txtZakupione_Czesci.Text = "";
        }

        private void Dane_Warsztat_DoubleClick(object sender, EventArgs e)
        {
            Warsztat.ReadData(this,ID);
            Work_Place.SelectTab(tabPage2);
            Button_Save.Enabled = false;
        }       
        private void Dane_Warsztat_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Convert to number
                ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells[0].Value.ToString());
                Warsztat.ReadData(this, ID);
            }
            catch 
            {
                MessageBox.Show("Wybrana Kolumna jest pusta");
            }
        }
        private void polskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interface.Localize_PL(this);
            Interface.Load_Data_Localize(this);
        }
        private void ukraińskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interface.Localize_UA(this);
            Interface.Load_Data_Localize(this);
        }
       
        private void Marka_TextChanged(object sender, EventArgs e)
        {
            Interface.Verify_Button(this);
        }

        private void Model_TextChanged(object sender, EventArgs e)
        {
            Interface.Verify_Button(this);  
        }

        private void NumerRejestracji_TextChanged(object sender, EventArgs e)
        {
            Interface.Verify_Button(this);    
        }

        private void _Name_TextChanged(object sender, EventArgs e)
        {
            Interface.Verify_Button(this);
        }
   
        private void stwórzNowąTabelkęToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Update.Load_To_Copy_Old_Tabel(this);
            Update.Create_New_DB();
            Update.Load_To_Copy_NEW_Tabel(this);
        }

        private void OLD_Table_MouseClick(object sender, MouseEventArgs e)
        {
            ID = Convert.ToInt32(OLD_Table.CurrentRow.Cells[0].Value.ToString()); 
        }

        private void kopjujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sql_conn.Open();
            Update.Save_data_old(ID);
            
            sql_conn.Close();
            Update.Load_To_Copy_NEW_Tabel(this);
            Update.Load_To_Copy_Old_Tabel(this);
            using (var sql_con = new SQLiteConnection(ConnectionString))
            {
                sql_con.Open();
                try
                {
                    //Дані до опису Транспорту
                    if (ID != 0)
                    {
                        sql_cmd = new SQLiteCommand("DELETE FROM Dane WHERE ID =@ID", sql_con);
                        sql_cmd.Parameters.AddWithValue("@ID", ID);
                        sql_cmd.ExecuteNonQuery();
                        Warsztat.Clear(this, ID);
                        Update.Load_To_Copy_Old_Tabel(this);
                    }
                    else
                    {
                        MessageBox.Show("Please Select Record to Delete");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie udało się usunąć dane", "Warsztat");
                }
                sql_con.Close();
            }
        }
        
                
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update.Delete();
        }

        private void zakończToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            try
            {
                Update.Finish();               
            }
            catch
            {
                MessageBox.Show("Proszę najpierw kliknąć na <Stwórz nową tabelkę> a później skopiować dane", "Warsztat");
            }
        }

        private void Plan_your_car_Click(object sender, EventArgs e)
        {

           PlanYourCar.Add(this, ID);
           Load_Scheduled_Cars();
        }
        private void Load_Scheduled_Cars()
        {
            PlanYourCar.Load(this);
        }

        private void Update_Scheduled_Cars_Click(object sender, EventArgs e)
        {
            Load_Scheduled_Cars();
        }

        private void Delete_Scheduled_Car_Click(object sender, EventArgs e)
        {
            PlanYourCar.Read_ID_Scheduled_Cars(ID);
            Warsztat.Clear(this, ID);
            Load_Scheduled_Cars();
        }

        private void Scheduled_Cars_View_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(Scheduled_Cars_View.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Wybrana Kolumna jest pusta");
            }
        }

        private void stwórzTabelkęZaplanowaneSamochodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("123");
                Update.Create_Scheduled_Cars();
            }
            catch
            {
                MessageBox.Show("Tabelka już dawno stworzona");
            }
        }

        private void IDNadwozia_TextChanged(object sender, EventArgs e)
        {         
           Interface.BodyNumberVerify(this);
        }

        private void txtZlecenie_Klienta_TextChanged(object sender, EventArgs e)
        {
            Interface.Zlecenie(this);
        }

        private void Scheduled_Cars_View_DoubleClick(object sender, EventArgs e)
        {
            PlanYourCar.ReadData(this, ID);
            Work_Place.SelectTab(tabPage2);
        }

        //Create Order
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePDF.PreviewPDF(this);
        }

        // Update Order
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePDF.UpdatePDF(this);
        }

        private void viewAllPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "pdf\\");
        }

        private void ZlecenieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GeneratePDF.DeletePDF(this);
        }

        //Create Invoice
        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GeneratePDF.Create_Invoice(this);
        }
        // Update Invoice
        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_Form b2 = new Setting_Form();
            b2.ShowDialog();            
        }
    }
}