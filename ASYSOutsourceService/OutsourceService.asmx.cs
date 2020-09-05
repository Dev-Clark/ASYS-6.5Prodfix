using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using log4net.Config;
using ASYSMVCQuery;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace ASYSOutsourceService
{
    /// <summary>
    /// Summary description for ASYSServiceASMX
    /// </summary>
    [WebService(Namespace = "http://localhost/", Description = "A.L.L Jewelry Division", Name = "ASYS Web Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ASYSService : System.Web.Services.WebService
    {
        #region All methods 
        #region Jan 18,2017
        //-------------------------------Start Jan 18,2017-----------------------------------//
        //-------------------------------Stephen Asi-----------------------------------//        
        //private string type;
        private int type2;
        private string sortCode;
        private string status;
        private string Dept;

        private SqlDataReader rdr;
        private SqlDataAdapter da;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public String TradeMonth2;
        public static String i_am = "REMOUT6.5";
        public ASYSService()
        {
            XmlConfigurator.Configure();
        }
        //------------------------On Load Main Form-------------------------------//
      
     
        
        #region CAll Bedrefy
        [WebMethod]
        public RespALLResult UpdateBedryfVisayas()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdatebedryfVisayas");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)


            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UpdateBedryfMindanao()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdatebedryfVismin");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UpdateBedryfLuzon()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdateBedryfLuzon");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UpdateBedryfShowroom()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdateBedryfShowroom");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UpdateHumressVismin()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdateHumresVismin");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UpdateHumressLuzon()
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("UpdateHumresLuzon");
                _sql.Execute3();

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        #endregion
        [WebMethod]
        public RespALLResult MainLoad(string userLog)
        {

            var _sql = new Connection();
            var models = new MainLoadModels();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select job_title from " + Connection.humres2 + " where fullname='" + userLog + "'");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.job_title = rdr["job_title"].ToString().Trim();


                    log.Info("MainLoad: " + models.job_title);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), MainLoaddata = models };
                }
                else
                {
                    log.Info("MainLoad: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("MainLoad: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
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
            throw new Exception ("err");
        }


        //------------------------On Load Main Form end-------------------------------//
        [WebMethod]
        public LoginResult Login(string username, string password)
        {
            var models = new LoginInfo();
            var _sql = new Connection();

            try
            {
                _sql.Connection3();
                _sql.Connection0();
                _sql.OpenConn();
                //_sql.commandExeParam("SELECT TOP 1 * FROM [REMS].dbo.vw_humresall WHERE usr_id=@username AND res_id=@password AND blocked = 1 AND job_title IN ('SORTER','ALLBOSMAN','MLWB','PRICING','DISTRI','ALLDEPTMNGR','VERIFIER','APISORTER')");
                _sql.commandExeParam("SELECT TOP 1 * FROM [REMS].dbo.vw_humresall WHERE usr_id=@username AND res_id=@password AND blocked = 1 AND job_title IN ('SORTER','ALLBOSMAN','MLWB','PRICING','DISTRI','ALLDEPTMNGR','VERIFIER','OPISORTER','RELEASESTAFF')");
                //SELECT TOP 1 * FROM [REMS].dbo.vw_humresall WHERE usr_id=@username AND res_id=@password AND blocked = 1 AND job_title IN ('SORTER','ALLBOSMAN','MLWB','PRICING','DISTRI','ALLDEPTMNGR','VERIFIER','OPISORTER','RELEASESTAFF')

                _sql.RetrieveInfoParam("@username", "@password", username, password);
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.jobTitle = rdr["job_title"].ToString().Trim();
                    models.fullName = rdr["fullname"].ToString().Trim().ToUpper();
                    models.idNumber = Convert.ToInt32(rdr["res_id"].ToString().Trim());
                    models.userFlag = true;
                    models.FlagLoggedIn = true;
                    models.photodes = Connection.photodes;

                    log.Info("Login: jobTitle: " + models.jobTitle + " | fullName: " + models.fullName + " | idNumber: " + models.idNumber);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new LoginResult { responseCode = respCode(1), responseMsg = respMessage(1), data = models };
                }
                else
                {
                    log.Info("Login: " + respMessage(3) + " | username: " + username);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new LoginResult { responseCode = respCode(0), responseMsg = respMessage(3) };
                }
            }
            catch (Exception ex)
            {
                log.Error("Login: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new LoginResult { responseCode = respCode(0), responseMsg = "Login: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult getNewLotNumber(string DeptType, string TransType)
         {
            var models = new OutSourceModels();

            var _sql = new Connection();
            var _sql2 = new Connection();
            var lotno = string.Empty;
            SqlDataReader dr;
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql2.Connection3();
                _sql2.OpenConn3();
                _sql.commandExeParam3("Select Lotno from ASYS_Lotno_Gen WHERE BusinessCenter ='" + DeptType + "'");
                rdr = _sql.ExecuteDr3();
               if (rdr.HasRows)
                {
                    rdr.Read();
                    lotno = rdr["Lotno"].ToString().Trim();
                    if (lotno == "")
                    {
                        models.getNewLot = 0;
                    }
                    else
                    {
                        models.getNewLot = Convert.ToInt32(rdr["Lotno"]) + 1;
                    }
                    log.Info("getNewLotNumber: " + models.getNewLot);
                    _sql.CloseDr3();
                    _sql.CloseConn3();

                    if (models.getNewLot == 0 && DeptType.ToUpper() == "PRICING")
                    {
                        _sql2.commandExeParam3("SELECT max(reflotno) as Lotno from rems.dbo.asys_pricing_detail where status in ('RELEASED','RECDISTRI')");
                        dr = _sql2.ExecuteDr3();
                        if (dr.Read())
                        {
                            models.getNewLot = Convert.ToInt32(dr["Lotno"]) + 1;
                            models.lastlot = Convert.ToInt32(dr["Lotno"]);
                            log.Info("getNewLotNumber: " + models.getNewLot);
                            dr.Dispose();
                            _sql2.OpenConn3();
                            _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='PRICING'");
                            _sql2.Execute3();
                        }
                    }
                    else if (models.getNewLot == 0 && DeptType.ToUpper() == "MLWB")
                    {
                        _sql2.commandExeParam3("SELECT max(reflotno) AS Lotno from rems.dbo.asys_mlwb_detail where status in ('RELEASED','RECPRICING')");
                        dr = _sql2.ExecuteDr3();
                        if (dr.Read())
                        {
                            models.getNewLot = Convert.ToInt32(dr["Lotno"]) + 1;
                            models.lastlot = Convert.ToInt32(dr["Lotno"]);
                            log.Info("getNewLotNumber: " + models.getNewLot);
                            dr.Dispose();
                            _sql2.OpenConn3();
                            _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='MLWB'");
                            _sql2.Execute3();
                        }
                    }
                    else if (models.getNewLot == 0 && DeptType.ToUpper() == "DISTRI")
                    {
                        _sql2.commandExeParam3("SELECT max(reflotno) as lotno from asys_distri_detail");
                        dr = _sql2.ExecuteDr3();
                        if (dr.Read())
                        {
                            models.getNewLot = Convert.ToInt32(dr["lotno"]) + 1;
                            models.lastlot = Convert.ToInt32(dr["Lotno"]);
                            log.Info("getNewLotNumber: " + models.getNewLot);
                            dr.Dispose();
                            _sql2.OpenConn3();
                            _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='DISTRI'");
                            _sql2.Execute3();
                        }
                    }
                    else if (models.getNewLot == 0 && DeptType.ToUpper() == "CONSIGNMENT")
                    {
                        _sql2.commandExeParam3("SELECT max(lotno) as lotno from asys_consign_detail");
                        dr = _sql2.ExecuteDr3();
                        if (dr.Read())
                        {
                            models.getNewLot = Convert.ToInt32(dr["lotno"]) + 1;
                            models.lastlot = Convert.ToInt32(dr["Lotno"]);
                            log.Info("getNewLotNumber: " + models.getNewLot);
                            dr.Dispose();
                            _sql2.OpenConn3();
                            _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='CONSIGNMENT'");
                            _sql2.Execute3();
                        }
                    }
                    else if (models.getNewLot == 0 && DeptType.ToUpper() == "REM")
                    {
                        if (TransType == "OUTSOURCE")
                        {
                            _sql2.commandExeParam3("SELECT max(reflotno) as lotno from asys_remoutsource_detail where status = 'RECMLWB'");
                            dr = _sql2.ExecuteDr3();
                            if (dr.Read())
                            {
                                models.getNewLot = Convert.ToInt32(dr["lotno"]) + 1;
                                models.lastlot = Convert.ToInt32(dr["Lotno"]);
                                log.Info("getNewLotNumber: " + models.getNewLot);
                                dr.Dispose();
                                _sql2.OpenConn3();
                                _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='REM'");
                                _sql2.Execute3();
                            }
                        }
                        else
                        {
                            _sql2.commandExeParam3("SELECT max(reflotno) as lotno from asys_rem_detail where status = 'RECMLWB'");
                            dr = _sql2.ExecuteDr3();
                            if (dr.Read())
                            {
                                models.getNewLot = Convert.ToInt32(dr["lotno"]) + 1;
                                models.lastlot = Convert.ToInt32(dr["Lotno"]);
                                log.Info("getNewLotNumber: " + models.getNewLot);
                                dr.Dispose();
                                _sql2.OpenConn3();
                                _sql2.commandExeParam3("UPDATE asys_lotno_gen SET lotno = '" + models.lastlot + "' WHERE BusinessCenter ='REM'");
                                _sql2.Execute3();
                            }
                        }
                    }
                    _sql2.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourcedata = models };
                }
                else
                {
                    log.Info("getNewLotNumber: " + respMessage(4));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    _sql2.CloseDr3();
                    _sql2.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(4), OutSourcedata = models };
                }
            }
            catch (Exception ex)
            {
                log.Error("getNewLotNumber: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "getNewLotNumber: " + respMessage(0) + ex.Message };
            }
        }

        [WebMethod]
        public RespALLResult exists(string barcode)
        {
            var models = new checkBarcodeIfExistModels();

            var _sql = new Connection();
            type2 = 3;
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParams3("@allbarcode", "@type", barcode, type2);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.exists = true;
                    log.Info("exists: " + "A.L.L. " + barcode + " already received.");
                }
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = "A.L.L. " + barcode + " already received.", checkdata2 = models };
            }
            catch (Exception ex)
            {
                log.Error("exists: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };

            }


        }
        //new saving 1st and 2nd      
        //new saving 3rd,4th
        [WebMethod]
        public RespALLResult saveOutSourceAll(string lotno, string actionClass, string userLog, string Receiver, string typeClass, string[][] dgEntry)
        {
            var _sql = new Connection();
            sortCode = "O";
            status = "RECEIVED";
            Dept = "REM";
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                //1st
                _sql.commandTraxParam3("UPDATE ASYS_Lotno_Gen SET lotno =@getNewLotUpdate WHERE BusinessCenter='REM'");
                _sql.RetrieveInfoParams2("@getNewLotUpdate", lotno);
                _sql.Execute3();
                //2nd
                _sql.commandTraxParam3("INSERT INTO ASYS_REMOutSource_Header (Lotno, TYPE) VALUES(@getNewLotInsert,@type)");
                _sql.RetrieveInfoParams("@getNewLotInsert", "@type", lotno, typeClass);
                _sql.Execute3();
                for (int i = 0; i <= dgEntry.Count() - 2; i++)
                {
                    //3rd
                    _sql.commandTraxParam3("INSERT INTO ASYS_REMOutSource_detail (Reflotno,lotno,refallbarcode,allbarcode,refitemcode,itemcode,branchitemdesc," +
                            "refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc,all_karat,all_carat,all_weight,price_desc,price_karat,price_weight,price_carat,sortdate," +
                            "sortername,receivedate,receiver,status) VALUES('" + lotno + "','" + lotno + "','" + dgEntry[i][0] + "','" + dgEntry[i][0] + "','" + dgEntry[i][1]
                            + "','" + dgEntry[i][1] + "','" + dgEntry[i][2] + "','" + dgEntry[i][3] + "','" + dgEntry[i][3] + "','" + dgEntry[i][5] + "','" + dgEntry[i][6] +
                            "','" + dgEntry[i][4] + "','" + actionClass + "','" + sortCode + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] + "','" + dgEntry[i][6] + "','" +
                            dgEntry[i][4] + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] + "','" + dgEntry[i][4] + "','" + dgEntry[i][6] + "',getdate(),'" +
                            userLog + "',getdate(),'" + Receiver + "','" + status + "')");
                    _sql.Execute3();
                    //4th
                    _sql.commandTraxParam3("INSERT INTO ASYS_BarcodeHistory (lotno,refallbarcode,allbarcode,itemcode,[description],karat,carat," +
                        "weight,empname,trandate,costcenter,status) VALUES ('" + lotno + "','" + dgEntry[i][0] + "','" + dgEntry[i][0] +
                        "','" + dgEntry[i][1] + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] + "','" + dgEntry[i][6] + "','" + dgEntry[i][4] + "','"
                        + userLog + "',getdate(),'" + Dept + "','" + status + "')");
                    _sql.Execute3();
                }
                log.Info("saveOutSourceAll: " + respMessage(1) + " | lotno: " + lotno + " | typeClass: " + typeClass + " | userLog: " + userLog);
                _sql.commitTransax3();
                //sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceAll: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveOutSourceAll: " + respMessage(0) + ex.Message };
            }
        }

        //insert return by A.L.L barcode
        //new
        [WebMethod]
        public RespALLResult saveReturnsALLByBarcode(string lotno, string branchCode, string branchName, string comboValue,
            string receiver, string userLog, string controlNum, string typeClass, string[][] dgEntry)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                //new
                //1st
                _sql.commandTraxParam3("UPDATE ASYS_Lotno_Gen SET lotno =@getNewLotUpdate WHERE BusinessCenter='REM'");
                _sql.RetrieveInfoParams2("@getNewLotUpdate", lotno);
                _sql.Execute3();
                //2nd
                _sql.commandTraxParam3("INSERT INTO ASYS_REMOutSource_Header (Lotno, TYPE) VALUES(@getNewLotInsert,@type)");
                _sql.RetrieveInfoParams("@getNewLotInsert", "@type", lotno, typeClass);
                _sql.Execute3();
                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    //3rd
                    _sql.commandTraxParam3("INSERT INTO ASYS_REMOutsource_detail (RefLotno, Lotno, RefallBarcode, ALLbarcode, PTN, PTNbarcode," +
                " BranchCode, BranchName, Loanvalue, LoanDate, RefItemcode, Itemcode, BranchItemDesc, " + "RefQty, Qty, KaratGrading, CaratSize, Weight, ActionClass, SortCode, ALL_desc, ALL_karat, ALL_carat, " +
                            "ALL_Cost, ALL_weight, " + "Currency, PhotoName, Price_desc, Price_karat, Price_weight, Price_carat, AppraiseValue, ALL_price, Cellular_cost, " +
                            "Watch_cost, Repair_cost, Cleaning_cost, Gold_cost, Mount_cost, YG_cost, WG_cost, MaturityDate, ExpiryDate, Receivedate, Receiver, status) " +
                            "SELECT TOP 1 '" + lotno + "' as reflotno, '" + lotno + "' as lotno, RefALLBarcode, refALLbarcode as ALLBarcode, PTN, PTNBarcode, '" + branchCode + "' as  BranchCode, '" + branchName + "' as BranchName, " +
                            "Loanvalue, LoanDate, RefItemcode, ItemCode , BranchItemDesc, RefQty, Qty, KaratGrading, CaratSize, Weight,'" + comboValue + "', SortCode," +
                            "price_desc as all_desc,price_karat as all_karat,price_carat as all_carat,ALL_Cost,price_weight as all_weight,  Currency," +
                            "PhotoName, Price_desc, Price_karat,  Price_weight,Price_carat,AppraiseValue, ALL_price, Cellular_cost" +
                                        ", Watch_cost, Repair_cost,Cleaning_cost, Gold_cost, Mount_cost, YG_cost, WG_cost, MaturityDate, ExpiryDate, getdate(),'" + receiver + "', 'COSTED' as status " +
                                        "FROM dbo.ASYS_DISTRI_detail WHERE refallbarcode ='" + dgEntry[i][0] + "' AND status = 'RELEASED' ORDER BY ID DESC");
                    _sql.Execute3();
                    //4th
                    _sql.commandTraxParam3("INSERT INTO ASYS_BarcodeHistory (lotno,RefALLBarcode, allbarcode,itemcode,[description]," +
                "karat,carat,weight,currency,price,cost,empname,trandate,costcenter,status) SELECT TOP 1 '" + lotno + "' as lotno, RefALLBarcode,RefALLBarcode as " +
                "allbarcode,  RefItemcode, Price_desc, Price_karat, Price_carat,Price_weight, Currency, ALL_price, ALL_Cost,'" + userLog + "' as empname,getdate() as trandate,'REM' as costcenter,'RETURNED' as status " +
                "FROM dbo.ASYS_DISTRI_detail  WHERE refallbarcode ='" + dgEntry[i][0] + "' AND status = 'RELEASED' ORDER BY ID DESC");
                    _sql.Execute3();
                    //5th
                    _sql.commandTraxParam3("UPDATE [REMS].dbo.ASYS_Distri_detail SET status = 'RETURNED' WHERE refallbarcode ='" + dgEntry[i][0] + "' AND status = 'RELEASED'");
                    _sql.Execute3();
                    //6th
                    _sql.commandTraxParam3("UPDATE [REMS].dbo.ASYS_Returned_Header SET HOStatus = 1 WHERE stocksreturnnumber ='" + controlNum + "' AND bedrnr ='" + branchCode + "'");
                    _sql.Execute3();
                }
                log.Info("saveReturnsALLByBarcode: " + respMessage(1) + " | lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax3();
                //_sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnsALLByBarcode: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveReturnsALLByBarcode: " + respMessage(0) + ex.Message };
            }

        }

        //insert return by control number
        //new
        [WebMethod]
        public RespALLResult saveReturnsALLByControlNumber(string lotno, string branchCode, string branchName, string comboValue, string receiver,
            string controlNum, string[][] dgEntry, string userLog, string typeClass)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                //new
                //1st
                _sql.commandTraxParam3("UPDATE ASYS_Lotno_Gen SET lotno =@getNewLotUpdate WHERE BusinessCenter='REM'");
                _sql.RetrieveInfoParams2("@getNewLotUpdate", lotno);
                _sql.Execute3();
                //2nd
                _sql.commandTraxParam3("INSERT INTO ASYS_REMOutSource_Header (Lotno, TYPE) VALUES(@getNewLotInsert,@type)");
                _sql.RetrieveInfoParams("@getNewLotInsert", "@type", lotno, typeClass);
                _sql.Execute3();
                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    //3rd
                    _sql.commandTraxParam3("INSERT INTO ASYS_REMOutsource_detail (RefLotno, Lotno, RefallBarcode, ALLbarcode, PTN," +
                " PTNbarcode, BranchCode, BranchName, Loanvalue, LoanDate, RefItemcode, Itemcode, BranchItemDesc, " +
                "RefQty, Qty, KaratGrading, CaratSize, Weight, ActionClass, SortCode, ALL_desc, ALL_karat, ALL_carat, " +
                  "ALL_Cost, ALL_weight, Currency, PhotoName, Price_desc, Price_karat, Price_weight, Price_carat," +
                  " AppraiseValue, ALL_price, Cellular_cost,Watch_cost, Repair_cost, Cleaning_cost, Gold_cost, Mount_cost," +
                  " YG_cost, WG_cost, MaturityDate, ExpiryDate, Receivedate, Receiver, status) " + "SELECT TOP 1 '" + lotno + "' AS reflotno,'" + lotno + "' AS lotno," +
                  " dd.RefALLBarcode, dd.refALLbarcode AS ALLBarcode, dd.PTN, dd.PTNBarcode,'" + branchCode + "' AS BranchCode,'" + branchName + "' AS BranchName, " +
                  "dd.Loanvalue, dd.LoanDate, dd.RefItemcode, dd.ItemCode, dd.BranchItemDesc, dd.RefQty, dd.Qty, dd.KaratGrading, dd.CaratSize, dd.Weight,'" + comboValue + "', dd.SortCode," +
                  "dd.price_desc AS all_desc, dd.price_karat AS all_karat, dd.price_carat AS all_carat, dd.ALL_Cost, dd.price_weight AS all_weight," +
                  " dd.Currency,dd.PhotoName, dd.Price_desc, dd.Price_karat, dd.Price_weight, dd.Price_carat, dd.AppraiseValue, dd.ALL_price," +
                  " dd.Cellular_cost, dd.Watch_cost, dd.Repair_cost,dd.Cleaning_cost, dd.Gold_cost, dd.Mount_cost, dd.YG_cost, dd.WG_cost, dd.MaturityDate, dd.ExpiryDate" +
                                        ", GETDATE(),'" + receiver + "', 'COSTED' as status FROM REMS.DBO.ASYS_DISTRI_Detail dd INNER JOIN REMS.DBO.ASYS_XMLITEMS item ON dd.refallbarcode=item.itemcode " +
                                        "WHERE item.ControlNo ='" + controlNum + "' AND item.ConsignCode='" + branchCode + "' AND dd.RefAllBarcode = '" + dgEntry[i][0] + "'");
                    _sql.Execute3();
                    //4th
                    _sql.commandTraxParam3("INSERT INTO ASYS_BarcodeHistory (lotno,RefALLBarcode, allbarcode,itemcode,[description],karat,carat,weight" +
                ",currency,price,cost,empname,trandate,costcenter,status) SELECT TOP 1 '" + lotno + "' as lotno, RefALLBarcode,RefALLBarcode as allbarcode," +
                " RefItemcode, Price_desc, Price_karat, Price_carat, Price_weight, Currency, ALL_price, ALL_Cost,'" + userLog + "' as empname,getdate()" +
                " as trandate,'REM' as costcenter,'RETURNED' as status FROM dbo.ASYS_DISTRI_detail  WHERE refallbarcode ='" + dgEntry[i][0] + "' AND status = 'RELEASED' ORDER BY ID DESC");
                    _sql.Execute3();
                    //5th
                    _sql.commandTraxParam3("UPDATE [REMS].dbo.ASYS_Distri_detail SET status = 'RETURNED' WHERE refallbarcode ='" + dgEntry[i][0] + "' AND status = 'RELEASED'");
                    _sql.Execute3();
                    //6th
                    _sql.commandTraxParam3("UPDATE [REMS].dbo.ASYS_Returned_Header SET HOStatus = 1 WHERE stocksreturnnumber ='" + controlNum + "' AND bedrnr ='" + branchCode + "'");
                    _sql.Execute3();
                    //7th
                    _sql.commandTraxParam3("UPDATE [REMS].dbo.ASYS_XMLItems SET ReturnREMStatus = 1 WHERE" +
                        " ControlNo ='" + controlNum + "' AND ConsignCode ='" + branchCode + "' AND ConsignName ='" + branchName + "' AND " +
                        "ItemCode ='" + dgEntry[i][0] + "' AND ReturnStatus=1 AND ReturnREMStatus=0");
                    _sql.Execute3();
                }

                 log.Info("saveReturnsALLByControlNumber: " + respMessage(1) + " | lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax3();
                //_sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnsALLByControlNumber: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveReturnsALLByControlNumber: " + respMessage(0) + ex.Message };
            }
        }
        //getbarcodeToReturn
        [WebMethod]
        public RespALLResult saveReturnSelect_ASYS_ConsignHeader(string allBarcode)
        {
            var models = new saveReturnSelect_ASYS_ConsignHeaderModels();
            var _sql = new Connection();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("SELECT TOP 1 b.Lotno, b.ConsignCode, b.ConsignName, " +
                     "a.ALLBarcode as refallbarcode, c.PTN, c.RefItemcode, c.RefQty, " +
                     "a.Description as [desc], a.Karat, a.Carat, a.Price as cost, a.Grams as wt, " +
                     "c.Status FROM dbo.ASYS_Consign_Detail as a INNER JOIN " +
                     "dbo.ASYS_Consign_header as b ON a.Lotno = b.Lotno INNER JOIN " +
                     "dbo.ASYS_DISTRI_detail as c ON a.ALLBarcode = c.RefALLBarcode " +
                     "WHERE a.ALLBarcode = '" + allBarcode + "' AND c.Status = 'RELEASED' ORDER BY b.[ConsignDate] DESC");
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.itemCode = Convert.ToInt32(rdr["refitemcode"].ToString());
                    models.description = rdr["desc"].ToString().ToUpper();
                    models.quantity = Convert.ToInt32(rdr["refqty"].ToString());
                    models.weight = Convert.ToDouble(rdr["wt"].ToString());
                    models.karat = Convert.ToInt32(rdr["karat"].ToString());
                    models.carat = Convert.ToDouble(rdr["carat"].ToString());
                    models.cost = Convert.ToDouble(rdr["cost"].ToString());
                    models.consignName = rdr["consignName"].ToString();
                    log.Info("saveReturnSelect_ASYS_ConsignHeader: " + " | itemCode: " + models.itemCode + " | consignName: " + models.consignName + " | description: " + models.description);
                }

                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelect_ASYS_ConsignHeaderdata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelect_ASYS_ConsignHeader: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //generate barcode with parameter
        [WebMethod]
        public RespALLResult saveReturnEXEC_ASYS_Barcode_Generator(string itemcode, string supplier, string year)
        {
            var models = new saveReturnEXEC_ASYS_Barcode_GeneratorModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("ASYS_Barcode_Generator");
                _sql.SaveReturnParams("@Pitemcode", "@PItemsource", "@PYear", itemcode, supplier, year);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.maxbarcode = models.IsNull(rdr["maxBarcode"]).Trim();
                    log.Info("saveReturnEXEC_ASYS_Barcode_Generator: " + models.maxbarcode);
                }

                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnEXEC_ASYS_Barcode_Generatordata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnEXEC_ASYS_Barcode_Generator: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveReturnSelectTop1_bedrnr_bedrnm(string branchName)
        {
            var models = new saveReturnSelectTop1_bedrnr_bedrnmModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("SELECT TOP 1 bedrnr, bedrnm FROM " + Connection.bedryf2 + " WHERE bedrnm LIKE '" + branchName + "'");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchCode = models.IsNull(rdr["bedrnr"]).Trim();
                    models.branchName = models.IsNull(rdr["bedrnm"]).Trim();
                    log.Info("saveReturnSelectTop1_bedrnr_bedrnm: " + models.branchCode + " | " + models.branchName);
                }
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelectTop1_bedrnr_bedrnmdata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelectTop1_bedrnr_bedrnm: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveReturnSelect_CustomerDetails(string branchName)
        {
            var models = new saveReturnSelect_CustomerDetailsModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("SELECT customerid, customerfname, customerlname FROM ASYS_CreateCustInfo WHERE CustomerFname + ' ' + " +
                           "CustomerLname LIKE '%@branchName%'");
                _sql.RetrieveInfoParam2("@branchName", branchName);
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchCode = models.IsNull(rdr["customerid"]).Trim();
                    models.branchName = models.IsNull(rdr["customerfname"]).Trim() + " " + models.IsNull(rdr["CustomerLname"]).Trim();
                    log.Info("saveReturnSelect_CustomerDetails: " + models.branchCode + " | " + models.branchName);
                }
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelect_CustomerDetailsdata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelect_CustomerDetails: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveReturnSelect_bedrnr_bedrnm(string branchCode)
        {
            var models = new saveReturnSelect_bedrnr_bedrnmModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("SELECT bedrnr, bedrnm FROM " + Connection.bedryf2 + " WHERE bedrnr='" + branchCode + "' ");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchName = models.IsNull(rdr["bedrnm"]).Trim();
                    log.Info("saveReturnSelect_bedrnr_bedrnm: " + models.branchName);
                }
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelect_bedrnr_bedrnmdata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelect_bedrnr_bedrnm: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveReturnSelect_CustomerDetails2(string branchCode)
        {
            var models = new saveReturnSelect_CustomerDetails2Models();

            var _sql = new Connection();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("SELECT customerid, customerfname, customerlname FROM ASYS_CreateCustInfo WHERE customerid=@branchCode");
                _sql.RetrieveInfoParam2("@branchCode", branchCode);
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchName = models.IsNull(rdr["customerfname"]).Trim() + " " + models.IsNull(rdr["CustomerLname"]).Trim();
                    log.Info("saveReturnSelect_CustomerDetails2: " + models.branchName);
                }
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelect_CustomerDetails2data = models };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelect_CustomerDetails2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveReturnSelect_ItemDetails(string branchCode, string branchName, string controlNumber)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.saveReturnSelect_ItemDetailsdata = new saveReturnSelect_ItemDetailsModels();
            List.saveReturnSelect_ItemDetailsdata.allbarcode = new List<string>();
            List.saveReturnSelect_ItemDetailsdata.itemCode = new List<int>();
            List.saveReturnSelect_ItemDetailsdata.description = new List<string>();
            List.saveReturnSelect_ItemDetailsdata.quantity = new List<int>();
            List.saveReturnSelect_ItemDetailsdata.weight = new List<double>();
            List.saveReturnSelect_ItemDetailsdata.karat = new List<int>();
            List.saveReturnSelect_ItemDetailsdata.carat = new List<double>();
            List.saveReturnSelect_ItemDetailsdata.price = new List<double>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("SELECT item.Lotno, item.ConsignCode, item.ConsignName, dd.refItemcode, dd.RefQty, dd.Status, " +
                    "dd.all_desc AS [desc], dd.all_karat, dd.all_carat, dd.all_price, dd.all_weight, dd.refallbarcode FROM REMS.DBO.ASYS_XMLITEMS item " +
                    "INNER JOIN REMS.DBO.ASYS_DISTRI_detail dd ON item.itemcode = dd.refALLBarcode WHERE dd.Status='RELEASED' AND" +
                    " item.ConsignCode =@branchCode AND item.ConsignName =@branchName AND item.ReturnStatus = 1 AND item.ReturnREMStatus = 0" +
                    " AND item.ControlNo =@controlNumber ORDER BY dd.ALLBarcode ASC");
                _sql.SaveReturnParams("@branchCode", "@branchName", "@controlNumber", branchCode, branchName, controlNumber);
                rdr = _sql.ExecuteDr3();
                while (rdr.Read())
                {
                    List.saveReturnSelect_ItemDetailsdata.allbarcode.Add(rdr["refallbarcode"].ToString().ToUpper());
                    List.saveReturnSelect_ItemDetailsdata.itemCode.Add(Convert.ToInt32(rdr["refitemcode"].ToString().ToUpper()));
                    List.saveReturnSelect_ItemDetailsdata.description.Add(rdr["desc"].ToString().ToUpper());
                    List.saveReturnSelect_ItemDetailsdata.quantity.Add(Convert.ToInt32(rdr["refqty"].ToString().ToUpper()));
                    List.saveReturnSelect_ItemDetailsdata.weight.Add(Convert.ToDouble(rdr["all_weight"].ToString().ToUpper()));
                    List.saveReturnSelect_ItemDetailsdata.karat.Add(Convert.ToInt32(rdr["all_karat"].ToString().ToUpper()));
                    List.saveReturnSelect_ItemDetailsdata.carat.Add(Convert.ToDouble(rdr["all_carat"].ToString().ToUpper()));
                    List.saveReturnSelect_ItemDetailsdata.price.Add(Convert.ToDouble(rdr["all_price"].ToString()));
                }
                log.Info("saveReturnSelect_ItemDetails: " + List.saveReturnSelect_ItemDetailsdata.allbarcode);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveReturnSelect_ItemDetailsdata = List.saveReturnSelect_ItemDetailsdata };
            }
            catch (Exception ex)
            {
                log.Error("saveReturnSelect_ItemDetails: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //OutSource AddEdit
        [WebMethod]
        public RespALLResult saveOutSourceAddEditSelectReturn_ASYS_ConsignHeader(
            string allbarcode, string consignCode, string consignName)
        {
            var models = new saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("SELECT dbo.ASYS_Consign_header.Lotno, dbo.ASYS_Consign_header.ConsignCode," +
            " dbo.ASYS_Consign_header.ConsignName,dbo.ASYS_Consign_Detail.ALLBarcode as refallbarcode, dbo.ASYS_DISTRI_detail.PTN, " +
            "dbo.ASYS_DISTRI_detail.RefItemcode, dbo.ASYS_DISTRI_detail.RefQty,dbo.ASYS_Consign_Detail.Description as [desc], " +
            "dbo.ASYS_Consign_Detail.Karat, dbo.ASYS_Consign_Detail.Carat, dbo.ASYS_Consign_Detail.Price as cost, " +
            "dbo.ASYS_Consign_Detail.Grams as wt,dbo.ASYS_DISTRI_detail.Status FROM dbo.ASYS_Consign_Detail INNER JOIN" +
            " dbo.ASYS_Consign_header ON dbo.ASYS_Consign_Detail.Lotno = dbo.ASYS_Consign_header.Lotno INNER JOIN dbo.ASYS_DISTRI_detail" +
            " ON dbo.ASYS_Consign_Detail.ALLBarcode = dbo.ASYS_DISTRI_detail.RefALLBarcode WHERE   dbo.ASYS_Consign_Detail.ALLBarcode =@allbarcode" +
            " and dbo.ASYS_Consign_header.ConsignCode =@consignCode and dbo.ASYS_Consign_header.ConsignName =@consignName");
                _sql.SaveReturnParams("@allbarcode", "@consignCode", "@consignName", allbarcode, consignCode, consignName);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.allreturns = false;
                    log.Info("saveOutSourceAddEditSelectReturn_ASYS_ConsignHeader: allreturns: " + models.allreturns);
                }
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderdata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceAddEditSelectReturn_ASYS_ConsignHeader: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveOutSourceAddEditSelect_ASYS_REM_Detail(string barcode, string barcode2)
        {
            var models = new saveOutSourceAddEditSelect_ASYS_REM_DetailModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select refallbarcode from ASYS_REM_Detail  where refallbarcode =@barcode" +
            " union all Select refallbarcode from ASYS_REMOutSOurce_Detail  where refallbarcode =@barcode2");
                _sql.RetrieveInfoParams("@barcode", "@barcode2", barcode, barcode2);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.allOutsource = true;
                    log.Info("saveOutSourceAddEditSelect_ASYS_REM_Detail: allOutsource: " + models.allOutsource);
                }
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveOutSourceAddEditSelect_ASYS_REM_Detaildata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceAddEditSelect_ASYS_REM_Detail: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveOutSourceAddEditSelect_ASYS_REMOutSource_Header(string LotNumber)
        {
            var models = new saveOutSourceAddEditSelect_ASYS_REMOutSource_HeaderModels();

            var _sql = new Connection();
            var List = new RespALLResult();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata = new saveOutSourceAddEditSelect_ASYS_REMOutSource_HeaderModels();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.allbarcode = new List<string>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.itemCode = new List<int>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.description = new List<string>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.quantity = new List<int>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.weight = new List<double>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.karat = new List<int>();
            List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.carat = new List<double>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("SELECT dbo.ASYS_REMOutSource_header.Lotno," +
            " dbo.ASYS_REMOutSource_detail.ReceiveDate, dbo.ASYS_REMOutSource_detail.Receiver,dbo.ASYS_REMOutSource_detail.RefallBarcode," +
            "dbo.ASYS_REMOutSource_detail.branchcode,dbo.ASYS_REMOutSource_detail.branchname, dbo.ASYS_REMOutSource_detail.RefItemcode," +
            "dbo.ASYS_REMOutSource_detail.BranchItemDesc,dbo.ASYS_REMOutSource_detail.RefQty, dbo.ASYS_REMOutSource_detail.KaratGrading," +
            " dbo.ASYS_REMOutSource_detail.CaratSize,dbo.ASYS_REMOutSource_detail.Weight FROM dbo.ASYS_REMOutSource_detail" +
            " INNER JOIN dbo.ASYS_REMOutSource_header  ON dbo.ASYS_REMOutSource_detail.Lotno = dbo.ASYS_REMOutSource_header.Lotno where" +
            " dbo.ASYS_REMOutSource_header.Lotno =@searchLot order by dbo.asys_remoutsource_detail.refallbarcode");
                _sql.RetrieveInfoParams2("@searchLot", LotNumber);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        models.receiver = models.IsNull(rdr["Receiver"]).ToString().ToUpper();
                        models.date = string.IsNullOrEmpty(rdr["Receivedate"].ToString()) ? null : Convert.ToDateTime(rdr["Receivedate"]).ToString("MMMM dd, yyyy").Trim();
                        models.branchCode = models.IsNull(rdr["branchcode"]);
                        models.branchName = models.IsNull(rdr["branchname"].ToString().ToUpper());
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.allbarcode.Add(models.IsNull(rdr["refallbarcode"]).ToUpper().Trim());
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.itemCode.Add(Convert.ToInt32(models.IsNull(rdr["refitemcode"]).ToUpper().Trim()));
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.description.Add(models.IsNull(rdr["BranchItemDesc"]).ToUpper().Trim());
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.quantity.Add(Convert.ToInt32(models.IsNull(rdr["refqty"]).ToUpper().Trim()));
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.weight.Add(Convert.ToDouble(models.IsNull(rdr["weight"]).ToUpper().Trim()));
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.karat.Add(Convert.ToInt32(models.IsNull(rdr["KaratGrading"]).ToUpper().Trim()));
                        List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata.carat.Add(Convert.ToDouble(models.IsNull(rdr["CaratSize"]).ToUpper().Trim()));
                    }
                    log.Info("saveOutSourceAddEditSelect_ASYS_REMOutSource_Header: " + models.receiver + " | " + models.date + " | " + models.branchCode + " | " + models.branchName);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata = List.saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata, saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata2 = models };
                }
                else
                {
                    log.Info("saveOutSourceAddEditSelect_ASYS_REMOutSource_Header: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceAddEditSelect_ASYS_REMOutSource_Header: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //new saving AddEdit OutSource
        [WebMethod]
        public RespALLResult saveOutSourceAddEditALL(string lotno, string[][] dgEntry, string actionClass, string userLog, string receiver)
        {
            var _sql = new Connection();
            status = "RECEIVED";
            sortCode = "O";
            Dept = "REM";
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    if (dgEntry[i] != null)
                    {
                        //1st
                        _sql.commandTraxParam3("Insert INTO ASYS_REMOutSource_detail (Reflotno,lotno,refallbarcode,allbarcode," +
                    "refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc,all_karat,all_carat," +
                    "all_weight,price_desc,price_karat,price_weight,price_carat,sortdate,sortername,receivedate, receiver,status)" +
                    " VALUES ('" + lotno + "','" + lotno + "','" + dgEntry[i][0] + "','" + dgEntry[i][0] + "','" + dgEntry[i][1] + "','" + dgEntry[i][1] +
                    "','" + dgEntry[i][2] + "','" + dgEntry[i][3] + "','" + dgEntry[i][3] + "','" + dgEntry[i][5] + "','" + dgEntry[i][6] +
                    "','" + dgEntry[i][4] + "','" + actionClass + "','" + sortCode + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] +
                    "','" + dgEntry[i][6] + "','" + dgEntry[i][4] + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] + "','" + dgEntry[i][4] +
                    "','" + dgEntry[i][6] + "',getdate(),'" + userLog + "',getdate(),'" + receiver + "','" + status + "')");
                        _sql.Execute3();
                        //2nd
                        _sql.commandTraxParam3("Insert INTO ASYS_BarcodeHistory (lotno,refallbarcode,allbarcode,itemcode,[description]," +
                    "karat,carat,weight,empname,trandate,costcenter,status) VALUES('" + lotno + "','" + dgEntry[i][0] + "','" + dgEntry[i][0] +
                    "','" + dgEntry[i][1] + "','" + dgEntry[i][2] + "','" + dgEntry[i][5] + "','" + dgEntry[i][6] + "','" + dgEntry[i][4] +
                    "','" + userLog + "',getdate(),'" + Dept + "','" + status + "')");
                        _sql.Execute3();
                    }
                    else
                    {
                        break;
                    }

                }
                log.Info("saveOutSourceAddEditALL: " + respMessage(1) + " | lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceAddEditALL: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveOutSourceAddEditALL: " + respMessage(0) + ex.Message };
            }
        }
        //new saving AddEdit Returns
        [WebMethod]
        public RespALLResult saveOutSourceReturnsAddEditALL(string lotno, string branchCode, string branchName, string actionClass,
            string userLog, string[][] ListView)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                for (int i = 0; i <= ListView.Count() - 1; i++)
                {
                    //1st
                    _sql.commandTraxParam3("INSERT INTO ASYS_REMOutsource_detail (RefLotno, Lotno, RefallBarcode, ALLbarcode, PTN, PTNbarcode," +
                    " BranchCode, BranchName, Loanvalue, LoanDate, RefItemcode, Itemcode, BranchItemDesc,RefQty, Qty, KaratGrading, CaratSize, Weight, ActionClass, SortCode, ALL_desc, ALL_karat, ALL_carat, " +
                                "ALL_Cost, ALL_weight, Currency, PhotoName, Price_desc, Price_karat, Price_weight, Price_carat, AppraiseValue, ALL_price, Cellular_cost, " +
                                "Watch_cost, Repair_cost, Cleaning_cost, Gold_cost, Mount_cost, YG_cost, WG_cost, MaturityDate, ExpiryDate, Receivedate, Receiver, status) " +
                                "SELECT TOP 1 '" + lotno + "' as reflotno, '" + lotno + "' as lotno, RefALLBarcode, refALLbarcode as ALLBarcode, PTN, PTNBarcode, '"
                                + branchCode + "' as  BranchCode, '" + branchName + "' as BranchName, " +
                                "Loanvalue, LoanDate, RefItemcode, ItemCode , BranchItemDesc, RefQty, Qty, KaratGrading, CaratSize, Weight, '" + actionClass + "', SortCode," +
                                "price_desc as all_desc,price_karat as all_karat,price_carat as all_carat,ALL_Cost,price_weight as all_weight,  Currency," +
                                "PhotoName, Price_desc, Price_karat,  Price_weight,Price_carat,AppraiseValue, ALL_price, Cellular_cost" +
                                            ", Watch_cost, Repair_cost,Cleaning_cost, Gold_cost, Mount_cost, YG_cost, WG_cost, MaturityDate, ExpiryDate, getdate(), '" + userLog + "', 'COSTED' as status " +
                                            "FROM dbo.ASYS_DISTRI_detail WHERE refallbarcode ='" + ListView[i][0] + "' AND status = 'RELEASED' ORDER BY ID DESC");
                    _sql.Execute3();
                    //2nd
                    _sql.commandTraxParam3("insert into ASYS_BarcodeHistory (lotno,refallbarcode,allbarcode,itemcode,[description],karat,carat," +
            "weight,currency,price,cost,empname,trandate,costcenter,status) SELECT '" + lotno + "' as lotno, RefALLBarcode,RefALLBarcode " +
            "as allbarcode, RefItemcode, Price_desc, Price_karat, Price_carat,Price_weight, Currency, ALL_price, ALL_Cost,'" + userLog + "' as empname," +
            "getdate() as trandate,'REM' as costcenter,'RETURNED' as status FROM dbo.ASYS_DISTRI_detail  where refallbarcode ='" + ListView[i][0] + "' and status = 'RELEASED'");
                    _sql.Execute3();
                    //3rd
                    _sql.commandTraxParam3("Update ASYS_Distri_detail  set status = 'RETURNED' where refallbarcode ='" + ListView[i][0] + "' and status = 'RELEASED'");
                    _sql.Execute3();
                };

                log.Info("saveOutSourceReturnsAddEditALL: lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceReturnsAddEditALL: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveOutSourceReturnsAddEditALL: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtail(string barcode, string barcode2)
        {
            var models = new saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtailModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select refallbarcode from ASYS_REM_DEtail  where refallbarcode = @barcode1" +
            " union all select refallbarcode from ASYS_REMOutSource_detail  where refallbarcode = @barcode2");
                _sql.RetrieveInfoParams("@barcode1", "@barcode2", barcode, barcode2);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.message = "Allbarcode already receive.";
                    log.Info("saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtail: " + models.message);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = "Exists!", saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtaildata = models };
                }
                else
                {
                    log.Info("saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtail: Not Exists!");
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = "2", responseMsg = respMessage(1) };
                }

            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtail: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_Detail(string barcode)
        {
            var models = new saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_DetailModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select refallbarcode from ASYS_DISTRI_detail  where refallbarcode =@barcode and status = 'returned'");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.message = "Allbarcode already receive";
                    log.Info("saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_Detail: " + models.message);
                }
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_Detaildata = models };
            }
            catch (Exception ex)
            {
                log.Error("saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_Detail: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceAddEditReturn_Select_ASYS_Distri_Detail(string barcode)
        {
            var models = new OutSourceAddEditReturn_Select_ASYS_Distri_DetailModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4("Select refallbarcode,price_desc,refitemcode,price_karat,price_weight,price_carat from ASYS_Distri_detail  where refallbarcode =@barcode and status = 'RELEASED'");
                _sql.RetrieveInfoParamss2("@barcode", barcode);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.priceDescription = rdr["price_desc"].ToString();
                    models.itemCode = rdr["refitemcode"].ToString();
                    models.priceKarat = rdr["price_karat"].ToString();
                    models.priceCarat = rdr["price_carat"].ToString();
                    models.priceWeight = rdr["price_weight"].ToString();
                    log.Info("OutSourceAddEditReturn_Select_ASYS_Distri_Detail: barcode: " + barcode + " | priceDescription: " + models.priceDescription);
                }
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceAddEditReturn_Select_ASYS_Distri_Detaildata = models };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceAddEditReturn_Select_ASYS_Distri_Detail: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_Bedryf(string branchCode)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_Bedryfdata = new OutSourceSearch_BedryfModels();
            List.OutSourceSearch_Bedryfdata.customerCode = new List<string>();
            List.OutSourceSearch_Bedryfdata.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnr as code,bedrnm as [name] from " + Connection.bedryf2 + " where bedrnr=@branchCode");
                _sql.RetrieveInfoParam2("@branchCode", branchCode);
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_Bedryfdata.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_Bedryfdata.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_Bedryf: " + List.OutSourceSearch_Bedryfdata.customerCode + " | " + List.OutSourceSearch_Bedryfdata.customerName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_Bedryfdata = List.OutSourceSearch_Bedryfdata };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_Bedryf: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_Bedryf2(string branchName)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_Bedryf2data = new OutSourceSearch_BedryfModels2();
            List.OutSourceSearch_Bedryf2data.customerCode = new List<string>();
            List.OutSourceSearch_Bedryf2data.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnr as code,bedrnm as [name]  from " + Connection.bedryf2 + " where bedrnm like '%" + branchName + "%'");
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_Bedryf2data.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_Bedryf2data.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_Bedryf2: " + List.OutSourceSearch_Bedryf2data.customerCode + " | " + List.OutSourceSearch_Bedryf2data.customerName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_Bedryf2data = List.OutSourceSearch_Bedryf2data };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_Bedryf2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_Bedryf3()
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_Bedryf3data = new OutSourceSearch_BedryfModels3();
            List.OutSourceSearch_Bedryf3data.customerCode = new List<string>();
            List.OutSourceSearch_Bedryf3data.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnr as code,bedrnm as [name]  from " + Connection.bedryf2 + "");
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_Bedryf3data.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_Bedryf3data.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_Bedryf3: " + List.OutSourceSearch_Bedryf3data.customerCode + " | " + List.OutSourceSearch_Bedryf3data.customerName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_Bedryf3data = List.OutSourceSearch_Bedryf3data };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_Bedryf3: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_CustDetails_ASYS_CreateCustInfo(string branchCode)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata = new OutSourceSearch_CustDetails_ASYS_CreateCustInfoModels();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata.customerCode = new List<string>();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select customerid as code, RTRIM(customerfname) + ' ' + RTRIM(customerlname) as [name] from ASYS_CreateCustInfo where customerid= @branchCode ORDER BY [name]");
                _sql.RetrieveInfoParam2("@branchCode", branchCode);
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_CustDetails_ASYS_CreateCustInfo: " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata.customerCode + " | " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_CustDetails_ASYS_CreateCustInfodata = List.OutSourceSearch_CustDetails_ASYS_CreateCustInfodata };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_CustDetails_ASYS_CreateCustInfo: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_CustDetails_ASYS_CreateCustInfo2(string branchName)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data = new OutSourceSearch_CustDetails_ASYS_CreateCustInfo2Models();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerCode = new List<string>();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select  customerid as code, RTRIM(customerfname) + ' ' + RTRIM(customerlname) as [name] from ASYS_CreateCustInfo where customerlname like '%@branchName%' ORDER BY [name]");
                _sql.RetrieveInfoParam2("@branchName", branchName);
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_CustDetails_ASYS_CreateCustInfo2: " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerCode + " | " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data.customerName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data = List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_CustDetails_ASYS_CreateCustInfo2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceSearch_CustDetails_ASYS_CreateCustInfo3()
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data = new OutSourceSearch_CustDetails_ASYS_CreateCustInfo3Models();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerCode = new List<string>();
            List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select customerid as code, RTRIM(customerfname) + ' ' + RTRIM(customerlname) as [name] from ASYS_CreateCustInfo ORDER BY [name]");
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerCode.Add(rdr["code"].ToString().ToUpper().Trim());
                    List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerName.Add(rdr["name"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceSearch_CustDetails_ASYS_CreateCustInfo3: " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerCode + " | " + List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data.customerName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data = List.OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceSearch_CustDetails_ASYS_CreateCustInfo3: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceDisplay_Bedryf()
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceDisplay_Bedryfdata = new OutSourceDisplay_BedryfModels();
            List.OutSourceDisplay_Bedryfdata.bedrnm = new List<string>();
            List.OutSourceDisplay_Bedryfdata.bedrnr = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select bedrnr,bedrnm from " + Connection.bedryf2 + "");
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceDisplay_Bedryfdata.bedrnm.Add(rdr["bedrnr"].ToString().ToUpper().Trim());
                    List.OutSourceDisplay_Bedryfdata.bedrnr.Add(rdr["bedrnm"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceDisplay_Bedryf: " + List.OutSourceDisplay_Bedryfdata.bedrnm + " | " + List.OutSourceDisplay_Bedryfdata.bedrnr);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceDisplay_Bedryfdata = List.OutSourceDisplay_Bedryfdata };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceDisplay_Bedryf: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OutSourceDisplay_Bedryf2()
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.OutSourceDisplay_Bedryf2data = new OutSourceDisplay_BedryfModels2();
            List.OutSourceDisplay_Bedryf2data.custID = new List<string>();
            List.OutSourceDisplay_Bedryf2data.custName = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select customerid,RTRIM(customerfname) + ' ' + RTRIM(customerlname) as CustName from ASYS_CreateCustInfo ORDER BY customerfname");
                rdr = _sql.ExecuteDr();
                while (rdr.Read() == true)
                {
                    List.OutSourceDisplay_Bedryf2data.custID.Add(rdr["CustomerID"].ToString().ToUpper().Trim());
                    List.OutSourceDisplay_Bedryf2data.custName.Add(rdr["CustName"].ToString().ToUpper().Trim());
                }
                log.Info("OutSourceDisplay_Bedryf2: " + List.OutSourceDisplay_Bedryf2data.custID + " | " + List.OutSourceDisplay_Bedryf2data.custName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceDisplay_Bedryf2data = List.OutSourceDisplay_Bedryf2data };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceDisplay_Bedryf2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult Search_LotNumber(string query)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.Search_LotNumberdata = new Search_LotNumberModels();
            List.Search_LotNumberdata.employeeNames = new List<string>();
            List.Search_LotNumberdata.lotNumbers = new List<string>();
            List.Search_LotNumberdata.receivedDates = new List<string>();
            List.Search_LotNumberdata.releasedDates = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(query);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        if (rdr["EmpName1"].ToString().Length > 0)
                        {
                            List.Search_LotNumberdata.employeeNames.Add(rdr["EmpName1"].ToString().ToUpper().Trim());
                        }
                        else
                        {
                            List.Search_LotNumberdata.employeeNames.Add(rdr["EmpName1"].ToString().ToUpper().Trim());
                        }
                        if (query.Contains("releasedate"))
                        {
                            List.Search_LotNumberdata.releasedDates.Add(rdr["releasedate"].ToString().ToUpper().Trim());
                            List.Search_LotNumberdata.lotNumbers.Add(rdr["LotNum"].ToString().ToUpper().Trim());
                        }
                        else
                        {
                            List.Search_LotNumberdata.receivedDates.Add(rdr["receivedDate"].ToString().ToUpper().Trim());
                            List.Search_LotNumberdata.lotNumbers.Add(rdr["LotNum"].ToString().ToUpper().Trim());
                        }
                    }
                    log.Info("Search_LotNumber: " + respMessage(1));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), Search_LotNumberdata = List.Search_LotNumberdata };
                }
                else
                {
                    log.Info("Search_LotNumber: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("Search_LotNumber: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult UnReceivedReportByBranchCode(string DB, string branchCode)
        {
            var models = new UnReceivedReportByBranchCodeModels();

            var _sql = new Connection();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo." + Connection.bedryf2 + DB + " where bedrnr=" + branchCode + "");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchName = rdr["bedrnm"].ToString().Trim();
                }
                log.Info("UnReceivedReportByBranchCode: " + models.branchName);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), UnReceivedReportByBranchCodedata = models };
            }
            catch (Exception ex)
            {
                log.Error("UnReceivedReportByBranchCode: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult UnReceivedReportByBranchName(string branchName)
        {
            var models = new UnReceivedReportByBranchNameModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm, bedrnr from " + Connection.bedryf2 + " where bedrnm like '%" + branchName + "%'");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchName = rdr["bedrnm"].ToString().Trim();
                    models.branchCode = rdr["bedrnr"].ToString().Trim();
                }
                log.Info("UnReceivedReportByBranchName: " + models.branchName + " | " + models.branchCode);
                _sql.CloseDr();
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), UnReceivedReportByBranchNamedata = models };
            }
            catch (Exception ex)
            {
                log.Error("UnReceivedReportByBranchName: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        #region REPORTS OUTSOURCE
        [WebMethod]
        public RespALLResult LoadOutSourceREPORT(string lotno, string type)
        {

            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.sp = new List<OutSourceRPTList>();
            List.ReportData.sp2 = new List<ReturnsRPTList>();
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                if (type == "OUTSOURCE")
                {
                    _sql.commandExeStoredParam3("ASYS_REMOutSource_rpt");
                    _sql.RetrieveInfoParams2("@Plotno", lotno);
                    rdr = _sql.ExecuteDr3();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var s = new OutSourceRPTList();
                            s.Lotno = rdr["Lotno"].ToString();
                            s.Receiver = rdr["Receiver"].ToString();
                            s.TYPE = rdr["TYPE"].ToString();
                            s.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            s.RefallBarcode = rdr["RefallBarcode"].ToString();
                            s.itemcode = rdr["itemcode"].ToString();
                            s.branchitemdesc = rdr["branchitemdesc"].ToString();
                            s.RefQty = rdr["RefQty"].ToString();
                            s.karatgrading = rdr["karatgrading"].ToString();
                            s.caratsize = string.IsNullOrEmpty(rdr["caratsize"].ToString()) ? 0 : Convert.ToDouble(rdr["caratsize"]);
                            s.SerialNo = rdr["SerialNo"].ToString();
                            s.weight = string.IsNullOrEmpty(rdr["weight"].ToString()) ? 0 : Convert.ToDouble(rdr["weight"]);
                            s.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                            List.ReportData.sp.Add(s);
                        }
                        log.Info("LoadOutSourceREPORT: lotno: " + lotno + " | type: " + type + " | " + List.ReportData.sp);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("LoadOutSourceREPORT: lotno: " + lotno + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
                else
                {
                    _sql.commandExeStoredParam3("ASYS_REMReturn_rpt");
                    _sql.RetrieveInfoParams2("@Plotno", lotno);
                    rdr = _sql.ExecuteDr3();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var s = new ReturnsRPTList();
                            s.Lotno = rdr["Lotno"].ToString();
                            s.Receiver = rdr["Receiver"].ToString();
                            s.BranchName = rdr["BranchName"].ToString();
                            s.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            s.RefallBarcode = rdr["RefallBarcode"].ToString();
                            s.RefItemcode = rdr["RefItemcode"].ToString();
                            s.Price_desc = rdr["Price_desc"].ToString();
                            //s.RefQty = rdr["RefQty"].ToString();
                            s.Price_karat = rdr["Price_karat"].ToString();
                            s.Price_carat = string.IsNullOrEmpty(rdr["Price_carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Price_carat"]);
                            s.SerialNo = rdr["SerialNo"].ToString();
                            s.Price_weight = string.IsNullOrEmpty(rdr["Price_weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Price_weight"]);
                            s.ALL_price = string.IsNullOrEmpty(rdr["ALL_price"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_price"]);
                            List.ReportData.sp2.Add(s);
                        }
                        log.Info("LoadOutSourceREPORT: lotno: " + lotno + " | " + type + " | " + List.ReportData.sp2);
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("LoadOutSourceREPORT: lotno: " + lotno + " | " + respMessage(2));
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("LoadOutSourceREPORT: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadOutSourceREPORT: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadUnReceivedReport(string branchCode, string branchName, string userLog, string DB, string all,
            string none, string branch, string trans, string check)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.sp3 = new List<OutSourceUnreceivedList>();
            List.ReportData.sp4 = new List<OutSourceALLUnReceivedItems>();
            try
            {
                if (trans == "REMRETURN")
                {
                    _sql.Connection3();
                    _sql.OpenConn3();
                    if (check == "rdALL")
                    {
                        _sql.commandExeStoredParam3("ASYS_GetUnreceiveItems");
                        rdr = _sql.ExecuteDr3();
                        if (rdr.HasRows)
                        {

                            while (rdr.Read())
                            {
                                var x = new OutSourceUnreceivedList();
                                x.StockReturnNumber = rdr["StockReturnNumber"].ToString();
                                x.Returndate = string.IsNullOrEmpty(rdr["Returndate"].ToString()) ? null : Convert.ToDateTime(rdr["Returndate"]).ToString("MMMM dd, yyyy").ToUpper();
                                x.bedrnr = rdr["bedrnr"].ToString();
                                x.Itemcode = rdr["Itemcode"].ToString();
                                x.bedrnm = rdr["bedrnm"].ToString();
                                x.Description = rdr["Description"].ToString();
                                x.Karat = rdr["Karat"].ToString();
                                x.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                                x.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                x.SalesPrice = string.IsNullOrEmpty(rdr["SalesPrice"].ToString()) ? 0 : Convert.ToDouble(rdr["SalesPrice"]);
                                x.Qty = Convert.ToInt32(rdr["Qty"]);
                                x.SerialNo = rdr["Qty"].ToString();
                                List.ReportData.sp3.Add(x);
                            }
                            log.Info("LoadUnReceivedReport: trans: " + trans + " | " + List.ReportData.sp3);
                             _sql.CloseDr3();
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData  };
                        }
                        else
                        {
                            log.Info("LoadUnReceivedReport: " + respMessage(2));
                            _sql.CloseDr3();
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                        }
                    }
                    else
                    {
                        _sql.commandExeStoredParam3("ASYS_GetUnreceiveItemsbyBranch");
                        _sql.RetrieveInfoParams("@branchcode", "@branchname", branchCode, branchName);
                        rdr = _sql.ExecuteDr3();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var m = new OutSourceUnreceivedList();
                                m.StockReturnNumber = rdr["StocksReturnNumber"].ToString();
                                m.Returndate = rdr["Returndate"].ToString();
                                m.Itemcode = rdr["Itemcode"].ToString();
                                m.Consigncode = rdr["Consigncode"].ToString();
                                m.Description = rdr["Description"].ToString();
                                m.Karat = rdr["Karat"].ToString();
                                m.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                                m.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                m.SalesPrice = Convert.ToDouble(rdr["SalesPrice"].ToString());
                                m.Qty = Convert.ToInt32(rdr["Qty"]);
                                m.SerialNo = rdr["Qty"].ToString();
                                List.ReportData.sp3.Add(m);
                            }
                            log.Info("LoadUnReceivedReport: trans: " + trans + " | " + List.ReportData.sp3);
                            _sql.CloseDr3();
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                        }
                        else
                        {
                            log.Info("LoadUnReceivedReport: " + respMessage(2));
                            _sql.CloseDr3();
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                        }
                    }
                }
                else
                {
                    _sql.Connection1(DB);
                    _sql.OpenConn1();
                    _sql.commandExeStoredParam1("REMS.dbo.Unrecievedview_All");
                    _sql.RetrieveInfoParam1("@sdb", Connection.sDB);
                    _sql.RetrieveInfoParam1("@username", userLog);
                    if (check == "rdALL")
                    {
                        _sql.RetrieveInfoParams1("@type", "@branchcode", all, none);
                    }
                    else
                    {
                        _sql.RetrieveInfoParams1("@type", "@branchcode", branch, branchCode);
                    }
                    rdr = _sql.ExecuteDr1();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var A = new OutSourceALLUnReceivedItems();
                            // A.ID =Convert.ToInt32(rdr["ID"].ToString());
                            A.BranchCode = rdr["BranchCode"].ToString();
                            A.BranchName = rdr["BranchName"].ToString();
                            A.PTN = rdr["PTN"].ToString();
                            A.Descri = rdr["Descri"].ToString();
                            A.Qty = Convert.ToInt32(rdr["Qty"]);
                            A.Karat = rdr["Karat"].ToString();
                            A.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                            A.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                            A.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                            List.ReportData.sp4.Add(A);
                        }
                        log.Info("LoadUnReceivedReport: trans: " + trans + " | " + List.ReportData.sp4);
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("LoadUnReceivedReport: " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadUnReceivedReport: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadUnReceivedReport: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult VPReleaseOSRPT(string branchCode, int month, int year, string actionClass, string DB, string tran)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.TList = new List<TradeInList>();
            List.ReportData.JList = new List<JewelryList>();
            try
            {
                _sql.Connection1(DB);
                _sql.OpenConn1();
                if (tran == "TRADE")
                {
                    _sql.commandExeStoredParam1("ASYS_TradeInReleasing");
                    _sql.SaveReturnParam21("@PMonth", "@PYear", "@PBranchcode", month, year, branchCode);
                    rdr = _sql.ExecuteDr1();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var f = new TradeInList();
                            f.Division = rdr["Division"].ToString();
                            f.Divisionname = rdr["Divisionname"].ToString();
                            f.Transaction_No = rdr["Transaction_No"].ToString();
                            f.Appraisal_Amount = string.IsNullOrEmpty(rdr["Appraisal_Amount"].ToString()) ? 0 : Convert.ToDouble(rdr["Appraisal_Amount"]);
                            f.Reflotno = rdr["Reflotno"].ToString();
                            f.Itemcode = rdr["Itemcode"].ToString();
                            f.Description = rdr["Description"].ToString();
                            f.Quantity = Convert.ToInt32(rdr["Quantity"]);
                            f.Karat = rdr["Karat"].ToString();
                            f.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                            f.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                            f.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            f.Receiver = rdr["Receiver"].ToString();
                            f.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            f.Releaser = rdr["Releaser"].ToString();
                            f.Status = rdr["Status"].ToString();
                            List.ReportData.TList.Add(f);
                        }
                        log.Info("VPReleaseOSRPT: tran: " + tran + " | " + List.ReportData.TList);
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("VPReleaseOSRPT: " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
                else
                {
                    _sql.commandExeStoredParam1("ASYS_REMRELEASING_JEWELRY");
                    _sql.SaveReturnByControlParam21("@PMONTH", "@PYEAR", "@PBRANCHCODE", "@PACTIONCLASS", month, year, branchCode, actionClass);
                    rdr = _sql.ExecuteDr1();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var D = new JewelryList();
                            D.BranchCode = rdr["BranchCode"].ToString();
                            D.BranchName = rdr["BranchName"].ToString();
                            D.Region = rdr["Region"].ToString();
                            D.Area = rdr["Area"].ToString();
                            D.PTN = rdr["PTN"].ToString();
                            D.BranchItemDesc = rdr["BranchItemDesc"].ToString();
                            D.Qty = Convert.ToInt32(rdr["Qty"]);
                            D.KaratGrading = rdr["KaratGrading"].ToString();
                            D.CaratSize = string.IsNullOrEmpty(rdr["CaratSize"].ToString()) ? 0 : Convert.ToDouble(rdr["CaratSize"]);
                            D.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                            D.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                            D.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                            D.Sortername = rdr["Sortername"].ToString();
                            D.ActionClass = rdr["ActionClass"].ToString();
                            D.SortCode = rdr["SortCode"].ToString();
                            List.ReportData.JList.Add(D);
                        }
                        log.Info("VPReleaseOSRPT: tran: " + tran + " | " + List.ReportData.JList);
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("VPReleaseOSRPT: " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("VPReleaseOSRPT: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "VPReleaseOSRPT: " + respMessage(0) + ex.Message };
            }
        }

        [WebMethod]
        public RespALLResult LoadTradeInReceiving(string branchCode, int month, string year, string DB, string tran, string whichCheck)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.TList = new List<TradeInList>();
            try
            {
                _sql.Connection1(DB);
                _sql.OpenConn1();
                if (tran == "REC")
                {
                    if (whichCheck == "R1")
                    {
                        _sql.commandExeStoredParam1("ASYS_TradeInReceiving");
                        _sql.SaveReturnParam212("@PBranchCode", "@PMonth", "@PYear", branchCode, month, year);// "042",9,"2017");//
                        rdr = _sql.ExecuteDr1();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var c = new TradeInList();
                                c.Division = rdr["Division"].ToString();
                                c.Divisionname = rdr["Divisionname"].ToString();
                                c.Transaction_No = rdr["Transaction_No"].ToString();
                                c.Appraisal_Amount = string.IsNullOrEmpty(rdr["Appraisal_Amount"].ToString()) ? 0 : Convert.ToDouble(rdr["Appraisal_Amount"]);
                                c.Reflotno = rdr["Reflotno"].ToString();
                                c.Itemcode = rdr["Itemcode"].ToString();
                                c.Description = rdr["Description"].ToString();
                                c.Quantity = Convert.ToInt32(rdr["Quantity"]);
                                c.Karat = rdr["Karat"].ToString();
                                c.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                                c.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                c.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                c.TradeMonth = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM yyyy").ToUpper();
                                c.Receiver = rdr["Receiver"].ToString();
                                c.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                c.Releaser = rdr["Releaser"].ToString();
                                c.Status = rdr["Status"].ToString();
                                List.ReportData.TList.Add(c);
                                TradeMonth2 = c.TradeMonth.ToString();
                            }
                            log.Info("LoadTradeInReceiving: " + List.ReportData.TList);
                            _sql.CloseDr1();
                            _sql.CloseConn1();
                            return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData, TradeMonth = TradeMonth2 };
                        }
                        else
                        {
                            log.Info("LoadTradeInReceiving: " + respMessage(2));
                            _sql.CloseDr1();
                            _sql.CloseConn1();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                        }
                    }
                    else
                    {
                        _sql.commandExeStoredParam1("ASYS_TradeInReceiving");
                        _sql.SaveReturnParam212("@PBranchCode", "@PMonth", "@PYear", branchCode, month, year);
                        rdr = _sql.ExecuteDr1();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var xd = new TradeInList();
                                xd.Division = rdr["Division"].ToString();
                                xd.Divisionname = rdr["Divisionname"].ToString();
                                xd.Transaction_No = rdr["Transaction_No"].ToString();
                                xd.Appraisal_Amount = string.IsNullOrEmpty(rdr["Appraisal_Amount"].ToString()) ? 0 : Convert.ToDouble(rdr["Appraisal_Amount"]);
                                xd.Reflotno = rdr["Reflotno"].ToString();
                                xd.Itemcode = rdr["Itemcode"].ToString();
                                xd.Description = rdr["Description"].ToString();
                                xd.Quantity = Convert.ToInt32(rdr["Quantity"]);
                                xd.Karat = rdr["Karat"].ToString();
                                xd.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                                xd.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                xd.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                xd.TradeMonth = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM yyyy").ToUpper();
                                xd.Receiver = rdr["Receiver"].ToString();
                                xd.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                xd.Releaser = rdr["Releaser"].ToString();
                                xd.Status = rdr["Status"].ToString();
                                List.ReportData.TList.Add(xd);
                                TradeMonth2 = xd.TradeMonth.ToString();
                            }
                            log.Info("LoadTradeInReceiving: " + List.ReportData.TList);
                            _sql.CloseDr1();
                            _sql.CloseConn1();
                            return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData, TradeMonth = TradeMonth2 };
                        }
                        else
                        {
                            log.Info("LoadTradeInReceiving: " + respMessage(2));
                            _sql.CloseDr1();
                            _sql.CloseConn1();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                        }
                    }
                }
                else
                {
                    _sql.commandExeStoredParam1("ASYS_TradeInReleasing");
                    _sql.SaveReturnParam212("@PBranchCode", "@PMonth", "@PYear", branchCode, month, year);
                    rdr = _sql.ExecuteDr1();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var mf = new TradeInList();
                            mf.Division = rdr["Division"].ToString();
                            mf.Divisionname = rdr["Divisionname"].ToString();
                            mf.Transaction_No = rdr["Transaction_No"].ToString();
                            mf.Appraisal_Amount = string.IsNullOrEmpty(rdr["Appraisal_Amount"].ToString()) ? 0 : Convert.ToDouble(rdr["Appraisal_Amount"]);
                            mf.Reflotno = rdr["Reflotno"].ToString();
                            mf.Itemcode = rdr["Itemcode"].ToString();
                            mf.Description = rdr["Description"].ToString();
                            mf.Quantity = Convert.ToInt32(rdr["Quantity"]);
                            mf.Karat = rdr["Karat"].ToString();
                            mf.Carat = string.IsNullOrEmpty(rdr["Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["Carat"]);
                            mf.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                            mf.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            mf.Receiver = rdr["Receiver"].ToString();
                            mf.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                            mf.Releaser = rdr["Releaser"].ToString();
                            mf.Status = rdr["Status"].ToString();
                            List.ReportData.TList.Add(mf);
                        }
                        log.Info("LoadTradeInReceiving: " + List.ReportData.TList);
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                    }
                    else
                    {
                        log.Info("LoadTradeInReceiving: " + respMessage(2));
                        _sql.CloseDr1();
                        _sql.CloseConn1();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadTradeInReceiving: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadTradeInReceiving: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadRemRecRPTSmmry(int month, int hyear, string branchCode, string tranType, string recrel, string rdChck,
            string DB, string name, int pyear, string lotno)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.RRRSList = new List<REMREVReportSmmry>();
            List.ReportData.RRRPTList = new List<REMReLRPT>();
            List.ReportData.ASYSREMROSRPTList = new List<ASYSREMROSRPT>();
            List.month = new List<string>();
            List.count = new List<int>();
            //int maxValue = Array
            //int maxIndex = anArray.ToList().IndexOf(maxValue);
            try
            {
                if (DB != "")
                {
                    _sql.Connection1(DB);
                    _sql.OpenConn1();

                    if (tranType == "NotVisible")
                    {
                        if (recrel == "T")
                        {
                            if (rdChck == "r1")
                            {
                                int x, y, z;
                                x = 9; y = 2018; z = 042;
                                _sql.commandExeStoredParam1("ASYS_spREMReceivingReport");
                                _sql.SaveReturnParam21("@HMonth", "@HYear", "@HBranchCode",month, hyear, branchCode);
                                rdr = _sql.ExecuteDr1();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new REMREVReportSmmry();
                                        sma.Lotno = rdr["Lotno"].ToString();
                                        sma.BranchCode = rdr["BranchCode"].ToString();
                                        sma.BranchName = rdr["BranchName"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.Itemcode = rdr["Itemcode"].ToString();
                                        sma.ItemDesc = rdr["ItemDesc"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.Karat = rdr["Karat"].ToString();
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.SortCode = rdr["SortCode"].ToString();
                                        sma.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                        sma.AppraiseValue = string.IsNullOrEmpty(rdr["AppraiseValue"].ToString()) ? 0 : Convert.ToDouble(rdr["AppraiseValue"]);
                                        sma.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                                        sma.Receiver = rdr["Receiver"].ToString();
                                        sma.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.TransDate = string.IsNullOrEmpty(rdr["TransDate"].ToString()) ? null : Convert.ToDateTime(rdr["TransDate"]).ToString("yyyy MMMM");
                                        //sma.pull_out_date = string.IsNullOrEmpty(rdr["pull_out_date"].ToString()) ? null : Convert.ToDateTime(rdr["pull_out_date"]).ToString("yyyy MMMM");
                                        List.ReportData.RRRSList.Add(sma);
                                        List.lotno = sma.Lotno;
                                        
                                        List.month.Add(sma.TransDate);
                                       // List.count.Add(DateTime.ParseExact(List.prendaMonth.ToString(), "yyyy,MMMM", null).Month);
                                    }
                                    List.prendaMonth = List.month[0].ToString();//sma.TransDate;//.ToString("MMMM dd, yyyy")
                                    log.Info("LoadRemRecRPTSmmry: DB: " + DB + " | " + List.ReportData.RRRSList);
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData, lotno = List.lotno, prendaMonth = List.prendaMonth};
                                    
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                            else
                            {
                                _sql.commandExeStoredParam1("ASYS_spREMReceivingReport");
                                _sql.SaveReturnParam21("@HMonth", "@HYear", "@HBranchCode", month, hyear, branchCode);
                                rdr = _sql.ExecuteDr1();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new REMREVReportSmmry();
                                        sma.Lotno = rdr["Lotno"].ToString();
                                        sma.BranchCode = rdr["BranchCode"].ToString();
                                        sma.BranchName = rdr["BranchName"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.Itemcode = rdr["Itemcode"].ToString();
                                        sma.ItemDesc = rdr["ItemDesc"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.Karat = rdr["Karat"].ToString();
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.SortCode = rdr["SortCode"].ToString();
                                        sma.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                        sma.AppraiseValue = string.IsNullOrEmpty(rdr["AppraiseValue"].ToString()) ? 0 : Convert.ToDouble(rdr["AppraiseValue"]);
                                        sma.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                                        sma.Receiver = rdr["Receiver"].ToString();
                                        sma.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.TransDate = string.IsNullOrEmpty(rdr["TransDate"].ToString()) ? null : Convert.ToDateTime(rdr["TransDate"]).ToString("yyyy MMMM").ToUpper();
                                        List.ReportData.RRRSList.Add(sma);
                                        List.lotno = sma.Lotno;

                                        List.prendaMonth = sma.TransDate;
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.RRRSList);
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData, lotno = List.lotno, prendaMonth = List.prendaMonth };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                        else
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(6));
                                _sql.CloseConn1();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                            }
                            else
                            {
                                _sql.commandExeStoredParam1("ASYS_REMRelease_rpt");
                                _sql.SaveReturnParam21("@Pmonth", "@Pyear", "@Pname", month, pyear, name);
                                rdr = _sql.ExecuteDr1();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var s = new REMReLRPT();
                                        s.lotno = rdr["lotno"].ToString();
                                        s.allbarcode = rdr["allbarcode"].ToString();
                                        s.PTN = rdr["PTN"].ToString();
                                        s.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        s.ActionClass = rdr["ActionClass"].ToString();
                                        s.ALL_Desc = rdr["ALL_Desc"].ToString();
                                        s.ALL_Karat = rdr["ALL_Karat"].ToString();
                                        s.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                        s.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                        s.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                        s.month = rdr["month"].ToString().ToUpper();
                                        s.year = rdr["year"].ToString().ToUpper();
                                        s.Releaser = rdr["Releaser"].ToString();
                                        s.releasedate = string.IsNullOrEmpty(rdr["releasedate"].ToString()) ? null : Convert.ToDateTime(rdr["releasedate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        s.Status = rdr["Status"].ToString();
                                        List.ReportData.RRRPTList.Add(s);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.RRRPTList);
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                    }
                    else
                    {
                        if (recrel == "T")
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(5));
                                _sql.CloseConn1();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                            }
                            else
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(5));
                                _sql.CloseConn1();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                            }
                        }
                        else
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(6));
                                _sql.CloseConn1();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                            }
                            else
                            {
                                _sql.commandExeStoredParam1("ASYS_REMReleaseOutsource_rpt");
                                _sql.RetrieveInfoParam1("@Plotno", lotno);
                                rdr = _sql.ExecuteDr1();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new ASYSREMROSRPT();
                                        sma.lotno = rdr["lotno"].ToString();
                                        sma.allbarcode = rdr["allbarcode"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.ALL_Desc = rdr["ALL_Desc"].ToString();
                                        sma.ALL_Karat = rdr["ALL_Karat"].ToString();
                                        sma.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                        sma.SerialNo = rdr["SerialNo"].ToString();
                                        sma.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                        sma.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                        sma.ALL_price = string.IsNullOrEmpty(rdr["ALL_price"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_price"]);
                                        sma.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.Releaser = rdr["Releaser"].ToString();
                                        sma.Status = rdr["Status"].ToString();
                                        List.ReportData.ASYSREMROSRPTList.Add(sma);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.ASYSREMROSRPTList);
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr1();
                                    _sql.CloseConn1();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                    }
                }
                else
                {
                    _sql.Connection3();
                    _sql.OpenConn3();
                    if (tranType == "NotVisible")
                    {
                        if (recrel == "T")
                        {
                            if (rdChck == "r1")
                            {
                                _sql.commandExeStoredParam3("ASYS_spREMReceivingReport");
                                _sql.SaveReturnParam213("@HMonth", "@HYear", "@HBranchCode", month, hyear, branchCode);
                                rdr = _sql.ExecuteDr3();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new REMREVReportSmmry();
                                        sma.Lotno = rdr["Lotno"].ToString();
                                        sma.BranchCode = rdr["BranchCode"].ToString();
                                        sma.BranchName = rdr["BranchName"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.Itemcode = rdr["Itemcode"].ToString();
                                        sma.ItemDesc = rdr["ItemDesc"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.Karat = rdr["Karat"].ToString();
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.SortCode = rdr["SortCode"].ToString();
                                        sma.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                        sma.AppraiseValue = string.IsNullOrEmpty(rdr["AppraiseValue"].ToString()) ? 0 : Convert.ToDouble(rdr["AppraiseValue"]);
                                        sma.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                                        sma.Receiver = rdr["Receiver"].ToString();
                                        sma.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.TransDate = string.IsNullOrEmpty(rdr["TransDate"].ToString()) ? null : Convert.ToDateTime(rdr["TransDate"]).ToString("yyyy MMMM").ToUpper();
                                        List.ReportData.RRRSList.Add(sma);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.RRRSList);
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                            else
                            {
                                _sql.commandExeStoredParam3("ASYS_spREMReceivingReport");
                                _sql.SaveReturnParam213("@HMonth", "@HYear", "@HBranchCode", month, hyear, branchCode);
                                rdr = _sql.ExecuteDr3();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new REMREVReportSmmry();
                                        sma.Lotno = rdr["Lotno"].ToString();
                                        sma.BranchCode = rdr["BranchCode"].ToString();
                                        sma.BranchName = rdr["BranchName"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.Itemcode = rdr["Itemcode"].ToString();
                                        sma.ItemDesc = rdr["ItemDesc"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.Karat = rdr["Karat"].ToString();
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.SortCode = rdr["SortCode"].ToString();
                                        sma.Weight = string.IsNullOrEmpty(rdr["Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["Weight"]);
                                        sma.AppraiseValue = string.IsNullOrEmpty(rdr["AppraiseValue"].ToString()) ? 0 : Convert.ToDouble(rdr["AppraiseValue"]);
                                        sma.LoanValue = string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? 0 : Convert.ToDouble(rdr["LoanValue"]);
                                        sma.Receiver = rdr["Receiver"].ToString();
                                        sma.ReceiveDate = string.IsNullOrEmpty(rdr["ReceiveDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReceiveDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.TransDate = string.IsNullOrEmpty(rdr["TransDate"].ToString()) ? null : Convert.ToDateTime(rdr["TransDate"]).ToString("yyyy MMMM").ToUpper();
                                        List.ReportData.RRRSList.Add(sma);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.RRRSList);
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                        else
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(6));
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                            }
                            else
                            {
                                _sql.commandExeStoredParam3("ASYS_REMRelease_rpt");
                                _sql.SaveReturnParam213("@Pmonth", "@Pyear", "@Pname", month, pyear, name);
                                rdr = _sql.ExecuteDr3();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var s = new REMReLRPT();
                                        s.lotno = rdr["lotno"].ToString();
                                        s.allbarcode = rdr["allbarcode"].ToString();
                                        s.PTN = rdr["PTN"].ToString();
                                        s.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        s.ActionClass = rdr["ActionClass"].ToString();
                                        s.ALL_Desc = rdr["ALL_Desc"].ToString();
                                        s.ALL_Karat = rdr["ALL_Karat"].ToString();
                                        s.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                        s.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                        s.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                        s.month = rdr["month"].ToString().ToUpper();
                                        s.year = rdr["year"].ToString().ToUpper();
                                        s.Releaser = rdr["Releaser"].ToString();
                                        s.releasedate = string.IsNullOrEmpty(rdr["releasedate"].ToString()) ? null : Convert.ToDateTime(rdr["releasedate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        s.Status = rdr["Status"].ToString();
                                        List.ReportData.RRRPTList.Add(s);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.RRRPTList);
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                    }
                    else
                    {
                        if (recrel == "T")
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(5));
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                            }
                            else
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(5));
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                            }
                        }
                        else
                        {
                            if (rdChck == "r1")
                            {
                                log.Info("LoadRemRecRPTSmmry: " + respMessage(6));
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                            }
                            else
                            {
                                _sql.commandExeStoredParam3("ASYS_REMReleaseOutsource_rpt");
                                _sql.RetrieveInfoParams2("@Plotno", lotno);
                                rdr = _sql.ExecuteDr3();
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        var sma = new ASYSREMROSRPT();
                                        sma.lotno = rdr["lotno"].ToString();
                                        sma.allbarcode = rdr["allbarcode"].ToString();
                                        sma.PTN = rdr["PTN"].ToString();
                                        sma.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                        sma.ActionClass = rdr["ActionClass"].ToString();
                                        sma.ALL_Desc = rdr["ALL_Desc"].ToString();
                                        sma.ALL_Karat = rdr["ALL_Karat"].ToString();
                                        sma.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                        sma.SerialNo = rdr["SerialNo"].ToString();
                                        sma.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                        sma.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                        sma.ALL_price = string.IsNullOrEmpty(rdr["ALL_price"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_price"]);
                                        sma.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                        sma.Releaser = rdr["Releaser"].ToString();
                                        sma.Status = rdr["Status"].ToString();
                                        List.ReportData.ASYSREMROSRPTList.Add(sma);
                                    }
                                    log.Info("LoadRemRecRPTSmmry: " + List.ReportData.ASYSREMROSRPTList);
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                                }
                                else
                                {
                                    log.Info("LoadRemRecRPTSmmry: " + respMessage(2));
                                    _sql.CloseDr3();
                                    _sql.CloseConn3();
                                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (DB != "")
                {
                    _sql.CloseConn1();
                }
                else
                {
                    _sql.CloseConn3();
                }
                log.Error("LoadRemRecRPTSmmry: " + respMessage(0) + ex.Message);
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadRemRecRPTSmmry: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult PrevLotReport(string tran, string rec, string rdChk, string lotno)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReportData = new ReportDataModels();
            List.ReportData.ASYSREMROSRPTList = new List<ASYSREMROSRPT>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                if (tran == "NotVisible")
                {
                    if (rec == "T")
                    {
                        if (rdChk == "r1")
                        {
                            log.Info("PrevLotReport: " + respMessage(5));
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                        }
                        else
                        {
                            log.Info("PrevLotReport: " + respMessage(5));
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                        }
                    }
                    else
                    {
                        if (rdChk == "r1")
                        {
                            log.Info("PrevLotReport: " + respMessage(6));
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                        }
                        else
                        {
                            _sql.commandExeStoredParam3("ASYS_REMReleaseByLot_rpt");
                            _sql.RetrieveInfoParams2("@lot", lotno);
                            rdr = _sql.ExecuteDr3();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    var asi = new ASYSREMROSRPT();
                                    asi.lotno = rdr["lotno"].ToString();
                                    asi.allbarcode = rdr["allbarcode"].ToString();
                                    asi.PTN = rdr["PTN"].ToString();
                                    asi.QTY = Convert.ToInt32(rdr["QTY"]);
                                    asi.ActionClass = rdr["ActionClass"].ToString();
                                    asi.ALL_Desc = rdr["ALL_Desc"].ToString();
                                    asi.ALL_Karat = rdr["ALL_Karat"].ToString();
                                    asi.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                    asi.SerialNo = rdr["SerialNo"].ToString();
                                    asi.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                    asi.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                    asi.ALL_price = string.IsNullOrEmpty(rdr["ALL_price"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_price"]);
                                    asi.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                    asi.Releaser = rdr["Releaser"].ToString();
                                    asi.Status = rdr["Status"].ToString();
                                    List.ReportData.ASYSREMROSRPTList.Add(asi);
                                }
                                log.Info("PrevLotReport: " + List.ReportData.ASYSREMROSRPTList);
                                _sql.CloseDr3();
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                            }
                            else
                            {
                                log.Info("PrevLotReport: " + respMessage(2));
                                _sql.CloseDr3();
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                            }
                        }
                    }
                }
                else
                {
                    if (rec == "T")
                    {
                        if (rdChk == "r1")
                        {
                            log.Info("PrevLotReport: " + "Summary is not available.");
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = "Summary is not available." };
                        }
                        else
                        {
                            log.Info("PrevLotReport: " + respMessage(5));
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(5) };
                        }
                    }
                    else
                    {
                        if (rdChk == "r1")
                        {
                            log.Info("PrevLotReport: " + respMessage(6));
                            _sql.CloseConn3();
                            return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(6) };
                        }
                        else
                        {
                            _sql.commandExeStoredParam3("ASYS_REMReleaseOutsource_rpt");
                            _sql.RetrieveInfoParams2("@Plotno", lotno);
                            rdr = _sql.ExecuteDr3();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    var ste = new ASYSREMROSRPT();
                                    ste.lotno = rdr["lotno"].ToString();
                                    ste.allbarcode = rdr["allbarcode"].ToString();
                                    ste.PTN = rdr["PTN"].ToString();
                                    ste.QTY = string.IsNullOrEmpty(rdr["QTY"].ToString()) ? 0 : Convert.ToInt32(rdr["QTY"]);
                                    ste.ActionClass = rdr["ActionClass"].ToString();
                                    ste.ALL_Desc = rdr["ALL_Desc"].ToString();
                                    ste.ALL_Karat = rdr["ALL_Karat"].ToString();
                                    ste.ALL_Carat = string.IsNullOrEmpty(rdr["ALL_Carat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Carat"]);
                                    ste.SerialNo = rdr["SerialNo"].ToString();
                                    ste.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                                    ste.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                                    ste.ALL_price = string.IsNullOrEmpty(rdr["ALL_price"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_price"]);
                                    ste.ReleaseDate = string.IsNullOrEmpty(rdr["ReleaseDate"].ToString()) ? null : Convert.ToDateTime(rdr["ReleaseDate"]).ToString("MMMM dd, yyyy").ToUpper();
                                    ste.Releaser = rdr["Releaser"].ToString();
                                    ste.Status = rdr["Status"].ToString();
                                    List.ReportData.ASYSREMROSRPTList.Add(ste);
                                }
                                log.Info("PrevLotReport: " + List.ReportData.ASYSREMROSRPTList);
                                _sql.CloseDr3();
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReportData = List.ReportData };
                            }
                            else
                            {
                                log.Info("PrevLotReport: " + respMessage(2));
                                _sql.CloseConn3();
                                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Info("PrevLotReport: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "PrevLotReport: " + respMessage(0) + ex.Message };
            }
        }
        #endregion
        [WebMethod]
        public RespALLResult OutSourceCostEdit(string DB, string barcode, string barcode2, string barcode3)
        {
            var models = new OutSourceCostEditModels();

            var _sql = new Connection();
            try
            {

                if (DB == "LNCR" || DB == "REMSLNCR")
                {
                    DB = "REMSNCR";
                }
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4("USE [" + DB + "] IF EXISTS(SELECT refallbarcode FROM ASYS_REM_Detail WHERE refallbarcode = '" + barcode + "') " +
                                "SELECT refallbarcode FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode2 + "' ELSE " +
                                "SELECT refallbarcode FROM ASYS_REMOutsource_Detail WHERE refallbarcode = '" + barcode3 + "'");
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.exists = true;
                }
                log.Info("OutSourceCostEdit: " + models.exists);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OutSourceCostEditdata = models };
            }
            catch (Exception ex)
            {
                log.Error("OutSourceCostEdit: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult getOutsourceDataCostEdit(string barcode)
        {
            var models = new getOutSourceCostEditModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4("SELECT itemid, ptn, ptnbarcode, branchcode, branchname, loanvalue, refitemcode, itemcode, branchitemdesc, " +
                    "refqty, qty, karatgrading, caratsize, SerialNo, weight, actionclass, sortcode, all_desc, all_karat, " +
                    "all_carat, all_weight, currency, all_cost, photoname, all_price, appraisevalue, cellular_cost, " +
                    "watch_cost, repair_cost, cleaning_cost, gold_cost, mount_cost, yg_cost, wg_cost, status, " +
                    "SerialNo FROM ASYS_REMOutsource_Detail " +
                    "WHERE refallbarcode=@barcode AND status NOT IN ('RELEASED','RECMLWB')");
                _sql.RetrieveInfoParamss2("@barcode", barcode);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.imagePath = models.IsNull(rdr["photoname"].ToString().Trim());
                    models.barcodestatus = models.IsNull(rdr["status"].ToString().Trim());
                    models.BranchCode = models.IsNull(rdr["branchcode"].ToString().Trim());
                    models.BranchName = models.IsNull(rdr["branchname"].ToString().Trim());
                    models.Action = models.IsNull(rdr["actionclass"].ToString().Trim());
                    models.PTN = models.IsNull(rdr["ptn"].ToString().Trim());
                    models.ptnBarcode = models.IsNull(rdr["ptnbarcode"].ToString().Trim());
                    models.AppraisedValue = models.IsNull(rdr["appraisevalue"]).Trim();
                    models.LoanValue = models.IsNull(rdr["loanvalue"]).Trim();
                    models.sortcode = models.IsNull(rdr["sortcode"].ToString().Trim());
                    models.currency = models.IsNull(rdr["currency"].ToString().Trim());
                    //----ListView
                    models.itemid = models.IsNull(rdr["itemid"].ToString().Trim());
                    models.itemcode = models.IsNull(rdr["itemcode"].ToString().Trim());
                    models.branchitemdesc = models.IsNull(rdr["branchitemdesc"].ToString().Trim());
                    models.qty = Convert.ToInt32(models.IsNull(rdr["qty"].ToString().Trim()));
                    models.karatgrading = models.IsNull(rdr["karatgrading"]).Trim();
                    models.caratsize = Convert.ToDouble(models.IsNull(rdr["caratsize"]).Trim());
                    models.weight = Convert.ToDouble(models.IsNull(rdr["weight"]).Trim());
                    models.all_cost = Convert.ToDouble(models.IsNull(rdr["all_cost"]).Trim());
                    //----ListView
                    //-----
                    models.all_desc = models.IsNull(rdr["all_desc"].ToString().Trim());
                    models.SerialNo = models.IsNull(rdr["SerialNo"].ToString().Trim());
                    models.ALL_Karat = models.IsNull(rdr["ALL_Karat"]).Trim();
                    models.ALL_Carat = Convert.ToDouble(models.IsNull(rdr["ALL_Carat"]).Trim());
                    models.ALL_Weight = Convert.ToDouble(models.IsNull(rdr["ALL_Weight"]).Trim());
                    models.ALL_price = Convert.ToDouble(models.IsNull(rdr["ALL_price"]).Trim());
                    //--ListView2                            
                    models.cellular_cost = Convert.ToDouble(models.IsNull(rdr["cellular_cost"]).Trim());
                    models.watch_cost = Convert.ToDouble(models.IsNull(rdr["watch_cost"]).Trim());
                    models.repair_cost = Convert.ToDouble(models.IsNull(rdr["repair_cost"]).Trim());
                    models.cleaning_cost = Convert.ToDouble(models.IsNull(rdr["cleaning_cost"]).Trim());
                    models.gold_cost = Convert.ToDouble(models.IsNull(rdr["gold_cost"]).Trim());
                    models.mount_cost = Convert.ToDouble(models.IsNull(rdr["mount_cost"]).Trim());
                    models.YG_cost = Convert.ToDouble(models.IsNull(rdr["YG_cost"]).Trim());
                    models.WG_cost = Convert.ToDouble(models.IsNull(rdr["WG_cost"]).Trim());
                    //models.ALL_cost2 = Convert.ToDouble(models.IsNull(dr["all_cost"]).Trim());
                    //--ListView2
                }
                log.Info("getOutsourceDataCostEdit: " + barcode);
                _sql.CloseDr4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getOutSourceCostEditdata = models };
            }
            catch (Exception ex)
            {
                log.Error("getOutsourceDataCostEdit: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult getREMDataCostEdit(string barcode, string barcode2, string db)
        {
            var models = new getREMCostEditModels();
            var _sql = new Connection();
            try
            {
                if (db == "LNCR")
                {
                    db = "NCR";
                }
                _sql.Connection1(db);
                _sql.OpenConn1();
                _sql.commandExeParam1(REMCosting.SelectCostingDetails_ASYS_REM_Detail);
                _sql.RetrieveInfoParams1("@barcode", "@barcode2", barcode, barcode2);
                rdr = _sql.ExecuteDr1();
                if (rdr.Read() == true)
                {
                    models.imagePath = models.IsNull(rdr["photoname"].ToString().Trim());
                    models.barcodestatus = models.IsNull(rdr["status"].ToString().Trim());

                    models.Action = models.IsNull(rdr["actionclass"].ToString().Trim());
                    models.PTN = models.IsNull(rdr["ptn"].ToString().Trim());
                    models.AppraisedValue = models.IsNull(rdr["appraisevalue"]).Trim();
                    models.LoanValue = models.IsNull(rdr["loanvalue"]).Trim();
                    models.sortcode = models.IsNull(rdr["sortcode"].ToString().Trim());
                    models.currency = models.IsNull(rdr["currency"].ToString().Trim());
                    //----ListView
                    models.itemid = models.IsNull(rdr["itemid"].ToString().Trim());
                    models.itemcode = models.IsNull(rdr["itemcode"].ToString().Trim());
                    models.branchitemdesc = models.IsNull(rdr["branchitemdesc"].ToString().Trim());
                    models.qty = models.IsNull(rdr["qty"].ToString().Trim());
                    models.karatgrading = models.IsNull(rdr["karatgrading"]).Trim();
                    models.caratsize = models.IsNull(rdr["caratsize"]).Trim();
                    models.weight = models.IsNull(rdr["weight"]).Trim();
                    models.all_cost = models.IsNull(rdr["all_cost"]).Trim();
                    //----ListView
                    //-----
                    models.stat = models.IsNull(rdr["stat"]).Trim();//
                    models.all_desc = models.IsNull(rdr["all_desc"].ToString().Trim());
                    models.SerialNo = models.IsNull(rdr["SerialNo"].ToString().Trim());
                    models.ALL_Karat = models.IsNull(rdr["all_karat"]).Trim();
                    models.ALL_Carat = models.IsNull(rdr["all_carat"]).Trim();
                    models.ALL_Weight = models.IsNull(rdr["all_weight"]).Trim();
                    models.ALL_price = models.IsNull(rdr["ALL_price"]).Trim();
                    //--ListView2                            
                    models.cellular_cost = models.IsNull(rdr["cellular_cost"]).Trim();
                    models.watch_cost = models.IsNull(rdr["watch_cost"]).Trim();
                    models.repair_cost = models.IsNull(rdr["repair_cost"]).Trim();
                    models.cleaning_cost = models.IsNull(rdr["cleaning_cost"]).Trim();
                    models.gold_cost = models.IsNull(rdr["gold_cost"]).Trim();
                    models.mount_cost = models.IsNull(rdr["mount_cost"]).Trim();
                    models.YG_cost = models.IsNull(rdr["YG_cost"]).Trim();
                    models.WG_cost = models.IsNull(rdr["WG_cost"]).Trim();
                    models.ALL_cost2 = models.IsNull(rdr["ALL_Cost"]).Trim();
                    //--ListView2

                    log.Info("getREMDataCostEdit: " + models);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getREMCostEditdata = models };
                }
                else
                {
                    log.Info("getREMDataCostEdit: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getREMDataCostEdit: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult getREMDataCostEdit2(string ptn, string db)
        {
            var models = new getREMDataCostEdit2Models();
            var _sql = new Connection();
            try
            {
                if (db == "LNCR")
                {
                    db = "NCR";
                }
                _sql.Connection1(db);
                _sql.OpenConn1();
                _sql.commandExeParam1(REMCosting.SelectCostingDetails_ASYS_REM_Header);
                _sql.RetrieveInfoParam1("@ptn", ptn);
                rdr = _sql.ExecuteDr1();
                if (rdr.Read() == true)
                {
                    models.branchCode = rdr["branchcode"].ToString().Trim();
                    models.branchName = rdr["branchname"].ToString().Trim();
                    models.loanValue = rdr["loanvalue"].ToString().Trim();
                    models.loanDate = string.IsNullOrEmpty(rdr["LoanDate"].ToString()) ? null : Convert.ToDateTime(rdr["LoanDate"]).ToString("MM/dd/yyyy"); ;
                    models.MaturityDate = string.IsNullOrEmpty(rdr["MaturityDate"].ToString()) ? null : Convert.ToDateTime(rdr["MaturityDate"]).ToString("MM/dd/yyyy"); ;
                    models.ExpiryDate = string.IsNullOrEmpty(rdr["ExpiryDate"].ToString()) ? null : Convert.ToDateTime(rdr["ExpiryDate"]).ToString("MM/dd/yyyy"); ;
                    models.ptnBarcode = models.isNull(rdr["ptnbarcode"]).Trim();

                    log.Info("getREMDataCostEdit2: " + ptn + " | " + models);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getREMCostEditData2 = models };
                }
                else
                {
                    log.Info("getREMDataCostEdit2: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getREMDataCostEdit2: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult getREMDataCostEditActionClass(string actionClass)
        {
            var models = new getREMDataCostEditActionClassModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4(REMCosting.SelectActClass_tblACtion);
                _sql.RetrieveInfoParamss2("@action", actionClass);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.costA = Convert.ToDouble(rdr["CostA"]);
                    models.costB = Convert.ToDouble(rdr["CostB"]);
                    models.costC = Convert.ToDouble(rdr["CostC"]);
                    models.costD = Convert.ToDouble(rdr["CostD"]);
                    log.Info("getREMDataCostEditActionClass: actionClass: " + actionClass + " | " + models);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getREMDataCostEditActionClassdata = models };
                }
                else
                {
                    log.Info("getREMDataCostEditActionClass: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getREMDataCostEditActionClass: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }

        }
        //-----------Goodstock
        [WebMethod]
        public RespALLResult getREMDataCostEditCostDetails(string barcode)
        {
            var models = new getREMDataCostEditCostDetailsModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4(REMCosting.SelectCostDetails_ASYS_REMOUTSOURCE_Detail);
                _sql.RetrieveInfoParamss2("@barcode", barcode);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.Cost1 = models.isNull(rdr["Gold_Cost"]);
                    models.Cost2 = models.isNull(rdr["Mount_Cost"]);
                    models.Cost3 = models.isNull(rdr["YG_Cost"]);
                    models.Cost4 = models.isNull(rdr["WG_Cost"]);
                    models.Cost5 = models.isNull(rdr["ALL_Cost"]);
                    log.Info("getREMDataCostEditCostDetails: " + barcode + " | " + models);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), getREMDataCostEditCostDetailsdata = models };
                }
                else
                {
                    log.Info("getREMDataCostEditCostDetails: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getREMDataCostEditCostDetails: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }

        }
        //new Saving Costing GoodStock
        [WebMethod]
        public RespALLResult saveREMOutSourceGoodStockCosting(int quantity, string description, string serial, string karat, double carat,
            double weight, double cost, string currency, double goldCost, double mountCost, double ygCost, double wgCost, string userLog, string barcode, string sDB)
        {
            var _sql = new Connection();
            try
            {
                if (sDB == "LNCR")
                {
                    sDB = "NCR";
                }
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.BeginTransax4();
                //1st
                _sql.commandTraxParam4("UPDATE ASYS_REM_Detail SET refqty = @quantity,all_desc =@description,SerialNo = @serialNo,all_karat =@karat,all_carat =@carat" +
                ",all_weight =@weight,all_cost =@cost,currency = @currency,price_desc = @description2,price_karat = @karat2,price_weight = @weight2" +
                ",price_carat = @carat2,gold_cost = @goldCost,mount_cost =@mounCost,yg_cost = @ygCost,wg_cost = @wgCost,costdate = getdate(),costname = @userLog" +
                ",status='COSTED' WHERE refallbarcode = @barcode AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.SaveOutSourceCostingGoodStock4("@quantity", "@description", "@serialNo", "@karat", "@carat", "@weight", "@cost", "@currency",
                    "@description2", "@karat2", "@weight2", "@carat2", "@goldCost", "@mounCost", "@ygCost", "@wgCost", "@userLog", "@barcode",
                    quantity, description, serial, karat, carat, weight, cost, currency, description, karat, weight, carat, goldCost, mountCost, ygCost,
                    wgCost, userLog, barcode);
                _sql.Execute4();
                //2nd
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REM_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',gold_cost = '" + goldCost +
                        "',mount_cost = '" + mountCost + "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();///////////////////////////////////////////////////
                //3rd
                _sql.commandTraxParam4("UPDATE ASYS_REMOutsource_Detail SET refqty = @quantity2,all_desc =@description3,SerialNo = @serialNo2,all_karat =@karat3,all_carat =@carat3" +
                ",all_weight =@weight3,all_cost =@cost3,currency = @currency3,price_desc = @description4,price_karat = @karat4,price_weight = @weight4" +
                ",price_carat = @carat4,gold_cost = @goldCost4,mount_cost =@mounCost4,yg_cost = @ygCost4,wg_cost = @wgCost4,costdate = getdate(),costname = @userLog2" +
                ",status='COSTED' WHERE refallbarcode = @barcode2 AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.SaveOutSourceCostingGoodStock4("@quantity2", "@description3", "@serialNo2", "@karat3", "@carat3", "@weight3", "@cost3", "@currency3",
                    "@description4", "@karat4", "@weight4", "@carat4", "@goldCost4", "@mounCost4", "@ygCost4", "@wgCost4", "@userLog2", "@barcode2",
                    quantity, description, serial, karat, carat, weight, cost, currency, description, karat, weight, carat, goldCost, mountCost, ygCost,
                    wgCost, userLog, barcode);
                _sql.Execute4();
                //4th
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REMOutsource_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',gold_cost = '" + goldCost +
                        "',mount_cost = '" + mountCost + "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();
                //5th
                _sql.commandTraxParam4("UPDATE REMS.dbo.ASYS_Barcodehistory SET description =@description6" +
                ",SerialNo =@serial6,karat =@karat6, carat =@carat6, weight =@weight6 WHERE refallbarcode =@barcode6");
                _sql.SaveCostingUpdateParam4("@description6", "@serial6", "@karat6", "@carat6", "@weight6", "@barcode6", description, serial, karat, carat,
                    weight, barcode);
                _sql.Execute4();


                log.Info("saveREMOutSourceGoodStockCosting: " + respMessage(1) + " | barcode: " + barcode + " | sDB: " + sDB);
                _sql.commitTransax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };//////////////aha
            }
            catch (Exception ex)
            {
                log.Error("saveREMOutSourceGoodStockCosting: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveREMOutSourceGoodStockCosting: " + respMessage(0) + ex.Message };
            }
        }//DONE
        //-------End GOODSTOCK
        //----
        //--------------------CELLULAR
        [WebMethod]
        public RespALLResult saveCostingForCellular(string barcode)
        {
            var models = new saveCostingForCellularModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4(REMCosting.CellularCosting_Select_REMOutSourceDetail);
                _sql.RetrieveInfoParamss2("@barcode", barcode);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.Cost1 = models.isNull(rdr["Cellular_Cost"]);
                    models.Cost2 = models.isNull(rdr["Repair_Cost"]);
                    models.Cost3 = models.isNull(rdr["Cleaning_Cost"]);
                    models.Cost4 = models.isNull(rdr["ALL_Cost"]);
                    log.Info("saveCostingForCellular: barcode: " + barcode + " | " + models);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), saveCostingForCellulardata = models };
                }
                else
                {
                    log.Info("saveCostingForCellular: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("saveCostingForCellular: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //new Saving Cellular
        [WebMethod]
        public RespALLResult saveCostingForCellularALL(int quantity, string description, string serial, string karat, double carat, double weight,
             double cost, string currency, double cellCost, double watchCost, double repairCost, double cleanCost, string goldCost, string ygCost,
            string wgCost, string userLog, string barcode, string sDB)
        {
            var _sql = new Connection();
            try
            {
                if (sDB == "LNCR")
                {
                    sDB = "NCR";
                }
                String x = "'";
                String y;
                y = x + barcode + x;
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.BeginTransax4();
                //1st
                _sql.commandTraxParam4("UPDATE ASYS_REM_Detail SET refqty =@quantity,all_desc =@description,SerialNo =@serialNo3,all_karat =@karat4," +
                "all_carat =@carat,all_weight = @weight,all_cost =@cost,currency =@currency,price_desc =@description2,price_karat =@karat2,price_weight = @weight2," +
                "price_carat = @carat2,cellular_cost =@cellularCost,watch_cost =@watchCost,repair_cost =@repairCost,cleaning_cost =@cleaningCost,gold_cost = @goldCost,yg_cost = @ygCost," +
                "wg_cost=@wgCost, costdate= getdate(),costname=@userLog, status='COSTED' WHERE refallbarcode = @barcode AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.saveCostingParam4("@quantity", "@description", "@serialNo3", "@karat4", "@carat", "@weight", "@cost", "@currency", "@description2",
                "@karat2", "@weight2", "@carat2", "@cellularCost", "@watchCost", "@repairCost", "@cleaningCost", "@goldCost", "@ygCost",
                "@wgCost", "@userLog", "@barcode", quantity, description, serial, karat, carat, weight, cost, currency, description, karat,
                weight, carat, cellCost, watchCost, repairCost, cleanCost, goldCost, ygCost, wgCost, userLog, barcode);//y);//
                _sql.Execute4();
                //2nd
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REM_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',cellular_cost='" +
                        cellCost + "',watch_cost='" + watchCost + "',repair_cost='" + repairCost + "',cleaning_cost='" + cleanCost + "',gold_cost = '" + goldCost +
                        "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();
                //3rd
                _sql.commandTraxParam4("UPDATE ASYS_REMOutsource_Detail SET refqty =@quantity2,all_desc =@description3,SerialNo =@serial3,all_karat = @karatt4," +
                "all_carat =@carat4,all_weight = @weight4,all_cost =@cost4,currency =@currency4,price_desc =@description4,price_karat =@karat5,price_weight = @weight5," +
                "price_carat = @carat5,cellular_cost =@cellularCost5,watch_cost =@watchCost5,repair_cost =@repairCost5,cleaning_cost =@cleaningCost5,gold_cost = @goldCost5,yg_cost = @ygCost5," +
                "wg_cost=@wgCost5, costdate= getdate(),costname=@userLog5, status='COSTED' WHERE refallbarcode = @barcode5 AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.saveCostingParam4("@quantity2", "@description3", "@serial3", "@karatt4", "@carat4", "@weight4", "@cost4", "@currency4", "@description4",
                    "@karat5", "@weight5", "@carat5", "@cellularCost5", "@watchCost5", "@repairCost5", "@cleaningCost5", "@goldCost5", "@ygCost5",
                    "@wgCost5", "@userLog5", "@barcode5", quantity, description, serial, karat, carat, weight, cost, currency, description, karat,
                    weight, carat, cellCost, watchCost, repairCost, cleanCost, goldCost, ygCost, wgCost, userLog, barcode);
                _sql.Execute4();/////////////////////////////////////////
                //4th
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REMOutsource_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',cellular_cost='" +
                        cellCost + "',watch_cost='" + watchCost + "',repair_cost='" + repairCost + "',cleaning_cost='" + cleanCost + "',gold_cost = '" + goldCost +
                        "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();
                //5th
                _sql.commandTraxParam4("UPDATE REMS.dbo.ASYS_Barcodehistory SET description =@description6" +
                ",SerialNo =@serial6,karat =@karat6, carat =@carat6, weight =@weight6 WHERE refallbarcode =@barcode6");
                _sql.SaveCostingUpdateParams4("@description6", "@serial6", "@karat6", "@carat6", "@weight6", "@barcode6", description,
                    serial, karat, carat, weight, barcode);
                _sql.Execute4();

                log.Info("saveCostingForCellularALL: " + respMessage(1) + " | barcode: " + barcode + " | sDB: " + sDB);
                _sql.commitTransax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveCostingForCellularALL: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveCostingForCellularALL: " + respMessage(0) + ex.Message };
            }
        }//DONE
        //----------------End Cellular
        //--------Watch
        [WebMethod]
        public RespALLResult selectWatchCosting(string barcode)
        {
            var models = new selectWatchCostingModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4(REMCosting.WatchCosting_Select_ASYS_REMOutSource_DEtail);
                _sql.RetrieveInfoParamss2("@barcode", barcode);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.Cost1 = models.isNull(rdr["Watch_Cost"]);
                    models.Cost2 = models.isNull(rdr["Repair_Cost"]);
                    models.Cost3 = models.isNull(rdr["Cleaning_Cost"]);
                    models.Cost4 = models.isNull(rdr["ALL_Cost"]);
                    log.Info("selectWatchCosting: barcode: " + barcode + " | " + models);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = "Succes!", selectWatchCostingdata = models };
                }
                else
                {
                    log.Info("selectWatchCosting: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("selectWatchCosting: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //new Saving For Watch Costing
        [WebMethod]
        public RespALLResult saveCostingForWatchALL(int quantity, string description, string serial, string karat, double carat, double weight,
            double cost, string currency, double cellCost, double watchCost, double repairCost, double cleanCost, double goldCost, double ygCost,
            double wgCost, string userLog, string barcode, string sDB)
        {
            var _sql = new Connection();
            try
            {
                if (sDB == "LNCR")
                {
                    sDB = "NCR";
                }
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.BeginTransax4();
                //1st
                _sql.commandTraxParam4("UPDATE ASYS_REM_Detail SET refqty = @qty,  all_desc = @description," +
                "SerialNo = @serialNo,all_karat =@karat,all_carat = @carat,all_weight = @weight,all_cost =@cost,currency = @currency," +
                "price_desc =@description2,price_karat =@karat2,price_weight = @weight2,price_carat = @carat2,cellular_cost =@cellCost," +
                "watch_cost = @watchCost,repair_cost =@repairCost,cleaning_cost =@cleanCost,gold_cost = @goldCost,yg_cost =@ygCost,wg_cost =@wgCost," +
                "costdate = getdate(),costname =@userLog,status = 'COSTED' WHERE refallbarcode =@barcode" +
                " AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.saveCostingParams4("@qty", "@description", "@serialNo", "@karat", "@carat", "@weight", "@cost", "@currency", "@description2",
                    "@karat2", "@weight2", "@carat2", "@cellCost", "@watchCost", "@repairCost", "@cleanCost", "@goldCost", "@ygCost",
                    "@wgCost", "@userLog", "@barcode", quantity, description, serial, karat, carat, weight, cost, currency, description, karat,
                    weight, carat, cellCost, watchCost, repairCost, cleanCost, goldCost, ygCost, wgCost, userLog, barcode);
                _sql.Execute4();
                //2nd
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REM_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',cellular_cost='" +
                        cellCost + "',watch_cost='" + watchCost + "',repair_cost='" + repairCost + "',cleaning_cost='" + cleanCost + "',gold_cost = '" + goldCost +
                        "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();
                //3rd
                _sql.commandTraxParam4("UPDATE ASYS_REMOutsource_Detail SET refqty = @qty2,  all_desc = @description3," +
               "SerialNo = @serialNo3,all_karat =@karat4,all_carat = @carat4,all_weight = @weight4,all_cost =@cost4,currency = @currency4," +
               "price_desc =@description4,price_karat =@karat5,price_weight = @weight5,price_carat = @carat5,cellular_cost =@cellCost5," +
               "watch_cost = @watchCost5,repair_cost =@repairCost5,cleaning_cost =@cleanCost5,gold_cost = @goldCost5,yg_cost =@ygCost5,wg_cost =@wgCost5," +
               "costdate = getdate(),costname =@userLog5,status = 'COSTED' WHERE refallbarcode =@barcode5" +
               " AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.saveCostingParams4("@qty2", "@description3", "@serialNo3", "@karat4", "@carat4", "@weight4", "@cost4", "@currency4", "@description4",
                    "@karat5", "@weight5", "@carat5", "@cellCost5", "@watchCost5", "@repairCost5", "@cleanCost5", "@goldCost5", "@ygCost5",
                    "@wgCost5", "@userLog5", "@barcode5", quantity, description, serial, karat, carat, weight, cost, currency, description, karat,
                    weight, carat, cellCost, watchCost, repairCost, cleanCost, goldCost, ygCost, wgCost, userLog, barcode);
                _sql.Execute4();
                //4th
                _sql.commandTraxParam4("UPDATE REMS" + sDB + ".dbo.ASYS_REMOutsource_Detail SET refqty = '" + quantity +
                        "',all_desc = '" + description + "',SerialNo = '" + serial + "',all_karat ='" + karat + "',all_carat = '" + carat +
                        "',all_weight = '" + weight + "',all_cost ='" + cost + "',currency ='" + currency + "',price_desc ='" + description +
                        "',price_karat = '" + karat + "',price_weight = '" + weight + "',price_carat ='" + carat + "',cellular_cost='" +
                        cellCost + "',watch_cost='" + watchCost + "',repair_cost='" + repairCost + "',cleaning_cost='" + cleanCost + "',gold_cost = '" + goldCost +
                        "',yg_cost = '" + ygCost + "',wg_cost = '" + wgCost + "',costdate = getdate(),costname ='" + userLog +
                        "',status='COSTED' WHERE refallbarcode ='" + barcode + "' AND STATUS NOT IN ('RELEASED','RECMLWB')");
                _sql.Execute4();
                //5th
                _sql.commandTraxParam4("UPDATE REMS.dbo.ASYS_Barcodehistory SET description =@description6" +
                ",SerialNo =@serial6,karat =@karat6, carat =@carat6, weight =@weight6 WHERE refallbarcode =@barcode6");
                _sql.SaveCostingUpdateParams4("@description6", "@serial6", "@karat6", "@carat6", "@weight6", "@barcode6", description, serial,
                    karat, carat, weight, barcode);
                _sql.Execute4();

                log.Info("saveCostingForWatchALL: " + respMessage(1) + " | barcode: " + barcode + " | sDB: " + sDB);
                _sql.commitTransax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveCostingForWatchALL: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveCostingForWatchALL: " + respMessage(0) + ex.Message };
            }
        }//DONE
        //-----end watch
        //---------------recode
        [WebMethod]
        public RespALLResult LoadRecodeActionType()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.LoadRecodeActionTypedata = new LoadRecodeActionTypeModels();
            List.LoadRecodeActionTypedata.actionType = new List<string>();
            List.LoadRecodeActionTypedata.actionID = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("SELECT action_id, action_type FROM tbl_action WHERE action_id in(1,3,8,11,10,2,4,7,9,5) order by action_type");
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.LoadRecodeActionTypedata.actionType.Add(rdr["action_type"].ToString().ToUpper().Trim());
                        List.LoadRecodeActionTypedata.actionID.Add(rdr["action_id"].ToString().Trim());
                    }
                    log.Info("LoadRecodeActionType: " + List.LoadRecodeActionTypedata.actionType + " | " + List.LoadRecodeActionTypedata.actionID);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadRecodeActionTypedata = List.LoadRecodeActionTypedata };
                }
                else
                {
                    log.Info("LoadRecodeActionType: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadRecodeActionType: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadRecodeActionType2()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.LoadRecodeActionTypedata = new LoadRecodeActionTypeModels();
            List.LoadRecodeActionTypedata.description = new List<string>();
            List.LoadRecodeActionTypedata.code = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select description, code from tbl_sortclass order by description");
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.LoadRecodeActionTypedata.description.Add(rdr["description"].ToString().ToUpper().Trim());
                        List.LoadRecodeActionTypedata.code.Add(rdr["code"].ToString().Trim());
                    }
                    log.Info("LoadRecodeActionType2: " + List.LoadRecodeActionTypedata.description + " | " + List.LoadRecodeActionTypedata.code);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadRecodeActionTypedata = List.LoadRecodeActionTypedata };
                }
                else
                {
                    log.Info("LoadRecodeActionType2: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadRecodeActionType2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //Retrieve
        //1st
        [WebMethod]
        public RespALLResult RecodeRetrieveInfo(string barcode, string barcode2)
        {
            var _sql = new Connection();
            var models = new RecodeRetrieveInfoModels();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select 1 as st,ptn,photoname as photo,itemID ,costdate as cost_id,actionclass,sortcode," +
                " status, reflotno  from ASYS_REM_Detail where  refALLBarcode=@barcode and status not in ('RECMLWB')" +
                " UNION ALL select 0 as st,ptn,photoname as photo,itemid ,costdate as cost_id,actionclass,sortcode," +
                " status, reflotno from ASYS_REMOutsource_detail where  refALLBarcode=@barcode2 and status not in ('RECMLWB')");
                _sql.RetrieveInfoParams("@barcode", "@barcode2", barcode, barcode2);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.st = rdr["st"].ToString();
                    models.costID = models.isNull(rdr["cost_id"]).Trim();
                    models.itemID = rdr["itemid"].ToString().Trim();
                    models.photo = models.isNull(rdr["photo"]).Trim();
                    models.status = rdr["status"].ToString().Trim();
                    models.ptn = models.isNull(rdr["ptn"]).Trim();
                    models.reflotno = models.isNull(rdr["reflotno"]).Trim();
                    models.actionClass = rdr["actionclass"].ToString().ToUpper();
                    models.sortCode = models.isNull(rdr["sortcode"]).Trim();
                    log.Info("RecodeRetrieveInfo: " + models);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RecodeRetrieveInfodata = models };
                }
                else
                {
                    log.Info("RecodeRetrieveInfo: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("RecodeRetrieveInfo: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //2nd
        [WebMethod]
        public RespALLResult RecodeRetrieveInfo2(string ptn, string lotno)
        {
            var _sql = new Connection();
            var models = new RecodeRetrieveInfo2Models();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select branchcode,branchname,ptn,ptnbarcode,loanvalue,maturitydate," +
                "expirydate,loandate from ASYS_REM_Header where ptn =@sPtn and lotno = @lotno");
                _sql.RetrieveInfoParams("@sPtn", "@lotno", ptn, lotno);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.branchCode = rdr["branchcode"].ToString().Trim();
                    models.branchName = rdr["branchname"].ToString().Trim();
                    models.loanValue = rdr["loanvalue"].ToString().Trim();
                    log.Info("RecodeRetrieveInfo2: ptn: " + ptn + " | lotno: " + lotno + " | " + models);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RecodeRetrieveInfo2data = models };
                }
                else
                {
                    log.Info("RecodeRetrieveInfo2: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("RecodeRetrieveInfo2: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }

        }
        //3rd
        [WebMethod]
        public RespALLResult RecodeRetrieveInfo3(string barcode)
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            var models = new RecodeRetrieveInfo3Models();
            List.RecodeRetrieveInfo3data = new RecodeRetrieveInfo3Models();
            List.RecodeRetrieveInfo3data.itemid = new List<string>();
            List.RecodeRetrieveInfo3data.itemcode = new List<string>();
            List.RecodeRetrieveInfo3data.branchitemdesc = new List<string>();
            List.RecodeRetrieveInfo3data.qty = new List<string>();
            List.RecodeRetrieveInfo3data.karatgrading = new List<string>();
            List.RecodeRetrieveInfo3data.caratsize = new List<string>();
            List.RecodeRetrieveInfo3data.weight = new List<string>();
            List.RecodeRetrieveInfo3data.all_cost = new List<string>();
            List.RecodeRetrieveInfo3data.price_desc = new List<string>();
            List.RecodeRetrieveInfo3data.serialno = new List<string>();
            List.RecodeRetrieveInfo3data.refqty = new List<string>();
            List.RecodeRetrieveInfo3data.price_karat = new List<string>();
            List.RecodeRetrieveInfo3data.price_carat = new List<string>();
            List.RecodeRetrieveInfo3data.price_weight = new List<string>();
            //List.RecodeRetrieveInfo3data.ALL_Cost = new List<string>();
            List.RecodeRetrieveInfo3data.ALL_price = new List<string>();
            List.RecodeRetrieveInfo3data.appraisevalue = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select itemid,refallbarcode,appraisevalue,refitemcode,itemcode,branchitemdesc," +
                "refqty,qty,karatgrading,caratsize,weight,all_desc,serialno,all_karat,all_carat,all_weight,all_cost,photoname,price_desc," +
                "price_karat,price_carat,price_weight,all_price,cellular_cost,watch_cost,repair_cost,cleaning_cost,Gold_cost,mount_cost," +
                "yg_cost,wg_cost from ASYS_REM_Detail where refALLBarcode =@barcode and status not in ('RECMLWB')");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.RecodeRetrieveInfo3data.itemid.Add(models.isNull(rdr["itemid"]).Trim());
                        List.RecodeRetrieveInfo3data.itemcode.Add(models.isNull(rdr["itemcode"]).Trim());
                        List.RecodeRetrieveInfo3data.branchitemdesc.Add(models.isNull(rdr["branchitemdesc"]).Trim());
                        List.RecodeRetrieveInfo3data.qty.Add(models.isNull(rdr["qty"]).Trim());
                        List.RecodeRetrieveInfo3data.karatgrading.Add(models.isNull(rdr["karatgrading"]).Trim());
                        List.RecodeRetrieveInfo3data.caratsize.Add(models.isNull(rdr["caratsize"]).Trim());
                        List.RecodeRetrieveInfo3data.weight.Add(models.isNull(rdr["weight"]).Trim());
                        List.RecodeRetrieveInfo3data.all_cost.Add(models.isNull(rdr["all_cost"]).Trim());
                        List.RecodeRetrieveInfo3data.price_desc.Add(models.isNull(rdr["price_desc"]).Trim());
                        List.RecodeRetrieveInfo3data.serialno.Add(models.isNull(rdr["serialno"]).Trim());
                        List.RecodeRetrieveInfo3data.refqty.Add(models.isNull(rdr["refqty"]).Trim());
                        List.RecodeRetrieveInfo3data.price_karat.Add(models.isNull(rdr["price_karat"]).Trim());
                        List.RecodeRetrieveInfo3data.price_carat.Add(models.isNull(rdr["price_carat"]).Trim());
                        List.RecodeRetrieveInfo3data.price_weight.Add(models.isNull(rdr["price_weight"]).Trim());
                        //List.RecodeRetrieveInfo3data.ALL_Cost.Add(models.isNull(rdr["ALL_Cost"]).Trim());
                        List.RecodeRetrieveInfo3data.ALL_price.Add(models.isNull(rdr["ALL_price"]).Trim());
                        List.RecodeRetrieveInfo3data.appraisevalue.Add(models.isNull(rdr["appraisevalue"]).Trim());
                    }
                    log.Info("RecodeRetrieveInfo3: barcode: " + barcode + " | " + List.RecodeRetrieveInfo3data);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RecodeRetrieveInfo3data = List.RecodeRetrieveInfo3data };
                }
                else
                {
                    log.Info("RecodeRetrieveInfo3: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("RecodeRetrieveInfo3: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }

        }
        //4th
        [WebMethod]
        public RespALLResult RecodeRetrieveInfo4(string barcode)
        {
            var _sql = new Connection();
            var models = new RecodeRetrieveInfo4Models();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select itemid,ptn,ptnbarcode,branchcode,branchname," +
                "loanvalue,refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode," +
                "all_desc,serialno,all_karat,all_carat,all_weight,all_cost,photoname,price_desc,price_karat,price_carat,price_weight," +
                "all_price,appraisevalue,cellular_cost,watch_cost,repair_cost,cleaning_cost,gold_cost,mount_cost,yg_cost,wg_cost,status" +
                " from ASYS_REMOutsource_detail where refallbarcode=@barcode and status not in ('RECMLWB')");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.photoname = models.isNull(rdr["photoname"]);
                    models.branchcode = models.isNull(rdr["branchcode"]);
                    models.appraisevalue = models.isNull(rdr["appraisevalue"]);
                    models.loanvalue = models.isNull(rdr["loanvalue"]);
                    models.itemcode = models.isNull(rdr["itemcode"]);
                    models.branchitemdesc = models.isNull(rdr["branchitemdesc"]);
                    models.qty = models.isNull(rdr["qty"]);
                    models.caratsize = models.isNull(rdr["caratsize"]);
                    models.weight = models.isNull(rdr["weight"]);
                    models.all_cost = models.isNull(rdr["all_cost"]);
                    models.price_desc = models.isNull(rdr["price_desc"]);
                    models.serialno = models.isNull(rdr["serialno"]);
                    models.refqty = models.isNull(rdr["refqty"]);
                    models.price_karat = models.isNull(rdr["price_karat"]);
                    models.price_carat = models.isNull(rdr["price_carat"]);
                    models.price_weight = models.isNull(rdr["price_weight"]);
                    models.ALL_price = models.isNull(rdr["ALL_price"]);
                    models.itemid = models.isNull(rdr["itemid"]);
                    models.karatgrading = models.isNull(rdr["karatgrading"]);

                    log.Info("RecodeRetrieveInfo4: barcode: " + barcode + " | " + models);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RecodeRetrieveInfo4data = models };
                }
                else
                {
                    log.Info("RecodeRetrieveInfo4: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("RecodeRetrieveInfo4: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //5th
        [WebMethod]
        public RespALLResult RecodeRetrieveInfo5(string sDb, string branchCode)
        {
            var models = new RecodeRetrieveInfo5Models();

            var _sql = new Connection();
            try
            {
                if (sDb == "LNCR")
                {
                    sDb = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + sDb + " where bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnm = models.isNull(rdr["bedrnm"]).Trim();

                    log.Info("RecodeRetrieveInfo5: branchCode: " + branchCode + " | " + models.bedrnm);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RecodeRetrieveInfo5data = models };
                }
                else
                {
                    log.Info("RecodeRetrieveInfo5: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("RecodeRetrieveInfo5: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        //-------Recode Display ActionClass
        [WebMethod]
        public RespALLResult DisplayActionClass(string selected)
        {
            var models = new DisplayActionClassModels();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select CostA, CostB, CostC, CostD from tbl_action where action_type =@selected");
                _sql.RetrieveInfoParams2("@selected", selected);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.CostA = models.isNull(rdr["CostA"].ToString().Trim());
                    models.CostB = models.isNull(rdr["CostB"].ToString().Trim());
                    models.CostC = models.isNull(rdr["CostC"].ToString().Trim());
                    models.CostD = models.isNull(rdr["CostD"].ToString().Trim());

                    log.Info("DisplayActionClass: " + models);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClassdata = models };
                }
                else
                {
                    log.Info("DisplayActionClass: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //2nd
        [WebMethod]
        public RespALLResult DisplayActionClass2(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost, ALL_Cost from" +
                " ASYS_REM_DEtail where refAllbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.Gold_Cost = models.isNUll(rdr["Gold_Cost"]).Trim();
                    models.Mount_Cost = models.isNUll(rdr["Mount_Cost"]).Trim();
                    models.YG_Cost = models.isNUll(rdr["YG_Cost"]).Trim();
                    models.WG_Cost = models.isNUll(rdr["WG_Cost"]).Trim();
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                }
                log.Info("DisplayActionClass2: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass2: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //3rd
        [WebMethod]
        public RespALLResult DisplayActionClass3(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_REM_DEtail where refAllbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                    models.Cellular_Cost = models.isNUll(rdr["Cellular_Cost"]).Trim();
                    models.Repair_Cost = models.isNUll(rdr["Repair_Cost"]).Trim();
                    models.Cleaning_Cost = models.isNUll(rdr["Cleaning_Cost"]).Trim();
                }
                log.Info("DisplayActionClass3: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass3: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //4th
        [WebMethod]
        public RespALLResult DisplayActionClass4(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_REM_DEtail where refAllbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                    models.Watch_Cost = models.isNUll(rdr["Watch_Cost"]).Trim();
                    models.Repair_Cost = models.isNUll(rdr["Repair_Cost"]).Trim();
                    models.Cleaning_Cost = models.isNUll(rdr["Cleaning_Cost"]).Trim();
                }
                log.Info("DisplayActionClass4: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass4: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //5th
        [WebMethod]
        public RespALLResult DisplayActionClass5(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost,  ALL_Cost from ASYS_REMOutsource_detail where refallbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.Gold_Cost = models.isNUll(rdr["Gold_Cost"]).Trim();
                    models.Mount_Cost = models.isNUll(rdr["Mount_Cost"]).Trim();
                    models.YG_Cost = models.isNUll(rdr["YG_Cost"]).Trim();
                    models.WG_Cost = models.isNUll(rdr["WG_Cost"]).Trim();
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                }
                log.Info("DisplayActionClass5: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass5: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //6th
        [WebMethod]
        public RespALLResult DisplayActionClass6(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_REMOutsource_detail where refallbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                    models.Cellular_Cost = models.isNUll(rdr["Cellular_Cost"]).Trim();
                    models.Repair_Cost = models.isNUll(rdr["Repair_Cost"]).Trim();
                    models.Cleaning_Cost = models.isNUll(rdr["Cleaning_Cost"]).Trim();
                }
                log.Info("DisplayActionClass6: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClass6: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //7th
        [WebMethod(MessageName = "Test")]
        public RespALLResult DisplayActionClassNew(string barcode)
        {
            var models = new DisplayActionClass2Modesl();

            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_REMOutsource_detail where refallbarcode =@barcode");
                _sql.RetrieveInfoParams2("@barcode", barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.ALL_Cost = models.isNUll(rdr["ALL_Cost"]).Trim();
                    models.Watch_Cost = models.isNUll(rdr["Watch_Cost"]).Trim();
                    models.Repair_Cost = models.isNUll(rdr["Repair_Cost"]).Trim();
                    models.Cleaning_Cost = models.isNUll(rdr["Cleaning_Cost"]).Trim();
                }
                log.Info("DisplayActionClassNew: barcode: " + barcode + " | " + models);
                _sql.CloseDr3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayActionClass2data = models };
            }
            catch (Exception ex)
            {
                log.Error("DisplayActionClassNew: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //-------Recode Edit Barcode
        //1st
        [WebMethod]
        public RespALLResult ExeMaxGen(string barcode)
        {
            var models = new ExeMaxGenModels();

            var _sql = new Connection();
            type2 = 3;
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParams3("@allbarcode", "@type", barcode, type2);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    //models.bcode = rdr["Barcode"].ToString().Trim();//-----------------> made modifications here 
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
                }
                else
                {
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult ExeMaxGen2(string barcode)
        {
            var models = new ExeMaxGenModels();

            var _sql = new Connection();
           // type2 = 3;
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParams3("@allbarcode", "@type", barcode, type2);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.bcode = rdr["Barcode"].ToString().Trim();
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), bcode = models.barcode.ToString() };
                }
                else
                {
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }

        [WebMethod]
        public RespALLResult ExeMaxGen3(string barcode)
        {
            var models = new ExeMaxGenModels();

            var _sql = new Connection();
            // type2 = 3;
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParams3("@allbarcode", "@type", barcode, type2);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    models.bcode = rdr["Barcode"].ToString().Trim();
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), bcode = models.barcode.ToString() };
                }
                else
                {
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult ExeMaxGen4(string bcode, int type2)
        {
            var models = new ExeMaxGenModels();

            var _sql = new Connection();
           // type2 = 3;
            try
            {
                String x;

                if (type2 == 1)
                {
                    x = "Barcode";
                }
             
                else
                {
                    x = "refallbarcode";
                }
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeStoredParam4("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParams3("@allbarcode", "@type", bcode, type2);
                rdr = _sql.ExecuteDr4();
                if (rdr.Read() == true)
                {
                    if (type2 == 1)
                    {
                        models.barcode = Convert.ToInt32(rdr[x]) + 1;
                    }
                    else
                    {
                        models.refallbarcode = Convert.ToInt64(rdr[x]) + 1; 
                        
                    }

                    log.Info("ExeMaxGen2: barcode: " + models.barcode);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ExeMaxGendata = models };
                }
                else
                {
                    log.Info("ExeMaxGen2: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("ExeMaxGen2: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        //--------Recode Edit Barcode Saving
        [WebMethod]
        public RespALLResult SaveRecodeEdit(string editedBarcode, string itemcode, string photoname, string comboValue)
        {
            var _sql = new Connection();
            var models = new CountBarcodeModels();
            SqlDataReader dr;// = new SqlDataReader();
            string x, y, result, message;

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
               // _sql.BeginTransax3();

                _sql.commandTraxParam3("SELECT REFALLBARCODE FROM REMS.DBO.ASYS_BARCODEHISTORY WHERE REFALLBARCODE ='" + editedBarcode + "'");
                dr = _sql.ExecuteDr3();
               if (dr.HasRows == true)
                {
                    dr.Read();
                    //dr.Read();
                    //x = dr["SORTERNAME"].ToString();
                    y = dr["REFALLBARCODE"].ToString();
                    dr.Close();
                    result = "3";
                    message = "ALLBARCODE ALREADY EXIST: " + y + "";//SORTED BY:" + x + "";

                    //_sql.RollBackTrax3();
                    return new RespALLResult { responseCode = result, responseMsg = message };
                }
                else
                {
                    _sql.CloseConn3();
                }

                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                //1st
                _sql.commandTraxParam3("Update REMS.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSNCR.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                    "Update REMSLuzon.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSVisayas.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSMindanao.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSShowroom.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMS.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+
                     "Update REMSNCR.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'" +

                    "Update REMSLuzon.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSVisayas.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSMindanao.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update REMSShowroom.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                    "' where refallbarcode ='" + comboValue + "'"+

                    "Update ASYS_BarcodeHistory  set refallbarcode ='" + editedBarcode +
                    "' where refallbarcode ='" + comboValue + "'"
                    );
                _sql.Execute3();
                #region
                ////2nd
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //3rd
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //4th
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //5th
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //RemOutsource DEtail
                //1st
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //2nd
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //3rd
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //4th
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //5th
                //_sql.commandTraxParam3();
                //_sql.Execute3();
                //6th
                //_sql.commandTraxParam3();
                //_sql.Execute3();






                ////1st
                //_sql.commandTraxParam3("Update REMS.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////2nd
                //_sql.commandTraxParam3("Update REMSLuzon.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////3rd
                //_sql.commandTraxParam3("Update REMSVisayas.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////4th
                //_sql.commandTraxParam3("Update REMSMindanao.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////5th
                //_sql.commandTraxParam3("Update REMSShowroom.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////RemOutsource DEtail
                ////1st
                //_sql.commandTraxParam3("Update REMS.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////2nd
                //_sql.commandTraxParam3("Update REMSLuzon.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////3rd
                //_sql.commandTraxParam3("Update REMSVisayas.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////4th
                //_sql.commandTraxParam3("Update REMSMindanao.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////5th
                //_sql.commandTraxParam3("Update REMSShowroom.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                ////6th
                //_sql.commandTraxParam3("Update ASYS_BarcodeHistory  set refallbarcode ='" + editedBarcode +
                //    "' where refallbarcode ='" + comboValue + "'");
                //_sql.Execute3();
                #endregion 

                //_sql.commandTraxParam3("SELECT SORTERNAME,REFALLBARCODE FROM REMS.DBO.ASYS_REM_DETAIL WHERE REFALLBARCODE ='" + editedBarcode + "' AND SORTERNAME NOT IN ('NULL','') UNION ALL SELECT SORTERNAME,REFALLBARCODE FROM REMS.DBO.ASYS_REMOUTSOURCE_DETAIL WHERE REFALLBARCODE ='" + editedBarcode + "' AND SORTERNAME NOT IN ('NULL','')");
                //dr = _sql.ExecuteDr3();
                //if (dr.HasRows == true)
                //{
                //    dr.Read();
                //    dr.Read();
                //    x = dr["SORTERNAME"].ToString();
                //    y = dr["REFALLBARCODE"].ToString();
                //    dr.Close();
                //    result = "3";
                //    message = "ALLBARCODE ALREADY EXIST: " + y + "SORTED BY:" + x + "";

                //    _sql.RollBackTrax3();
                //    return new RespALLResult { responseCode = result, responseMsg = message };
                //}

                models.photoname = Connection.photodes;

                log.Info("SaveRecodeEdit: " + respMessage(1) + " | " + models + " | editedBarcode: " + editedBarcode);
                _sql.commitTransax3();
               //sql.RollBackTrax3();
                File.Copy(Connection.photodes + comboValue + ".jpg", Connection.photodes + editedBarcode + ".jpg", true);

                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CountBarcodedata = models };
            }
            catch (Exception ex)
            {
                log.Error("SaveRecodeEdit: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "SaveRecodeEdit: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult SaveRecodeEdit2(string editedBarcode, string itemcode, string comboValue)
        {
            var _sql = new Connection();
            SqlDataReader dr;// = new SqlDataReader();
            string x, y, result, message;

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.Connection1("VISAYAS");
                
                _sql.OpenConn1();

                _sql.BeginTransax3();

                _sql.commandTraxParam1("SELECT REFALLBARCODE FROM REMS.DBO.ASYS_BARCODEHISTORY WHERE REFALLBARCODE ='" + editedBarcode + "' ");
                dr = _sql.ExecuteDr1();
                if (dr.HasRows == true)
                {

                    dr.Read();
                    //x = dr["SORTERNAME"].ToString();
                    y = dr["REFALLBARCODE"].ToString();
                    dr.Close();
                    result = "3";
                    message = "ALLBARCODE ALREADY EXIST: " + y + "";//SORTED BY:" + x + "";

                   // _sql.RollBackTrax3();
                    return new RespALLResult { responseCode = result, responseMsg = message };
                }

                else
                {
                    dr.Close();
                    //_sql.CloseConn1();
                    //_sql.BeginTransax3();
                }



                _sql.commandTraxParam3("Update REMS.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSNCR.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSLuzon.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSVisayas.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSMindanao.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSShowroom.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMS.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSNCR.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSLuzon.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSVisayas.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSMindanao.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update REMSShowroom.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                   "',refitemcode = '" + itemcode + "',photoname = '" + Connection.photodes + editedBarcode + ".jpg" +
                   "' where refallbarcode ='" + comboValue + "'" +

                   "Update ASYS_BarcodeHistory  set refallbarcode ='" + editedBarcode +
                   "' where refallbarcode ='" + comboValue + "'"
                   );

                #region the original update  
                //_sql.commandTraxParam3("Update REMS.dbo.ASYS_REM_Detail set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSLuzon.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSVisayas.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSMindanao.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSShowroom.dbo.ASYS_REM_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMS.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSLuzon.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSVisayas.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSMindanao.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update REMSShowroom.dbo.ASYS_REMOutsource_Detail  set refallbarcode = '" + editedBarcode +
                //    "',refitemcode = '" + itemcode + "' where refallbarcode ='" + comboValue + "'" +

                //    "Update ASYS_BarcodeHistory  set refallbarcode ='" + editedBarcode +
                //    "' where refallbarcode ='" + comboValue + "'"
                //    );
                #endregion

                _sql.Execute3();

                

                log.Info("SaveRecodeEdit2: " + respMessage(1) + " | editedBarcode: " + editedBarcode);
                //File.Copy(Connection.photodes + comboValue + ".jpg", Connection.photodes + editedBarcode + ".jpg", true);//editedBarcode
                //GC.Collect();
                //File.Delete(Connection.photodes + comboValue + ".jpg");
                _sql.commitTransax3();
                //_sql.RollBackTrax3();
                //File.Copy(Connection.photodes + comboValue + ".jpg", Connection.photodes + editedBarcode + ".jpg",true);//editedBarcode
                // GC.Collect();
                // File.Delete(Connection.photodes + comboValue + ".jpg");
                //_sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("SaveRecodeEdit2: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "SaveRecodeEdit2: " + respMessage(0) + ex.Message };
            }
        }
        //-------------OutSource Releasing-------------------//
        [WebMethod]
        public RespALLResult callActionClass()
        {
            var List = new RespALLResult();
            List.ReleasingActClassdata = new ReleasingActClassModels();
            List.ReleasingActClassdata.actionType = new List<string>();

            var _sql = new Connection();

            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("select action_type from tbl_action where action_id  in (6,11,10,9,5,13,4,8,3)");
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.ReleasingActClassdata.actionType.Add(rdr["action_type"].ToString());
                    }
                    log.Info("callActionClass: " + List.ReleasingActClassdata.actionType);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingActClassdata = List.ReleasingActClassdata };
                }
                else
                {
                    log.Info("callActionClass: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("callActionClass: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult refreshLotNumber()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.RefreshLotdata = new RefreshLotModels();
            List.RefreshLotdata.lotNumber = new List<string>();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("SELECT reflotno as lotno FROM ASYS_REMOutsource_Detail where status = 'COSTED' and" +
                " actionclass in ('Return','Outsource','Cellular','Watch') group by reflotno order by reflotno desc");
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.RefreshLotdata.lotNumber.Add(rdr["lotno"].ToString().Trim());
                    }
                    log.Info("refreshLotNumber: " + respMessage(1));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RefreshLotdata = List.RefreshLotdata };
                }
                else
                {
                    log.Info("refreshLotNumber: " + "No Data To Release.");
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = "No Data To Release." };
                }

            }
            catch (Exception ex)
            {
                log.Error("refreshLotNumber: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult countBarcode(string[][] DgEntry)
        {
            var _sql = new Connection();

            var models = new CountBarcodeModels();
            try
            {
                for (int i = 0; i <= DgEntry.Count() - 1; i++)
                {
                    _sql.Connection3();
                    _sql.OpenConn3();
                    _sql.commandExeParam3("SELECT photoname FROM ASYS_REM_detail WHERE refallbarcode = '" + DgEntry[i][0] + "'" +
                        " UNION ALL SELECT photoname FROM ASYS_REMOutsource_detail WHERE refallbarcode = '" + DgEntry[i][0] + "'");

                    rdr = _sql.ExecuteDr3();
                    if (rdr.Read() == true)
                    {
                        models.photoname = models.isNull(rdr["photoname"]).Trim();

                        _sql.CloseDr3();
                        _sql.CloseConn3();
                    }
                    else
                    {
                        _sql.CloseDr3();
                        _sql.CloseConn3();
                        return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                    }

                }
                log.Info("countBarcode: " + models.photoname);
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CountBarcodedata = models };
            }
            catch (Exception ex)
            {
                log.Error("countBarcode: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult releaseToVP(string DB, string userLog, string ptn, string itemID)
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
                _sql.commandTraxParam1("UPDATE REMS" + Connection.sDB + ".dbo.ASYS_REM_Detail SET status ='RELEASED', releaser = '" + userLog +
                    "', releasedate = getdate() WHERE ptn = '" + ptn + "' AND itemid = '" + itemID + "'");
                _sql.Execute1();
                //2nd
                _sql.commandTraxParam1("UPDATE [REMS].dbo.ASYS_REM_Detail SET status ='RELEASED', releaser = '" + userLog +
                    "', releasedate = getdate() WHERE ptn = '" + ptn + "' AND itemid = '" + itemID + "'");
                _sql.Execute1();

                log.Info("releaseToVP: " + respMessage(1) + " | ptn: " + ptn + " | DB: " + Connection.sDB + " | userLog: " + userLog);
                _sql.commitTransax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("releaseToVP: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "releaseToVP: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult releaseToVP2(string DB, string userLog, string[][] ListView)
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
                for (int i = 0; i <= ListView.Count() - 1; i++)
                {
                    //1st
                    _sql.commandTraxParam1("Update REMS" + Connection.sDB + ".dbo.ASYS_REM_Detail SET releasedate = getdate(),releaser= '" + userLog +
                    "',status = 'RELEASED' WHERE ptn = '" + ListView[i][0] + "' AND status = 'SORTED'");
                    _sql.Execute1();
                    //2nd
                    _sql.commandTraxParam1("Update [REMS].dbo.ASYS_REM_Detail SET releasedate = getdate(),releaser = '" + userLog +
                        "',status = 'RELEASED' WHERE ptn = '" + ListView[i][0] + "' AND status = 'SORTED' AND itemid = '" + ListView[i][1] + "'");
                    _sql.Execute1();
                }
                log.Info("releaseToVP2: " + respMessage(1) + " | DB: " + Connection.sDB + " | userLog: " + userLog);
                _sql.commitTransax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("releaseToVP2: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "releaseToVP2: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult releaseToVP3(string DB, string userLog, string[][] ListView)
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
                    //1st
                    _sql.commandTraxParam1("Update REMS" + Connection.sDB + ".dbo.ASYS_TRADEIN_Detail SET releasedate = getdate(),releaser= '" + userLog +
                        "',status = 'RELEASED' WHERE transaction_no = '" + ListView[i][0] + "' AND status = 'RECEIVED'");
                    _sql.Execute1();
                    //2nd
                    _sql.commandTraxParam1("Update [REMS].dbo.ASYS_TRADEIN_Detail SET releasedate = getdate(),releaser = '" + userLog +
                        "',status = 'RELEASED' WHERE transaction_no = '" + ListView[i][0] + "' AND status = 'RECEIVED' AND item_id = '" + ListView[i][1] + "'");
                    _sql.Execute1();
                }
                log.Info("releaseToVP3: " + respMessage(1) + " | DB: " + Connection.sDB + " | userLog: " + userLog);
                _sql.commitTransax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("releaseToVP3: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax1();
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "releaseToVP3: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult DisplayToReleaseBarcode(string lotno)
        {


            var models = new DisplayToReleaseBarcodeModels();
            var _sql = new Connection();
            var List = new RespALLResult();
            List.DisplayToReleaseBarcodedata = new DisplayToReleaseBarcodeModels();
            List.DisplayToReleaseBarcodedata.ptn = new List<string>();
            List.DisplayToReleaseBarcodedata.refallbarcode = new List<string>();
            List.DisplayToReleaseBarcodedata.barcodeMid = new List<string>();
            List.DisplayToReleaseBarcodedata.all_desc = new List<string>();
            List.DisplayToReleaseBarcodedata.alL_weight = new List<string>();
            List.DisplayToReleaseBarcodedata.all_karat = new List<string>();
            List.DisplayToReleaseBarcodedata.all_carat = new List<string>();
            List.DisplayToReleaseBarcodedata.all_price = new List<string>();

            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(REMReleasing.DisplayReleaseLotNoItems);
                _sql.RetrieveInfoParams("@lotno", "@lotno2", lotno, lotno);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.DisplayToReleaseBarcodedata.ptn.Add(models.isNull(rdr["ptn"]).Trim());
                        List.DisplayToReleaseBarcodedata.refallbarcode.Add(models.isNull(rdr["refallbarcode"]).Trim());
                        List.DisplayToReleaseBarcodedata.barcodeMid.Add(models.isNull(rdr["refallbarcode"]).Trim());
                        List.DisplayToReleaseBarcodedata.all_desc.Add(models.isNull(rdr["all_desc"]).Trim());
                        List.DisplayToReleaseBarcodedata.alL_weight.Add(models.isNull(rdr["alL_weight"]).Trim());
                        List.DisplayToReleaseBarcodedata.all_karat.Add(models.isNull(rdr["all_karat"]).Trim());
                        List.DisplayToReleaseBarcodedata.all_carat.Add(models.isNull(rdr["all_carat"]).Trim());
                        List.DisplayToReleaseBarcodedata.all_price.Add(models.isNull(rdr["all_price"]).Trim());
                    }
                    log.Info("DisplayToReleaseBarcode: lotno: " + lotno + " | " + List.DisplayToReleaseBarcodedata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayToReleaseBarcodedata = List.DisplayToReleaseBarcodedata };
                }
                else
                {
                    log.Info("DisplayToReleaseBarcode: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("DisplayToReleaseBarcode: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "DisplayToReleaseBarcode: " + respMessage(0) + ex.Message };
            }
        }
        //------------------------Release to MLWB---------------//
        [WebMethod]
        public RespALLResult releaseToMLWB(string[][] dgEntry, string lotno, string userLog)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.BeginTransax4();
                //1st
                _sql.commandTraxParam4(REMReleasing.ReleaseUpdateASYS_Lotno_gen);
                _sql.RetrieveInfoParamss2("@lotno", lotno);
                _sql.Execute4();

                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    if (dgEntry[i] != null)
                    {
                        //2nd
                        _sql.commandTraxParam4("INSERT INTO [REMS].dbo.ASYS_BarcodeHistory (lotno,refallbarcode,allbarcode,itemcode," +
                            "[description],karat,carat,weight,currency,price,cost,custodian,trandate,status,costcenter,empname,SerialNO)" +
                            " SELECT lotno,allbarcode as refallbarcode,allbarcode,itemcode,[description],karat,carat,weight,currency,price,cost" +
                            ",custodian,trandate,status,costcenter,empname,SerialNO FROM (SELECT '" + lotno + "' as lotno, RefALLBarcode as" +
                            " allbarcode, RefItemcode as itemcode, ALL_Desc as [description],ALL_Karat as karat, ALL_Carat as carat, ALL_Weight" +
                            " as weight, Currency , ALL_price as price, ALL_Cost as cost,SorterName as custodian,getdate() as trandate" +
                            ",'RELEASED' as status,'REM' as costcenter,'" + userLog + "' as empname,SerialNO FROM [REMS].dbo.ASYS_REM_Detail" +
                            " WHERE refallbarcode = '" + dgEntry[i][0] + "' AND status = 'COSTED' UNION ALL SELECT '" + lotno + "' as lotno," +
                            " RefALLBarcode as allbarcode, RefItemcode as itemcode, ALL_Desc as [description],ALL_Karat as karat, ALL_Carat " +
                            "as carat, ALL_Weight as weight, Currency , ALL_price as price, ALL_Cost as cost,SorterName as custodian," +
                            "getdate() as trandate,'RELEASED' as status,'REM' as costcenter,'" + userLog + "' as empname,SerialNO " +
                            "FROM [REMS].dbo.ASYS_REMOutsource_Detail WHERE refallbarcode = '" + dgEntry[i][0] + "' AND status = 'COSTED')a");
                        _sql.Execute4();
                        //3rd
                        _sql.commandTraxParam4("UPDATE [REMS].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSNCR].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSLUZON].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSVISAYAS].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSMINDANAO].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSSHOWROOM].DBO.ASYS_REM_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMS].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSNCR].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSLUZON].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSVISAYAS].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSMINDANAO].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';" +
                      "UPDATE [REMSSHOWROOM].DBO.ASYS_REMOutsource_detail SET reflotno = '" + lotno + "',releasedate = getdate(),releaser = '" + userLog + "',status = 'RELEASED' where refallbarcode = '" + dgEntry[i][0] + "' and status = 'COSTED';");
                        _sql.Execute4();
                    }
                    else
                    {
                        break;
                    }
                }
                log.Info("releaseToMLWB: " + respMessage(1) + " | lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("releaseToMLWB: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax4();
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "releaseToMLWB: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult checkIfCosted(string barcode)
        {

            var models = new DisplayToReleaseALLBarcodeModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(REMReleasing.DisplayToReleaseALLBarcode);
                _sql.RetrieveInfoParams("@barcode", "@barcode2", barcode, barcode);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.status = rdr["status"].ToString().Trim();
                    models.Photoname = models.isNull(rdr["Photoname"]);
                    models.ptn = models.isNull(rdr["ptn"]).Trim();
                    models.all_price = models.isNull(rdr["all_price"]).Trim();
                    models.ALL_DESC = rdr["ALL_DESC"].ToString();
                    models.ALL_weight = rdr["ALL_weight"].ToString().Trim();
                    models.ALL_karat = rdr["ALL_karat"].ToString().Trim();
                    models.all_carat = rdr["all_carat"].ToString().Trim();
                    models.Price_desc = rdr["Price_desc"].ToString().Trim();
                    models.Price_weight = rdr["Price_weight"].ToString().Trim();
                    models.Price_karat = rdr["Price_karat"].ToString().Trim();
                    models.Price_carat = rdr["Price_carat"].ToString().Trim();

                    log.Info("checkIfCosted: barcode: " + barcode + " | " + models);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DisplayToReleaseALLBarcodedata = models };

                }
                else
                {
                    log.Info("checkIfCosted: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("checkIfCosted: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "checkIfCosted: " + respMessage(0) + ex.Message };
            }
        }
        //Release VP RPT
        [WebMethod]
        public RespALLResult RPTLoad()
        {

            var _sql = new Connection();
            var List = new RespALLResult();
            List.RPTLoaddata = new RPTLoadModels();
            List.RPTLoaddata.actionType = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(REMReleasing.RPTLoadData);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.RPTLoaddata.actionType.Add(rdr["action_type"].ToString().Trim().ToUpper());
                    }
                    log.Info("RPTLoad: " + List.RPTLoaddata.actionType);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RPTLoaddata = List.RPTLoaddata };
                }
                else
                {
                    log.Info("RPTLoad: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("RPTLoad: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "RPTLoad: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult RPTCheckBranchCode(string DB, string branchCode)
        {

            var models = new RPTCheckBranchCodeModels();
            var _sql = new Connection();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnm = rdr["bedrnm"].ToString().Trim();

                    log.Info("RPTCheckBranchCode: " + models.bedrnm);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RPTCheckBranchCodedata = models };
                }
                else
                {
                    log.Info("RPTCheckBranchCode: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("RPTCheckBranchCode: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "RPTCheckBranchCode: " + respMessage(0) + ex.Message };
            }

        }//DONE
        //Releasing Report
        [WebMethod]
        public RespALLResult ReleasingRPTLoad(string userLog)
        {

            var _sql = new Connection();
            var models = new ReleasingRPTLoadModels();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select  isnull(max(reflotno), 0) as reflotno from ASYS_REMOutsource_detail where releaser =@userLog and status = 'RELEASED'");//(REMReleasing.RPTReleaseLoadData);
                _sql.RetrieveInfoParams2("@userLog", userLog);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.reflotno = rdr["reflotno"].ToString().Trim();

                    log.Info("ReleasingRPTLoad: " + models.reflotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingRPTLoaddata = models };
                }
                else
                {
                    log.Info("ReleasingRPTLoad: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("ReleasingRPTLoad: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "ReleasingRPTLoad: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult ReleasingRPTLoadEMP()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.ReleasingRPTLoadEMPdata = new ReleasingRPTLoadEMPModels();
            List.ReleasingRPTLoadEMPdata.fullname = new List<string>();

            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam(REMReleasing.RPTRelLoadEmp);
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.ReleasingRPTLoadEMPdata.fullname.Add(rdr["fullname"].ToString().ToUpper());
                    }
                    log.Info("ReleasingRPTLoadEMP: " + List.ReleasingRPTLoadEMPdata.fullname);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingRPTLoadEMPdata = List.ReleasingRPTLoadEMPdata };
                }
                else
                {
                    log.Info("ReleasingRPTLoadEMP: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("ReleasingRPTLoadEMP: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "ReleasingRPTLoadEMP: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult CheckBranchCodeREPORT(string sDB, string branchCode)
        {
            var _sql = new Connection();
            var models = new CheckBranchCodeREPORTModels();

            try
            {
                if (sDB == "LNCR")
                {
                    sDB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + sDB + " where bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnm = rdr["bedrnm"].ToString().ToUpper();

                    log.Info("CheckBranchCodeREPORT: " + models.bedrnm);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CheckBranchCodeREPORTdata = models };
                }
                else
                {
                    log.Info("CheckBranchCodeREPORT: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("CheckBranchCodeREPORT: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult CheckBranchNameREPORT(string sDB, string branchName)
        {
            var models = new CheckBranchNameREPORTModels();
            var _sql = new Connection();

            try
            {
                if (sDB == "LNCR")
                {
                    sDB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnr from REMS.dbo.vw_bedryf" + sDB + " where bedrnm='" + branchName + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnr = rdr["bedrnr"].ToString();

                    log.Info("CheckBranchNameREPORT: " + models.bedrnr);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CheckBranchNameREPORTdata = models };
                }
                else
                {
                    log.Info("CheckBranchNameREPORT: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("CheckBranchNameREPORT: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "CheckBranchNameREPORT: " + respMessage(0) + ex.Message };
            }
        }//DONE
        //1st
        [WebMethod]
        public RespALLResult RelLoadDataByYear(string comboYearValue, string comboMonthIndexValue, string costName, string DB)
        {
            var _sql = new Connection();
            var models = new RelLoadDataByYearModels();
            var List = new RespALLResult();
            List.RelLoadDataByYeardata = new RelLoadDataByYearModels();
            List.RelLoadDataByYeardata.ptn = new List<string>();
            List.RelLoadDataByYeardata.refallbarcode = new List<string>();
            List.RelLoadDataByYeardata.itemcode = new List<string>();
            List.RelLoadDataByYeardata.all_desc = new List<string>();
            List.RelLoadDataByYeardata.alL_weight = new List<string>();
            List.RelLoadDataByYeardata.all_karat = new List<string>();
            List.RelLoadDataByYeardata.all_carat = new List<string>();
            List.RelLoadDataByYeardata.all_price = new List<string>();

            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection1(DB);
                _sql.OpenConn1();
                _sql.commandExeParam1(REMReleasing.RelDisplayDataByYear);
                _sql.SaveCostingUpdateParam1("@year", "@month", "@costName", "@year2", "@month2", "@costName2", comboYearValue, comboMonthIndexValue,
                    costName, comboYearValue, comboMonthIndexValue, costName);
                rdr = _sql.ExecuteDr1();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.RelLoadDataByYeardata.ptn.Add(models.isNull(rdr["ptn"]).Trim());
                        List.RelLoadDataByYeardata.refallbarcode.Add(models.isNull(rdr["refallbarcode"]).Trim());
                        List.RelLoadDataByYeardata.itemcode.Add(models.isNull(rdr["refallbarcode"]).Trim());
                        List.RelLoadDataByYeardata.all_desc.Add(models.isNull(rdr["all_desc"]).Trim());
                        List.RelLoadDataByYeardata.alL_weight.Add(models.isNull(rdr["alL_weight"]).Trim());
                        List.RelLoadDataByYeardata.all_karat.Add(models.isNull(rdr["all_karat"]).Trim());
                        List.RelLoadDataByYeardata.all_carat.Add(models.isNull(rdr["all_carat"]).Trim());
                        List.RelLoadDataByYeardata.all_price.Add(models.isNull(rdr["all_price"]).Trim());
                    }
                    log.Info("RelLoadDataByYear: " + List.RelLoadDataByYeardata + " | " + respMessage(1));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RelLoadDataByYeardata = List.RelLoadDataByYeardata };
                }
                else
                {
                    log.Info("RelLoadDataByYear: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("RelLoadDataByYear: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult LoadTradeInOrMissingItem(string query)
        {

            var _sql = new Connection();
            var models = new LoadTradeInOrMissingItemModels();
            var List = new RespALLResult();
            List.LoadTradeInOrMissingItemdata = new LoadTradeInOrMissingItemModels();
            List.LoadTradeInOrMissingItemdata.PTN = new List<string>();
            List.LoadTradeInOrMissingItemdata.refitemcode = new List<string>();
            List.LoadTradeInOrMissingItemdata.all_desc = new List<string>();
            List.LoadTradeInOrMissingItemdata.Refqty = new List<string>();
            List.LoadTradeInOrMissingItemdata.all_karat = new List<string>();
            List.LoadTradeInOrMissingItemdata.all_carat = new List<string>();
            List.LoadTradeInOrMissingItemdata.sortcode = new List<string>();
            List.LoadTradeInOrMissingItemdata.all_weight = new List<string>();
            List.LoadTradeInOrMissingItemdata.loanvalue = new List<string>();
            List.LoadTradeInOrMissingItemdata.all_cost = new List<string>();
            List.LoadTradeInOrMissingItemdata.itemid = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(query);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.LoadTradeInOrMissingItemdata.PTN.Add(models.isNull(rdr["PTN"]).Trim());
                        List.LoadTradeInOrMissingItemdata.refitemcode.Add(models.isNull(rdr["refitemcode"]).Trim());
                        List.LoadTradeInOrMissingItemdata.all_desc.Add(models.isNull(rdr["all_desc"]).Trim());
                        List.LoadTradeInOrMissingItemdata.Refqty.Add(models.isNull(rdr["Refqty"]).Trim());
                        List.LoadTradeInOrMissingItemdata.all_karat.Add(models.isNull(rdr["all_karat"]).Trim());
                        List.LoadTradeInOrMissingItemdata.all_carat.Add(models.isNull(rdr["all_carat"]).Trim());
                        List.LoadTradeInOrMissingItemdata.sortcode.Add(models.isNull(rdr["sortcode"]).Trim());
                        List.LoadTradeInOrMissingItemdata.all_weight.Add(models.isNull(rdr["all_weight"]).Trim());
                        List.LoadTradeInOrMissingItemdata.loanvalue.Add(models.isNull(rdr["loanvalue"]).Trim());
                        List.LoadTradeInOrMissingItemdata.all_cost.Add(models.isNull(rdr["all_cost"]).Trim());
                        List.LoadTradeInOrMissingItemdata.itemid.Add(models.isNull(rdr["itemid"]).Trim());
                    }
                    log.Info("LoadTradeInOrMissingItem: " + respMessage(1) + " | " + List.LoadTradeInOrMissingItemdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadTradeInOrMissingItemdata = List.LoadTradeInOrMissingItemdata };
                }
                else
                {
                    log.Info("LoadTradeInOrMissingItem: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadTradeInOrMissingItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadTradeInOrMissingItem: " + respMessage(0) + ex.Message };
            }

        }
        [WebMethod]
        public RespALLResult DisplayBranchCodeRel(string DB, string branchCode)
        {

            var _sql = new Connection();
            var models = new CheckBranchCodeREPORTModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + branchCode + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnm = rdr["bedrnm"].ToString();

                    log.Info("DisplayBranchCodeRel: " + models.bedrnm);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CheckBranchCodeREPORTdata = models };
                }
                else
                {
                    log.Info("DisplayBranchCodeRel: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("DisplayBranchCodeRel: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult comboChange(string fullname)
        {

            var _sql = new Connection();
            var models = new comboChangeModels();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select res_id from " + Connection.humres2 + " where fullname='" + fullname + "'");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.res_id = rdr["res_id"].ToString();

                    log.Info("comboChange: " + models.res_id);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), comboChangedata = models };
                }
                else
                {
                    log.Info("comboChange: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("comboChange: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }

        }

        //-------------------------------------REM ITEM RECEIVING--------------------------//
        [WebMethod]
        public RespALLResult OnLoadREMItem()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.OnLoadREMItemdata = new OnLoadREMItemModels();
            List.OnLoadREMItemdata.action_type = new List<string>();
            List.OnLoadREMItemdata.action_id = new List<string>();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam(REMREVITEM.LoadSelectActionType);
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.OnLoadREMItemdata.action_type.Add(rdr["action_type"].ToString().Trim().ToUpper());
                        List.OnLoadREMItemdata.action_id.Add(rdr["action_id"].ToString().Trim().ToUpper());
                    }
                    log.Info("OnLoadREMItem: " + respMessage(1) + " | " + List.OnLoadREMItemdata.action_type + " | " + List.OnLoadREMItemdata.action_id);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OnLoadREMItemdata = List.OnLoadREMItemdata };
                }
                else
                {
                    log.Info("OnLoadREMItem: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("OnLoadREMItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "OnLoadREMItem: " + respMessage(0) + ex.Message };
            }

        }
        //---------------------------------------------------feb 10,l2017
        [WebMethod]
        public RespALLResult OnLoadREMInDividually(string DB)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.OnLoadREMItemdata = new OnLoadREMItemModels();
            List.OnLoadREMItemdata.action_type = new List<string>();
            List.OnLoadREMItemdata.action_id = new List<string>();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection1(DB);
                _sql.OpenConn1();
                //REMREVITEM.LoadSelectActionType;
                _sql.commandExeParam1("select actiontype, action_id from rems.dbo.tbl_action where action_id in (6,1,3,8,11) order by action_type");

                rdr = _sql.ExecuteDr1();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.OnLoadREMItemdata.action_type.Add(rdr["action_type"].ToString().Trim().ToUpper());
                        List.OnLoadREMItemdata.action_id.Add(rdr["action_id"].ToString().Trim().ToUpper());
                    }
                    log.Info("OnLoadREMInDividually: " + List.OnLoadREMItemdata.action_type + " | " + List.OnLoadREMItemdata.action_id);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), OnLoadREMItemdata = List.OnLoadREMItemdata };
                }
                else
                {
                    log.Info("OnLoadREMInDividually: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("OnLoadREMInDividually: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "OnLoadREMInDividually: " + respMessage(0) + ex.Message };
            }

        }//DONE
        //---------------------------------------------------end feb 10,l2017
        //------------------------from jan 18 - February 6,2017 End-------------------------------------//
        #endregion
        #region February 7,2017
        [WebMethod]
        public RespALLResult getBedryf1(string DB, string BranchCode)
        {
            var _sql = new Connection();
            var models = new RPTCheckBranchCodeModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where bedrnr='" + BranchCode + "' and dateend is null");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnm = rdr["bedrnm"].ToString();

                    log.Info("getBedryf1: " + models.bedrnm);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RPTCheckBranchCodedata = models };
                }
                else
                {
                    log.Info("getBedryf1: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getBedryf1: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "getBedryf1: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult getBedryf2(string BranchName)
        {
            var _sql = new Connection();
            var models = new UnReceivedReportByBranchNameModels();
            try
            {
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select top 1 * from " + Connection.bedryf2 + " where bedrnm like '%" + BranchName + "%'");
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.branchCode = rdr["bedrnr"].ToString();
                    models.branchName = rdr["bedrnm"].ToString();

                    log.Info("getBedryf2: " + models.branchCode + " | " + models.branchName);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), UnReceivedReportByBranchNamedata = models };
                }
                else
                {
                    log.Info("getBedryf2: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("getBedryf2: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "getBedryf2: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult GenerateLotNo()
        {
            var _sql = new Connection();
            var models = new ReleasingRPTLoadModels();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select lotno from ASYS_LOTNO_Gen");
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.reflotno = rdr["lotno"].ToString();

                    log.Info("GenerateLotNo: " + models.reflotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingRPTLoaddata = models };
                }
                else
                {
                    log.Info("GenerateLotNo: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("GenerateLotNo: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "GenerateLotNo: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult checkDuplicate(string DB, string Lotno)
        {
            var _sql = new Connection();
            var models = new checkBarcodeIfExistModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection1(DB);
                _sql.OpenConn1();
                _sql.commandExeParam1("Select lotno from ASYS_REMOutsource_header where lotno = '" + Lotno + "'");
                rdr = _sql.ExecuteDr1();
                if (rdr.Read() == true)
                {
                    models.exists = true;

                    log.Info("checkDuplicate: " + models.exists);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), checkdata2 = models };
                }
                else
                {
                    log.Info("checkDuplicate: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("checkDuplicate: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "checkDuplicate: " + respMessage(0) + ex.Message };
            }
        }//DONE


        [WebMethod]
        public RespALLResult SaveRemItem(string DB, string Lotno, string barcode, string itemCode, string Description, string Quantity, string Karat,
            string carat, string weight, string comboValue, string userLog)
           
        {
            var _sql = new Connection();
            var x = new entries();                       
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                if (check_entries(barcode) && red == false)
                {
                    x.code = "3";//respCode(1);
                    x.fetch = respMessage(0).ToString();//"Barcode is already in use";
                }
                else if (red == true)
                {
                    red = false;
                    x.code = "4";//respCode(1);
                    x.fetch = read_me;//respMessage(0).ToString();//"Barcode is already in use";
                }
                else
                {
                    _sql.Connection3();
                    _sql.OpenConn3();
                    _sql.BeginTransax3();
                    //1st
                    _sql.commandTraxParam3("Insert into rems" + DB + ".dbo.ASYS_REMOutSource_Header(Lotno,TYPE)  values ('" + Lotno + "','REMGOODSTOCK')");
                    _sql.Execute3();
                    //2nd
                    _sql.commandTraxParam3("Insert into rems" + DB + ".dbo.ASYS_REMOutSource_detail(Reflotno,lotno,refallbarcode,allbarcode," +
                        "refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc," +
                        "all_karat,all_carat,all_weight,price_desc,price_karat,price_weight,price_carat,sortdate,sortername,Receivedate," +
                        "Receiver,status) values ('" + Lotno + "','" + Lotno + "','" + barcode + "','" + barcode + "','" + itemCode +
                        "','" + itemCode + "','" + Description + "','" + Quantity + "','" + Quantity + "','" + Karat + "','" + carat +
                        "','" + weight + "','" + comboValue + "','O','" + Description + "','" + Karat + "','" + carat + "','" + weight +
                        "','" + Description + "','" + Karat + "','" + weight + "','" + carat + "',getdate(),'" + userLog +
                        "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute3();
                    //3rd
                    _sql.commandTraxParam3("Insert into ASYS_REMOutSource_Header(Lotno,TYPE)  values ('" + Lotno + "','REMGOODSTOCK')");
                    _sql.Execute3();
                    //4th
                    _sql.commandTraxParam3("Insert into ASYS_REMOutSource_detail(Reflotno,lotno,refallbarcode,allbarcode,refitemcode,itemcode" +
                        ",branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc,all_karat,all_carat,all_weight" +
                        ",price_desc,price_karat,price_weight,price_carat,sortdate,sortername,Receivedate,Receiver,status) values ('" + Lotno +
                        "','" + Lotno + "','" + barcode + "','" + barcode + "','" + itemCode + "','" + itemCode + "','" + Description +
                        "','" + Quantity + "','" + Quantity + "','" + Karat + "','" + carat + "','" + weight + "','" + comboValue +
                        "','O','" + Description + "','" + Karat + "','" + carat + "','" + weight + "','" + Description + "','" + Karat +
                        "','" + weight + "','" + carat + "',getdate(),'" + userLog + "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute3();
                    //5th
                    _sql.commandTraxParam3("Insert into ASYS_BarcodeHistory(lotno,refallbarcode,allbarcode,itemcode,Description,karat,carat,weight" +
                        ",currency,empname,custodian,trandate,costcenter,status)values ('" + Lotno + "','" + barcode + "','" + barcode +
                        "','" + itemCode + "','" + Description + "','" + Karat + "','" + carat + "','" + weight + "','PHP','" + userLog +
                        "','" + userLog + "',getdate(),'REM','RECEIVED')");
                    _sql.Execute3();

                    log.Info("SaveRemItem: " + respMessage(1) + " | Lotno: " + Lotno + " | userLog: " + userLog + " | DB: " + DB);
                    _sql.commitTransax3();
                    //_sql.RollBackTrax3();
                    _sql.CloseConn3();
                    x.code = respCode(1);///"0";
                    x.fetch = respMessage(1);//"Data saved";
                }
                return new RespALLResult { responseCode = x.code, responseMsg = x.fetch};//respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("SaveRemItem: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "SaveRemItem: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult SaveRemItem2(string DB, string Lotno, string barcode, string itemCode, string Description, string Quantity, string Karat,
            string carat, string weight, string comboValue, string userLog)
        {
            var _sql = new Connection();
            var x = new entries();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                if (check_entries(barcode) && red == false)
                {
                    x.code = "3";//respCode(1);
                    x.fetch = respMessage(0).ToString();//"Barcode is already in use";
                }
                else if ( red == true)
                {
                    red = false;
                    x.code = "4";//respCode(1);
                    x.fetch = read_me;//respMessage(0).ToString();//"Barcode is already in use";
                }
                else
                {
                    _sql.Connection3();
                    _sql.OpenConn3();
                    _sql.BeginTransax3();

                    // this one is suppose to be the third query to executed; this was one was moved due multiple transaction 
                    // that will be going on ahead, and this table is used as the reference for every transaction for barcodes


                    _sql.commandTraxParam3("Insert into ASYS_BarcodeHistory(lotno,refallbarcode,allbarcode,itemcode,Description,karat,carat,weight," +
                     "currency,empname,custodian,trandate,costcenter,status)values ('" + Lotno + "','" + barcode + "','" + barcode + "','" + itemCode +
                     "','" + Description + "','" + Karat + "','" + carat + "','" + weight + "','PHP','" + userLog + "','" + userLog +
                     "',getdate(),'REM','RECEIVED')");
                    _sql.Execute3();
                    //1st
                    _sql.commandTraxParam3("Insert into rems" + DB + ".dbo.ASYS_REMOutSource_detail(Reflotno,lotno,refallbarcode,allbarcode," +
                        "refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc,all_karat,all_carat" +
                        ",all_weight,price_desc,price_karat,price_weight,price_carat,sortdate,sortername,Receivedate,Receiver,status)" +
                        " values ('" + Lotno + "','" + Lotno + "','" + barcode + "','" + barcode + "','" + itemCode + "','" + itemCode + "','" + Description +
                        "','" + Quantity + "','" + Quantity + "','" + Karat + "','" + carat + "','" + weight + "','" + comboValue + "','O','" + Description +
                        "','" + Karat + "','" + carat + "','" + weight + "','" + Description + "','" + Karat + "','" + weight + "','" + carat + "',getdate(),'" + userLog +
                        "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute3();
                    //2nd
                    _sql.commandTraxParam3("Insert into ASYS_REMOutSource_detail(Reflotno,lotno,refallbarcode,allbarcode,refitemcode,itemcode," +
                        "branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,alL_desc,all_karat,all_carat,all_weight," +
                        "price_desc,price_karat,price_weight,price_carat,sortdate,sortername,Receivedate,Receiver,status)" +
                        " values ('" + Lotno + "','" + Lotno + "','" + barcode + "','" + barcode + "','" + itemCode + "','" + itemCode +
                        "','" + Description + "','" + Quantity + "','" + Quantity + "','" + Karat + "','" + carat + "','" + weight + "','" + comboValue +
                        "','O','" + Description + "','" + Karat + "','" + carat + "','" + weight + "','" + Description + "','" + Karat +
                        "','" + weight + "','" + carat + "',getdate(),'" + userLog + "',getdate(),'" + userLog + "','RECEIVED')");
                    _sql.Execute3();
                    //3rd
                 

                    log.Info("SaveRemItem2: " + respMessage(1) + " | Lotno: " + Lotno + " | userLog: " + userLog + " | DB: " + DB);
                    _sql.commitTransax3();
                   //_sql.RollBackTrax3();
                    _sql.CloseConn3();
                    x.code = respCode(1);///"0";
                    x.fetch = respMessage(1);//"Data saved";
 
                }

                return new RespALLResult { responseCode = x.code, responseMsg = x.fetch };//= respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("SaveRemItem2: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "SaveRemItem2: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult checkRemDetail(string DB, string barcode)
        {
            var _sql = new Connection();
            var models = new saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection1(DB);
                _sql.OpenConn1();
                _sql.commandExeParam1("SELECT a.allbarcode FROM REMS" + DB + ".dbo.ASYS_REMOutSource_detail a INNER JOIN " +
                    "[REMS].dbo.ASYS_REMOutSource_detail b ON a.ALLbarcode = b. Refallbarcode WHERE a.allbarcode = '" + barcode + "'");
                rdr = _sql.ExecuteDr1();
                if (rdr.Read())
                {
                    models.allreturns = true;
                    log.Info("checkRemDetail: " + models.allreturns);
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = "Success.", saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderdata = models };
                }
                else
                {
                    log.Info("checkRemDetail: " + respMessage(2));
                    _sql.CloseDr1();
                    _sql.CloseConn1();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("checkRemDetail: " + respMessage(0) + ex.Message);
                _sql.CloseConn1();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "checkRemDetail: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult saveTBLReceiving(string lotno, string userLog, string barcode, string branchCode, string PTN, string temp, string ItemCode,
            string description, int quantity, int karat, double carat, string SortList, double weight, double itemApp)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                //1st
                _sql.commandTraxParam3("insert into ASYS_REMOutSource_Header (Lotno,ReceiveDate,Receiver,type) values ('" + lotno +
                    "',getdate(),'" + userLog + "','OUTSOURCE')");
                _sql.Execute3();
                //2nd
                _sql.commandTraxParam3("insert into ASYS_REMOutSource_Detail(LOTNO_DFOR,ALLBArcode_dfor,DIVISION_DFOR,PTN_DFOR,FOR_ID_DFOR," +
                    "MPTN_DFOR,ITEMCODE_DFOR,PTNITEMDESC_DFOR,QUANTITY_DFOR,KARATGRADING_DFOR,CARATSIZE_DFOR,SORTINGCLASS_DFOR,WEIGHT_DFOR," +
                    "All_wt_dfor,All_carat_dfor,ITEMAPPRAISEVALUE_DFOR,oSTATUS_DFOR)VALUES('" + lotno + "','" + barcode + "','" +
                    branchCode + "','" + PTN + "','" + temp + "','" + PTN + "','" + ItemCode + "','" + description + "','" + quantity +
                    "','" + karat + "','" + carat + "','" + SortList + "','" + weight + "','" + weight + "','" + carat + "','" +
                   itemApp + "','1')");
                _sql.Execute3();

                log.Info("saveTBLReceiving: " + respMessage(1) + " | lotno: " + lotno + " | userLog: " + userLog);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("saveTBLReceiving: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "saveTBLReceiving: " + respMessage(0) + ex.Message };
            }
        }

        //-------------------------------------REM ITEM RECEIVING END--------------------------//
        #endregion
        #region February 8,2017
        [WebMethod]
        public RespALLResult ExeGenMaxBarcodeCon3(string barcode)
        {
            var _sql = new Connection();
            var models = new ExeMaxGenModels();
            type2 = 1;
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeStoredParam3("ASYS_GetMaxBarcode");
                _sql.RetrieveInfoParamss3("@allbarcode", "@type", barcode, type2);
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.barcode = Convert.ToInt32(rdr["Barcode"]) + 1;

                    log.Info("ExeGenMaxBarcodeCon3: " + models.barcode);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ExeMaxGendata = models };
                }
                else
                {
                    log.Info("ExeGenMaxBarcodeCon3: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("ExeGenMaxBarcodeCon3: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult REMGenerateLot(string DB, string userLog, string comboValue)
        {
            var _sql = new Connection();
            var models = new ReleasingRPTLoadModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("if (select Count([REMS" + DB + "].dbo.ASYS_REM_Detail.reflotno) as lotno from" +
                    " [REMS" + DB + "].dbo.ASYS_REM_Detail  where ( [REMS" + DB + "].dbo.ASYS_REM_detail.SorterName = '" + userLog + "') " +
                    "AND ( [REMS" + DB + "].dbo.ASYS_REM_detail.actionclass = '" + comboValue + "') and (CAST(SUBSTRING( " +
                    "[REMS" + DB + "].dbo.ASYS_REM_Detail.RefLotno, 5, 2) AS int(2)) = MONTH(GETDATE())) AND (CAST(SUBSTRING(" +
                    " [REMS" + DB + "].dbo.ASYS_REM_Detail.RefLotno, 1, 4) AS int(2)) = YEAR(GETDATE()))) = 0 begin if (Select Count(LOTNO)" +
                    " as lotno from REMS.dbo.ASYS_LOTNO_gen  where  (CAST(SUBSTRING(Lotno, 5, 2) AS int(2)) = MONTH(GETDATE())) and " +
                    "(CAST(SUBSTRING(LOTNO, 1, 4) AS int(2)) = YEAR(GETDATE()))) = 0 begin select  Cast(YEar(getdate())" +
                    " as char(4)) + Cast(REPLACE(STR(month(getdate()), 2, 0), SPACE(1), '0')  as char(2)) + '0001' LOTNO " +
                    "from [REMS].dbo.ASYS_LOTNO_Gen end else select LOTNO from [REMS].dbo.ASYS_lotno_Gen end  else select " +
                    "Isnull(Max(reflotno),0) as lotno from [REMS" + DB + "].dbo.ASYS_REM_Detail  where" +
                    " ( [REMS" + DB + "].dbo.ASYS_REM_detail.SorterName = '" + userLog + "') AND ( [REMS" + DB + "].dbo.ASYS_REM_detail.actionclass" +
                    " = '" + comboValue + "') and (CAST(SUBSTRING( [REMS" + DB + "].dbo.ASYS_REM_Detail.RefLotno, 5, 2) AS int(2))" +
                    " = MONTH(GETDATE())) AND (CAST(SUBSTRING( [REMS" + DB + "].dbo.ASYS_REM_Detail.RefLotno, 1, 4) AS int(2)) = YEAR(GETDATE()))");
                rdr = _sql.ExecuteDr3();
                if (rdr.Read() == true)
                {
                    models.reflotno = rdr["lotno"].ToString();
                    log.Info("REMGenerateLot: " + models.reflotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingRPTLoaddata = models };
                }
                else
                {
                    log.Info("REMGenerateLot: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("REMGenerateLot: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "REMGenerateLot: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult GenerateLot()
        {
            var _sql = new Connection();
            var models = new ReleasingRPTLoadModels();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select lotno from ASYS_LOTNO_Gen");
                rdr = _sql.ExecuteDr3();
                if (rdr.Read())
                {
                    models.reflotno = rdr["lotno"].ToString();
                    log.Info("GenerateLot: " + models.reflotno);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), ReleasingRPTLoaddata = models };
                }
                else
                {
                    log.Info("GenerateLot: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("GenerateLot: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "GenerateLot: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult BCodeItems(string DB)
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.RPTLoaddata = new RPTLoadModels();
            List.RPTLoaddata.actionType = new List<string>();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnm from REMS.dbo.vw_bedryf" + DB + " where dateend is null order by bedrnm");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.HasRows)
                {
                    while (rdr.Read() == true)
                    {
                        List.RPTLoaddata.actionType.Add(rdr["bedrnm"].ToString());
                    }
                    log.Info("BCodeItems: " + List.RPTLoaddata.actionType);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), RPTLoaddata = List.RPTLoaddata };
                }
                else
                {
                    log.Info("BCodeItems: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("BCodeItems: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "BCodeItems: " + respMessage(0) + ex.Message };
            }
        }//DONE
        [WebMethod]
        public RespALLResult DisplayBranchCode(string DB, string bCode)
        {
            var _sql = new Connection();
            var models = new CheckBranchNameREPORTModels();
            try
            {
                if (DB == "LNCR")
                {
                    DB = "NCR";
                }
                _sql.Connection0();
                _sql.OpenConn();
                _sql.commandExeParam("Select bedrnr from REMS.dbo.vw_bedryf" + DB + " where bedrnm='" + bCode + "' and dateend is null");//Modify query for split
                rdr = _sql.ExecuteDr();
                if (rdr.Read() == true)
                {
                    models.bedrnr = rdr["bedrnr"].ToString();

                    log.Info("DisplayBranchCode: " + models.bedrnr);
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), CheckBranchNameREPORTdata = models };
                }
                else
                {
                    log.Info("DisplayBranchCode: " + respMessage(2));
                    _sql.CloseDr();
                    _sql.CloseConn();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("DisplayBranchCode: " + respMessage(0) + ex.Message);
                _sql.CloseConn();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "DisplayBranchCode: " + respMessage(0) + ex.Message };
            }
        }//DONE
        #endregion
        //--------------------------------Feb 20,2017----------------------------//
      
        [WebMethod]
        public RespALLResult LoadGoldItem(bool prcing)
        {
            String command;
            var _sql = new Connection();
            var List = new RespALLResult();
            List.LoadGoldItemdata = new LoadGoldItemModels();
            List.LoadGoldItemdata.gold_karat = new List<string>();
            List.LoadGoldItemdata.plain = new List<string>();
            List.LoadGoldItemdata.mounted = new List<string>();
            List.LoadGoldItemdata.id = new List<string>();
            try
            {
                if (prcing == true)
                {
                    command = "Select id,gold_karat,plain,mounted from ASYS_GoldKarat";
                }
                else
                {
                    command = "Select id,gold_karat,plain,mounted from ASYS_GoldKarat_SUBASTA";
                }
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3(command);
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        List.LoadGoldItemdata.gold_karat.Add(rdr["gold_karat"].ToString());
                        List.LoadGoldItemdata.plain.Add(List.LoadGoldItemdata.isNULL(rdr["plain"]));
                        List.LoadGoldItemdata.mounted.Add(List.LoadGoldItemdata.isNULL(rdr["mounted"]));
                        List.LoadGoldItemdata.id.Add(rdr["id"].ToString());
                    }
                    log.Info("LoadGoldItem: " + respMessage(1) + " | " + List.LoadGoldItemdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadGoldItemdata = List.LoadGoldItemdata };
                }
                else
                {
                    log.Info("LoadGoldItem: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadGoldItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult EditGoldItem(double T1, double T2, string ListViewItem, string L3, bool pricing)
        {
            var _sql = new Connection();
            try
            {
                String table;
                String x = "";
                if (pricing == true)
                {
                    table = " ASYS_GOLDKARAT";
                }
                else
                {
                    table = " ASYS_GOLDKARAT_SUBASTA";
                }
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                _sql.commandTraxParam3("UPDATE"+x+ table+ "  SET PLAIN = " + T1 + ",MOUNTED = " + T2 +
                    " WHERE ID = '" + ListViewItem + "' AND GOLD_KARAT = '" + L3 + "' ");
                _sql.Execute3();

                log.Info("EditGoldItem: " + respMessage(1) + " | " + ListViewItem);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("EditGoldItem: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "EditGoldItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadDiamondPriceItem()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.DiamondPriceItemdata = new DiamondPriceItemModels();
            List.DiamondPriceItemdata.color_Type = new List<string>();
            List.DiamondPriceItemdata.vs = new List<string>();
            List.DiamondPriceItemdata.si = new List<string>();
            List.DiamondPriceItemdata.i = new List<string>();
            List.DiamondPriceItemdata.carat_wt_from = new List<string>();
            List.DiamondPriceItemdata.carat_wt_to = new List<string>();
            List.DiamondPriceItemdata.id = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select id,color_type,vs,si,i,carat_wt_from,carat_wt_to from ASYS_DiaPriceGuide");
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        List.DiamondPriceItemdata.color_Type.Add(rdr["color_Type"].ToString());
                        List.DiamondPriceItemdata.vs.Add(List.DiamondPriceItemdata.ifNull(rdr["vs"]));
                        List.DiamondPriceItemdata.si.Add(List.DiamondPriceItemdata.ifNull(rdr["si"]));
                        List.DiamondPriceItemdata.i.Add(rdr["i"].ToString());
                        List.DiamondPriceItemdata.carat_wt_from.Add(rdr["carat_wt_from"].ToString());
                        List.DiamondPriceItemdata.carat_wt_to.Add(rdr["carat_wt_to"].ToString());
                        List.DiamondPriceItemdata.id.Add(rdr["id"].ToString());
                    }
                    log.Info("LoadDiamondPriceItem: " + respMessage(1) + " | " + List.DiamondPriceItemdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), DiamondPriceItemdata = List.DiamondPriceItemdata };
                }
                else
                {
                    log.Info("LoadDiamondPriceItem: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }

            }
            catch (Exception ex)
            {
                log.Error("LoadDiamondPriceItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult EditDiamondPriceItem(double vs, double si, double i, string ListViewItem, string colorType)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                _sql.commandTraxParam3("UPDATE ASYS_DiaPriceGuide SET VS = " + vs + ",SI = " + si + ",I = " + i +
                    " WHERE ID = '" + ListViewItem + "' AND color_type = '" + colorType + "' ");
                _sql.Execute3();

                log.Info("EditDiamondPriceItem: " + respMessage(1) + " | " + ListViewItem);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("EditDiamondPriceItem: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "EditDiamondPriceItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadPearlPriceItem()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.LoadPearlPriceItemdata = new LoadPearlPriceItemModels();
            List.LoadPearlPriceItemdata.pearl_type = new List<string>();
            List.LoadPearlPriceItemdata.verygood = new List<string>();
            List.LoadPearlPriceItemdata.good = new List<string>();
            List.LoadPearlPriceItemdata.poor = new List<string>();
            List.LoadPearlPriceItemdata.id = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select id,pearl_type,verygood,good,poor from ASYS_PearlGuide");
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        List.LoadPearlPriceItemdata.pearl_type.Add(rdr["pearl_type"].ToString());
                        List.LoadPearlPriceItemdata.verygood.Add(List.LoadPearlPriceItemdata.ifNull(rdr["verygood"]));
                        List.LoadPearlPriceItemdata.good.Add(List.LoadPearlPriceItemdata.ifNull(rdr["good"]));
                        List.LoadPearlPriceItemdata.poor.Add(rdr["poor"].ToString());
                        List.LoadPearlPriceItemdata.id.Add(rdr["id"].ToString());
                    }
                    log.Info("LoadPearlPriceItem: " + respMessage(1) + " | " + List.LoadPearlPriceItemdata);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadPearlPriceItemdata = List.LoadPearlPriceItemdata };
                }
                else
                {
                    log.Info("LoadPearlPriceItem: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadPearlPriceItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadPearlPriceItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult EditPearlPriceItem(double vg, double g, double p, string ListViewItem, string pearType)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                _sql.commandTraxParam3("UPDATE ASYS_PearlGuide SET verygood = " + vg + ",good = " + g + ",poor = " + p + " WHERE ID = '" + ListViewItem + "' AND pearl_type = '" + pearType + "' ");
                _sql.Execute3();

                log.Info("EditPearlPriceItem: " + respMessage(1) + " | " + ListViewItem);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("EditPearlPriceItem: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "EditPearlPriceItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult LoadSonePriceItem()
        {
            var _sql = new Connection();
            var List = new RespALLResult();
            List.LoadSonePriceItem = new PearlPriceItemModels();
            List.LoadSonePriceItem.colstone_type = new List<string>();
            List.LoadSonePriceItem.colstoneverygood = new List<string>();
            List.LoadSonePriceItem.colstonegood = new List<string>();
            List.LoadSonePriceItem.colstonepoor = new List<string>();
            List.LoadSonePriceItem.id = new List<string>();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.commandExeParam3("Select id,colstone_type,colstoneverygood,colstonegood,colstonepoor from ASYS_ColStoneGuide");
                rdr = _sql.ExecuteDr3();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        List.LoadSonePriceItem.colstone_type.Add(rdr["colstone_type"].ToString());
                        //List.LoadSonePriceItem.colstoneverygood.Add(rdr["colstoneverygood"].ToString());
                        //List.LoadSonePriceItem.colstonegood.Add(rdr["colstonegood"].ToString());
                        List.LoadSonePriceItem.colstoneverygood.Add(List.LoadPearlPriceItemdata.ifNull(rdr["colstoneverygood"]));
                        List.LoadSonePriceItem.colstonegood.Add(List.LoadPearlPriceItemdata.ifNull(rdr["colstonegood"]));
                        List.LoadSonePriceItem.colstonepoor.Add(rdr["colstonepoor"].ToString());
                        List.LoadSonePriceItem.id.Add(rdr["id"].ToString());
                    }
                    log.Info("LoadSonePriceItem: " + respMessage(1) + " | " + List.LoadSonePriceItem);
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), LoadSonePriceItem = List.LoadSonePriceItem };
                }
                else
                {
                    log.Info("LoadSonePriceItem: " + respMessage(2));
                    _sql.CloseDr3();
                    _sql.CloseConn3();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadSonePriceItem: " + respMessage(0) + ex.Message);
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "LoadSonePriceItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult EditSonePriceItem(double vg, double g, double p, string ListViewItem, string stoneType)
        {
            var _sql = new Connection();
            try
            {
                _sql.Connection3();
                _sql.OpenConn3();
                _sql.BeginTransax3();
                _sql.commandTraxParam3("UPDATE ASYS_ColStoneGuide SET colstoneverygood = " + vg + ",colstonegood = " + g + ",colstonepoor = " + p + " WHERE ID = '" + ListViewItem + "' AND colstone_type = '" + stoneType + "' ");
                _sql.Execute3();

                log.Info("EditSonePriceItem: " + respMessage(1) + " | " + ListViewItem);
                _sql.commitTransax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) };
            }
            catch (Exception ex)
            {
                log.Error("EditSonePriceItem: " + respMessage(0) + ex.Message);
                _sql.RollBackTrax3();
                _sql.CloseConn3();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "EditSonePriceItem: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult REM_COSTING(string whichClass, string barcode)
        {
            var models = new REMCOStingModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                if (whichClass == "OVERAPPRAISED")
                {
                    _sql.commandExeParam4("SELECT ALL_Weight, ALL_Karat,ALL_Cost FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode + "' AND status <> 'RECMLWB'");
                }
                else if (whichClass == "GOODSTOCK")
                {
                    _sql.commandExeParam4("SELECT Gold_Cost, Mount_Cost, YG_Cost, WG_Cost, ALL_Cost FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode + "' AND status <> 'RECMLWB'");
                }
                else if (whichClass == "TAKENBACK")
                {
                    _sql.commandExeParam4("SELECT  ALL_Cost FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode + "' AND status <> 'RECMLWB'");
                }
                else if (whichClass == "CELLULAR")
                {
                    _sql.commandExeParam4("SELECT Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode + "' AND status <> 'RECMLWB'");
                }
                else
                {
                    _sql.commandExeParam4("SELECT Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost FROM ASYS_REM_Detail WHERE refallbarcode ='" + barcode + "' AND status <> 'RECMLWB'");
                }
                rdr = _sql.ExecuteDr4();
                if (rdr.Read())
                {
                    if (whichClass == "OVERAPPRAISED")
                    {
                        models.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                        models.ALL_Karat = string.IsNullOrEmpty(rdr["ALL_Karat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Karat"]);
                        models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                    }
                    else if (whichClass == "GOODSTOCK")
                    {
                        models.Gold_Cost = string.IsNullOrEmpty(rdr["Gold_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Gold_Cost"]);
                        models.Mount_Cost = string.IsNullOrEmpty(rdr["Mount_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Mount_Cost"]);
                        models.YG_Cost = string.IsNullOrEmpty(rdr["YG_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["YG_Cost"]);
                        models.WG_Cost = string.IsNullOrEmpty(rdr["WG_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["WG_Cost"]);
                        models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                    }
                    else if (whichClass == "TAKENBACK")
                    {
                        models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                    }
                    else if (whichClass == "CELLULAR")
                    {
                        models.Cellular_Cost = string.IsNullOrEmpty(rdr["Cellular_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Cellular_Cost"]);
                        models.Repair_Cost = string.IsNullOrEmpty(rdr["Repair_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Repair_Cost"]);
                        models.Cleaning_Cost = string.IsNullOrEmpty(rdr["Cleaning_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Cleaning_Cost"]);
                        models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                    }
                    else
                    {
                        models.Watch_Cost = string.IsNullOrEmpty(rdr["Watch_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Watch_Cost"]);
                        models.Repair_Cost = string.IsNullOrEmpty(rdr["Repair_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Repair_Cost"]);
                        models.Cleaning_Cost = string.IsNullOrEmpty(rdr["Cleaning_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["Cleaning_Cost"]);
                        models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);
                    }
                    log.Info("REM_COSTING: " + respMessage(1) + " | " + models);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), REMCOStingdata = models };
                }
                else
                {
                    log.Info("REM_COSTING: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("REM_COSTING: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "REM_COSTING: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult OUTSOURCE_OA(string barcode)
        {
            var models = new REMCOStingModels();
            var _sql = new Connection();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
                _sql.commandExeParam4("SELECT ALL_Weight, ALL_Karat,ALL_Cost FROM ASYS_REMOUTSOURCE_Detail WHERE refallbarcode ='" + barcode +
                    "' AND status <> 'RECMLWB'");
                rdr = _sql.ExecuteDr4();
                if (rdr.Read())
                {
                    models.ALL_Weight = string.IsNullOrEmpty(rdr["ALL_Weight"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Weight"]);
                    models.ALL_Karat = string.IsNullOrEmpty(rdr["ALL_Karat"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Karat"]);
                    models.ALL_Cost = string.IsNullOrEmpty(rdr["ALL_Cost"].ToString()) ? 0 : Convert.ToDouble(rdr["ALL_Cost"]);

                    log.Info("OUTSOURCE_OA: " + models.ALL_Weight + " | " + models.ALL_Karat + " | " + models.ALL_Cost);
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), REMCOStingdata = models };
                }
                else
                {
                    log.Info("OUTSOURCE_OA: " + respMessage(2));
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                    return new RespALLResult { responseCode = respCode(0), responseMsg = respMessage(2) };
                }
            }
            catch (Exception ex)
            {
                log.Error("OUTSOURCE_OA: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "OUTSOURCE_OA: " + respMessage(0) + ex.Message };
            }
        }
        private string respMessage(int msg)
        {
            string message = "";
            switch (msg)
            {
                case 0:
                    return message = "Service Error: ";
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
        //--------------------------------Feb 20,2017----------------------------//
        //-------------------------------Stephen Asi-----------------------------------//
        [WebMethod]
        public _ClickInfo x(string code, string name, bool branch)
        {
            //bool branch = false;
            var _sql = new Connection();
            var x = new _ClickInfo();

            try
            {

                _sql.Connection4();
                _sql.OpenConn4();
                #region CONDITION
                #region IF
                if (branch == true)
                {
                    x.query = "SELECT branchcode, branchname, branchaddress, xmlabbr FROM ASYS_BranchAddress WHERE branchcode = '" + code + "' and branchname = '" + name + "'";
                    _sql.commandTraxParam4(x.query);
                    _sql.x = _sql.ExecuteDr4();
                    while (_sql.x.Read())
                    {
                        x.branchCode = (string.IsNullOrEmpty(_sql.x["branchcode"].ToString()) ? "NULL" : _sql.x["branchcode"].ToString().Trim());
                        x.branchName = (string.IsNullOrEmpty(_sql.x["branchname"].ToString()) ? "NULL" : _sql.x["branchname"].ToString().Trim());
                        x.branchAddress = (string.IsNullOrEmpty(_sql.x["branchaddress"].ToString()) ? "NULL" : _sql.x["branchaddress"].ToString().Trim());
                        x.xmlCode = (string.IsNullOrEmpty(_sql.x["XMLAbbr"].ToString()) ? "NULL" : _sql.x["XMLAbbr"].ToString().Trim());

                    }
                    _sql.CloseConn4();
                    return new _ClickInfo { resMsg = "AHA!", resCode = "1", branchCode = x.branchCode, branchName = x.branchName, branchAddress = x.branchAddress, xmlCode = x.xmlCode };
                }
                #endregion
                #region ELSE
                else
                {
                    x.query = "SELECT CustomerID, CustomerFname, CustomerMName, CustomerLname, CustomerAddress FROM ASYS_CreateCustInfo WHERE CustomerID = " + code + "";
                    _sql.commandTraxParam4(x.query);
                    _sql.x = _sql.ExecuteDr4();
                    while (_sql.x.Read())
                    {
                        x.CustId = (string.IsNullOrEmpty(_sql.x["CustomerID"].ToString()) ? "NULL" : _sql.x["CustomerID"].ToString().Trim());
                        x.firstName = (string.IsNullOrEmpty(_sql.x["CustomerFname"].ToString()) ? "NULL" : _sql.x["CustomerFname"].ToString().Trim());
                        x.mi = (string.IsNullOrEmpty(_sql.x["CustomerMName"].ToString()) ? "NULL" : _sql.x["CustomerMName"].ToString().Trim());
                        x.lastName = (string.IsNullOrEmpty(_sql.x["CustomerLname"].ToString()) ? "NULL" : _sql.x["CustomerLname"].ToString().Trim());
                        x.custAddress = (string.IsNullOrEmpty(_sql.x["CustomerAddress"].ToString()) ? "NULL" : _sql.x["CustomerAddress"].ToString().Trim());

                    }
                    _sql.CloseConn4();
                    return new _ClickInfo { resMsg = "AHA!", resCode = "2", CustId = x.CustId, firstName = x.firstName, mi = x.mi, lastName = x.lastName, custAddress = x.custAddress };
                }
                #endregion


                #endregion


            }
            catch (Exception ex)
            {
                log.Error("exists: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();

                return new _ClickInfo { resMsg = ex.Message, resCode = "0" };
            }

        }
        [WebMethod]
        public Create_Brancha_And_Customer ViewBranchorCustInfo(bool branch)
        {

            var _sql = new Connection();
            var x = new Create_Brancha_And_Customer();
            x.code = new List<string>();
            x.name = new List<string>();
            x.address = new List<string>();
            try
            {

                _sql.Connection4();
                _sql.OpenConn4();
                #region CONDITION
                if (branch == true)
                {
                    x.query = "SELECT branchcode AS code, branchname AS name, branchaddress as address FROM ASYS_BranchAddress ORDER BY branchcode";
                }
                else
                {
                    x.query = "SELECT customerid AS code, CustomerFname + ' ' + CustomerLname AS name, customeraddress as address FROM ASYS_CreateCustinfo ORDER BY customerfname,CustomerLname";
                }

                #endregion
                _sql.commandTraxParam4(x.query);
                _sql.x = _sql.ExecuteDr4();
                while (_sql.x.Read())
                {
                    x.code.Add(string.IsNullOrEmpty(_sql.x["code"].ToString()) ? "NULL" : _sql.x["code"].ToString().Trim());
                    x.name.Add(string.IsNullOrEmpty(_sql.x["name"].ToString()) ? "NULL" : _sql.x["name"].ToString().Trim());
                    x.address.Add(string.IsNullOrEmpty(_sql.x["address"].ToString()) ? "NULL" : _sql.x["address"].ToString().Trim());
                }
                _sql.CloseConn4();
            }
            catch (Exception ex)
            {
                log.Error("exists: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();

                return new Create_Brancha_And_Customer { resMsg = ex.Message, resCode = "0" };
            }
            return new Create_Brancha_And_Customer { resMsg = "AHA!", resCode = "1", code = x.code, name = x.name, address = x.address };
        }

        #endregion 
        [WebMethod]
        public RespALLResult frmEdit_Load()
        {
            var models = new REMCOStingModels();
            var _sql = new Connection();
            var _list = new RespALLResult();
            _list.action_type = new List<string>();
            try
            {
                _sql.Connection4();
                _sql.OpenConn4();
               
                    _sql.commandExeParam4("SELECT action_type, action_id FROM REMS.DBO.tbl_action WHERE action_id in(3,4,5,6,8,9,10,11) ORDER BY action_type");
               
                
                rdr = _sql.ExecuteDr4();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        _list.action_type.Add(rdr["action_type"].ToString());
                    }
                    _sql.CloseDr4();
                    _sql.CloseConn4();
                   
                }
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), action_type = _list.action_type };
            }

            catch (Exception ex)
            {
                log.Error("REM_COSTING: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "REM_COSTING: " + respMessage(0) + ex.Message };
            }
        }
        [WebMethod]
        public RespALLResult frmEdit_Load_ptn(string ptn, String db)
        {
            var models = new REMCOStingModels();
            var _sql = new Connection();
            var _list = new edit_();
         
           
            try
            {
                if (db == "LNCR")
                {
                    db = "NCR";
                }
                _sql.Connection4();
                _sql.OpenConn4();

                _sql.commandExeParam4("SELECT BranchCode,BranchName,Ptn,PtnBarcode,Loanvalue,transdate,pulloutdate,custId,custname,custaddress,custcity,custgender,custtel,maturitydate,expirydate,loandate FROM rems" + db + ".dbo.ASYS_REM_Header WHERE ptn ='" + ptn + "'");

                
                rdr = _sql.ExecuteDr4();
                rdr.Read();
                if (rdr.HasRows)
                {
                    //while (rdr.Read())
                    //{


                    _list.BranchCode = (string.IsNullOrEmpty(rdr["BranchCode"].ToString()) ? "NULL" : rdr["BranchCode"].ToString().Trim());//(rdr["BranchCode"].ToString());
                    _list.BranchName = (string.IsNullOrEmpty(rdr["BranchName"].ToString()) ? "NULL" : rdr["BranchName"].ToString().Trim());//(rdr["BranchName"].ToString());
                    _list.Ptn = (string.IsNullOrEmpty(rdr["Ptn"].ToString()) ? "NULL" : rdr["Ptn"].ToString().Trim());//(rdr["Ptn"].ToString());
                    _list.PtnBarcode = (string.IsNullOrEmpty(rdr["PtnBarcode"].ToString()) ? "NULL" : rdr["PtnBarcode"].ToString().Trim());//(rdr["PtnBarcode"].ToString());
                    _list.LoanValue = (string.IsNullOrEmpty(rdr["LoanValue"].ToString()) ? "0.00" : rdr["LoanValue"].ToString().Trim());//(rdr["LoanValue"].ToString());
                    _list.transdate = (string.IsNullOrEmpty(rdr["transdate"].ToString()) ? "NULL" : rdr["transdate"].ToString().Trim());//(rdr["transdate"].ToString());
                    _list.pulloutdate = (string.IsNullOrEmpty(rdr["pulloutdate"].ToString()) ? "NULL" : rdr["pulloutdate"].ToString().Trim());//(rdr["pulloutdate"].ToString());
                    _list.custId = (string.IsNullOrEmpty(rdr["custId"].ToString()) ? "NULL" : rdr["custId"].ToString().Trim());//(rdr["custId"].ToString());
                    _list.custName = (string.IsNullOrEmpty(rdr["custName"].ToString()) ? "NULL" : rdr["custName"].ToString().Trim());//(rdr["custName"].ToString());
                    _list.custAddress = (string.IsNullOrEmpty(rdr["custAddress"].ToString()) ? "NULL" : rdr["custAddress"].ToString().Trim());//(rdr["custAddress"].ToString());
                    _list.custCity = (string.IsNullOrEmpty(rdr["custCity"].ToString()) ? "NULL" : rdr["custCity"].ToString().Trim());//(rdr["custCity"].ToString());
                    _list.custGender = (string.IsNullOrEmpty(rdr["custGender"].ToString()) ? "NULL" : rdr["custGender"].ToString().Trim());//(rdr["custGender"].ToString());
                    _list.custtel = (string.IsNullOrEmpty(rdr["custtel"].ToString()) ? "NULL" : rdr["custtel"].ToString().Trim());//(rdr["custtel"].ToString());
                    _list.maturitydate = (string.IsNullOrEmpty(rdr["maturitydate"].ToString()) ? "NULL" : rdr["maturitydate"].ToString().Trim());//(rdr["maturitydate"].ToString());
                    _list.expirydate = (string.IsNullOrEmpty(rdr["expirydate"].ToString()) ? "NULL" : rdr["expirydate"].ToString().Trim());//(rdr["expirydate"].ToString());
                    _list.loandate = (string.IsNullOrEmpty(rdr["loandate"].ToString()) ? "NULL" : rdr["loandate"].ToString().Trim());//(rdr["loandate"].ToString());//


                    //}
                    _sql.CloseDr4();
                    _sql.CloseConn4();

                }
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1) , edit = _list };
            }

            catch (Exception ex)
            {
                log.Error("REM_COSTING: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "REM_COSTING: " + respMessage(0) + ex.Message };
            }
        }//DONE
        public bool red;
        public String read_me;
        [WebMethod]
        public bool check_entries(String allBarcode)
        {
            try
            {
                var _sql = new Connection();
            var entry = new entries();
            _sql.Connection4();
            _sql.OpenConn4();


            entry.query = "SELECT ID FROM REMS.DBO.ASYS_BARCODEHISTORY WHERE REFALLBARCODE = '" + allBarcode + "'";
            
            

            _sql.commandTraxParam4(entry.query);
            _sql.x = _sql.ExecuteDr4();
           if (_sql.x.Read())
            {
                entry.fetch = "data found";
                entry.code = "1";
                return true;
            }
            else
            {
                entry.fetch = "no data found";
                entry.code = "0";
                return false;
            }
            }
            catch (Exception x)
            {
                read_me = x.Message;
                red = true;
                return false; 
            }

            //return new entries { fetch = entry.fetch, code = entry.code};
        }
        [WebMethod]
        public RespALLResult frmEdit_Load_exist(string txtptn, String zone) //
        {
            var models = new REMCOStingModels();
            var _sql = new Connection();
            var _list = new RespALLResult();
           _list.results = new ptn();
            _list.results.refallbarcode = new List<string>();
            _list.results.reflotno = new List<string>();
            _list.results.id = new List<string>(); 
            _list.results.branchitemdesc = new List<string>(); 
            _list.results.refqty = new List<string>();
            _list.results.karatgrading = new List<string>();
            _list.results.caratsize = new List<string>();
            _list.results.weight = new List<string>();
            _list.results.actionclass = new List<string>(); 
            _list.results.sortcode = new List<string>();
            _list.results.all_karat = new List<string>();
            _list.results.all_cost = new List<string>();
            _list.results.all_weight = new List<string>();
            _list.results.appraisevalue = new List<string>();
            _list.results.status= new List<string>();
            _list.results.refitemcode =new List<string>();
            _list.results.appraiser = new List<string>();

            try
            {
                if (zone == "LNCR")
                {
                    zone = "NCR";
                }
                _sql.Connection4();
                _sql.OpenConn4();

                _sql.commandExeParam4("SELECT reflotno,refallbarcode,itemid as id,refitemcode,branchitemdesc,refqty,karatgrading,caratsize,weight,actionclass,sortcode,all_karat,all_cost,all_weight,appraiser,appraisevalue,status FROM rems"+zone+".dbo.ASYS_REM_Detail WHERE ptn = '" + txtptn + "' and status = 'sorted' ORDER BY refitemcode ASC");


                rdr = _sql.ExecuteDr4();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        _list.results.refitemcode.Add(string.IsNullOrEmpty(rdr["refitemcode"].ToString()) ? "NULL" : rdr["refitemcode"].ToString());//(rdr["refitemcode"].ToString());
                        _list.results.reflotno.Add(string.IsNullOrEmpty(rdr["reflotno"].ToString()) ? "NULL" : rdr["reflotno"].ToString());//(rdr["reflotno"].ToString());
                        _list.results.refallbarcode.Add(string.IsNullOrEmpty(rdr["refallbarcode"].ToString()) ? "NULL" : rdr["refallbarcode"].ToString());//(rdr["refallbarcode"].ToString());
                        _list.results.id.Add(string.IsNullOrEmpty(rdr["id"].ToString()) ? "NULL" : rdr["id"].ToString());//(rdr["id"].ToString());
                        _list.results.branchitemdesc.Add(string.IsNullOrEmpty(rdr["branchitemdesc"].ToString()) ? "NULL" : rdr["branchitemdesc"].ToString());//(rdr["branchitemdesc"].ToString());
                        _list.results.refqty.Add(string.IsNullOrEmpty(rdr["refqty"].ToString()) ? "0.00" : rdr["refqty"].ToString());//(rdr["refqty"].ToString());
                        _list.results.karatgrading.Add(string.IsNullOrEmpty(rdr["karatgrading"].ToString()) ? "0.00" : rdr["karatgrading"].ToString());//(rdr["karatgrading"].ToString());
                        _list.results.caratsize.Add(string.IsNullOrEmpty(rdr["caratsize"].ToString()) ? "0" : rdr["caratsize"].ToString());//(rdr["caratsize"].ToString());
                        _list.results.weight.Add(string.IsNullOrEmpty(rdr["weight"].ToString()) ? "0.00" : rdr["weight"].ToString());//(rdr["weight"].ToString());
                        _list.results.actionclass.Add(string.IsNullOrEmpty(rdr["actionclass"].ToString()) ? "NULL" : rdr["actionclass"].ToString());//(rdr["actionclass"].ToString());
                        _list.results.sortcode.Add(string.IsNullOrEmpty(rdr["sortcode"].ToString()) ? "NULL" : rdr["sortcode"].ToString());//(rdr["sortcode"].ToString());
                        _list.results.all_karat.Add(string.IsNullOrEmpty(rdr["all_karat"].ToString()) ? "0.00" : rdr["all_karat"].ToString());//(rdr["all_karat"].ToString());
                        _list.results.all_cost.Add(string.IsNullOrEmpty(rdr["all_cost"].ToString()) ? "0.00" : rdr["all_cost"].ToString());//(rdr["all_cost"].ToString());
                        _list.results.all_weight.Add(string.IsNullOrEmpty(rdr["all_weight"].ToString()) ? "0.00" : rdr["all_weight"].ToString());//(rdr["all_weight"].ToString());
                        _list.results.appraisevalue.Add(string.IsNullOrEmpty(rdr["appraisevalue"].ToString()) ? "0" : rdr["appraisevalue"].ToString());//(rdr["appraisevalue"].ToString());
                        _list.results.status.Add(string.IsNullOrEmpty(rdr["status"].ToString()) ? "NULL" : rdr["appraiser"].ToString());//(rdr["status"].ToString());
                        _list.results.appraiser.Add(string.IsNullOrEmpty(rdr["appraiser"].ToString()) ? "NULL" : rdr["appraiser"].ToString());
                       // list.releaserlist.sur_name.Add(string.IsNullOrEmpty(dr["sur_name"].ToString()) ? "NULL" : dr["sur_name"].ToString().Trim());
                    }
                    _sql.CloseDr4();
                    _sql.CloseConn4();

                }
                return new RespALLResult { responseCode = respCode(1), responseMsg = respMessage(1), results = _list.results };
            }

            catch (Exception ex)
            {
                log.Error("REM_COSTING: " + respMessage(0) + ex.Message);
                _sql.CloseConn4();
                return new RespALLResult { responseCode = respCode(0), responseMsg = "REM_COSTING: " + respMessage(0) + ex.Message };
            }
        }//DONE




     
    }
}

