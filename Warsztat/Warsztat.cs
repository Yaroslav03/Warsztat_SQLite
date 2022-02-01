using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace Warsztat
{
    internal class Warsztat
    {
        private SQLiteConnection conn = new SQLiteConnection("Data Source=Warsztat.db;Version=3;New=False;Compress=True;");
        private SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter DB = new SQLiteDataAdapter();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();

        public void Search(Form1 form)
        {
            if (form.txtSearch.Text != "")
            {
                conn.Open();
                DB = new SQLiteDataAdapter("SELECT * from Dane WHERE Marka LIKE '" + form.txtSearch.Text + "%' OR Model Like '" + form.txtSearch.Text + "%' OR NumerNadwozia Like '" + form.txtSearch.Text + "%'", conn);
                DT = new DataTable();
                DB.Fill(DT);
                form.Dane_Warsztat.DataSource = DT;
                conn.Close();
            }
            else if (form.txtSearch.Text != null)
            {
                Load_DB(form);
            }
        }
        public void Update(Form1 form, int ID)
        {
            try
            {
                ID.ToString();
                conn.Open();
                cmd.CommandText = "UPDATE Dane set Marka=@Marka, Model=@Model, NumerRejestracji=@NumerRejestracji, RokProdukcji=@RokProdukcji, PojemnoscSilnika=@PojemnoscSilnika, Przebieg=@Przebieg, NumerNadwozia=@NumerNadwozia, IDSilnika=@IDSilnika, Imie=@Imie, Nazwisko=@Nazwisko, NIP=@NIP, Telefon=@Telefon, Adres=@Adres, ZlecenieKlienta=@ZlecenieKlienta, Diagnostyka=@Diagnostyka, Naprawa=@Naprawa, Koszt_Koncowy=@Koszt_Koncowy, Koszt_Szacunkowy=@Koszt_Szacunkowy, DataPrzyjecia=@DataPrzyjecia, DataWydania=@DataWydania, TestDrive=@TestDrive, PozostawioneKluczyki=@PozostawioneKluczyki, PozostawioneDokumenty=@PozostawioneDokumenty, Zakupione_Czesci=@Zakupione_Czesci  WHERE ID=@ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                Edit_AND_Save(form);
                cmd.ExecuteNonQuery();
                conn.Close();
                Load_DB(form);
                MessageBox.Show("Dane zostałe odświeżone", "Baza danych");

                form.Work_Place.SelectTab(form.tabPage1);
                Clear(form, ID);
                form.Button_Save.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Nie było wprowadzonego tekstu");
            }
        }
        public void Save(Form1 form, int ID)
        {
            try
            {
                Save_Data(form);
                Clear(form, ID);
            }
            catch
            {
                MessageBox.Show("Probłem z podłączeniem do bazy danych bądź z zapisem danych", "Baza danych");
            }
            Load_DB(form);
        }
        public void ReadData(Form1 form, int ID)
        {
            conn.Open();

            ID = Convert.ToInt32(form.Dane_Warsztat.CurrentRow.Cells[0].Value.ToString());
            form.DataPrzyjecia.Text = form.Dane_Warsztat.CurrentRow.Cells[1].Value.ToString();
            form.DataWydania.Text = form.Dane_Warsztat.CurrentRow.Cells[2].Value.ToString();

            form.Marka.Text = form.Dane_Warsztat.CurrentRow.Cells[3].Value.ToString();
            form.Model.Text = form.Dane_Warsztat.CurrentRow.Cells[4].Value.ToString();
            form.NumerRejestracji.Text = form.Dane_Warsztat.CurrentRow.Cells[5].Value.ToString();

            form.YearOfProduction.Text = form.Dane_Warsztat.CurrentRow.Cells[6].Value.ToString();
            form.PojemnoscSilnika.Text = form.Dane_Warsztat.CurrentRow.Cells[7].Value.ToString();
            form.Przebieg.Text = form.Dane_Warsztat.CurrentRow.Cells[8].Value.ToString();

            form.IDNadwozia.Text = form.Dane_Warsztat.CurrentRow.Cells[9].Value.ToString();
            form.IDSilnika.Text = form.Dane_Warsztat.CurrentRow.Cells[10].Value.ToString();
            form._Name.Text = form.Dane_Warsztat.CurrentRow.Cells[11].Value.ToString();

            form.LastName.Text = form.Dane_Warsztat.CurrentRow.Cells[12].Value.ToString();
            form.NIP.Text = form.Dane_Warsztat.CurrentRow.Cells[13].Value.ToString();
            form.TelefonKomurkowy.Text = form.Dane_Warsztat.CurrentRow.Cells[14].Value.ToString();

            form.Adress.Text = form.Dane_Warsztat.CurrentRow.Cells[15].Value.ToString();
            form.txtZlecenie_Klienta.Text = form.Dane_Warsztat.CurrentRow.Cells[16].Value.ToString();
            form.txtDiagostyka.Text = form.Dane_Warsztat.CurrentRow.Cells[17].Value.ToString();

            form.txtNaprawa.Text = form.Dane_Warsztat.CurrentRow.Cells[18].Value.ToString();
            form.Price.Text = form.Dane_Warsztat.CurrentRow.Cells[19].Value.ToString();
            form.Price_Finally.Text = form.Dane_Warsztat.CurrentRow.Cells[20].Value.ToString();

            var jazda_ = form.Dane_Warsztat.CurrentRow.Cells[21].Value.ToString();
            form.TestDrive.Checked = jazda_ == "Tak";
            form.TestDrive.Checked = !(jazda_ == "Nie");

            var kluczyki_ = form.Dane_Warsztat.CurrentRow.Cells[22].Value.ToString();
            form.LeftKey.Checked = kluczyki_ == "Tak";
            form.LeftKey.Checked = !(kluczyki_ == "Nie");

            var dokumenty_ = form.Dane_Warsztat.CurrentRow.Cells[23].Value.ToString();
            form.LeftDocumets.Checked = dokumenty_ == "Tak";
            form.LeftDocumets.Checked = !(dokumenty_ == "Nie");

            form.txtZakupione_Czesci.Text = form.Dane_Warsztat.CurrentRow.Cells[24].Value.ToString();

            conn.Close();
            form.Button_Delete.Enabled = form.Enabled;
        }

        public void Load_DB(Form1 form)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            string CommandText = "select * from Dane";
            DB = new SQLiteDataAdapter(CommandText, conn);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            form.Dane_Warsztat.DataSource = DT;
            form.Dane_Warsztat.Columns[0].Visible = false;
            conn.Close();
        }
        public void Delete(Form1 form, int ID)
        {
            conn.Open();
            try
            {
                //Дані до опису Транспорту
                if (ID != 0)
                {
                    cmd = new SQLiteCommand("DELETE FROM Dane WHERE ID =@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                    Clear(form, ID);
                    Load_DB(form);
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
            conn.Close();
        }
        private void Save_Data(Form1 form)
        {
            conn.Open();
            //Створення процедури
            cmd.CommandText =
                "INSERT INTO Dane(Model, Marka, NumerRejestracji, RokProdukcji, PojemnoscSilnika, Przebieg, NumerNadwozia, IDSilnika, Imie, Nazwisko, NIP, Telefon, Adres, ZlecenieKlienta, Diagnostyka, Naprawa, Koszt_Szacunkowy, Koszt_Koncowy, DataPrzyjecia, DataWydania, TestDrive, PozostawioneKluczyki, PozostawioneDokumenty, Zakupione_Czesci) " +
                "VALUES(@Model, @Marka, @NumerRejestracji, @RokProdukcji, @PojemnoscSilnika, @Przebieg, @NumerNadwozia, @IDSilnika, @Imie, @Nazwisko, @NIP, @Telefon, @Adres, @ZlecenieKlienta, @Diagnostyka, @Naprawa, @Koszt_Szacunkowy, @Koszt_Koncowy, @DataPrzyjecia, @DataWydania, @TestDrive, @PozostawioneKluczyki, @PozostawioneDokumenty, @Zakupione_Czesci)";
            //Запис Основної Інформації завдяки Parameters
            Edit_AND_Save(form); //Беремо звідти команди виконнання 
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wszystko zostało zapisane");
            conn.Close();
            Console.WriteLine("Sql close");
            form.Work_Place.SelectTab(form.tabPage1);
        }
        private void Edit_AND_Save(Form1 form) // Тут містяться параметри для sql
        {
            cmd.Parameters.AddWithValue("@Model", form.Model.Text);
            cmd.Parameters.AddWithValue("@Marka", form.Marka.Text);
            cmd.Parameters.AddWithValue("@NumerRejestracji", form.NumerRejestracji.Text);
            cmd.Parameters.AddWithValue("@RokProdukcji", form.YearOfProduction.Text);

            cmd.Parameters.AddWithValue("@PojemnoscSilnika", form.PojemnoscSilnika.Text);
            cmd.Parameters.AddWithValue("@Przebieg", form.Przebieg.Text);
            cmd.Parameters.AddWithValue("@NumerNadwozia", form.IDNadwozia.Text);
            cmd.Parameters.AddWithValue("@IDSilnika", form.IDSilnika.Text);

            cmd.Parameters.AddWithValue("@Imie", form._Name.Text);
            cmd.Parameters.AddWithValue("@Nazwisko", form.LastName.Text);
            cmd.Parameters.AddWithValue("@NIP", form.NIP.Text);
            cmd.Parameters.AddWithValue("@Telefon", form.TelefonKomurkowy.Text);

            cmd.Parameters.AddWithValue("@Adres", form.Adress.Text);
            cmd.Parameters.AddWithValue("@ZlecenieKlienta", form.txtZlecenie_Klienta.Text);
            cmd.Parameters.AddWithValue("@Diagnostyka", form.txtDiagostyka.Text);

            cmd.Parameters.AddWithValue("@Naprawa", form.txtNaprawa.Text);
            cmd.Parameters.AddWithValue("@Koszt_Szacunkowy", form.Price.Text);
            cmd.Parameters.AddWithValue("@DataPrzyjecia", form.DataPrzyjecia.Text);
            if (form.lngReleaseDate.Checked == true)
            {
                Console.WriteLine("Data Wyadania Text");
                cmd.Parameters.AddWithValue("@DataWydania", form.DataWydania.Text);
            }
            else if (form.lngReleaseDate.Checked == false)
            {
                Console.WriteLine("Data wydania !text");
                cmd.Parameters.AddWithValue("@DataWydania", form.nothing = "");
            }
            cmd.Parameters.AddWithValue("@Koszt_Koncowy", form.Price_Finally.Text);
            cmd.Parameters.AddWithValue("@Zakupione_Czesci", form.txtZakupione_Czesci.Text);

            //Інформація додаткова
            //Пробний пробіг
            if (form.TestDrive.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TestDrive", form.Jazda = "Tak");
            }
            else if (form.TestDrive.Checked == false)
            {
                cmd.Parameters.AddWithValue("@TestDrive", form.Jazda = "Nie");
            }
            //Залишені ключі
            if (form.LeftKey.Checked == true)
            {
                cmd.Parameters.AddWithValue("@PozostawioneKluczyki", form.Kluczyki = "Tak");
            }
            else if (form.LeftKey.Checked == false)
            {
                cmd.Parameters.AddWithValue("@PozostawioneKluczyki", form.Kluczyki = "Nie");
            }
            //Залишені документи
            if (form.LeftDocumets.Checked == true)
            {
                cmd.Parameters.AddWithValue("@PozostawioneDokumenty", form.Dokumenty = "Tak");
            }
            else if (form.LeftDocumets.Checked == false)
            {
                cmd.Parameters.AddWithValue("@PozostawioneDokumenty", form.Dokumenty = "Nie");
            }
        }

        public void Clear(Form1 form, int ID)
        {
            //Opis pojazdu 
            ClearPart(form, form.panel1.Controls);
            //Dane klienta
            ClearPart(form, form.panel3.Controls);
            // Naprawa           Diagnostyka            Zlecenie Klienta            Zakupione Czesci
            form.txtNaprawa.Text = form.txtDiagostyka.Text = form.txtZlecenie_Klienta.Text = form.txtZakupione_Czesci.Text = "";
            form.YearOfProduction.Text = form.TelefonKomurkowy.Text = "";
            //Дані додаткові 
            form.TestDrive.Checked = form.LeftDocumets.Checked = form.LeftKey.Checked = false;
            form.Price.Text = form.Price_Finally.Text = "";

            ID = 0;
            form.Button_Save.Text = "Zapisz";
            form.Button_Delete.Enabled = false;
        }
        public void ClearPart(Form form, Control.ControlCollection controlCollection)
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

    }
}
