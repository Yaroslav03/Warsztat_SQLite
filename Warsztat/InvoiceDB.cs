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
        Warsztat Warsztat = new Warsztat();

        int ID, id;

        string  InvoiceNR;

        #region Function
        //create a Invoice Number
        public void CreateInvoiceNR(Form1 form)
        {
            int actualYear = DateTime.Now.Year;
            string olddate = form.InvoiceDataGridView.CurrentRow.Cells["TerminZaplaty_Invoice"].Value.ToString();
            string idStr = form.InvoiceDataGridView.CurrentRow.Cells["FakturaNr_Invoice"].Value.ToString();

            id = Convert.ToInt32(idStr.Substring(0,2));
            int newYear = Convert.ToInt32(olddate.Substring(0, 4));
            if (actualYear == newYear)
            {
                id++;
                InvoiceNR = id + "/" + newYear;
                form.FakturaNrtxt.Text = InvoiceNR;
            }
            else
            {
                id = 0;
                InvoiceNR = id + "/" + newYear;
                form.FakturaNrtxt.Text = InvoiceNR;
            }
        }

        //Load data from DB
        public void Load(Form1 form)
        {
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                string Load = "SELECT ID, FakturaNr, Name, NIP, Adres, Usluga1, Usluga2, Usluga3, Usluga4, " +
                    "KosztUslugi1, KosztUslugi2, KosztUslugi3, KosztUslugi4, CenaKoncowa, SposobPlatnosci, Konto, Bank, TerminZaplaty FROM Faktura";
                DB = new SQLiteDataAdapter(Load, conn);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                form.InvoiceDataGridView.DataSource = DT;
                conn.Close();                                   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }

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

            form.InvoiceDataGridView.Columns["Konto_Invoice"].Visible = false;
            form.InvoiceDataGridView.Columns["Bank_Invoice"].Visible = false;
        }
        //Read data from DB
        public void Read(Form1 form)
        {
            var invoice = form.InvoiceDataGridView.CurrentRow.Cells;

                ID = Convert.ToInt32(invoice["ID_Invoice"].Value.ToString());
                
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

                form.KontoInvoice.Text = invoice["Konto_Invoice"].Value.ToString();

            form.Button_Save_Invoice.Enabled = false;

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
        public void Update(Form1 form)
        {
            string Update = "UPDATE Faktura set FakturaNr = @FakturaNr, Name = @Name, NIP = @NIP, Adres = @Adres, Usluga1 = @Usluga1, Usluga2 = @Usluga2, Usluga3 = @Usluga3, Usluga4 = @Usluga4, KosztUslugi1 = @KosztUslugi1, " +
                "KosztUslugi2 = @KosztUslugi2, KosztUslugi3 = @KosztUslugi3, KosztUslugi4 = @KosztUslugi4, SposobPlatnosci = @SposobPlatnosci,TerminZaplaty = @TerminZaplaty, Konto = @Konto, Bank = @Bank, CenaKoncowa = @CenaKoncowa WHERE ID=@ID";
            Console.WriteLine("ID = " + ID);
            try
            {
                ID.ToString();
                conn.Open();
                cmd.CommandText = Update;
                cmd.Parameters.AddWithValue("@ID", ID);
                EditAndSave(form);
                cmd.ExecuteNonQuery();
                conn.Close();
                Load(form);
                MessageBox.Show("Dane zostałe odświeżone", "Baza danych");
                
            }
            catch(Exception ex)
            {

                MessageBox.Show("Nie było wprowadzonego tekstu");
                Console.WriteLine(ex);
            }
            form.Button_Save_Invoice.Enabled = true;
        }
        //Save data from DB
        public void Save(Form1 form)
        {
            string Save = "INSERT INTO Faktura( FakturaNr, Name, NIP, Adres, Usluga1, Usluga2, Usluga3, Usluga4, KosztUslugi1, KosztUslugi2, KosztUslugi3, KosztUslugi4, SposobPlatnosci,TerminZaplaty, Konto, Bank, CenaKoncowa)" +
                "VALUES(@FakturaNr, @Name, @NIP, @Adres, @Usluga1, @Usluga2, @Usluga3, @Usluga4, @KosztUslugi1, @KosztUslugi2, @KosztUslugi3, @KosztUslugi4, @SposobPlatnosci,@TerminZaplaty, @Konto, @Bank, @CenaKoncowa)";
            conn.Close();
            conn.Open();
            cmd = new SQLiteCommand(Save, conn);
            EditAndSave(form);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wszystko zostało zapisane");
            conn.Close();

            Load(form);
        }
        //
        private void EditAndSave (Form1 form)
        {
            GeneratePDF pdf = new GeneratePDF();
            cmd.Parameters.AddWithValue("@FakturaNr", form.FakturaNrtxt.Text);
            cmd.Parameters.AddWithValue("@Name", form.NameORNameCompanyInvoice.Text.Trim());
            cmd.Parameters.AddWithValue("@NIP", form.NipInvoice.Text.Trim());
            cmd.Parameters.AddWithValue("@Adres", form.AdresInvoice.Text.Trim());

            cmd.Parameters.AddWithValue("@Usluga1", form.Service1Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@Usluga2", form.Service2Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@Usluga3", form.Service3Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@Usluga4", form.Service4Invoice.Text.Trim());

            cmd.Parameters.AddWithValue("@KosztUslugi1", form.PriceService1Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@KosztUslugi2", form.PriceService2Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@KosztUslugi3", form.PriceService3Invoice.Text.Trim());
            cmd.Parameters.AddWithValue("@KosztUslugi4", form.PriceService4Invoice.Text.Trim());

            if (form.Card.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SposobPlatnosci", "przelew");
            }
            else if (form.Cash.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SposobPlatnosci", "gotówka");
            }

            if (form.todayInvoice.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TerminZaplaty", form.dateTimePickerInvoice.Text);
            }
            else if (form.forTimeInvoice.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TerminZaplaty", form.DateOfPayInvoice.Text);
            }

            cmd.Parameters.AddWithValue("@Konto", form.KontoInvoice.Text.Trim());
            cmd.Parameters.AddWithValue("@Bank", form.InBankInvoice.Text.Trim());
            cmd.Parameters.AddWithValue("@CenaKoncowa", form.PriceFinall.Text);
        }
        //Remove data from DB
        public void Remove(Form1 form)
        {
            ID = Convert.ToInt32(form.InvoiceDataGridView.CurrentRow.Cells["ID_Invoice"].Value.ToString());

            conn.Open();
            try
            {
                //Дані до опису Транспорту
                if (ID != 0)
                {
                    cmd = new SQLiteCommand("DELETE FROM Faktura WHERE ID =@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                    Interface.ClearPart(form, form.panel11.Controls);
                    Interface.ClearPart(form, form.panel10.Controls);
                    Interface.ClearPart(form, form.panel12.Controls);
                    Interface.ClearPart(form, form.panel9.Controls);
                }
                else
                {
                    MessageBox.Show("Proszę wybrać kolumnę do Usunięcia");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się usunąć dane ", "Warsztat");
            }
            conn.Close();
            Load(form);
        }
        #endregion
        #region Function for this class

        private void CashCard(Form1 form)
        {
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
        
        public void CalculatePrice (Form1 form)
        {
            try
            {
                int a = Convert.ToInt32(form.PriceService1Invoice.Text);
                int b = Convert.ToInt32(form.PriceService2Invoice.Text);
                int c = Convert.ToInt32(form.PriceService3Invoice.Text);
                int d = Convert.ToInt32(form.PriceService4Invoice.Text);

                int x = a + b + c + d;
                form.PriceFinall.Text = x.ToString();
            }
            catch //fix when text box have a empty string
            {}
        }
        #endregion
    }
}
