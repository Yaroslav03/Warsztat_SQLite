using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Warsztat
{
    internal class Settings
    {
        XmlTextReader reader = new XmlTextReader("Settings.xml");

        public  void SaveXml(Setting_Form form, string lang)
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
                xmlWrite.WriteStartElement("CompanyData");
                xmlWrite.WriteElementString("NameCompany", form.NameCompany.Text.Trim());
                xmlWrite.WriteElementString("AdresCompany", form.AdresCompany.Text.Trim());
                xmlWrite.WriteElementString("NIPCompany", form.NIP.Text.Trim());
                xmlWrite.WriteElementString("KontoCompany", form.Konto.Text.Trim());
                xmlWrite.WriteElementString("NumerBDOCompany", form.NumerBDO.Text.Trim());

                xmlWrite.WriteEndDocument();
                xmlWrite.Flush();
                xmlWrite.Close();
            });        
        }
        public  void ReadXML(Setting_Form form)
        {
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
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "NumerBDOCompany")
                {
                    form.NumerBDO.Text = reader.ReadElementContentAsString();
                }

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Language")
                {
                    string ukranian = "Ukranian";
                    string polish = "Polish";
                    string lang = reader.ReadElementContentAsString();
                    
                    if (lang == ukranian)
                    {
                        form.Ukranian.Checked = true;
                    }
                    if (lang == polish)
                    {
                        form.Ukranian.Checked = true;
                    }
                }
            }
        }
    }
}
