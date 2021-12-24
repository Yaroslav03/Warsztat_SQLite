﻿using System;
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
        private SQLiteDataAdapter DB = new SQLiteDataAdapter();
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public string Jazda;
        public string Kluczyki;
        public string Dokumenty;
        int ID = 0;
        string ConnectionString = "Data Source=Warsztat.db;Version=3;New=False;Compress=True;";

        //підключаємося до іншого класу створюючи конструктур
        private Verify_DB verify_DB = new Verify_DB();
        private Copy_DB copy = new Copy_DB();
        public Form1()
        {
            
            InitializeComponent();
            sql_conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
            Load_DB();
            verify_DB.Verify();
           

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                sql_conn.Open();
                DB = new SQLiteDataAdapter("SELECT * from Dane WHERE Marka LIKE '" + txtSearch.Text + "%' OR Model Like '" + txtSearch.Text + "%'", sql_conn);
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
            ID.ToString();
            sql_conn.Open();
            sql_cmd.CommandText = "UPDATE Dane set Marka=@Marka, Model=@Model, NumerRejestracji=@NumerRejestracji, RokProdukcji=@RokProdukcji, PojemnoscSilnika=@PojemnoscSilnika, Przebieg=@Przebieg, NumerNadwozia=@NumerNadwozia, IDSilnika=@IDSilnika, Imie=@Imie, Nazwisko=@Nazwisko, NIP=@NIP, Telefon=@Telefon, Adres=@Adres, ZlecenieKlienta=@ZlecenieKlienta, Diagnostyka=@Diagnostyka, Naprawa=@Naprawa, Koszt=@Koszt, DataPrzyjecia=@DataPrzyjecia, TestDrive=@TestDrive, PozostawioneKluczyki=@PozostawioneKluczyki, PozostawioneDokumenty=@PozostawioneDokumenty WHERE ID=@ID";
            sql_cmd.Parameters.AddWithValue("@ID", ID);
            Edit_AND_Save();
            sql_cmd.ExecuteNonQuery();
            sql_conn.Close();
            Load_DB();
            MessageBox.Show("Dane zostałe odświeżone", "Baza danych");

            tabControl1.SelectTab(tabPage1);
            Clear();
            Button_Save.Enabled = true;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
            Button_Save.Enabled = true;
        }

        private void Change_Word_Click(object sender, EventArgs e)
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
                { "<Kluczyki_TAK/NIE>", Kluczyki }
            };
            helper.Process(items);

            DialogResult Finish;
                Finish = MessageBox.Show("Prosze oczekiwac na wydruk?", "Warsztat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Finish == DialogResult.OK)
            {
                tabControl1.SelectTab(tabPage1);
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
                "INSERT INTO Dane(Model, Marka, NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt, DataPrzyjecia, DataWydania, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty) " +
                "VALUES(@Model, @Marka, @NumerRejestracji, @RokProdukcji, @PojemnoscSilnika, @Przebieg, @NumerNadwozia, @IDSilnika, @Imie, @Nazwisko, @NIP, @Telefon, @Adres, @ZlecenieKlienta, @Diagnostyka, @Naprawa, @Koszt, @DataPrzyjecia, @DataWydania, @TestDrive, @PozostawioneKluczyki, @PozostawioneDokumenty)";
            //Запис Основної Інформації завдяки Parameters

            Edit_AND_Save(); //Беремо звідти команди виконнання 
            sql_cmd.ExecuteNonQuery();
            MessageBox.Show("Wszystko zostało zapisane");
            sql_conn.Close();
            tabControl1.SelectTab(tabPage1);
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
            sql_cmd.Parameters.AddWithValue("@Koszt", Price.Text);
            sql_cmd.Parameters.AddWithValue("@DataPrzyjecia", DataPrzyjecia.Text);
            sql_cmd.Parameters.AddWithValue("@DataWydania", DataWydania.Text);


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
            //Zlecenie Klienta
            txtZlecenie_Klienta.Text = "";
            //Diagnostyka
            txtDiagostyka.Text = "";
            // Naprawa
            txtNaprawa.Text = Price.Text = "";
            //Дані додаткові 
            TestDrive.Checked = LeftDocumets.Checked = LeftKey.Checked = false;
            Price.Text = "";

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

             var jazda_ = Dane_Warsztat.CurrentRow.Cells[20].Value.ToString();
             TestDrive.Checked = jazda_ == "Tak";
             TestDrive.Checked = !(jazda_ == "Nie");

             var kluczyki_ = Dane_Warsztat.CurrentRow.Cells[21].Value.ToString();
             LeftKey.Checked = kluczyki_ == "Tak";
             LeftKey.Checked = !(kluczyki_ == "Nie");

             var dokumenty_ = Dane_Warsztat.CurrentRow.Cells[22].Value.ToString();
             LeftDocumets.Checked = dokumenty_ == "Tak";
             LeftDocumets.Checked = !(dokumenty_ == "Nie");

           
            sql_conn.Close();
            Button_Delete.Enabled = Enabled;
        }

        private void Dane_Warsztat_DoubleClick(object sender, EventArgs e)
        {
            Read_Data_BD();
            tabControl1.SelectTab(tabPage2);
            Button_Save.Enabled = false;
        }       
        private void Dane_Warsztat_MouseClick(object sender, MouseEventArgs e)
        {
            //Конвертує в число
            ID = Convert.ToInt32(Dane_Warsztat.CurrentRow.Cells[0].Value.ToString());
            Read_Data_BD();
        }
        private void polskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ustawieniaToolStripMenuItem.Text = "Ustawienia";
            językToolStripMenuItem.Text = "Język";
            polskiToolStripMenuItem.Text = "Polski";
            ukraińskiToolStripMenuItem.Text = "Ukraiński";
            angielskiToolStripMenuItem.Text = "Angielski";
            //Кнопки
            Button_Save.Text = "Zapisz";
            tabPage2.Text = "Dodaj Dane";
            Button_Clear.Text = "Wyczyść";
            Button_Update.Text = "Odśwież Dane";
            Button_Delete.Text = "Usuń";
            Change_Word.Text = "Drukuj";
            tabPage1.Text = "Zobacz Baze Danych";
            Button_Clear_DK.Text = "Wyczyść";
            Button_Clear_OP.Text = "Wyczyść";
            Button_Clear_Naprawa.Text = "Wyczyść";
            Button_Clear_Zlecenie_Klienta.Text = "Wyczyść";
            Button_Clear_Diagnostyka.Text = "Wyczyść";
            //Текст
            lngMarka.Text = "Marka";
            lngModel.Text = "Model";
            lngRegistrationNumber.Text = "Numer Rejestracji";
            lngYearOfProduction.Text = "Rok Produkcji";
            lngLastName.Text = "Nazwisko";
            lngSearch.Text = "Szukaj";
            lngAdress.Text = "Adres";
            lngVehicleDescription.Text = "Opis Pojazdu";
            lngCustomerData.Text = "Dane Klienta";
            lngMileage.Text = "Przebieg";
            lngEngineCapacity.Text = "Pojemność Silnika";
            lngBodyNumber.Text = "Numer Nadwozia";
            lngName.Text = "Imię";
            lngNIP.Text = "NIP";
            lngTelephon.Text = "Telefon Komurkowy";
            lngAdditionalInformation.Text = "Informacja Dodatkowa";
            lngDateOfAdmission.Text = "Data Przyjęcia";
            lngReleaseDate.Text = "Data Wydania";

            lngClientsOrder.Text = "Zlecenie Klienta";
            lngDiagnostics.Text = "Diagnostyka";
            lngRepair.Text = "Naprawa";
            LeftDocumets.Text = "Pozostawione dokumenty samochodu";
            LeftKey.Text = "Pozostawione kluczyki";
            TestDrive.Text = "Klient wyraża zgodę na jazdę próbną";
            lngPrice.Text = "Koszt Szacunkowy";
        }

        private void ukraińskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ustawieniaToolStripMenuItem.Text = "Налаштування";
            językToolStripMenuItem.Text = "Мова";
            polskiToolStripMenuItem.Text = "Польська";
            ukraińskiToolStripMenuItem.Text = "Українська";
            angielskiToolStripMenuItem.Text = "Англійська";
            //Кнопки
            Button_Save.Text = "Зберегти";
            tabPage2.Text = "Додати дані";
            Button_Clear.Text = "Стерти";
            Button_Update.Text = "Оновити Дані";
            Button_Delete.Text = "Видалити";
            Change_Word.Text = "Друкувати";
            tabPage1.Text = "Переглянути базу даних";
            Button_Clear_DK.Text = "Стерти";
            Button_Clear_OP.Text = "Стерти";
            Button_Clear_Naprawa.Text = "Стерти";
            Button_Clear_Zlecenie_Klienta.Text = "Стерти";
            Button_Clear_Diagnostyka.Text = "Стерти";
            //Текст
            lngMarka.Text = "Марка";
            lngModel.Text = "Модель";
            lngRegistrationNumber.Text = "Номер Реєстрації";
            lngYearOfProduction.Text = "Рок продукції";
            lngLastName.Text = "Фамілія";
            lngSearch.Text = "Пошук";
            lngAdress.Text = "Адрес";
            lngVehicleDescription.Text = "Опис транспортного засобу";
            lngCustomerData.Text = "Дані клієнта";
            lngMileage.Text = "Пробіг";
            lngEngineCapacity.Text = "Об'єм двигуна";
            lngBodyNumber.Text = "Номер кузова";
            lngName.Text = "Ім'я";
            lngNIP.Text = "ІД";
            lngTelephon.Text = "Номер Телефону";
            lngAdditionalInformation.Text = "Інформація Додаткова";
            lngDateOfAdmission.Text = "Дата прийняття";
            lngReleaseDate.Text = "Дата випуску";

            lngClientsOrder.Text = "Замовлення клієнта";
            lngDiagnostics.Text = "Діагностика";
            lngRepair.Text = "Ремонт";
            LeftDocumets.Text = "залишені документи транспортного засобу";
            LeftKey.Text = "Залишені ключі";
            TestDrive.Text = "Клієнт дає згоду на  пробну їзду";
            lngPrice.Text = "Ціна";
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

        private void zróbKopięDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                copy.Update_DB();
                
            }
            catch
            {
                MessageBox.Show("Tabelka już dawno stworzona", "Warsztat");
            }
           
            Application.Restart();
        }
    }
}