using Spire.Pdf;
using System;
using Spire.Pdf.Texts;
using Spire.Pdf.ColorSpace;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.IO;

namespace Warsztat
{

    internal class GeneratePDF
    {
        PdfDocument pdfDocument = new PdfDocument();

        public void PreviewPDF(Form1 form)
        {
            string path = "pdf\\" + form.IDNadwozia.Text + "_" + form.Marka.Text + "_" + form.Model.Text + "_" + ".pdf";
            System.Diagnostics.Process.Start(path);
        }
        public  void Create(Form1 form)
        {
            PdfSolidBrush brush1 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont fontTrue = new PdfTrueTypeFont(new Font("Bahnschrift Light", 10f), true);//write a polish letter
            PdfTrueTypeFont FT = new PdfTrueTypeFont(new Font("Bahnschrift Light", 6f), true);
            PdfPageBase page = pdfDocument.Pages.Add();
            PdfPen pen1 = new PdfPen(Color.Red, 3f);
            PdfStringFormat leftAlignment = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
            PdfBrush brush = new PdfSolidBrush(Color.DarkGray);

            //Tytuł
            page.Canvas.DrawString("Data Przyjęcia: " + form.DataPrzyjecia.Text, fontTrue, brush1, 0, -2);
            //Dane Samochodu
            page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 14), new SizeF(1300, 14)));
            page.Canvas.DrawString("DANE SAMOCHODU " ,fontTrue, brush1, 5,15);
            page.Canvas.DrawString("Marka: " + form.Marka.Text, fontTrue, brush1, 0,33);
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
            //Naprawa 
            page.Canvas.DrawRectangle(brush, new RectangleF(new Point(0, 228), new SizeF(1300, 15)));
            page.Canvas.DrawString("Naprawa", fontTrue, brush1, 0, 230);
            page.Canvas.DrawString(form.txtNaprawa.Text, fontTrue, brush1, 0, 245);

            pdfDocument.SaveToFile("pdf\\"+form.IDNadwozia.Text + "_" + form.Marka.Text+"_"+ form.Model.Text + "_" + ".pdf");
            
            pdfDocument.Close();
            //pdfDocument.LoadFromFile("pdf\\" + form.IDNadwozia.Text + "_" + form.Marka.Text + "_" + form.Model.Text + "_" + ".pdf");
           // PrintPreviewDialog
        }
       
       
    }
}
