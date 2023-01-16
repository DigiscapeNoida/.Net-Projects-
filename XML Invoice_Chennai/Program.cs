using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xmlinv
{
    internal class Program
    {
        static Log log = new Log();
        static void Main(string[] args)
        {
            foreach (string newPath in Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["in_path"], "*.*", SearchOption.TopDirectoryOnly))
            {
                log.Generatelog("=================================================================");
                log.Generatelog("Processing file " + newPath);
                if (Path.GetExtension(newPath) == ".xls" || Path.GetExtension(newPath) == ".xlsx")
                {
                    //string filepath = @"D:\Deepak\ExcelToXML\input\inv_details.xlsx";
                    bool isok = true;
                    string conn = string.Empty;
                    DataTable dt1 = new DataTable();
                    //DataTable dt1 = new DataTable();
                    //if (Path.GetExtension(newPath) == ".xls")
                    // {
                    conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + newPath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';"; //for below excel 2007  
                                                                                                                                            // }
                                                                                                                                            //else if (Path.GetExtension(newPath) == ".xlsx")
                                                                                                                                            //{
                                                                                                                                                //conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + newPath + ";Extended Properties='Excel 12.0 XML;HDR=YES';"; //for above excel 2007  
                                                                                                                                            //}
                    using (OleDbConnection con = new OleDbConnection(conn))
                    {
                        try
                        {
                            OleDbDataAdapter oleAdpt1 = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                            oleAdpt1.Fill(dt1); //fill excel data into dataTable  
                                                //OleDbDataAdapter oleAdpt2 = new OleDbDataAdapter("select * from [Sheet2$]", con); //here we read data from sheet1  
                                                //oleAdpt2.Fill(dt1);
                        }
                        catch (Exception ex)
                        {
                            log.Generatelog("Error found in file " + newPath + " " + ex.Message.ToString());
                            isok = false;
                        }
                    }
                    if (isok == true)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            try
                            {
                                string doc_name = Path.GetFileNameWithoutExtension(newPath);
                                string xml = "";
                                string[] xml_base1;
                                if (dt1.Rows[1]["jrno"].ToString() == "0")
                                {
                                    xml_base1 = File.ReadAllLines(Application.StartupPath + "\\file1_book.txt");
                                }
                                else
                                {
                                    xml_base1 = File.ReadAllLines(Application.StartupPath + "\\file1_jour.txt");
                                }
                                foreach (string ln in xml_base1)
                                {
                                    xml = xml + ln;
                                }
                                if (System.Configuration.ConfigurationSettings.AppSettings["add_new_message"].Trim().ToLower() == "yes")
                                {
                                    if (dt1.Rows[1]["jrno"].ToString() == "0")
                                    {
                                        xml = xml.Replace("</InvoiceDetailShipping>", "</InvoiceDetailShipping><Extrinsic name=\"NewMessage\">" + System.Configuration.ConfigurationSettings.AppSettings["new_message"] + "</Extrinsic>");
                                    }
                                    else
                                    {
                                        xml = xml.Replace("</InvoiceDetailRequestHeader>", "<Extrinsic name=\"new message\">" + System.Configuration.ConfigurationSettings.AppSettings["new_message"] + "</Extrinsic></InvoiceDetailRequestHeader>");
                                    }
                                }
                                Random rnd=new Random();
                                string payload=rnd.Next(100000000, 999999999).ToString();
                                xml = xml.Replace("<cXML payloadID=\"xxxxx\"", "<cXML payloadID=\""+payload+"\"");
                                decimal total_amount = 0;
                                if (dt1.Rows.Count > 1)
                                {
                                    xml = xml.Replace("timestamp=\"xxxxx\"", "timestamp=\"" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "+05:30" + "\"");
                                    xml = xml.Replace("invoiceDate=\"xxxxx\"", "invoiceDate=\"" + dt1.Rows[1]["documentdate"].ToString().Split('-')[2] + "-" + dt1.Rows[1]["documentdate"].ToString().Split('-')[1] + "-" + dt1.Rows[1]["documentdate"].ToString().Split('-')[0] + DateTime.Now.ToString("'T'HH':'mm':'ss") + "+05:30" + "\"");
                                    xml = xml.Replace("invoiceID=\"MDPEXTxxxxx\"", "invoiceID=\"MDPEXT22" + doc_name + "\"");
                                    //xml = xml.Replace("invoiceID=\"TDGTKEXTxxxxx\"", "invoiceID=\"TDGTKEXT22" + doc_name + "\"");
                                    string c_code = country_code(dt1.Rows[1]["countrycode"].ToString());
                                    //if (c_code == "")
                                    //{
                                    //    log.Generatelog("Country code not found.");
                                    //    return;
                                    //}
                                    if (c_code == "ES")
                                    {
                                        xml = xml.Replace("addressID=\"xxxxx\"", "addressID=\"51055283\"");
                                    }
                                    else if (c_code == "NL")
                                    {
                                        xml = xml.Replace("addressID=\"xxxxx\"", "addressID=\"117445\"");
                                    }
                                    else if (c_code == "DE")
                                    {
                                        xml = xml.Replace("addressID=\"xxxxx\"", "addressID=\"50283771\"");
                                    }
                                    else if (c_code == "US")
                                    {
                                        xml = xml.Replace("addressID=\"xxxxx\"", "addressID=\"49132\"");
                                    }
                                    else if (c_code == "UK")
                                    {
                                        xml = xml.Replace("addressID=\"xxxxx\"", "addressID=\"68687\"");
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    xml = xml.Replace("<Contact role=\"billTo\"><Name xml:lang=\"en-US\">xxxxx</Name><PostalAddress><Street>xxxxx</Street><City>xxxxx</City><State>xxxxx</State><PostalCode>xxxxx</PostalCode><Country isoCountryCode=\"xxxxx\">xxxxx</Country></PostalAddress></Contact>", "<Contact role=\"billTo\"><Name xml:lang=\"en-US\">" + dt1.Rows[1]["entityname"] + "</Name><PostalAddress><Street>" + dt1.Rows[1]["address2"] + "</Street><City>" + dt1.Rows[1]["city"] + "</City><State>" + dt1.Rows[1]["state"] + "</State><PostalCode>" + dt1.Rows[1]["zip"] + "</PostalCode><Country isoCountryCode=\"" + c_code + "\">" + dt1.Rows[1]["countrycode"] + "</Country></PostalAddress></Contact>");
                                   // string abc = dt1.Rows[1]["remarks"].ToString();
                                    string[] dtl = dt1.Rows[1]["remarks"].ToString().Replace("\r\n", " ").Split(' ');
                                    string isbn = dtl[1].Split('\n')[0];
                                    string email = "";
                                    string order_id = "";
                                    bool oid = false;
                                    bool oid_found=false;
                                    foreach (string dtl2 in dtl)
                                    {
                                        if (dtl2.Contains("@"))
                                        {
                                            email = dtl2.Trim();
                                        }
                                        if (dtl2.Trim().Length >0 && oid==true && oid_found==false)
                                        {
                                            order_id = dtl2.Trim();
                                            oid_found = true;
                                        }
                                        if (dtl2.Trim().ToLower()=="order")
                                        {
                                            oid= true;
                                        }
                                    }
                                    if (dt1.Rows[1]["jrno"].ToString() == "0")
                                    {
                                        xml = xml.Replace("<Contact role=\"soldTo\"><Name xml:lang=\"en-US\">xxxxx</Name><PostalAddress><Street>xxxxx</Street><City>xxxxx</City><State>xxxxx</State><PostalCode>xxxxx</PostalCode><Country isoCountryCode=\"xxxxx\">xxxxx</Country></PostalAddress><Email>xxxxx</Email></Contact>", "<Contact role=\"soldTo\"><Name xml:lang=\"en-US\">" + dt1.Rows[1]["entityname"] + "</Name><PostalAddress><Street>" + dt1.Rows[1]["address2"] + "</Street><City>" + dt1.Rows[1]["city"] + "</City><State>" + dt1.Rows[1]["state"] + "</State><PostalCode>" + dt1.Rows[1]["zip"] + "</PostalCode><Country isoCountryCode=\"" + c_code + "\">" + dt1.Rows[1]["countrycode"] + "</Country></PostalAddress><Email>" + email + "</Email></Contact>");
                                        string bllto = Regex.Match(xml, "(<Contact role=\"billTo\">)(.*?)(<\\/Contact>)").Value;
                                        string bllto1 = bllto.Replace("</Contact>", "<Email>" + email + "</Email></Contact>");
                                        xml = xml.Replace(bllto, bllto1);
                                    }
                                    else
                                    {
                                        xml = xml.Replace("<Contact role=\"soldTo\"><Name xml:lang=\"en-US\">xxxxx</Name><PostalAddress><Street>xxxxx</Street><City>xxxxx</City><State>xxxxx</State><PostalCode>xxxxx</PostalCode><Country isoCountryCode=\"xxxxx\">xxxxx</Country></PostalAddress><Email>xxxxx</Email></Contact>", "<Contact role=\"soldTo\"><Name xml:lang=\"en-US\">" + dt1.Rows[1]["entityname"] + "</Name><PostalAddress><Street>" + dt1.Rows[1]["address2"] + "</Street><City>" + dt1.Rows[1]["city"] + "</City><State>" + dt1.Rows[1]["state"] + "</State><PostalCode>" + dt1.Rows[1]["zip"] + "</PostalCode><Country isoCountryCode=\"" + c_code + "\">" + dt1.Rows[1]["countrycode"] + "</Country></PostalAddress></Contact>");
                                    }
                                    xml = xml.Replace("<Contact role=\"shipTo\"><Name xml:lang=\"en-US\">xxxxx</Name><PostalAddress><Street>xxxxx</Street><City>xxxxx</City><State>xxxxx</State><PostalCode>xxxxx</PostalCode><Country isoCountryCode=\"xxxxx\">xxxxx</Country></PostalAddress></Contact>", "<Contact role=\"shipTo\"><Name xml:lang=\"en-US\">" + dt1.Rows[1]["entityname"] + "</Name><PostalAddress><Street>" + dt1.Rows[1]["address2"] + "</Street><City>" + dt1.Rows[1]["city"] + "</City><State>" + dt1.Rows[1]["state"] + "</State><PostalCode>" + dt1.Rows[1]["zip"] + "</PostalCode><Country isoCountryCode=\"" + c_code + "\">" + dt1.Rows[1]["countrycode"] + "</Country></PostalAddress></Contact>");
                                    xml = xml.Replace("<Extrinsic name=\"tag\">xxxxx</Extrinsic>", "<Extrinsic name=\"tag\">" + isbn + "</Extrinsic>");
                                    xml = xml.Replace("<Extrinsic name=\"journalNo\">xxxxx</Extrinsic>", "<Extrinsic name=\"journalNo\">" + dt1.Rows[1]["jrno"].ToString() + "</Extrinsic>");
                                    xml = xml.Replace("<Extrinsic name=\"volumeNo\">xxxxx</Extrinsic>", "<Extrinsic name=\"volumeNo\">" + dt1.Rows[1]["title"].ToString() + "</Extrinsic>");
                                    if (dt1.Rows[1]["jrno"].ToString() != "0")
                                    {
                                        order_id = dt1.Rows[1]["entityref"].ToString();
                                    }
                                    xml = xml.Replace("<InvoiceDetailOrderInfo><OrderIDInfo orderID=\"xxxxx\"/></InvoiceDetailOrderInfo>", "<InvoiceDetailOrderInfo><OrderIDInfo orderID=\"" + order_id + "\"/></InvoiceDetailOrderInfo>");
                                    string[] xml_base2 = File.ReadAllLines(Application.StartupPath + "\\file2.txt");
                                    string item = "";
                                    foreach (string ln1 in xml_base2)
                                    {
                                        item = item + ln1;
                                    }
                                    string items = item;
                                    int line_no = 0;
                                    for (int i = 5; i < dt1.Rows.Count; i++)
                                    {
                                        if (dt1.Rows[i][0] is DBNull)
                                        {
                                            continue;
                                        }
                                        if (dt1.Rows[i][0].ToString().Trim() == "0")
                                        {
                                            continue;
                                        }
                                        line_no = line_no + 1;
                                        total_amount = total_amount + Math.Round(Convert.ToDecimal(dt1.Rows[i][14]) * Convert.ToDecimal(dt1.Rows[i][10]), 2);
                                        string amount = Math.Round(Convert.ToDecimal(dt1.Rows[i][14]) * Convert.ToDecimal(dt1.Rows[i][10]), 2).ToString();
                                        item = item.Replace("<InvoiceDetailItem invoiceLineNumber=\"xxxxx\" quantity=\"xxxxx\">", "<InvoiceDetailItem invoiceLineNumber=\"" + line_no.ToString() + "\" quantity=\"" + dt1.Rows[i][10].ToString().Trim() + "\">");
                                        item = item.Replace("<UnitOfMeasure>xxxxx</UnitOfMeasure>", "<UnitOfMeasure>" + dt1.Rows[i][2].ToString().Trim() + "</UnitOfMeasure>");
                                        item = item.Replace("<UnitPrice><Money currency=\"xxxxx\">xxxxx</Money></UnitPrice>", "<UnitPrice><Money currency=\"" + dt1.Rows[i][12].ToString().Trim() + "\">" + dt1.Rows[i][14].ToString().Trim() + "</Money></UnitPrice>");
                                        item = item.Replace("<InvoiceDetailItemReference lineNumber=\"xxxxx\">", "<InvoiceDetailItemReference lineNumber=\"" + line_no.ToString() + "\">");
                                        if (dt1.Rows[1]["jrno"].ToString() == "0")
                                        {
                                            string ab = dt1.Rows[i][1].ToString();
                                            string ac = ab.Substring(0, ab.IndexOf("-")).Trim();
                                            //string ad = ab.Substring(ab.IndexOf("-") + 1).Trim();
                                            item = item.Replace("<SupplierPartID>xxxxx</SupplierPartID>", "<SupplierPartID>" + isbn + "</SupplierPartID>");
                                            item = item.Replace("<Description xml:lang=\"en-US\">xxxxx</Description>", "<Description xml:lang=\"en-US\">" + ab.Replace(isbn+":","").Replace(isbn + " :", "").Replace(isbn + "-", "").Replace(isbn + " -", "") + "</Description>");
                                        }
                                        else
                                        {
                                            item = item.Replace("<SupplierPartID>xxxxx</SupplierPartID>", "<SupplierPartID>td100036207E9</SupplierPartID>");
                                            item = item.Replace("<Description xml:lang=\"en-US\">xxxxx</Description>", "<Description xml:lang=\"en-US\">" + dt1.Rows[i][1].ToString().Trim() + "</Description>");
                                        }
                                        item = item.Replace("<SubtotalAmount><Money currency=\"xxxxx\">xxxxx</Money></SubtotalAmount>", "<SubtotalAmount><Money currency=\"" + dt1.Rows[i][12].ToString().Trim() + "\">" + amount + "</Money></SubtotalAmount>");
                                        item = item.Replace("<GrossAmount><Money currency=\"xxxxx\">xxxxx</Money></GrossAmount>", "<GrossAmount><Money currency=\"" + dt1.Rows[i][12].ToString().Trim() + "\">" + amount + "</Money></GrossAmount>");
                                        item = item.Replace("<NetAmount><Money currency=\"xxxxx\">xxxxx</Money></NetAmount>", "<NetAmount><Money currency=\"" + dt1.Rows[i][12].ToString().Trim() + "\">" + amount + "</Money></NetAmount>");
                                        //item = item.Replace("<Extrinsic name=\"punchinItemFromCatalog\">xxxxx</Extrinsic>", "<Extrinsic name=\"punchinItemFromCatalog\">" + dt1.Rows[i][5].ToString().Trim() + "</Extrinsic>");
                                        item = item.Replace("<Extrinsic name=\"businessActivity\">xxxxx</Extrinsic>", "<Extrinsic name=\"businessActivity\">" + dt1.Rows[i][5].ToString().Trim() + "</Extrinsic>");
                                        xml = xml + item;
                                        item = items;
                                    }
                                }
                                string[] xml_base3 = File.ReadAllLines(Application.StartupPath + "\\file3.txt");
                                string fl3 = "";
                                foreach (string ln3 in xml_base3)
                                {
                                    fl3 = fl3 + ln3;
                                }
                                fl3 = fl3.Replace("<SubtotalAmount><Money alternateAmount=\"xxxxx\" alternateCurrency=\"xxxxx\" currency=\"xxxxx\">xxxxx</Money></SubtotalAmount>", "<SubtotalAmount><Money alternateAmount=\"" + total_amount.ToString() + "\" alternateCurrency=\"" + dt1.Rows[5][12].ToString() + "\" currency=\"" + dt1.Rows[5][12].ToString() + "\">" + total_amount.ToString() + "</Money></SubtotalAmount>");
                                fl3 = fl3.Replace("<Tax><Money currency=\"xxxxx\">xxxxx</Money><Description xml:lang=\"en-US\"/></Tax>", "<Tax><Money currency=\"" + dt1.Rows[5][12].ToString() + "\">0</Money><Description xml:lang=\"en-US\"/></Tax>");
                                fl3 = fl3.Replace("<GrossAmount><Money currency=\"xxxxx\">xxxxx</Money></GrossAmount>", "<GrossAmount><Money currency=\"" + dt1.Rows[5][12].ToString() + "\">" + total_amount.ToString() + "</Money></GrossAmount>");
                                fl3 = fl3.Replace("<NetAmount><Money currency=\"xxxxx\">xxxxx</Money></NetAmount>", "<NetAmount><Money currency=\"" + dt1.Rows[5][12].ToString() + "\">" + total_amount.ToString() + "</Money></NetAmount>");
                                fl3 = fl3.Replace("<DueAmount><Money alternateAmount=\"xxxxx\" alternateCurrency=\"xxxxx\" currency=\"xxxxx\">xxxxx</Money></DueAmount>", "<DueAmount><Money alternateAmount=\"" + total_amount.ToString() + "\" alternateCurrency=\"" + dt1.Rows[5][12].ToString() + "\" currency=\"" + dt1.Rows[5][12].ToString() + "\">" + total_amount.ToString() + "</Money></DueAmount>");
                                xml = xml + fl3;
                                xml = xml.Replace("><", ">\n<");
                                doc_name = doc_name + "_22" + doc_name + ".xml";
                                if (File.Exists(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + doc_name))
                                {
                                    File.Delete(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + doc_name);
                                }
                                File.AppendAllText(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + doc_name, xml);
                                log.Generatelog("Output XML file created " + doc_name);
                                File.Copy(newPath, System.Configuration.ConfigurationSettings.AppSettings["success"] + Path.GetFileName(newPath), true);
                                File.Delete(newPath);
                                log.Generatelog("Output XML file created " + doc_name);
                                log.Generatelog("Source file moved to success location " + Path.GetFileName(newPath));
                            }
                            catch (Exception ex)
                            {
                                log.Generatelog("Error found in " + newPath + " " + ex.Message.ToString());
                                isok = false;
                            }
                        }
                        else
                        {
                            log.Generatelog("Related data not found in sheet1.");
                            isok = false;
                        }
                    }
                    if (isok == false)
                    {
                        if (File.Exists(newPath))
                        {
                            File.Copy(newPath, System.Configuration.ConfigurationSettings.AppSettings["fail_dir"] + Path.GetFileName(newPath), true);
                            File.Delete(newPath);
                            log.Generatelog("Source file moved to fail location " + Path.GetFileName(newPath));
                        }
                    }
                }
            }
        }
        static string country_code(string country)
        {
            string country_code = "";
            string conn1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + Application.StartupPath + "\\ISO_Country.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';"; //for above excel 2007  
            //string conn1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\ISO_Country.xlsx;Extended Properties='Excel 12.0 XML;HDR=YES';";
            DataTable dt2 = new DataTable();
            using (OleDbConnection con = new OleDbConnection(conn1))
            {
                try
                {
                    OleDbDataAdapter oleAdpt2 = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt2.Fill(dt2);
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i][0].ToString().Trim().ToLower() == country.Trim().ToLower())
                        {
                            country_code = dt2.Rows[i][1].ToString();
                            break;
                        }
                    }
                }
                catch
                { }
            }
            return country_code;
        }
    }
}
