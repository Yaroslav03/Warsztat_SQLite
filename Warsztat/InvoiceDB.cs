using System;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;


namespace Warsztat
{
    internal class InvoiceDB
    {
        private SQLiteConnection conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
        private SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter DB = new SQLiteDataAdapter();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        Interface Interface = new Interface();
        int id;

        #region Function
        //create a Invoice Number
        public void CreateInvoiceNR(Form1 form)
        {
            int actualYear = DateTime.Now.Year;
            conn.Open();
            string olddate = form.InvoiceDataGridView.CurrentRow.Cells["TerminZaplaty_Invoice"].Value.ToString();
            string idStr = form.InvoiceDataGridView.CurrentRow.Cells["ID_Invoice"].Value.ToString();
            conn.Close();

            id = Convert.ToInt32(idStr);
            int newYear = Convert.ToInt32(olddate.Substring(0, 4));
            if (actualYear == newYear)
            {
                id++;
                string InvoiceNR = id + "/" + newYear;
                form.FakturaNr.Text = InvoiceNR;
            }
            else if (actualYear > newYear)
            {
                id = 0;
                string InvoiceNR = id + "/" + newYear;
                form.FakturaNr.Text = InvoiceNR;
            }
        }

        //Load data from DB
        public void Load(Form1 form)
        {

                try
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    string Load = "SELECT ID, FakturaNr, Name, NIP, Adres," +
                        " Usluga1, Usluga2, Usluga3, Usluga4, " +
                        "KosztUslugi1, KosztUslugi2, KosztUslugi3, KosztUslugi4, " +
                        "CenaKoncowa, SposobPlatnosci, Bank, TerminZaplaty FROM Faktura";
                    DB = new SQLiteDataAdapter(Load, conn);
                    DS.Reset();
                    DB.Fill(DS);
                    DT = DS.Tables[0];
                    form.InvoiceDataGridView.DataSource = DT;

                    //hide unnecessary data
                    form.InvoiceDataGridView.Columns["ID_Invoice"].Visible = false;

                    form.InvoiceDataGridView.Columns["Usluga1_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["Usluga2_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["Usluga3_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["Usluga4_Invoice"].Visible = false;

                    form.InvoiceDataGridView.Columns["KosztUslugi1_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["KosztUslugi2_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["KosztUslugi3_Invoice"].Visible = false;
                    form.InvoiceDataGridView.Columns["KosztUslugi4_Invoice"].Visible = false;

                    form.InvoiceDataGridView.Columns["Bank_Invoice"].Visible = false;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }            
        }
        //Read data from DB
        public void Read(Form1 form, int ID)
        {
            var invoice = form.InvoiceDataGridView.CurrentRow.Cells;

                ID = Convert.ToInt32(invoice[0].Value.ToString());
                
                CashCard(form);
                VerefyDate(form);


                form.NameORNameCompanyInvoice.Text = invoice["Name_Invoice"].Value.ToString();
                form.NipInvoice.Text = invoice["NIP_Invoice"].Value.ToString();
                form.AdresInvoice.Text = invoice["Adres_Invoice"].Value.ToString();
                form.InBankInvoice.Text = invoice["Bank_Invoice"].Value.ToString();

                form.Service1Invoice.Text = invoice["Usluga1_Invoice"].Value.ToString();
                form.Service2Invoice.Text = invoice["Usluga2_Invoice"].Value.ToString();
                form.Service3Invoice.Text = invoice["Usluga3_Invoice"].Value.ToString();
                form.Service4Invoice.Text = invoice["Usluga4_Invoice"].Value.ToString();

                form.PriceService1Invoice.Text = invoice["KosztUslugi1_Invoice"].Value.ToString();
                form.PriceService2Invoice.Text = invoice["KosztUslugi2_Invoice"].Value.ToString();
                form.PriceService3Invoice.Text = invoice["KosztUslugi3_Invoice"].Value.ToString();
                form.PriceService4Invoice.Text = invoice["KosztUslugi4_Invoice"].Value.ToString();
        }
        //Search data from DB
        public void Search(Form1 form)
        {
            var search = form.Search_Invoice.Text.Trim();

            if (form.Search_Invoice.Text != String.Empty)
            {
                conn.Open();
                DB = new SQLiteDataAdapter("SELECT * FROM Faktura WHERE FakturaNr LIKE '" + search + "%' OR NIP LIKE'" + search + "%' OR Name LIKE '" + search + "%'", conn);
                DT = new DataTable();
                DB.Fill(DT);
                form.InvoiceDataGridView.DataSource = DT;
                conn.Close();
            }
            else
            {
                Load(form);
            }
        }
        #endregion
        #region Command DB
        //Update data from DB
        public void Update()
        {

        }
        //Save data from DB
        public void Save()
        {

        }
        //
        private void EditAndSave (Form1 form)
        {
            //cmd.Parameters.AddWithValue("@FakturaNr", InvoiceNR());
        }
        //Remove data from DB
        public void Remove()
        {

        }
        #endregion
        #region Function for this class
        
        private void CashCard(Form1 form)
        {
            string cash = "gotówka";
            string card = "przelew";

            var textBox = form.KontoInvoice.Enabled = form.InBankInvoice.Enabled;
            var invoice = form.InvoiceDataGridView.CurrentRow.Cells;

            string pay = invoice["SposobPlatnosci_Invoice"].Value.ToString().Trim();
            
            if (card == pay)
            {
                form.Card.Checked = textBox = true;
            }
            else
            {
                form.Cash.Checked = true;
                textBox = false;
                form.KontoInvoice.Text = form.InBankInvoice.Text = string.Empty;
            }
        }
        private void VerefyDate(Form1 form)
        {
            string date = form.InvoiceDataGridView.CurrentRow.Cells["TerminZaplaty_Invoice"].Value.ToString().Trim();

            if (date.Length == 10)
            {
                form.todayInvoice.Checked = true;

                Interface.VerefyCheckDateOfPayInvoice(form);
                                                       
                form.dateTimePickerInvoice.Text = date;
            }
            else
            {
                form.forTimeInvoice.Checked = true;
                
                Interface.VerefyCheckDateOfPayInvoice(form);

                form.DateOfPayInvoice.Text = date;
            }
            
        }
        #endregion
    }
}
