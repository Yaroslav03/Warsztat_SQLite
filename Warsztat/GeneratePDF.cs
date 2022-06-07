using Spire.Pdf;
using Spire.Pdf.Graphics;
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

        private string NameCompany;
        private string AdresCompany;
        private string NIP;
        private string Konto;
        private string NumerBDD;


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
        
       // string path_Zlecenie = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
        #region Zlecenie
        
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

        public void UpdatePDF(Form1 form) //methods for button Update
        {
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
                PreviewPDF(form);
            }
        }
        public void DeletePDF(Form1 form)
        {
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + ".pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
                MessageBox.Show("File was deleted", "Warsztat");
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
        #endregion Zlecenie
        #region Faktura
        public void PreviewInvoice(Form1 form)
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

        public async void Create_Invoice(Form1 form)
        {
            PdfTrueTypeFont Title = new PdfTrueTypeFont(new Font("Bahnschrift SemiBold", 20f), true);//write a polish letter
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            PdfPageBase page = pdfDocument.Pages.Add();

            _ = Task.Run(() => { ReadXML(); });
                VerefyChceckBox(form);
            string path = "pdf\\" + form.IDNadwozia.Text.Trim() + "_" + form.Marka.Text.Trim() + "_" + form.Model.Text.Trim() + "_" + "_FAKTURA.pdf";
            await Task.Run(() => {
                //Title
                page.Canvas.DrawString("Miejsce i Data " ,  fontTrue, brush1, 330, -2);
                page.Canvas.DrawLine(pen, new PointF(350, 10), new PointF(510, 10));
                page.Canvas.DrawString(AdresCompany + "/" + form.DataWydania.Text, fontTrue, brush1, 350, 12);
                page.Canvas.DrawString(NameCompany.Trim(), Title, brush1, 200, 30);
                //seller
                page.Canvas.DrawString("Sprzedawca " + NameCompany.Trim(), fontTrue, brush1, 0, 70);
                page.Canvas.DrawString("NIP: " + NIP.Trim(), fontTrue, brush1, 0, 80);
                page.Canvas.DrawString("KONTO: " + Konto.Trim(), fontTrue, brush1, 0, 90);
                page.Canvas.DrawString("Numer BDD: " + NumerBDD.Trim(), fontTrue, brush1, 0, 100);
                            });
            await Task.Run(() => {
                //Buyer
                page.Canvas.DrawLine(pen, new PointF(250, 60), new PointF(250, 120));
                page.Canvas.DrawString("Nabywca", fontTrue, brush1, 300, 70);
                page.Canvas.DrawString("Imię: ", fontTrue, brush1, 300, 80);
                page.Canvas.DrawString("NIP: ", fontTrue, brush1, 300, 90); 
                page.Canvas.DrawString("Adres: ", fontTrue, brush1, 300, 100); ; 
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
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NumerBDDCompany")
                {
                    NumerBDD = reader.ReadElementContentAsString();
                }
            }
        }
        #endregion
    }
}
