using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace Warsztat
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Update update = new Update();
        Interface Interface = new Interface();
        Warsztat Warsztat = new Warsztat();
        PlanYourCar PlanYourCar = new PlanYourCar();
        GeneratePDF GeneratePDF = new GeneratePDF();
        InvoiceDB invoiceDB = new InvoiceDB();

        int ID;

        public Form1()
        {          
            InitializeComponent();  
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
            Interface.Clear(this, ID);
            Button_Save.Enabled = true;
        }

        private void Button_Clear_OP_Click(object sender, EventArgs e)
        {
            Interface.ClearPart(this, panel1.Controls);
            YearOfProduction.Text = string.Empty;
        }

        private void Button_Clear_Zlecenie_Klienta_Click(object sender, EventArgs e)
        {
            txtZlecenie_Klienta.Text = string.Empty;
        }

        private void Button_Clear_Diagnostyka_Click(object sender, EventArgs e)
        {
            txtDiagostyka.Text = string.Empty;
        }

        private void Button_Clear_DK_Click(object sender, EventArgs e)
        {
            Interface.ClearPart(this, panel3.Controls);
            TelefonKomurkowy.Text = string.Empty;
        }

        private void Button_Clear_Naprawa_Click(object sender, EventArgs e)
        {
            txtNaprawa.Text = string.Empty;
        }
        private void Button_Clear_Zakupione_Czesci_Click(object sender, EventArgs e)
        {
            txtZakupione_Czesci.Text = string.Empty;
        }

        private void Dane_Warsztat_DoubleClick(object sender, EventArgs e)
        {
            Warsztat.ReadData(this, ID);
            Work_Place.SelectTab(AddData);
            Button_Save.Enabled = false;
        }       
        private void Dane_Warsztat_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells["ID_Column_Main"].Value.ToString());
                Button_Delete.Enabled = Enabled;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wybrana Kolumna jest pusta");
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
            update.Load_To_Copy_Old_Tabel(this);
            update.Create_New_DB();
            update.Load_To_Copy_NEW_Tabel(this);
        }

        private void OLD_Table_MouseClick(object sender, MouseEventArgs e)
        {
            ID = Convert.ToInt32(OLD_Table.CurrentRow.Cells[0].Value.ToString()); 
        }

        private void kopjujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update.Save_data_old(ID);
            update.Load_To_Copy_NEW_Tabel(this);
            update.Load_To_Copy_Old_Tabel(this);
            update.deleteData(this);
        }
                        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update.Delete();
        }

        private void zakończToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                update.Finish();               
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
            Interface.Clear(this, ID);
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
            update.Create_Scheduled_Cars();
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
            Interface.Clear(this, ID);
            PlanYourCar.ReadData(this, ID);
            Work_Place.SelectTab(AddData);
            Button_Save.Enabled = true;
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

            Warsztat.Load_DB(this);
            PlanYourCar.Load(this);
            invoiceDB.Load(this);

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
            update.Create_Faktura();
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