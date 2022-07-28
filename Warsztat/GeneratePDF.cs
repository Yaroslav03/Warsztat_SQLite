using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;

using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace Warsztat
{  
    internal class GeneratePDF
    {
        PdfDocument pdfDocument = new PdfDocument();
        PdfSolidBrush brush1 = new PdfSolidBrush(Color.Black);
        PdfTrueTypeFont polish = new PdfTrueTypeFont(new Font("Bahnschrift Light", 10f), true);//write a polish letter
        PdfTrueTypeFont FT = new PdfTrueTypeFont(new Font("Bahnschrift Light", 6f), true);

        PdfPen pen1 = new PdfPen(Color.Red, 3f);
        PdfStringFormat leftAlignment = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
        PdfBrush brush = new PdfSolidBrush(Color.DarkGray);
        
        private string NameCompany, AdresCompany, NIP, Konto, NrPhone, jazda_, kluczyki_, dokumenty_, pay, dateOfPay;

        #region Function GeneratePDF
        private void MessageOpenPDF(string path)
        {
            DialogResult result = MessageBox.Show("Pdf stworzony otwórzyć go? ", "Warsztat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Process.Start(path);
            }
        }
        #endregion

        #region Zlecenie
        public void PreviewPDF(Form1 form)
        {         
            string dir = "pdf\\" + form.IDNadwozia.Text;
            string path = dir +"\\ZLECENIE_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + ".pdf";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                Create(form, path);
                MessageOpenPDF(path);
            }
            else if (Directory.Exists(dir) && !File.Exists(path))
            {
                Create(form, path);
                MessageOpenPDF(path);
            }
            else
            {
                Process.Start(path);
            }
        }

        public void UpdatePDF(Form1 form) //methods for button Update
        {
            string dir = "pdf\\" + form.IDNadwozia.Text;
            string path = dir + "\\ZLECENIE_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + ".pdf";

            if (!Directory.Exists(dir) || !File.Exists(path))
            {
                MessageBox.Show("Pliku nie istnieje, proszę go najpierw stworzyć a póżniej edytować", "Warsztat");
            }
            else
            {
                File.Delete(path);
                PreviewPDF(form);
            }
        }
        public void DeletePDF(Form1 form)
        {

            string dir = "pdf\\" + form.IDNadwozia.Text;
            string path = dir + "\\ZLECENIE_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + ".pdf";

            if (!Directory.Exists(dir) || !File.Exists(path))
            {
                MessageBox.Show("Pliku nie istnieje, proszę go najpierw stworzyć a póżniej usuwać :)", "Warsztat");
            }
            else
            {
                File.Delete(path);
                MessageBox.Show("Plik usunięty", "Warsztat");
            }
        }

        public async void Create(Form1 form, string path)
        {
            PdfPageBase page = pdfDocument.Pages.Add();

            //Verefy radio button
            jazda_ = form.TestDrive.Checked ? "Tak" : "Nie";
            kluczyki_ = form.LeftKey.Checked ? "Tak" : "Nie";
            dokumenty_ = form.LeftDocumets.Checked ? "Tak" : "Nie";

            string releaseDate = form.lngReleaseDate.Checked ? form.DataWydania.Text : string.Empty;

            await Task.Run(() => {
                //Tytuł
                page.Canvas.DrawString("Data Przyjęcia: " + form.DataPrzyjecia.Text, polish, brush1, 0, -2);
                //Dane Samochodu
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 14), new SizeF(1300, 14)));
                page.Canvas.DrawString("DANE SAMOCHODU ", polish, brush1, 5, 15);
                page.Canvas.DrawString("Marka: " + form.Marka.Text, polish, brush1, 0, 33);
                page.Canvas.DrawString("Model: " + form.Model.Text, polish, brush1, 0, 43);
                page.Canvas.DrawString("Numer Rejestracji:" + form.NumerRejestracji.Text, polish, brush1, 0, 53);
                page.Canvas.DrawString("Rok Produkcji: " + form.YearOfProduction.Text, polish, brush1, 0, 63);
                page.Canvas.DrawString("Przebieg: " + form.Przebieg.Text, polish, brush1, 300, 33);
                page.Canvas.DrawString("ID Nadwozia: " + form.IDNadwozia.Text, polish, brush1, 300, 43);
                page.Canvas.DrawString("ID Silnika: " + form.IDSilnika.Text, polish, brush1, 300, 53);
                page.Canvas.DrawString("Pojemność Silnika: " + form.PojemnoscSilnika.Text, polish, brush1, 300, 63);

                //Dane Klienta
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 78), new SizeF(1300, 13)));
                page.Canvas.DrawString("DANE KLIENTA", polish, brush1, 5, 78);
                page.Canvas.DrawString("Imię: " + form._Name.Text, polish, brush1, 0, 91);
                page.Canvas.DrawString("Nazwisko: " + form.LastName.Text, polish, brush1, 0, 101);
                page.Canvas.DrawString("Telefon Komurkowy: " + form.TelefonKomurkowy.Text, polish, brush1, 300, 91);
                page.Canvas.DrawString("NIP: " + form.NIP.Text, polish, brush1, 300, 101);
                page.Canvas.DrawString("Adres: " + form.Adress.Text, polish, brush1, 0, 111);
                //Zakres zleconych prac przez klienta
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 139), new SizeF(1300, 13)));
                page.Canvas.DrawString("ZAKRES PRAC ZLECONYCH PRZEZ KLIENTA", polish, brush1, 5, 139);
                page.Canvas.DrawString("Opis (słowami klienta)", FT, brush1, 0, 153);
                page.Canvas.DrawString(form.txtZlecenie_Klienta.Text, polish, brush1, 0, 160);
            });
            await Task.Run(() => {
                //Naprawa 
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 258), new SizeF(1300, 15)));
                page.Canvas.DrawString("NAPRAWA", polish, brush1, 0, 260);
                page.Canvas.DrawString(form.txtNaprawa.Text, polish, brush1, 0, 275);
                //Zakupione Części
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 371), new SizeF(1300, 15)));
                page.Canvas.DrawString("ZAKUPIONE CZĘŚCI", polish, brush1, 0, 373);
                page.Canvas.DrawString(form.txtZakupione_Czesci.Text, polish, brush1, 0, 386);
                //Informacje Dodatkowe
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 482), new SizeF(1300, 15)));
                page.Canvas.DrawString("INFORMACJA DODATKOWA", polish, brush1, 0, 484);              
                page.Canvas.DrawString("Data Wydania: " + releaseDate, polish, brush1, 0, 497);
                page.Canvas.DrawString("Klient wyraża zgodę na jazdę próbną: " + jazda_, polish, brush1, 0, 510);
                page.Canvas.DrawString("Pozostawione dokumenty samochodu: " + dokumenty_, polish, brush1, 0, 523);
                page.Canvas.DrawString("Pozostawione kluczyki: " + kluczyki_, polish, brush1, 0, 536);
                page.Canvas.DrawString("Koszt: " + form.Price_Finally.Text, polish, brush1, 0, 549);
                //Zakończenie
                page.Canvas.DrawString("Pieczątka Serwisu ___________________ ", polish, brush1, 0, 752);
                page.Canvas.DrawString("Podpis Klienta _____________________", polish, brush1, 300, 752);
            });

            pdfDocument.SaveToFile(path);
            pdfDocument.Close();
        }

        #endregion Zlecenie
        #region Faktura
        public void PreviewInvoice(Form1 form)
        {
            string dir = "pdf\\" + form.IDNadwozia.Text;
            string path = dir + "\\FAKTURA_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + ".pdf";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                Create_Invoice(form, path);
                MessageOpenPDF(path);
            }
            else if(Directory.Exists(dir) && !File.Exists(path))
            {
                Create_Invoice(form, path);
                MessageOpenPDF(path);
            }
            else
            {
                Process.Start(path);
            }
        }

        public void UpdateInvoice(Form1 form) //methods for button Update
        {
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
                PreviewInvoice(form);
            }
        }
        public void DeleteInvoice(Form1 form)
        {
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
                MessageBox.Show("File was deleted", "Warsztat");
            }
        }

        private async void Create_Invoice(Form1 form, string path)
        {
            PdfTrueTypeFont Title = new PdfTrueTypeFont(new Font("Bahnschrift SemiBold", 20f), true);//write a polish letter
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            PdfPen lineTable = new PdfPen(PdfBrushes.Black, 1.0f);
            PdfSolidBrush gray = new PdfSolidBrush(Color.Gray);
            PdfSolidBrush white = new PdfSolidBrush(Color.White);
            PdfPageBase page = pdfDocument.Pages.Add();

            pay = form.Card.Checked ? "przelew" : "gotówka";
            dateOfPay = form.todayInvoice.Checked ? form.dateTimePickerInvoice.Text.Trim() : form.DateOfPayInvoice.Text.Trim();

            _ = Task.Run(() => { ReadXML(); });
            
            await Task.Run(() => {
                //IMPORTANT INFORMATION
                page.Canvas.DrawRectangle(pen, gray, new Rectangle(new Point(0, 0), new Size(505, 20)));
                page.Canvas.DrawString("SPRZEDAWCA ZWOLNIONY PODMIOTOWO Z PODATKU OD TOWARÓWW I USŁUG", polish, white, 0, 0);
                page.Canvas.DrawString(
                    "[dostawa towarów lub świadczenie usług zwolnione na podstawie art.113 ust. 1 (albo ust. 9) " +
                    "ustawy z dnia 11 marca 2004 r. o podatku od towarów i usług (Dz.U. z 2016 r. poz. 710, z późn. zm)]",
                    FT, white, 0, 10);
                //Title
                page.Canvas.DrawString("Miejsce i Data          Faktura Nr: " + form.FakturaNrtxt.Text, polish, brush1, 330, 25);
                page.Canvas.DrawLine(pen, new PointF(330, 37), new PointF(510, 37));
                page.Canvas.DrawString(AdresCompany + "/" + form.DataWydania.Text, polish, brush1, 330, 38);
                page.Canvas.DrawString(NameCompany.Trim(), Title, brush1, 200, 55);
                //seller
                page.Canvas.DrawString("Sprzedawca " + NameCompany.Trim(), polish, brush1, 0, 90);
                page.Canvas.DrawString("NIP: " + NIP.Trim(), polish, brush1, 0, 100);
                page.Canvas.DrawString("Numer telefonu: " + NrPhone.Trim(), polish, brush1, 0, 110);
                page.Canvas.DrawString("KONTO: " + Konto.Trim(), polish, brush1, 0, 120);                
                //Buyer
                page.Canvas.DrawLine(pen, new PointF(250, 90), new PointF(250, 130));
                page.Canvas.DrawString("Nabywca", polish, brush1, 300, 90);
                page.Canvas.DrawString("Imię: " + form.NameORNameCompanyInvoice.Text.Trim(), polish, brush1, 300, 100);
                page.Canvas.DrawString("NIP: " + form.NipInvoice.Text.Trim(), polish, brush1, 300, 110);
                page.Canvas.DrawString("Adres: " + form.AdresInvoice.Text.Trim(), polish, brush1, 300, 120);
            });
            await Task.Run(() => {
                //Type of services
                PdfTable table = new PdfTable();
                //style text
                table.Style.DefaultStyle.Font = polish;
                table.Style.HeaderStyle.Font = polish;
                //create table
                DataTable dataTable = new DataTable();
                //create Columns
                dataTable.Columns.Add("Rodzaj usług");
                dataTable.Columns.Add("Koszt");
                //Create Rows with information
                dataTable.Rows.Add(new string[] { form.Service1Invoice.Text.Trim(), form.PriceService1Invoice.Text.Trim() + "zł" });
                dataTable.Rows.Add(new string[] { form.Service2Invoice.Text.Trim(), form.PriceService2Invoice.Text.Trim() + "zł" });
                dataTable.Rows.Add(new string[] { form.Service3Invoice.Text.Trim(), form.PriceService3Invoice.Text.Trim() + "zł" });
                dataTable.Rows.Add(new string[] { form.Service4Invoice.Text.Trim(), form.PriceService4Invoice.Text.Trim() + "zł" });
                table.DataSource = dataTable;   //Fill(Wypełnij) data into the PDF table
                table.Style.ShowHeader = true; //display the header (not displayed by default)
                table.Draw(page, new RectangleF(0, 150, 900, 400)); //size table
                page.Canvas.DrawLine(lineTable, new PointF(515, 150), new PointF(515, 215));
                //Information
                page.Canvas.DrawString("Sposób płatności: " + pay, polish, brush1, 0, 220);                               
                page.Canvas.DrawString("termin zapłaty: " + dateOfPay, polish, brush1, 0, 230);               
                page.Canvas.DrawString("W banku: " + form.InBankInvoice.Text.Trim(), polish, brush1, 300, 230);
                page.Canvas.DrawString("Nr Konta: " + form.KontoInvoice.Text.Trim(), polish, brush1, 0, 240);
                page.Canvas.DrawString("Kwota należności ogółem do zapłaty: " + form.PriceFinall.Text + "zł", polish, brush1, 300, 240);
                page.Canvas.DrawRectangle(pen, white, new Rectangle(new Point(350, 260), new Size(150, 50)));
                page.Canvas.DrawString("podpis wystawcy faktury", FT, gray, 435, 310);
            });

            pdfDocument.SaveToFile(path);
            pdfDocument.Close();
        }

        #endregion Faktura
        #region RemoveLaterFromHere
        public void ReadXML()
        {
            XmlTextReader reader = new XmlTextReader("Settings.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NameCompany")
                {
                    NameCompany = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "AdresCompany")
                {
                    AdresCompany = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NIPCompany")
                {
                    NIP = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "KontoCompany")
                {
                    Konto = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NrTelefonu")
                {
                    NrPhone = reader.ReadElementContentAsString();
                }
            }
        }
        #endregion
    }
}