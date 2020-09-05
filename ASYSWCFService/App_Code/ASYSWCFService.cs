using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Services;
using log4net;
using log4net.Config;
using System.ServiceModel.Activation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFService" in code, svc and config file together.
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class ASYSWCFService : IASYSWCFService
{
    public String asysSaveImagefile1;
    private SqlDataReader dr;
    private SqlDataReader dr2;
    private int typeClass;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public ASYSWCFService()
    {
        XmlConfigurator.Configure();
    }
    string i_am = "REMCF6.5";
    //---------------------------February 8,2017----------------steph--------------//
    public String where_am_I()
    {

        try
        {
            return IPAddress.Parse(locate()).GetAddressBytes()[3] + "/" + i_am;
        }
        catch (Exception x)
        {

            return x.Message;
        }

    }
    public static string locate()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("err");
    }
    public WCFRespALLResult TempoLotNo()
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select lotno from ASYS_LOTNO_Gen WHERE BusinessCenter ='REM'");
            dr = _sql.ExecuteDr3();
            if (dr.Read() == true)
            {
                models.lotno = dr["LOTNO"].ToString();

                log.Info("TempoLotNo: " + models.lotno);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("TempoLotNo: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("TempoLotNo: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "TempoLotNo: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult DisplayBarcodeItems(string DB, string barcode)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            
            if (DB == "Luzon")
            {
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and dateend is null");// Modify query for split
            }
            else if (DB == "VISMIN")
            {
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and Class_02 in('Visayas','Mindanao') ");
            }
            else if (DB == "Visayas")
            {
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and Class_02 in('Visayas') ");
            }
            else if (DB == "Showroom")
            {
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and Class_02 in('SHOWROOMS')");
            }
            else if (DB == "Mindanao")
            {
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and Class_02 in('Mindanao')");
            }
            else if (DB == "LNCR")
            {
                DB = "NCR";
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + barcode + "' and dateend is null");//NEED TO WORK ON THIS ONE Modify query for split
            }
            dr = _sql.ExecuteDr();
            if (dr.Read() == true)
            {
                models.bedrnm = dr["bedrnm"].ToString().ToUpper();

                log.Info("DisplayBarcodeItems: " + models.bedrnm);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("DisplayBarcodeItems: " + respMessage(2));
                models.bedrnm = "";
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(2), DisplayBarcodeItemsdata = models };
            }
        }
        catch (Exception ex)
        {
            log.Error("DisplayBarcodeItems: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayBarcodeItems: " + respMessage(0) + ex.Message };
        }

    }//WITH NCR
    public WCFRespALLResult costcenters()
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("Select * from tbl_CostCenter");
            dr = _sql.ExecuteDr();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    List.costcentersdata.costDept.Add(dr["CostDept"].ToString());
                }
                log.Info("costcenters: " + List.costcentersdata.costDept);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("costcenters: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("costcenters: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "costcenters: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SaveTradeIN_transaction(string DB, string lotno, string branchCode, int combo1Value, int Month, string lblCode,
        string lbltwelve, string userLog, string[][] ListView)//WTIH NCR

    {
        var _sql = new Connection();
        var _sql2 = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql2.Connection1(DB);
            _sql2.OpenConn1();

            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            //1st
            _sql.commandTraxParam1("update REMS.dbo.ASYS_LOTNO_Gen set lotno ='" + lotno + "' WHERE BusinessCenter ='REM'");
            _sql.Execute1();
            //2nd
            _sql2.commandExeParam1("SELECT Division,[ID], transaction_no, resource_id, customer_id, appraisal_amount, pull_out_date, received" +
                " FROM dbo.cstTradeIn_Transactions where division = '" + branchCode + "' and Year(pull_out_date) = '" + combo1Value +
                "' and MOnth(pull_out_date) = '" + Month + "' and received =  0");
            dr = _sql2.ExecuteDr1();
            //_sql.BeginTransax1();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    //3rd
                    _sql.commandTraxParam1("Insert into REMS" + DB + ".dbo.ASYS_TradeIN_header(Tran_ID, Division,Divisionname,Transaction_No," +
                        "Resource_ID,Customer_ID,Appraisal_Amount,Pull_out_DATE) values ('" + dr["ID"] + "','" + branchCode + "','" + lblCode +
                        "','" + dr["Transaction_No"] + "','" + dr["Resource_ID"] + "','" + dr["Customer_ID"] + "','" + dr["appraisal_amount"] +
                        "','" + dr["pull_out_date"] + "')");
                    _sql.Execute1();
                    //4th
                    _sql.commandTraxParam1("Insert into [REMS].dbo.ASYS_TradeIN_header  (Lotno,Tran_ID, Division,Divisionname,Transaction_No," +
                        "Resource_ID,Customer_ID,Appraisal_Amount,Pull_out_DATE) values ('" + lbltwelve + "','" + dr["ID"] + "','" + branchCode +
                        "','" + lblCode + "','" + dr["Transaction_No"] + "','" + dr["Resource_ID"] + "','" + dr["Customer_ID"] + "','" + dr["appraisal_amount"] +
                        "','" + dr["pull_out_date"] + "')");
                    _sql.Execute1();
                }
                _sql2.CloseDr1();
            }
            else
            {
                log.Info("SaveTradeIN_transaction: " + respMessage(2));
                _sql2.CloseDr1();
                _sql2.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
            for (int i = 0; i <= ListView.Count() - 1; i++)
            {
                if (ListView[i] != null)
                {
                    //5th
                    _sql.commandTraxParam1("Insert into REMS" + DB + ".dbo.ASYS_TradeIN_Detail  (item_id,reflotno,lotno,transaction_no,itemcode" +
                        ",description,quantity,karat,carat,weight,receivedate,receiver,Status) values ('" + ListView[i][7] + "','" + lbltwelve +
                        "','" + lbltwelve + "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] +
                        "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][5] + "','" + ListView[i][6] +
                        "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute1();
                    //6th
                    _sql.commandTraxParam1("Insert into [REMS].dbo.ASYS_TradeIN_Detail  (item_id,reflotno,lotno,transaction_no,itemcode,description" +
                        ",quantity,karat,carat,weight,receivedate,receiver,Status) values ('" + ListView[i][7] + "','" + lbltwelve + "','" + lbltwelve +
                        "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] + "','" + ListView[i][3] +
                        "','" + ListView[i][4] + "','" + ListView[i][5] + "','" + ListView[i][6] + "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute1();
                }
                else
                {
                    break;
                }
            }
            //7th
            _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.cstTradeIn_Transactions SET received = 1 " +
                "WHERE dbo.cstTradeIn_Transactions.Division = '" + branchCode + "' and Month(pull_out_date) = '" + Month +
                "' and year(pull_out_date) = '" + combo1Value + "' and received = 0");
            _sql.Execute1();

            log.Info("SaveTradeIN_transaction: " + respMessage(1) + " | DB: " + DB + " | userLog: " + userLog + " | lotno: " + lotno);
            _sql.commitTransax1();
            _sql.CloseConn1();
            _sql2.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SaveTradeIN_transaction: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            _sql2.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SaveTradeIN_transaction: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult updateTRANptn(string DB, string branchCode, string[][] dgEntry, int date, int year)// WITH NCR


    {
        var _sql = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            for (int i = 0; i <= dgEntry.Count() - 1; i++)
            {
                if (dgEntry[i] != null)
                {
                    _sql.commandTraxParam1("update tbl_pt_tran set received = 1 where division ='" + branchCode +
                "' and ptn='" + dgEntry[i][0] + "' and month(transdate) ='" + date + "' and year(transdate)='" + year +
                "'  and pulloutstocks= 1 and pulloutdate is not null and ptstatus =1 and received=0");
                    _sql.Execute1();
                }
                else
                {
                    break;
                }
            }
            log.Info("updateTRANptn: DB: " + DB + " | branchCode: " + branchCode);
            _sql.commitTransax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("updateTRANptn: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "updateTRANptn: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SavePTNbyBatch(string DB, string getNewlot, string branchCode, int combo1, int m, string lblBC, string[][] ListView,
        string lbltwelve, string userLog)
    {
        var _sql = new Connection();
        var _sql2 = new Connection();
        string ptn,new_DB;
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
         
            _sql2.Connection1(DB);
            _sql2.OpenConn1();

            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            //1st
            _sql.commandTraxParam1("update REMS.dbo.ASYS_LOTNO_Gen set lotno ='" + getNewlot + "' WHERE BusinessCenter ='REM'");
            _sql.Execute1();
            //2nd
            new_DB = DB;
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql2.commandExeParam1("SELECT dbo.tbl_PT_Tran.Division AS BranchCode, RTRIM(REMS.dbo.vw_bedryf" + DB + ".bedrnm) AS BranchName," +
                " RTRIM(REMS.dbo.vw_bedryf" + DB + ".Class_03) AS Region,RTRIM(REMS.dbo.vw_bedryf" + DB + ".Class_04) AS Area," +
                " Cast(REPLACE(STR(dbo.tbl_PT_Tran.PTN, 12, 0), SPACE(1), '0') as char(12)) AS PTN, dbo.tbl_PT_Tran.ptn_barcode AS PTNBarcode" +
                ",dbo.tbl_PT_Tran.PTNPrincipal AS LoanValue, dbo.tbl_PT_Tran.TransDate AS Transdate, dbo.tbl_PT_Tran.PullOutDate AS PullOutDate," +
                "dbo.tbl_PT_Tran.CustID, RTrim(dbo.tbl_PT_Tran.CustFirstName) + ' ' + RTrim(dbo.tbl_PT_Tran.CustLastName) + ' ' + Replace(RTrim(dbo.tbl_PT_Tran.CustMiddleInitial),'''','.')" +
                " AS CustName,Replace(RTRIM(dbo.tbl_PT_Tran.CustAdd),'''','') AS CustAddress, Replace(RTRIM(dbo.tbl_PT_Tran.CustCity),'''','') as CustCity, dbo.tbl_PT_Tran.CustGender," +
                " dbo.tbl_PT_Tran.CustTelNO as CustTel,RTRIM(dbo.tbl_PT_Tran.Appraiser) as Appraiser, dbo.tbl_PT_Tran.AppraiseValue," +
                " dbo.tbl_PT_Tran.MaturityDate, dbo.tbl_PT_Tran.ExpiryDate,dbo.tbl_PT_Tran.LoanDate FROM dbo.tbl_PT_Tran INNER JOIN " +
                "REMS.dbo.vw_bedryf" + DB + " ON dbo.tbl_PT_Tran.Division = REMS.dbo.vw_bedryf" + DB + ".bedrnr " +
                "where dbo.tbl_PT_Tran.Division = '" + branchCode + "' and dbo.tbl_pt_tran.transtype <> 'LUKAT'" +
                " and Month(transdate) = '" + m + "' and year(transdate) = '" + combo1 + "' and pulloutstocks = 1 and pulloutdate is not" +
                " null and ptstatus = 1 and received = 0");
            dr = _sql2.ExecuteDr1();


            //_sql.BeginTransax1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    String sptn = dr["PTN"].ToString();
                    //3rd
                  
                        ptn = dr["ptn"].ToString();
                        _sql.commandTraxParam1("Insert into REMS" + DB + ".dbo.ASYS_REM_header(Branchcode, BranchName, Region, Area,PTN," +
                            " PTNBarcode, LoanValue,  Transdate, PullOutDate, CustId,CustName,CustAddress,CustCity,CustGender,CustTel," +
                            "PTTime,PTDate ,MaturityDate,ExpiryDate,LoanDate) values('" + branchCode + "', '" + lblBC + "','" + dr["REgion"] +
                                "', '" + dr["Area"] + "','" + dr["PTN"] + "', '" + dr["PTNBarcode"] + "', " + dr["LoanValue"] + " ,'" + dr["Transdate"] +
                                    "', '" + dr["PullOutDate"] + "','" + dr["CustID"] + "','" + dr["CustName"] + "','" + dr["CustAddress"] +
                                    "','" + dr["CustCity"] + "','" + dr["CustGender"] + "','" + dr["CustTel"] + "','" + dr["Transdate"] +
                                    "','" + dr["Transdate"] + "','" + dr["MaturityDate"] + "','" + dr["ExpiryDate"] + "','" + dr["LoanDate"] + "')");
                        _sql.Execute1();

                        //4th
                        _sql.commandTraxParam1("Insert into [REMS].dbo.ASYS_REM_header(Branchcode, BranchName, Region, Area,PTN, PTNBarcode," +
                            " LoanValue,  Transdate, PullOutDate, CustId,CustName,CustAddress,CustCity,CustGender,CustTel,PTTime,PTDate ," +
                            "MaturityDate,ExpiryDate,LoanDate) values ('" + branchCode + "', '" + lblBC + "','" + dr["REgion"] + "', '" + dr["Area"] +
                            "','" + dr["PTN"] + "', '" + dr["PTNBarcode"] + "', " + dr["LoanValue"] + " ,'" + dr["Transdate"] + "', '" + dr["PullOutDate"] +
                            "','" + dr["CustID"] + "','" + dr["CustName"] + "','" + dr["CustAddress"] + " ','" + dr["CustCity"] + "','" + dr["CustGender"] +
                            "','" + dr["CustTel"] + "','" + dr["Transdate"] + "','" + dr["Transdate"] + "','" + dr["MaturityDate"] + "','" + dr["ExpiryDate"] +
                            "','" + dr["LoanDate"] + "')");
                        _sql.Execute1();


           
                    
                }
                _sql2.CloseDr1();
            }
            else
            {
                log.Info("SavePTNbyBatch: " + respMessage(2));
                _sql2.CloseDr1();
                _sql2.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
            for (int i = 0; i <= ListView.Count() - 1; i++)
            {
                if (ListView[i] != null)
                {
                    //5th
                    _sql.commandTraxParam1("Update [REMS].dbo.ASYS_REM_Header  set lotno = '" + lbltwelve + "' where ptn ='" + ListView[i][0] +
                        "' and branchcode = '" + branchCode + "' and branchname = '" + lblBC + "'");
                    _sql.Execute1();
                    //6th
                    _sql.commandTraxParam1("Insert into REMS" + DB + ".dbo.ASYS_REM_Detail (reflotno,lotno,PTN,Itemid,RefItemcode,Itemcode," +
                        "BranchItemDesc,RefQty,Qty,KaratGrading,CaratSize,Weight,ActionClass,SortCode,ALL_desc,ALL_Karat,ALL_Carat,ALL_Weight," +
                        "Price_desc,Price_karat,Price_Weight,Price_carat,Appraiser, AppraiseValue, ReceiveDate,Receiver,Status)" +
                        " values ('" + lbltwelve + "','" + lbltwelve + "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] +
                        "','" + ListView[i][2] + "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][4] +
                        "','" + ListView[i][5] + "','" + ListView[i][6] + "','" + ListView[i][8] + "','Jewelry','" + ListView[i][7] +
                        "','" + ListView[i][3] + "','" + ListView[i][5] + "','" + ListView[i][6] + "','" + ListView[i][8] +
                        "','" + ListView[i][3] + "','" + ListView[i][5] + "','" + ListView[i][8] + "','" + ListView[i][6] +
                        "','" + ListView[i][10] + "','" + ListView[i][9] + "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute1();
                    //7th
                    _sql.commandTraxParam1("Insert into [REMS].DBO.ASYS_REM_Detail (reflotno,lotno,PTN,Itemid,RefItemcode,Itemcode,BranchItemDesc" +
                        ",RefQty,Qty,KaratGrading,CaratSize,Weight,ActionClass,SortCode,ALL_desc,ALL_Karat,ALL_Carat,ALL_Weight,Price_desc," +
                        "Price_karat,Price_Weight,Price_carat,Appraiser, AppraiseValue,ReceiveDate,Receiver,Status) values ('" + lbltwelve +
                        "','" + lbltwelve + "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] + "','" + ListView[i][2] +
                        "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][4] + "','" + ListView[i][5] + "','" + ListView[i][6] +
                        "','" + ListView[i][8] + "','Jewelry','" + ListView[i][7] + "','" + ListView[i][3] + "','" + ListView[i][5] +
                        "','" + ListView[i][6] + "','" + ListView[i][8] + "','" + ListView[i][3] + "','" + ListView[i][5] +
                        "','" + ListView[i][8] + "','" + ListView[i][6] + "','" + ListView[i][10] + "','" + ListView[i][9] +
                        "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute1();
                }
                else
                {
                    break;
                }
            }
            _sql.commandTraxParam1("update dbo.tbl_pt_tran  set received = 1 where dbo.tbl_PT_Tran.Division = '" + branchCode +
                "' and dbo.tbl_pt_tran.transtype <> 'LUKAT' and Month(transdate) = '" + m + "' and year(transdate) = '" + combo1 +
                "' and pulloutstocks = 1 and pulloutdate is not null and ptstatus = 1 and received = 0");
            _sql.Execute1();

            log.Info("SavePTNbyBatch: " + respMessage(1) + " | lotno: " + getNewlot + " | DB: " + DB + " | userLog: " + userLog);
            _sql.commitTransax1();
            //_sql.RollBackTrax1();//TESTING 12
            _sql.CloseConn1();
            _sql2.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SavePTNbyBatch: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            _sql2.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SavePTNbyBatch: " + respMessage(0) + ex.Message };
        }

    }
    //---------------------------------End February 8------------------//
    //---------------------------February 9,2017----------------steph--------------//
    public WCFRespALLResult GetResID(string cbValue)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("Select res_id from " + Connection.humres2 + " where fullname='" + cbValue + "'");
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                models.lotno = dr["res_id"].ToString();

                log.Info("GetResID: " + models.lotno);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("GetResID: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetResID: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetResID: " + respMessage(0) + ex.Message };
        }
    }
    public bool checker(string ptn)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("SELECT ptn FROM REMS.dbo.ASYS_REM_header WHERE PTN = '"+ptn+"'");
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                dr.Close();
                return true; 
            }
            else
            {
                dr.Close();
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public WCFRespALLResult REMPopulateData(string DB, int month, int year, string branch)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.REMPopulatedata = new REMPopulateModels();
        List.REMPopulatedata.ID = new List<string>();
        List.REMPopulatedata.PTN = new List<string>();
        List.REMPopulatedata.MPTN = new List<string>();
        List.REMPopulatedata.itemcode = new List<string>();
        List.REMPopulatedata.ptnitemdesc = new List<string>();
        List.REMPopulatedata.quantity = new List<string>();
        List.REMPopulatedata.KaratGrading = new List<string>();
        List.REMPopulatedata.CaratSize = new List<string>();
        List.REMPopulatedata.SortingClass = new List<string>();
        List.REMPopulatedata.Weight = new List<string>();
        List.REMPopulatedata.ptnprincipal = new List<string>();
        List.REMPopulatedata.appraisevalue = new List<string>();
        List.REMPopulatedata.appraiser = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select Cast(REPLACE(STR(a.ptn, 12, 0), SPACE(1), '0') as char(12)) AS ptn, a.mptn, b.[ID], b.itemcode, " +
                "b.PTNItemDesc, b.Quantity, b.KaratGrading, b.caratsize, b.SortingClass, b.Weight, a.PTNPrincipal, a.appraisevalue, a.appraiser" +
                " from tbl_pt_tran a INNER JOIN tbl_PTN_Item b ON a.mptn = b.mptn WHERE  a.division ='" + branch + "' and TransType <> 'LUKAT' " +
                "and month(transdate) ='" + month + "' and year(transdate)='" + year + "' and pulloutstocks= 1 and pulloutdate is not null and " +
                "ptstatus =1 and received = 0 order by a.ptn, b.[ID]");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.REMPopulatedata.ID.Add(List.REMPopulatedata.isNull(dr["ID"]));
                    List.REMPopulatedata.PTN.Add(List.REMPopulatedata.isNull(dr["PTN"]));
                    List.REMPopulatedata.MPTN.Add(List.REMPopulatedata.isNull(dr["MPTN"]));
                    List.REMPopulatedata.itemcode.Add(List.REMPopulatedata.isNull(dr["itemcode"]));
                    List.REMPopulatedata.ptnitemdesc.Add(List.REMPopulatedata.isNull(dr["ptnitemdesc"]));
                    List.REMPopulatedata.quantity.Add(List.REMPopulatedata.isNull(dr["quantity"]));
                    List.REMPopulatedata.KaratGrading.Add(List.REMPopulatedata.isNull(dr["KaratGrading"]));
                    List.REMPopulatedata.CaratSize.Add(List.REMPopulatedata.isNull(dr["CaratSize"]));
                    List.REMPopulatedata.SortingClass.Add(List.REMPopulatedata.isNull(dr["SortingClass"]));
                    List.REMPopulatedata.Weight.Add(List.REMPopulatedata.isNull(dr["Weight"]));
                    List.REMPopulatedata.ptnprincipal.Add(List.REMPopulatedata.isNull(dr["ptnprincipal"]));
                    List.REMPopulatedata.appraisevalue.Add(List.REMPopulatedata.isNull(dr["appraisevalue"]));
                    List.REMPopulatedata.appraiser.Add(List.REMPopulatedata.isNull(dr["appraiser"]));

                }
                log.Info("REMPopulateData: " + List.REMPopulatedata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), REMPopulatedata = List.REMPopulatedata };
            }
            else
            {
                log.Info("REMPopulateData: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("REMPopulateData: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "REMPopulateData: " + respMessage(0) + ex.Message };
        }
    }//DONE 
    public WCFRespALLResult RetrieveTradeIN_Items(string DB, int month, int year, string branch)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.RetrieveTradeINItemsdata = new RetrieveTradeINItemsModels();
        List.RetrieveTradeINItemsdata.Division = new List<string>();
        List.RetrieveTradeINItemsdata.Transaction_no = new List<string>();
        List.RetrieveTradeINItemsdata.Itemcode = new List<string>();
        List.RetrieveTradeINItemsdata.Description = new List<string>();
        List.RetrieveTradeINItemsdata.Quantity = new List<string>();
        List.RetrieveTradeINItemsdata.Karat = new List<string>();
        List.RetrieveTradeINItemsdata.Carat = new List<string>();
        List.RetrieveTradeINItemsdata.Weight = new List<string>();
        List.RetrieveTradeINItemsdata.Resource_ID = new List<string>();
        List.RetrieveTradeINItemsdata.Customer_ID = new List<string>();
        List.RetrieveTradeINItemsdata.Pull_out_date = new List<string>();
        List.RetrieveTradeINItemsdata.ID = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            //_sql.commandExeParam1("SELECT entry.Division, entry.transaction_no, entry.resource_id,entry.customer_id, entry.appraisal_amount," +
            //    " entry.pull_out_date,entry.received, item.ID, item.itemcode, item.description,item.quantity, item.karat, item.carat," +
            //    " item.weight FROM DBO.cstTradeIn_Items item INNER JOIN dbo.cstTradeIn_Transactions entry ON item.transaction_no " +
            //    "= entry.transaction_no WHERE entry.division = '" + branch + "' AND YEAR(entry.pull_out_date) = '" + year +
            //    "' AND MONTH(entry.pull_out_date) = '" + month + "' AND entry.received = 0");

            _sql.commandExeParam1("SELECT entry.Division, entry.transaction_no, entry.resource_id, entry.customer_id, entry.appraisal_amount, entry.pull_out_date, entry.received, item.ID, item.itemcode, item.description, item.quantity, item.karat, item.carat, item.weight FROM DBO.cstTradeIn_Items item INNER JOIN dbo.cstTradeIn_Transactions entry ON item.transaction_no = entry.transaction_no WHERE entry.division = '"+branch+"' AND YEAR(entry.pull_out_date) = '"+year+"' AND MONTH(entry.pull_out_date) = '"+month+"' AND entry.received = 0");
            //_sql.commandExeParam1("SELECT entry.Division, entry.transaction_no, entry.resource_id, entry.customer_id, entry.appraisal_amount, entry.pull_out_date, entry.received, item.ID, item.itemcode, item.description, item.quantity, item.karat, item.carat, item.weight FROM DBO.cstTradeIn_Items item INNER JOIN dbo.cstTradeIn_Transactions entry ON item.transaction_no = entry.transaction_no WHERE entry.division = '042' AND YEAR(entry.pull_out_date) = '2018'AND MONTH(entry.pull_out_date) = '1' AND entry.received = 0");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.RetrieveTradeINItemsdata.Division.Add(dr["Division"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Transaction_no.Add(dr["Transaction_no"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Itemcode.Add(dr["Itemcode"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Description.Add(dr["Description"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Quantity.Add(dr["Quantity"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Karat.Add(dr["Karat"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Carat.Add(dr["Carat"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Weight.Add(dr["Weight"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Resource_ID.Add(dr["Resource_ID"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Customer_ID.Add(dr["Customer_ID"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.Pull_out_date.Add(dr["Pull_out_date"].ToString().ToUpper());
                    List.RetrieveTradeINItemsdata.ID.Add(dr["ID"].ToString().ToUpper());
                }
                log.Info("RetrieveTradeIN_Items: " + List.RetrieveTradeINItemsdata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RetrieveTradeINItemsdata = List.RetrieveTradeINItemsdata };
            }
            else
            {
                log.Info("RetrieveTradeIN_Items: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetrieveTradeIN_Items: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrieveTradeIN_Items: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult habwa_date(string DB)
    {
        var _sql = new Connection();
        var models = new habwaMonthModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            if (DB == "")
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year");
                dr = _sql.ExecuteDr3();
                if (dr.Read())
                {
                    models.month = Convert.ToInt32(dr["month"].ToString());
                    models.year = Convert.ToInt32(dr["year"].ToString());

                    log.Info("habwa_date: DB: " + DB + " | month: " + models.month + " | year" + models.year);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), habwaMonthdata = models };
                }
                else
                {
                    log.Info("habwa_date: DB: " + DB + " | " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.Connection1(DB);
                _sql.OpenConn1();
                _sql.commandExeParam1("select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year");
                dr = _sql.ExecuteDr1();
                if (dr.Read())
                {
                    models.month = Convert.ToInt32(dr["month"].ToString());
                    models.year = Convert.ToInt32(dr["year"].ToString());

                    log.Info("habwa_date: DB: " + DB + " | month: " + models.month + " | year" + models.year);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), habwaMonthdata = models };
                }
                else
                {
                    log.Info("habwa_date: DB: " + DB + " | " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            if (DB == "")
            {
                _sql.CloseConn3();
            }
            else
            {
                _sql.CloseConn1();
            }
            log.Error("habwa_date: " + respMessage(0) + ex.Message);
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "habwa_date: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult valEmp()
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("SELECT DISTINCT UPPER(fullname) AS FULLNAME FROM REMS.dbo.vw_humresall WHERE job_title='ALLBOSMAN' " +
                "OR job_title='SORTER' OR job_title='VERIFIER' AND blocked=1 and job_title <> 'PRICING' And job_title <> 'MLWB' " +
                "And job_title <> 'DISTRI' ORDER BY FULLNAME");
            dr = _sql.ExecuteDr();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.costcentersdata.costDept.Add(dr["FULLNAME"].ToString());
                }
                log.Info("valEmp: " + List.costcentersdata.costDept);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("valEmp: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("valEmp: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "valEmp: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult summaryRPTLoad(string userLog)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select  top 1 lotno_for from tbl_Forcedoutrcv where recname_for = '" + userLog +
                "' and Month(receivedate_for)=Month(getdate()) and Year(receivedate_for)= Year(getdate()) and Day(receivedate_for)= Day(getdate())" +
                " and outsource_status_for = 1");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.lotno = models.isNull(dr["lotno_for"]);

                log.Info("summaryRPTLoad: " + models.lotno);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("summaryRPTLoad: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("summaryRPTLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "summaryRPTLoad: " + respMessage(0) + ex.Message };
        }
    }
    #region REMReport
    public WCFRespALLResult DisplayMultipleSmmryRPT(string DB, string cases, int pmonth, int pyear, string pname, string month, string year,
        DateTime habwadate, string zone, int month2, int year2)
    {
        String c = "";
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.CellSummaryRPTdata = new List<CellSummaryRPTModels>();
        List.ReceivedBranchesRPTdata = new List<ReceivedBranchesRPTModels>();
        List.UnReceivedBranchesdata = new List<UnReceivedBranchesModels>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            if (DB == "")
            {
                _sql.Connection3();
                _sql.OpenConn3();
                if (cases == respCode(1))
                {
                    _sql.commandExeStoredParam3("ASYS_REMSummaryCell_rpt");
                    _sql.SummaryParams3("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var x = new CellSummaryRPTModels();
                            x.branchcode = dr["branchcode"].ToString().Trim();
                            x.branchname = dr["branchname"].ToString().Trim();
                            x.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c + c + "0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();dr["ptn"].ToString().Trim();
                            x.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            x.lotno = dr["lotno"].ToString().Trim();
                            x.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            x.refitemcode = dr["refitemcode"].ToString().Trim();
                            x.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            x.all_desc = dr["all_desc"].ToString().Trim();
                            x.SerialNo = dr["SerialNo"].ToString().Trim();
                            x.all_karat = dr["all_karat"].ToString().Trim();
                            x.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            x.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            x.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            x.sortdate = string.IsNullOrEmpty(dr["sortdate"].ToString()) ? null : Convert.ToDateTime(dr["sortdate"]).ToString("MMMM dd, yyyy").ToUpper();
                            x.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(x);
                        }
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
                else if (cases == "2")
                {
                    _sql.commandExeStoredParam3("ASYS_REMSummaryWatch_rpt");
                    _sql.SummaryParams3("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var z = new CellSummaryRPTModels();
                            z.branchcode = dr["branchcode"].ToString().Trim();
                            z.branchname = dr["branchname"].ToString().Trim();
                            z.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c + c + "0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();
                            z.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            z.lotno = dr["lotno"].ToString().Trim();
                            z.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            z.refitemcode = dr["refitemcode"].ToString().Trim();
                            z.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            z.all_desc = dr["all_desc"].ToString().Trim();
                            z.SerialNo = dr["SerialNo"].ToString().Trim();
                            z.all_karat = dr["all_karat"].ToString().Trim();
                            z.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            z.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            z.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            z.sortdate = string.IsNullOrEmpty(dr["sortdate"].ToString()) ? null : Convert.ToDateTime(dr["sortdate"]).ToString("MMMM dd, yyyy").ToUpper();
                            z.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(z);
                        }
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
                else if (cases == "3")
                {
                    _sql.commandExeStoredParam3("ASYS_REMSummaryGoodStock_rpt");
                    _sql.SummaryParams3("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var y = new CellSummaryRPTModels();
                            y.branchcode = dr["branchcode"].ToString().Trim();
                            y.branchname = dr["branchname"].ToString().Trim();
                            y.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c + c + "0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();
                            y.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            y.lotno = dr["lotno"].ToString().Trim();
                            y.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            y.refitemcode = dr["refitemcode"].ToString().Trim();
                            y.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            y.all_desc = dr["all_desc"].ToString().Trim();
                            y.transdate = string.IsNullOrEmpty(dr["transdate"].ToString()) ? null : Convert.ToDateTime(dr["transdate"]).ToString("yyyy MMMM").ToUpper();
                            y.all_karat = dr["all_karat"].ToString().Trim();
                            y.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            y.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            y.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            y.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(y);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                }
                else if (cases == "4")
                {
                    _sql.commandExeStoredParam3("procASYS_Received_Branches");
                    _sql.SummaryParams32("@MONTH", "@YEAR", month, year);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
                }
                else if (cases == "5")
                {
                    _sql.commandExeStoredParam3("ASYS_Unreceived_Branches");
                    _sql.SummaryParams321("@HABWADATE", "@ZONE", habwadate, zone);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var st = new UnReceivedBranchesModels();
                            st.BranchCode = dr["BranchCode"].ToString().Trim();
                            st.BranchName = dr["BranchName"].ToString().Trim();
                            st.zone = dr["zone"].ToString();
                            List.UnReceivedBranchesdata.Add(st);
                        }
                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.UnReceivedBranchesdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), UnReceivedBranchesdata = List.UnReceivedBranchesdata };
                }
                else if (cases == "6")
                {
                    _sql.commandExeStoredParam3("procASYS_Released_Branches");
                    _sql.SummaryParams32("@MONTH", "@YEAR", month, year);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
                }
                else if (cases == "7")
                {
                    _sql.commandExeStoredParam3("procASYS_UnReleased_branches");
                    _sql.SummaryParams35("@Month", "@Year", month2, year2);
                    dr = _sql.ExecuteDr3();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            m.MONTH = dr["MONTH"].ToString().Trim();
                            m.YEAR = dr["YEAR"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
                log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
            }
            else
            {
                _sql.Connection1(DB);
                _sql.OpenConn1();
                if (cases == respCode(1))
                {
                    _sql.commandExeStoredParam1("ASYS_REMSummaryCell_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var x = new CellSummaryRPTModels();
                            x.branchcode = dr["branchcode"].ToString().Trim();
                            x.branchname = dr["branchname"].ToString().Trim();
                            x.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c+c+"0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();
                            x.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            x.lotno = dr["lotno"].ToString().Trim();
                            x.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            x.refitemcode = dr["refitemcode"].ToString().Trim();
                            x.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            x.all_desc = dr["all_desc"].ToString().Trim();
                            x.SerialNo = dr["SerialNo"].ToString().Trim();
                            x.all_karat = dr["all_karat"].ToString().Trim();
                            x.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            x.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            x.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            x.sortdate = string.IsNullOrEmpty(dr["sortdate"].ToString()) ? null : Convert.ToDateTime(dr["sortdate"]).ToString("MMMM dd, yyyy").ToUpper();
                            x.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(x);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                }
                else if (cases == "2")
                {
                    _sql.commandExeStoredParam1("ASYS_REMSummaryWatch_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var z = new CellSummaryRPTModels();
                            z.branchcode = dr["branchcode"].ToString().Trim();
                            z.branchname = dr["branchname"].ToString().Trim();
                            z.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c + c + "0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();
                            z.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            z.lotno = dr["lotno"].ToString().Trim();
                            z.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            z.refitemcode = dr["refitemcode"].ToString().Trim();
                            z.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            z.all_desc = dr["all_desc"].ToString().Trim();
                            z.SerialNo = dr["SerialNo"].ToString().Trim();
                            z.all_karat = dr["all_karat"].ToString().Trim();
                            z.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            z.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            z.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            z.sortdate = string.IsNullOrEmpty(dr["sortdate"].ToString()) ? null : Convert.ToDateTime(dr["sortdate"]).ToString("MMMM dd, yyyy").ToUpper();
                            z.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(z);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                }
                else if (cases == "3")
                {
                    _sql.commandExeStoredParam1("ASYS_REMSummaryGoodStock_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pname", pmonth, pyear, pname);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var y = new CellSummaryRPTModels();
                            y.branchcode = dr["branchcode"].ToString().Trim();
                            y.branchname = dr["branchname"].ToString().Trim();
                            y.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? c + c + "0" : dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();//dr["ptn"].ToString().Trim();
                            y.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            y.lotno = dr["lotno"].ToString().Trim();
                            y.refallbarcode = dr["refallbarcode"].ToString().Trim();
                            y.refitemcode = dr["refitemcode"].ToString().Trim();
                            y.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            y.all_desc = dr["all_desc"].ToString().Trim();
                            y.transdate = string.IsNullOrEmpty(dr["transdate"].ToString()) ? null : Convert.ToDateTime(dr["transdate"]).ToString("yyyy MMMM").ToUpper();
                            y.all_karat = dr["all_karat"].ToString().Trim();
                            y.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                            y.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            y.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString().Trim()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                            y.sortername = dr["sortername"].ToString().Trim();
                            List.CellSummaryRPTdata.Add(y);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.CellSummaryRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CellSummaryRPTdata = List.CellSummaryRPTdata };
                }
                else if (cases == "4")
                {
                    _sql.commandExeStoredParam1("procASYS_Received_Branches");
                    _sql.SummaryParams12("@MONTH", "@YEAR", month, year);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
                }
                else if (cases == "5")
                {
                    _sql.commandExeStoredParam1("REMS.dbo.ASYS_Unreceived_Branches");
                    _sql.SummaryParams331("@HABWADATE", "@ZONE", habwadate, zone);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var st = new UnReceivedBranchesModels();
                            st.BranchCode = dr["BranchCode"].ToString().Trim();
                            st.BranchName = dr["BranchName"].ToString().Trim();
                            st.zone = dr["zone"].ToString().Trim();
                            List.UnReceivedBranchesdata.Add(st);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.UnReceivedBranchesdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), UnReceivedBranchesdata = List.UnReceivedBranchesdata };
                }
                else if (cases == "6")
                {
                    _sql.commandExeStoredParam1("procASYS_Released_Branches");
                    _sql.SummaryParams12("@MONTH", "@YEAR", month, year);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }

                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
                }
                else
                {
                    _sql.commandExeStoredParam1("procASYS_UnReleased_Branches");
                    _sql.SummaryParams356("@Month", "@Year", month2, year2);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var m = new ReceivedBranchesRPTModels();
                            m.BRANCHCODE = dr["BRANCHCODE"].ToString().Trim();
                            m.BRANCHNAME = dr["BRANCHNAME"].ToString().Trim();
                            m.MONTH = dr["MONTH"].ToString().Trim();
                            m.YEAR = dr["YEAR"].ToString().Trim();
                            List.ReceivedBranchesRPTdata.Add(m);
                        }
                    }
                    else
                    {
                        log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    log.Info("DisplayMultipleSmmryRPT: DB: " + DB + " | " + List.ReceivedBranchesRPTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReceivedBranchesRPTdata = List.ReceivedBranchesRPTdata };
                }
            }
        }
        catch (Exception ex)
        {
            if (DB == "")
            {
                _sql.CloseConn3();
            }
            else
            {
                _sql.CloseConn1();
            }
            log.Error("DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(0) + ex.Message);
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayMultipleSmmryRPT: DB: " + DB + " | " + respMessage(0) + ex.Message };
        }
    }//DONE
    #endregion

    //---------------------------------End February 9------------------//
    //---------------------------February 10,2017----------------steph--------------//
    public WCFRespALLResult getBranchLotno(string DB, string branchCode)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select top 1 ASYS_REM_Detail.Lotno as Lotno from ASYS_REM_Detail Inner join ASYS_REM_Header on" +
                " ASYS_REM_Detail.PTN = ASYS_REM_Header.PTN where year(receivedate) = year(getdate()) and month(receivedate)= Month(getdate())" +
                " and branchcode='" + branchCode + "' and lotno is not null");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.lotno = dr["Lotno"].ToString().Trim();

                log.Info("getBranchLotno: " + models.lotno);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("getBranchLotno: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("getBranchLotno: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getBranchLotno: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult RetrievePTN_Barcode(string DB, string branchCode)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.PTNBarcodedata = new PTNBarcodeModels();
        List.PTNBarcodedata.ptn = new List<string>();
        List.PTNBarcodedata.ptn_barcode = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select ptn, ptn_barcode from tbl_pt_tran where Year(transdate) = Year(getdate()) and  pulloutstocks " +
                "= 1 and transtype <> 'LUKAT' and pulloutdate is not null and received = 0 and division='" + branchCode +
                "' order by ptn, ptn_barcode");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.PTNBarcodedata.ptn.Add(List.PTNBarcodedata.ifNull(dr["ptn"]).Trim());
                    List.PTNBarcodedata.ptn_barcode.Add(List.PTNBarcodedata.ifNull(dr["ptn_barcode"]).Trim());
                }
                log.Info("RetrievePTN_Barcode: " + List.PTNBarcodedata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNBarcodedata = List.PTNBarcodedata };
            }
            else
            {
                log.Info("RetrievePTN_Barcode: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetrievePTN_Barcode: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrievePTN_Barcode: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult RetrieveInfoREM(string DB, string whichSet, string ptn)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichSet == respCode(1))
            {
                _sql.commandExeParam1("select status from rems"+DB+".dbo.ASYS_REM_Detail where ptn = '" + ptn + "'");
            }
            else
            {
                _sql.commandExeParam1("SELECT ASYS_REM_Detail.Status as status FROM  ASYS_REM_Detail  INNER JOIN  ASYS_REM_Header ON ASYS_REM_Detail.PTN" +
                    " = ASYS_REM_Header.PTN  where ASYS_REM_Header.ptnbarcode = '" + ptn + "'");
            }
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.lotno = models.isNull(dr["status"]).Trim();

                log.Info("RetrieveInfoREM: " + models.lotno);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("RetrieveInfoREM: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("RetrieveInfoREM: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrieveInfoREM: " + respMessage(0) + ex.Message };
        }

    }//DONE
    public WCFRespALLResult RetrieveInfoREM2(string db, string whichSet, string ptn)
   {
        var _sql = new Connection();
        var models = new PTNBarcode2Models();
        try
        {
            if (db == "LNCR")
            {
                db = "NCR";
            }
            _sql.Connection1(db);
            _sql.OpenConn1();
            if (whichSet == respCode(1))
            {
                _sql.commandExeParam1("SELECT TOP 1 GETDATE() as curdate, transdate, division, mptn, appraiser, custid, custmiddleinitial," +
                    " custfirstname, custlastname, custadd, custcity, custgender, custtelno, ptnprincipal, loandate, maturitydate," +
                    " expirydate, appraisevalue, ptn, ptn_barcode FROM REMS" + db + ".DBO.tbl_pt_tran WHERE ptn = '" + ptn +
                    "' AND transtype <> 'LUKAT' AND received=0 and pulloutdate is not null and ptstatus =1");
            }
            else
            {
                _sql.commandExeParam1("SELECT TOP 1 GETDATE() as curdate, transdate, division, mptn, appraiser, custid," +
                    " custmiddleinitial, custfirstname, custlastname, custadd, custcity, custgender, custtelno, ptnprincipal," +
                    " loandate, maturitydate, expirydate, appraisevalue, ptn, ptn_barcode FROM REMS" + db +
                    ".DBO.tbl_pt_tran WHERE ptn_barcode = '" + ptn + "' and transtype <> 'LUKAT' AND received=0 and pulloutdate is not null and ptstatus =1");
            }
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.transdate = dr["transdate"].ToString();
                models.curdate = dr["curdate"].ToString();
                models.Division = dr["Division"].ToString().Trim();
                models.mptn = dr["mptn"].ToString().Trim();
                models.Appraiser = models.ifNull(dr["Appraiser"]).ToString().Trim();
                models.CustID = dr["CustID"].ToString();
                models.CustMiddleInitial = models.ifNull(dr["CustMiddleInitial"]).ToString().Trim();
                models.CustFirstName = dr["CustFirstName"].ToString().Trim();
                models.CustLastName = dr["CustLastName"].ToString().Trim();
                models.CustAdd = models.ifNull(dr["CustAdd"].ToString());
                models.CustCity = models.ifNull(dr["CustCity"].ToString());
                models.CustGender = models.ifNull(dr["CustGender"]).ToUpper();
                models.CustTelno = models.ifNull(dr["CustTelno"].ToString());
                models.PTNPrincipal = dr["PTNPrincipal"].ToString();
                models.LoanDate = dr["LoanDate"].ToString().Trim();
                models.MaturityDate = dr["MaturityDate"].ToString().Trim();
                models.ExpiryDate = dr["ExpiryDate"].ToString().Trim();
                models.AppraiseValue = dr["AppraiseValue"].ToString();
                models.ptn = dr["ptn"].ToString().Trim();
                models.ptn_barcode = models.ifNull(dr["ptn_barcode"]).ToString().Trim();

                log.Info("RetrieveInfoREM2: " + models);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNBarcode2data = models };
            }
            else
            {
                log.Info("RetrieveInfoREM2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetrieveInfoREM2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrieveInfoREM2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult RetrieveInfoREM3(string db, string ptn)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.REMPopulatedata = new REMPopulateModels();
        List.REMPopulatedata.ID = new List<string>();
        List.REMPopulatedata.itemcode = new List<string>();
        List.REMPopulatedata.ptnitemdesc = new List<string>();
        List.REMPopulatedata.quantity = new List<string>();
        List.REMPopulatedata.KaratGrading = new List<string>();
        List.REMPopulatedata.CaratSize = new List<string>();
        List.REMPopulatedata.SortingClass = new List<string>();
        List.REMPopulatedata.Weight = new List<string>();
        try
        {
            if (db == "LNCR")
            {
                db = "NCR";
            }
            _sql.Connection1(db);
            _sql.OpenConn1();
            _sql.commandExeParam1("select distinct top 3 [id],itemcode,ptnitemdesc,quantity,karatgrading,caratsize,sortingclass,weight" +
                " from  rems"+db+".dbo.tbl_ptn_item where mptn = '" + ptn + "' ");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.REMPopulatedata.ID.Add(List.REMPopulatedata.isNull(dr["ID"].ToString().Trim()));
                    List.REMPopulatedata.itemcode.Add(List.REMPopulatedata.isNull(dr["itemcode"].ToString().Trim()));
                    List.REMPopulatedata.ptnitemdesc.Add(List.REMPopulatedata.isNull(dr["ptnitemdesc"].ToString().Trim()));
                    List.REMPopulatedata.quantity.Add(List.REMPopulatedata.isNull(dr["quantity"].ToString().Trim()));
                    List.REMPopulatedata.KaratGrading.Add(List.REMPopulatedata.isNull(dr["KaratGrading"].ToString().Trim()));
                    List.REMPopulatedata.CaratSize.Add(List.REMPopulatedata.isNull(dr["CaratSize"].ToString().Trim()));
                    List.REMPopulatedata.SortingClass.Add(List.REMPopulatedata.isNull(dr["SortingClass"].ToString().Trim()));
                    List.REMPopulatedata.Weight.Add(List.REMPopulatedata.isNull(dr["Weight"].ToString().Trim()));
                }
                log.Info("RetrieveInfoREM3: " + List.REMPopulatedata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), REMPopulatedata = List.REMPopulatedata };
            }
            else
            {
                log.Info("RetrieveInfoREM3: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetrieveInfoREM3: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrieveInfoREM3: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult SelectFrmBedryf(string DB, string branchName)
    {
        var _sql = new Connection();
        var models = new BedRnmBedrnrModels();
        try
        {
            if (DB == "LNCR")
            { 
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("Select top 1 * from REMS.dbo.bedryf" + DB + " where bedrnm like '%" + branchName + "%'");
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                models.bedrnr = dr["bedrnr"].ToString().Trim();
                models.bedrnm = dr["bedrnm"].ToString().Trim();

                log.Info("SelectFrmBedryf: " + models.bedrnr + " | " + models.bedrnm);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), BedRnmBedrnrdata = models };
            }
            else
            {
                log.Info("SelectFrmBedryf: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SelectFrmBedryf: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectFrmBedryf: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult DisplayDetails(string DB, string ptn, string whichCk)
    {
        var _sql = new Connection();
        var models = new DisplayDetailsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichCk == respCode(1))
            {
                _sql.commandExeParam1("Select BranchCode,BranchName,PTN,PTNBarcode,Loanvalue,Transdate,PullOutDate,custid,custname," +
                    "custaddress,custcity,custgender,custtel,maturitydate,expirydate,loandate from ASYS_REM_HEADER  where ptn ='" + ptn + "'");
            }
            else
            {
                _sql.commandExeParam1("Select BranchCode,BranchName,PTN,PTNBarcode,Loanvalue,Transdate,PullOutDate,custid,custname,custaddress" +
                    ",custcity,custgender,custtel,maturitydate,expirydate,loandate from ASYS_REM_HEADER  where ptnBarcode ='" + ptn + "'");
            }
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.BranchCode = dr["BranchCode"].ToString().Trim();
                models.BranchName = dr["BranchName"].ToString().Trim();
                models.PTNBarcode = models.isNull(dr["PTNBarcode"].ToString().Trim());
                models.LoanValue = dr["LoanValue"].ToString().Trim();
                models.PTN = dr["PTN"].ToString().Trim();
                models.Transdate = dr["Transdate"].ToString().Trim();
                models.Pulloutdate = dr["Pulloutdate"].ToString().Trim();
                models.CustID = models.isNull(dr["CustID"].ToString().Trim());
                models.CustName = models.isNull(dr["CustName"].ToString().Trim());
                models.CustAddress = models.isNull(dr["CustAddress"].ToString().Trim());
                models.CustCity = models.isNull(dr["CustCity"].ToString().Trim());
                models.CustGender = models.isNull(dr["CustGender"].ToString().Trim().ToUpper());
                models.CustTel = models.isNull(dr["CustTel"].ToString().Trim());
                models.MaturityDate = models.isNull(dr["MaturityDate"].ToString().Trim());
                models.ExpiryDate = models.isNull(dr["ExpiryDate"].ToString().Trim());
                models.loandate = models.isNull(dr["loandate"].ToString().Trim());
                log.Info("DisplayDetails: BranchCode: " + models.BranchCode + " | BranchName: " + models.BranchName + " | ptn: " + ptn);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayDetailsdata = models };
            }
            else
            {
                log.Info("DisplayDetails: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("DisplayDetails: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayDetails: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult DisplayDetails2(string DB, string ptn)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.DisplayDetails2data = new DisplayDetails2Models();
        List.DisplayDetails2data.ItemCode = new List<string>();
        List.DisplayDetails2data.BranchItemDesc = new List<string>();
        List.DisplayDetails2data.Qty = new List<string>();
        List.DisplayDetails2data.KaratGrading = new List<string>();
        List.DisplayDetails2data.CaratSize = new List<string>();
        List.DisplayDetails2data.ActionClass = new List<string>();
        List.DisplayDetails2data.Weight = new List<string>();
        List.DisplayDetails2data.Appraiser = new List<string>();
        List.DisplayDetails2data.AppraiseValue = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select distinct Itemcode,BranchItemDesc,Qty,KaratGrading,CaratSize,ActionClass,Weight,appraisevalue," +
                "appraiser from ASYS_REM_DEtail  where ptn ='" + ptn + "' order by itemcode");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.DisplayDetails2data.ItemCode.Add(dr["ItemCode"].ToString().Trim());
                    List.DisplayDetails2data.BranchItemDesc.Add(List.DisplayDetails2data.ifNull(dr["BranchItemDesc"].ToString()));
                    List.DisplayDetails2data.Qty.Add(List.DisplayDetails2data.ifNull(dr["Qty"].ToString()));
                    List.DisplayDetails2data.KaratGrading.Add(List.DisplayDetails2data.ifNull(dr["KaratGrading"].ToString()));
                    List.DisplayDetails2data.CaratSize.Add(List.DisplayDetails2data.ifNull(dr["CaratSize"].ToString()));
                    List.DisplayDetails2data.ActionClass.Add(List.DisplayDetails2data.ifNull(dr["ActionClass"].ToString()));
                    List.DisplayDetails2data.Weight.Add(List.DisplayDetails2data.ifNull(dr["Weight"].ToString()));
                    List.DisplayDetails2data.Appraiser.Add(List.DisplayDetails2data.ifNull(dr["Appraiser"].ToString()));
                    List.DisplayDetails2data.AppraiseValue.Add(List.DisplayDetails2data.ifNull(dr["AppraiseValue"].ToString()));
                }
                log.Info("DisplayDetails2: " + List.DisplayDetails2data + " | ptn: " + ptn);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayDetails2data = List.DisplayDetails2data };
            }
            else
            {
                log.Info("DisplayDetails2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("DisplayDetails2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayDetails2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    //---------------------------End February 10,2017----------------steph--------------//
    //---------------------------February 11,2017----------------steph--------------//
    public WCFRespALLResult getBedryf1(string DB, string branchCode)
    {
        var _sql = new Connection();
        var models = new getBedryfModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("Select bedrnm, class_04, class_03 from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + branchCode + "' and dateend is null");//Modify Query Split
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnm"].ToString().Trim().ToUpper();
                models.Class_04 = dr["Class_04"].ToString().Trim();
                models.Class_03 = dr["Class_03"].ToString().Trim();

                log.Info("getBedryf1: " + models.bedrnm + " | " + models.Class_04 + " | " + models.Class_03 + " | " + branchCode + " | " + DB);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1),getBedryfdata = models  };
            }
            else
            {
                log.Info("getBedryf1: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("getBedryf1: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getBedryf1: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult saveReceivingIndividually(string DB, string[][] ListView, string branchCode, string branchname, string lblRegion,
        string lblArea, string txt15, string txt16, double lbl31, string lbl36, string lbl10, string lbl23, string lbl24, string lbl25, string lbl26,
        string lbl27, string lbl28, string lbl29, string lbl30, string lbl32, string blotno, string combo4, string lbl22, double lbl33,
        string lblReceiver)
    {
        var _sql = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            //1st
            _sql.commandTraxParam1("Insert Into REMS" + DB + ".dbo.ASYS_REM_Header  (BranchCode,BranchName,Region,Area,PTN,PTNBarcode,LoanValue" +
                ",Transdate,PulloutDate,CustId,CustName,CustAddress,CustCity,CustGender,CustTel,PTTime,PTDate,MaturityDate,ExpiryDate,LoanDate)" +
                " Values ('" + branchCode + "', '" + branchname + "','" + lblRegion + "','" + lblArea + "', '" + txt15 + "','" + txt16 +
                "','" + lbl31 + "','" + lbl36 + "','" + lbl10 + "','" + lbl23 + "','" + lbl24 + "','" + lbl25 + "','" + lbl26 +
                "','" + lbl27 + "','" + lbl28 + "','" + lbl36 + "','" + lbl36 + "','" + lbl29 + "','" + lbl30 + "','" + lbl32 + "' )");
            _sql.Execute1();
            //2nd
            _sql.commandTraxParam1("Insert Into [REMS].dbo.ASYS_REM_Header (Lotno,BranchCode,BranchName,Region,Area,PTN,PTNBarcode,LoanValue," +
                "Transdate,PulloutDate,CustId,CustName,CustAddress,CustCity,CustGender,CustTel,PTTime,PTDate,MaturityDate,ExpiryDate,LoanDate)" +
                " Values ('" + blotno + "','" + branchCode + "', '" + branchname + "','" + lblRegion + "','" + lblArea + "', '" + txt15 + "','" + txt16 +
                "','" + lbl31 + "','" + lbl36 + "','" + lbl10 + "','" + lbl23 + "','" + lbl24 + "','" + lbl25 + "','" + lbl26 + "','" + lbl27 +
                "','" + lbl28 + "','" + lbl36 + "','" + lbl36 + "','" + lbl29 + "','" + lbl30 + "','" + lbl32 + "' )");
            _sql.Execute1();
            for (int i = 0; i <= ListView.Count() - 1; i++)
            {
                if (ListView[i] != null)
                {
                    //3rd
                    _sql.commandTraxParam1("Insert into REMS" + DB + ".dbo.ASYS_REM_detail (reflotno,lotno,PTN,itemid,RefItemcode,ItemCode," +
                        "BranchItemDesc,RefQty,Qty,KaratGrading,CaratSize,Weight,ActionClass,Sortcode,ALL_desc,ALL_Karat,ALL_carat,ALL_Weight," +
                        "Price_desc,Price_Karat,Price_weight,Price_carat,Appraiser,AppraiseValue,ReceiveDate,Receiver,Status)" +
                        " Values ('" + blotno + "','" + blotno + "','" + txt15 + "','" + ListView[i][7] + "','" + ListView[i][0] +
                        "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] + "','" + ListView[i][2] +
                        "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][6] + "','" + combo4 +
                        "','" + ListView[i][5] + "','" + ListView[i][1] + "','" + ListView[i][3] + "','" + ListView[i][4] +
                        "','" + ListView[i][6] + "','" + ListView[i][1] + "','" + ListView[i][3] + "','" + ListView[i][6] +
                        "','" + ListView[i][4] + "','" + lbl22 + "','" + lbl33 + "',getdate(),'" + lblReceiver + "','RECEIVED')");
                    _sql.Execute1();
                    //4th
                    _sql.commandTraxParam1("Insert into [REMS].dbo.ASYS_REM_detail (reflotno,lotno,PTN,itemid,RefItemcode,ItemCode,BranchItemDesc" +
                        ",RefQty,Qty,KaratGrading,CaratSize,Weight,ActionClass,Sortcode,ALL_desc,ALL_Karat,ALL_carat,ALL_Weight,Price_desc," +
                        "Price_Karat,Price_weight,Price_carat,Appraiser,AppraiseValue,ReceiveDate,Receiver,Status) Values" +
                        " ('" + blotno + "','" + blotno + "','" + txt15 + "','" + ListView[i][7] + "','" + ListView[i][0] +
                        "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] + "','" + ListView[i][2] +
                        "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][6] + "','" + combo4 + "','" + ListView[i][5] +
                        "','" + ListView[i][1] + "','" + ListView[i][3] + "','" + ListView[i][4] + "','" + ListView[i][6] +
                        "','" + ListView[i][1] + "','" + ListView[i][3] + "','" + ListView[i][6] + "','" + ListView[i][4] +
                        "','" + lbl22 + "','" + lbl33 + "',getdate(),'" + lblReceiver + "','RECEIVED')");
                    _sql.Execute1();
                }
                else
                {
                    break;
                }
            }
            //5th
            _sql.commandTraxParam1("Update tbl_pt_tran set received =1 where division = '" + branchCode + "' and ptn = '" + txt15 + "'");
            _sql.Execute1();

            log.Info("saveReceivingIndividually: " + respMessage(1) + " | " + DB + " | " + branchCode + " | " + blotno + " | " + lblReceiver);
            _sql.commitTransax1();
            //_sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };

        }
        catch (Exception ex)
        {
            log.Error("saveReceivingIndividually: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "saveReceivingIndividually: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult EditReceivingInDividually(string DB, string lblReceiver, string combo4, string txt15)
    {
        var _sql = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            //1st
            _sql.commandTraxParam1("update ASYS_REM_Detail set receivedate =  getdate(), receiver = '" + lblReceiver + "', actionclass = '" + combo4 + "' where ptn = '" + txt15 + "' and status = 'RECEIVED'");
            _sql.Execute1();
            //2nd
            _sql.commandTraxParam1("update rems.dbo.ASYS_REM_Detail set receivedate =  getdate(), receiver = '" + lblReceiver + "', actionclass = '" + combo4 + "' where ptn = '" + txt15 + "' and status  = 'RECEIVED' ");
            _sql.Execute1();
            log.Info("EditReceivingInDividually: " + respMessage(1) + " | " + DB + " | " + lblReceiver);
            _sql.commitTransax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("EditReceivingInDividually: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "EditReceivingInDividually: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveStats(string DB, string whichSet, string ptn)
    {
        var _sql = new Connection();
        var models = new PTQueryREtrieveModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichSet == respCode(1))
            {
                _sql.commandExeParam1("select rtrim(status) as status, sortcode, actionclass from asys_rem_detail  where ptn ='" + ptn + "'");
            }
            else
            {
                _sql.commandExeParam1("select rtrim(status) as status, sortcode, actionclass from asys_rem_detail  where ptn ='" + ptn + "' ");
            }
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.status = dr["status"].ToString();
                models.actionclass = dr["actionclass"].ToString();
                models.sortcode = dr["sortcode"].ToString();

                log.Info("PTNQueryRetrieveStats: " + models.status + " | " + models.actionclass + " | " + models.sortcode);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTQueryREtrievedata = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveStats: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveStats: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveStats: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTQueryREtrieveModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("SELECT asys_REM_header.branchcode as division, asys_REM_header.loanvalue, " +
                "isnull(asys_REM_detail.appraisevalue,0) as totalappraisevalue from asys_REM_header inner join asys_REM_detail" +
                " on asys_rem_header.ptn = asys_REM_detail.ptn where asys_rem_header.ptn= '" + ptn + "' ");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.status = dr["division"].ToString();
                models.sortcode = dr["LoanValue"].ToString();
                models.actionclass = dr["TotalAppraiseValue"].ToString();

                log.Info("PTNQueryRetrieveDetails: " + models.status + " | " + models.sortcode + " | " + models.actionclass);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTQueryREtrievedata = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails2(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTNQueryDetails2Models();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select asys_REM_header.branchcode as division, asys_REM_header.ptnbarcode," +
                " asys_REM_detail.appraisevalue as appraisevalue, asys_REM_header.loanvalue as loanvalue, asys_REM_header.loandate," +
                " asys_REM_header.maturitydate, asys_REM_header.expirydate from asys_REM_header" +
                " inner join asys_REM_detail on asys_REM_header.ptn = asys_REM_detail.ptn where asys_REM_header.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.Division = models.ifNull(dr["Division"].ToString().Trim());
                models.ptnbarcode = models.ifNull(dr["ptnbarcode"].ToString().Trim());
                //models.ptn = models.ifNull(dr["ptn"].ToString().Trim());
                log.Info("PTNQueryRetrieveDetails2: " + models.Division + " | " + models.ptnbarcode + " | " + models.ptn);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails2data = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails3(string DB, string ptn)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.PTNQueryDetails3data = new PTNQueryDetails3Models();
        List.PTNQueryDetails3data.ItemCode = new List<string>();
        List.PTNQueryDetails3data.itemdesc = new List<string>();
        List.PTNQueryDetails3data.Quantity = new List<string>();
        List.PTNQueryDetails3data.Karat = new List<string>();
        List.PTNQueryDetails3data.Carat = new List<string>();
        List.PTNQueryDetails3data.SortClass = new List<string>();
        List.PTNQueryDetails3data.Weight = new List<string>();
        List.PTNQueryDetails3data.AppraiseValue = new List<string>();
        List.PTNQueryDetails3data.ALL_price = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("Select asys_REM_detail.refitemcode as itemcode, asys_REM_detail.branchitemdesc as itemdesc," +
                " asys_REM_detail.refqty as quantity, asys_REM_detail.karatgrading as karat, asys_REM_detail.caratsize as carat," +
                " asys_REM_detail.actionclass as sortclass, asys_REM_detail.weight as weight, asys_REM_detail.appraisevalue as" +
                " appraisevalue, asys_REM_detail.all_price as all_price from asys_REM_header inner join asys_REM_detail on " +
                "asys_REM_header.ptn = asys_REM_detail.ptn where asys_REM_detail.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.PTNQueryDetails3data.ItemCode.Add(List.PTNQueryDetails3data.ifNull(dr["ItemCode"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.itemdesc.Add(List.PTNQueryDetails3data.ifNull(dr["itemdesc"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.Quantity.Add(List.PTNQueryDetails3data.ifNull(dr["Quantity"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.Karat.Add(List.PTNQueryDetails3data.ifNull(dr["Karat"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.Carat.Add(List.PTNQueryDetails3data.ifNull(dr["Carat"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.SortClass.Add(List.PTNQueryDetails3data.ifNull(dr["SortClass"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.Weight.Add(List.PTNQueryDetails3data.ifNull(dr["Weight"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.AppraiseValue.Add(List.PTNQueryDetails3data.ifNull(dr["AppraiseValue"]).ToString().Trim().ToUpper());
                    List.PTNQueryDetails3data.ALL_price.Add(List.PTNQueryDetails3data.ifNull(dr["ALL_price"]).ToString().Trim().ToUpper());
                }
                log.Info("PTNQueryRetrieveDetails3: " + List.PTNQueryDetails3data);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails3data = List.PTNQueryDetails3data };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails3: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails3: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails3: Service2 Error: " + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails4(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTNQueryDetails4Models();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select asys_REM_detail.receivedate as receivedate,asys_REM_detail.actionclass as " +
                "recactionid,asys_REM_detail.receiver as recname, asys_REM_detail.sortername as recsortername," +
                "asys_REM_detail.sortdate as sortdate, asys_REM_detail.costdate as costdate,asys_REM_detail.costname as costname," +
                " asys_REM_detail.releasedate as releasedate,asys_REM_detail.releaser as releaser" +
                " from asys_REM_detail where asys_REM_detail.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.receivedate = models.ifNull(dr["receivedate"]).Trim().ToUpper();
                models.recName = models.ifNull(dr["recName"]).Trim().ToUpper();
                log.Info("PTNQueryRetrieveDetails4: DB: " + DB + models.receivedate + " | " + models.recName + " | " + ptn);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails4data = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails4: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails4: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails4: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails5(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTNQueryDetails4Models();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select asys_REM_detail.receivedate as receivedate,asys_REM_detail.actionclass as " +
                "recactionid,asys_REM_detail.receiver as recname, asys_REM_detail.sortername as recsortername," +
                "asys_REM_detail.sortdate as sortdate, asys_REM_detail.costdate as costdate,asys_REM_detail.costname as costname," +
                " asys_REM_detail.releasedate as releasedate,asys_REM_detail.releaser as releaser" +
                " from asys_REM_detail where asys_REM_detail.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.recactionid = models.ifNull(dr["recactionid"]).Trim().ToUpper();
                models.sortdate = models.ifNull(dr["sortdate"]).Trim().ToUpper();
                models.recSorterName = models.ifNull(dr["recSorterName"]).Trim().ToUpper();
                log.Info("PTNQueryRetrieveDetails5: ptn: " + ptn + models.recactionid + " | " + models.sortdate + " | " + models.recSorterName);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails4data = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails5: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails5: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails5: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails6(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTNQueryDetails4Models();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select asys_REM_detail.receivedate as receivedate,asys_REM_detail.actionclass as " +
                "recactionid,asys_REM_detail.receiver as recname, asys_REM_detail.sortername as recsortername," +
                "asys_REM_detail.sortdate as sortdate, asys_REM_detail.costdate as costdate,asys_REM_detail.costname as costname," +
                " asys_REM_detail.releasedate as releasedate,asys_REM_detail.releaser as releaser" +
                " from asys_REM_detail where asys_REM_detail.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.recactionid = models.ifNull(dr["recactionid"]).Trim().ToUpper();
                models.costdate = models.ifNull(dr["costdate"]).Trim().ToUpper();
                models.costname = models.ifNull(dr["costname"]).Trim().ToUpper();
                log.Info("PTNQueryRetrieveDetails6: ptn: " + ptn + " | " + models.recactionid + " | " + models.costdate + " | " + models.costname);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails4data = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails6: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails6: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails6: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveDetails7(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new PTNQueryDetails4Models();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select asys_REM_detail.receivedate as receivedate,asys_REM_detail.actionclass as " +
                "recactionid,asys_REM_detail.receiver as recname, asys_REM_detail.sortername as recsortername," +
                "asys_REM_detail.sortdate as sortdate, asys_REM_detail.costdate as costdate,asys_REM_detail.costname as costname," +
                " asys_REM_detail.releasedate as releasedate,asys_REM_detail.releaser as releaser" +
                " from asys_REM_detail where asys_REM_detail.ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.recactionid = models.ifNull(dr["recactionid"]).Trim().ToUpper();
                models.releasedate = models.ifNull(dr["releasedate"]).Trim().ToUpper();
                models.releaser = models.ifNull(dr["releaser"]).Trim().ToUpper();
                log.Info("PTNQueryRetrieveDetails7: ptn: " + ptn + " | " + models.recactionid + " | " + models.releasedate + " | " + models.releaser);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNQueryDetails4data = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveDetails7: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveDetails7: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveDetails7: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveActClass(string DB)
    {
        var _sql = new Connection();
        var models = new PTQueryREtrieveModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select action_type from tbl_action where action_type = 'RELEASE'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.actionclass = dr["action_type"].ToString().Trim().ToUpper();
                log.Info("PTNQueryRetrieveActClass: DB: " + DB + " | " + models.actionclass);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTQueryREtrievedata = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveActClass: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveActClass: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveActClass: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult PTNQueryRetrieveActClass2(string DB, string sortCode)
    {
        var _sql = new Connection();
        var models = new PTQueryREtrieveModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select action_type from tbl_action where action_type = 'RELEASE'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.status = dr["action_type"].ToString().Trim().ToUpper();

                log.Info("PTNQueryRetrieveActClass2: DB: " + DB + " | " + models.status);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTQueryREtrievedata = models };
            }
            else
            {
                log.Info("PTNQueryRetrieveActClass2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("PTNQueryRetrieveActClass2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "PTNQueryRetrieveActClass2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    //---------------------------February 11,2017----------------steph--------------//
    //---------------------------February 13,2017----------------steph--------------//
    public WCFRespALLResult RetrievePTN_Barcode2(string DB, string branchCode)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.PTNBarcodedata = new PTNBarcodeModels();
        List.PTNBarcodedata.ptn = new List<string>();
        List.PTNBarcodedata.ptnbarcode = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select distinct ptn, ptnbarcode from asys_rem_header where branchcode = '" + branchCode +
                "' order by ptn, ptnbarcode");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.PTNBarcodedata.ptn.Add(List.PTNBarcodedata.ifNull(dr["ptn"]).Trim());
                    List.PTNBarcodedata.ptnbarcode.Add(List.PTNBarcodedata.ifNull(dr["ptnbarcode"]).Trim());
                }
                log.Info("RetrievePTN_Barcode2: " + List.PTNBarcodedata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNBarcodedata = List.PTNBarcodedata };
            }
            else
            {
                log.Info("RetrievePTN_Barcode2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetrievePTN_Barcode2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetrievePTN_Barcode2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult DisplayBranchName(string DB, string branchCode)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnm"].ToString().Trim();
                log.Info("DisplayBranchName: DB: " + DB + " | branchCode: " + branchCode + " | " + models.bedrnm);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("DisplayBranchName: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("DisplayBranchName: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayBranchName: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult DisplayBranchCode(string DB, string branchName)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("Select bedrnr from rems" + DB + ".dbo.vw_bedryf" + DB + " where bedrnm='" + branchName + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnr"].ToString().Trim();
                log.Info("DisplayBranchCode: DB: " + DB + " | " + branchName + " | " + models.bedrnm);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("DisplayBranchCode: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("DisplayBranchCode: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DisplayBranchCode: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult stringdate_maturity(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new MaturityDateModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("SELECT month(maturitydate) as month, day(maturitydate) as day, year(maturitydate) as year" +
                " FROM asys_REM_header WHERE ptn = '" + ptn + "' UNION ALL SELECT month(maturitydate) as month, day(maturitydate)" +
                " as day, year(maturitydate) as year FROM asys_remoutsource_detail WHERE ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.month = dr["month"].ToString();
                models.day = dr["day"].ToString();
                models.year = dr["year"].ToString();
                log.Info("stringdate_maturity: ptn: " + ptn + " | " + models.month + " | " + models.day + " | " + models.year);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), MaturityDatedata = models };
            }
            else
            {
                log.Info("stringdate_maturity: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("stringdate_maturity: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "stringdate_maturity: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult stringdate_expiry(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new MaturityDateModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("SELECT month(expirydate) as month, day(expirydate) as day, year(expirydate) as year" +
                " FROM asys_REM_header WHERE ptn = '" + ptn + "' UNION ALL SELECT month(expirydate) as month, day(expirydate)" +
                " as day, year(expirydate) as year FROM asys_remoutsource_detail WHERE ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.month = dr["month"].ToString();
                models.day = dr["day"].ToString();
                models.year = dr["year"].ToString();
                log.Info("stringdate_expiry: ptn: " + ptn + " | " + models.month + " | " + models.day + " | " + models.year);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), MaturityDatedata = models };
            }
            else
            {
                log.Info("stringdate_expiry: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("stringdate_expiry: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "stringdate_expiry: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult stringdate_loan(string DB, string ptn)
    {
        var _sql = new Connection();
        var models = new MaturityDateModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("SELECT month(loandate) as month, day(loandate) as day, year(loandate) as year " +
                "FROM asys_REM_header WHERE ptn = '" + ptn + "' UNION ALL SELECT month(loandate) as month, day(loandate) as day," +
                " year(loandate) as year FROM asys_remoutsource_detail WHERE ptn = '" + ptn + "'");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.month = dr["month"].ToString();
                models.day = dr["day"].ToString();
                models.year = dr["year"].ToString();
                log.Info("stringdate_loan: ptn: " + ptn + " | DB: " + DB + " | " + models.month + " | " + models.day + " | " + models.year);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), MaturityDatedata = models };
            }
            else
            {
                log.Info("stringdate_loan: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("stringdate_loan: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "stringdate_loan: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult Selectbranch(string DB, string branchName)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + branchName + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnm"].ToString();
                log.Info("Selectbranch: DB: " + DB + " | branchName: " + branchName + " | " + models.bedrnm);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("Selectbranch: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("Selectbranch: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "Selectbranch: " + respMessage(0) + ex.Message };
        }

    }//DONE
    public WCFRespALLResult SortLoad()
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeParam4("SELECT action_type FROM tbl_action where action_id in(3,4,5,6,8,9,10,11) ORDER BY action_type");
            dr = _sql.ExecuteDr4();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.costcentersdata.costDept.Add(dr["action_type"].ToString());
                }
                log.Info("SortLoad: " + List.costcentersdata.costDept);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("SortLoad: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SortLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SortLoad: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult getPTNData(string DB, string comboPTN)
    {
        var _sql = new Connection();
        var models = new DisplayDetailsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("SELECT BranchCode,BranchName,Ptn,PtnBarcode,Loanvalue,transdate,pulloutdate,custId,custname," +
                "custaddress,custcity,custgender,custtel,maturitydate,expirydate,loandate FROM ASYS_REM_Header WHERE ptn = '" + comboPTN + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {

                models.BranchCode = models.isNull(dr["BranchCode"].ToString().Trim());
                models.BranchName = models.isNull(dr["BranchName"].ToString().Trim());
                models.PTNBarcode = models.isNull(dr["PTNBarcode"].ToString().Trim());
                models.LoanValue = models.isNull(dr["LoanValue"].ToString().Trim());
                models.PTN = models.isNull(dr["PTN"].ToString().Trim());
                models.Transdate = models.isNull(dr["Transdate"].ToString().Trim());
                models.Pulloutdate = models.isNull(dr["Pulloutdate"].ToString().Trim());
                models.CustID = models.isNull(dr["CustID"].ToString().Trim());
                models.CustName = models.isNull(dr["CustName"].ToString().Trim());
                models.CustAddress = models.isNull(dr["CustAddress"].ToString().Trim());
                models.CustCity = models.isNull(dr["CustCity"].ToString().Trim());
                models.CustGender = models.isNull(dr["CustGender"].ToString().Trim().ToUpper());
                models.CustTel = models.isNull(dr["CustTel"].ToString().Trim());
                models.MaturityDate = string.IsNullOrEmpty(dr["MaturityDate"].ToString()) ? null : Convert.ToDateTime(dr["MaturityDate"]).ToString("MM/dd/yyyy");
                models.ExpiryDate = string.IsNullOrEmpty(dr["ExpiryDate"].ToString()) ? null : Convert.ToDateTime(dr["ExpiryDate"]).ToString("MM/dd/yyyy");
                models.loandate = string.IsNullOrEmpty(dr["loandate"].ToString()) ? null : Convert.ToDateTime(dr["loandate"]).ToString("MM/dd/yyyy");
                _sql.CloseDr1();
                _sql.CloseConn1();
                log.Info("getPTNData: " + models);
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayDetailsdata = models };
            }
            else
            {
                log.Info("getPTNData: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("getPTNData: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getPTNData: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult GetPTNDoData(string DB, string comboPTN)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.getPTNdatadata = new getPTNdataModels();
        List.getPTNdatadata.refitemcode = new List<string>();
        List.getPTNdatadata.id = new List<string>();
        List.getPTNdatadata.branchitemdesc = new List<string>();
        List.getPTNdatadata.refqty = new List<string>();
        List.getPTNdatadata.karatgrading = new List<string>();
        List.getPTNdatadata.all_cost = new List<string>();
        List.getPTNdatadata.all_karat = new List<string>();
        List.getPTNdatadata.caratsize = new List<string>();
        List.getPTNdatadata.weight = new List<string>();
        List.getPTNdatadata.all_weight = new List<string>();
        List.getPTNdatadata.appraiser = new List<string>();
        List.getPTNdatadata.appraisevalue = new List<string>();
        List.getPTNdatadata.reflotno = new List<string>();
        List.getPTNdatadata.status = new List<string>();
        List.getPTNdatadata.refallbarcode = new List<string>();
        List.getPTNdatadata.sortcode = new List<string>();
        List.getPTNdatadata.actionclass = new List<string>();

        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("SELECT reflotno,refallbarcode,itemid as id,refitemcode,branchitemdesc,refqty,karatgrading,caratsize," +
                "weight,actionclass,sortcode,all_karat,all_cost,all_weight,appraiser,appraisevalue,status" +
                " FROM ASYS_REM_Detail WHERE ptn = '" + comboPTN + "' ORDER BY refitemcode ASC");
            dr = _sql.ExecuteDr1();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.getPTNdatadata.refitemcode.Add(List.getPTNdatadata.IfNuL(dr["refitemcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.id.Add(List.getPTNdatadata.IfNuL(dr["id"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.branchitemdesc.Add(List.getPTNdatadata.IfNuL(dr["branchitemdesc"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.refqty.Add(List.getPTNdatadata.IfNuL(dr["refqty"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.karatgrading.Add(List.getPTNdatadata.IfNuL(dr["karatgrading"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_karat.Add(List.getPTNdatadata.IfNuL(dr["all_karat"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.caratsize.Add(List.getPTNdatadata.IfNuL(dr["caratsize"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.sortcode.Add(List.getPTNdatadata.IfNuL(dr["sortcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.weight.Add(List.getPTNdatadata.IfNuL(dr["weight"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_weight.Add(List.getPTNdatadata.IfNuL(dr["all_weight"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.appraisevalue.Add(List.getPTNdatadata.IfNuL(dr["appraisevalue"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_cost.Add(List.getPTNdatadata.IfNuL(dr["all_cost"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.refallbarcode.Add(List.getPTNdatadata.IfNuL(dr["refallbarcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.appraiser.Add(List.getPTNdatadata.IfNuL(dr["appraiser"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.reflotno.Add(List.getPTNdatadata.IfNuL(dr["reflotno"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.status.Add(List.getPTNdatadata.IfNuL(dr["status"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.actionclass.Add(List.getPTNdatadata.IfNuL(dr["actionclass"]).ToString().ToUpper().Trim());
                }
                log.Info("GetPTNDoData: " + List.getPTNdatadata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getPTNdatadata = List.getPTNdatadata };
            }
            else
            {
                log.Info("GetPTNDoData: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetPTNDoData: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetPTNDoData: " + respMessage(0) + ex.Message };
        }
    }//DONE
    //---------------------------February 13,2017----------------steph--------------//
    //---------------------------February 14,2017----------------steph--------------//
    public WCFRespALLResult generateAllBarcode(string barcode)
    {
        var _sql = new Connection();
        //var _sql = new Connection();
        var models = new IntBarcodeModels();
        typeClass = 3;//--> The previous value here was 1 
        try
        {
         
         
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
            _sql.generateAllBarcode("@allbarcode", "@type", barcode, typeClass);
            dr = _sql.ExecuteDr4();
            if (dr.Read())
            {
                models.newllBarcode = Convert.ToInt64(dr["REFALLBARCODE"]) + 1;
                models.allBarcode = models.newllBarcode.ToString();

                log.Info("generateAllBarcode2: " + models.barcode);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), IntBarcodedata = models };
            }
            else
            {
                log.Info("generateAllBarcode: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("generateAllBarcode: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "generateAllBarcode: " + respMessage(0) + ex.Message };
        }

    }
    public WCFRespALLResult generateAllBarcode2(string barcode)
    {
        var _sql = new Connection();
        var models = new IntBarcodeModels();
        typeClass = 2;
        try
        {
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
            _sql.generateAllBarcode("@allbarcode", "@type", barcode, typeClass);
            dr = _sql.ExecuteDr4();
            if (dr.Read())
            {
                models.newllBarcode = Convert.ToInt64(dr["refallbarcode"]) +1;
                models.allBarcode = models.newllBarcode.ToString();
                log.Info("generateAllBarcode2: " + models.barcode);

                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), IntBarcodedata = models };
            }
            else
            {
                log.Info("generateAllBarcode2: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("generateAllBarcode2: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "generateAllBarcode2: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult generateAllBarcode3(string barcode)
    {
        var _sql = new Connection();
        var models = new IntBarcodeModels();
        typeClass = 1;
        try
        {
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
            _sql.generateAllBarcode("@allbarcode", "@type", barcode, typeClass);
            dr = _sql.ExecuteDr4();
           if (dr.Read())
            {
                models.barcode = Convert.ToInt32(dr["barcode"]) + 1;

                models.allBarcode = models.barcode.ToString();

                log.Info("generateAllBarcode3: " + models.barcode);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), IntBarcodedata = models };
            }
            else
            {
                log.Info("generateAllBarcode3: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("generateAllBarcode3: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "generateAllBarcode3: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult getBranchPTN_PTNbarcode(string DB, string branchCode)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.PTNBarcodedata = new PTNBarcodeModels();
        List.PTNBarcodedata.ptn = new List<string>();
        List.PTNBarcodedata.ptnbarcode = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("SELECT DISTINCT TOP 100 PERCENT rh.PTNBarcode, rd.PTN FROM dbo.ASYS_REM_Detail rd INNER JOIN" +
                " dbo.ASYS_REM_Header rh ON rd.PTN = rh.PTN WHERE rd.Status NOT IN ('RELEASED','RECMLWB')" +
                " AND rh.BranchCode = '" + branchCode + "' ORDER BY rd.PTN ");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.PTNBarcodedata.ptn.Add(List.PTNBarcodedata.ifNull(dr["ptn"]).ToString().Trim().ToUpper());
                    List.PTNBarcodedata.ptnbarcode.Add(List.PTNBarcodedata.ifNull(dr["ptnbarcode"]).ToString().Trim().ToUpper());
                }
                log.Info("getBranchPTN_PTNbarcode: " + List.PTNBarcodedata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PTNBarcodedata = List.PTNBarcodedata };
            }
            else
            {
                log.Info("getBranchPTN_PTNbarcode: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("getBranchPTN_PTNbarcode: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getBranchPTN_PTNbarcode: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult getPTNBarcodeData(string DB, string PTN)
    {
        var _sql = new Connection();
        var models = new DisplayDetailsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("Select BranchCode,BranchName,Ptn,PtnBarcode,Loanvalue,transdate,pulloutdate,custId,custname,custaddress" +
                ",custcity,custgender,custtel,pttime,ptdate,maturitydate,expirydate,loandate from ASYS_REM_Header  where ptnbarcode = '" + PTN + "'");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.BranchCode = models.isNull(dr["branchcode"]).ToString().ToUpper().Trim();
                models.BranchName = models.isNull(dr["branchname"]).ToString().ToUpper().Trim();
                models.PTNBarcode = models.isNull(dr["ptnbarcode"]).ToString().ToUpper().Trim();
                models.pttime = models.isNull(dr["pttime"]).ToString().ToUpper().Trim();
                models.ptdate = models.isNull(dr["ptdate"]).ToString().ToUpper().Trim();
                models.CustID = models.isNull(dr["custid"]).ToString().ToUpper().Trim();
                models.CustName = models.isNull(dr["Custname"]).ToString().ToUpper().Trim();
                models.CustAddress = models.isNull(dr["custaddress"]).ToString().ToUpper().Trim();
                models.CustCity = models.isNull(dr["custcity"]).ToString().ToUpper().Trim();
                models.CustGender = models.isNull(dr["custgender"]).ToString().ToUpper().Trim();
                models.CustTel = models.isNull(dr["custtel"]).ToString().ToUpper().Trim();
                models.LoanValue = models.isNull(dr["loanvalue"]).ToString().ToUpper().Trim();
                models.loandate = models.isNull(dr["loandate"]).ToString().ToUpper().Trim();
                models.MaturityDate = models.isNull(dr["maturitydate"]).ToString().ToUpper().Trim();
                models.ExpiryDate = models.isNull(dr["expirydate"]).ToString().ToUpper().Trim();
                log.Info("getPTNBarcodeData: " + models.BranchCode + " | " + models.BranchName);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayDetailsdata = models };
            }
            else
            {
                log.Info("getPTNBarcodeData: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("getPTNBarcodeData: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getPTNBarcodeData: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult getPTNBarcodeData2(string DB, string cmbPTN)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.getPTNdatadata = new getPTNdataModels();
        List.getPTNdatadata.status = new List<string>();
        List.getPTNdatadata.actionclass = new List<string>();
        List.getPTNdatadata.sortcode = new List<string>();
        List.getPTNdatadata.refitemcode = new List<string>();
        List.getPTNdatadata.id = new List<string>();
        List.getPTNdatadata.branchitemdesc = new List<string>();
        List.getPTNdatadata.refqty = new List<string>();
        List.getPTNdatadata.karatgrading = new List<string>();
        List.getPTNdatadata.all_karat = new List<string>();
        List.getPTNdatadata.caratsize = new List<string>();
        List.getPTNdatadata.weight = new List<string>();
        List.getPTNdatadata.all_weight = new List<string>();
        List.getPTNdatadata.all_cost = new List<string>();
        List.getPTNdatadata.appraisevalue = new List<string>();
        List.getPTNdatadata.refallbarcode = new List<string>();
        List.getPTNdatadata.appraiser = new List<string>();
        List.getPTNdatadata.reflotno = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("Select reflotno,refallbarcode,itemid as id,refitemcode,branchitemdesc,refqty,karatgrading,caratsize," +
                "weight,actionclass,sortcode,all_karat,all_cost,all_weight,appraiser,appraisevalue,status from ASYS_REM_detail" +
                " where ptn = '" + cmbPTN + "'");
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.getPTNdatadata.refitemcode.Add(List.getPTNdatadata.IfNuL(dr["refitemcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.id.Add(List.getPTNdatadata.IfNuL(dr["id"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.branchitemdesc.Add(List.getPTNdatadata.IfNuL(dr["branchitemdesc"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.refqty.Add(List.getPTNdatadata.IfNuL(dr["refqty"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.karatgrading.Add(List.getPTNdatadata.IfNuL(dr["karatgrading"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_karat.Add(List.getPTNdatadata.IfNuL(dr["all_karat"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.caratsize.Add(List.getPTNdatadata.IfNuL(dr["caratsize"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.sortcode.Add(List.getPTNdatadata.IfNuL(dr["sortcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.weight.Add(List.getPTNdatadata.IfNuL(dr["weight"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_weight.Add(List.getPTNdatadata.IfNuL(dr["all_weight"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.appraisevalue.Add(List.getPTNdatadata.IfNuL(dr["appraisevalue"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.all_cost.Add(List.getPTNdatadata.IfNuL(dr["all_cost"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.refallbarcode.Add(List.getPTNdatadata.IfNuL(dr["refallbarcode"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.appraiser.Add(List.getPTNdatadata.IfNuL(dr["appraiser"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.reflotno.Add(List.getPTNdatadata.IfNuL(dr["reflotno"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.status.Add(List.getPTNdatadata.IfNuL(dr["status"]).ToString().ToUpper().Trim());
                    List.getPTNdatadata.actionclass.Add(List.getPTNdatadata.IfNuL(dr["actionclass"]).ToString().ToUpper().Trim());
                }
                log.Info("getPTNBarcodeData2: " + List.getPTNdatadata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getPTNdatadata = List.getPTNdatadata };
            }
            else
            {
                log.Info("getPTNBarcodeData2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("getPTNBarcodeData2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "getPTNBarcodeData2: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult checkBarcodeIfExist(string barcode)
    {
        var _sql = new Connection();
        var models = new BoolVarModels();
        typeClass = 3;
        try
        {
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
            _sql.generateAllBarcode("@allbarcode", "@type", barcode, typeClass);
            dr = _sql.ExecuteDr4();
            if (dr.Read())
            {
                models.exist = true;

                log.Info("checkBarcodeIfExist: " + models.exist);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "Exists!", BoolVardata = models };
            }
            else
            {
                log.Info("checkBarcodeIfExist: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("checkBarcodeIfExist: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "checkBarcodeIfExist: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult saveSortData(string DB, string[][] Listview, string ActionClass, string sortcode, string userLog, string PTN, string typex,
        string lotno)
    {
        var _sql = new Connection();
        String x,y,result,message;
        try
        {

            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            for (int i = 0; i <= Listview.Count() - 1; i++)
            {


                //if (Listview[i] != null && bcodechecker(Listview[i][5])!= true && red == false)
                if (Listview[i] != null && red == false)
                {
                    if (ActionClass == "GOODSTOCK" || ActionClass == "CELLULAR" || ActionClass == "WATCH")
                    {
                        #region
                        //_sql.commandTraxParam1("SELECT SORTERNAME,REFALLBARCODE FROM REMS.DBO.ASYS_REM_DETAIL WHERE REFALLBARCODE ='" + Listview[i][5] + "'");

                        //dr = _sql.ExecuteDr1();//-------------------------------
                        //if (dr.HasRows == true)
                        //{
                        //    dr.Read();
                        //    x = dr["SORTERNAME"].ToString();
                        //    y = dr["REFALLBARCODE"].ToString();
                        //    dr.Close();
                        //    result = "3";
                        //    message = "ALLBARCODE ALREADY EXIST: " + y + "";//SORTED BY:" +x+ "";
                        //    //
                        //    Int64 newbarcode = Convert.ToInt64(y) + 1;

                        //    //return new WCFRespALLResult { responseCode = result, responseMsg = message };
                        //    dr.Close();
                        //    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + newbarcode.ToString() +
                        //                        "',allbarcode='" + newbarcode.ToString() + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                        //                        "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                        //                        "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        //    _sql.Execute1();
                        //    _sql.commandTraxParam1("UPDATE [REMS].DBO.ASYS_REM_Detail SET refallbarcode='" + newbarcode.ToString() +
                        //        "',allbarcode='" + newbarcode.ToString() + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                        //        "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                        //        "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        //    _sql.Execute1();

                        //    if (typex == "GOODSTOCK" || typex == "CELLULAR" || typex == "WATCH")
                        //    {

                        //        _sql.commandTraxParam1("INSERT INTO [REMS].DBO.ASYS_Barcodehistory (lotno, refallbarcode, allbarcode, itemcode, itemid," +
                        //           " [description], karat, carat, weight, empname,trandate, costcenter, status) VALUES ('" + lotno + "','" + newbarcode.ToString() +
                        //           "','" + newbarcode.ToString() + "','" + Listview[i][6] + "','" + Listview[i][0] + "','" + Listview[i][1] +
                        //           "','" + Listview[i][2] + "','" + Listview[i][3] + "','" + Listview[i][4] + "','" + userLog + "',GETDATE(),'REM','RECEIVED')");
                        //        _sql.Execute1();
                        //    }
                        //    //_sql.RollBackTrax1();
                        //}

                        //else
                        //{
                        //    dr.Close();
                        //    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                        //                        "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                        //                        "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                        //                        "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        //    _sql.Execute1();
                        //    _sql.commandTraxParam1("UPDATE [REMS].DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                        //        "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                        //        "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                        //        "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        //    _sql.Execute1();

                        //    if (typex == "GOODSTOCK" || typex == "CELLULAR" || typex == "WATCH")
                        //    {

                        //        _sql.commandTraxParam1("INSERT INTO [REMS].DBO.ASYS_Barcodehistory (lotno, refallbarcode, allbarcode, itemcode, itemid," +
                        //           " [description], karat, carat, weight, empname,trandate, costcenter, status) VALUES ('" + lotno + "','" + Listview[i][5] +
                        //           "','" + Listview[i][5] + "','" + Listview[i][6] + "','" + Listview[i][0] + "','" + Listview[i][1] +
                        //           "','" + Listview[i][2] + "','" + Listview[i][3] + "','" + Listview[i][4] + "','" + userLog + "',GETDATE(),'REM','RECEIVED')");
                        //        _sql.Execute1();
                        //    }

                        //    //-----> RESPONSE MESSAGE HERE! 
                        //   // _sql.RollBackTrax1();
                        //}
                        #endregion

                        //dr.Close();
                        _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                                            "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                                            "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                                            "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        _sql.Execute1();
                        _sql.commandTraxParam1("UPDATE [REMS].DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                            "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                            "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                            "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                        _sql.Execute1();

                        if (typex == "GOODSTOCK" || typex == "CELLULAR" || typex == "WATCH")
                        {

                            _sql.commandTraxParam1("INSERT INTO [REMS].DBO.ASYS_Barcodehistory (lotno, refallbarcode, allbarcode, itemcode, itemid," +
                               " [description], karat, carat, weight, empname,trandate, costcenter, status) VALUES ('" + lotno + "','" + Listview[i][5] +
                               "','" + Listview[i][5] + "','" + Listview[i][6] + "','" + Listview[i][0] + "','" + Listview[i][1] +
                               "','" + Listview[i][2] + "','" + Listview[i][3] + "','" + Listview[i][4] + "','" + userLog + "',GETDATE(),'REM','RECEIVED')");
                            _sql.Execute1();
                        }

                    }
                    else 
                    {
                        //if (bcodechecker(Listview[i][5]) != true && red == false)
                        if (red == false)
                        {
                            _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                                                                         "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                                                                         "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                                                                         "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                            _sql.Execute1();
                            _sql.commandTraxParam1("UPDATE [REMS].DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][5] +
                                "',allbarcode='" + Listview[i][5] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                                "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                                "' AND status NOT IN ('RELEASED','RECMLWB') AND itemid = '" + Listview[i][0] + "' ");

                            _sql.Execute1();
                        }
                        else
                        {
                            return new WCFRespALLResult { responseCode = respCode(0), responseMsg =result1};
                        }
                       
                        //
                    }

                    //_sql.RollBackTrax1();
                }
                else
                {
                    red = false;
                    
                    _sql.RollBackTrax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = result1};
                }
            }
            log.Info("saveSortData: " + respMessage(1) + " | DB: " + DB + " | PTN: " + PTN + " | userLog: " + userLog + " | lotno: " + lotno);
            //_sql.commitTransax1();
            red = false;
            _sql.commitTransax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            
        }
        catch (Exception ex)
        {
            String trapThis;
            if (ex.Message.Contains("deadlocked"))
            {
                trapThis = "This issue is typically caused by parallel transactions happening at the same time or probably barcode already exist";
            }
            else
            {
                trapThis = ex.Message;
            }
            log.Error("saveSortData: " + respMessage(0) + ex.Message);
           // dr.Close();
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "saveSortData: " + respMessage(0) + trapThis.ToString() };
        }
    }//DONE

    public WCFRespALLResult saveSortData2(string DB, string[][] Listview, string ActionClass, string sortcode, string userLog, string PTN, string typex,
      string lotno)
    {
        var _sql = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            for (int i = 0; i <= Listview.Count() - 1; i++)
            {
                //if (Listview[i] != null && bcodechecker(Listview[i][5]) != true && red == false)
                if (Listview[i] != null && red == false)
                {
                 
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][0] +
                      "',allbarcode='" + Listview[i][0] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                      "',all_karat='" + Listview[i][1] + "',all_weight = '" + Listview[i][2] + "',appraisevalue = " +Listview[i][11]+//-----+
                      ",Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                      " 'and itemid = '" + Listview[i][4] + "' AND status NOT IN ('RELEASED','RECMLWB') ");
                    _sql.Execute1();

                    _sql.commandTraxParam1("UPDATE [REMS].DBO.ASYS_REM_Detail SET refallbarcode='" + Listview[i][0] +
                        "',allbarcode='" + Listview[i][0] + "',Actionclass = '" + ActionClass + "',sortcode = '" + sortcode +
                  "',all_karat='" + Listview[i][1] + "',all_weight = '" + Listview[i][2] + "',appraisevalue = " + Listview[i][11] +//-----+
                        ",Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + PTN +
                        "' and itemid = '" + Listview[i][4] + "' AND status NOT IN ('RELEASED','RECMLWB') ");
                    _sql.Execute1();

                    if (typex == "GOODSTOCK" || typex == "CELLULAR" || typex == "WATCH")
                    {
                       
                        _sql.commandTraxParam1("INSERT INTO [REMS].DBO.ASYS_Barcodehistory (lotno, refallbarcode, allbarcode, itemcode, itemid," +
                           " [description], karat, carat, weight, empname,trandate, costcenter, status) VALUES ('" + lotno + "','" + Listview[i][0] +
                           "','" + Listview[i][0] + "','" + Listview[i][5] + "','" + Listview[i][4] + "','" + Listview[i][8] +
                           "','" + Listview[i][1] + "','" + Listview[i][6] + "','" + Listview[i][6] + "','" + userLog + "',GETDATE(),'REM','RECEIVED')");
                        _sql.Execute1();
                    }
                }
                else
                {
                    red = false;

                    _sql.RollBackTrax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = result1 };
                }
            }
            log.Info("saveSortData: " + respMessage(1) + " | DB: " + DB + " | PTN: " + PTN + " | userLog: " + userLog + " | lotno: " + lotno);
            _sql.commitTransax1();
     
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("saveSortData: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "saveSortData: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult SelectBedryfByBranchCode(string DB, string branchCode)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeParam4("SELECT bedrnm FROM REMS.dbo.vw_bedryf" + DB + " WHERE bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr4();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnm"].ToString();

                log.Info("SelectBedryfByBranchCode: " + models.bedrnm);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("SelectBedryfByBranchCode: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SelectBedryfByBranchCode: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectBedryfByBranchCode: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult SelectBedryfByBranchName(string branchName, string DB)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("SELECT bedrnr FROM REMS.dbo.vw_bedryf" + DB + " WHERE bedrnm='" + branchName + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnr"].ToString();
                log.Info("SelectBedryfByBranchName: " + models.bedrnm);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("SelectBedryfByBranchName: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SelectBedryfByBranchName: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectBedryfByBranchName: " + respMessage(0) + ex.Message };
        }
    }//DONE
    public WCFRespALLResult OnLoadEdit()
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            _sql.Connection4();
            _sql.OpenConn4();
            _sql.commandExeParam4("SELECT action_type, action_id FROM REMS.DBO.tbl_action WHERE action_id in(3,4,5,6,8,9,10,11) ORDER BY action_type");
            dr = _sql.ExecuteDr4();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.costcentersdata.costDept.Add(dr["action_type"].ToString().Trim().ToUpper());
                }
                log.Info("OnLoadEdit: " + List.costcentersdata);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("OnLoadEdit: " + respMessage(2));
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("OnLoadEdit: " + respMessage(0) + ex.Message);
            _sql.CloseConn4();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "OnLoadEdit: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult saveEditData(string DB, string typeClass, string[][] ListView, string cmbAction, string sortcode, string userLog, string ptn,
        string sortLotno)
    {
        var _sql = new Connection();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.BeginTransax1();
            for (int i = 0; i <= ListView.Count() - 1; i++)
            {
                if (ListView[i] != null)
                {
                    //1st
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_Detail SET refallbarcode='" + ListView[i][8] +
                        "',allbarcode='" + ListView[i][8] + "',Actionclass = '" + cmbAction + "',sortcode = '" + sortcode +
                        "',all_karat = '" + ListView[i][3] + "',all_weight = '" + ListView[i][6] + "',all_cost = '" + ListView[i][7] +
                        "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + ptn + "' AND itemid = '" + ListView[i][0] +
                        "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                    _sql.Execute1();
                    //2nd
                    _sql.commandTraxParam1("UPDATE REMS.DBO.ASYS_REM_Detail SET refallbarcode='" + ListView[i][8] + "',allbarcode='" + ListView[i][8] +
                        "',Actionclass = '" + cmbAction + "',sortcode = '" + sortcode + "',all_karat = '" + ListView[i][3] + "',all_weight = '" + ListView[i][6] +
                        "',all_cost = '" + ListView[i][7] + "',Sortdate = getdate(),sortername = '" + userLog + "',status = 'SORTED' WHERE ptn ='" + ptn +
                        "' AND itemid = '" + ListView[i][0] + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                    _sql.Execute1();
                    if (typeClass == "GOODSTOCK" || typeClass == "CELLULAR" || typeClass == "WATCH")
                    {
                        _sql.commandTraxParam1("INSERT INTO [REMS].DBO.ASYS_Barcodehistory (lotno, refallbarcode, allbarcode, itemcode,itemid," +
                            " [description], karat, carat, weight, empname,trandate, costcenter,  status ) VALUES ('" + sortLotno + "','" + ListView[i][8] +
                            "','" + ListView[i][8] + "','" + ListView[i][10] + "','" + ListView[i][0] + "','" + ListView[i][1] + "','" + ListView[i][2] +
                            "','" + ListView[i][4] + "','" + ListView[i][5] + "','" + userLog + "',getdate(),'REM','RECEIVED')");
                        _sql.Execute1();
                    }
                    else
                    {
                        _sql.commandTraxParam1("DELETE FROM [REMS].DBO.ASYS_Barcodehistory WHERE itemid='" + ListView[i][0] + "' AND itemcode='" + ListView[i][11] +
                            "' AND LOTNO='" + ListView[i][9] + "' AND costcenter='REM' AND status='RECEIVED'");
                        _sql.Execute1();
                    }

                }
                else
                {
                    break;
                }
            }
            log.Info("saveEditData: " + respMessage(1) + " | DB: " + DB + " | typeClass: " + typeClass + " | userLog: " + userLog + " | ptn: " + ptn);
            _sql.commitTransax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("saveEditData: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax1();
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "saveEditData: " + respMessage(0) + ex.Message };
        }
    }//DONE

    public WCFRespALLResult SavePhoto(string ptn, string ifPTNVisible, string txt1, string DB, byte[] photoname, String unit_address)
    {

        var _sql = new Connection();
        String Destinationpath,nave,x,local;
        String folders;

       
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();

     

            bool f = Directory.Exists(Connection.GoodstockSDestination);
            if (f == true)
            {
                asysSaveImagefile1 = Connection.GoodstockSDestination;
                if (ifPTNVisible == "Visible")
                {

                    Destinationpath = Connection.JewelrySDestination;
                }
                else
                {
                    Destinationpath = Connection.GoodstockSDestination;
                   
                }
            }
            else
            {

                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SavePhoto: " + respMessage(0) + "CANNOT LOCATE DRIVE" };
            }

            if (ifPTNVisible == "Visible")
            {
                asysSaveImagefile1 = Destinationpath;//File.ReadAllText(Connection.JewelrySDestination);
                _sql.commandStoredTraxParam3("ASYS_UpdatePhotoField");
                _sql.PhotoParam("@photoname", "@allbarcode", "@FCoated", "@PTN", "@zonecode",  Connection.JewelrySDestination + ptn + ".JPG", respCode(0), respCode(1), ptn, DB.ToUpper());
                _sql.Execute3();
                GC.Collect();

                y(photoname, txt1);

 
               
            }
            else
            {
               // asysSaveImagefile1 = File.ReadAllText(Connection.GoodstockSDestination);
                
                _sql.commandStoredTraxParam3("ASYS_UpdatePhotoField");
                _sql.PhotoParam("@photoname", "@allbarcode", "@FCoated", "@PTN", "@zonecode", Connection.GoodstockSDestination + txt1 + ".JPG", txt1, respCode(0), respCode(0), respCode(0));
                _sql.Execute3();
                GC.Collect();
                y(photoname, txt1);
               
                
                

                
                
               
                
            }
            log.Info("SavePhoto: " + respMessage(1) + " | ptn: " + ptn + " | DB: " + DB);
            _sql.commitTransax3();
            //_sql.RollBackTrax3();

            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SavePhoto: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SavePhoto: " + respMessage(0) + ex.Message };
        }
    
    }
    public WCFRespALLResult SelectPhoto(string ptn,String zone)
    {
        var _sql = new Connection();
        var models = new PhotoMembersModels();
        try
        {
            if (zone == "LNCR")
            {
                zone = "NCR";
            }
            _sql.Connection3();
            _sql.OpenConn3();

            if (!Directory.Exists(Connection.photodes))
            {
                Directory.CreateDirectory(Connection.photodes);
            }
            if (!Directory.Exists(Connection.photodes + "FakeAndCoated\\"))
            {
                Directory.CreateDirectory(Connection.photodes + "FakeAndCoated\\");
            }
            _sql.commandExeParam3("Select refallbarcode, ptn, photoname as photo from rems"+zone+".dbo.ASYS_REM_Detail where ptn = '" + ptn +
                "' AND Actionclass in ('fake','coated')");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.photo = models.ifNull(dr["Photo"]).ToString();
                models.strPhotoFileName = ptn + ".jpg";

                if (models.photo != respCode(0))
                {
                    models.strPhotoWholeFileName = models.photo;
                    models.strPhotoFolder = models.strPhotoWholeFileName.Replace(models.strPhotoFileName, "");
                    if (!File.Exists(models.strPhotoWholeFileName))
                    {


                        Image xx;
                        asysSaveImagefile1 = models.strPhotoWholeFileName;
                        String combinedPath = asysSaveImagefile1;// +barcode + @"\.JPG";
                        models.IfExist = "EXISTS";
                        using (var photoname = new Bitmap(combinedPath))
                        {
                            xx = new Bitmap(photoname);

                        }
                        models.photocontainer = x(xx);
                        models.IfExist = "EXIST";
                        log.Info("SelectPhoto: " + models.IfExist);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = "2", responseMsg = respMessage(8), PhotoMembersdata = models };
                    }
                    else
                    {
                        models.ptn = dr["ptn"].ToString();
                        Image xx;
                        asysSaveImagefile1 = models.strPhotoWholeFileName;
                        String combinedPath = asysSaveImagefile1;// +barcode + @"\.JPG";
                        models.IfExist = "EXISTS";
                        using (var photoname = new Bitmap(combinedPath))
                        {
                            xx = new Bitmap(photoname);

                        }
                        models.photocontainer = x(xx);
                    }
                }
                

                else
                {
                    if (IfFolderNotEmpty(Connection.LImageSource + "\\" + models.strPhotoFileName))
                    {
                        models.fileNameInC = Connection.LImageSource + models.strPhotoFileName;
                    }

                }
                log.Info("SelectPhoto: " + models.fileNameInC + " | " + models.ptn);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PhotoMembersdata = models };
            }
            else
            {
                log.Info("SelectPhoto: " + respMessage(7));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(7) };
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Out of Memory"))
            {
                log.Error("SelectPhoto: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectPhoto: " + respMessage(0) + respMessage(9) + ex.Message };
            }
            else
            {
                log.Error("SelectPhoto: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectPhoto: " + respMessage(0) + ex.Message };
            }
        }
    }//DONE
    protected bool IfFolderNotEmpty(string Path)
    {
        bool folderNotEmpty = false;
        try
        {
            if (!File.Exists(Path))
            {
                folderNotEmpty = false;
                return folderNotEmpty;
            }
            else
            {
                folderNotEmpty = true;
                return folderNotEmpty;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return false;
        }
    }
    public String serverpath; 
    public WCFRespALLResult GetBarcodePhoto(string barcode)
    {
        var _sql = new Connection();
    
        var models = new PhotoMembersModels();
        try
        {
            
         
            _sql.Connection3();
            _sql.OpenConn3();


            if (!Directory.Exists(Connection.photodes))
            {
                Directory.CreateDirectory(Connection.photodes);
                //Directory.Exists(Connection.photodes);
                serverpath = Connection.photodes.ToString() + barcode + ".JPG";
                //return new WCFRespALLResult { responseCode = "0", responseMsg = "WAS NOT ABLE TO FETCH DRIVE " +Connection.photodes, PhotoMembersdata = models };
            }
            else
            {
                serverpath = Connection.photodes.ToString() + barcode + ".JPG";
            }
            models.strPhotoFileName = barcode + ".jpg";

            _sql.commandExeStoredParam3("ASYS_GetBarcodePhoto");
            _sql.SummaryParams32("@allbarcode", "@IsEdit", barcode, respCode(0));
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.photo = models.ifNull(dr["photo"]).ToString();
                if (models.photo != respCode(0))
                {
                    models.strPhotoWholeFileName = models.photo;
                    models.strPhotoFolder = models.strPhotoWholeFileName.Replace(models.strPhotoFileName, "");
                    

                    if (!File.Exists(serverpath))//(models.strPhotoWholeFileName))
                    {
                        Image xx;
                        asysSaveImagefile1 = serverpath;
                        String combinedPath = asysSaveImagefile1;// +barcode + @"\.JPG";
                        models.IfExist = "EXISTS";
                        using (var photoname = new Bitmap(combinedPath))
                        {
                            xx = new Bitmap(photoname);

                        }
                        models.photocontainer = x(xx);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new WCFRespALLResult { responseCode = "2", responseMsg = "EXIST", PhotoMembersdata = models };
                    }

                }
                else
                {
                    if (IfFolderNotEmpty(Connection.LImageSource + "\\" + models.strPhotoFileName))
                    {
                        models.fileNameInC = Connection.LImageSource + models.strPhotoFileName;
                    }
                    String combinedPath = asysSaveImagefile1 + barcode + ".JPG";//@"C:\AsysCaptureImageFile1\"
             
                    Image xx;
                    using (var photoname = new Bitmap(combinedPath))
                    {
                        xx = new Bitmap(photoname);
                        
                    }
                    models.photocontainer = x(xx);
                }


                log.Info("GetBarcodePhoto: " + models.IfExist + " | " + models.fileNameInC);
                asysSaveImagefile1 = serverpath;
                String combinedPat = asysSaveImagefile1;// +barcode + ".JPG";// @"C:\AsysCaptureImageFile1\" @"\\192.168.19.142\c$\AsysCaptureImageFile1\"
                
                Image xxx;
                using (var photoname = new Bitmap(combinedPat))
                {
                    xxx = new Bitmap(photoname);
                  
                }
                models.photocontainer = x(xxx);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PhotoMembersdata = models };
            }
            else
            {
                log.Info("GetBarcodePhoto: " + respMessage(10));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(10) };
            }

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Out of Memory"))
            {
                log.Error("GetBarcodePhoto: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhoto: " + respMessage(0) + respMessage(9) + ex.Message };
            }
            else
            {
                log.Error("GetBarcodePhoto: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhoto: " + respMessage(0) + ex.Message };
            }

        }
    
    }
    public WCFRespALLResult GetBarcodePhoto2(string barcode)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeStoredParam3("ASYS_GetBarcodePhoto");
            _sql.SummaryParams32("@allbarcode", "@IsEdit", barcode, respCode(1));
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.lotno = dr["photo"].ToString();

                log.Info("GetBarcodePhoto2: " + models.lotno);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("GetBarcodePhoto2: " + respMessage(11));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(11) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetBarcodePhoto2: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhoto2: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult GetSources()
    {
        var _sql = new Connection();
        var models = new EditModels();
        try
        {
            _sql.Connection3();
            models.imageSource = Connection.LImageSource;
            models.destination = Connection.JewelrySDestination;
            models.photodes = Connection.photodes;

            log.Info("GetSources: " + models.imageSource + " | " + models.destination + " | " + models.photodes);
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), Editdata = models };
        }
        catch (Exception ex)
        {
            log.Error("GetSources: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetSources: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SortedListLoad(string DB)
    {
        var _sql = new Connection();
        var models = new habwaMonthModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.month = string.IsNullOrEmpty(dr["month"].ToString()) ? 0 : Convert.ToInt32(dr["month"]);
                models.year = string.IsNullOrEmpty(dr["year"].ToString()) ? 0 : Convert.ToInt32(dr["month"]);
                log.Info("SortedListLoad: " + models.month + " | " + models.year);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), habwaMonthdata = models };
            }
            else
            {
                log.Info("SortedListLoad: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SortedListLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SortedListLoad: " + respMessage(0) + ex.Message };
        }

    }//DONE
    public WCFRespALLResult SortBedryfByBranchCode(string DB, string bCode)
    {
        var _sql = new Connection();
        var models = new DisplayBarcodeItemsModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + bCode + "' and dateend is null");//Modify query for split
            dr = _sql.ExecuteDr();
            if (dr.Read())
            {
                models.bedrnm = dr["bedrnm"].ToString();
                log.Info("SortBedryfByBranchCode: " + models.bedrnm);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayBarcodeItemsdata = models };
            }
            else
            {
                log.Info("SortBedryfByBranchCode: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SelectBedryfByBranchCode: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SelectBedryfByBranchCode: " + respMessage(0) + ex.Message };
        }

    }//DONE
    #region SortReport
    public WCFRespALLResult SortedListReport(string DB, string userLog, string branchCode, int date, int comboyYear, string rdCheck, string zone)
    {
        var _sql = new Connection();
        var _sql2 = new Connection();
        var List = new WCFRespALLResult();
        List.ReportSortLuzondata = new List<ReportSortLuzonModels>();
        List.ReportSortSummarydata = new List<ReportSortSummaryModels>();
        try
        {
              if (DB == "LNCR")
                {
                    DB = "NCR";
                }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (rdCheck == respCode(1))
            {
                if (zone == "LUZON" || zone == "LNCR")
                {
                    _sql.commandExeStoredParam1("ASYS_REMSortedList_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pbranch", date, comboyYear, branchCode);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var s = new ReportSortLuzonModels();
                            s.mygroup = dr["mygroup"].ToString();
                            s.branchCode = dr["branchCode"].ToString().Trim();
                            s.branchname = dr["branchname"].ToString().Trim();
                            s.ptn = dr["ptn"].ToString().Trim();
                            s.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            s.branchitemdesc = dr["branchitemdesc"].ToString().Trim();
                            s.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            s.karatgrading = dr["karatgrading"].ToString().Trim();
                            s.weight = string.IsNullOrEmpty(dr["weight"].ToString()) ? 0 : Convert.ToDouble(dr["weight"]);
                            s.actionclass = dr["actionclass"].ToString().Trim();
                            s.sortcode = dr["sortcode"].ToString().Trim();
                            s.sortdesc = dr["sortdesc"].ToString().Trim();
                            s.all_karat = dr["all_karat"].ToString().Trim();
                            s.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            //s.sortdate =string.IsNullOrEmpty(dr["sortdate"].ToString())?null: Convert.ToDateTime(dr["sortdate"]).ToString("MMMM dd, yyyy").ToUpper();
                            s.sortername = dr["sortername"].ToString().Trim();
                            s.status = dr["status"].ToString().Trim();
                            List.ReportSortLuzondata.Add(s);
                        }
                        dr.Close();
                    }
                    else
                    {
                        log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////
                    //_sql.commandExeParam1("SELECT DISTINCT SUM(all_weight) AS all_wt, ptn FROM dbo.ASYS_vw_SortedList where sortdate like '%" + comboyYear + "%' GROUP BY ptn");
                    //dr2 = _sql.ExecuteDr1();
                    //if (dr2.HasRows)
                    //{
                    //    while (dr2.Read())
                    //    {
                    //        var x = new ReportSortLuzonModels();
                    //        x.all_wt = string.IsNullOrEmpty(dr2["all_wt"].ToString()) ? 0 : Convert.ToDouble(dr2["all_wt"]);
                    //        //x.ptn = dr2["ptn"].ToString();
                    //        List.ReportSortLuzondata.Add(x);
                    //    }

                    //}
                    //else
                    //{
                    //    log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                    //    _sql.CloseDr1();
                    //    _sql.CloseConn1();
                    //    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    //} dr2.Close();
                    ////////////////////////////////////////////////////////////////////////////////////////

                    _sql.BeginTransax1();
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog +
                "', sortdate = getdate(), actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status =" +
                " 'RECEIVED'; UPDATE REMS.DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog + "', sortdate = getdate()," +
                " actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status = 'RECEIVED';");
                    _sql.Execute1();
                    log.Info("SortedListReport: " + respMessage(1) + " | userLog: " + userLog + " | DB: " + DB + " | branchCode: " + branchCode + " | zone: " + zone);
                    _sql.commitTransax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportSortLuzondata = List.ReportSortLuzondata };

                }
                else if (zone == "VISAYAS" || zone == "MINDANAO")
                {
                    _sql.commandExeStoredParam1("ASYS_REMSortedList_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pbranch", date, comboyYear, branchCode);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var s = new ReportSortLuzonModels();
                            s.mygroup = dr["mygroup"].ToString();
                            s.branchCode = dr["branchCode"].ToString().Trim();
                            s.branchname = dr["branchname"].ToString().Trim();
                            s.ptn = dr["ptn"].ToString().Trim();
                            s.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            s.branchitemdesc = dr["branchitemdesc"].ToString().Trim();
                            s.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            s.karatgrading = dr["karatgrading"].ToString().Trim();
                            s.weight = string.IsNullOrEmpty(dr["weight"].ToString()) ? 0 : Convert.ToDouble(dr["weight"]);
                            s.actionclass = dr["actionclass"].ToString().Trim();
                            s.sortcode = dr["sortcode"].ToString().Trim();
                            s.sortdesc = dr["sortdesc"].ToString().Trim();
                            s.all_karat = dr["all_karat"].ToString().Trim();
                            s.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            //s.sortdate =string.IsNullOrEmpty(dr["sortdate"].ToString())?null: Convert.ToDateTime(dr["sortdate"]).ToString().ToUpper();
                            s.sortername = dr["sortername"].ToString().Trim();
                            s.status = dr["status"].ToString().Trim();
                            List.ReportSortLuzondata.Add(s);

                            
                        }
                        dr.Close();
                    }
                    else
                    {
                        log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }

                    //made changes here 
                    //_sql.commandExeParam1("SELECT DISTINCT SUM(all_weight) AS all_wt, ptn FROM dbo.ASYS_vw_SortedList where sortdate like '%" + comboyYear + "%' GROUP BY ptn");
                    //dr2 = _sql.ExecuteDr1();
                    //if (dr2.HasRows)
                    //{
                    //    while (dr2.Read())
                    //    {
                    //        var x = new ReportSortLuzonModels();
                    //        x.all_wt = string.IsNullOrEmpty(dr2["all_wt"].ToString()) ? 0 : Convert.ToDouble(dr2["all_wt"].ToString());
                    //        //x.ptn = dr2["ptn"].ToString();
                    //        List.ReportSortLuzondata.Add(x);
                    //    }
                    //}
                    //else
                    //{
                    //    log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                    //    _sql.CloseDr1();
                    //    _sql.CloseConn1();
                    //    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    //}
                    //dr2.Close();
                    _sql.BeginTransax1();
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog +
                "', sortdate = getdate(), actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status =" +
                " 'RECEIVED'; UPDATE REMS.DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog + "', sortdate = getdate()," +
                " actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status = 'RECEIVED';");
                    _sql.Execute1();
                    log.Info("SortedListReport: " + respMessage(1) + " | userLog: " + userLog + " | DB: " + DB + " | branchCode: " + branchCode + " | zone: " + zone);
                    _sql.commitTransax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportSortLuzondata = List.ReportSortLuzondata };
                }
                else
                {
                    _sql.commandExeStoredParam1("ASYS_REMSortedList_rpt");
                    _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pbranch", date, comboyYear, branchCode);
                    dr = _sql.ExecuteDr1();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var s = new ReportSortLuzonModels();
                            s.mygroup = dr["mygroup"].ToString();
                            s.branchCode = dr["branchCode"].ToString().Trim();
                            s.branchname = dr["branchname"].ToString().Trim();
                            s.ptn = dr["ptn"].ToString().Trim();
                            s.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                            s.branchitemdesc = dr["branchitemdesc"].ToString().Trim();
                            s.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                            s.karatgrading = dr["karatgrading"].ToString().Trim();
                            s.weight = string.IsNullOrEmpty(dr["weight"].ToString()) ? 0 : Convert.ToDouble(dr["weight"]);
                            s.actionclass = dr["actionclass"].ToString().Trim();
                            s.sortcode = dr["sortcode"].ToString().Trim();
                            s.sortdesc = dr["sortdesc"].ToString().Trim();
                            s.all_karat = dr["all_karat"].ToString().Trim();
                            s.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                            s.transdate = string.IsNullOrEmpty(dr["transdate"].ToString()) ? null : Convert.ToDateTime(dr["transdate"]).ToString("yyyy MMMM").ToUpper();
                            s.sortername = dr["sortername"].ToString().Trim();
                            s.status = dr["status"].ToString().Trim();
                            List.ReportSortLuzondata.Add(s);
                        }
                        dr.Close();
                    }
                    else
                    {
                        log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    _sql.commandExeParam1("SELECT DISTINCT SUM(all_weight) AS all_wt, ptn FROM dbo.ASYS_vw_SortedList where sortdate like '%" + comboyYear + "%' GROUP BY ptn");
                    dr2 = _sql.ExecuteDr1();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            var x = new ReportSortLuzonModels();
                            x.all_wt = string.IsNullOrEmpty(dr2["all_wt"].ToString()) ? 0 : Convert.ToDouble(dr2["all_wt"].ToString());
                            //x.ptn = dr2["ptn"].ToString();
                           // List.ReportSortLuzondata.Add(x); //-->HERE 
                        }
                    }
                    else
                    {
                        log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                    dr2.Close();
                    _sql.BeginTransax1();
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog +
                "', sortdate = getdate(), actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status =" +
                " 'RECEIVED'; UPDATE REMS.DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog + "', sortdate = getdate()," +
                " actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status = 'RECEIVED';");
                    _sql.Execute1();
                    log.Info("SortedListReport: " + respMessage(1) + " | userLog: " + userLog + " | DB: " + DB + " | branchCode: " + branchCode + " | zone: " + zone);
                    _sql.commitTransax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportSortLuzondata = List.ReportSortLuzondata };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("ASYS_REMSortedSummary_rpt");
                _sql.SummaryParams1("@Pmonth", "@Pyear", "@Pbranch", date, comboyYear, branchCode);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var b = new ReportSortSummaryModels();
                        b.mygroup = dr["mygroup"].ToString();
                        b.actionclass = dr["actionclass"].ToString();
                        b.ptnprincipal = string.IsNullOrEmpty(dr["ptnprincipal"].ToString()) ? 0 : Convert.ToDouble(dr["ptnprincipal"]);
                        b.wt = string.IsNullOrEmpty(dr["wt"].ToString()) ? 0 : Convert.ToDouble(dr["wt"]);
                        b.qty = string.IsNullOrEmpty(dr["qty"].ToString()) ? 0 : Convert.ToInt32(dr["qty"]);
                        b.packs = string.IsNullOrEmpty(dr["packs"].ToString()) ? 0 : Convert.ToInt32(dr["packs"]);
                        List.ReportSortSummarydata.Add(b);
                    } dr.Close();
                    _sql.BeginTransax1();
                    _sql.commandTraxParam1("UPDATE REMS" + DB + ".DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog +
                "', sortdate = getdate(), actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status =" +
                " 'RECEIVED'; UPDATE REMS.DBO.ASYS_REM_detail SET status = 'SORTED', sortername = '" + userLog + "', sortdate = getdate()," +
                " actionclass = 'MissingItem', sortcode = 'O' WHERE Substring(ptn,1,3) = '" + branchCode +
                "' AND month(receivedate) = '" + date + "' AND year(receivedate) = '" + comboyYear + "' AND status = 'RECEIVED';");
                    _sql.Execute1();
                    log.Info("SortedListReport: " + respMessage(1) + " | userLog: " + userLog + " | DB: " + DB + " | branchCode: " + branchCode + " | zone: " + zone);
                    _sql.commitTransax1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportSortSummarydata = List.ReportSortSummarydata };
                }
                else
                {
                    log.Info("SortedListReport: zone: " + zone + " | " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("SortedListReport: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SortedListReport: " + respMessage(0) + ex.Message };
        }
    }
    #endregion
    //---------------------------February 15,2017----------------steph--------------//
    //---------------------------February 16,2017----------------steph--------------//
    public WCFRespALLResult MemoLoad(string jobTitle)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            _sql.Connection0();
            _sql.OpenConn();
            if (jobTitle == "SORTER")
            {
                _sql.commandExeParam("SELECT DISTINCT UPPER(fullname) AS FULLNAME, usr_id, blocked FROM " + Connection.humres2 +
                    " WHERE (job_title='SORTER' OR job_title='ALLDEPTMNGR') AND blocked=1 ORDER BY FULLNAME");
            }
            else
            {
                _sql.commandExeParam("SELECT DISTINCT UPPER(fullname) AS FULLNAME, usr_id, blocked FROM " + Connection.humres2 +
                    " WHERE job_title='ALLDEPTMNGR' AND blocked=1 ORDER BY FULLNAME");
            }
            dr = _sql.ExecuteDr();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.costcentersdata.costDept.Add(dr["fullname"].ToString().Trim().ToUpper());
                }
                log.Info("MemoLoad: " + List.costcentersdata.costDept);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("MemoLoad: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("MemoLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "MemoLoad: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult MemoLoad2(string DB)
    {
        var _sql = new Connection();
        var models = new habwaMonthModels();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeParam1("select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year");
            dr = _sql.ExecuteDr1();
            if (dr.Read())
            {
                models.month = Convert.ToInt32(dr["month"].ToString());
                models.year = Convert.ToInt32(dr["year"].ToString());

                log.Info("MemoLoad2: " + models.month + " | " + models.year);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), habwaMonthdata = models };
            }
            else
            {
                log.Info("MemoLoad2: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("MemoLoad2: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "MemoLoad2: " + respMessage(0) + ex.Message };
        }

    }
    #region REPORTS
    public WCFRespALLResult MultipleMemoRPTLoad(string DB, int month, string year, string name, string whichMemo)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.photonames = new List<byte[]>();
        List.barcode = new List<string>();
        List.MemoMissingItemsdata = new List<MemoMissingItemsModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichMemo == "2")
            {
                _sql.commandExeStoredParam1("ASYS_MemoMissingItem");
                _sql.SummaryParams12("@Pmonth", "@Pyear", "@pname", month, year, name);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new MemoMissingItemsModels();
                        x.BranchCode = dr["BranchCode"].ToString();
                        x.Branchname = dr["Branchname"].ToString();
                        x.Region = dr["Region"].ToString();
                        x.Area = dr["Area"].ToString();
                        x.Class_01 = dr["Class_01"].ToString();
                        x.PTN = dr["PTN"].ToString();
                        x.LoanValue = string.IsNullOrEmpty(dr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(dr["LoanValue"]);
                        x.BranchItemDesc = dr["BranchItemDesc"].ToString();
                        x.Qty = string.IsNullOrEmpty(dr["Qty"].ToString()) ? 0 : Convert.ToInt32(dr["Qty"]);
                        x.KaratGrading = dr["KaratGrading"].ToString();
                        x.CaratSize = string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? 0 : Convert.ToDouble(dr["CaratSize"]);
                        x.Weight = string.IsNullOrEmpty(dr["Weight"].ToString()) ? 0 : Convert.ToDouble(dr["Weight"]);
                        x.reflotno = dr["reflotno"].ToString();
                        x.sortername = dr["sortername"].ToString();
                        x.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();//string.IsNullOrEmpty(dr["month"].ToString()) ? null : Convert.ToDateTime(dr["month"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim();// string.IsNullOrEmpty(dr["year"].ToString()) ? null : Convert.ToDateTime(dr["year"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.manager_id = dr["manager_id"].ToString();
                        x.ALL_Desc = dr["ALL_Desc"].ToString();
                        x.ALL_Karat = dr["ALL_Karat"].ToString();
                        x.ALL_Carat = string.IsNullOrEmpty(dr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Carat"]);
                        x.ALL_Weight = string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Weight"]);
                        x.ALL_Cost = string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Cost"]);
                        List.MemoMissingItemsdata.Add(x);
                    }
                    _sql.CloseDr1();
                }
                else
                {
                    log.Info("MultipleMemoRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else if (whichMemo == "3")
            {
                _sql.commandExeStoredParam1("ASYS_MemoFakeItem");
                _sql.SummaryParams12("@Pmonth", "@Pyear", "@pname", month, year, name);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new MemoMissingItemsModels();
                        x.BranchCode = dr["BranchCode"].ToString();
                        x.Branchname = dr["Branchname"].ToString();
                        x.Region = dr["Region"].ToString();
                        x.Area = dr["Area"].ToString();
                        x.Class_01 = dr["Class_01"].ToString();
                        x.PTN = dr["PTN"].ToString();
                        x.LoanValue = string.IsNullOrEmpty(dr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(dr["LoanValue"]);
                        x.BranchItemDesc = dr["BranchItemDesc"].ToString();
                        x.Qty = string.IsNullOrEmpty(dr["Qty"].ToString()) ? 0 : Convert.ToInt32(dr["Qty"]);
                        x.KaratGrading = dr["KaratGrading"].ToString();
                        x.CaratSize = string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? 0 : Convert.ToDouble(dr["CaratSize"]);
                        x.Weight = string.IsNullOrEmpty(dr["Weight"].ToString()) ? 0 : Convert.ToDouble(dr["Weight"]);
                        x.reflotno = dr["reflotno"].ToString();
                        x.sortername = dr["sortername"].ToString();
                        x.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();//string.IsNullOrEmpty(dr["month"].ToString()) ? null : Convert.ToDateTime(dr["month"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim();//string.IsNullOrEmpty(dr["year"].ToString()) ? null : Convert.ToDateTime(dr["year"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.manager_id = dr["manager_id"].ToString();
                        x.ALL_Desc = dr["ALL_Desc"].ToString();
                        x.ALL_Karat = dr["ALL_Karat"].ToString();
                        x.ALL_Carat = string.IsNullOrEmpty(dr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Carat"]);
                        x.ALL_Weight = string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Weight"]);
                        x.ALL_Cost = string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Cost"]);
                        x.PhotoName = dr["PhotoName"].ToString();
                        Image xxx;
                        x.PhotoName = dr["PhotoName"].ToString().Trim();
                        if (x.PhotoName != "")
                        {
                            using (var photoname = new Bitmap(x.PhotoName))
                            {
                                xxx = new Bitmap(photoname);
                            }
                            List.photocontainer = photoconverter(xxx);
                            x.PhotoName = @"C:\remsyncphoto\" + x.PTN + ".JPG";
                        }
                        else
                        {
                            x.PhotoName = @"C:\remsyncphoto\" + x.PTN + ".JPG";
                            List.photocontainer = null;
                        }
                        List.photonames.Add(List.photocontainer);
                        List.barcode.Add(x.PhotoName);
                        List.MemoMissingItemsdata.Add(x);
                    }
                    _sql.CloseDr1();
                }
                else
                {
                    log.Info("MultipleMemoRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else if (whichMemo == "4")
            {
                _sql.commandExeStoredParam1("ASYS_MemoCoatedItem");
                _sql.SummaryParams12("@Pmonth", "@Pyear", "@pname", month, year, name);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new MemoMissingItemsModels();
                        x.BranchCode = dr["BranchCode"].ToString();
                        x.Branchname = dr["Branchname"].ToString();
                        x.Region = dr["Region"].ToString();
                        x.Area = dr["Area"].ToString();
                        x.Class_01 = dr["Class_01"].ToString();
                        x.PTN = dr["PTN"].ToString();
                        x.LoanValue = string.IsNullOrEmpty(dr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(dr["LoanValue"]);
                        x.BranchItemDesc = dr["BranchItemDesc"].ToString();
                        x.Qty = string.IsNullOrEmpty(dr["Qty"].ToString()) ? 0 : Convert.ToInt32(dr["Qty"]);
                        x.KaratGrading = dr["KaratGrading"].ToString();
                        x.CaratSize = string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? 0 : Convert.ToDouble(dr["CaratSize"]);
                        x.Weight = string.IsNullOrEmpty(dr["Weight"].ToString()) ? 0 : Convert.ToDouble(dr["Weight"]);
                        x.reflotno = dr["reflotno"].ToString();
                        x.sortername = dr["sortername"].ToString();
                        x.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();//string.IsNullOrEmpty(dr["month"].ToString()) ? null : Convert.ToDateTime(dr["month"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim();//string.IsNullOrEmpty(dr["year"].ToString()) ? null : Convert.ToDateTime(dr["year"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.manager_id = dr["manager_id"].ToString();
                        x.ALL_Desc = dr["ALL_Desc"].ToString();
                        x.ALL_Karat = dr["ALL_Karat"].ToString();
                        x.ALL_Carat = string.IsNullOrEmpty(dr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Carat"]);
                        x.ALL_Weight = string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Weight"]);
                        x.ALL_Cost = string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Cost"]);
                        //x.PhotoName = dr["PhotoName"].ToString();

                        Image xxx;
                        x.PhotoName = dr["PhotoName"].ToString().Trim();
                        if (x.PhotoName != "")
                        {
                            using (var photoname = new Bitmap(x.PhotoName))
                            {
                                xxx = new Bitmap(photoname);
                            }
                            List.photocontainer = photoconverter(xxx);
                            x.PhotoName = @"C:\remsyncphoto\" + x.PTN + ".JPG";
                        }
                        else
                        {
                            x.PhotoName = @"C:\remsyncphoto\" + x.PTN + ".JPG";
                            List.photocontainer = null;
                        }
                        List.photonames.Add(List.photocontainer);
                        List.barcode.Add(x.PhotoName);
                        List.MemoMissingItemsdata.Add(x);
                    }
                    _sql.CloseDr1();
                }
                else
                {
                    log.Info("MultipleMemoRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("ASYS_MemoOverAppItem");
                _sql.SummaryParams12("@Pmonth", "@Pyear", "@pname", month, year, name);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new MemoMissingItemsModels();
                        x.BranchCode = dr["BranchCode"].ToString();
                        x.Branchname = dr["Branchname"].ToString();
                        x.Region = dr["Region"].ToString();
                        x.Area = dr["Area"].ToString();
                        x.Class_01 = dr["Class_01"].ToString();
                        x.PTN = dr["PTN"].ToString();
                        x.LoanValue = string.IsNullOrEmpty(dr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(dr["LoanValue"]);
                        x.BranchItemDesc = dr["BranchItemDesc"].ToString();
                        x.Qty = string.IsNullOrEmpty(dr["Qty"].ToString()) ? 0 : Convert.ToInt32(dr["Qty"]);
                        x.KaratGrading = dr["KaratGrading"].ToString();
                        x.CaratSize = string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? 0 : Convert.ToDouble(dr["CaratSize"]);
                        x.Weight = string.IsNullOrEmpty(dr["Weight"].ToString()) ? 0 : Convert.ToDouble(dr["Weight"]);
                        x.reflotno = dr["reflotno"].ToString();
                        x.sortername = dr["sortername"].ToString();
                        x.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();//string.IsNullOrEmpty(dr["month"].ToString()) ? null : Convert.ToDateTime(dr["month"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim(); //string.IsNullOrEmpty(dr["year"].ToString()) ? null : Convert.ToDateTime(dr["year"]).ToString("MMMM dd, yyyy").ToUpper();
                        x.manager_id = dr["manager_id"].ToString();
                        x.ALL_Desc = dr["ALL_Desc"].ToString();
                        x.ALL_Karat = dr["ALL_Karat"].ToString();
                        x.ALL_Carat = string.IsNullOrEmpty(dr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Carat"]);
                        x.ALL_Weight = string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Weight"]);
                        x.ALL_Cost = string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_Cost"]);
                        List.MemoMissingItemsdata.Add(x);
                    }
                    _sql.CloseDr1();
                }
                else
                {
                    log.Info("MultipleMemoRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            _sql.CloseConn1();
            
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), MemoMissingItemsdata = List.MemoMissingItemsdata ,
            photonames = List.photonames, barcode = List.barcode};

        }
        catch (Exception ex)
        {
            log.Error("MultipleMemoRPTLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "MultipleMemoRPTLoad: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult MultipleProcessRPTLoad(string DB, string process, int month, int year)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.ProcessQTYdata = new List<ProcessQTYModels>();
        List.ProcessTWTdata = new List<ProcessTWTModels>();
        List.ProcessLoanAmoutdata = new List<ProcessLoanAmoutModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (process == respCode(1))
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessqtypcsv31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessQTYModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToInt32(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToInt32(dr["watch"]);
                        x.cellular = string.IsNullOrEmpty(dr["cellular"].ToString()) ? 0 : Convert.ToInt32(dr["cellular"]);
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToInt32(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToInt32(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToInt32(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToInt32(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToInt32(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessQTYdata.Add(x);
                    }
                    log.Info("MultipleProcessRPTLoad: " + List.ProcessQTYdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessQTYdata = List.ProcessQTYdata };
                }
                else
                {
                    log.Info("MultipleProcessRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else if (process == "2")
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessTWTV31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessTWTModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToInt32(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToInt32(dr["watch"]);
                        //x.cellular = Convert.ToInt32(dr["cellular"]).ToString();
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToInt32(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToInt32(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToInt32(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToInt32(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToInt32(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessTWTdata.Add(x);
                    }
                    log.Info("MultipleProcessRPTLoad: " + List.ProcessTWTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessTWTdata = List.ProcessTWTdata };
                }
                else
                {
                    log.Info("MultipleProcessRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessLoanAmountV31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessLoanAmoutModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToDouble(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToDouble(dr["watch"]);
                        x.cellular = string.IsNullOrEmpty(dr["cellular"].ToString()) ? 0 : Convert.ToDouble(dr["cellular"]);
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToDouble(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToDouble(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToDouble(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToDouble(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToDouble(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessLoanAmoutdata.Add(x);
                    }
                    log.Info("MultipleProcessRPTLoad: " + List.ProcessLoanAmoutdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessLoanAmoutdata = List.ProcessLoanAmoutdata };
                }
                else
                {
                    log.Info("MultipleProcessRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("MultipleProcessRPTLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "MultipleProcessRPTLoad: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult JewelrySummaryRPTLoad(string DB, int month, int year)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.JewelrySummarydata = new List<JewelrySummaryModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            _sql.commandExeStoredParam1("ASYS_REMJewelrySummaryV31");
            _sql.SummaryParams356("@prenda_month", "@prenda_year", month, year);
            dr = _sql.ExecuteDr1();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var k = new JewelrySummaryModels();
                    k.branchcode = dr["branchcode"].ToString();
                    k.PrendaAmount = string.IsNullOrEmpty(dr["PrendaAmount"].ToString()) ? 0 : Convert.ToDouble(dr["PrendaAmount"]);
                    k.REMAmount = string.IsNullOrEmpty(dr["REMAmount"].ToString()) ? 0 : Convert.ToDouble(dr["REMAmount"]);
                    k.branchname = dr["branchname"].ToString();
                    k.region = dr["region"].ToString();
                    k.area = dr["area"].ToString();
                    List.JewelrySummarydata.Add(k);
                }
                log.Info("JewelrySummaryRPTLoad: " + List.JewelrySummarydata);
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), JewelrySummarydata = List.JewelrySummarydata };
            }
            else
            {
                log.Info("JewelrySummaryRPTLoad: " + respMessage(2));
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }

        }
        catch (Exception ex)
        {
            log.Error("JewelrySummaryRPTLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "JewelrySummaryRPTLoad: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult ProcessSummaryRPTLoad(string DB, int month, int year, string process)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.ProcessQTYdata = new List<ProcessQTYModels>();
        List.ProcessTWTdata = new List<ProcessTWTModels>();
        List.ProcessLoanAmoutdata = new List<ProcessLoanAmoutModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (process == respCode(1))
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessQtyPcsV31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessQTYModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToInt32(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToInt32(dr["watch"]);
                        x.cellular = string.IsNullOrEmpty(dr["cellular"].ToString()) ? 0 : Convert.ToInt32(dr["cellular"]);
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToInt32(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToInt32(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToInt32(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToInt32(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToInt32(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessQTYdata.Add(x);
                    }
                    log.Info("ProcessSummaryRPTLoad: " + List.ProcessQTYdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessQTYdata = List.ProcessQTYdata };
                }
                else
                {
                    log.Info("ProcessSummaryRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else if (process == "2")
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessTWTV31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessTWTModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToInt32(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToInt32(dr["watch"]);
                        //x.cellular = Convert.ToInt32(dr["cellular"]).ToString();
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToInt32(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToInt32(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToInt32(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToInt32(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToInt32(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessTWTdata.Add(x);
                    }
                    log.Info("ProcessSummaryRPTLoad: " + List.ProcessTWTdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessTWTdata = List.ProcessTWTdata };
                }
                else
                {
                    log.Info("ProcessSummaryRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("ASYS_REMProcessLoanAmountV31");
                _sql.SummaryParams356("@month", "@year", month, year);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var x = new ProcessLoanAmoutModels();
                        x.branchcode = dr["branchcode"].ToString();
                        x.branchname = dr["branchname"].ToString();
                        x.region = dr["region"].ToString();
                        x.area = dr["area"].ToString();
                        x.mlstocks = string.IsNullOrEmpty(dr["mlstocks"].ToString()) ? 0 : Convert.ToDouble(dr["mlstocks"]);
                        x.watch = string.IsNullOrEmpty(dr["watch"].ToString()) ? 0 : Convert.ToDouble(dr["watch"]);
                        x.cellular = string.IsNullOrEmpty(dr["cellular"].ToString()) ? 0 : Convert.ToDouble(dr["cellular"]);
                        x.goodstock = string.IsNullOrEmpty(dr["goodstock"].ToString()) ? 0 : Convert.ToDouble(dr["goodstock"]);
                        x.missing = string.IsNullOrEmpty(dr["missing"].ToString()) ? 0 : Convert.ToDouble(dr["missing"]);
                        x.overapp = string.IsNullOrEmpty(dr["overapp"].ToString()) ? 0 : Convert.ToDouble(dr["overapp"]);
                        x.fake = string.IsNullOrEmpty(dr["fake"].ToString()) ? 0 : Convert.ToDouble(dr["fake"]);
                        x.coated = string.IsNullOrEmpty(dr["coated"].ToString()) ? 0 : Convert.ToDouble(dr["coated"]);
                        x.ninek = dr["ninek"].ToString();
                        x.twelvek = dr["twelvek"].ToString();
                        x.twentyk = dr["twentyk"].ToString();
                        x.sixteenk = dr["sixteenk"].ToString();
                        x.threek = dr["threek"].ToString();
                        List.ProcessLoanAmoutdata.Add(x);
                    }
                    log.Info("ProcessSummaryRPTLoad: " + List.ProcessLoanAmoutdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ProcessLoanAmoutdata = List.ProcessLoanAmoutdata };
                }
                else
                {
                    log.Info("ProcessSummaryRPTLoad: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("ProcessSummaryRPTLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "ProcessSummaryRPTLoad: " + respMessage(0) + ex.Message };
        }
    }
    //---------------------------February 16,2017----------------steph--------------//
    //---------------------------February 17,2017----------------steph--------------//
    public WCFRespALLResult ComparativeReports(string DB, DateTime habwa, string whichComparative)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.ComparativeOverAppdata = new List<ComparativeOverAppModels>();
        List.ComparativeFakeCoateddata = new List<ComparativeFakeCoatedModels>();
        List.ComparativeCellWatchdata = new List<ComparativeCellWatchModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichComparative == "2")
            {
                _sql.commandExeStoredParam1("ASYS_ComparativeOverAppraisedv31");
                _sql.REportsComparative("@habwamonth", habwa);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var j = new ComparativeOverAppModels();
                        j.branches = dr["branches"].ToString();
                        j.region = dr["region"].ToString();
                        j.prevalue = string.IsNullOrEmpty(dr["prevalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevalue"]);
                        j.curvalue = string.IsNullOrEmpty(dr["curvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curvalue"]);
                        j.prevprincipal = string.IsNullOrEmpty(dr["prevprincipal"].ToString()) ? 0 : Convert.ToDouble(dr["prevprincipal"]);
                        j.curprincipal = string.IsNullOrEmpty(dr["curprincipal"].ToString()) ? 0 : Convert.ToDouble(dr["curprincipal"]);
                        j.prevallcost = string.IsNullOrEmpty(dr["prevallcost"].ToString()) ? 0 : Convert.ToDouble(dr["prevallcost"]);
                        j.curallcost = string.IsNullOrEmpty(dr["curallcost"].ToString()) ? 0 : Convert.ToDouble(dr["curallcost"]);
                        j.prevqty = string.IsNullOrEmpty(dr["prevqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevqty"]);
                        j.curqty = string.IsNullOrEmpty(dr["curqty"].ToString()) ? 0 : Convert.ToInt32(dr["curqty"]);
                        List.ComparativeOverAppdata.Add(j);
                    }
                    log.Info("ComparativeReports: " + List.ComparativeOverAppdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ComparativeOverAppdata = List.ComparativeOverAppdata };
                }
                else
                {
                    log.Info("ComparativeReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            else if (whichComparative == "3")
            {
                _sql.commandExeStoredParam1("ASYS_ComparativeFakeCoatedV31");
                _sql.REportsComparative("@habwamonth", habwa);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var g = new ComparativeFakeCoatedModels();
                        g.region = dr["region"].ToString();
                        g.prevfakevalue = string.IsNullOrEmpty(dr["prevfakevalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevfakevalue"]);
                        g.curfakevalue = string.IsNullOrEmpty(dr["curfakevalue"].ToString()) ? 0 : Convert.ToDouble(dr["curfakevalue"]);
                        g.prevcoatedvalue = string.IsNullOrEmpty(dr["prevcoatedvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevcoatedvalue"]);
                        g.curcoatedvalue = string.IsNullOrEmpty(dr["curcoatedvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curcoatedvalue"]);
                        g.month = string.IsNullOrEmpty(dr["month"].ToString()) ? null : Convert.ToDateTime(dr["month"]).ToString("MMMM");
                        g.year = string.IsNullOrEmpty(dr["year"].ToString()) ? null : Convert.ToDateTime(dr["year"]).ToString("yyyy");
                        List.ComparativeFakeCoateddata.Add(g);
                    }
                    log.Info("ComparativeReports: " + List.ComparativeFakeCoateddata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ComparativeFakeCoateddata = List.ComparativeFakeCoateddata };
                }
                else
                {
                    log.Info("ComparativeReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("ASYS_ComparativeCellWatchV31");
                _sql.REportsComparative("@habwamonth", habwa);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var f = new ComparativeCellWatchModels();
                        f.region = dr["region"].ToString();
                        f.curcellvalue = string.IsNullOrEmpty(dr["curcellvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curcellvalue"]);
                        f.curwatchvalue = string.IsNullOrEmpty(dr["curwatchvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curwatchvalue"]);
                        f.prevcellvalue = string.IsNullOrEmpty(dr["prevcellvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevcellvalue"]);
                        f.prevwatchvalue = string.IsNullOrEmpty(dr["prevwatchvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevwatchvalue"]);
                        List.ComparativeCellWatchdata.Add(f);
                    }
                    log.Info("ComparativeReports: " + List.ComparativeCellWatchdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ComparativeCellWatchdata = List.ComparativeCellWatchdata };
                }
                else
                {
                    log.Info("ComparativeReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("ComparativeReports: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "ComparativeReports: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult NewRematadoReports(string DB, string whichReport, DateTime habwa0, DateTime habwa1, DateTime habwa3, DateTime prevmonth3,
        DateTime habwa5, DateTime prevmonth5)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.TOP10Branchesdata = new List<TOP10BranchesModels>();
        List.ComparativeReportsdata = new List<ComparativeReportsModels>();
        List.LVMComparativeReportsdata = new List<LVMComparativeReportsModels>();
        try
        {
            _sql.Connection1(DB);
            _sql.OpenConn1();
            if (whichReport == respCode(0))
            {
                _sql.commandExeStoredParam1("ASYS_TOP10BranchesV31");
                _sql.REportsComparative("@habwaMonth", habwa0);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var i = new TOP10BranchesModels();
                        i.topreport = dr["topreport"].ToString();
                        i.area = dr["area"].ToString();
                        i.region = dr["region"].ToString();
                        i.Branchname = dr["Branchname"].ToString();
                        i.prendaamount = string.IsNullOrEmpty(dr["prendaamount"].ToString()) ? 0 : Convert.ToDouble(dr["prendaamount"]);
                        i.remamount = string.IsNullOrEmpty(dr["remamount"].ToString()) ? 0 : Convert.ToDouble(dr["remamount"]);
                        i.PrendaMonth = string.IsNullOrEmpty(dr["PrendaMonth"].ToString()) ? "NULL" : dr["PrendaMonth"].ToString().Trim();//string.IsNullOrEmpty(dr["PrendaMonth"].ToString()) ? null : Convert.ToDateTime(dr["PrendaMonth"]).ToString("yyyy MMMM");
                        i.HabwaMonth = string.IsNullOrEmpty(dr["HabwaMonth"].ToString()) ? null : Convert.ToDateTime(dr["HabwaMonth"]).ToString("yyyy MMMM");
                        i.percentage = string.IsNullOrEmpty(dr["percentage"].ToString()) ? 0 : Convert.ToDouble(dr["percentage"]);
                        List.TOP10Branchesdata.Add(i);

                    }
                    log.Info("NewRematadoReports: " + List.TOP10Branchesdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TOP10Branchesdata = List.TOP10Branchesdata };
                }
                else
                {
                    log.Info("NewRematadoReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            else if (whichReport == respCode(1))
            {
                _sql.commandExeStoredParam1("REMS.dbo.ASYS_TOP10BranchesLuzonVisminV31");
                _sql.REportsComparative("@habwaMonth", habwa1);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var i = new TOP10BranchesModels();
                        i.topten = dr["topten"].ToString();
                        i.area = dr["area"].ToString();
                        i.region = dr["region"].ToString();
                        i.Branchname = dr["Branchname"].ToString();
                        i.prendaamount = string.IsNullOrEmpty(dr["prendaamount"].ToString()) ? 0 : Convert.ToDouble(dr["prendaamount"]);
                        i.remamount = string.IsNullOrEmpty(dr["remamount"].ToString()) ? 0 : Convert.ToDouble(dr["remamount"]);
                        i.PrendaMonth = string.IsNullOrEmpty(dr["PrendaMonth"].ToString()) ? null : Convert.ToDateTime(dr["PrendaMonth"]).ToString("yyyy MMMM");
                        i.HabwaMonth = string.IsNullOrEmpty(dr["HabwaMonth"].ToString()) ? null : Convert.ToDateTime(dr["HabwaMonth"]).ToString("yyyy MMMM");
                        i.percentage = string.IsNullOrEmpty(dr["percentage"].ToString()) ? 0 : Convert.ToDouble(dr["percentage"]);
                        List.TOP10Branchesdata.Add(i);
                    }
                    log.Info("NewRematadoReports: " + List.TOP10Branchesdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TOP10Branchesdata = List.TOP10Branchesdata };
                }
                else
                {
                    log.Info("NewRematadoReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else if (whichReport == "2")
            {
                log.Info("NewRematadoReports: " + "REM VISMIN is not Available.");
                _sql.CloseDr1();
                _sql.CloseConn1();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "REM VISMIN is not Available." };
            }
            else if (whichReport == "34")
            {
                _sql.commandExeStoredParam1("ASYS_ComparativeV31");
                _sql.REportsComparative2("@habwaMonth", "@prevmonth", habwa3, prevmonth3);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var p = new ComparativeReportsModels();
                        p.Cur_JAmnt = string.IsNullOrEmpty(dr["Cur_JAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_JAmnt"]);
                        p.Cur_WAmnt = string.IsNullOrEmpty(dr["Cur_WAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_WAmnt"]);
                        p.Cur_CAmnt = string.IsNullOrEmpty(dr["Cur_CAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_CAmnt"]);
                        p.Cur_CoAmnt = string.IsNullOrEmpty(dr["Cur_CoAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_CoAmnt"]);
                        p.Cur_FAmnt = string.IsNullOrEmpty(dr["Cur_FAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_FAmnt"]);
                        p.Cur_OvAmnt = string.IsNullOrEmpty(dr["Cur_OvAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_OvAmnt"]);
                        p.Cur_PAmnt = string.IsNullOrEmpty(dr["Cur_PAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_PAmnt"]);
                        p.Cur_GSAmnt = string.IsNullOrEmpty(dr["Cur_GSAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_GSAmnt"]);
                        p.Cur_MisAmnt = string.IsNullOrEmpty(dr["Cur_MisAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_MisAmnt"]);

                        p.Prev_JAmnt = string.IsNullOrEmpty(dr["Prev_JAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_JAmnt"]);
                        p.Prev_WAmnt = string.IsNullOrEmpty(dr["Prev_WAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_WAmnt"]);
                        p.Prev_CAmnt = string.IsNullOrEmpty(dr["Prev_CAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_CAmnt"]);
                        p.Prev_CoAmnt = string.IsNullOrEmpty(dr["Prev_CoAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_CoAmnt"]);
                        p.Prev_FAmnt = string.IsNullOrEmpty(dr["Prev_FAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_FAmnt"]);
                        p.Prev_OvAmnt = string.IsNullOrEmpty(dr["Prev_OvAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_OvAmnt"]);
                        p.Prev_PAmnt = string.IsNullOrEmpty(dr["Prev_PAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_PAmnt"]);
                        p.Prev_GSAmnt = string.IsNullOrEmpty(dr["Prev_GSAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_GSAmnt"]);
                        p.Prev_MisAmnt = string.IsNullOrEmpty(dr["Prev_MisAmnt"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_MisAmnt"]);

                        p.Cur_JQty = string.IsNullOrEmpty(dr["Cur_JQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_JQty"]);
                        p.Cur_WQty = string.IsNullOrEmpty(dr["Cur_WQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_WQty"]);
                        p.Cur_CQty = string.IsNullOrEmpty(dr["Cur_CQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_CQty"]);
                        p.Cur_CoQty = string.IsNullOrEmpty(dr["Cur_CoQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_CoQty"]);
                        p.Cur_FQty = string.IsNullOrEmpty(dr["Cur_FQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_FQty"]);
                        p.Cur_OvQty = string.IsNullOrEmpty(dr["Cur_OvQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_OvQty"]);
                        p.Cur_GSQty = string.IsNullOrEmpty(dr["Cur_GSQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_GSQty"]);
                        p.Cur_MisQty = string.IsNullOrEmpty(dr["Cur_MisQty"].ToString()) ? 0 : Convert.ToInt32(dr["Cur_MisQty"]);

                        p.Prev_JQty = string.IsNullOrEmpty(dr["Prev_JQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_JQty"]);
                        p.Prev_WQty = string.IsNullOrEmpty(dr["Prev_WQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_WQty"]);
                        p.Prev_CQty = string.IsNullOrEmpty(dr["Prev_CQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_CQty"]);
                        p.Prev_CoQty = string.IsNullOrEmpty(dr["Prev_CoQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_CoQty"]);
                        p.Prev_FQty = string.IsNullOrEmpty(dr["Prev_FQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_FQty"]);
                        p.Prev_OvQty = string.IsNullOrEmpty(dr["Prev_OvQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_OvQty"]);
                        p.Prev_GSQty = string.IsNullOrEmpty(dr["Prev_GSQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_GSQty"]);
                        p.Prev_MisQty = string.IsNullOrEmpty(dr["Prev_MisQty"].ToString()) ? 0 : Convert.ToInt32(dr["Prev_MisQty"]);

                        p.Cur_Ninek = dr["Cur_Ninek"].ToString();
                        p.Prev_Ninek = dr["Prev_Ninek"].ToString();
                        p.Cur_Twelvek = dr["Cur_Twelvek"].ToString();
                        p.Prev_Twelvek = dr["Prev_Twelvek"].ToString();
                        p.Cur_Sixteenk = dr["Cur_Sixteenk"].ToString();
                        p.Prev_Sixteenk = dr["Prev_Sixteenk"].ToString();
                        p.Cur_20K = dr["Cur_20K"].ToString();
                        p.Prev_20K = dr["Prev_20K"].ToString();
                        p.Cur_Threek = dr["Cur_Threek"].ToString();
                        p.Prev_Threek = dr["Prev_Threek"].ToString();
                        p.Cur_goodStock = dr["Cur_goodStock"].ToString();
                        p.Prev_goodStock = dr["Prev_goodStock"].ToString();
                        p.Cur_Fake = dr["Cur_Fake"].ToString();
                        p.Prev_Fake = dr["Prev_Fake"].ToString();
                        p.Cur_Coated = dr["Cur_Coated"].ToString();
                        p.Prev_Coated = dr["Prev_Coated"].ToString();
                        p.Cur_OverAppraised = dr["Cur_OverAppraised"].ToString();
                        p.Prev_OverAppraised = dr["Prev_OverAppraised"].ToString();

                        p.Cur_all_cost = string.IsNullOrEmpty(dr["Cur_all_cost"].ToString()) ? 0 : Convert.ToDouble(dr["Cur_all_cost"]);
                        p.Prev_all_cost = string.IsNullOrEmpty(dr["Prev_all_cost"].ToString()) ? 0 : Convert.ToDouble(dr["Prev_all_cost"]);

                        p.Cur_bcode = dr["Cur_bcode"].ToString();
                        p.Prev_bcode = dr["Prev_bcode"].ToString();
                        List.ComparativeReportsdata.Add(p);

                    }
                    log.Info("NewRematadoReports: " + List.ComparativeReportsdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ComparativeReportsdata = List.ComparativeReportsdata };
                }
                else
                {
                    log.Info("NewRematadoReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            else
            {
                _sql.commandExeStoredParam1("REM.dbo.ASYS_LuzonVisminMonthlyComparativeV31");
                _sql.REportsComparative2("@habwaMonth", "@prevmonth", habwa5, prevmonth5);
                dr = _sql.ExecuteDr1();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var z = new LVMComparativeReportsModels();
                        z.curdivision = dr["curdivision"].ToString();
                        z.curjewelryvalue = string.IsNullOrEmpty(dr["curjewelryvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curjewelryvalue"]);
                        z.curwatchvalue = string.IsNullOrEmpty(dr["curwatchvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curwatchvalue"]);
                        z.curcellvalue = string.IsNullOrEmpty(dr["curcellvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curcellvalue"]);
                        z.curmissingitemvalue = string.IsNullOrEmpty(dr["curmissingitemvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curmissingitemvalue"]);
                        z.curgoodstockvalue = string.IsNullOrEmpty(dr["curgoodstockvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curgoodstockvalue"]);

                        z.curjewelryqty = string.IsNullOrEmpty(dr["curjewelryqty"].ToString()) ? 0 : Convert.ToInt32(dr["curjewelryqty"]);
                        z.curwatchqty = string.IsNullOrEmpty(dr["curwatchqty"].ToString()) ? 0 : Convert.ToInt32(dr["curwatchqty"]);
                        z.curcellqty = string.IsNullOrEmpty(dr["curcellqty"].ToString()) ? 0 : Convert.ToInt32(dr["curcellqty"]);
                        z.curmissingitemqty = string.IsNullOrEmpty(dr["curmissingitemqty"].ToString()) ? 0 : Convert.ToInt32(dr["curmissingitemqty"]);
                        z.curgoodstockqty = string.IsNullOrEmpty(dr["curgoodstockqty"].ToString()) ? 0 : Convert.ToInt32(dr["curgoodstockqty"]);
                        z.curcoatedqty = string.IsNullOrEmpty(dr["curcoatedqty"].ToString()) ? 0 : Convert.ToInt32(dr["curcoatedqty"]);
                        z.curfakeqty = string.IsNullOrEmpty(dr["curfakeqty"].ToString()) ? 0 : Convert.ToInt32(dr["curfakeqty"]);
                        z.curoverappraisedqty = string.IsNullOrEmpty(dr["curoverappraisedqty"].ToString()) ? 0 : Convert.ToInt32(dr["curoverappraisedqty"]);

                        z.cur9Kweight = string.IsNullOrEmpty(dr["cur9Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["cur9Kweight"]);
                        z.cur12Kweight = string.IsNullOrEmpty(dr["cur12Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["cur12Kweight"]);
                        z.cur16Kweight = string.IsNullOrEmpty(dr["cur16Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["cur16Kweight"]);
                        z.cur20Kweight = string.IsNullOrEmpty(dr["cur20Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["cur20Kweight"]);
                        z.cur21Kweight = string.IsNullOrEmpty(dr["cur21Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["cur21Kweight"]);
                        z.curGSweight = string.IsNullOrEmpty(dr["curGSweight"].ToString()) ? 0 : Convert.ToDouble(dr["curGSweight"]);
                        z.curFakeweight = string.IsNullOrEmpty(dr["curFakeweight"].ToString()) ? 0 : Convert.ToDouble(dr["curFakeweight"]);
                        z.curCoatedweight = string.IsNullOrEmpty(dr["curCoatedweight"].ToString()) ? 0 : Convert.ToDouble(dr["curCoatedweight"]);
                        z.curOAweight = string.IsNullOrEmpty(dr["curOAweight"].ToString()) ? 0 : Convert.ToDouble(dr["curOAweight"]);
                        z.curmissingitemweight = string.IsNullOrEmpty(dr["curmissingitemweight"].ToString()) ? 0 : Convert.ToDouble(dr["curmissingitemweight"]);

                        z.curfakevalue = string.IsNullOrEmpty(dr["curfakevalue"].ToString()) ? 0 : Convert.ToDouble(dr["curfakevalue"]);
                        z.curCoatedevalue = string.IsNullOrEmpty(dr["curCoatedevalue"].ToString()) ? 0 : Convert.ToDouble(dr["curCoatedevalue"]);
                        z.curOAvalue = string.IsNullOrEmpty(dr["curOAvalue"].ToString()) ? 0 : Convert.ToDouble(dr["curOAvalue"]);
                        z.curprenda = string.IsNullOrEmpty(dr["curprenda"].ToString()) ? 0 : Convert.ToDouble(dr["curprenda"]);
                        z.curoaamount = string.IsNullOrEmpty(dr["curoaamount"].ToString()) ? 0 : Convert.ToDouble(dr["curoaamount"]);

                        z.prevdivision = dr["prevdivision"].ToString();

                        z.prevdivision = dr["prevdivision"].ToString();
                        z.prevjewelryvalue = string.IsNullOrEmpty(dr["prevjewelryvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevjewelryvalue"]);
                        z.prevwatchvalue = string.IsNullOrEmpty(dr["prevwatchvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevwatchvalue"]);
                        z.prevcellvalue = string.IsNullOrEmpty(dr["prevcellvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevcellvalue"]);
                        z.prevmissingitemvalue = string.IsNullOrEmpty(dr["prevmissingitemvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevmissingitemvalue"]);
                        z.prevGoodstockvalue = string.IsNullOrEmpty(dr["prevgoodstockvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevgoodstockvalue"]);

                        z.prevjewelryqty = string.IsNullOrEmpty(dr["prevjewelryqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevjewelryqty"]);
                        z.prevwatchqty = string.IsNullOrEmpty(dr["prevwatchqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevwatchqty"]);
                        z.prevcellqty = string.IsNullOrEmpty(dr["prevcellqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevcellqty"]);
                        z.prevmissingitemqty = string.IsNullOrEmpty(dr["prevmissingitemqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevmissingitemqty"]);
                        z.prevGoodstockqty = string.IsNullOrEmpty(dr["prevgoodstockqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevgoodstockqty"]);
                        z.prevcoatedqty = string.IsNullOrEmpty(dr["prevcoatedqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevcoatedqty"]);
                        z.prevfakeqty = string.IsNullOrEmpty(dr["prevfakeqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevfakeqty"]);
                        z.prevoverappraisedqty = string.IsNullOrEmpty(dr["prevoverappraisedqty"].ToString()) ? 0 : Convert.ToInt32(dr["prevoverappraisedqty"]);

                        z.prev9Kweight = string.IsNullOrEmpty(dr["prev9Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["prev9Kweight"]);
                        z.prev12Kweight = string.IsNullOrEmpty(dr["prev12Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["prev12Kweight"]);
                        z.prev16Kweight = string.IsNullOrEmpty(dr["prev16Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["prev16Kweight"]);
                        z.prev20Kweight = string.IsNullOrEmpty(dr["prev20Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["prev20Kweight"]);
                        z.prev21Kweight = string.IsNullOrEmpty(dr["prev21Kweight"].ToString()) ? 0 : Convert.ToDouble(dr["prev21Kweight"]);
                        z.prevGSweight = string.IsNullOrEmpty(dr["prevGSweight"].ToString()) ? 0 : Convert.ToDouble(dr["prevGSweight"]);
                        z.prevFakeweight = string.IsNullOrEmpty(dr["prevFakeweight"].ToString()) ? 0 : Convert.ToDouble(dr["prevFakeweight"]);
                        z.prevCoatedweight = string.IsNullOrEmpty(dr["prevCoatedweight"].ToString()) ? 0 : Convert.ToDouble(dr["prevCoatedweight"]);
                        z.prevOAweight = string.IsNullOrEmpty(dr["prevOAweight"].ToString()) ? 0 : Convert.ToDouble(dr["prevOAweight"]);
                        z.prevmissingitemweight = string.IsNullOrEmpty(dr["prevmissingitemweight"].ToString()) ? 0 : Convert.ToDouble(dr["prevmissingitemweight"]);

                        z.prevfakevalue = string.IsNullOrEmpty(dr["prevfakevalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevfakevalue"]);
                        z.prevCoatedevalue = string.IsNullOrEmpty(dr["prevCoatedevalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevCoatedevalue"]);
                        z.prevOAvalue = string.IsNullOrEmpty(dr["prevOAvalue"].ToString()) ? 0 : Convert.ToDouble(dr["prevOAvalue"]);
                        z.prevprenda = string.IsNullOrEmpty(dr["prevprenda"].ToString()) ? 0 : Convert.ToDouble(dr["prevprenda"]);
                        z.prevoaamount = string.IsNullOrEmpty(dr["prevoaamount"].ToString()) ? 0 : Convert.ToDouble(dr["prevoaamount"]);

                        z.curmonth = string.IsNullOrEmpty(dr["curmonth"].ToString()) ? null : Convert.ToDateTime(dr["curmonth"]).ToString("yyyy MMMM");
                        z.prevmonth = string.IsNullOrEmpty(dr["prevmonth"].ToString()) ? null : Convert.ToDateTime(dr["prevmonth"]).ToString("yyyy MMMM");

                        List.LVMComparativeReportsdata.Add(z);
                    }
                    log.Info("NewRematadoReports: " + List.LVMComparativeReportsdata);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LVMComparativeReportsdata = List.LVMComparativeReportsdata };
                }
                else
                {
                    log.Info("NewRematadoReports: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("NewRematadoReports: " + respMessage(0) + ex.Message);
            _sql.CloseConn1();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "NewRematadoReports: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult retreiveActionClass(string actionType)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.RetreiveActiondata = new RetreiveActionModels();
        List.RetreiveActiondata.costa = new List<double>();
        List.RetreiveActiondata.costb = new List<double>();
        List.RetreiveActiondata.costc = new List<double>();
        List.RetreiveActiondata.costd = new List<double>();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("select * from tbl_action where action_id <> " + actionType + " order by action_type");
            dr = _sql.ExecuteDr3();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.RetreiveActiondata.costa.Add(string.IsNullOrEmpty(dr["costa"].ToString()) ? 0 : Convert.ToDouble(dr["costa"]));
                    List.RetreiveActiondata.costb.Add(string.IsNullOrEmpty(dr["costb"].ToString()) ? 0 : Convert.ToDouble(dr["costb"]));
                    List.RetreiveActiondata.costc.Add(string.IsNullOrEmpty(dr["costc"].ToString()) ? 0 : Convert.ToDouble(dr["costc"]));
                    List.RetreiveActiondata.costd.Add(string.IsNullOrEmpty(dr["costd"].ToString()) ? 0 : Convert.ToDouble(dr["costd"]));
                    List.RetreiveActiondata.action_id.Add(List.RetreiveActiondata.ifNull(dr["action_id"]).Trim());
                    List.RetreiveActiondata.action_type.Add(List.RetreiveActiondata.ifNull(dr["action_type"]).Trim());
                }
                log.Info("retreiveActionClass: " + List.RetreiveActiondata);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RetreiveActiondata = List.RetreiveActiondata };
            }
            else
            {
                log.Info("retreiveActionClass: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("retreiveActionClass: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "retreiveActionClass: " + respMessage(0) + ex.Message };
        }
    }
    #endregion
    //---------------------------February 17,2017----------------steph--------------//
    //---------------------------February 18,2017----------------steph--------------//
    public WCFRespALLResult SaveAction(string T1, double T2, double T3, double T4, double T5)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("insert into tbl_action(action_type, CostA, CostB, CostC, CostD) Values('" + T1 + "'," + T2 +
                "," + T3 + "," + T4 + ", " + T5 + ")");
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("insert into remsluzon.dbo.tbl_action(action_type, CostA, CostB, CostC, CostD) Values('" + T1 + "'," + T2 +
                "," + T3 + "," + T4 + ", " + T5 + ")");
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("insert into remsvismin.dbo.tbl_action(action_type, CostA, CostB, CostC, CostD) Values('" + T1 + "'," + T2 +
                "," + T3 + "," + T4 + ", " + T5 + ")");
            _sql.Execute3();

            log.Info("SaveAction: " + respMessage(1) + " | " + T1);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SaveAction: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SaveAction: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult EditAction(string T1, double T2, double T3, double T4, double T5, string T6)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("update tbl_action set action_type = '" + T1 + "', CostA = " + T2 + ", CostB = " + T3 + ", CostC = " + T4 +
                ", CostD = " + T5 + " where action_id = " + T6);
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("update remsvismin.dbo.tbl_action set action_type = '" + T1 + "', CostA = " + T2 + ", CostB = " + T3 +
                ", CostC = " + T4 + ", CostD = " + T5 + " where action_id = " + T6);
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("update remsluzon.dbo.tbl_action set action_type = '" + T1 + "', CostA = " + T2 + ", CostB = " + T3 +
                ", CostC = " + T4 + ", CostD = " + T5 + " where action_id = " + T6);
            _sql.Execute3();

            log.Info("EditAction: " + respMessage(1) + " | " + T1);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("EditAction: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "EditAction: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult DeleteAction(string T6)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("delete from tbl_action where action_id = " + T6);
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("delete from remsluzon.dbo.tbl_action where action_id = " + T6);
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("delete from remsvismin.dbo.tbl_action where action_id = " + T6);
            _sql.Execute3();

            log.Info("DeleteAction: " + respMessage(1) + " | " + T6);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("DeleteAction: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DeleteAction: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult retreivesortclass()
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.retreivesortdata = new retreivesortModels();
        List.retreivesortdata.id = new List<string>();
        List.retreivesortdata.code = new List<string>();
        List.retreivesortdata.description = new List<string>();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("select id, code, description from tbl_sortclass order by code");
            dr = _sql.ExecuteDr3();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.retreivesortdata.id.Add(dr["id"].ToString());
                    List.retreivesortdata.code.Add(List.retreivesortdata.ifNull(dr["code"]));
                    List.retreivesortdata.description.Add(List.retreivesortdata.ifNull(dr["description"]));
                }
                log.Info("retreivesortclass: " + List.retreivesortdata);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), retreivesortdata = List.retreivesortdata };
            }
            else
            {
                log.Info("retreivesortclass: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("retreivesortclass: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "retreivesortclass: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SaveJewelryClass(string T1, string T2)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("insert into tbl_sortclass(code, description) Values('" + T1 + "', '" + T2 + "')");
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("insert into remsluzon.dbo.tbl_sortclass(code, description) Values('" + T1 + "', '" + T2 + "')");
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("insert into remsvismin.dbo.tbl_sortclass(code, description) Values('" + T1 + "', '" + T2 + "')");
            _sql.Execute3();

            log.Info("SaveJewelryClass: " + respMessage(1) + " | " + T1 + " | " + T2);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SaveJewelryClass: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SaveJewelryClass: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult EditJewelryClass(string T1, string T2, string T3)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("update tbl_sortclass set code = '" + T1 + "', description = '" + T2 + "' where id = " + T3);
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("update remsluzon.dbo.tbl_sortclass set code = '" + T1 + "', description = '" + T2 + "' where id = " + T3);
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("update remsvismin.dbo.tbl_sortclass set code = '" + T1 + "', description = '" + T2 + "' where id = " + T3);
            _sql.Execute3();

            log.Info("EditJewelryClass: " + respMessage(1) + " | " + T1 + " | " + T2 + " | " + T3);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("EditJewelryClass: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "EditJewelryClass: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult DeleteJewelryClass(string T3)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            //1st
            _sql.commandTraxParam3("delete from tbl_sortclass where id = " + T3);
            _sql.Execute3();
            //2nd
            _sql.commandTraxParam3("delete from remsluzon.dbo.tbl_sortclass where id = " + T3);
            _sql.Execute3();
            //3rd
            _sql.commandTraxParam3("delete from remsvismin.dbo.tbl_sortclass where id = " + T3);
            _sql.Execute3();

            log.Info("DeleteJewelryClass: " + respMessage(1) + " | " + T3);
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("DeleteJewelryClass: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "DeleteJewelryClass: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SearchBranch(string DB, string branchCode, string branchName, string region, string press)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.SearchBranchdata = new SearchBranchModels();
        List.SearchBranchdata.bedrnm = new List<string>();
        List.SearchBranchdata.bedrnr = new List<string>();
        List.SearchBranchdata.class_03 = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            if (press == "BBR")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + "  where bedrnr like '" + branchCode +
                        "%' and bedrnm like '" + branchName + "%' and class_03 like '" + region + "%' and Class_03 not like '%SHOWROOMS%' and" +
                        " Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + "  where bedrnr like '" + branchCode +
                        "%' and bedrnm like '" + branchName + "%' and class_03 like '" + region + "%' and Class_03 not like '%SHOWROOMS%' and" +
                        " Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + "  where bedrnr like '" + branchCode +
                        "%' and bedrnm like '" + branchName + "%' and class_03 like '" + region + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + "  where bedrnr like '" + branchCode +
                        "%' and bedrnm like '" + branchName + "%' and class_03 like '" + region + "%'");
                }
                if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + "  where bedrnr like '" + branchCode +
                        "%' and bedrnm like '" + branchName + "%' and class_03 like '" + region + "%' and Class_03 not like '%SHOWROOMS%' and" +
                        " Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnr like '" + branchCode + "%' and bedrnm like '" +
                        branchName + "%' and class_03 like '" + region + "%'");
                }
            }
            else if (press == "BB")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and bedrnm like '"
                        + branchName + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and bedrnm like '" +
                        branchName + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and bedrnm like '" +
                        branchName + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and bedrnm like '" +
                        branchName + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and bedrnm like '"
                        + branchName + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnr like '" + branchCode + "%' and bedrnm like '" +
                        branchName + "%'");
                }
            }
            else if (press == "BCR")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnr like '" + branchCode + "%' and class_03 like '" +
                        region + "%'");
                }
            }
            else if (press == "BNR")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnm like '" + branchName + "%' and class_03 like '" +
                        region + "%'");
                }
            }
            else if (press == "BC")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnr like '" + branchCode +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnr like '" + branchCode + "%'");
                }
            }
            else if (press == "BN")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where bedrnm like '" + branchName +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where bedrnm like '" + branchName + "%'");
                }
            }
            else if (press == "R")
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where class_03 like '" + region +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where class_03 like '" + region +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%'");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where class_03 like '" + region + "%'");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where class_03 like '" + region + "%'");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where class_03 like '" + region +
                        "%' and Class_03 not like '%SHOWROOMS%' and Class_03 not like '%HO%' and dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf where class_03 like '" + region + "%'");
                }
            }
            else
            {
                if (DB == "LUZON")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where dateend is null");//Modify query for split
                }
                else if (DB == "VISAYAS")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " ");
                }
                else if (DB == "SHOWROOM")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " ");
                }
                else if (DB == "MINDANAO")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " ");
                }
                else if (DB == "NCR")
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf" + DB + " where dateend is null");//Modify query for split
                }
                else
                {
                    _sql.commandExeParam("Select * from REMS.dbo.vw_bedryf ");
                }
            }
            dr = _sql.ExecuteDr();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.SearchBranchdata.bedrnm.Add(dr["bedrnm"].ToString());
                    List.SearchBranchdata.bedrnr.Add(dr["bedrnr"].ToString());
                    List.SearchBranchdata.class_03.Add(dr["class_03"].ToString());
                }
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), SearchBranchdata = List.SearchBranchdata };
            }
            else
            {
                log.Info("SearchBranch: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SearchBranch: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SearchBranch: " + respMessage(0) + ex.Message };
        }
    }//DONE
    //---------------------------February 18,2017----------------steph--------------//
    //---------------------------February 19,2017----------------steph--------------//
    public WCFRespALLResult SearchBranchLoad(string DB)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.costcentersdata = new costcentersModels();
        List.costcentersdata.costDept = new List<string>();
        try
        {
            if (DB == "LNCR")
            {
                DB = "NCR";
            }
            _sql.Connection0();
            _sql.OpenConn();
            if (DB == "LUZON")
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf" + DB +
                    " where class_03 not like '%Showroom%' and dateend is null order by class_03 asc");//Modify query for split
            }
            else if (DB == "NCR")
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf" + DB +
                    " where class_03 not like '%Showroom%' and dateend is null order by class_03 asc");//Modify query for split
            }
            else if (DB == "VISAYAS")
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf" + DB +
                    " where class_03 not like '%Showroom%' order by class_03 asc");
            }
            else if (DB == "SHOWROOM")
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf" + DB + " order by class_03 asc");
            }
            else if (DB == "MINDANAO")
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf" + DB + " order by class_03 asc");
            }
            else
            {
                _sql.commandExeParam("Select distinct class_03 from REMS.dbo.vw_bedryf order by class_03 asc");
            }
            dr = _sql.ExecuteDr();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.costcentersdata.costDept.Add(dr["class_03"].ToString().ToUpper());
                }
                log.Info("SearchBranchLoad: " + List.costcentersdata.costDept);
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), costcentersdata = List.costcentersdata };
            }
            else
            {
                log.Info("SearchBranchLoad: " + respMessage(2));
                _sql.CloseDr();
                _sql.CloseConn();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("SearchBranchLoad: " + respMessage(0) + ex.Message);
            _sql.CloseConn();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SearchBranchLoad: " + respMessage(0) + ex.Message };
        }
    }//DONE

    //---------------------------------------------------------------------------------------------------------------------------------------------------


    //public WCFRespALLResult RetreiveBarcodeHistory2(string barcode)
    //{
    //    var _sql = new Connection();
    //    var models = new BarcodeHistoryModels();
    //    try
    //    {
    //        _sql.Connection3();
    //        _sql.OpenConn3();
    //        _sql.commandExeParam3("SELECT TOP 1 PHOTONAME FROM REMS.DBO.ASYS_REM_DETAIL WHERE REFALLBARCODE = 'TODD'");
            
            
            
    //        //UNION ALL SELECT TOP 1 PHOTONAME FROM REMS.DBO.ASYS_REMOUTSOURCE_DETAIL ");//= '10130004201700057' union all select top 1 photoname from ASYS_REMoutsource_Detail where refallbarcode = '10130004201700057'" );
    //          // " = '" + barcode + "' order by trandate desc ");
    //        dr = _sql.ExecuteDr3();
    //        if (dr.HasRows == false)
    //        {
    //            dr.Close();
    //            _sql.Connection3();
    //            _sql.OpenConn3();
    //            _sql.commandExeParam3("SELECT TOP 1 PHOTONAME FROM REMS.DBO.ASYS_REM_DETAIL WHERE REFALLBARCODE = 'TODD'");
    //            dr = _sql.ExecuteDr3();
                
    //        }


    //        if (dr.Read())
    //        {
    //            models.description = models.ifNull(dr["description"]);
    //            models.lblqty = respCode(1);
    //            models.weight = models.ifNull(dr["weight"]);
    //            models.karat = models.ifNull(dr["karat"]);
    //            models.carat = models.ifNull(dr["carat"]);
    //            models.SerialNo = models.ifNull(dr["SerialNo"]);
    //            models.price = models.ifNull(dr["price"]);
    //            log.Info("RetreiveBarcodeHistory: " + barcode + " | " + models.description + " | price: " + models.price);
    //            _sql.CloseDr3();
    //            _sql.CloseConn3();
    //            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), BarcodeHistorydata = models };
    //        }
    //        else
    //        {
    //            log.Info("RetreiveBarcodeHistory: " + respMessage(2));
    //            _sql.CloseDr3();
    //            _sql.CloseConn3();
    //            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error("RetreiveBarcodeHistory: " + respMessage(0) + ex.Message);
    //        _sql.CloseConn3();
    //        return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetreiveBarcodeHistory: " + respMessage(0) + ex.Message };
    //    }
    //}
    //---------------------------------------------------------------------------------------------------------------------------------------------------





    public WCFRespALLResult GetBarcodePhotos(string barcode)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("select top 1 photoname from ASYS_REM_Detail where refallbarcode = '" + barcode +"'");
            
            dr = _sql.ExecuteDr3();

            if (dr.HasRows == false)
            {
                dr.Close();
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select top 1 photoname from ASYS_REMoutsource_Detail where refallbarcode = '" + barcode + "'");
                 
                dr = _sql.ExecuteDr3();

            }

            if (dr.Read())
            {
                models.lotno = models.isNull(dr["photoname"]);

                log.Info("GetBarcodePhotos: " + models.lotno);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), TempLotnodata = models };
            }
            else
            {
                log.Info("GetBarcodePhotos: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetBarcodePhotos: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhotos: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult RetreiveBarcodeHistory(string barcode)
    {
        var _sql = new Connection();
        var models = new BarcodeHistoryModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("select top 1 description,karat,carat,weight,SerialNo,price,cost from ASYS_BarcodeHistory where refallbarcode" +
               " = '" + barcode + "' order by trandate desc ");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.description = models.ifNull(dr["description"]);
                models.lblqty = respCode(1);
                models.weight = models.ifNull(dr["weight"]);
                models.karat = models.ifNull(dr["karat"]);
                models.carat = models.ifNull(dr["carat"]);
                models.SerialNo = models.ifNull(dr["SerialNo"]);
                models.price = models.ifNull(dr["price"]);
                log.Info("RetreiveBarcodeHistory: " + barcode + " | " + models.description + " | price: " + models.price);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), BarcodeHistorydata = models };
            }
            else
            {
                log.Info("RetreiveBarcodeHistory: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("RetreiveBarcodeHistory: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "RetreiveBarcodeHistory: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult BarcodeHistory(string barcode)
    {
        var _sql = new Connection();
        var List = new WCFRespALLResult();
        List.RetreiveBarcodeHistorydata = new RetreiveBarcodeHistoryModels();
        List.RetreiveBarcodeHistorydata.trandate = new List<string>();
        List.RetreiveBarcodeHistorydata.lotno = new List<string>();
        List.RetreiveBarcodeHistorydata.status = new List<string>();
        List.RetreiveBarcodeHistorydata.consignto = new List<string>();
        List.RetreiveBarcodeHistorydata.costcenter = new List<string>();
        List.RetreiveBarcodeHistorydata.empname = new List<string>();
        List.RetreiveBarcodeHistorydata.cost = new List<string>();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("SELECT lotno,trandate,costcenter,consignto,status,empname,custodian,cost FROM ASYS_BarcodeHistory WHERE" +
                " refallbarcode = '" + barcode + "' ORDER BY trandate DESC");
            dr = _sql.ExecuteDr3();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    List.RetreiveBarcodeHistorydata.trandate.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["trandate"]));
                    List.RetreiveBarcodeHistorydata.lotno.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["lotno"]));
                    List.RetreiveBarcodeHistorydata.status.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["status"]));
                    List.RetreiveBarcodeHistorydata.consignto.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["consignto"]));
                    List.RetreiveBarcodeHistorydata.costcenter.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["costcenter"]));
                    List.RetreiveBarcodeHistorydata.empname.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["empname"]));
                    List.RetreiveBarcodeHistorydata.cost.Add(List.RetreiveBarcodeHistorydata.IFNULL(dr["cost"]));
                }
                log.Info("BarcodeHistory: barcode: " + barcode + " | " + List.RetreiveBarcodeHistorydata);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RetreiveBarcodeHistorydata = List.RetreiveBarcodeHistorydata };
            }
            else
            {
                log.Info("BarcodeHistory: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0) , responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("BarcodeHistory: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "BarcodeHistory: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult ViewHistory(string barcode)
    {
        var _sql = new Connection();
        var models = new ViewBarcodeHistoryModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select receivedate,allbarcode,itemcode,branchitemdesc,weight,karatgrading,SerialNo,all_cost from ASYS_REM_detail where" + ////("Select receivedate,allbarcode,itemcode,branchitemdesc,weight,karatgrading,SerialNo,all_cost from" +
                " refallbarcode='" + barcode + "'");// union all Select receivedate,allbarcode,itemcode,branchitemdesc,weight,karatgrading," +
                //"SerialNo,all_cost from ASYS_REMoutsource_detail where refallbarcode='" + barcode + "')a order by receivedate desc");
            
            dr = _sql.ExecuteDr3();

            if (dr.HasRows == false)
            {
                _sql.CloseConn3();
                _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select receivedate,allbarcode,itemcode,branchitemdesc,weight,karatgrading," +
                "SerialNo,all_cost from ASYS_REMoutsource_detail where refallbarcode='" + barcode + "'");
            
            dr = _sql.ExecuteDr3();
            }


            if (dr.Read())
            {
                models.allbarcode = dr["allbarcode"].ToString();
                models.itemcode = dr["itemcode"].ToString();
                models.branchitemdesc = dr["branchitemdesc"].ToString();
                models.weight = dr["weight"].ToString();
                models.karatgrading = dr["karatgrading"].ToString();
                models.all_cost = models.ifNull(dr["all_cost"]);

                log.Info("ViewHistory: barcode: " + barcode + " | " + models.allbarcode + " | " + models.branchitemdesc + " | " + models.all_cost);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ViewBarcodeHistorydata = models };
            }
            else
            {
                log.Info("ViewHistory: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("ViewHistory: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "ViewHistory: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult CheckBarcode(string barcode)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("If (Select Count(*) from ASYS_REM_Detail where refallbarcode = '" + barcode + "')<> 0 begin Select" +
                " refAllbarcode from ASYS_REM_Detail where refallbarcode ='" + barcode + "' end else Select refallbarcode from" +
                " ASYS_REMOutsource_detail where refallbarcode = '" + barcode + "'");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                log.Info("CheckBarcode: " + barcode);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            else
            {
                log.Info("CheckBarcode: " + respMessage(12));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(12) };
            }
        }
        catch (Exception ex)
        {
            log.Error("CheckBarcode: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "CheckBarcode: " + respMessage(0) + ex.Message };

        }
    }
    public WCFRespALLResult CheckAvailableBarcode(string barcode, string midBarcode)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("Select refallbarcode as barcode from ASYS_REM_Detail where refallbarcode = '" + barcode + midBarcode +
                "' union all select refallbarcode as barcode from ASYS_REMOutsource_detail where refallbarcode = '" + barcode + midBarcode + "' ");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                log.Info("CheckAvailableBarcode: " + barcode);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            else
            {
                log.Info("CheckAvailableBarcode: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("CheckAvailableBarcode: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "CheckAvailableBarcode: " + respMessage(0) + ex.Message };
        }
    }
    public WCFRespALLResult SaveEditedBarcode(string[][] ListView)
    {
        var _sql = new Connection();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.BeginTransax3();
            for (int i = 0; i <= ListView.Count() - 1; i++)
            {
                if (ListView[i] != null)
                {
                    //1st
                    _sql.commandTraxParam3("Update ASYS_REM_Detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //2nd
                    _sql.commandTraxParam3("Update ASYS_REMOutsource_detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //3rd
                    _sql.commandTraxParam3("Update [remsluzon].dbo.ASYS_REM_Detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //4th
                    _sql.commandTraxParam3("Update [remsluzon].dbo.ASYS_REMOutsource_detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //5th
                    _sql.commandTraxParam3("Update [remsvisayas].dbo.ASYS_REM_Detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //6th
                    _sql.commandTraxParam3("Update [remsvisayas].dbo.ASYS_REMOutsource_detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //7th
                    _sql.commandTraxParam3("Update [remsmindanao].dbo.ASYS_REM_Detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //8th
                    _sql.commandTraxParam3("Update [remsmindanao].dbo.ASYS_REMOutsource_detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //9th
                    _sql.commandTraxParam3("Update [remsshowroom].dbo.ASYS_REM_Detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                    //10th
                    _sql.commandTraxParam3("Update [remsshowroom].dbo.ASYS_REMOutsource_detail set refallbarcode = '" + ListView[i][1] +
                        "'  where refallbarcode = '" + ListView[i][0] + "'");
                    _sql.Execute3();
                }
                else
                {
                    break;
                }
            }
            log.Info("SaveEditedBarcode: " + respMessage(1));
            _sql.commitTransax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
        }
        catch (Exception ex)
        {
            log.Error("SaveEditedBarcode: " + respMessage(0) + ex.Message);
            _sql.RollBackTrax3();
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "SaveEditedBarcode: " + respMessage(0) + ex.Message };
        }
    }
    //---------------------------February 19,2017----------------steph--------------//
    private string respMessage(int msg)
    {
        string message = "";
        switch (msg)
        {
            case 0:
                return message = "Service2 Error: ";
            case 1:
                return message = "Success!";
            case 2:
                return message = "No Data Found!";
            case 3:
                return message = "You are unauthorized to use this application.";
            case 4:
                return message = "Failed To Regenerate Lot Number.";
            case 5:
                return message = "Not Available.";
            case 6:
                return message = "Releasing Summary is not Available.";
            case 7:
                return message = "PTN doesn't exist.";
            case 8:
                return message = "Itemcode has an attached photo already!\n Unable to display image, file Path not found!";
            case 9:
                return message = "The Image was corrupted due to out of memory";
            case 10:
                return message = "A.L.L Barcode does not exist!";
            case 11:
                return message = "Wrong barcode or barcode doesn't have an attached photo.";
            case 12:
                return message = "ALLBarcode doesn't exist.";
            default:
                return message;
        }
    }
    private string respCode(int code)
    {
        string xcode = "";
        switch (code)
        {
            case 0:
                return xcode = "0";
            case 1:
                return xcode = "1";
            default:
                return xcode;
        }
    }

    public WCFRespALLResult Bytestoimage(byte[] bytes, string allbarcode, bool flag)
    {
        try
        {
            imageconverter(bytes, allbarcode,flag);
            
            return new WCFRespALLResult { responseCode = "1" };
        }
        catch (Exception)
        {


            return new WCFRespALLResult { responseCode = "0" };
        }

        
    }

    public byte[] x(Image img)
    {
        int originalwidth = img.Width;
        int originalheight = img.Height;
        //File
        Bitmap bmpimage = new Bitmap(originalwidth, originalheight);

        Graphics gf = Graphics.FromImage(bmpimage);
        gf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        gf.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
        gf.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        Rectangle rect = new Rectangle(0, 0, originalwidth, originalheight);
        gf.DrawImage(img, rect, 0, 0, originalwidth, originalheight, GraphicsUnit.Pixel);

        byte[] imagearray = null;

        using (MemoryStream ms = new MemoryStream())
        {
            bmpimage.Save(ms, ImageFormat.Jpeg);
            imagearray = ms.ToArray();
        }
        bmpimage.Dispose();
        return imagearray;
    }


    public byte[] photoconverter(Image img)
    {
        int originalwidth = img.Width;
        int originalheight = img.Height;
        //File
        Bitmap bmpimage = new Bitmap(originalwidth, originalheight);

        Graphics gf = Graphics.FromImage(bmpimage);
        gf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        gf.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
        gf.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        Rectangle rect = new Rectangle(0, 0, originalwidth, originalheight);
        gf.DrawImage(img, rect, 0, 0, originalwidth, originalheight, GraphicsUnit.Pixel);

        byte[] imagearray = null;

        using (MemoryStream ms = new MemoryStream())
        {
            bmpimage.Save(ms, ImageFormat.Jpeg);
            imagearray = ms.ToArray();
        }
        bmpimage.Dispose();
        return imagearray;
    }
    public Bitmap y(byte[] bytes, string allbarcode)
    {
        try
        {
           

            
            MemoryStream ms = new MemoryStream(bytes);
        Bitmap btmap = new Bitmap(ms);
        Image imge = Image.FromStream(ms);

        String path = asysSaveImagefile1 + allbarcode + ".JPG";
        imge.Save(path, ImageFormat.Jpeg);

        ms.Close();
        ms.Dispose();
        return btmap;
        }
        catch (Exception ex)
        {
            
            throw;
        }

    }

    public Bitmap imageconverter(byte[] bytes, string allbarcode, bool flag)
    {
        try
        {
            bool todd;
            todd = Directory.Exists(Connection.GoodstockSDestination);
            if (todd == true && flag == false)
            {
                asysSaveImagefile1 = Connection.GoodstockSDestination.ToString();
            }
            else 
            {
                asysSaveImagefile1 = Connection.JewelrySDestination.ToString();
            }
            MemoryStream ms = new MemoryStream(bytes);

            Bitmap btmap = new Bitmap(ms);
            Image imge = Image.FromStream(ms);
            //ms.Dispose();

            String path = asysSaveImagefile1 + allbarcode + ".JPG";
            imge.Save(path, ImageFormat.Jpeg);

            ms.Close();
            ms.Dispose();
            return btmap;


          

        }
        catch (Exception ex)
        {

            throw;
        }

 
    }


    public WCFRespALLResult getphoto(string allbarcode)

    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        var mod = new PhotoMembersModels();
        try
        {
            //_sql.Connection3();
            //_sql.OpenConn3();
            //_sql.commandExeParam3("select top 1 photoname from ASYS_REMoutsource_Detail where refallbarcode = '" + allbarcode + "'");
            //dr = _sql.ExecuteDr3();

            //if (dr.HasRows == false)
            //{
               // dr.Close();

                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select top 1 photoname from ASYS_REM_Detail where refallbarcode = '" + allbarcode + "' union all select top 1 photoname from ASYS_REMoutsource_Detail where refallbarcode = '" + allbarcode + "'");
                dr = _sql.ExecuteDr3();

            //}
            
            if (dr.Read())
            {
                models.lotno = models.isNull(dr["photoname"]);
                /*if (File.Exists(models.lotno) == false)
                {
                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(0), PhotoMembersdata = mod };
                }
                else
                {*/
                    Image xxx;
                    using (var photoname = new Bitmap(models.lotno))
                    {
                        xxx = new Bitmap(photoname);

                    }
                    mod.photocontainer = x(xxx);
                    log.Info("GetBarcodePhotos: " + models.lotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PhotoMembersdata = mod };
               // }
         

                
               
            }
            else
            {
                log.Info("GetBarcodePhotos: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetBarcodePhotos: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhotos: " + respMessage(0) + ex.Message };
        }
       
    }




    public WCFRespALLResult getphotoptn(string allbarcode)
    {
        var _sql = new Connection();
        var models = new TempLotnoModels();
        var mod = new PhotoMembersModels();
        try
        {
            _sql.Connection3();
            _sql.OpenConn3();
            _sql.commandExeParam3("select top 1 photoname from ASYS_REM_Detail where ptn = '" + allbarcode +
                "' union all select top 1 photoname from ASYS_REMoutsource_Detail where ptn = '" + allbarcode + "'");
            dr = _sql.ExecuteDr3();
            if (dr.Read())
            {
                models.lotno = models.isNull(dr["photoname"]);
                if (File.Exists(models.lotno) == false)
                {

                    return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(0), PhotoMembersdata = mod };
                }
                else
                {
                    Image xxx;
                    using (var photoname = new Bitmap(models.lotno))
                    {
                        xxx = new Bitmap(photoname);

                    }
                    mod.photocontainer = x(xxx);

                    log.Info("GetBarcodePhotos: " + models.lotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new WCFRespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), PhotoMembersdata = mod };
                }
           
                
            }
            else
            {
                log.Info("GetBarcodePhotos: " + respMessage(2));
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new WCFRespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
            }
        }
        catch (Exception ex)
        {
            log.Error("GetBarcodePhotos: " + respMessage(0) + ex.Message);
            _sql.CloseConn3();
            return new WCFRespALLResult { responseCode = respCode(0), responseMsg = "GetBarcodePhotos: " + respMessage(0) + ex.Message };
        }

    }
    public bool red;
    public String result1;
    //public bool bcodechecker(string barcode)
    //{
    //    var _sql = new Connection();
    //    var models = new TempLotnoModels();
    //    try
    //    {


    //        _sql.Connection0();
    //        _sql.OpenConn();
    //        _sql.commandExeParam("SELECT * FROM REMS.dbo.ASYS_BARCODEHISTORY WHERE REFALLBARCODE = '" + barcode + "'");
    //        dr = _sql.ExecuteDr();
    //        if (dr.Read())
    //        {
    //            dr.Close();
    //            result1 = "Barcode already exist : "+barcode;
    //            return true;
                
    //        }
    //        else
    //        {
    //            dr.Close();
    //            return false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result1 = ex.Message;
    //        red = true;
    //        return false;
    //    }
    //}
}





