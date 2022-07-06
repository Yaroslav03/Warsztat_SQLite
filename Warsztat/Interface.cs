using System;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Warsztat
{
    internal class Interface
    {
        public string language;
        public string UA = "Ukranian";

        public void Localize_UA(Form1 _form)
        {          
            _form.ustawieniaToolStripMenuItem.Text = "Налаштування";
            _form.Update_DB.Text = "Оновити до новішої версії базу даних";
            _form.akcjeToolStripMenuItem1.Text = "Дії";
            _form.stwórzNowąTabelkęToolStripMenuItem1.Text = "Створити нову таблицю";
            _form.stwórzTabelkęZaplanowaneSamochodyToolStripMenuItem.Text = "Створити таблицю заплоновані автомобіллі";
            _form.zakończToolStripMenuItem.Text = "Закінчити";
            _form.deleteToolStripMenuItem.Text = "Видалити нову створену таблицю";
            _form.kopjujToolStripMenuItem.Text = "Копіювати";
            _form.Scheduled_Cars.Text = "Заплановані автомобілі";
            //Кнопки
            _form.Button_Save.Text = "Зберегти";
            _form.AddData.Text = "Додати дані";
            _form.Button_Clear.Text = "Стерти";
            _form.Button_Update.Text = "Оновити Дані";
            _form.Button_Delete.Text = "Видалити";
            _form.Home.Text = "Переглянути базу даних";
            _form.Button_Clear_DK.Text = "Стерти";
            _form.Button_Clear_OP.Text = "Стерти";
            _form.Button_Clear_Naprawa.Text = "Стерти";
            _form.Button_Clear_Zlecenie_Klienta.Text = "Стерти";
            _form.Button_Clear_Diagnostyka.Text = "Стерти";
            _form.Button_PurchasedParts_Clear.Text = "Стерти";
            _form.Delete_Scheduled_Car.Text = "Видалити";
            _form.Update_Scheduled_Cars.Text = "Оновити";
            _form.Plan_your_car.Text = "Запланувати автомобіль";
            //Текст
            _form.lngMarka.Text = "Марка";
            _form.lngModel.Text = "Модель";
            _form.lngRegistrationNumber.Text = "Номер Реєстрації";
            _form.lngYearOfProduction.Text = "Рок продукції";
            _form.lngLastName.Text = "Прізвище";
            _form.lngSearch.Text = "Пошук";
            _form.lngAdress.Text = "Адрес";
            _form.lngVehicleDescription.Text = "Опис транспортного засобу";
            _form.lngCustomerData.Text = "Дані клієнта";
            _form.lngMileage.Text = "Пробіг";
            _form.lngEngineCapacity.Text = "Об'єм двигуна";
            _form.lngBodyNumber.Text = "Номер кузова";
            _form.lngName.Text = "Ім'я";
            _form.lngNIP.Text = "ІД";
            _form.lngTelephon.Text = "Номер Телефону";
            _form.lngAdditionalInformation.Text = "Інформація Додаткова";
            _form.lngDateOfAdmission.Text = "Дата прийняття";
            _form.lngReleaseDate.Text = "Дата випуску";
            _form.lngClientsOrder.Text = "Замовлення клієнта";
            _form.lngDiagnostics.Text = "Діагностика";
            _form.lngRepair.Text = "Ремонт";
            _form.LeftDocumets.Text = "залишені документи транспортного засобу";
            _form.LeftKey.Text = "Залишені ключі";
            _form.TestDrive.Text = "Клієнт дає згоду на  пробну їзду";
            _form.lngPrice.Text = "Ціна приблизна";
            _form.lngPriceFinally.Text = "Ціна кінцева";
            _form.lngPurchasedParts.Text = "Закуплені запчастини";
            _form.lngIDEngine.Text = "Номер/Код двигуна";
            _form.lngOldBD.Text = "Стара база даних";
            _form.lngNewBD.Text = "Нова база даних";
        }
        public   void Localize_PL(Form1 _form)
        {         
            _form.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            _form.Update_DB.Text = "Odśwież do nowszej wersji Bazy Danych";
            _form.akcjeToolStripMenuItem1.Text = "Akcje";
            _form.stwórzNowąTabelkęToolStripMenuItem1.Text = "Stwórz nową tabelkę ";
            _form.stwórzTabelkęZaplanowaneSamochodyToolStripMenuItem.Text = "Stwórz tabelkę Zaplanowane Samochody";
            _form.zakończToolStripMenuItem.Text = "Zakończ";
            _form.deleteToolStripMenuItem.Text = "Usuń nową tabelkę";
            _form.kopjujToolStripMenuItem.Text = "Kopjuj";
            _form.Scheduled_Cars.Text = "Zaplanowane Samochody";

            //Кнопки
            _form.Button_Save.Text = "Zapisz";
            _form.AddData.Text = "Dodaj Dane";
            _form.Button_Clear.Text = "Wyczyść";
            _form.Button_Update.Text = "Odśwież Dane";
            _form.Button_Delete.Text = "Usuń";
            _form.Home.Text = "Zobacz Baze Danych";
            _form.Button_Clear_DK.Text = "Wyczyść";
            _form.Button_Clear_OP.Text = "Wyczyść";
            _form.Button_Clear_Naprawa.Text = "Wyczyść";
            _form.Button_Clear_Zlecenie_Klienta.Text = "Wyczyść";
            _form.Button_Clear_Diagnostyka.Text = "Wyczyść";
            _form.Button_PurchasedParts_Clear.Text = "Wyczyść";
            _form.Delete_Scheduled_Car.Text = "Usuń";
            _form.Update_Scheduled_Cars.Text = "Odśwież";
            _form.Plan_your_car.Text = "Zaplanuj Samochód";

            //Текст
            _form.lngMarka.Text = "Marka";
            _form.lngModel.Text = "Model";
            _form.lngRegistrationNumber.Text = "Numer Rejestracji";
            _form.lngYearOfProduction.Text = "Rok Produkcji";
            _form.lngLastName.Text = "Nazwisko";
            _form.lngSearch.Text = "Szukaj";
            _form.lngAdress.Text = "Adres";
            _form.lngVehicleDescription.Text = "Opis Pojazdu";
            _form.lngCustomerData.Text = "Dane Klienta";
            _form.lngMileage.Text = "Przebieg";
            _form.lngEngineCapacity.Text = "Pojemność Silnika";
            _form.lngBodyNumber.Text = "Numer Nadwozia";
            _form.lngName.Text = "Imię";
            _form.lngNIP.Text = "NIP";
            _form.lngTelephon.Text = "Telefon Komurkowy";
            _form.lngAdditionalInformation.Text = "Informacja Dodatkowa";
            _form.lngDateOfAdmission.Text = "Data Przyjęcia";
            _form.lngReleaseDate.Text = "Data Wydania";
            _form.lngClientsOrder.Text = "Zlecenie Klienta";
            _form.lngDiagnostics.Text = "Diagnostyka";
            _form.lngRepair.Text = "Naprawa";
            _form.LeftDocumets.Text = "Pozostawione dokumenty samochodu";
            _form.LeftKey.Text = "Pozostawione kluczyki";
            _form.TestDrive.Text = "Klient wyraża zgodę na jazdę próbną";
            _form.lngPrice.Text = "Koszt Szacunkowy";
            _form.lngPriceFinally.Text = "Koszt Koncowy";
            _form.lngPurchasedParts.Text = "Zakupione Części";
            _form.lngIDEngine.Text = "Numer/kod Silnika";
            _form.lngOldBD.Text = "Stara Baza Danych";
            _form.lngNewBD.Text = "Nowa Baza Danych";
        }
        public void Verify_Button(Form1 form)
        {
            form.Marka.BackColor = String.IsNullOrEmpty(form.Marka.Text) ? Color.Red : Color.White;
            form.Model.BackColor = String.IsNullOrEmpty(form.Model.Text) ? Color.Red : Color.White;
            form.NumerRejestracji.BackColor = String.IsNullOrEmpty(form.NumerRejestracji.Text) ? Color.Red : Color.White;
        }
        public void BodyNumberVerify(Form1 form)
        {
            form.IDNadwozia.MaxLength = 17;
            form.IDNadwozia.Text = String.Concat(form.IDNadwozia.Text.Where(char.IsLetterOrDigit));

            form.BodyNumberLenght.Text = form.IDNadwozia.Text.Length.ToString();
            if (form.IDNadwozia.Text.Length == 17)
            {
                form.Button_Save.Enabled = true;
            }
            else if (form.IDNadwozia.Text.Length <= 16)
            {
                form.Button_Save.Enabled = false;
            }
        }
        public void VerefyCheckDateOfPayInvoice(Form1 form)
        {
            if (form.todayInvoice.Checked == true)
            {
                form.dateTimePickerInvoice.Text = DateTime.Now.ToString("yyyy.MM.dd");
                form.DateOfPayInvoice.Text = String.Empty;
                form.dateTimePickerInvoice.Show();
                form.DateOfPayInvoice.Hide();
            }
            else if (form.forTimeInvoice.Checked == true)
            {
                form.dateTimePickerInvoice.Text = DateTime.Now.ToString("yyyy.MM.dd");
                form.dateTimePickerInvoice.Hide();
                form.DateOfPayInvoice.Show();
                form.DateOfPayInvoice.Enabled = true;
            }
        }

        public void Zlecenie(Form1 form)
        {
            Console.WriteLine(form.txtZlecenie_Klienta.Text.Length);
            
            if(form.txtZlecenie_Klienta.Text.Length >= 105 && form.txtZlecenie_Klienta.Text.Length <= 109)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 215 && form.txtZlecenie_Klienta.Text.Length <= 220)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 325 && form.txtZlecenie_Klienta.Text.Length <= 330)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 440 && form.txtZlecenie_Klienta.Text.Length <= 444)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 547 && form.txtZlecenie_Klienta.Text.Length <= 552)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 663 && form.txtZlecenie_Klienta.Text.Length <= 668)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length >= 765 && form.txtZlecenie_Klienta.Text.Length <= 770)
            {
                form.Zlecenie_Message.Text = "We recommend pressing Enter";
            }
            else if (form.txtZlecenie_Klienta.Text.Length > 770)
            {
                form.Zlecenie_Message.Text = "There is no place for printing any further";
            }
            else
            {
                form.Zlecenie_Message.Text = String.Empty;
            }
        }

        public  void Load_Data_Localize(Form1 _form)
        {
            XmlTextReader reader = new XmlTextReader("Settings.xml");
            try 
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Language")
                    {
                        language = reader.ReadElementContentAsString();
                    }
                }
                if (UA == language)
                {
                    Localize_UA(_form);
                }
            }
            catch
            {
                Localize_PL(_form);
            }  
        }
    }
}
