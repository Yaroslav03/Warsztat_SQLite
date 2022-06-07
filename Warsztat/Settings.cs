using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Warsztat
{
    internal class Settings
    {
        public void SaveXml(Setting_Form form, string lang)
        {
             Task.Run(() =>
            {
                //Create the XmlDocument.
                XmlTextWriter xmlWrite = new XmlTextWriter("Settings.xml", Encoding.UTF8);
                xmlWrite.Formatting = Formatting.Indented;
                xmlWrite.WriteStartDocument();

                xmlWrite.WriteStartElement("Settings");
                xmlWrite.WriteStartElement("Interface");
                xmlWrite.WriteElementString("Language", lang);
               // xmlWrite.WriteEndElement();
                xmlWrite.WriteStartElement("CompanyData");
                xmlWrite.WriteElementString("NameCompany", form.NameCompany.Text.Trim());
                xmlWrite.WriteElementString("AdresCompany", form.AdresCompany.Text.Trim());
                xmlWrite.WriteElementString("NIPCompany", form.NIP.Text.Trim());
                xmlWrite.WriteElementString("KontoCompany", form.Konto.Text.Trim());
                xmlWrite.WriteElementString("NumerBDDCompany", form.NumerBDD.Text.Trim());

                xmlWrite.WriteEndDocument();
                xmlWrite.Flush();
                xmlWrite.Close();
            });        
        }
        public void ReadXML(Setting_Form form)
        {
            XmlTextReader reader = new XmlTextReader("Settings.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NameCompany")
                {
                    form.NameCompany.Text = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "AdresCompany")
                {
                    form.AdresCompany.Text = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NIPCompany")
                {
                    form.NIP.Text = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "KontoCompany")
                {
                    form.Konto.Text = reader.ReadElementContentAsString();
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NumerBDDCompany")
                {
                    form.NumerBDD.Text = reader.ReadElementContentAsString();
                }
            }
        }
    }
}
