using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderView
/// </summary>
public class OrderView
{
    public int Encyc_id { get; set; }
    public DateTime Openreceiveddate { get; set; }
    public string Collection_title { get; set; }
    public string folio { get; set; }
    public string item_type { get; set; }
    public string itemdtd { get; set; }
    public string title { get; set; }
}
public class OrderViewNews
{

    public string did { get; set; }
    public string INDATE { get; set; }
    public string DECLINATION { get; set; }
    public string CTITLE { get; set; }
    public string DEMANDTYPE { get; set; }
    public string DURATION { get; set; }
    public string ITERATION { get; set; }
    public string PAGECOUNT { get; set; }
    public string STAGE { get; set; }
    public string filename { get; set; }
    public string DUEDATE { get; set; }
    public string remarks { get; set; }
    public string userid { get; set; }
    public string fullname { get; set; }
    public string Delivered_DATE { get; set; }
    public string Author { get; set; }
}
public class OrderViewEncylo
{
    public string  Eid { get; set; }
    public string DTITLE { get; set; }
    public string FOLIO { get; set; }
    public string DEMANDTYPE { get; set; }
    public string ITERATION { get; set; }
    public string INDATE { get; set; }
    public string PAGECOUNT { get; set; }
    public string STAGE { get; set; }
    public string filesname { get; set; }
    public string comments { get; set; }
    public string erid { get; set; }
    public string tdfilename { get; set; }
    public string tat { get; set; }
    public string DUEDATE { get; set; }
    public string userid { get; set; }
    public string fullname { get; set; }
}
public class OrderViewJournal
{
    public string Articleid { get; set; }
    public string jid { get; set; }
    public string aid { get; set; }
    public string ArticleTitle { get; set; }
    public string AuthorName { get; set; }
    public string ArticleType { get; set; }
    public string Publishing_Number { get; set; }
    public string tat { get; set; }
    public string iteration { get; set; }
    public string IN_DATE { get; set; }
    public string DUEDATE { get; set; }
    public string PAGECOUNT { get; set; }
	public string charactercount { get; set; }
    public string STAGE { get; set; }
    public string userid { get; set; }
    public string fullname { get; set; }
    public string filename { get; set; }
    public string tdfilename { get; set; }
    public string comments { get; set; }
    public string Articlerid { get; set; }
    public string Delivery_DATE { get; set; }
    public string journal_Name { get; set; }
    public string WorkTobeDone { get; set; }
    public string Cancel_comment { get; set; }
}
public class OrderViewFiche
{
    public string Fid { get; set; }
    public string Ftitle { get; set; }
    public string FOLIO { get; set; }
    public string DEMANDTYPE { get; set; }
    public string ITERATION { get; set; }
    public string Duration { get; set; }
    public string INDATE { get; set; }
    public string PAGECOUNT { get; set; }
    public string STAGE { get; set; }
    public string filesname { get; set; }
    public string DUEDATE { get; set; }
    public string comments { get; set; }
    public string fullname { get; set; }
    public string tat { get; set; }
    public string userid { get; set; }
    public string FRid { get; set; }
    public string tdfilename { get; set; }
    public string tdcomments { get; set; }
    public string combi { get; set; }
    public string codecoll { get; set; }
    public string nummac { get; set; }
    public string artfas { get; set; }
    public string numfas { get; set; }
    public string Sgm_Filename { get; set; }
    public string DELIVERED_DATE { get; set; }
    public string errorfilename { get; set; }

}