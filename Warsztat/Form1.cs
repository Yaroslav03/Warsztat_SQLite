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
    public partial class Form1 : System.Windows.Forms.Form
    {
        private SQLiteConnection sql_conn;
        private SQLiteCommand sql_cmd = new SQLiteCommand();
        SQLiteDataAdapter DB = new SQLiteDataAdapter();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        public string Jazda;
        public string Kluczyki;
        public string Dokumenty;
        public string nothing;
        int ID = 0;
        string ConnectionString = "Data Source=Warsztat.db;Version=3;New=False;Compress=True;";
        //підключаємося до іншого класу створюючи конструктур
        private Copy_DB copy = new Copy_DB();
        //private Interface Interface = new Interface();
       Interface Interface ;

        public Form1()
        {          
            InitializeComponent();
            sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            Load_DB();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                sql_conn.Open();
                DB = new SQLiteDataAdapter("SELECT * from Dane WHERE Marka LIKE '" + txtSearch.Text + "%' OR Model Like '" + txtSearch.Text + "%' OR NumerNadwozia Like '" + txtSearch.Text + "%'", sql_conn);
                DT = new DataTable();
                DB.Fill(DT);
                Dane_Warsztat.DataSource = DT;
                sql_conn.Close();
            }
            else if (txtSearch.Text != null)
            {
                Load_DB();
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
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
                        Clear();
                        Load_DB();
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

        private void Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Save_Data();
                Clear();
            }
            catch
            {
                MessageBox.Show("Probłem z podłączeniem do bazy danych bądź z zapisem danych", "Baza danych");
            }
            Load_DB();
        }

        private void Button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                ID.ToString();
                sql_conn.Open();
                sql_cmd.CommandText = "UPDATE Dane set Marka=@Marka, Model=@Model, NumerRejestracji=@NumerRejestracji, RokProdukcji=@RokProdukcji, PojemnoscSilnika=@PojemnoscSilnika, Przebieg=@Przebieg, NumerNadwozia=@NumerNadwozia, IDSilnika=@IDSilnika, Imie=@Imie, Nazwisko=@Nazwisko, NIP=@NIP, Telefon=@Telefon, Adres=@Adres, ZlecenieKlienta=@ZlecenieKlienta, Diagnostyka=@Diagnostyka, Naprawa=@Naprawa, Koszt_Koncowy=@Koszt_Koncowy, Koszt_Szacunkowy=@Koszt_Szacunkowy, DataPrzyjecia=@DataPrzyjecia, DataWydania=@DataWydania, TestDrive=@TestDrive, PozostawioneKluczyki=@PozostawioneKluczyki, PozostawioneDokumenty=@PozostawioneDokumenty, Zakupione_Czesci=@Zakupione_Czesci  WHERE ID=@ID";
                sql_cmd.Parameters.AddWithValue("@ID", ID);
                Edit_AND_Save();
                sql_cmd.ExecuteNonQuery();
                sql_conn.Close();
                Load_DB();
                MessageBox.Show("Dane zostałe odświeżone", "Baza danych");

                Work_Place.SelectTab(tabPage1);
                Clear();
                Button_Save.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Nie było wprowadzonego tekstu");
            }
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
            Button_Save.Enabled = true;
        }

        private async void Change_Word_Click(object sender, EventArgs e)
        {
            DialogResult iPrint;
            iPrint = MessageBox.Show("Czy zapisać dane przed drukowaniem?", "Warsztat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (iPrint == DialogResult.Yes)
            {

                Save_Data();
                Load_DB();
            }
            var helper = new Word("Print_Document.docx");
            var items = new Dictionary<string, string>
            {
                //Дані клієнта

                { "<Imię>",_Name.Text },
                { "<Nazwisko>", LastName.Text},
                { "<TelefonKomurkowy>", TelefonKomurkowy.Text},
                { "<Adres>", Adress.Text},
                { "<NIP>", NIP.Text },

                //Дані транспорту

                { "<Marka>", Marka.Text},
                { "<Model>", Model.Text},
                { "<RokProdukcji>", RokProdukcji.Text},
                { "<NumerNadwozia>", IDNadwozia.Text},
                { "<NumerRejestracji>", NumerRejestracji.Text},
                { "<PojemnośćSilnika>", PojemnoscSilnika.Text},
                { "<Przebieg>", Przebieg.Text},
                { "<KodSilnika>", IDSilnika.Text},

                //Стан технічний траспорту з опису слів клієнта

                { "<Zlecenie>", txtZlecenie_Klienta.Text},
                //Ремонт
                {"<Naprawa>", txtNaprawa.Text},
                //Інформація додаткова
                { "<DataPryjęcia>", DataPrzyjecia.Text},
                { "<DataWydania>", DataWydania.Text},
                { "<PRICE>", Price.Text},
                { "<Jazda_TAK/NIE>", Jazda},
                { "<Pozostawione>", Dokumenty},
                { "<Kluczyki_TAK/NIE>", Kluczyki },
                { "< ZakupioneCzęści>", txtZakupione_Czesci.Text}
            };
            helper.Process(items);

            DialogResult Finish;
                Finish = MessageBox.Show("Prosze oczekiwac na wydruk?", "Warsztat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Finish == DialogResult.OK)
            {
                Work_Place.SelectTab(tabPage1);
            }
        }

        private void Button_Clear_OP_Click(object sender, EventArgs e)
        {
            ClearPart(panel1.Controls);
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
            ClearPart(panel3.Controls);
        }

        private void Button_Clear_Naprawa_Click(object sender, EventArgs e)
        {
            txtNaprawa.Text = "";
        }
        private void Button_Clear_Zakupione_Czesci_Click(object sender, EventArgs e)
        {
            txtZakupione_Czesci.Text = "";
        }

        private void Load_DB()
        {
            sql_conn.Open();
            sql_cmd = sql_conn.CreateCommand();
            string CommandText = "select * from Dane";
            DB = new SQLiteDataAdapter(CommandText, sql_conn);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            Dane_Warsztat.DataSource = DT;
            Dane_Warsztat.Columns[0].Visible = false;
            sql_conn.Close();
        }
        
        private void Save_Data()
        {
            sql_conn.Open();
            //Створення процедури
            sql_cmd.CommandText =
                "INSERT INTO Dane(Model, Marka, NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, DataPrzyjecia, DataWydania, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci) " +
                "VALUES(@Model, @Marka, @NumerRejestracji, @RokProdukcji, @PojemnoscSilnika, @Przebieg, @NumerNadwozia, @IDSilnika, @Imie, @Nazwisko, @NIP, @Telefon, @Adres, @ZlecenieKlienta, @Diagnostyka, @Naprawa, @Koszt_Szacunkowy, @Koszt_Koncowy, @DataPrzyjecia, @DataWydania, @TestDrive, @PozostawioneKluczyki, @PozostawioneDokumenty, @Zakupione_Czesci)";
            //Запис Основної Інформації завдяки Parameters
            Edit_AND_Save(); //Беремо звідти команди виконнання 
            sql_cmd.ExecuteNonQuery();
            MessageBox.Show("Wszystko zostało zapisane");
            sql_conn.Close();
            Console.WriteLine("Sql close");
            Work_Place.SelectTab(tabPage1);
        }
        private void Edit_AND_Save() // Тут містяться параметри для sql
        {
            sql_cmd.Parameters.AddWithValue("@Model", Model.Text);
            sql_cmd.Parameters.AddWithValue("@Marka", Marka.Text);
            sql_cmd.Parameters.AddWithValue("@NumerRejestracji", NumerRejestracji.Text);
            sql_cmd.Parameters.AddWithValue("@RokProdukcji", RokProdukcji.Text);

            sql_cmd.Parameters.AddWithValue("@PojemnoscSilnika", PojemnoscSilnika.Text);
            sql_cmd.Parameters.AddWithValue("@Przebieg", Przebieg.Text);
            sql_cmd.Parameters.AddWithValue("@NumerNadwozia", IDNadwozia.Text);
            sql_cmd.Parameters.AddWithValue("@IDSilnika", IDSilnika.Text);

            sql_cmd.Parameters.AddWithValue("@Imie", _Name.Text);
            sql_cmd.Parameters.AddWithValue("@Nazwisko", LastName.Text);
            sql_cmd.Parameters.AddWithValue("@NIP", NIP.Text);
            sql_cmd.Parameters.AddWithValue("@Telefon", TelefonKomurkowy.Text);

            sql_cmd.Parameters.AddWithValue("@Adres", Adress.Text);
            sql_cmd.Parameters.AddWithValue("@ZlecenieKlienta", txtZlecenie_Klienta.Text);
            sql_cmd.Parameters.AddWithValue("@Diagnostyka", txtDiagostyka.Text);

            sql_cmd.Parameters.AddWithValue("@Naprawa", txtNaprawa.Text);
            sql_cmd.Parameters.AddWithValue("@Koszt_Szacunkowy", Price.Text);
            sql_cmd.Parameters.AddWithValue("@DataPrzyjecia", DataPrzyjecia.Text);
             if (lngReleaseDate.Checked == true)
             {
                Console.WriteLine("Data Wyadania Text");
                sql_cmd.Parameters.AddWithValue("@DataWydania", DataWydania.Text);
             }
              else if(lngReleaseDate.Checked == false)
             {
                Console.WriteLine("Data wydania !text");
                sql_cmd.Parameters.AddWithValue("@DataWydania", nothing = "");
             }
            sql_cmd.Parameters.AddWithValue("@Koszt_Koncowy", Price_Finally.Text); 
            sql_cmd.Parameters.AddWithValue("@Zakupione_Czesci", txtZakupione_Czesci.Text);

            //Інформація додаткова
            //Пробний пробіг
            if (TestDrive.Checked == true)
                {
                    sql_cmd.Parameters.AddWithValue("@TestDrive", Jazda = "Tak");
                }
                else if (TestDrive.Checked == false)
                {
                    sql_cmd.Parameters.AddWithValue("@TestDrive", Jazda = "Nie");
                }
                //Залишені ключі
                if (LeftKey.Checked == true)
                {
                    sql_cmd.Parameters.AddWithValue("@PozostawioneKluczyki", Kluczyki = "Tak");
                }
                else if (LeftKey.Checked == false)
                {
                    sql_cmd.Parameters.AddWithValue("@PozostawioneKluczyki", Kluczyki = "Nie");
                }
                //Залишені документи
                if (LeftDocumets.Checked == true)
                {
                    sql_cmd.Parameters.AddWithValue("@PozostawioneDokumenty", Dokumenty = "Tak");
                }
                else if (LeftDocumets.Checked == false)
                {
                    sql_cmd.Parameters.AddWithValue("@PozostawioneDokumenty", Dokumenty = "Nie");
                }        
        }    
        void Clear()
        {
            //Opis pojazdu 
            ClearPart(panel1.Controls);
            //Dane klienta
            ClearPart(panel3.Controls);
            // Naprawa           Diagnostyka            Zlecenie Klienta            Zakupione Czesci
            txtNaprawa.Text = txtDiagostyka.Text = txtZlecenie_Klienta.Text = txtZakupione_Czesci.Text = "";
            //Дані додаткові 
            TestDrive.Checked = LeftDocumets.Checked = LeftKey.Checked = false;
            Price.Text = Price_Finally.Text = "";

            ID = 0;
            Button_Save.Text = "Zapisz";
            Button_Delete.Enabled = false;
        }

        private void ClearPart(Control.ControlCollection controlCollection)
        {
            try
            {
                foreach (Control c in controlCollection)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                    else if (c is RichTextBox)
                        ((RichTextBox)c).Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Read_Data_BD()
        {
            sql_conn.Open();

            ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells[0].Value.ToString());
            DataPrzyjecia.Text = Dane_Warsztat.CurrentRow.Cells[1].Value.ToString();
            DataWydania.Text = Dane_Warsztat.CurrentRow.Cells[2].Value.ToString();

             Marka.Text = Dane_Warsztat.CurrentRow.Cells[3].Value.ToString();
             Model.Text = Dane_Warsztat.CurrentRow.Cells[4].Value.ToString();
             NumerRejestracji.Text = Dane_Warsztat.CurrentRow.Cells[5].Value.ToString();

             RokProdukcji.Text = Dane_Warsztat.CurrentRow.Cells[6].Value.ToString();
             PojemnoscSilnika.Text = Dane_Warsztat.CurrentRow.Cells[7].Value.ToString();
             Przebieg.Text = Dane_Warsztat.CurrentRow.Cells[8].Value.ToString();

             IDNadwozia.Text = Dane_Warsztat.CurrentRow.Cells[9].Value.ToString();
             IDSilnika.Text = Dane_Warsztat.CurrentRow.Cells[10].Value.ToString();
             _Name.Text = Dane_Warsztat.CurrentRow.Cells[11].Value.ToString();

             LastName.Text = Dane_Warsztat.CurrentRow.Cells[12].Value.ToString();
             NIP.Text = Dane_Warsztat.CurrentRow.Cells[13].Value.ToString();
             TelefonKomurkowy.Text = Dane_Warsztat.CurrentRow.Cells[14].Value.ToString();

             Adress.Text = Dane_Warsztat.CurrentRow.Cells[15].Value.ToString();
             txtZlecenie_Klienta.Text = Dane_Warsztat.CurrentRow.Cells[16].Value.ToString();
             txtDiagostyka.Text = Dane_Warsztat.CurrentRow.Cells[17].Value.ToString();

             txtNaprawa.Text = Dane_Warsztat.CurrentRow.Cells[18].Value.ToString();
             Price.Text = Dane_Warsztat.CurrentRow.Cells[19].Value.ToString();
             Price_Finally.Text = Dane_Warsztat.CurrentRow.Cells[20].Value.ToString();

             var jazda_ = Dane_Warsztat.CurrentRow.Cells[21].Value.ToString();
             TestDrive.Checked = jazda_ == "Tak";
             TestDrive.Checked = !(jazda_ == "Nie");

             var kluczyki_ = Dane_Warsztat.CurrentRow.Cells[22].Value.ToString();
             LeftKey.Checked = kluczyki_ == "Tak";
             LeftKey.Checked = !(kluczyki_ == "Nie");

             var dokumenty_ = Dane_Warsztat.CurrentRow.Cells[23].Value.ToString();
             LeftDocumets.Checked = dokumenty_ == "Tak";
             LeftDocumets.Checked = !(dokumenty_ == "Nie");

            txtZakupione_Czesci.Text = Dane_Warsztat.CurrentRow.Cells[24].Value.ToString();
           
            sql_conn.Close();
            Button_Delete.Enabled = Enabled;
        }

        private void Dane_Warsztat_DoubleClick(object sender, EventArgs e)
        {
            Read_Data_BD();
            Work_Place.SelectTab(tabPage2);
            Button_Save.Enabled = false;
        }       
        private void Dane_Warsztat_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Конвертує в число
                ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells[0].Value.ToString());
                Read_Data_BD();
            }
            catch 
            {
                MessageBox.Show("Wybrana Kolumna jest pusta");
            }
        }
        private void polskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interface.Localize_PL();
        }

        private void ukraińskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interface.Localize_UA();
        }
        private void Verify_Button()
        {
            Marka.BackColor = String.IsNullOrEmpty(Marka.Text) ? Color.Red : Color.White;
            Model.BackColor = String.IsNullOrEmpty(Model.Text)? Color.Red : Color.White;
            _Name.BackColor = String.IsNullOrEmpty(_Name.Text)? Color.Red : Color.White;
            NumerRejestracji.BackColor = String.IsNullOrEmpty(NumerRejestracji.Text) ? Color.Red : Color.White;
            TelefonKomurkowy.BackColor = String.IsNullOrEmpty(TelefonKomurkowy.Text) ? Color.Red : Color.White;
        }

        private void Marka_TextChanged(object sender, EventArgs e)
        {
            Verify_Button();
        }

        private void Model_TextChanged(object sender, EventArgs e)
        {
            Verify_Button();   
        }

        private void NumerRejestracji_TextChanged(object sender, EventArgs e)
        {
            Verify_Button();    
        }

        private void _Name_TextChanged(object sender, EventArgs e)
        {
            Verify_Button();
        }

        private void TelefonKomurkowy_TextChanged(object sender, EventArgs e)
        {
            Verify_Button();    
        }
   
        private void stwórzNowąTabelkęToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Load_To_Copy_Old_Tabel();
            copy.Create_New_DB();
            Load_To_Copy_NEW_Tabel();
        }

        private void kopjujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sql_conn.Open();
            Save_data_old();
            sql_cmd.ExecuteNonQuery();
            sql_conn.Close();
            Load_To_Copy_NEW_Tabel();
            Load_To_Copy_Old_Tabel();
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
                        Clear();
                        Load_To_Copy_Old_Tabel(); 
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
        private void Load_To_Copy_Old_Tabel()
        {
            sql_conn.Open();
            sql_cmd = sql_conn.CreateCommand();
            string CommandText = "select * from Dane";
            DB = new SQLiteDataAdapter(CommandText, sql_conn);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            OLD_Table.DataSource = DT;
            OLD_Table.Columns[0].Visible = false;
            sql_conn.Close();
        }
        private void Load_To_Copy_NEW_Tabel()
        { 
         SQLiteDataAdapter sqlDB = new SQLiteDataAdapter();
         DataSet sqlDS = new DataSet();
         DataTable sqlDT = new DataTable();

        sql_conn.Open();
            sql_cmd = sql_conn.CreateCommand();
            string CommandText = "select * from Dane_New";
            sqlDB = new SQLiteDataAdapter(CommandText, sql_conn);
            sqlDS.Reset();
            sqlDB.Fill(sqlDS);
            sqlDT = sqlDS.Tables[0];
            NEW_Table.DataSource = sqlDT;
            NEW_Table.Columns[0].Visible = false;
            sql_conn.Close();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy.Delete();
        }
        private void Save_data_old()
        {
            try
            {
                sql_cmd.CommandText = "INSERT INTO Dane_New(ID, DataPrzyjecia, DataWydania, Marka, Model,  NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci)" +
                     "VALUES ((SELECT ID FROM Dane),(SELECT DataPrzyjecia From Dane), (''), (SELECT Marka From Dane),(SELECT Model From Dane),(SELECT NumerRejestracji From Dane),(SELECT RokProdukcji From Dane), (SELECT PojemnoscSilnika From Dane),(SELECT Przebieg From Dane),(SELECT NumerNadwozia From Dane),(SELECT IDSilnika From Dane), (SELECT Imie From Dane),(SELECT Nazwisko From Dane),(SELECT NIP From Dane),(SELECT Telefon From Dane),(SELECT Adres From Dane),(SELECT ZlecenieKlienta From Dane),(SELECT Diagnostyka From Dane),(SELECT Naprawa From Dane),(SELECT Koszt_Szacunkowy From Dane),('0'),(SELECT TestDrive From Dane),(SELECT PozostawioneKluczyki From Dane),(SELECT PozostawioneDokumenty From Dane), (''))";
                sql_cmd.Parameters.AddWithValue("@ID", ID);
            }
            catch
            {
                sql_cmd.CommandText = "INSERT INTO Dane_New(ID, DataPrzyjecia, DataWydania, Marka, Model,  NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci)" +
                        "VALUES ((SELECT ID FROM Dane),(SELECT DataPrzyjecia From Dane), (''), (SELECT Marka From Dane),(SELECT Model From Dane),(SELECT NumerRejestracji From Dane),(SELECT RokProdukcji From Dane), (SELECT PojemnoscSilnika From Dane),(SELECT Przebieg From Dane),(SELECT NumerNadwozia From Dane),(SELECT IDSilnika From Dane), (SELECT Imie From Dane),(SELECT Nazwisko From Dane),(SELECT NIP From Dane),(SELECT Telefon From Dane),(SELECT Adres From Dane),(SELECT ZlecenieKlienta From Dane),(SELECT Diagnostyka From Dane),(SELECT Naprawa From Dane),(SELECT Koszt From Dane),('0'),(SELECT TestDrive From Dane),(SELECT PozostawioneKluczyki From Dane),(SELECT PozostawioneDokumenty From Dane), (''))";
                sql_cmd.Parameters.AddWithValue("@ID", ID);
            }
        }

        private void OLD_Table_MouseClick(object sender, MouseEventArgs e)
        {
            ID = Convert.ToInt32(OLD_Table.CurrentRow.Cells[0].Value.ToString());
        }

        private void zakończToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            copy.Finish();
            Application.Restart();
        }

        private void Plan_your_car_Click(object sender, EventArgs e)
        {
            
            try
            {
                sql_conn.Open();
                sql_cmd.CommandText =
                "INSERT INTO Zaplanowane_Samochody(DataPrzyjecia,Model, Marka,  Imie, Nazwisko, Zlecenie_Klienta) " +
                "VALUES(@DataPrzyjecia, @Model, @Marka, @Imie, @Nazwisko, @Zlecenie_Klienta)";
                sql_cmd.Parameters.AddWithValue("@Model", Model.Text);
                sql_cmd.Parameters.AddWithValue("@Marka", Marka.Text);
                sql_cmd.Parameters.AddWithValue("@Imie", _Name.Text);
                sql_cmd.Parameters.AddWithValue("@Nazwisko", LastName.Text);
                sql_cmd.Parameters.AddWithValue("@Zlecenie_Klienta", txtZlecenie_Klienta.Text);
                sql_cmd.Parameters.AddWithValue("@DataPrzyjecia", DataPrzyjecia.Text);
                sql_cmd.ExecuteNonQuery();
                sql_conn.Close();
                
                MessageBox.Show("Samochód  "+ Model.Text + " " + Marka.Text + " jest zapłanowany na " + DataPrzyjecia.Text);
                Clear();
            }
            catch
            {
                MessageBox.Show("Probłem z podłączeniem do bazy danych bądź z zapisem danych", "Baza danych");
            }
            
           Load_Scheduled_Cars();
        }
        private void Load_Scheduled_Cars()
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
            Scheduled_Cars_View.DataSource = Scheduled_Cars_DT;
            sql_conn.Close();
        }

        private void Update_Scheduled_Cars_Click(object sender, EventArgs e)
        {
            Load_Scheduled_Cars();
        }

        private void Delete_Scheduled_Car_Click(object sender, EventArgs e)
        {
            copy.Read_ID_Scheduled_Cars(ID);
            Clear();
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
                copy.Create_Scheduled_Cars();
            }
            catch
            {
                MessageBox.Show("Tabelka już dawno stworzona");
            }
        }
    }
}