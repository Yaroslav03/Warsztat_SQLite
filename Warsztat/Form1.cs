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
        InvoiceDB invoiceDB = new InvoiceDB();

        SQLiteConnection sql_conn = new SQLiteConnection();
        SQLiteCommand sql_cmd = new SQLiteCommand();


        public Form1()
        {          
            sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            InitializeComponent();
            Warsztat.Load_DB(this);
            PlanYourCar.Load(this);
            invoiceDB.Load(this);

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
            Work_Place.SelectTab(AddData);
            Button_Save.Enabled = false;
        }       
        private void Dane_Warsztat_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Convert to number
                ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells["ID_Column_Main"].Value.ToString());
                Console.WriteLine(ID);
                Warsztat.ReadData(this, ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wybrana Kolumna jest pusta" + ex);
            }
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
                Update.Create_Scheduled_Cars();
            }
            catch
            {
                MessageBox.Show("Tabelka zaplanowane samochody już dawno stworzona");
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
            Work_Place.SelectTab(AddData);
            Button_Save.Enabled = true;
            Warsztat.Clear(this, ID);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePDF.UpdatePDF(this);
        }

        private void viewAllPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "pdf\\");
        }

        //Create Invoice
        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GeneratePDF.PreviewInvoice(this);
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_Form b2 = new Setting_Form();
            b2.ShowDialog();         
        }

        private void stwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePDF.PreviewPDF(this);
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratePDF.DeletePDF(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Interface.Load_Data_Localize(this);

            Interface.BodyNumberVerify(this);
            Interface.Verify_Button(this);
            Interface.Zlecenie(this);

            todayInvoice.Checked = Card.Checked = true;
            FakturaNrMask.Visible = false;
        }

        private void Card_CheckedChanged(object sender, EventArgs e)
        {
            KontoInvoice.Enabled  = InBankInvoice.Enabled = true;
        }

        private void Cash_CheckedChanged(object sender, EventArgs e)
        {
            KontoInvoice.Enabled = InBankInvoice.Enabled = false;
            KontoInvoice.Text = InBankInvoice.Text = string.Empty; 
        }

        private void InvoiceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            invoiceDB.CreateInvoiceNR(this);
        }

        private void Search_Invoice_TextChanged(object sender, EventArgs e)
        {
            invoiceDB.Search(this);
        }


        private void Button_Delete_Invoice_Click(object sender, EventArgs e)
        {
            invoiceDB.Remove(this);
        }
        private void Button_Save_Invoice_Click(object sender, EventArgs e)
        {
            invoiceDB.Save(this);
        }

        private void todayInvoice_CheckedChanged(object sender, EventArgs e)
        {
            Interface.VerefyCheckDateOfPayInvoice(this);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            invoiceDB.CalculatePrice(this);
        }

        private void InvoiceDataGridView_DoubleClick(object sender, EventArgs e)
        {
            invoiceDB.Read(this);
            invoiceDB.CreateInvoiceNR(this);
        }

        private void Button_Update_Invoice_Click(object sender, EventArgs e)
        {
            invoiceDB.Update(this);
        }

        private void stwórzTabelkęFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update.Create_Faktura();
        }

        private void FakturaNrlng_MouseClick(object sender, MouseEventArgs e)
        {
            FakturaNrMask.Visible = true;
        }

        private void FakturaNrMask_Leave(object sender, EventArgs e)
        {
            FakturaNrtxt.Text = FakturaNrMask.Text;
            FakturaNrMask.Visible = false;
        }
    }
}