using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Warsztat
{
    internal class Interface
    {

        public void Localize_UA(Form1 _form)
        {
            _form.ustawieniaToolStripMenuItem.Text = "Налаштування";
            _form.językToolStripMenuItem.Text = "Мова";
            _form.polskiToolStripMenuItem.Text = "Польська";
            _form.ukraińskiToolStripMenuItem.Text = "Українська";

            //Кнопки
            _form.Button_Save.Text = "Зберегти";
            _form.tabPage2.Text = "Додати дані";
            _form.Button_Clear.Text = "Стерти";
            _form.Button_Update.Text = "Оновити Дані";
            _form.Button_Delete.Text = "Видалити";
            _form.tabPage1.Text = "Переглянути базу даних";
            _form.Button_Clear_DK.Text = "Стерти";
            _form.Button_Clear_OP.Text = "Стерти";
            _form.Button_Clear_Naprawa.Text = "Стерти";
            _form.Button_Clear_Zlecenie_Klienta.Text = "Стерти";
            _form.Button_Clear_Diagnostyka.Text = "Стерти";
            _form.Button_PurchasedParts_Clear.Text = "Стерти";
            //Текст
            _form.lngMarka.Text = "Марка";
            _form.lngModel.Text = "Модель";
            _form.lngRegistrationNumber.Text = "Номер Реєстрації";
            _form.lngYearOfProduction.Text = "Рок продукції";
            _form.lngLastName.Text = "Фамілія";
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
        }
        public void Localize_PL(Form1 _form)
        {
            _form.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            _form.językToolStripMenuItem.Text = "Język";
            _form.polskiToolStripMenuItem.Text = "Polski";
            _form.ukraińskiToolStripMenuItem.Text = "Ukraiński";

            //Кнопки
            _form.Button_Save.Text = "Zapisz";
            _form.tabPage2.Text = "Dodaj Dane";
            _form.Button_Clear.Text = "Wyczyść";
            _form.Button_Update.Text = "Odśwież Dane";
            _form.Button_Delete.Text = "Usuń";
            _form.tabPage1.Text = "Zobacz Baze Danych";
            _form.Button_Clear_DK.Text = "Wyczyść";
            _form.Button_Clear_OP.Text = "Wyczyść";
            _form.Button_Clear_Naprawa.Text = "Wyczyść";
            _form.Button_Clear_Zlecenie_Klienta.Text = "Wyczyść";
            _form.Button_Clear_Diagnostyka.Text = "Wyczyść";
            _form.Button_PurchasedParts_Clear.Text = "Wyczyść";
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
            else if (form.IDNadwozia.Text.Length < 17)
            {
                form.Button_Save.Enabled = false;
            }
        }
        public void Zlecenie(Form1 form)
        {
            if (form.txtZlecenie_Klienta.Text.Length > 330)
            {
                form.GeneretePDF_.Enabled = false;
                form.Zlecenie_Message.Text = " Dałej nie ma miejsca na druk";
            }
            else
            {
                form.GeneretePDF_.Enabled = true;
                form.Zlecenie_Message.Text = " ";
            }
        }
    }
}
