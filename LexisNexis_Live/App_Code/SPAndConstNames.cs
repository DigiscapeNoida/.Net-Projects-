using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SPAndConstNames
/// </summary>
public static class SPNames
{
    public const string Login = "usp_Login";
    public const string getCollection = "usp_getCollection";
    public const string getinputcategory = "usp_getinputcategory";
    public const string getproduct = "usp_getproduct";
    public const string gettask = "usp_gettask";
    public const string itemtypeEncyclo = "usp_itemtypeEncyclo";
    public const string itemDTDEncyclo = "usp_itemDTDEncyclo";
    public const string demandNature = "usp_demandNature";
    public const string Tat = "usp_Tat";
    public const string Declination = "usp_Declination";
    public const string insertencyclo = "usp_insertencyclo";
    public const string getEncycloid = "usp_getEncycloid";
    public const string getEncycloERID = "usp_getEncycloERID";
    public const string insertdossier = "usp_insertdossier";
    public const string insertdossier_withduedate = "usp_insertdossier_withduedate";
    public const string getdossierid = "usp_getdossierid";
    public const string removeDossierItem = "usp_removeDossierItem";
    public const string AllocteDossierItem = "usp_AllocteDossierItem";

    public const string removeEncycloItem = "usp_removeEncycloItem";
    public const string AllocteEncycloItem = "usp_AllocteEncycloItem";
    public const string getdossier_bystage = "usp_getdossier_bystage";

    public const string getencycloReviseentry = "usp_getencycloReviseentry";

    public const string gettduploadEncyclo = "usp_gettduploadEncyclo";
    public const string tduploadInsertencyclo = "usp_tduploadInsertencyclo";
    public const string getemaildata = "usp_getemaildata";
    public const string getemaildataencyclo = "usp_getemaildataencyclo";
    public const string getdossierforedit = "usp_getdossierforedit";
    public const string updatedossier = "usp_updatedossier";
    public const string getencycloforedit = "usp_getencycloforedit";
    public const string Updateencyclo = "usp_Updateencyclo";

    public const string updateloginfordashboard = "usp_updateloginfordashboard";
    public const string changepassword = "usp_changepassword";
    public const string resetpassword = "usp_resetpassword";
    public const string getdossiertdcomment = "usp_getdossiertdcomment";
    public const string updatedossiertdComment = "usp_updatedossiertdComment";

    public const string getuserbyrole = "usp_getuserbyrole";

    public const string getJournalInfo = "usp_getJournalInfo";
    public const string getmaxaid = "usp_getmaxaid";
    public const string getJournalArticleRID = "usp_getJournalArticleRID";
    public const string insertJournal = "usp_insertJournal";

    public const string removeJournalItem = "usp_removeJournalItem";
    public const string getemaildatajournal = "usp_getemaildatajournal";
    public const string gettduploadjournal = "usp_gettduploadjournal";
    public const string tduploadInsertjournal = "usp_tduploadInsertjournal";
    public const string getJOURNALReviseentry = "usp_getJOURNALReviseentry";

    public const string completejournalItem = "usp_completejournalItem";

    public const string insertMailformemories = "usp_insertMailformemories";
    public const string getmailfromemories = "usp_getmailfromemories";

    public const string updateloginfordashboard_New = "usp_updateloginfordashboard_New";

    public const string FichesFreshFile = "usp_FichesFreshFile";
    public const string updatefiche = "usp_updatefiche";

    public const string gettduploadfiche = "usp_gettduploadfiche";
    public const string tduploadInsertfiche = "usp_tduploadInsertfiche";
    public const string getemaildatafiche = "usp_getemaildatafiche";
    public const string getficheReviseentry = "usp_getficheReviseentry";
    public const string getfichefrid = "usp_getfichefrid";

    public const string insertrevisefiche = "usp_insertrevisefiche";
    public const string AllocteficheItem = "usp_AllocteficheItem";

    public const string removeFicheItem = "usp_removeFicheItem";
 
public const string usp_getUser = "usp_getUser";
    public const string deleteUser = "usp_deleteUser";
    public const string chkuserExist = "usp_chkuserExist";
    public const string createUser = "usp_createUser";

}
