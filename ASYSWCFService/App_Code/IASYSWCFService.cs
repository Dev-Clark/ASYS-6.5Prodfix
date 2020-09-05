using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService" in both code and config file together.
[ServiceContract]
[XmlSerializerFormat]
public interface IASYSWCFService
{
    [OperationContract]
    String where_am_I();
    [OperationContract]
    WCFRespALLResult TempoLotNo();
    [OperationContract]
    WCFRespALLResult DisplayBarcodeItems(string DB, string barcode);
    [OperationContract]
    WCFRespALLResult costcenters();
    [OperationContract]
    WCFRespALLResult SaveTradeIN_transaction(string DB, string lotno, string branchCode, int combo1Value, int Month, string lblCode,
        string lbltwelve, string userLog, string[][] ListView);
    [OperationContract]
    WCFRespALLResult updateTRANptn(string DB, string branchCode, string[][] dgEntry, int date, int year);
    [OperationContract]
    WCFRespALLResult SavePTNbyBatch(string DB, string getNewlot, string branchCode, int combo1, int m, string lblBC, string[][] ListView,
        string lbltwelve, string userLog);
    [OperationContract]
    WCFRespALLResult GetResID(string cbValue);
    [OperationContract]
    bool checker(string ptn);
    [OperationContract]
    WCFRespALLResult REMPopulateData(string DB, int month, int year, string branch);
    [OperationContract]
    WCFRespALLResult RetrieveTradeIN_Items(string DB, int month, int year, string branch);
    [OperationContract]
    WCFRespALLResult habwa_date(string DB);
    [OperationContract]
    WCFRespALLResult valEmp();
    ////////////////////////////////////////////////////////////////////////////////////////////
    //[OperationContract]
    //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml,
    //       BodyStyle = WebMessageBodyStyle.Wrapped,
    //       UriTemplate = "log/{userLog}")]////////////////////////////////////////////////// here 
    [OperationContract]
    WCFRespALLResult summaryRPTLoad(string userLog);
    //////////////////////////////////////////////////////////////////////////////////////
    [OperationContract]
    WCFRespALLResult DisplayMultipleSmmryRPT(string DB, string cases, int pmonth, int pyear, string pname, string month, string year,
        DateTime habwadate, string zone, int month2, int year2);
    [OperationContract]
    WCFRespALLResult getBranchLotno(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult RetrievePTN_Barcode(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult RetrieveInfoREM(string DB, string whichSet, string ptn);
    [OperationContract]
    WCFRespALLResult RetrieveInfoREM2(string db, string whichSet, string ptn);
    [OperationContract]
    WCFRespALLResult RetrieveInfoREM3(string db, string ptn);
    [OperationContract]
    WCFRespALLResult SelectFrmBedryf(string DB, string branchName);
    [OperationContract]
    WCFRespALLResult DisplayDetails(string DB, string ptn, string whichCk);
    [OperationContract]
    WCFRespALLResult DisplayDetails2(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult getBedryf1(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult saveReceivingIndividually(string DB, string[][] ListView, string branchCode, string branchname, string lblRegion,
        string lblArea, string txt15, string txt16, double lbl31, string lbl36, string lbl10, string lbl23, string lbl24, string lbl25, string lbl26,
        string lbl27, string lbl28, string lbl29, string lbl30, string lbl32, string blotno, string combo4, string lbl22, double lbl33,
        string lblReceiver);
    [OperationContract]
    WCFRespALLResult EditReceivingInDividually(string DB, string lblReceiver, string combo4, string txt15);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveStats(string DB, string whichSet, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails2(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails3(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails4(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails5(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails6(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveDetails7(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveActClass(string DB);
    [OperationContract]
    WCFRespALLResult PTNQueryRetrieveActClass2(string DB, string sortCode);
    [OperationContract]
    WCFRespALLResult RetrievePTN_Barcode2(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult DisplayBranchName(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult DisplayBranchCode(string DB, string branchName);
    [OperationContract]
    WCFRespALLResult stringdate_maturity(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult stringdate_expiry(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult stringdate_loan(string DB, string ptn);
    [OperationContract]
    WCFRespALLResult Selectbranch(string DB, string branchName);
    [OperationContract]
    WCFRespALLResult SortLoad();
    [OperationContract]
    WCFRespALLResult getPTNData(string DB, string comboPTN);
    [OperationContract]
    WCFRespALLResult GetPTNDoData(string DB, string comboPTN);
    [OperationContract]
    WCFRespALLResult generateAllBarcode(string barcode);
    [OperationContract]
    WCFRespALLResult generateAllBarcode2(string barcode);
    [OperationContract]
    WCFRespALLResult generateAllBarcode3(string barcode);
    [OperationContract]
    WCFRespALLResult getBranchPTN_PTNbarcode(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult getPTNBarcodeData(string DB, string PTN);
    [OperationContract]
    WCFRespALLResult getPTNBarcodeData2(string DB, string cmbPTN);
    [OperationContract]
    WCFRespALLResult checkBarcodeIfExist(string barcode);
    [OperationContract]
    WCFRespALLResult saveSortData(string DB, string[][] Listview, string ActionClass, string sortcode, string userLog, string PTN, string typex,
        string lotno);
    [OperationContract]
    WCFRespALLResult saveSortData2(string DB, string[][] Listview, string ActionClass, string sortcode, string userLog, string PTN, string typex,
        string lotno);
    [OperationContract]
    WCFRespALLResult SelectBedryfByBranchCode(string DB, string branchCode);
    [OperationContract]
    WCFRespALLResult SelectBedryfByBranchName(string branchName, string DB);
    [OperationContract]
    WCFRespALLResult OnLoadEdit();
    [OperationContract]
    WCFRespALLResult saveEditData(string DB, string typeClass, string[][] ListView, string cmbAction, string sortcode, string userLog, string ptn,
        string sortLotno);
    [OperationContract]
    WCFRespALLResult SavePhoto(string ptn, string ifPTNVisible, string txt1, string DB, byte[] photoname, String unit_address);
    [OperationContract]
    WCFRespALLResult SelectPhoto(string ptn,String zone);
    [OperationContract]
    WCFRespALLResult GetBarcodePhoto(string barcode);
    [OperationContract]
    WCFRespALLResult GetBarcodePhoto2(string barcode);
    [OperationContract]
    WCFRespALLResult GetSources();
    [OperationContract]
    WCFRespALLResult SortedListLoad(string DB);
    [OperationContract]
    WCFRespALLResult SortBedryfByBranchCode(string DB, string bCode);
    [OperationContract]
    WCFRespALLResult SortedListReport(string DB, string userLog, string branchCode, int date, int comboyYear, string rdCheck, string zone);
    [OperationContract]
    WCFRespALLResult MemoLoad(string jobTitle);
    [OperationContract]
    WCFRespALLResult MemoLoad2(string DB);
    [OperationContract]
    WCFRespALLResult MultipleMemoRPTLoad(string DB, int month, string year, string name, string whichMemo);
    [OperationContract]
    WCFRespALLResult MultipleProcessRPTLoad(string DB, string process, int month, int year);
    [OperationContract]
    WCFRespALLResult JewelrySummaryRPTLoad(string DB, int month, int year);
    [OperationContract]
    WCFRespALLResult ProcessSummaryRPTLoad(string DB, int month, int year, string process);
    [OperationContract]
    WCFRespALLResult ComparativeReports(string DB, DateTime habwa, string whichComparative);
    [OperationContract]
    WCFRespALLResult NewRematadoReports(string DB, string whichReport, DateTime habwa0, DateTime habwa1, DateTime habwa3, DateTime prevmonth3,
        DateTime habwa5, DateTime prevmonth5);
    [OperationContract]
    WCFRespALLResult retreiveActionClass(string actionType);
    [OperationContract]
    WCFRespALLResult SaveAction(string T1, double T2, double T3, double T4, double T5);
    [OperationContract]
    WCFRespALLResult EditAction(string T1, double T2, double T3, double T4, double T5, string T6);
    [OperationContract]
    WCFRespALLResult DeleteAction(string T6);
    [OperationContract]
    WCFRespALLResult retreivesortclass();
    [OperationContract]
    WCFRespALLResult SaveJewelryClass(string T1, string T2);
    [OperationContract]
    WCFRespALLResult EditJewelryClass(string T1, string T2, string T3);
    [OperationContract]
    WCFRespALLResult DeleteJewelryClass(string T3);
    [OperationContract]
    WCFRespALLResult SearchBranch(string DB, string branchCode, string branchName, string region, string press);
    [OperationContract]
    WCFRespALLResult SearchBranchLoad(string DB);

    [OperationContract]
    WCFRespALLResult RetreiveBarcodeHistory(string barcode);
    [OperationContract]
    WCFRespALLResult GetBarcodePhotos(string barcode);
 
 
    [OperationContract]
    WCFRespALLResult BarcodeHistory(string barcode);
    [OperationContract]
    WCFRespALLResult ViewHistory(string barcode);
    [OperationContract]
    WCFRespALLResult CheckBarcode(string barcode);
    [OperationContract]
    WCFRespALLResult CheckAvailableBarcode(string barcode, string midBarcode);
    [OperationContract]
    WCFRespALLResult SaveEditedBarcode(string[][] ListView);
    [OperationContract]
    WCFRespALLResult Bytestoimage(byte[] bytes, string allbarcode, bool flag);
    [OperationContract]
    WCFRespALLResult getphoto(string allbarcode);
    [OperationContract]
    WCFRespALLResult getphotoptn(string allbarcode);
}

[DataContract]
public class Connection
{
    #region DEclaration    
    private SqlConnection connection = new SqlConnection();
    private SqlConnection connection1 = new SqlConnection();
    private SqlConnection connection2 = new SqlConnection();
    private SqlConnection connection3 = new SqlConnection();
    private SqlConnection connection4 = new SqlConnection();
    private SqlDataReader dr;
    private SqlDataReader dr1;
    private SqlDataReader dr2;
    private SqlDataReader dr3;
    private SqlDataReader dr4;
    private SqlCommand command = new SqlCommand();
    private SqlCommand command1 = new SqlCommand();
    private SqlCommand command2 = new SqlCommand();
    private SqlCommand command3 = new SqlCommand();
    private SqlCommand command4 = new SqlCommand();
    private SqlTransaction tranCon;
    private SqlTransaction tranCon1;
    private SqlTransaction tranCon2;
    private SqlTransaction tranCon3;
    private SqlTransaction tranCon4;
    //private SqlDataAdapter da;
    //private string conString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    [DataMember]
    public string sDatasource1 { get; set; }
    [DataMember]
    public string sDatabase1 { get; set; }
    [DataMember]
    public string sUsername1 { get; set; }
    [DataMember]
    public string sPassword1 { get; set; }
    [DataMember]
    public string sDatasource2 { get; set; }
    [DataMember]
    public string sDatabase2 { get; set; }
    [DataMember]
    public string sUsername2 { get; set; }
    [DataMember]
    public string sPassword2 { get; set; }
    [DataMember]
    public string sDatasource3 { get; set; }
    [DataMember]
    public string sDatabase3 { get; set; }
    [DataMember]
    public string sUsername3 { get; set; }
    [DataMember]
    public string sPassword3 { get; set; }

    public string conString { get; set; }
    public string conString1 { get; set; }
    public string conString2 { get; set; }
    public string conString3 { get; set; }
    public string conString4 { get; set; }
    //------------------------------------Module---------------------------------//
    [DataMember]
    public static string sDB { get; set; }
    [DataMember]
    public static string dserver { get; set; }
    [DataMember]
    public static string humres2 { get; set; }
    [DataMember]
    public static string bedryf2 { get; set; }
    [DataMember]
    public static string bedryfLuzon { get; set; }
    [DataMember]
    public static string bedryfVisayas { get; set; }
    [DataMember]
    public static string bedryfShowroom { get; set; }
    [DataMember]
    public static string bedryfMindanao { get; set; }
    [DataMember]
    public static string bedryfLNCR { get; set; }
    [DataMember]
    public static string myregion { get; set; }
    [DataMember]
    public static string photodes { get; set; }
    [DataMember]
    public static string LImageDestination { get; set; }
    [DataMember]
    public static string XMLSource { get; set; }
    [DataMember]
    public static string SPassword { get; set; }
    [DataMember]
    public static string LImageSource { get; set; }
    [DataMember]
    public static string SUsername { get; set; }
    [DataMember]
    public static string XMLDestination { get; set; }
    [DataMember]
    public static string JewelrySDestination { get; set; }
    [DataMember]
    public static string GoodstockSDestination { get; set; }
    [DataMember]
    public static string ImportFolderWC { get; set; }
    [DataMember]
    public static string ImportFolderJW { get; set; }
    [DataMember]
    public static string ARJDestination { get; set; }
    [DataMember]
    public static string ImportExportServer { get; set; }
    [DataMember]
    public static string ExportFolderBackup { get; set; }
    [DataMember]
    string sPath = AppDomain.CurrentDomain.BaseDirectory;
    //-----------------------------Module---------------------------//
    //--------------------------
    #endregion
    #region Con

    public void Connection0()
    {
        //string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();

        StreamReader sr = new StreamReader((sPath + "\\REMSystem1.INI"));
        string Key_Server1;
        string Key_Database1;
        string Key_User1;
        string Key_Pass1;
        string Key_Humres;
        string Key_Bedryf;
        string Key_BedryfL;
        string Key_BedryfVis;
        string Key_BedryfS;
        string Key_BedryfM;
        string Key_BedryfLNCR;
        Key_Server1 = "[Server]=";
        Key_User1 = "[User]=";
        Key_Database1 = "[Database]=";
        Key_Pass1 = "[Password]=";
        Key_Humres = "[Humres]=";
        Key_Bedryf = "[Bedryf]=";
        Key_BedryfL = "[BedryfL]=";
        Key_BedryfVis = "[BedryfVis]=";
        Key_BedryfS = "[BedryfS]=";
        Key_BedryfM = "[BedryfM]=";
        Key_BedryfLNCR = "[BedryfLNCR]=";
        string line = sr.ReadLine();
        while (!(line == null))
        {
            line.Replace(" =", "=").Replace("= ", "=");
            if (line.StartsWith(Key_Server1))
            {
                sDatasource1 = line.Replace(Key_Server1, "");
                dserver = sDatasource1;
            }
            if (line.StartsWith(Key_Database1))
            {
                sDatabase1 = line.Replace(Key_Database1, "");
            }
            if (line.StartsWith(Key_User1))
            {
                sUsername1 = line.Replace(Key_User1, "");
            }
            if (line.StartsWith(Key_Pass1))
            {
                sPassword1 = line.Replace(Key_Pass1, "");
            }
            if (line.StartsWith(Key_Humres))
            {
                humres2 = line.Replace(Key_Humres, "");
            }
            if (line.StartsWith(Key_Bedryf))
            {
                bedryf2 = line.Replace(Key_Bedryf, "");
            }
            if (line.StartsWith(Key_BedryfL))
            {
                bedryfLuzon = line.Replace(Key_BedryfL, "");
            }
            if (line.StartsWith(Key_BedryfVis))
            {
                bedryfVisayas = line.Replace(Key_BedryfVis, "");
            }
            if (line.StartsWith(Key_BedryfS))
            {
                bedryfShowroom = line.Replace(Key_BedryfS, "");
            }
            if (line.StartsWith(Key_BedryfM))
            {
                bedryfMindanao = line.Replace(Key_BedryfM, "");
            }
            if (line.StartsWith(Key_BedryfLNCR))
            {
                bedryfLNCR = line.Replace(Key_BedryfLNCR, "");
            }
            line = sr.ReadLine();
        }

        sr.Close();
        conString = "Data Source=" + sDatasource1 + "; Initial Catalog=" + sDatabase1 + "; User ID=" + sUsername1 + "; Password=" + sPassword1;
    }
    #endregion
    #region Con1
    public void Connection1(string DB)
    {

        string Key_Server1 = string.Empty;
        string Key_DataBase1 = string.Empty;
        string Key_User1 = string.Empty;
        string Key_Pass1 = string.Empty;
        StreamReader sr = new StreamReader((sPath + "\\REMSystem.INI"));

        if ((DB == ""))
        {
            sDB = DB;
        }
        else if (DB == "NCR" || DB == "LNCR")
        {
            sDB = "LNCR";
        }
        else if (DB != null)
        {
            sDB = DB;
        }
        switch (sDB.ToUpper())
        {
            case "LUZON":
                Key_Server1 = "[ServerL]=";
                Key_DataBase1 = "[DataBaseL]=";
                Key_User1 = "[UserL]=";
                Key_Pass1 = "[PasswordL]=";
                break;
            case "VISAYAS":
                Key_Server1 = "[ServerVis]=";
                Key_DataBase1 = "[DataBaseVis]=";
                Key_User1 = "[UserVis]=";
                Key_Pass1 = "[PasswordVis]=";
                break;
            case "MINDANAO":
                Key_Server1 = "[ServerM]=";
                Key_DataBase1 = "[DataBaseM]=";
                Key_User1 = "[UserM]=";
                Key_Pass1 = "[PasswordM]=";
                break;
            case "SHOWROOM":
                Key_Server1 = "[ServerS]=";
                Key_DataBase1 = "[DataBaseS]=";
                Key_User1 = "[UserS]=";
                Key_Pass1 = "[PasswordS]=";
                break;
            case "LNCR":
                Key_Server1 = "[ServerLNCR]=";
                Key_DataBase1 = "[DataBaseLNCR]=";
                Key_User1 = "[UserLNCR]=";
                Key_Pass1 = "[PasswordLNCR]=";
                break;
        }
        string line = sr.ReadLine();
        while (!(line == null))
        {
            line.Replace(" =", "=").Replace("= ", "=");
            if (line.StartsWith(Key_Server1))
            {
                sDatasource1 = line.Replace(Key_Server1, "");
            }

            if (line.StartsWith(Key_DataBase1))
            {
                sDatabase1 = line.Replace(Key_DataBase1, "");
            }

            if (line.StartsWith(Key_User1))
            {
                sUsername1 = line.Replace(Key_User1, "");
            }

            if (line.StartsWith(Key_Pass1))
            {
                sPassword1 = line.Replace(Key_Pass1, "");
            }

            line = sr.ReadLine();
        }

        sr.Close();
        conString1 = "Data Source=" + sDatasource1 + "; Initial Catalog=" + sDatabase1 + "; User ID=" + sUsername1 + "; Password=" + sPassword1;
    }
    #endregion
    #region Con2
    public void Connection2(string DB)
    {

        StreamReader sr = new StreamReader((sPath + "\\REMSystem.INI"));
        string Key_Server1 = string.Empty;
        string Key_DataBase1 = string.Empty;
        string Key_User1 = string.Empty;
        string Key_Pass1 = string.Empty;

        if ((DB == ""))
        {
            sDB = DB;
        }
        else if (DB != null)
        {
            sDB = DB;
        }
        switch (sDB.ToUpper())
        {
            case "LUZON":
                Key_Server1 = "[ServerL]=";
                Key_DataBase1 = "[DataBaseL]=";
                Key_User1 = "[UserL]=";
                Key_Pass1 = "[PasswordL]=";
                break;
            case "VISAYAS":
                Key_Server1 = "[ServerVis]=";
                Key_DataBase1 = "[DataBaseVis]=";
                Key_User1 = "[UserVis]=";
                Key_Pass1 = "[PasswordVis]=";
                break;
            case "MINDANAO":
                Key_Server1 = "[ServerM]=";
                Key_DataBase1 = "[DataBaseM]=";
                Key_User1 = "[UserM]=";
                Key_Pass1 = "[PasswordM]=";
                break;
            case "SHOWROOM":
                Key_Server1 = "[ServerS]=";
                Key_DataBase1 = "[DataBaseS]=";
                Key_User1 = "[UserS]=";
                Key_Pass1 = "[PasswordS]=";
                break;
        }
        string line = sr.ReadLine();
        while (!(line == null))
        {
            line.Replace(" =", "=").Replace("= ", "=");
            if (line.StartsWith(Key_Server1))
            {
                sDatasource1 = line.Replace(Key_Server1, "");
            }

            if (line.StartsWith(Key_DataBase1))
            {
                sDatabase1 = line.Replace(Key_DataBase1, "");
            }

            if (line.StartsWith(Key_User1))
            {
                sUsername1 = line.Replace(Key_User1, "");
            }

            if (line.StartsWith(Key_Pass1))
            {
                sPassword1 = line.Replace(Key_Pass1, ""); //decryptPass(line.Replace(Key_Pass1, ""));
            }

            line = sr.ReadLine();
        }

        sr.Close();
        conString2 = "Data Source=" + sDatasource1 + "; Initial Catalog=" + sDatabase1 + "; User ID=" + sUsername1 + "; Password=" + sPassword1;

    }
    #endregion
    #region Con3
    public void Connection3()
    {

        StreamReader sr = new StreamReader((sPath + "\\REMS.INI"));
        string Key_Server1;
        string Key_DataBase1;
        string Key_User1;
        string Key_Pass1;
        string Key_Photo;
        string Key_LImageDestination;
        string Key_LImageSource;
        string Key_JewelrySDestination;
        string Key_GoodstockSDestination;
        string Key_SUsername;
        string Key_SPassword;
        string Key_XMLSource;
        string Key_XMLDestination;
        string Key_ARJDestination;
        string Key_ImportExportServer;
        string Key_ImportFolderJW;
        string Key_ImportFolderWC;
        string Key_ExportFolderBackup;
        Key_Server1 = "[Server]=";
        Key_DataBase1 = "[DataBase]=";
        Key_User1 = "[User]=";
        Key_Pass1 = "[Password]=";
        Key_Photo = "[Photo]=";
        Key_LImageDestination = "[LImageDestination]=";
        Key_LImageSource = "[LImageSource]=";
        Key_JewelrySDestination = "[JewelrySDestination]=";
        Key_GoodstockSDestination = "[GoodstockSDes]=";
        Key_SUsername = "[SUsername]=";
        Key_SPassword = "[SPassword]=";
        Key_XMLSource = "[XMLSource]=";
        Key_XMLDestination = "[XMLDestination]=";
        Key_ARJDestination = "[ARJDestination]=";
        Key_ImportExportServer = "[ImportExportServer]=";
        Key_ImportFolderJW = "[ImportFolderJW]=";
        Key_ImportFolderWC = "[ImportFolderWC]=";
        Key_ExportFolderBackup = "[ExportFolderBackUp]=";
        string line = sr.ReadLine();
        while (!(line == null))
        {
            line.Replace(" =", "=").Replace("= ", "=");
            if (line.StartsWith(Key_Server1))
            {
                sDatasource1 = line.Replace(Key_Server1, "");
            }
            if (line.StartsWith(Key_DataBase1))
            {
                sDatabase1 = line.Replace(Key_DataBase1, "");
            }
            if (line.StartsWith(Key_User1))
            {
                sUsername1 = line.Replace(Key_User1, "");
            }
            if (line.StartsWith(Key_Pass1))
            {
                sPassword1 = line.Replace(Key_Pass1, "");
            }
            if (line.StartsWith(Key_Photo))
            {
                photodes = line.Replace(Key_Photo, "");
            }
            if (line.StartsWith(Key_LImageDestination))
            {
                LImageDestination = line.Replace(Key_LImageDestination, "");
            }
            if (line.StartsWith(Key_LImageSource))
            {
                LImageSource = line.Replace(Key_LImageSource, "");
            }
            if (line.StartsWith(Key_JewelrySDestination))
            {
                JewelrySDestination = line.Replace(Key_JewelrySDestination, "");
            }
            if (line.StartsWith(Key_GoodstockSDestination))
            {
                GoodstockSDestination = line.Replace(Key_GoodstockSDestination, "");
            }
            if (line.StartsWith(Key_SUsername))
            {
                SUsername = line.Replace(Key_SUsername, "");
            }

            if (line.StartsWith(Key_SPassword))
            {
                SPassword = line.Replace(Key_SPassword, "");
            }
            if (line.StartsWith(Key_XMLSource))
            {
                XMLSource = line.Replace(Key_XMLSource, "");
            }
            if (line.StartsWith(Key_XMLDestination))
            {
                XMLDestination = line.Replace(Key_XMLDestination, "");
            }
            if (line.StartsWith(Key_ARJDestination))
            {
                ARJDestination = line.Replace(Key_ARJDestination, "");
            }
            if (line.StartsWith(Key_ImportExportServer))
            {
                ImportExportServer = line.Replace(Key_ImportExportServer, "");
            }
            if (line.StartsWith(Key_ImportFolderJW))
            {
                ImportFolderJW = line.Replace(Key_ImportFolderJW, "");
            }
            if (line.StartsWith(Key_ImportFolderWC))
            {
                ImportFolderWC = line.Replace(Key_ImportFolderWC, "");
            }

            if (line.StartsWith(Key_ExportFolderBackup))
            {
                ExportFolderBackup = line.Replace(Key_ExportFolderBackup, "");
            }

            line = sr.ReadLine();

        }
        sr.Close();
        conString3 = "Data Source=" + sDatasource1 + "; Initial Catalog=" + sDatabase1 + "; User ID=" + sUsername1 + "; Password=" + sPassword1;
    }
    #endregion
    #region Con4
    public void Connection4()
    {
        StreamReader sr = new StreamReader((sPath + "\\REMS.INI"));
        string Key_Server1;
        string Key_DataBase1;
        string Key_User1;
        string Key_Pass1;
        Key_Server1 = "[Server]=";
        Key_DataBase1 = "[DataBase]=";
        Key_User1 = "[User]=";
        Key_Pass1 = "[Password]=";
        string line = sr.ReadLine();
        while (!(line == null))
        {
            line.Replace(" =", "=").Replace("= ", "=");
            if (line.StartsWith(Key_Server1))
            {
                sDatasource1 = line.Replace(Key_Server1, "");
            }

            if (line.StartsWith(Key_DataBase1))
            {
                sDatabase1 = line.Replace(Key_DataBase1, "");
            }

            if (line.StartsWith(Key_User1))
            {
                sUsername1 = line.Replace(Key_User1, "");
            }

            if (line.StartsWith(Key_Pass1))
            {
                sPassword1 = line.Replace(Key_Pass1, "");//decryptPass(line.Replace(Key_Pass1, ""));
            }

            line = sr.ReadLine();
        }

        sr.Close();
        conString4 = "Data Source=" + sDatasource1 + "; Initial Catalog=" + sDatabase1 + "; User ID=" + sUsername1 + "; Password=" + sPassword1;
    }
    #endregion
    #region Decrypt
    string decryptPass(string RawStr)
    {
        int i;
        string decryptedPass;
        i = 3;
        decryptedPass = "";
        while ((i < RawStr.Length))
        {
            decryptedPass = (decryptedPass + RawStr.Substring((i - 1), 1));
            i = this.NextPrime(i);
        }

        return decryptedPass;
    }
    int NextPrime(int i)
    {
        int ctr;
        ctr = (i + 1);
        while (!this.isPrime(ctr))
        {
            ctr = (ctr + 1);
        }

        return ctr;
    }

    bool isPrime(int i)
    {
        if (((i == 3)
                    || (i == 5)))
        {
            return true;
        }

        if (((i % 2)
                    == 0))
        {
            return false;
        }

        if (((i % 3)
                    == 0))
        {
            return false;
        }

        if (((i % 5)
                    == 0))
        {
            return false;
        }

        return true;
    }
    #endregion
    //------Connection
    #region Connections
    public void OpenConn()
    {
        connection.ConnectionString = conString;
        connection.Open();
    }
    public void CloseConn()
    {
        connection.Close();
    }
    //------End Connection     
    //-----------DataReader
    public SqlDataReader ExecuteDr()
    {
        dr = command.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;
    }
    public void CloseDr()
    {
        dr.Close();
    }
    //-----------End DataReader
    public void commandTraxParam(string query)
    {
        command.Connection = connection;
        command.Transaction = tranCon;
        command.CommandTimeout = 600;
        command.CommandText = query;
    }
    public void commandExeParam(string query)
    {
        command = connection.CreateCommand();
        command.CommandTimeout = 600;
        command.CommandText = query;
    }
    public void commandExeStoredParam(string query)
    {
        command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 600;
        command.CommandText = query;
    }
    //---------Transaction
    public void BeginTransax()
    {
        tranCon = connection.BeginTransaction();
        command = connection.CreateCommand();
    }
    public void commitTransax()
    {
        tranCon.Commit();
    }
    public void RollBackTrax()
    {
        tranCon.Rollback();
    }
    //---------End Transaction
    public void Execute()
    {
        command.ExecuteNonQuery();
    }
    //--------Command Paramaters
    #endregion
    //--------------
    #region FirstConnections
    public void OpenConn1()
    {
        connection1.ConnectionString = conString1;
        connection1.Open();
    }
    public void CloseConn1()
    {
        connection1.Close();
    }
    //------End Connection     
    //-----------DataReader
    public SqlDataReader ExecuteDr1()
    {
        dr1 = command1.ExecuteReader();

        return dr1;
    }
    public void CloseDr1()
    {
        dr1.Close();
    }
    //-----------End DataReader
    public void commandTraxParam1(string query)
    {
        command1.Connection = connection1;
        command1.Transaction = tranCon1;
        command1.CommandTimeout = 600;
        command1.CommandText = query;
    }
    public void commandExeParam1(string query)
    {
        command1 = connection1.CreateCommand();
        command1.CommandTimeout = 600;
        command1.CommandText = query;
    }
    public void commandExeStoredParam1(string query)
    {
        command1 = connection1.CreateCommand();
        command1.CommandType = CommandType.StoredProcedure;
        command1.CommandTimeout = 600;
        command1.CommandText = query;
    }
    public void commandExeStoredTraxParam1(string query)
    {
        command1 = connection1.CreateCommand();
        command1.Transaction = tranCon1;
        command1.CommandType = CommandType.StoredProcedure;
        command1.CommandTimeout = 600;
        command1.CommandText = query;
    }
    //---------Transaction
    public void BeginTransax1()
    {
        tranCon1 = connection1.BeginTransaction();
        command1 = connection1.CreateCommand();
    }
    public SqlTransaction BeginTransax12() {
        tranCon1 = connection1.BeginTransaction();
        command1 = connection1.CreateCommand();
        return tranCon1;
    }
    public void commitTransax1()
    {
        tranCon1.Commit();
    }
    public void RollBackTrax1()
    {
        tranCon1.Rollback();
    }
    //---------End Transaction
    public void Execute1()
    {
        command1.ExecuteNonQuery();
    }
    //--------Command Paramaters
    #endregion

    #region SecondConnections
    public void OpenConn2()
    {
        connection2.ConnectionString = conString2;
        connection2.Open();
    }
    public void CloseConn2()
    {
        connection2.Close();
    }
    //------End Connection     
    //-----------DataReader
    public SqlDataReader ExecuteDr2()
    {
        dr2 = command2.ExecuteReader(CommandBehavior.CloseConnection);

        return dr2;
    }
    public void CloseDr2()
    {
        dr2.Close();
    }
    //-----------End DataReader
    public void commandTraxParam2(string query)
    {
        command2.Connection = connection2;
        command2.Transaction = tranCon2;
        command2.CommandTimeout = 600;
        command2.CommandText = query;
    }
    public void commandExeParam2(string query)
    {
        command2 = connection2.CreateCommand();
        command2.CommandTimeout = 600;
        command2.CommandText = query;
    }
    public void commandExeStoredParam2(string query)
    {
        command2 = connection2.CreateCommand();
        command2.CommandType = CommandType.StoredProcedure;
        command2.CommandTimeout = 600;
        command2.CommandText = query;
    }
    //---------Transaction
    public void BeginTransax2()
    {
        tranCon2 = connection2.BeginTransaction();
        command2 = connection2.CreateCommand();
    }
    public void commitTransax2()
    {
        tranCon2.Commit();
    }
    public void RollBackTrax2()
    {
        tranCon2.Rollback();
    }
    //---------End Transaction
    public void Execute2()
    {
        command2.ExecuteNonQuery();
    }
    //--------Command Paramaters
    #endregion

    #region ThirdConnections
    public void OpenConn3()
    {
        connection3.ConnectionString = conString3;
        connection3.Open();
    }
    public void CloseConn3()
    {
        connection3.Close();
    }
    //------End Connection     
    //-----------DataReader
    public SqlDataReader ExecuteDr3()
    {
       
            dr3 = command3.ExecuteReader(CommandBehavior.CloseConnection);

            return dr3;
     
    }
    public void CloseDr3()
    {
        dr3.Close();
    }
    //-----------End DataReader
    public void commandTraxParam3(string query)
    {
        command3.Connection = connection3;
        command3.Transaction = tranCon3;
        command3.CommandTimeout = 600;
        command3.CommandText = query;
    }
    public void commandStoredTraxParam3(string query)
    {
        command3.Connection = connection3;
        command3.Transaction = tranCon3;
        command3.CommandType = CommandType.StoredProcedure;
        command3.CommandTimeout = 600;
        command3.CommandText = query;
    }
    public void commandExeParam3(string query)
    {
        try
        {
            command3 = connection3.CreateCommand();
            command3.CommandTimeout = 600;
            command3.CommandText = query;
        }
        catch (Exception ex )
        {
            
            //throw;
        }
       
    }
    public void commandExeStoredParam3(string query)
    {
        command3 = connection3.CreateCommand();
        command3.CommandType = CommandType.StoredProcedure;
        command3.CommandTimeout = 600;
        command3.CommandText = query;
    }
    public void PhotoParam(string param, string param2, string param3, string param4, string param5, string data, string data2, string data3,
        string data4,string data5)
    {
        command3.Parameters.AddWithValue(param, data);
        command3.Parameters.AddWithValue(param2, data2);
        command3.Parameters.AddWithValue(param3, data3);
        command3.Parameters.AddWithValue(param4, data4);
        command3.Parameters.AddWithValue(param5, data5);
    }
    public void SortReport(string param, string param2, string param3, string param4, string param5, int data, int data2, int data3,
        string data4, string data5)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
        command1.Parameters.AddWithValue(param3, data3);
        command1.Parameters.AddWithValue(param4, data4);
        command1.Parameters.AddWithValue(param5, data5);        
    }
    public void SummaryParams3(string param,string param2,string param3,int data,int data2,string data3)
    {
        command3.Parameters.AddWithValue(param,data);
        command3.Parameters.AddWithValue(param2,data2);
        command3.Parameters.AddWithValue(param3,data3);
    }
    public void SummaryParams35(string param, string param2, int data, int data2)
    {
        command3.Parameters.AddWithValue(param, data);
        command3.Parameters.AddWithValue(param2, data2);        
    }
    public void SummaryParams356(string param, string param2, int data, int data2)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
    }
    public void SummaryParams32(string param, string param2, string data, string data2)
    {
        command3.Parameters.AddWithValue(param, data);
        command3.Parameters.AddWithValue(param2, data2);        
    }
    public void SummaryParams321(string param, string param2, DateTime data, string data2)
    {
        command3.Parameters.AddWithValue(param, data);
        command3.Parameters.AddWithValue(param2, data2);
    }
    public void SummaryParams331(string param, string param2, DateTime data, string data2)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
    }
    public void SummaryParams1(string param, string param2, string param3, int data, int data2, string data3)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
        command1.Parameters.AddWithValue(param3, data3);
    }
    public void SummaryParams12(string param, string param2, string param3, int data, string data2, string data3)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
        command1.Parameters.AddWithValue(param3, data3);
    }
    public void SummaryParams12(string param, string param2, string data, string data2)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);        
    }
    public void REportsComparative(string param, DateTime data)
    {
        command1.Parameters.AddWithValue(param, data);        
    }
    public void REportsComparative2(string param, string param2, DateTime data, DateTime data2)
    {
        command1.Parameters.AddWithValue(param, data);
        command1.Parameters.AddWithValue(param2, data2);
    }
    
    //---------Transaction
    public void BeginTransax3()
    {
        tranCon3 = connection3.BeginTransaction();
        command3 = connection3.CreateCommand();
    }
    public void commitTransax3()
    {
        tranCon3.Commit();
    }
    public void RollBackTrax3()
    {
        tranCon3.Rollback();
    }
    //---------End Transaction
    public void Execute3()
    {
        command3.ExecuteNonQuery();
    }
    //--------Command Paramaters
    #endregion
    #region FourthConnections
    public void OpenConn4()
    {
        connection4.ConnectionString = conString4;
        connection4.Open();
    }
    public void CloseConn4()
    {
        connection4.Close();
    }
    //------End Connection     
    //-----------DataReader
    public SqlDataReader ExecuteDr4()
    {
        dr4 = command4.ExecuteReader(CommandBehavior.CloseConnection);

        return dr4;
    }
    public void CloseDr4()
    {
        dr4.Close();
    }
    //-----------End DataReader
    public void commandTraxParam4(string query)
    {
        command4.Connection = connection4;
        command4.Transaction = tranCon4;
        command4.CommandTimeout = 600;
        command4.CommandText = query;
    }
    public void commandExeParam4(string query)
    {
        command4 = connection4.CreateCommand();
        command4.CommandTimeout = 600;
        command4.CommandText = query;
    }
    public void commandExeStoredParam4(string query)
    {
        command4 = connection4.CreateCommand();
        command4.CommandType = CommandType.StoredProcedure;
        command4.CommandTimeout = 600;
        command4.CommandText = query;
    }
    //---------Transaction
    public void BeginTransax4()
    {
        tranCon4 = connection4.BeginTransaction();
        command4 = connection4.CreateCommand();
    }
    public void commitTransax4()
    {
        tranCon4.Commit();
    }
    public void RollBackTrax4()
    {
        tranCon4.Rollback();
    }
    //---------End Transaction
    public void Execute4()
    {
        command4.ExecuteNonQuery();
    }
    #endregion
    public void generateAllBarcode(string param, string param2, string data, int data2) 
    {
        command4.Parameters.AddWithValue(param,data);
        command4.Parameters.AddWithValue(param2, data2);
    }
}
[DataContract]
public class WCFRespALLResult
{
    public byte[] photocontainer;
    public List<byte[]> photonames;
    public List<string> barcode;
    [DataMember]
    public string responseMsg { get; set; }
    [DataMember]
    public string responseCode { get; set; }
    [DataMember]
    public TempLotnoModels TempLotnodata { get; set; }
    [DataMember]
    public DisplayBarcodeItemsModels DisplayBarcodeItemsdata { get; set; }
    [DataMember]
    public costcentersModels costcentersdata { get; set; }
    [DataMember]
    public REMPopulateModels REMPopulatedata { get; set; }
    [DataMember]
    public RetrieveTradeINItemsModels RetrieveTradeINItemsdata { get; set; }
    [DataMember]
    public habwaMonthModels habwaMonthdata { get; set; }
    [DataMember]
    public List<CellSummaryRPTModels> CellSummaryRPTdata { get; set; }
    [DataMember]
    public List<ReceivedBranchesRPTModels> ReceivedBranchesRPTdata { get; set; }
    [DataMember]
    public List<UnReceivedBranchesModels> UnReceivedBranchesdata { get; set; }
    [DataMember]
    public PTNBarcodeModels PTNBarcodedata { get; set; }
    [DataMember]
    public PTNBarcode2Models PTNBarcode2data { get; set; }
    [DataMember]
    public BedRnmBedrnrModels BedRnmBedrnrdata { get; set; }
    [DataMember]
    public DisplayDetailsModels DisplayDetailsdata { get; set; }
    [DataMember]
    public DisplayDetails2Models DisplayDetails2data { get; set; }
    [DataMember]
    public getBedryfModels getBedryfdata { get; set; }
    [DataMember]
    public PTQueryREtrieveModels PTQueryREtrievedata { get; set; }
    [DataMember]
    public PTNQueryDetails2Models PTNQueryDetails2data { get; set; }
    [DataMember]
    public PTNQueryDetails3Models PTNQueryDetails3data { get; set; }
    [DataMember]
    public PTNQueryDetails4Models PTNQueryDetails4data { get; set; }
    [DataMember]
    public MaturityDateModels MaturityDatedata { get; set; }
    [DataMember]
    public getPTNdataModels getPTNdatadata { get; set; }
    [DataMember]
    public IntBarcodeModels IntBarcodedata { get; set; }
    [DataMember]
    public BoolVarModels BoolVardata { get; set; }
    [DataMember]
    public PhotoMembersModels PhotoMembersdata { get; set; }
    [DataMember]
    public EditModels Editdata { get; set; }
    [DataMember]
    public List<ReportSortLuzonModels> ReportSortLuzondata { get; set; }
    [DataMember]
    public List<ReportSortSummaryModels> ReportSortSummarydata { get; set; }
    [DataMember]
    public List<MemoMissingItemsModels> MemoMissingItemsdata { get; set; }
    [DataMember]
    public List<ProcessQTYModels> ProcessQTYdata { get; set; }
    [DataMember]
    public List<ProcessTWTModels> ProcessTWTdata { get; set; }
    [DataMember]
    public List<ProcessLoanAmoutModels> ProcessLoanAmoutdata { get; set; }
    [DataMember]
    public List<JewelrySummaryModels> JewelrySummarydata { get; set; }
    [DataMember]
    public List<ComparativeOverAppModels> ComparativeOverAppdata { get; set; }
    [DataMember]
    public List<ComparativeFakeCoatedModels> ComparativeFakeCoateddata { get; set; }
    [DataMember]
    public List<ComparativeCellWatchModels> ComparativeCellWatchdata { get; set; }
    [DataMember]
    public List<TOP10BranchesModels> TOP10Branchesdata { get; set; }
    [DataMember]
    public List<ComparativeReportsModels> ComparativeReportsdata { get; set; }
    [DataMember]
    public List<LVMComparativeReportsModels> LVMComparativeReportsdata { get; set; }
    [DataMember]
    public RetreiveActionModels RetreiveActiondata { get; set; }
    [DataMember]
    public retreivesortModels retreivesortdata { get; set; }
    [DataMember]
    public SearchModels Searchdata { get; set; }
    [DataMember]
    public SearchBranchModels SearchBranchdata { get; set; }
    [DataMember]
    public BarcodeHistoryModels BarcodeHistorydata { get; set; }
    [DataMember]
    public RetreiveBarcodeHistoryModels RetreiveBarcodeHistorydata { get; set; }
    [DataMember]
    public ViewBarcodeHistoryModels ViewBarcodeHistorydata { get; set; }    


}
#region DATACONTRACT
[DataContract]
public class TempLotnoModels 
{
    [DataMember]
    public string lotno { get; set; }
    public string isNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class DisplayBarcodeItemsModels 
{
    [DataMember]
    public string bedrnm { get; set; }
}
[DataContract]
public class costcentersModels 
{
    [DataMember]
    public List<string> costDept { get; set; }
}
[DataContract]
public class REMPopulateModels 
{
    [DataMember]
    public List<string> ID { get; set; }
    [DataMember]
    public List<string> PTN { get; set; }
    [DataMember]
    public List<string> MPTN { get; set; }
    [DataMember]
    public List<string> itemcode { get; set; }
    [DataMember]
    public List<string> ptnitemdesc { get; set; }
    [DataMember]
    public List<string> quantity { get; set; }
    [DataMember]
    public List<string> KaratGrading { get; set; }
    [DataMember]
    public List<string> CaratSize { get; set; }
    [DataMember]
    public List<string> SortingClass { get; set; }
    [DataMember]
    public List<string> Weight { get; set; }
    [DataMember]
    public List<string> ptnprincipal { get; set; }
    [DataMember]
    public List<string> appraisevalue { get; set; }
    [DataMember]
    public List<string> appraiser { get; set; }
    public string isNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class RetrieveTradeINItemsModels {
    [DataMember]
    public List<string> Division { get; set; }
    [DataMember]
    public List<string> Transaction_no { get; set; }
    [DataMember]
    public List<string> Itemcode { get; set; }
    [DataMember]
    public List<string> Description { get; set; }
    [DataMember]
    public List<string> Quantity { get; set; }
    [DataMember]
    public List<string> Karat { get; set; }
    [DataMember]
    public List<string> Carat { get; set; }
    [DataMember]
    public List<string> Weight { get; set; }
    [DataMember]
    public List<string> Resource_ID { get; set; }
    [DataMember]
    public List<string> Customer_ID { get; set; }
    [DataMember]
    public List<string> Pull_out_date { get; set; }
    [DataMember]
    public List<string> ID { get; set; }
}
[DataContract]
public class habwaMonthModels 
{
    [DataMember]
    public int month { get; set; }
    [DataMember]
    public int year { get; set; }
}
[DataContract]
public class CellSummaryRPTModels 
{
    [DataMember]
    public string branchcode { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string ptn { get; set; }
    [DataMember]
    public double loanvalue { get; set; }
    [DataMember]
    public string lotno { get; set; }
    [DataMember]
    public string refallbarcode { get; set; }
    [DataMember]
    public string refitemcode { get; set; }
    [DataMember]
    public int refqty { get; set; }
    [DataMember]
    public string all_desc { get; set; }
    [DataMember]
    public string SerialNo { get; set; }
    [DataMember]
    public string all_karat { get; set; }
    [DataMember]
    public double all_carat { get; set; }
    [DataMember]
    public double all_weight { get; set; }
    [DataMember]
    public double all_cost { get; set; }
    [DataMember]
    public string sortdate { get; set; }
    [DataMember]
    public string transdate { get; set; }
    [DataMember]
    public string sortername { get; set; }
}
[DataContract]
public class ReceivedBranchesRPTModels 
{
    [DataMember]
    public string BRANCHCODE { get; set; }
    [DataMember]
    public string BRANCHNAME { get; set; }
    [DataMember]
    public string MONTH { get; set; }
    [DataMember]
    public string YEAR { get; set; }
    
}
[DataContract]
public class UnReceivedBranchesModels 
{
    [DataMember]
    public string BranchCode { get; set; }
    [DataMember]
    public string BranchName { get; set; }
    [DataMember]
    public string zone { get; set; }
}
[DataContract]
public class PTNBarcodeModels 
{
    [DataMember]
    public List<string> ptn { get; set; }
    [DataMember]
    public List<string> ptn_barcode { get; set; }
    [DataMember]
    public List<string> ptnbarcode { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class PTNBarcode2Models 
{
    [DataMember]
    public string transdate { get; set; }
    [DataMember]
    public string curdate { get; set; }
    [DataMember]
    public string Division { get; set; }
    [DataMember]
    public string mptn { get; set; }
    [DataMember]
    public string Appraiser { get; set; }
    [DataMember]
    public string CustID { get; set; }
    [DataMember]
    public string CustFirstName { get; set; }
    [DataMember]
    public string CustLastName { get; set; }
    [DataMember]
    public string CustMiddleInitial { get; set; }
    [DataMember]
    public string CustAdd { get; set; }
    [DataMember]
    public string CustCity { get; set; }
    [DataMember]
    public string CustGender { get; set; }
    [DataMember]
    public string CustTelno { get; set; }
    [DataMember]
    public string PTNPrincipal { get; set; }
    [DataMember]
    public string LoanDate { get; set; }
    [DataMember]
    public string MaturityDate { get; set; }
    [DataMember]
    public string ExpiryDate { get; set; }
    [DataMember]
    public string AppraiseValue { get; set; }
    [DataMember]
    public string ptn_barcode { get; set; }
    [DataMember]
    public string ptn { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class BedRnmBedrnrModels
{
    [DataMember]
    public string bedrnr { get; set; }
    [DataMember]
    public string bedrnm { get; set; }
}
[DataContract]
public class DisplayDetailsModels 
{
    [DataMember]
    public string BranchCode { get; set; }
    [DataMember]
    public string BranchName { get; set; }
    [DataMember]
    public string PTNBarcode { get; set; }
    [DataMember]
    public string LoanValue { get; set; }
    [DataMember]
    public string PTN { get; set; }
    [DataMember]
    public string Transdate { get; set; }
    [DataMember]
    public string Pulloutdate { get; set; }
    [DataMember]
    public string CustID { get; set; }
    [DataMember]
    public string CustName { get; set; }
    [DataMember]
    public string CustAddress { get; set; }
    [DataMember]
    public string CustCity { get; set; }
    [DataMember]
    public string CustGender { get; set; }
    [DataMember]
    public string CustTel { get; set; }
    [DataMember]
    public string MaturityDate { get; set; }
    [DataMember]
    public string ExpiryDate { get; set; }
    [DataMember]
    public string loandate { get; set; }
    
    [DataMember]
    public string pttime { get; set; }
    [DataMember]
    public string ptdate { get; set; }
    public string isNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class DisplayDetails2Models 
{
    [DataMember]
    public List<string> ItemCode { get; set; }
    [DataMember]
    public List<string> BranchItemDesc { get; set; }
    [DataMember]
    public List<string> Qty { get; set; }
    [DataMember]
    public List<string> KaratGrading { get; set; }
    [DataMember]
    public List<string> CaratSize { get; set; }
    [DataMember]
    public List<string> ActionClass { get; set; }
    [DataMember]
    public List<string> Weight { get; set; }
    [DataMember]
    public List<string> Appraiser { get; set; }
    [DataMember]
    public List<string> AppraiseValue { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class getBedryfModels 
{
    [DataMember]
    public string bedrnm { get; set; }
    [DataMember]
    public string Class_04 { get; set; }
    [DataMember]
    public string Class_03 { get; set; }
}
[DataContract]
public class PTQueryREtrieveModels 
{
    [DataMember]
    public string status { get; set; }
    [DataMember]
    public string actionclass { get; set; }
    [DataMember]
    public string sortcode { get; set; }
}
[DataContract]
public class PTNQueryDetails2Models 
{
    [DataMember]
    public string Division { get; set; }
    [DataMember]
    public string ptnbarcode { get; set; }
    [DataMember]
    public string ptn { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class PTNQueryDetails3Models 
{
    [DataMember]
    public List<string> ItemCode { get; set; }
    [DataMember]
    public List<string> itemdesc { get; set; }
    [DataMember]
    public List<string> Quantity { get; set; }
    [DataMember]
    public List<string> Karat { get; set; }
    [DataMember]
    public List<string> Carat { get; set; }
    [DataMember]
    public List<string> SortClass { get; set; }
    [DataMember]
    public List<string> Weight { get; set; }
    [DataMember]
    public List<string> AppraiseValue { get; set; }
    [DataMember]
    public List<string> ALL_price { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class PTNQueryDetails4Models 
{
    [DataMember]
    public string receivedate { get; set; }
    [DataMember]
    public string recName { get; set; }
    [DataMember]
    public string recactionid { get; set; }
    [DataMember]
    public string sortdate { get; set; }
    [DataMember]
    public string recSorterName { get; set; }
    [DataMember]
    public string costdate { get; set; }
    [DataMember]
    public string costname { get; set; }
    [DataMember]
    public string releasedate { get; set; }
    [DataMember]
    public string releaser { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class MaturityDateModels 
{
    [DataMember]
    public string month { get; set; }
    [DataMember]
    public string day { get; set; }
    [DataMember]
    public string year { get; set; }
}
[DataContract]
public class getPTNdataModels 
{
    [DataMember]
    public List<string> refitemcode { get; set; }
    [DataMember]
    public List<string> id { get; set; }
    [DataMember]
    public List<string> branchitemdesc { get; set; }
    [DataMember]
    public List<string> refqty { get; set; }
    [DataMember]
    public List<string> karatgrading { get; set; }
    [DataMember]
    public List<string> all_karat { get; set; }
    [DataMember]
    public List<string> caratsize { get; set; }
    [DataMember]
    public List<string> sortcode { get; set; }
    [DataMember]
    public List<string> weight { get; set; }
    [DataMember]
    public List<string> all_weight { get; set; }
    [DataMember]
    public List<string> appraisevalue { get; set; }
    [DataMember]
    public List<string> all_cost { get; set; }
    [DataMember]
    public List<string> refallbarcode { get; set; }
    [DataMember]
    public List<string> appraiser { get; set; }
    [DataMember]
    public List<string> status { get; set; }
    [DataMember]
    public List<string> reflotno { get; set; }
    [DataMember]
    public List<string> actionclass { get; set; }
    public string IfNuL(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class IntBarcodeModels 
{
    [DataMember]
    public int barcode { get; set; }
    public String allBarcode;
    public Int64 newllBarcode;
    

}
[DataContract]
public class BoolVarModels
{
    [DataMember]
    public bool exist { get; set; }
}
[DataContract]
public class PhotoMembersModels 
{
    [DataMember]
    public string photo { get; set; }
    [DataMember]
    public string strPhotoFolder { get; set; }
    [DataMember]
    public string strPhotoFileName { get; set; }
    [DataMember]
    public string strPhotoWholeFileName { get; set; }
    [DataMember]
    public string ptn { get; set; }
    [DataMember]
    public string fileNameInC { get; set; }
    [DataMember]
    public string IfExist { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
   // public Image photoname;
    public byte[] photocontainer;
}
[DataContract]
public class EditModels 
{
    [DataMember]
    public string imageSource { get; set; }
    [DataMember]
    public string destination { get; set; }
    [DataMember]
    public string photodes { get; set; }

}
[DataContract]
public class ReportSortLuzonModels 
{
    [DataMember]
    public string mygroup { get; set; }
    [DataMember]
    public string branchCode { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string ptn { get; set; }
    [DataMember]
    public double loanvalue { get; set; }
    [DataMember]
    public string branchitemdesc { get; set; }
    [DataMember]
    public int refqty { get; set; }
    [DataMember]
    public string karatgrading { get; set; }
    [DataMember]
    public double weight { get; set; }
    [DataMember]
    public string actionclass { get; set; }
    [DataMember]
    public string sortcode { get; set; }
    [DataMember]
    public string sortdesc { get; set; }
    [DataMember]
    public string all_karat { get; set; }
    [DataMember]
    public double all_weight { get; set; }
    [DataMember]
    public string sortdate { get; set; }
    [DataMember]
    public string sortername { get; set; }
    [DataMember]
    public string transdate { get; set; }
    public string sortcodeDesc;


    [DataMember]
    public string status { get; set; }
    [DataMember]
    public double all_wt { get; set; }
  
}

[DataContract]
public class ReportSortSummaryModels 
{
    [DataMember]
    public string mygroup { get; set; }
    [DataMember]
    public string actionclass { get; set; }
    [DataMember]
    public double ptnprincipal { get; set; }
    [DataMember]
    public double wt { get; set; }
    [DataMember]
    public int qty { get; set; }
    [DataMember]
    public int packs { get; set; }
}
[DataContract]
public class MemoMissingItemsModels 
{
    [DataMember]
    public string BranchCode { get; set; }
    [DataMember]
    public string Branchname { get; set; }
    [DataMember]
    public string Region { get; set; }
    [DataMember]
    public string Area { get; set; }
    [DataMember]
    public string Class_01 { get; set; }
    [DataMember]
    public string PTN { get; set; }
    [DataMember]
    public double LoanValue { get; set; }
    [DataMember]
    public string BranchItemDesc { get; set; }
    [DataMember]
    public int Qty { get; set; }
    [DataMember]
    public string KaratGrading { get; set; }
    [DataMember]
    public double CaratSize { get; set; }
    [DataMember]
    public double Weight { get; set; }
    [DataMember]
    public string reflotno { get; set; }
    [DataMember]
    public string sortername { get; set; }
    [DataMember]
    public string month { get; set; }
    [DataMember]
    public string year { get; set; }
    [DataMember]
    public string manager_id { get; set; }
    [DataMember]
    public string ALL_Desc { get; set; }
    [DataMember]
    public string ALL_Karat { get; set; }
    [DataMember]
    public double ALL_Carat { get; set; }
    [DataMember]
    public double ALL_Weight { get; set; }
    [DataMember]
    public double ALL_Cost { get; set; }
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string VisMinOfficial { get; set; }
    [DataMember]
    public string NCROfficial { get; set; }
    [DataMember]
    public string PhotoName { get; set; }
    
}
[DataContract]
public class ProcessQTYModels
{
    [DataMember]
    public string branchcode { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public string area { get; set; }
    [DataMember]
    public int mlstocks { get; set; }
    [DataMember]
    public int watch { get; set; }
    [DataMember]
    public int cellular { get; set; }
    [DataMember]
    public int goodstock { get; set; }
    [DataMember]
    public int missing { get; set; }
    [DataMember]
    public int overapp { get; set; }
    [DataMember]
    public int fake { get; set; }
    [DataMember]
    public int coated { get; set; }
    [DataMember]
    public string ninek { get; set; }
    [DataMember]
    public string twelvek { get; set; }
    [DataMember]
    public string sixteenk { get; set; }
    [DataMember]
    public string twentyk { get; set; }
    [DataMember]
    public string threek { get; set; }
}
[DataContract]
public class ProcessTWTModels
{
    [DataMember]
    public string branchcode { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public string area { get; set; }
    [DataMember]
    public int mlstocks { get; set; }
    [DataMember]
    public int watch { get; set; }
    [DataMember]
    public int cellular { get; set; }
    [DataMember]
    public int goodstock { get; set; }
    [DataMember]
    public int missing { get; set; }
    [DataMember]
    public int overapp { get; set; }
    [DataMember]
    public int fake { get; set; }
    [DataMember]
    public int coated { get; set; }
    [DataMember]
    public string ninek { get; set; }
    [DataMember]
    public string twelvek { get; set; }
    [DataMember]
    public string sixteenk { get; set; }
    [DataMember]
    public string twentyk { get; set; }
    [DataMember]
    public string threek { get; set; }
}
[DataContract]
public class ProcessLoanAmoutModels
{
    [DataMember]
    public string branchcode { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public string area { get; set; }
    [DataMember]
    public double mlstocks { get; set; }
    [DataMember]
    public double watch { get; set; }
    [DataMember]
    public double cellular { get; set; }
    [DataMember]
    public double goodstock { get; set; }
    [DataMember]
    public double missing { get; set; }
    [DataMember]
    public double overapp { get; set; }
    [DataMember]
    public double fake { get; set; }
    [DataMember]
    public double coated { get; set; }
    [DataMember]
    public string ninek { get; set; }
    [DataMember]
    public string twelvek { get; set; }
    [DataMember]
    public string sixteenk { get; set; }
    [DataMember]
    public string twentyk { get; set; }
    [DataMember]
    public string threek { get; set; }
}
[DataContract]
public class JewelrySummaryModels 
{
    [DataMember]
    public string branchcode { get; set; }
    [DataMember]
    public double PrendaAmount { get; set; }
    [DataMember]
    public double REMAmount { get; set; }
    [DataMember]
    public string branchname { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public string area { get; set; }
}
[DataContract]
public class ComparativeOverAppModels 
{
    [DataMember]
    public string branches { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public double prevalue { get; set; }
    [DataMember]
    public double curvalue { get; set; }
    [DataMember]
    public double prevprincipal { get; set; }
    [DataMember]
    public double curprincipal { get; set; }
    [DataMember]
    public double prevallcost { get; set; }
    [DataMember]
    public double curallcost { get; set; }
    [DataMember]
    public int prevqty { get; set; }
    [DataMember]
    public int curqty { get; set; }
}
[DataContract]
public class ComparativeFakeCoatedModels 
{
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public double prevfakevalue { get; set; }
    [DataMember]
    public double curfakevalue { get; set; }
    [DataMember]
    public double prevcoatedvalue { get; set; }
    [DataMember]
    public double curcoatedvalue { get; set; }
    [DataMember]
    public string month { get; set; }
    [DataMember]
    public string year { get; set; }
}
[DataContract]
public class ComparativeCellWatchModels 
{
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public double curcellvalue { get; set; }
    [DataMember]
    public double curwatchvalue { get; set; }
    [DataMember]
    public double prevcellvalue { get; set; }
    [DataMember]
    public double prevwatchvalue { get; set; }
}
[DataContract]
public class TOP10BranchesModels 
{
    
    [DataMember]
    public string topten { get; set; }
    [DataMember]
    public string topreport { get; set; }
    [DataMember]
    public string area { get; set; }
    [DataMember]
    public string region { get; set; }
    [DataMember]
    public string Branchname { get; set; }
    [DataMember]
    public double prendaamount { get; set; }
    [DataMember]
    public double remamount { get; set; }
    [DataMember]
    public string PrendaMonth { get; set; }
    [DataMember]
    public string HabwaMonth { get; set; }
    [DataMember]
    public double percentage { get; set; }
}
[DataContract]
public class ComparativeReportsModels 
{
    [DataMember]
    public double Cur_JAmnt { get; set; }
    [DataMember]
    public double Cur_WAmnt { get; set; }
    [DataMember]
    public double Cur_CAmnt { get; set; }
    [DataMember]
    public double Cur_CoAmnt { get; set; }
    [DataMember]
    public double Cur_FAmnt { get; set; }
    [DataMember]
    public double Cur_OvAmnt { get; set; }
    [DataMember]
    public double Cur_PAmnt { get; set; }
    [DataMember]
    public double Cur_GSAmnt { get; set; }
    [DataMember]
    public double Cur_MisAmnt { get; set; }
    [DataMember]
    public double Prev_JAmnt { get; set; }
    [DataMember]
    public double Prev_WAmnt { get; set; }
    [DataMember]
    public double Prev_CAmnt { get; set; }
    [DataMember]
    public double Prev_CoAmnt { get; set; }
    [DataMember]
    public double Prev_FAmnt { get; set; }
    [DataMember]
    public double Prev_OvAmnt { get; set; }
    [DataMember]
    public double Prev_PAmnt { get; set; }
    [DataMember]
    public double Prev_GSAmnt { get; set; }
    [DataMember]
    public double Prev_MisAmnt { get; set; }
    [DataMember]
    public int Cur_JQty { get; set; }
    [DataMember]
    public int Cur_WQty { get; set; }
    [DataMember]
    public int Cur_CQty { get; set; }
    [DataMember]
    public int Cur_CoQty { get; set; }
    [DataMember]
    public int Cur_FQty { get; set; }
    [DataMember]
    public int Cur_OvQty { get; set; }
    [DataMember]
    public int Cur_GSQty { get; set; }
    [DataMember]
    public int Cur_MisQty { get; set; }
    [DataMember]
    public int Prev_JQty { get; set; }
    [DataMember]
    public int Prev_WQty { get; set; }
    [DataMember]
    public int Prev_CQty { get; set; }
    [DataMember]
    public int Prev_CoQty { get; set; }
    [DataMember]
    public int Prev_FQty { get; set; }
    [DataMember]
    public int Prev_OvQty { get; set; }
    [DataMember]
    public int Prev_GSQty { get; set; }
    [DataMember]
    public int Prev_MisQty { get; set; }
    [DataMember]
    public string Cur_Ninek { get; set; }
    [DataMember]
    public string Prev_Ninek { get; set; }
    [DataMember]
    public string Cur_Twelvek { get; set; }
    [DataMember]
    public string Prev_Twelvek { get; set; }
    [DataMember]
    public string Cur_Sixteenk { get; set; }
    [DataMember]
    public string Prev_Sixteenk { get; set; }
    [DataMember]
    public string Cur_20K { get; set; }
    [DataMember]
    public string Prev_20K { get; set; }
    [DataMember]
    public string Cur_Threek { get; set; }
    [DataMember]
    public string Prev_Threek { get; set; }
    [DataMember]
    public string Cur_goodStock { get; set; }
    [DataMember]
    public string Prev_goodStock { get; set; }
    [DataMember]
    public string Cur_Fake { get; set; }
    [DataMember]
    public string Prev_Fake { get; set; }
    [DataMember]
    public string Cur_Coated { get; set; }
    [DataMember]
    public string Prev_Coated { get; set; }
    [DataMember]
    public string Cur_OverAppraised { get; set; }
    [DataMember]
    public string Prev_OverAppraised { get; set; }
    [DataMember]
    public double Cur_all_cost { get; set; }
    [DataMember]
    public double Prev_all_cost { get; set; }
    [DataMember]
    public string Cur_bcode { get; set; }
    [DataMember]
    public string Prev_bcode { get; set; }
    [DataMember]
    public string curMonth { get; set; }
    [DataMember]
    public string prevMonth { get; set; }   
}
[DataContract]
public class LVMComparativeReportsModels 
{
    [DataMember]
    public string curdivision { get; set; }
    [DataMember]
    public double curjewelryvalue { get; set; }
    [DataMember]
    public double curwatchvalue { get; set; }
    [DataMember]
    public double curcellvalue { get; set; }
    [DataMember]
    public double curmissingitemvalue { get; set; }
    [DataMember]
    public double curgoodstockvalue { get; set; }
    [DataMember]
    public int curjewelryqty { get; set; }
    [DataMember]
    public int curwatchqty { get; set; }
    [DataMember]
    public int curcellqty { get; set; }
    [DataMember]
    public int curmissingitemqty { get; set; }
    [DataMember]
    public int curgoodstockqty { get; set; }
    [DataMember]
    public int curcoatedqty { get; set; }
    [DataMember]
    public int curfakeqty { get; set; }
    [DataMember]
    public int curoverappraisedqty { get; set; }
    [DataMember]
    public double cur9Kweight { get; set; }
    [DataMember]
    public double cur12Kweight { get; set; }
    [DataMember]
    public double cur16Kweight { get; set; }
    [DataMember]
    public double cur20Kweight { get; set; }
    [DataMember]
    public double cur21Kweight { get; set; }
    [DataMember]
    public double curGSweight { get; set; }
    [DataMember]
    public double curFakeweight { get; set; }
    [DataMember]
    public double curCoatedweight { get; set; }
    [DataMember]
    public double curOAweight { get; set; }
    [DataMember]
    public double curmissingitemweight { get; set; }
    [DataMember]
    public double curfakevalue { get; set; }
    [DataMember]
    public double curCoatedevalue { get; set; }
    [DataMember]
    public double curOAvalue { get; set; }
    [DataMember]
    public double curprenda { get; set; }
    [DataMember]
    public double curoaamount { get; set; }
    [DataMember]
    public string prevdivision { get; set; }
    [DataMember]
    public double prevjewelryvalue { get; set; }
    [DataMember]
    public double prevwatchvalue { get; set; }
    [DataMember]
    public double prevcellvalue { get; set; }
    [DataMember]
    public double prevmissingitemvalue { get; set; }
    [DataMember]
    public double prevGoodstockvalue { get; set; }
    [DataMember]
    public int prevjewelryqty { get; set; }
    [DataMember]
    public int prevwatchqty { get; set; }
    [DataMember]
    public int prevcellqty { get; set; }
    [DataMember]
    public int prevmissingitemqty { get; set; }
    [DataMember]
    public int prevGoodstockqty { get; set; }
    [DataMember]
    public int prevcoatedqty { get; set; }
    [DataMember]
    public int prevfakeqty { get; set; }
    [DataMember]
    public int prevoverappraisedqty { get; set; }
    [DataMember]
    public double prev9Kweight { get; set; }
    [DataMember]
    public double prev12Kweight { get; set; }
    [DataMember]
    public double prev16Kweight { get; set; }
    [DataMember]
    public double prev20Kweight { get; set; }
    [DataMember]
    public double prev21Kweight { get; set; }
    [DataMember]
    public double prevGSweight { get; set; }
    [DataMember]
    public double prevFakeweight { get; set; }
    [DataMember]
    public double prevCoatedweight { get; set; }
    [DataMember]
    public double prevOAweight { get; set; }
    [DataMember]
    public double prevmissingitemweight { get; set; }
    [DataMember]
    public double prevfakevalue { get; set; }
    [DataMember]
    public double prevCoatedevalue { get; set; }
    [DataMember]
    public double prevOAvalue { get; set; }
    [DataMember]
    public double prevprenda { get; set; }
    [DataMember]
    public double prevoaamount { get; set; }
    [DataMember]
    public string curmonth { get; set; }
    [DataMember]
    public string prevmonth { get; set; }
}
[DataContract]
public class RetreiveActionModels 
{
    [DataMember]
    public List<double> costa { get; set; }
    [DataMember]
    public List<double> costb { get; set; }
    [DataMember]
    public List<double> costc { get; set; }
    [DataMember]
    public List<double> costd { get; set; }
    [DataMember]
    public List<string> action_id { get; set; }
    [DataMember]
    public List<string> action_type { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class retreivesortModels 
{
    [DataMember]
    public List<string> id { get; set; }
    [DataMember]
    public List<string> code { get; set; }
    [DataMember]
    public List<string> description { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class SearchModels 
{
    [DataMember]
    public List<string> bedrnr { get; set; }
    [DataMember]
    public List<string> bedrnm { get; set; }
    [DataMember]
    public List<string> class_03 { get; set; }
}
[DataContract]
public class SearchBranchModels 
{
    [DataMember]
    public List<string> bedrnr { get; set; }
    [DataMember]
    public List<string> bedrnm { get; set; }
    [DataMember]
    public List<string> class_03 { get; set; }
}
[DataContract]
public class BarcodeHistoryModels 
{
    [DataMember]
    public string description { get; set; }
    [DataMember]
    public string lblqty { get; set; }
    [DataMember]
    public string weight { get; set; }
    [DataMember]
    public string karat { get; set; }
    [DataMember]
    public string carat { get; set; }
    [DataMember]
    public string SerialNo { get; set; }
    [DataMember]
    public string price { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class RetreiveBarcodeHistoryModels 
{
    [DataMember]
    public List<string> trandate { get; set; }
    [DataMember]
    public List<string> lotno { get; set; }
    [DataMember]
    public List<string> status { get; set; }
    [DataMember]
    public List<string> consignto { get; set; }
    [DataMember]
    public List<string> costcenter { get; set; }
    [DataMember]
    public List<string> empname { get; set; }
    [DataMember]
    public List<string> cost { get; set; }
    public string IFNULL(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
[DataContract]
public class ViewBarcodeHistoryModels 
{
    [DataMember]
    public string allbarcode { get; set; }
    [DataMember]
    public string itemcode { get; set; }
    [DataMember]
    public string branchitemdesc { get; set; }
    [DataMember]
    public string weight { get; set; }
    [DataMember]
    public string karatgrading { get; set; }
    [DataMember]
    public string all_cost { get; set; }
    public string ifNull(object data)
    {
        if (Convert.IsDBNull(data))
        {
            return "0";
        }
        else
        {
            return data.ToString();
        }
    }
}
#endregion




