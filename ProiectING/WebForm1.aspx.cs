using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ProiectING
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<CursValutar> cursuri;
        String path = HostingEnvironment.ApplicationPhysicalPath + "\\test.csv";

        protected void Page_Load(object sender, EventArgs e)
        {
            cursuri = descarcaXml("http://www.bnr.ro/nbrfxrates.xml");
            SalvareCsv(path);
            IncarcaCsv(path);
        }

        private List<CursValutar> descarcaXml(String link)
        {
            List<CursValutar> cursuriLocale = new List<CursValutar>();
            XmlDocument doc = CitesteXml(link);
            XmlNodeList elemList = doc.GetElementsByTagName("Rate");

            foreach (XmlNode node in elemList)
            {
                string currency = node.Attributes[0].Value;
                double multiplier = 1;
                try
                {
                    multiplier = Double.Parse(node.Attributes[1].Value);
                }
                catch (Exception ex)
                {

                }
                double value = Double.Parse(node.InnerXml);

                cursuriLocale.Add(new CursValutar(currency, value * multiplier));
            }
            return cursuriLocale;
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            cursuri = descarcaXml(HostingEnvironment.ApplicationPhysicalPath + "\\nbrfxrates.xml");
            //cursuri = descarcaXml("http://www.bnr.ro/nbrfxrates.xml");
            for (int i = 1; i < Table1.Rows.Count; i++)
            {
                CursValutar cursValutar = cursuri[i - 1];
                Table1.Rows[i].Cells[0].Text = cursValutar.GetCurrency();
                double cursVechi = Double.Parse(Table1.Rows[i].Cells[1].Text.ToString());
                Table1.Rows[i].Cells[1].Text = cursValutar.GetValue() + "";
                double diferentaProcentuala = 100 * (cursValutar.GetValue() - cursVechi) / cursVechi;
                if (diferentaProcentuala > 0)
                {
                    Table1.Rows[i].Cells[2].Text = "+";
                }
                else if (diferentaProcentuala == 0)
                {
                    Table1.Rows[i].Cells[2].Text = "*";
                }
                else Table1.Rows[i].Cells[2].Text = "-";
                Table1.Rows[i].Cells[3].Text = String.Format("{0:0.00}%", diferentaProcentuala);
            }

            SalvareCsv(path);
        }

        public XmlDocument CitesteXml(String path)
        {
            WebClient client = new WebClient();
            Stream data = client.OpenRead(path);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            return doc;
        }

        public void SalvareCsv(String fisier)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(fisier, FileMode.Create, FileAccess.Write)))
            {
                foreach (CursValutar curs in cursuri)
                {
                    writer.WriteLine(curs.ToString());
                }
            }
        }

        public void IncarcaCsv(String fisier)
        {
            using (var file = File.OpenRead(fisier))
            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var linie = reader.ReadLine();
                    var content = linie.Split(',');
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Text = content[0];
                    TableCell tableCell2 = new TableCell();
                    tableCell2.Text = content[1];
                    TableCell tableCell3 = new TableCell();
                    tableCell3.Text = "...";
                    TableCell tableCell4 = new TableCell();
                    tableCell4.Text = "...";

                    TableRow row = new TableRow();
                    row.Controls.Add(tableCell1);
                    row.Controls.Add(tableCell2);
                    row.Controls.Add(tableCell3);
                    row.Controls.Add(tableCell4);

                    Table1.Controls.Add(row);
                }
            }
        }
    }
}