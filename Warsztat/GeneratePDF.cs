using Spire.Pdf;
using Spire.License;
using System;
using Spire.Pdf.Texts;
using Spire.Pdf.ColorSpace;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Warsztat
{
    internal class GeneratePDF
    {
        PdfDocument pdfDocument = new PdfDocument();
        PdfSolidBrush brush1 = new PdfSolidBrush(Color.Black);
        PdfTrueTypeFont fontTrue = new PdfTrueTypeFont(new Font("Bahnschrift Light", 10f), true);//write a polish letter
        PdfTrueTypeFont FT = new PdfTrueTypeFont(new Font("Bahnschrift Light", 6f), true);
        
        PdfPen pen1 = new PdfPen(Color.Red, 3f);
        PdfStringFormat leftAlignment = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
        PdfBrush brush = new PdfSolidBrush(Color.DarkGray);

        string jazda_;
        string kluczyki_;
        string dokumenty_;

        public void PreviewPDF(Form1 form)
        {
            
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            var FileInfo = File.Exists(path);
            if (FileInfo)
            {
                Process.Start(path);
            }
            else if (!FileInfo)
            {
                Create(form);
                DialogResult result = MessageBox.Show("Pdf stworzony otwórzyć go? ", "Warsztat", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Process.Start(path);
                }

            }
        }


        public async void Create(Form1 form)
        {

            PdfPageBase page = pdfDocument.Pages.Add();
            VerefyChceckBox(form);
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            await Task.Run(() => {
                //Tytuł
                page.Canvas.DrawString("Data Przyjęcia: " + form.DataPrzyjecia.Text, fontTrue, brush1, 0, -2);
            //Dane Samochodu
            page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 14), new SizeF(1300, 14)));
            page.Canvas.DrawString("DANE SAMOCHODU ", fontTrue, brush1, 5, 15);
            page.Canvas.DrawString("Marka: " + form.Marka.Text, fontTrue, brush1, 0, 33);
            page.Canvas.DrawString("Model: " + form.Model.Text, fontTrue, brush1, 0, 43);
            page.Canvas.DrawString("Numer Rejestracji:" + form.NumerRejestracji.Text, fontTrue, brush1, 0, 53);
            page.Canvas.DrawString("Rok Produkcji: " + form.YearOfProduction.Text, fontTrue, brush1, 0, 63);
            page.Canvas.DrawString("Przebieg: " + form.Przebieg.Text, fontTrue, brush1, 300, 33);
            page.Canvas.DrawString("ID Nadwozia: " + form.IDNadwozia.Text, fontTrue, brush1, 300, 43);
            page.Canvas.DrawString("ID Silnika: " + form.IDSilnika.Text, fontTrue, brush1, 300, 53);
            page.Canvas.DrawString("Pojemność Silnika: " + form.PojemnoscSilnika.Text, fontTrue, brush1, 300, 63);

            //Dane Klienta
            page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 78), new SizeF(1300, 13)));
            page.Canvas.DrawString("DANE KLIENTA", fontTrue, brush1, 5, 78);
            page.Canvas.DrawString("Imię: " + form._Name.Text, fontTrue, brush1, 0, 91);
            page.Canvas.DrawString("Nazwisko: " + form.LastName.Text, fontTrue, brush1, 0, 101);
            page.Canvas.DrawString("Telefon Komurkowy: " + form.TelefonKomurkowy.Text, fontTrue, brush1, 300, 91);
            page.Canvas.DrawString("NIP: " + form.NIP.Text, fontTrue, brush1, 300, 101);
            page.Canvas.DrawString("Adres: " + form.Adress.Text, fontTrue, brush1, 0, 111);
            //Zakres zleconych prac przez klienta
            page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 139), new SizeF(1300, 13)));
            page.Canvas.DrawString("ZAKRES PRAC ZLECONYCH PRZEZ KLIENTA", fontTrue, brush1, 5, 139);
            page.Canvas.DrawString("Opis (słowami klienta)", FT, brush1, 0, 153);
            page.Canvas.DrawString(form.txtZlecenie_Klienta.Text, fontTrue, brush1, 0, 160);
        });
            await Task.Run(() => {
                //Naprawa 
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 258), new SizeF(1300, 15)));
                page.Canvas.DrawString("NAPRAWA", fontTrue, brush1, 0, 260);
                page.Canvas.DrawString(form.txtNaprawa.Text, fontTrue, brush1, 0, 275);
                //Zakupione Części
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 371), new SizeF(1300, 15)));
                page.Canvas.DrawString("ZAKUPIONE CZĘŚCI", fontTrue, brush1, 0, 373);
                page.Canvas.DrawString(form.txtZakupione_Czesci.Text, fontTrue, brush1, 0, 386);
                //Informacje Dodatkowe
                page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 482), new SizeF(1300, 15)));
                page.Canvas.DrawString("INFORMACJA DODATKOWA", fontTrue, brush1, 0, 484);
                if (form.lngReleaseDate.Checked == true)
                {
                    page.Canvas.DrawString("Data Wydania: " + form.DataWydania.Text, fontTrue, brush1, 0, 497);
                }
                page.Canvas.DrawString("Data Wydania: ", fontTrue, brush1, 0, 497);
                page.Canvas.DrawString("Klient wyraża zgodę na jazdę próbną: " + jazda_, fontTrue, brush1, 0, 510);
                page.Canvas.DrawString("Pozostawione dokumenty samochodu: " + dokumenty_, fontTrue, brush1, 0, 523);
                page.Canvas.DrawString("Pozostawione kluczyki: " + kluczyki_, fontTrue, brush1, 0, 536);
                page.Canvas.DrawString("Koszt: " + form.Price_Finally.Text, fontTrue, brush1, 0, 549);
                //Zakończenie
                page.Canvas.DrawString("Pieczątka Serwisu ___________________ ", fontTrue, brush1, 0, 752);
                page.Canvas.DrawString("Podpis Klienta _____________________", fontTrue, brush1, 300, 752);
            });

                pdfDocument.SaveToFile(path);
                pdfDocument.Close();
        }
        
        private void VerefyChceckBox(Form1 form)
        { 
         if (form.TestDrive.Checked == true)
         {
         jazda_ = "Tak";
         }
         else if (form.TestDrive.Checked == false)
         {
             jazda_ = "Nie";
         }
         if (form.LeftKey.Checked == true)
         {
             kluczyki_ = "Tak";
         }
         else if (form.LeftKey.Checked == false)
         {
             kluczyki_ = "Nie";
         }
         if (form.LeftDocumets.Checked == true)
         {
             dokumenty_ = "Tak";
         }
         else if (form.LeftDocumets.Checked == false)
         {
             dokumenty_ = "Nie";
         }
        }
    }
}
