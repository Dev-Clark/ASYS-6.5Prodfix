using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace ASYSPricingService
{
    #region
    [WebService(Namespace = "http://tempuri.org/", Description =
    "A.L.L Jewelry Division", Name = "ASYS Web Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    #endregion
    public class ASYSPricingService : System.Web.Services.WebService
    {
        #region
        public string container, container2, container3, Item6;
        public string Item, Item2, Item3, Item4, Item5, Item12;
        public string Item7, Item8, Item9, Item10, Item11;
        public string Item13, Item14, Item15, Item16, Item17;
        public string Item18, Item19, Item20, Item21, Item22;
        public string Item23, Item24, Item25, Item26, Item27;
        public string Item28, Item29, Item30, Item31, Item32;
        public string Item33, Item34, Item35, Item36, Item37;
        public string Item38, Item39, Item40, Item41, Item42;
        public string Item43, Item44, Item45, Item46, Item47;
        public string Item48, Item49, Item50, x, z, columns, f;
        public string fetch, connection, value, value2, identifier;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region
        public ASYSPricingService()
        {
            XmlConfigurator.Configure();
        }
        #endregion


        #region FIRST REGION
        [WebMethod]
        public PricingResult Receiving_DisplayData(string reflotno, string refallbarcode)
        {
            log.Info("Receiving_DisplayData");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);

            try
            {

                if (reflotno == "" && refallbarcode == "")
                {
                    return new PricingResult { respMsg = "Please Check Entry" };
                }
                if (reflotno == "" && refallbarcode != "")
                {
                    return new PricingResult { respMsg = "Please Enter Reference LOTNUMBER" };
                }
                if (reflotno != "" && refallbarcode == "")
                {
                    return new PricingResult { respMsg = "Please Enter ALL BARCODE" };
                }
                else
                {
                    x = "REMS";
                    z = "Select refallbarcode as barcode, all_desc as alldesc, all_weight as weight, all_karat as karat, all_carat as carat, all_price as price from rems.dbo.asys_MLWB_detail  where reflotno =" + reflotno + " and refallbarcode= " + refallbarcode + "";
                    columns = "ptn";
                    pricingReceiving model = new pricingReceiving();
                    var List = new PricingResult();
                    List.data = new pricingReceiving();
                    List.data.barcode = new List<string>();
                    List.data.alldesc = new List<string>();
                    List.data.weight = new List<string>();
                    List.data.karat = new List<string>();
                    List.data.carat = new List<string>();
                    List.data.price = new List<string>();
                    using (conn)
                    {

                        conn.Open();
                        using (SqlCommand command = new SqlCommand(z, conn))
                        {
                            command.CommandTimeout = 400000;
                            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    List.data.barcode.Add(model.isNull(dr["barcode"]));
                                    List.data.alldesc.Add(model.isNull(dr["alldesc"]));
                                    List.data.weight.Add(model.isNull(dr["weight"]));
                                    List.data.karat.Add(model.isNull(dr["karat"]));
                                    List.data.carat.Add(model.isNull(dr["carat"]));
                                    List.data.price.Add(model.isNull(dr["price"]));
                                    fetch = "1";
                                }

                            }
                            else
                            {
                                fetch = "0";
                            }

                            conn.Close();
                            return new PricingResult { data = List.data, respCode = fetch };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                log.Error("Receiving_DisplayData:" + ex.Message);
                return new PricingResult { respMsg = "SORRY FOR THE INCOVENIENCE: Please Try to contact your Administrator:" + ex.Message };

            }
        }
        [WebMethod]
        public InsertLotnumberPHeader Receiving_InsertLotNumber(string LotNumber)
        {
            log.Info("Receiving_InsertLotNumber");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select * from Rems.dbo.asys_Pricing_header where lotno = '" + LotNumber + "'";
                com.Transaction = Trans;
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        identifier = "Data already exist";
                        fetch = "1";

                    }
                    dr.Close();
                    conn.Close();
                }

                else
                {
                    dr.Close();
                    com.CommandText = "insert into rems.dbo.asys_PRICING_header  (lotno) values ('" + LotNumber + "')";
                    com.Transaction = Trans;
                    com.ExecuteNonQuery();
                    Trans.Commit();
                    identifier = "DATA SAVED!";
                    conn.Close();
                }


                return new InsertLotnumberPHeader { LotNumber = identifier };
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Receiving_InsertLotNumber:" + ex.Message);
                return new InsertLotnumberPHeader { LotNumber = "transaction failed" + ex.Message };
            }

        }
        [WebMethod]
        public PricingResult3 Receiving_RetrieveData(string reflotno)
        {
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var data = new PricingResult3();
            try
            {
                conn.Open();
                if (reflotno == "")
                {
                    conn.Close();
                    return new PricingResult3 { fetch = "Please Enter LOTNUMBER" };
                }
                else
                {
                    SqlCommand com = conn.CreateCommand();
                    com.CommandTimeout = 400000;
                    com.CommandText = "Select ptn,refallbarcode as barcode, all_desc as alldesc, all_weight as weight, all_karat as karat, all_carat as carat, all_price as price from rems.dbo.asys_MLWB_detail  where reflotno =" + reflotno + " and status = 'released'";
                    com.ExecuteNonQuery();
                    SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr.HasRows)
                    {
                        dr.Close();
                        SqlDataAdapter adapter = new SqlDataAdapter("Select case when ptn is null then '0' else ptn end ptn,refallbarcode as barcode, all_desc as alldesc, all_weight as weight, all_karat as karat, all_carat as carat, case when all_price is null then '0' else all_price end all_price from rems.dbo.asys_MLWB_detail  where reflotno =" + reflotno + " and status = 'released'", conn);
                        DataSet fields = new DataSet();
                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        adapter.Fill(fields, "REMS");
                        data.result = "1";
                        conn.Close();
                        return new PricingResult3 { container2 = fields, result = data.result };
                    }

                    else
                    {
                        dr.Close();
                        data.result = "0";
                        data.fetch = "LOT NUMBER DOESN'T EXIST OR ALLREADY RECEIVE";
                        conn.Close();
                        return new PricingResult3 { fetch = data.fetch, result = data.result };
                    }

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                String error = "Sorry for this issue. May we request to contact your administrator about this:" + ex.ToString();
                return new PricingResult3 { fetch = error, result = "0" };
            }
        }
        [WebMethod]
        public PricingResult Receiving_RetrieveLotNumber()
        {
            log.Info("Receiving_RetrieveLotNumber");
            var list = new PricingResult();
            list.data = new pricingReceiving();
            list.data.lotNumber = new List<string>();
            x = "REMS";
            z = "select distinct reflotno from asys_mlwb_detail where status = 'RELEASED' and reflotno is not null and reflotno <> '' order by reflotno desc";
            //z = "select * from dbo.Msreplication_options where value = '0'";
            columns = "reflotno";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection connec = new SqlConnection(connection);
            try
            {

                using (connec)
                {
                    connec.Open();
                    using (SqlCommand command = new SqlCommand(z, connec))
                    {
                        command.CommandTimeout = 400000;

                        SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);


                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.data.lotNumber.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());
                                //list.data.lotNumber.Add(string.IsNullOrEmpty(dr["optname"].ToString()) ? "NULL" : dr["optname"].ToString().Trim());
                            }
                            fetch = "1";
                        }
                        else
                        {
                            fetch = "0";
                        }
                        connec.Close();
                        return new PricingResult { respCode = fetch, respMsg = "Successful", data = list.data };
                    }

                }
            }
            catch (Exception ex)
            {
                connec.Close();
                log.Error("Receiving_RetrieveLotNumber:" + ex.Message);
                return new PricingResult { respMsg = ex.Message, respCode = "0" };
            }

        }
        [WebMethod]
        public PricingResult5 Receiving_SaveData(string[][] DgEntry, string LogUser, string Lotno)
        {
            log.Info("Receiving_SaveData");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction tranCon;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            tranCon = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand("insert into rems.dbo.asys_PRICING_header  (lotno) values ('" + Lotno + "')", conn, tranCon))
                {
                    cmd.CommandTimeout = 400000;
                    cmd.ExecuteNonQuery();
                }
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                for (int i = 0; i <= DgEntry.Count() - 1; i++)
                {
                    if (DgEntry[i][1] == "0" || DgEntry[i][1] == ".00" || DgEntry[i][1] == "0.00" || DgEntry[i][1] == "NULL")
                    {
                        identifier = "RECEIVED";
                    }
                    else
                    {
                        identifier = "PRICED";
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO REMS.DBO.ASYS_BarcodeHistory (Lotno,refallbarcode, ALlBarcode,"
                                          + "itemcode, itemid, [description], karat, carat, weight, currency, price, cost, "
                                          + "empname, custodian, trandate, costcenter, status, SerialNO) select '" + Lotno + "','"
                                          + "" + DgEntry[i][0] + "', '" + DgEntry[i][0] + "', itemcode, itemid, all_desc, all_karat,"
                                          + "all_carat, all_weight, currency, all_price, all_cost, '" + LogUser + "', '" + LogUser + "', "
                                          + "getdate(),'PRICING', 'RECEIVED',SerialNO from rems.dbo.asys_MLWB_detail  where reflotno = '"
                                          + "" + Lotno + "' and refallbarcode = '" + DgEntry[i][0] + "' and status = 'RELEASED'", conn, tranCon))
                    {
                        cmd.CommandTimeout = 400000;
                        cmd.ExecuteNonQuery();
                    }




                    using (SqlCommand cmd = new SqlCommand("insert into rems.dbo.ASYS_Pricing_detail  (reflotno, LOTNO, refALLbarcode,"
                            + "allbarcode, ptn, itemid, ptnbarcode, branchcode, branchname, loanvalue,"
                            + "refitemcode, itemcode,branchitemdesc,SerialNo, refqty, qty, karatgrading,"
                            + "caratsize, weight, actionclass, sortcode, all_desc, all_karat, all_carat,"
                            + "all_cost, all_weight, appraisevalue, currency, photoname, price_desc,"
                            + "price_karat, price_weight, price_carat, all_price, cellular_cost, watch_cost,"
                            + "repair_cost, cleaning_cost, gold_cost, mount_cost, yg_cost, wg_cost,"
                            + "maturitydate, expirydate, loandate, receivedate,receiver,custodian,status)"
                            + "SELECT  RefLotno, refLotno as lotno, RefallBarcode,"
                            + "ALLbarcode, PTN, ItemID, PTNBarcode, BranchCode,"
                            + "BranchName, Loanvalue, RefItemcode, Itemcode,"
                            + "BranchItemDesc,SerialNo, RefQty, Qty, KaratGrading,"
                            + "CaratSize, Weight, ActionClass, SortCode, ALL_desc,"
                            + "ALL_karat, ALL_carat, ALL_Cost, ALL_Weight,AppraiseValue,"
                            + "Currency, PhotoName, Price_desc, Price_karat, Price_weight,"
                            + "Price_carat, ALL_price, Cellular_cost, Watch_cost, Repair_cost,"
                            + "Cleaning_cost, Gold_cost, Mount_cost, YG_cost, WG_cost,"
                            + " MaturityDate, ExpiryDate, LoanDate,getdate() as receivedate,"
                            + "'" + LogUser.Trim() + "' ,releaser as custodian,'" + identifier.Trim() + "'"
                            + "FROM  rems.dbo.ASYS_MLWB_detail"
                            + " WHERE     RefallBarcode = '" + DgEntry[i][0] + "' and status = 'RELEASED'", conn, tranCon))
                    {
                        cmd.CommandTimeout = 400000;
                        cmd.ExecuteNonQuery();
                    }



                    using (SqlCommand cmd = new SqlCommand("Update rems.dbo.asys_MLWB_detail  set status = 'RECPRICING' where refallbarcode='" + DgEntry[i][0] + "' and status = 'RELEASED'", conn, tranCon))
                    {
                        cmd.CommandTimeout = 400000;
                        cmd.ExecuteNonQuery();
                    }

                }
                tranCon.Commit();
                //tranCon.Rollback();
                conn.Close();

                return new PricingResult5 { LotNumber = "Items are Saved!", Result = "1", Respons = "Succesful" };



            }
            catch (Exception ex)
            {
                tranCon.Rollback();
                conn.Close();
                log.Error("Receiving_SaveData:" + ex.Message);
                return new PricingResult5 { LotNumber = "Transaction Failed" + ex.Message, Result = "0" };
            }


        }
        [WebMethod]
        public ViewReport Receiving_ViewReportReceived()
        {
            log.Info("Receiving_ViewReportReceived");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = "select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container = (string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim());
                    }
                    dr.Close();
                }



                conn.Close();

                return new ViewReport { Report = container, Respons = "Success" };

            }
            catch (Exception ex)
            {
                conn.Close();
                log.Error("Receiving_ViewReportReceived" + ex.Message);
                return new ViewReport { Report = ex.ToString() };

            }



        }
        [WebMethod]
        public RetrieveDatabyLotNumber Pricing_RetrieveDataByALLBarcode(string ALLbarcode)
        {
            log.Info("Pricing_RetrieveDataByALLBarcode");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = "SELECT refAllBarcode , ptn FROM ASYS_REM_Detail WHERE refallbarcode "
                             + "= '" + ALLbarcode + "'UNION SELECT refallbarcode , ptn FROM ASYS_REMOu"
                             + "tsource_detail WHERE refallbarcode = '" + ALLbarcode + "'";
                com.ExecuteNonQuery();
                z = "refallbarcode";
                columns = "ptn";

                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container = (string.IsNullOrEmpty(dr["refallbarcode"].ToString()) ? "NULL" : dr["refallbarcode"].ToString().Trim());
                        container2 = (string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim());
                        fetch = "1";
                    }
                    dr.Close();
                }
                else
                {

                    fetch = "0";
                }
                conn.Close();

                return new RetrieveDatabyLotNumber { Respons = container2, Result = fetch, AllBarcode = container };
            }
            catch (Exception ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveDataByALLBarcode" + ex.Message);
                return new RetrieveDatabyLotNumber { Respons = ex.Message };
            }
        }
        [WebMethod]
        public RetrieveDataByStatus Pricing_RetriveDataByStatus(string AllBarcode)
        {
            log.Info("Pricing_RetriveDataByStatus");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT GoldKaratPrice, MountedPrice, Gold_Karat FROM rems.dbo.ASYS_Pricing_detail WHERE RefallBarcode =@AllBarcode and status not in ('RELEASED','RECDISTRI')";
                com.Parameters.AddWithValue("AllBarcode", AllBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container = (string.IsNullOrEmpty(dr["GoldKaratPrice"].ToString()) ? "0.00" : dr["GoldKaratPrice"].ToString().Trim());
                        container2 = (string.IsNullOrEmpty(dr["MountedPrice"].ToString()) ? "0.00" : dr["MountedPrice"].ToString().Trim());
                        container3 = (string.IsNullOrEmpty(dr["Gold_Karat"].ToString()) ? "0.00" : dr["Gold_Karat"].ToString().Trim());
                        var value = decimal.Parse(container, CultureInfo.InvariantCulture).ToString("N");
                        var value2 = decimal.Parse(container2, CultureInfo.InvariantCulture).ToString("N");
                        Item = value.ToString();
                        Item2 = value2.ToString();
                        fetch = "1";
                    }

                    dr.Close();
                    conn.Close();
                }
                else
                {

                    fetch = "0";
                    dr.Close();
                    conn.Close();
                }
                return new RetrieveDataByStatus { GoldkaratPrice = Item, MountedPrice = Item2, Gold_Karat = container3, Respons = "Results", Result = fetch };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetriveDataByStatus" + Ex.Message);
                return new RetrieveDataByStatus { Respons = Ex.Message, Result = "0" };

            }

        }
        [WebMethod]
        public RetrieveCostedData Pricing_RetrieveCostedData(string ALLbarcode)
        {
            log.Info("Pricing_RetrieveCostedData");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT RefLotno, RefallBarcode, PTN, ItemID, PTNBarcode, BranchCode, BranchName, Loanvalue, RefItemcode,"
                + "Itemcode, BranchItemDesc, RefQty, Qty,  KaratGrading, CaratSize, Weight,Actionclass,Sortcode, ALL_desc,"
                + "SerialNo, ALL_karat, ALL_carat, ALL_Cost, ALL_Weight,currency, PhotoName,all_price,price_DESC,price_karat,"
                + "price_carat,price_weight, Cellular_cost, Watch_cost, Repair_cost,  Cleaning_cost, Gold_cost, Mount_cost,"
                + "YG_cost, WG_cost, MaturityDate, ExpiryDate, LoanDate, Status, GoldKaratPrice, MountedPrice, Gold_Karat"
                + " FROM dbo.ASYS_PRicing_detail where refallbarcode = @ALLbarcode and status not in ('RELEASED','RECDISTRI')";
                com.Parameters.AddWithValue("ALLbarcode", ALLbarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["RefLotno"].ToString()) ? "NULL" : dr["RefLotno"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["RefallBarcode"].ToString()) ? "NULL" : dr["RefallBarcode"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["PTN"].ToString()) ? "NULL" : dr["PTN"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ItemID"].ToString()) ? "NULL" : dr["ItemID"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["PTNBarcode"].ToString()) ? "NULL" : dr["PTNBarcode"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["BranchName"].ToString()) ? "NULL" : dr["BranchName"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["LoanValue"].ToString()) ? "0" : dr["LoanValue"].ToString().Trim());
                        Item8 = (string.IsNullOrEmpty(dr["RefItemcode"].ToString()) ? "NULL" : dr["RefItemcode"].ToString().Trim());
                        Item9 = (string.IsNullOrEmpty(dr["Itemcode"].ToString()) ? "NULL" : dr["Itemcode"].ToString().Trim());
                        Item10 = (string.IsNullOrEmpty(dr["BranchItemDesc"].ToString()) ? "NULL" : dr["BranchItemDesc"].ToString().Trim());
                        Item11 = (string.IsNullOrEmpty(dr["RefQty"].ToString()) ? "0" : dr["RefQty"].ToString().Trim());
                        Item12 = (string.IsNullOrEmpty(dr["Qty"].ToString()) ? "0" : dr["Qty"].ToString().Trim());
                        Item13 = (string.IsNullOrEmpty(dr["KaratGrading"].ToString()) ? "0" : dr["KaratGrading"].ToString().Trim());
                        Item14 = (string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? "0" : dr["CaratSize"].ToString().Trim());
                        Item15 = (string.IsNullOrEmpty(dr["Weight"].ToString()) ? "0" : dr["Weight"].ToString().Trim());
                        Item16 = (string.IsNullOrEmpty(dr["Actionclass"].ToString()) ? "NULL" : dr["Actionclass"].ToString().Trim());
                        Item17 = (string.IsNullOrEmpty(dr["Sortcode"].ToString()) ? "NULL" : dr["Sortcode"].ToString().Trim());
                        Item18 = (string.IsNullOrEmpty(dr["ALL_desc"].ToString()) ? "NULL" : dr["ALL_desc"].ToString().Trim());
                        Item19 = (string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "0" : dr["SerialNo"].ToString().Trim());
                        Item20 = (string.IsNullOrEmpty(dr["ALL_karat"].ToString()) ? "0" : dr["ALL_karat"].ToString().Trim());
                        Item21 = (string.IsNullOrEmpty(dr["ALL_carat"].ToString()) ? "0" : dr["ALL_carat"].ToString().Trim());
                        Item22 = (string.IsNullOrEmpty(dr["ALL_cost"].ToString()) ? "0" : dr["ALL_cost"].ToString().Trim());
                        Item23 = (string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? "0" : dr["ALL_Weight"].ToString().Trim());
                        Item24 = (string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString().Trim());
                        Item25 = (string.IsNullOrEmpty(dr["PhotoName"].ToString()) ? "NULL" : dr["PhotoName"].ToString().Trim());
                        Item26 = (string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());
                        Item27 = (string.IsNullOrEmpty(dr["price_DESC"].ToString()) ? "0" : dr["price_DESC"].ToString().Trim());
                        Item28 = (string.IsNullOrEmpty(dr["price_carat"].ToString()) ? "0" : dr["price_carat"].ToString().Trim());
                        Item29 = (string.IsNullOrEmpty(dr["price_weight"].ToString()) ? "0" : dr["price_weight"].ToString().Trim());
                        Item30 = (string.IsNullOrEmpty(dr["Cellular_cost"].ToString()) ? "0" : dr["Cellular_cost"].ToString().Trim());
                        Item31 = (string.IsNullOrEmpty(dr["Watch_cost"].ToString()) ? "0" : dr["Watch_cost"].ToString().Trim());
                        Item32 = (string.IsNullOrEmpty(dr["Repair_cost"].ToString()) ? "0" : dr["Repair_cost"].ToString().Trim());
                        Item33 = (string.IsNullOrEmpty(dr["Cleaning_cost"].ToString()) ? "0" : dr["Cleaning_cost"].ToString().Trim());
                        Item34 = (string.IsNullOrEmpty(dr["Gold_cost"].ToString()) ? "0" : dr["Gold_cost"].ToString().Trim());
                        Item35 = (string.IsNullOrEmpty(dr["Mount_cost"].ToString()) ? "0" : dr["Mount_cost"].ToString().Trim());
                        Item36 = (string.IsNullOrEmpty(dr["YG_cost"].ToString()) ? "0" : dr["YG_cost"].ToString().Trim());
                        Item37 = (string.IsNullOrEmpty(dr["WG_cost"].ToString()) ? "0" : dr["WG_cost"].ToString().Trim());
                        Item38 = (string.IsNullOrEmpty(dr["MaturityDate"].ToString()) ? "NULL" : dr["MaturityDate"].ToString().Trim());
                        Item39 = (string.IsNullOrEmpty(dr["ExpiryDate"].ToString()) ? "NULL" : dr["ExpiryDate"].ToString().Trim());
                        Item40 = (string.IsNullOrEmpty(dr["LoanDate"].ToString()) ? "NULL" : dr["LoanDate"].ToString().Trim());
                        Item41 = (string.IsNullOrEmpty(dr["Status"].ToString()) ? "NULL" : dr["Status"].ToString().Trim());
                        Item42 = (string.IsNullOrEmpty(dr["GoldKaratPrice"].ToString()) ? "0" : dr["GoldKaratPrice"].ToString().Trim());
                        Item43 = (string.IsNullOrEmpty(dr["MountedPrice"].ToString()) ? "0" : dr["MountedPrice"].ToString().Trim());
                        Item44 = (string.IsNullOrEmpty(dr["Gold_Karat"].ToString()) ? "0" : dr["Gold_Karat"].ToString().Trim());
                        Item45 = (string.IsNullOrEmpty(dr["BranchCode"].ToString()) ? "NULL" : dr["BranchCode"].ToString().Trim());
                        Item46 = (string.IsNullOrEmpty(dr["Price_karat"].ToString()) ? "0" : dr["Price_karat"].ToString().Trim());
                        var value = decimal.Parse(Item42, CultureInfo.InvariantCulture).ToString("N");
                        var value2 = decimal.Parse(Item43, CultureInfo.InvariantCulture).ToString("N");
                        var value3 = decimal.Parse(Item22, CultureInfo.InvariantCulture).ToString("N");
                        Item48 = value.ToString();
                        Item49 = value2.ToString();
                        Item50 = value3.ToString();
                        fetch = "1";

                    }

                    dr.Close();
                    conn.Close();
                }
                else
                {
                    fetch = "0";
                    conn.Close();
                }
                return new RetrieveCostedData
                {
                    Reflotno = Item,
                    RefallBarcode = Item2,
                    PTN = Item3,
                    ItemID = Item4,
                    PTNBarCode = Item5,
                    BranchName = Item6,
                    LoanValue = Item7,
                    RefItemcode = Item8,
                    Itemcode = Item9,
                    BranchItemDesc = Item10,
                    RefQty = Item11,
                    Qty = Item12,
                    KaratGrading = Item13,
                    Weight = Item15,
                    Actionclass = Item16,
                    Sortcode = Item17,
                    ALL_desc = Item18,
                    SerialNo = Item19,
                    ALL_karat = Item20,
                    ALL_carat = Item21,
                    ALL_cost = Item50,
                    ALL_Weight = Item23,
                    currency = Item24,
                    PhotoName = Item25,
                    all_price = Item26,
                    price_DESC = Item27,
                    price_carat = Item28,
                    price_weight = Item29,
                    Cellular_cost = Item30,
                    Watch_cost = Item31,
                    Repair_cost = Item32,
                    Cleaning_cost = Item33,
                    Gold_cost = Item34,
                    Mount_cost = Item35,
                    YG_cost = Item36,
                    WG_cost = Item37,
                    MaturityDate = Item38,
                    ExpiryDate = Item39,
                    LoanDate = Item40,
                    Status = Item41,
                    GoldKaratPrice = Item48,
                    MountedPrice = Item49,
                    Gold_Karat = Item44,
                    BranchCode = Item45,
                    Price_karat = Item46,
                    Result = fetch

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveCostedData" + Ex.Message);
                return new RetrieveCostedData { Respons = Ex.Message, Result = "0" };

            }

        }
        [WebMethod]
        public RetrieveGoldKaratById Pricing_RetrieveGoldKaratById()
        {
            log.Info("Pricing_RetrieveGoldKaratById");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var list = new PricingResult();
                list.Gold = new RetrieveGoldKaratById();
                list.Gold.gold_karat = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT GOLD_KARAT FROM ASYS_GOLDKARAT ORDER BY [ID]";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Gold.gold_karat.Add(string.IsNullOrEmpty(dr["GOLD_KARAT"].ToString()) ? "0" : dr["GOLD_KARAT"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                    conn.Close();
                }
                else
                {
                    dr.Close();
                    fetch = "0";
                    conn.Close();
                }
                return new RetrieveGoldKaratById { Respons = Item, Result = fetch, gold_karat = list.Gold.gold_karat };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveGoldKaratById:" + Ex.Message);
                return new RetrieveGoldKaratById { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveGoldKaratById Pricing_RetrieveGoldKaratById_subasta()
        {
            log.Info("Pricing_RetrieveGoldKaratById");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var list = new PricingResult();
                list.Gold = new RetrieveGoldKaratById();
                list.Gold.gold_karat = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT GOLD_KARAT FROM ASYS_GOLDKARAT_SUBASTA ORDER BY [ID]";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Gold.gold_karat.Add(string.IsNullOrEmpty(dr["GOLD_KARAT"].ToString()) ? "0" : dr["GOLD_KARAT"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                    conn.Close();
                }
                else
                {
                    dr.Close();
                    fetch = "0";
                    conn.Close();
                }
                return new RetrieveGoldKaratById { Respons = Item, Result = fetch, gold_karat = list.Gold.gold_karat };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveGoldKaratById:" + Ex.Message);
                return new RetrieveGoldKaratById { Respons = Ex.Message, Result = "0" };
            }

        }

        [WebMethod]
        public RetrieveCostbyAction_Type Pricing_RetrieveCostbyAction_Type(string Action_Type)
        {
            log.Info("Pricing_RetrieveCostbyAction_Type");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select CostA, CostB, CostC, CostD from tbl_action where action_type = @Action_Type";
                com.Parameters.AddWithValue("Action_Type", Action_Type);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = dr["CostA"].ToString().Trim();
                        Item2 = dr["CostB"].ToString().Trim();
                        Item3 = dr["CostC"].ToString().Trim();
                        Item4 = dr["CostD"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                return new RetrieveCostbyAction_Type { Respons = "aha", Result = fetch, CostA = Item, CostB = Item2, CostC = Item3, CostD = Item4 };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveCostbyAction_Type:" + Ex.Message);
                return new RetrieveCostbyAction_Type { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveDatabyAction_Type Pricing_RetrieveDatabyAction_Type(string Action_type, string ptn, string ALLrefbarcode)
        {
            var list = new PricingResult();
            list.Action_type = new RetrieveDatabyAction_Type();
            list.Action_type.ALL_weightlist = new List<string>();
            list.Action_type.ALL_kartlist = new List<string>();
            list.Action_type.ALL_costlist = new List<string>();
            list.Action_type.Gold_CostList = new List<string>();
            list.Action_type.Mount_Costlist = new List<string>();
            list.Action_type.YG_Costlist = new List<string>();
            list.Action_type.WG_Costllist = new List<string>();
            list.Action_type.Cellular_Costlist = new List<string>();
            list.Action_type.Repair_Costlist = new List<string>();
            list.Action_type.Cleanign_Costlist = new List<string>();
            list.Action_type.Watch_Costlist = new List<string>();
            log.Info("Pricing_RetrieveDatabyAction_Type");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                if (Action_type == "OverAppraised")
                {
                    container = "select ALL_Weight, ALL_Karat, ALL_Cost from ASYS_Pricing_DEtail where ptn = @ptn";
                }
                if (Action_type == "GOODSTOCK")
                {
                    container = "select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost, ALL_Cost from ASYS_Pricing_DEtail where refA"
                    + "llbarcode = @ALLrefbarcode and status not in ('RELEASED','RECDISTRI')";
                }
                if (Action_type == "TakenBack")
                {
                    container = "select ALL_Cost from ASYS_Pricing_DEtail where ptn = @ptn and refAllbarcode = @ALLrefbarcode2 and "
                    + "status not in ('RELEASED','RECDISTRI')";
                }
                if (Action_type == "CELLULAR")
                {
                    container = "select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_Pricing_DEtail where refAllbarcode = @ALLrefbarcode3 and status not in ('RELEASED','RECDISTRI')";
                }
                if (Action_type == "WATCH")
                {
                    container = "select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_Pricing_DEtail where refAllbarcode = @ALLrefbarcode and status not in ('RELEASED','RECDISTRI')";
                }

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = container;
                com.Parameters.AddWithValue("ptn", ptn);
                com.Parameters.AddWithValue("ptn2", ptn);
                com.Parameters.AddWithValue("ALLrefbarcode", ALLrefbarcode);
                com.Parameters.AddWithValue("ALLrefbarcode2", ALLrefbarcode);
                com.Parameters.AddWithValue("ALLrefbarcode3", ALLrefbarcode);
                com.Parameters.AddWithValue("ALLrefbarcode4", ALLrefbarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {

                    if (Action_type == "OverAppraised")
                    {
                        while (dr.Read())
                        {
                            Item = (string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? "0" : dr["ALL_Weight"].ToString().Trim());
                            Item2 = (string.IsNullOrEmpty(dr["ALL_Karat"].ToString()) ? "0" : dr["ALL_Karat"].ToString().Trim());
                            Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                        }
                        dr.Close();
                    }

                    if (Action_type == "GOODSTOCK")
                    {
                        while (dr.Read())
                        {
                            Item4 = (string.IsNullOrEmpty(dr["Gold_Cost"].ToString()) ? "0" : dr["Gold_Cost"].ToString().Trim());
                            Item5 = (string.IsNullOrEmpty(dr["Mount_Cost"].ToString()) ? "0" : dr["Mount_Cost"].ToString().Trim());
                            Item6 = (string.IsNullOrEmpty(dr["YG_Cost"].ToString()) ? "0" : dr["YG_Cost"].ToString().Trim());
                            Item7 = (string.IsNullOrEmpty(dr["WG_Cost"].ToString()) ? "0" : dr["WG_Cost"].ToString().Trim());
                            Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                        }
                        dr.Close();
                    }

                    if (Action_type == "TakenBack")
                    {
                        while (dr.Read())
                        {
                            Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                        }
                        dr.Close();
                    }

                    if (Action_type == "CELLULAR")
                    {
                        while (dr.Read())
                        {
                            Item8 = (string.IsNullOrEmpty(dr["Cellular_Cost"].ToString()) ? "0" : dr["Cellular_Cost"].ToString().Trim());
                            Item9 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0" : dr["Repair_Cost"].ToString().Trim());
                            Item10 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0" : dr["Cleaning_Cost"].ToString().Trim());
                            Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                        }
                        dr.Close();
                    }

                    if (Action_type == "WATCH")
                    {
                        while (dr.Read())
                        {
                            Item11 = (string.IsNullOrEmpty(dr["Watch_Cost"].ToString()) ? "0" : dr["Watch_Cost"].ToString().Trim());
                            Item9 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0" : dr["Repair_Cost"].ToString().Trim());
                            Item10 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0" : dr["Cleaning_Cost"].ToString().Trim());
                            Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                        }
                        dr.Close();
                    }
                    fetch = "1";

                }
                else
                {
                    fetch = "0";

                }
                conn.Close();
                return new RetrieveDatabyAction_Type
                {
                    Respons = "aha",
                    Result = fetch,
                    ALL_weight = Item,
                    ALL_karat = Item2,
                    ALL_cost = Item3,
                    Gold_cost = Item4,
                    Mount_cost = Item5,
                    YG_cost = Item6,
                    WG_cost = Item7,
                    Cellular_cost = Item8,
                    Repair_cost = Item9,
                    Cleaning_cost = Item10,
                    Watch_cost = Item11
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveDatabyAction_Type:" + Ex.Message);
                return new RetrieveDatabyAction_Type { Respons = Ex.Message, Result = "0" };
            }
        }
        [WebMethod]
        public MLWBDisplayAction Pricing_MLWBDisplayActionGetCost(string iselect)
        {
            log.Info("Pricing_MLWBDisplayActionGetCost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select CostA, CostB, CostC, CostD from tbl_action where action_type =@iselect";
                com.Parameters.AddWithValue("iselect", iselect);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["CostA"].ToString()) ? "NULL" : dr["CostA"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["CostB"].ToString()) ? "NULL" : dr["CostB"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["CostC"].ToString()) ? "NULL" : dr["CostC"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["CostD"].ToString()) ? "NULL" : dr["CostD"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new MLWBDisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    CostA = Item,
                    CostB = Item2,
                    CostC = Item3,
                    CostD = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_MLWBDisplayActionGetCost:" + Ex.Message);
                return new MLWBDisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        #endregion


        #region SECOND REGION

        [WebMethod]
        public MLWBDisplayAction Pricing_MLWBDisplayActionGetGold_Cost(string iselect, string ALL_Barcode)
        {
            log.Info("Pricing_MLWBDisplayActionGetGold_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost, ALL_Cost from ASYS_MLWB_DEtail where refAllbarcode =@ALL_Barcode  and status <> 'RECPRICING'";
                com.Parameters.AddWithValue("ALL_Barcode", ALL_Barcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = dr["Gold_Cost"].ToString().Trim();
                        Item2 = dr["Mount_Cost"].ToString().Trim();
                        Item3 = dr["YG_Cost"].ToString().Trim();
                        Item4 = dr["WG_Cost"].ToString().Trim();
                        Item5 = dr["ALL_Cost"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new MLWBDisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Gold_Cost = Item,
                    Mount_Cost = Item2,
                    YG_Cost = Item3,
                    WG_Cost = Item4,
                    ALL_Cost = Item5,
                    Gold = "1"
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_MLWBDisplayActionGetGold_Cost:" + Ex.Message);
                return new MLWBDisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public MLWBDisplayAction Pricing_MLWBDisplayActionGetCellular_Cost(string iselect, string ALL_Barcode)
        {
            log.Info("Pricing_MLWBDisplayActionGetCellular_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_MLWB_DEtail where refAllbarcode = @ALL_Barcode  and status <> 'RECPRICING'";
                com.Parameters.AddWithValue("ALL_Barcode", ALL_Barcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = dr["Cellular_Cost"].ToString().Trim();
                        Item2 = dr["Repair_Cost"].ToString().Trim();
                        Item3 = dr["Cleaning_Cost"].ToString().Trim();
                        Item4 = dr["ALL_Cost"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new MLWBDisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Cellular_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4,
                    Cellular = "1"
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_MLWBDisplayActionGetCellular_Cost:" + Ex.Message);
                return new MLWBDisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public MLWBDisplayAction Pricing_MLWBDisplayActionGetWatch_Cost(string iselect, string ALL_Barcode)
        {
            log.Info("Pricing_MLWBDisplayActionGetWatch_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_MLWB_DEtail where refAllbarcode = @ALL_Barcode  and status <> 'RECPRICING'";
                com.Parameters.AddWithValue("ALL_Barcode", ALL_Barcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = dr["Watch_Cost"].ToString().Trim();
                        Item2 = dr["Repair_Cost"].ToString().Trim();
                        Item3 = dr["Cleaning_Cost"].ToString().Trim();
                        Item4 = dr["ALL_Cost"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new MLWBDisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Watch_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4,
                    Watch = "1"
                };
            }
            catch (Exception Ex)
            {
                log.Error("Pricing_MLWBDisplayActionGetWatch_Cost" + Ex.Message);
                return new MLWBDisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayAction(string iselect)
        {
            log.Info("Pricing_DisplayAction");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select CostA, CostB, CostC, CostD from tbl_action where action_type =@iselect";
                com.Parameters.AddWithValue("iselect", iselect);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["CostA"].ToString()) ? "NULL" : dr["CostA"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["CostB"].ToString()) ? "NULL" : dr["CostB"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["CostC"].ToString()) ? "NULL" : dr["CostC"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["CostD"].ToString()) ? "NULL" : dr["CostD"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    CostA = Item,
                    CostB = Item2,
                    CostC = Item3,
                    CostD = Item4,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayAction" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionGetALL_Weight(string ptn)
        {
            log.Info("Pricing_DisplayActionGetALL_Weight");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select ALL_Weight, ALL_Karat, ALL_Cost from ASYS_REM_DEtail where ptn = @ptn and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("ptn", ptn);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? "0" : dr["ALL_Weight"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["ALL_Karat"].ToString()) ? "0" : dr["ALL_Karat"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "1";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    ALL_Weight = Item,
                    ALL_Karat = Item2,
                    ALL_Cost = Item3
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionGetALL_Weight:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionGetGold_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionGetGold_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost, ALL_Cost from ASYS_REM_DEtail where refAllbarcode =@refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Gold_Cost"].ToString()) ? "0.00" : dr["Gold_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Mount_Cost"].ToString()) ? "0.00" : dr["Mount_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["YG_Cost"].ToString()) ? "0.00" : dr["YG_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["WG_Cost"].ToString()) ? "0.00" : dr["WG_Cost"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Gold_Cost = Item,
                    Mount_Cost = Item2,
                    YG_Cost = Item3,
                    WG_Cost = Item5
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionGetGold_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionGetALL_Cost(string ptn, string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionGetALL_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select ALL_Cost from ASYS_REM_DEtail where ptn = @ptn and refAllbarcode =@refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("ptn", ptn);
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item4 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    ALL_Cost = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionGetALL_Cost" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionGetCellular_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionGetCellular_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_REM_DEtail where refAllbarcode =@refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Cellular_Cost"].ToString()) ? "0.00" : dr["Cellular_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0.00" : dr["Repair_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0.00" : dr["Cleaning_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Cellular_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionGetWatch_Cost(string ptn, string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionGetWatch_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_REM_DEtail where refAllbarcode = @refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Watch_Cost"].ToString()) ? "0.00" : dr["Watch_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0.00" : dr["Repair_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0.00" : dr["Cleaning_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Watch_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionGetWatch_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }


        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionSelect_ALL_Weight(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionSelect_ALL_Weight");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select ALL_Weight, ALL_Karat,ALL_Cost from ASYS_REMOutsource_detail where  refallbarcode ='" + refALLBarcode + "' and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? "0" : dr["month"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["ALL_Karat"].ToString()) ? "0" : dr["month"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["month"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    ALL_Weight = Item,
                    ALL_Karat = Item2,
                    ALL_Cost = Item3
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionSelect_ALL_Weight:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionSelect_GOLD_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionSelect_GOLD_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Gold_Cost, Mount_Cost, YG_Cost, WG_Cost,  ALL_Cost from ASYS_REMOutsource_detail where refallbarcode = @refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Gold_Cost"].ToString()) ? "NULL" : dr["Gold_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Mount_Cost"].ToString()) ? "NULL" : dr["Mount_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["YG_Cost"].ToString()) ? "NULL" : dr["YG_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["WG_Cost"].ToString()) ? "NULL" : dr["WG_Cost"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "NULL" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Gold_Cost = Item,
                    Mount_Cost = Item2,
                    YG_Cost = Item3,
                    Watch_Cost = Item4,
                    ALL_Cost = Item5
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionSelect_GOLD_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        #endregion


        #region THIRD REGION
        [WebMethod]
        public DisplayAction Pricing_DisplayActionSelect_ALL_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionSelect_ALL_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select  ALL_Cost from ASYS_REMOutsource_detail where refallbarcode = @refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item5 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    ALL_Cost = Item5
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionSelect_ALL_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionSelect_Cellular_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionSelect_Cellular_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Cellular_Cost, Repair_Cost, Cleaning_Cost,ALL_Cost from ASYS_REMOutsource_detail where refallbarcode = @refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Cellular_Cost"].ToString()) ? "0.00" : dr["Cellular_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0.00" : dr["Repair_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0.00" : dr["Cleaning_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Cellular_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionSelect_Cellular_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public DisplayAction Pricing_DisplayActionSelect_Watch_Cost(string refALLBarcode)
        {
            log.Info("Pricing_DisplayActionSelect_Watch_Cost");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select Watch_Cost, Repair_Cost, Cleaning_Cost, ALL_Cost from ASYS_REMOutsource_detail where refallbarcode = @refALLBarcode and status <> 'RECMLWB'";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Watch_Cost"].ToString()) ? "0.00" : dr["Watch_Cost"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["Repair_Cost"].ToString()) ? "0.00" : dr["Repair_Cost"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["Cleaning_Cost"].ToString()) ? "0.00" : dr["Cleaning_Cost"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0.00" : dr["ALL_Cost"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new DisplayAction
                {
                    Respons = "aha",
                    Result = fetch,
                    Watch_Cost = Item,
                    Repair_Cost = Item2,
                    Cleaning_Cost = Item3,
                    ALL_Cost = Item4
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_DisplayActionSelect_Watch_Cost:" + Ex.Message);
                return new DisplayAction { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo Pricing_RetrieveInfo(string refALLBarcode)
        {
            log.Info("Pricing_RetrieveInfo");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "If (Select Count(refallbarcode) from ASYS_REM_Detail  where refALLBarcode= @refALLBarcode)<> 0 begin select 1 as st,"
                + "photoname as photo,itemID ,costdate as cost_id,actionclass,sortcode, status  from ASYS_REM_Detail  where  refALLBarcode= @refALLBarcode2 and "
                + "status not in ('RELEASED','RECMLWB') end else select 0 as st,photoname as photo,itemid ,costdate as cost_id,actionclass,sortcode,"
                + "status from ASYS_REMOutsource_detail  where  refALLBarcode= @refALLBarcode and status not in ('RELEASED','RECMLWB')";
                com.Parameters.AddWithValue("refALLBarcode", refALLBarcode);
                com.Parameters.AddWithValue("refALLBarcode2", refALLBarcode);
                com.Parameters.AddWithValue("refALLBarcode3", refALLBarcode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["st"].ToString()) ? "0" : dr["st"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["photo"].ToString()) ? "NULL" : dr["photo"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["itemID"].ToString()) ? "NULL" : dr["itemID"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["cost_id"].ToString()) ? "0" : dr["cost_id"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "0" : dr["sortcode"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfo
                {
                    Respons = "aha",
                    Result = fetch,
                    st = Item,
                    photo = Item2,
                    itemIDs = Item3,
                    cost_id = Item4,
                    actionclass = Item5,
                    sortcode = Item6,
                    status = Item7
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo" + Ex.Message);
                return new RetrieveInfo { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo Pricing_RetrieveInfo_Select_BranchCode(string ptn)
        {
            log.Info("Pricing_RetrieveInfo_Select_BranchCode");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select branchcode,branchname,ptn,ptnbarcode,loanvalue,maturitydate,expirydate,loandate from ASYS_REM_Header  where ptn = @ptn";
                com.Parameters.AddWithValue("ptn", ptn);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["branchcode"].ToString()) ? "NULL" : dr["branchcode"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["branchname"].ToString()) ? "NULL" : dr["branchname"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["ptnbarcode"].ToString()) ? "NULL" : dr["ptnbarcode"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? "0" : dr["loanvalue"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["maturitydate"].ToString()) ? "NULL" : dr["maturitydate"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["expirydate"].ToString()) ? "NULL" : dr["expirydate"].ToString().Trim());
                        Item8 = (string.IsNullOrEmpty(dr["loandate"].ToString()) ? "NULL" : dr["loandate"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfo
                {
                    Respons = "aha",
                    Result = fetch,
                    BranchCode = Item,
                    BranchName = Item2,
                    ptn = Item3,
                    ptnbarcode = Item4,
                    loanvalue = Item5,
                    maturitydate = Item6,
                    expirydate = Item7,
                    loandate = Item8

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo_Select_BranchCode:" + Ex.Message);
                return new RetrieveInfo { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo Pricing_RetrieveInfo_Select_itemid(string ALLBar)
        {
            log.Info("Pricing_RetrieveInfo_Select_itemid");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select itemid,refallbarcode,refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,all_desc,SerialNo,all_karat,all_carat,all_weight,currency,all_cost,photoname,all_price,cellular_cost,watch_cost,repair_cost,cleaning_cost,Gold_cost,mount_cost,yg_cost,wg_cost,appraisevalue from ASYS_REM_Detail  where refALLBarcode = @ALLBar";
                com.Parameters.AddWithValue("ALLBar", ALLBar);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["itemid"].ToString()) ? "NULL" : dr["itemid"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["refallbarcode"].ToString()) ? "NULL" : dr["refallbarcode"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["refitemcode"].ToString()) ? "NULL" : dr["refitemcode"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["itemcode"].ToString()) ? "NULL" : dr["itemcode"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["branchitemdesc"].ToString()) ? "NULL" : dr["branchitemdesc"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["refqty"].ToString()) ? "0" : dr["refqty"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["qty"].ToString()) ? "0" : dr["qty"].ToString().Trim());
                        Item8 = (string.IsNullOrEmpty(dr["karatgrading"].ToString()) ? "0" : dr["karatgrading"].ToString().Trim());
                        Item9 = (string.IsNullOrEmpty(dr["caratsize"].ToString()) ? "0" : dr["caratsize"].ToString().Trim());
                        Item10 = (string.IsNullOrEmpty(dr["weight"].ToString()) ? "0" : dr["weight"].ToString().Trim());
                        Item11 = (string.IsNullOrEmpty(dr["all_desc"].ToString()) ? "NULL" : dr["all_desc"].ToString().Trim());
                        Item12 = (string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "0" : dr["SerialNo"].ToString().Trim());
                        Item13 = (string.IsNullOrEmpty(dr["all_karat"].ToString()) ? "0" : dr["all_karat"].ToString().Trim());
                        Item14 = (string.IsNullOrEmpty(dr["all_carat"].ToString()) ? "0" : dr["all_carat"].ToString().Trim());
                        Item15 = (string.IsNullOrEmpty(dr["all_weight"].ToString()) ? "0" : dr["all_weight"].ToString().Trim());
                        Item16 = (string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString().Trim());
                        Item17 = (string.IsNullOrEmpty(dr["all_cost"].ToString()) ? "0" : dr["all_cost"].ToString().Trim());
                        Item18 = (string.IsNullOrEmpty(dr["photoname"].ToString()) ? "NULL" : dr["photoname"].ToString().Trim());
                        Item19 = (string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());
                        Item20 = (string.IsNullOrEmpty(dr["cellular_cost"].ToString()) ? "0" : dr["cellular_cost"].ToString().Trim());
                        Item21 = (string.IsNullOrEmpty(dr["watch_cost"].ToString()) ? "0" : dr["watch_cost"].ToString().Trim());
                        Item22 = (string.IsNullOrEmpty(dr["repair_cost"].ToString()) ? "0" : dr["repair_cost"].ToString().Trim());
                        Item23 = (string.IsNullOrEmpty(dr["cleaning_cost"].ToString()) ? "0" : dr["cleaning_cost"].ToString().Trim());
                        Item24 = (string.IsNullOrEmpty(dr["Gold_cost"].ToString()) ? "0" : dr["Gold_cost"].ToString().Trim());
                        Item25 = (string.IsNullOrEmpty(dr["mount_cost"].ToString()) ? "0" : dr["mount_cost"].ToString().Trim());
                        Item26 = (string.IsNullOrEmpty(dr["yg_cost"].ToString()) ? "0" : dr["yg_cost"].ToString().Trim());
                        Item27 = (string.IsNullOrEmpty(dr["wg_cost"].ToString()) ? "0" : dr["wg_cost"].ToString().Trim());
                        Item28 = (string.IsNullOrEmpty(dr["appraisevalue"].ToString()) ? "0" : dr["appraisevalue"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                return new RetrieveInfo
                {
                    Respons = "aha",
                    Result = fetch,
                    itemIDs = Item,
                    refallbarcode = Item2,
                    refitemcode = Item3,
                    itemcode = Item4,
                    branchitemdesc = Item5,
                    refqty = Item6,
                    qty = Item7,
                    karatgrading = Item8,
                    caratsize = Item9,
                    weight = Item10,
                    all_desc = Item11,
                    SerialNo = Item12,
                    all_karat = Item13,
                    all_carat = Item14,
                    all_weight = Item15,
                    currency = Item16,
                    all_cost = Item17,
                    photoname = Item18,
                    all_price = Item19,
                    cellular_cost = Item20,
                    watch_cost = Item21,
                    repair_cost = Item22,
                    cleaning_cost = Item23,
                    Gold_cost = Item24,
                    mount_cost = Item25,
                    yg_cost = Item26,
                    wg_cost = Item27,
                    appraisevalue = Item28
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo_Select_itemid:" + Ex.Message);
                return new RetrieveInfo { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo Pricing_RetrieveInfo_Select_Another_itemid(string ALLBar)
        {
            log.Info("Pricing_RetrieveInfo_Select_Another_itemid");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select itemid,ptn,ptnbarcode,branchcode,branchname,loanvalue,refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,all_desc,SerialNo,all_karat,all_carat,all_weight,all_cost,photoname,all_price,appraisevalue,cellular_cost,watch_cost,repair_cost,cleaning_cost,gold_cost,mount_cost,yg_cost,wg_cost,status from ASYS_REMOutsource_detail  where refallbarcode= @ALLBar";
                com.Parameters.AddWithValue("ALLBar", ALLBar);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["itemid"].ToString()) ? "NULL" : dr["itemid"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["ptnbarcode"].ToString()) ? "NULL" : dr["ptnbarcode"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["branchcode"].ToString()) ? "NULL" : dr["branchcode"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["branchname"].ToString()) ? "NULL" : dr["branchname"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? "0" : dr["loanvalue"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["refitemcode"].ToString()) ? "NULL" : dr["refitemcode"].ToString().Trim());
                        Item8 = (string.IsNullOrEmpty(dr["itemcode"].ToString()) ? "NULL" : dr["itemcode"].ToString().Trim());
                        Item9 = (string.IsNullOrEmpty(dr["branchitemdesc"].ToString()) ? "NULL" : dr["branchitemdesc"].ToString().Trim());
                        Item10 = (string.IsNullOrEmpty(dr["refqty"].ToString()) ? "0" : dr["refqty"].ToString().Trim());
                        Item11 = (string.IsNullOrEmpty(dr["qty"].ToString()) ? "0" : dr["qty"].ToString().Trim());
                        Item12 = (string.IsNullOrEmpty(dr["karatgrading"].ToString()) ? "0" : dr["karatgrading"].ToString().Trim());
                        Item13 = (string.IsNullOrEmpty(dr["caratsize"].ToString()) ? "0" : dr["caratsize"].ToString().Trim());
                        Item14 = (string.IsNullOrEmpty(dr["weight"].ToString()) ? "0" : dr["weight"].ToString().Trim());
                        Item15 = (string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Item16 = (string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim());
                        Item17 = (string.IsNullOrEmpty(dr["all_desc"].ToString()) ? "NULL" : dr["all_desc"].ToString().Trim());
                        Item18 = (string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "0" : dr["SerialNo"].ToString().Trim());
                        Item19 = (string.IsNullOrEmpty(dr["all_karat"].ToString()) ? "0" : dr["all_karat"].ToString().Trim());
                        Item20 = (string.IsNullOrEmpty(dr["all_carat"].ToString()) ? "0" : dr["all_carat"].ToString().Trim());
                        Item21 = (string.IsNullOrEmpty(dr["all_weight"].ToString()) ? "0" : dr["all_weight"].ToString().Trim());
                        Item22 = (string.IsNullOrEmpty(dr["all_cost"].ToString()) ? "0" : dr["all_cost"].ToString().Trim());
                        Item23 = (string.IsNullOrEmpty(dr["photoname"].ToString()) ? "NULL" : dr["photoname"].ToString().Trim());
                        Item24 = (string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());
                        Item25 = (string.IsNullOrEmpty(dr["appraisevalue"].ToString()) ? "0" : dr["appraisevalue"].ToString().Trim());
                        Item26 = (string.IsNullOrEmpty(dr["cellular_cost"].ToString()) ? "0" : dr["cellular_cost"].ToString().Trim());
                        Item27 = (string.IsNullOrEmpty(dr["watch_cost"].ToString()) ? "0" : dr["watch_cost"].ToString().Trim());
                        Item28 = (string.IsNullOrEmpty(dr["repair_cost"].ToString()) ? "0" : dr["repair_cost"].ToString().Trim());
                        Item29 = (string.IsNullOrEmpty(dr["cleaning_cost"].ToString()) ? "0" : dr["cleaning_cost"].ToString().Trim());
                        Item30 = (string.IsNullOrEmpty(dr["gold_cost"].ToString()) ? "0" : dr["gold_cost"].ToString().Trim());
                        Item31 = (string.IsNullOrEmpty(dr["mount_cost"].ToString()) ? "0" : dr["mount_cost"].ToString().Trim());
                        Item32 = (string.IsNullOrEmpty(dr["yg_cost"].ToString()) ? "0" : dr["itemid"].ToString().Trim());
                        Item33 = (string.IsNullOrEmpty(dr["wg_cost"].ToString()) ? "0" : dr["wg_cost"].ToString().Trim());
                        Item34 = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfo
                {
                    Respons = "aha",
                    Result = fetch,
                    itemIDs = Item,
                    ptn = Item2,
                    ptnbarcode = Item3,
                    BranchCode = Item4,
                    BranchName = Item5,
                    loanvalue = Item6,
                    refitemcode = Item7,
                    itemcode = Item8,
                    branchitemdesc = Item9,
                    refqty = Item10,
                    qty = Item11,
                    karatgrading = Item12,
                    caratsize = Item13,
                    weight = Item14,
                    actionclass = Item15,
                    sortcode = Item16,
                    all_desc = Item17,
                    SerialNo = Item18,
                    all_karat = Item19,
                    all_carat = Item20,
                    all_weight = Item21,
                    all_cost = Item22,
                    photoname = Item23,
                    all_price = Item24,
                    appraisevalue = Item25,
                    cellular_cost = Item26,
                    watch_cost = Item27,
                    repair_cost = Item28,
                    cleaning_cost = Item29,
                    Gold_cost = Item30,
                    mount_cost = Item31,
                    yg_cost = Item32,
                    wg_cost = Item33,
                    status = Item34,


                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo_Select_Another_itemid:" + Ex.Message);
                return new RetrieveInfo { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo Pricing_RetrieveInfo_Select_bedrnm(string bedryf2, string sDivision)
        {
            log.Info("Pricing_RetrieveInfo_Select_bedrnm");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select bedrnm from @bedryf2   where bedrnr = @sDivision ";
                com.Parameters.AddWithValue("bedryf2", bedryf2);
                com.Parameters.AddWithValue("sDivision", sDivision);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["bedrnm"].ToString()) ? "NULL" : dr["bedrnm"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfo
                {
                    Respons = "aha",
                    Result = fetch,
                    bedrnm = Item,


                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo_Select_bedrnm" + Ex.Message);
                return new RetrieveInfo { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfoRet Pricing_RetrieveInfoRet(string ALLBar)
        {
            log.Info("Pricing_RetrieveInfoRet");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "If (Select Count(refallbarcode) from ASYS_REM_Detail  where refALLBarcode= @ALLBar)<> 0 begin select 1 as st,photoname as photo,itemID ,costdate as cost_id,actionclass,sortcode, status  from ASYS_REM_Detail  where  refALLBarcode=@ALLBar2 and status not in ('RELEASED','RECMLWB') end else select 0 as st,photoname as photo,itemid ,costdate as cost_id,actionclass,sortcode,  status from ASYS_REMOutsource_detail  where  refALLBarcode='0' and status not in ('RELEASED','RECMLWB')";
                com.Parameters.AddWithValue("ALLBar", ALLBar);
                com.Parameters.AddWithValue("ALLBar2", ALLBar);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["st"].ToString()) ? "NULL" : dr["st"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["photo"].ToString()) ? "NULL" : dr["photo"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["itemID"].ToString()) ? "NULL" : dr["itemID"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["cost_id"].ToString()) ? "NULL" : dr["cost_id"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfoRet
                {
                    Respons = "aha",
                    Result = fetch,
                    st = Item,
                    photo = Item2,
                    itemid = Item3,
                    cost_id = Item4,
                    actionclass = Item5,
                    sortcode = Item6,
                    status = Item7
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfoRet:" + Ex.Message);
                return new RetrieveInfoRet { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfoRet Pricing_RetrieveInfoRet_Select_st(string ALLBar)
        {
            log.Info("Pricing_RetrieveInfoRet_Select_st");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select 0 as st,photoname as photo,itemid ,costdate as cost_id,actionclass,sortcode,  status from ASYS_REMOutsource_detail where  refALLBarcode= @ALLBar and status in ('RECEIVED','COSTED')";
                com.Parameters.AddWithValue("ALLBar", ALLBar);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["st"].ToString()) ? "NULL" : dr["st"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["photo"].ToString()) ? "NULL" : dr["photo"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["cost_id"].ToString()) ? "NULL" : dr["cost_id"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfoRet
                {
                    Respons = "aha",
                    Result = fetch,
                    st = Item,
                    photo = Item2,
                    cost_id = Item3,
                    actionclass = Item4,
                    sortcode = Item5,
                    status = Item6
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfoRet_Select_st" + Ex.Message);
                return new RetrieveInfoRet { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfoRet Pricing_RetrieveInfoRet_Select_Itemid(string ALLBar)
        {
            log.Info("Pricing_RetrieveInfoRet_Select_Itemid");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select itemid,ptn,ptnbarcode,branchcode,branchname,loanvalue,refitemcode,itemcode,branchitemdesc,refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,all_desc,SerialNo,all_karat,all_carat,all_weight,currency,all_cost,photoname,all_price,appraisevalue,cellular_cost,watch_cost,repair_cost,cleaning_cost,gold_cost,mount_cost,yg_cost,wg_cost,status from ASYS_REMOutsource_detail where refallbarcode= @ALLBar and status in ('RECEIVED','COSTED')";
                com.Parameters.AddWithValue("ALLBar", ALLBar);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["itemid"].ToString()) ? "NULL" : dr["itemid"].ToString().Trim());
                        Item2 = (string.IsNullOrEmpty(dr["ptn"].ToString()) ? "0" : dr["ptn"].ToString().Trim());
                        Item3 = (string.IsNullOrEmpty(dr["ptnbarcode"].ToString()) ? "NULL" : dr["ptnbarcode"].ToString().Trim());
                        Item4 = (string.IsNullOrEmpty(dr["branchcode"].ToString()) ? "NULL" : dr["branchcode"].ToString().Trim());
                        Item5 = (string.IsNullOrEmpty(dr["branchname"].ToString()) ? "NULL" : dr["branchname"].ToString().Trim());
                        Item6 = (string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? "0" : dr["loanvalue"].ToString().Trim());
                        Item7 = (string.IsNullOrEmpty(dr["refitemcode"].ToString()) ? "NULL" : dr["refitemcode"].ToString().Trim());
                        Item8 = (string.IsNullOrEmpty(dr["itemcode"].ToString()) ? "NULL" : dr["itemcode"].ToString().Trim());
                        Item9 = (string.IsNullOrEmpty(dr["branchitemdesc"].ToString()) ? "NULL" : dr["branchitemdesc"].ToString().Trim());
                        Item10 = (string.IsNullOrEmpty(dr["qty"].ToString()) ? "0" : dr["qty"].ToString().Trim());
                        Item11 = (string.IsNullOrEmpty(dr["karatgrading"].ToString()) ? "0" : dr["karatgrading"].ToString().Trim());
                        Item12 = (string.IsNullOrEmpty(dr["caratsize"].ToString()) ? "0" : dr["caratsize"].ToString().Trim());
                        Item13 = (string.IsNullOrEmpty(dr["weight"].ToString()) ? "0" : dr["weight"].ToString().Trim());
                        Item14 = (string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Item15 = (string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim());
                        Item16 = (string.IsNullOrEmpty(dr["all_desc"].ToString()) ? "NULL" : dr["all_desc"].ToString().Trim());
                        Item17 = (string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "0" : dr["SerialNo"].ToString().Trim());
                        Item18 = (string.IsNullOrEmpty(dr["all_karat"].ToString()) ? "0" : dr["all_karat"].ToString().Trim());
                        Item19 = (string.IsNullOrEmpty(dr["all_carat"].ToString()) ? "0" : dr["all_carat"].ToString().Trim());
                        Item20 = (string.IsNullOrEmpty(dr["all_weight"].ToString()) ? "NULL" : dr["all_weight"].ToString().Trim());
                        Item21 = (string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString().Trim());
                        Item22 = (string.IsNullOrEmpty(dr["all_cost"].ToString()) ? "0" : dr["all_cost"].ToString().Trim());
                        Item23 = (string.IsNullOrEmpty(dr["photoname"].ToString()) ? "NULL" : dr["photoname"].ToString().Trim());
                        Item24 = (string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());
                        Item25 = (string.IsNullOrEmpty(dr["appraisevalue"].ToString()) ? "0" : dr["appraisevalue"].ToString().Trim());
                        Item26 = (string.IsNullOrEmpty(dr["cellular_cost"].ToString()) ? "0" : dr["cellular_cost"].ToString().Trim());
                        Item27 = (string.IsNullOrEmpty(dr["watch_cost"].ToString()) ? "0" : dr["watch_cost"].ToString().Trim());
                        Item28 = (string.IsNullOrEmpty(dr["repair_cost"].ToString()) ? "0" : dr["repair_cost"].ToString().Trim());
                        Item29 = (string.IsNullOrEmpty(dr["cleaning_cost"].ToString()) ? "0" : dr["cleaning_cost"].ToString().Trim());
                        Item30 = (string.IsNullOrEmpty(dr["gold_cost"].ToString()) ? "0" : dr["gold_cost"].ToString().Trim());
                        Item31 = (string.IsNullOrEmpty(dr["mount_cost"].ToString()) ? "0" : dr["mount_cost"].ToString().Trim());
                        Item32 = (string.IsNullOrEmpty(dr["yg_cost"].ToString()) ? "0" : dr["yg_cost"].ToString().Trim());
                        Item33 = (string.IsNullOrEmpty(dr["wg_cost"].ToString()) ? "0" : dr["wg_cost"].ToString().Trim());
                        Item34 = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                        Item35 = (string.IsNullOrEmpty(dr["refqty"].ToString()) ? "0" : dr["refqty"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrieveInfoRet
                {
                    Respons = "aha",
                    Result = fetch,
                    itemid = Item,
                    ptn = Item2,
                    ptnbarcode = Item3,
                    branchcode = Item4,
                    branchname = Item5,
                    loanvalue = Item6,
                    refitemcode = Item7,
                    itemcode = Item8,
                    branchitemdesc = Item9,
                    qty = Item10,
                    karatgrading = Item11,
                    caratsize = Item12,
                    weight = Item13,
                    actionclass = Item14,
                    sortcode = Item15,
                    all_desc = Item16,
                    SerialNo = Item17,
                    all_karat = Item18,
                    all_carat = Item19,
                    all_weight = Item20,
                    currency = Item21,
                    all_cost = Item22,
                    photoname = Item23,
                    all_price = Item24,
                    appraisevalue = Item25,
                    cellular_cost = Item26,
                    watch_cost = Item27,
                    repair_cost = Item28,
                    cleaning_cost = Item29,
                    gold_cost = Item30,
                    mount_cost = Item31,
                    yg_cost = Item32,
                    wg_cost = Item33,
                    status = Item34,
                    refqty = Item35

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfoRet_Select_Itemid:" + Ex.Message);
                return new RetrieveInfoRet { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public RetrievePTN_Barcode Pricing_RetrievePTN_Barcode(string BranchCode)
        {
            log.Info("Pricing_RetrievePTN_Barcode");
            var list = new PricingResult();
            list.Action = new RetrievePTN_Barcode();
            list.Action.BranchCode = new List<string>();
            list.Action.BranchName = new List<string>();
            list.Action.PTN = new List<string>();
            list.Action.PTNBarcode = new List<string>();
            list.Action.RefALLBarcode = new List<string>();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT dbo.ASYS_REM_Header.BranchCode, dbo.ASYS_REM_Header.BranchName, dbo.ASYS_REM_Header.PTN, dbo.ASYS_REM_Header.PTNBarcode, dbo.ASYS_REM_Detail.RefALLBarcode FROM  dbo.ASYS_REM_Detail INNER JOIN   dbo.ASYS_REM_Header ON dbo.ASYS_REM_Detail.PTN = dbo.ASYS_REM_Header.PTN  where status = 'SORTED' and Actionclass in ('GoodStock','Watch','Cellular') and branchcode= @BranchCode";
                com.Parameters.AddWithValue("BranchCode", BranchCode);
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Action.BranchCode.Add(string.IsNullOrEmpty(dr["BranchCode"].ToString()) ? "NULL" : dr["BranchCode"].ToString().Trim());
                        list.Action.BranchName.Add(string.IsNullOrEmpty(dr["BranchName"].ToString()) ? "NULL" : dr["BranchName"].ToString().Trim());
                        list.Action.PTN.Add(string.IsNullOrEmpty(dr["PTN"].ToString()) ? "NULL" : dr["PTN"].ToString().Trim());
                        list.Action.PTNBarcode.Add(string.IsNullOrEmpty(dr["PTNBarcode"].ToString()) ? "NULL" : dr["PTNBarcode"].ToString().Trim());
                        list.Action.RefALLBarcode.Add(string.IsNullOrEmpty(dr["RefALLBarcode"].ToString()) ? "NULL" : dr["RefALLBarcode"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new RetrievePTN_Barcode
                {
                    Respons = "aha",
                    Result = fetch,
                    BranchCode = list.Action.BranchCode,
                    BranchName = list.Action.BranchName,
                    PTN = list.Action.PTN,
                    PTNBarcode = list.Action.PTNBarcode,
                    RefALLBarcode = list.Action.RefALLBarcode
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrievePTN_Barcode:" + Ex.Message);
                return new RetrievePTN_Barcode { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public NewCostingSave Pricing_NewCostingSave(string refqty, string all_desc, string SerialNo, string all_karat, string all_carat, string all_weight, string all_cost, string currency, string price_desc, string price_karat, string price_weight, string price_carat, string gold_cost, string mount_cost, string yg_cost, string wg_cost, string costname, string status, string refallbarcode, string sDB)
        {
            log.Info("Pricing_NewCostingSave");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                if (all_cost == "" || all_cost == "NULL" || all_cost == "0")
                {
                    all_cost = "0.00";
                }
                if (SerialNo == "" || SerialNo == "NULL" || SerialNo == "0")
                {
                    SerialNo = "0.00";
                }
                if (all_cost == "" || all_cost == "NULL" || all_cost == "0")
                {
                    all_cost = "0.00";
                }
                if (gold_cost == "" || gold_cost == "NULL" || gold_cost == "0")
                {
                    gold_cost = "0.00";
                }
                if (mount_cost == "" || mount_cost == "NULL" || mount_cost == "0")
                {
                    mount_cost = "0.00";
                }
                if (yg_cost == "" || yg_cost == "NULL" || yg_cost == "0")
                {
                    yg_cost = "0.00";
                }
                if (wg_cost == "" || wg_cost == "NULL" || wg_cost == "0")
                {
                    wg_cost = "0.00";
                }
                if (costname == "" || costname == "NULL" || costname == "0")
                {
                    costname = "0.00";
                }
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update ASYS_REM_Detail  set refqty = '" + refqty + "',all_desc = '" + all_desc + "',"
                + "SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',"
                + "all_cost ='" + all_cost + "',currency =  '" + currency + "',price_desc = '" + price_desc + "',price_karat = '" + price_karat + "',"
                + "price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',gold_cost = '" + gold_cost + "', "
                + "mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
                + "status='" + status + "' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update REMS" + sDB + ".dbo.ASYS_REM_Detail  set refqty = '" + refqty + "',all_desc = '" + all_desc + "',"
               + "SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',"
               + "all_cost ='" + all_cost + "',currency =  '" + currency + "',price_desc = '" + price_desc + "',price_karat = '" + price_karat + "',"
               + "price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',gold_cost = '" + gold_cost + "', "
               + "mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
               + "status='" + status + "' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_REMOutsource_Detail   set refqty = '" + refqty + "',all_desc = '" + all_desc + "',"
              + "SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',"
              + "all_cost ='" + all_cost + "',currency =  '" + currency + "',price_desc = '" + price_desc + "',price_karat = '" + price_karat + "',"
              + "price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',gold_cost = '" + gold_cost + "', "
              + "mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
              + "status='" + status + "' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.CommandText = ("Update REMS" + sDB + ".dbo.ASYS_REMOutsource_Detail    set refqty = '" + refqty + "',all_desc = '" + all_desc + "',"
              + "SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',"
              + "all_cost ='" + all_cost + "',currency =  '" + currency + "',price_desc = '" + price_desc + "',price_karat = '" + price_karat + "',"
              + "price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',gold_cost = '" + gold_cost + "', "
              + "mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
              + "status='" + status + "' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new NewCostingSave { Respons = "Items are Saved!", Result = "1" };


            }
            catch (Exception ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_NewCostingSave" + ex.Message);
                return new NewCostingSave { Respons = "Transaction Failed", Result = "0" };
            }


        }//DONE
        [WebMethod]
        public SaveMounted Pricing_SaveMounted(string Lotno)
        {
            log.Info("Pricing_SaveMounted");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT Lotno FROM ASYS_PRICING_detail WHERE refallbarcode = '" + Lotno + "' AND status not in ('RELEASED','RECDISTRI')";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Lotno"].ToString()) ? "NULL" : dr["Lotno"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    Item = "NO DATA FOUND";
                    fetch = "0";
                }
                conn.Close();
                return new SaveMounted
                {
                    Respons = "aha",
                    Result = fetch,
                    Lotno = Item,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_SaveMounted:" + Ex.Message);
                return new SaveMounted { Respons = "Invalid Entry!", Result = "0" };
            }

        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_AsysGold(string ALLbarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_AsysGold");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {


                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("DELETE FROM ASYS_Gold WHERE AllBarcode = '" + ALLbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();



                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_AsysGold:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_Into_AsysGold(string LotNo, string allbarcode, string karat, string karatprice, string goldprice, string grams)
        {
            log.Info("Pricing_SaveMounted_Insert_Into_AsysGold");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {



                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("INSERT INTO ASYS_Gold (Lotno, ALLBarcode, Karat, Grams, KaratPrice, GoldPrice)VALUES ('" + LotNo + "', '" + allbarcode + "', '" + karat + "', '" + grams + "', '" + karatprice + "', '" + goldprice + "')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();



                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM SAVE", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_Into_AsysGold:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_AsysDiamond(string ALLbarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_AsysDiamond");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = ("DELETE FROM ASYS_Diamond WHERE AllBarcode = '" + ALLbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_AsysDiamond" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_Into_AsysDiamond(string Lotno, string Allbarcode, string NoOfDias, string Diamond_Cut, string Diamond_Color, string Diamond_Clarity, string Diamond_CWT, string Diamond_Price)
        {
            log.Info("Pricing_SaveMounted_Insert_Into_AsysDiamond");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("INSERT INTO ASYS_Diamond (Lotno, ALLBarcode, NoOfDias, Diamond_Cut, Diamond_Color, Diamond_Clarity, Diamond_CWT, Diamond_Price)VALUES ('" + Lotno + "', '" + Allbarcode + "', '" + NoOfDias + "', '" + Diamond_Cut + "',"
                + "'" + Diamond_Color + "','" + Diamond_Clarity + "','" + Diamond_CWT + "','" + Diamond_Price + "')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM SAVED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_Into_AsysDiamond:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }//DONE
        # endregion


        #region FOURTH REGION
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_AsysPEARL(string ALLbarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_AsysPEARL");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("DELETE FROM ASYS_Pearl WHERE AllBarcode = '" + ALLbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_AsysPEARL:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_Into_AsysPearl(string Lotno, string ALLBarcode, string Pearl_No_of_Pear, string Pearl_Type, string Pearl_Size, string Pearl_Quality, string Pearl_Price)
        {
            log.Info("Pricing_SaveMounted_Insert_Into_AsysPearl");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {


                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("INSERT INTO ASYS_Pearl (Lotno, ALLBarcode, Pearl_No_of_Pear, Pearl_Type, Pearl_Size, Pearl_Quality, Pearl_Price) VALUES ('" + Lotno + "', '" + ALLBarcode + "','" + Pearl_No_of_Pear + "','" + Pearl_Type + "','" + Pearl_Size + "','" + Pearl_Quality + "','" + Pearl_Price + "') ");
                com.Transaction = Trans;
                com.ExecuteNonQuery();



                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM SAVED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_Into_AsysPearl:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_AsysPreciousStone(string ALLbarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_AsysPreciousStone");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("DELETE FROM ASYS_PreciousStone WHERE AllBarcode = '" + ALLbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_AsysPreciousStone:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_From_AsysPreciousStone(string Lotno, string ALLBarcode, string PStone_Type, string PStone_Quality, string PStone_CWT, string PStone_Price)
        {
            log.Info("Pricing_SaveMounted_Insert_From_AsysPreciousStone");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("INSERT INTO ASYS_PreciousStone (Lotno, ALLBarcode, PStone_Type, PStone_Quality, PStone_CWT, PStone_Price)VALUES ('" + Lotno + "','" + ALLBarcode + "','" + PStone_Type + "','" + PStone_Quality + "','" + PStone_CWT + "','" + PStone_Price + "')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_From_AsysPreciousStone:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_Asys_SyntheticStone(string ALLBarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_Asys_SyntheticStone");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("DELETE FROM ASYS_SyntheticStone WHERE AllBarcode = '" + ALLBarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };
            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_Asys_SyntheticStone:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }

        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_Into_Asys_Synthetic(string Lotno, string ALLBarcode, string SStone_Type, string SStone_Quality, string SStone_CWT, string SStone_Price)
        {
            log.Info("Pricing_SaveMounted_Insert_Into_Asys_Synthetic");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("INSERT INTO ASYS_SyntheticStone (Lotno, ALLBarcode, SStone_Type, SStone_Quality, SStone_CWT, SStone_Price) VALUES ('" + Lotno + "','" + ALLBarcode + "','" + SStone_Type + "','" + SStone_Quality + "','" + SStone_CWT + "','" + SStone_Price + "')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM SAVE", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_Into_Asys_Synthetic:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }

        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Delete_From_AsysMountedPrice(string ALLbarcode)
        {
            log.Info("Pricing_SaveMounted_Delete_From_AsysMountedPrice");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {


                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("DELETE FROM ASYS_MountedPrice WHERE AllBarcode = '" + ALLbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();



                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Delete_From_AsysMountedPrice:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Insert_Into_Asys_MountedPrice(string Lotno, string ALLBarcode, string Total_GoldPrice, string Total_DiaPrice, string Total_PearlPrice, string Total_CStonePrice, string Total_SellingPrice)
        {
            log.Info("Pricing_SaveMounted_Insert_Into_Asys_MountedPrice");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("INSERT INTO ASYS_MountedPrice (Lotno, ALLBarcode, Total_GoldPrice, Total_DiaPrice, Total_PearlPrice, Total_CStonePrice, Total_SellingPrice) VALUES ('" + Lotno + "','" + ALLBarcode + "','" + Total_GoldPrice + "','" + Total_DiaPrice + "','" + Total_PearlPrice + "','" + Total_CStonePrice + "','" + Total_SellingPrice + "')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM SAVED", Result = "1" };
            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Insert_Into_Asys_MountedPrice:" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public SaveMounted Pricing_SaveMounted_Update_Asys_Pricing_Detail(string refqty, string all_cost, string all_price, string currency, string price_desc, string SerialNo, string price_karat, string price_weight, string price_carat, string gold_cost, string mount_cost, string yg_cost, string wg_cost, string GoldKaratPrice, string MountedPrice, string Gold_Karat, string costname, string allbarcode, string Description, string Weight, string karat, string carat, string cost, string price)
        {
            log.Info("Pricing_SaveMounted_Update_Asys_Pricing_Detail");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                if (refqty == "" || refqty == "NULL")
                {
                    refqty = "0.00";
                }
                if (all_cost == "" || all_cost == "NULL")
                {
                    all_cost = "0.00";
                }
                if (all_price == "" || all_price == "NULL")
                {
                    all_price = "0.00";
                }
                if (currency == "" || currency == "NULL")
                {
                    currency = "0.00";
                }
                if (price_desc == "" || price_desc == "NULL")
                {
                    price_desc = "0.00";
                }
                if (SerialNo == "" || SerialNo == "NULL")
                {
                    SerialNo = "0.00";
                }
                if (price_karat == "" || price_karat == "NULL")
                {
                    price_karat = "0.00";
                }
                if (wg_cost == "" || wg_cost == "NULL")
                {
                    wg_cost = "0.00";
                }
                if (GoldKaratPrice == "" || GoldKaratPrice == "NULL")
                {
                    GoldKaratPrice = "0.00";
                }
                if (MountedPrice == "" || MountedPrice == "NULL")
                {
                    MountedPrice = "0.00";
                }
                if (Gold_Karat == "" || Gold_Karat == "NULL")
                {
                    Gold_Karat = "0.00";
                }
                if (costname == "" || costname == "NULL")
                {
                    costname = "0.00";
                }
                if (allbarcode == "" || allbarcode == "NULL")
                {
                    allbarcode = "0.00";
                }
                if (Weight == "" || Weight == "NULL")
                {
                    Weight = "0.00";
                }
                if (karat == "" || karat == "NULL")
                {
                    karat = "0.00";
                }
                if (carat == "" || carat == "NULL")
                {
                    carat = "0.00";
                }
                if (cost == "" || cost == "NULL")
                {
                    cost = "0.00";
                }
                if (price == "" || price == "NULL")
                {
                    price = "0.00";
                }
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update ASYS_Pricing_Detail  set refqty = '" + refqty + "',all_cost ='" + all_cost + "',all_price = '" + all_price + "',currency = '" + currency + "',price_desc = '" + price_desc + "',SerialNo = '" + SerialNo + "',price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',gold_cost = '" + gold_cost + "',mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "', GoldKaratPrice ='" + GoldKaratPrice + "', MountedPrice ='" + MountedPrice + "', Gold_Karat ='" + Gold_Karat + "', costdate = getdate(),costname = '" + costname + "',status='PRICED' where refallbarcode = '" + allbarcode + "' and status not in ('RELEASED','RECDISTRI')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_BarcodeHistory  set Description = '" + Description + "',  Weight = '" + Weight + "', Karat = '" + karat + "', Carat = '" + carat + "',cost = '" + cost + "',price = '" + price + "',currency =  '" + currency + "' where refallbarcode ='" + allbarcode + "' and COSTCENTER = 'PRICING'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new SaveMounted { Respons = "ITEM DELETED", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Update_Asys_Pricing_Detail" + Ex.Message);
                return new SaveMounted { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public saveCellular Pricing_saveCellular(string all_desc, string SerialNo, string all_karat, string all_carat, string all_weight, string all_cost, string currency, string price_desc, string price_karat, string price_weight, string price_carat, string cellular_cost, string watch_cost, string repair_cost, string cleaning_cost, string gold_cost, string yg_cost, string wg_cost, string costname, string refallbarcode, string sDB)
        {
            log.Info("Pricing_saveCellular");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                if (SerialNo == "" || SerialNo == "NULL")
                {
                    SerialNo = "0.00";
                }
                if (all_karat == "" || all_karat == "NULL")
                {
                    all_karat = "0.00";
                }
                if (all_carat == "" || all_carat == "NULL")
                {
                    all_carat = "0.00";
                }
                if (all_weight == "" || all_weight == "NULL")
                {
                    all_weight = "0.00";
                }
                if (all_cost == "" || all_cost == "NULL")
                {
                    all_cost = "0.00";
                }
                if (currency == "" || currency == "NULL")
                {
                    currency = "0.00";
                }

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update ASYS_REM_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
                + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update rems" + sDB + ".dbo.ASYS_REM_Detail set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
              + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
              + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
              + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
              + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
              + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_REMOutsource_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
                + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update rems" + sDB + ".dbo.ASYS_REMOutsource_Detail set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
             + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
             + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
             + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
             + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
             + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("update REMS.dbo.asys_barcodehistory  set description = '" + all_desc + "', karat = '" + all_karat + "', carat = '" + all_carat + "', weight = '" + all_weight + "' where refallbarcode = '" + refallbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new saveCellular { Respons = "DATA UPDATED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_saveCellular" + Ex.Message);
                return new saveCellular { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public saveCellular Pricing_saveCellular_Else_Update(string all_desc, string SerialNo, string all_karat, string all_carat, string all_weight, string all_cost, string currency, string price_desc, string price_karat, string price_weight, string price_carat, string cellular_cost, string watch_cost, string repair_cost, string cleaning_cost, string gold_cost, string yg_cost, string wg_cost, string costname, string refallbarcode)
        {
            log.Info("Pricing_saveCellular_Else_Update");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("Update rems.dbo.ASYS_REMOutsource_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_weight + "',cellular_cost ='" + cellular_cost + "', "
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("update REMS.dbo.asys_barcodehistory  set description = '" + all_desc + "', karat = '" + all_karat + "', carat = '" + all_carat + "', weight = '" + price_weight + "' where refallbarcode = '" + refallbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new saveCellular { Respons = "DATA UPDATED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_saveCellular_Else_Update:" + Ex.Message);
                return new saveCellular { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public saveCellular1 Pricing_saveCellular1(string refqty, string all_cost, string all_price, string currency, string price_desc, string SerialNo, string price_karat, string price_weight, string price_carat, string cellular_cost, string repair_cost, string cleaning_cost, string GoldKaratPrice, string MountedPrice, string costname, string refallbarcode)
        {
            log.Info("Pricing_saveCellular1");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("Update ASYS_Pricing_Detail  set refqty = '" + refqty + "',all_cost ='" + all_cost + "',all_price = '" + all_price + "',currency =  '" + currency + "',"
                + "price_desc = '" + price_desc + "',SerialNo = '" + SerialNo + "',price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',"
                + "price_carat = '" + price_carat + "',cellular_cost = '" + cellular_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost = '" + cleaning_cost + "',"
                + "GoldKaratPrice ='" + GoldKaratPrice + "', MountedPrice ='" + MountedPrice + "',costdate = getdate(),costname = '" + costname + "',"
                + "status='PRICED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECDISTRI')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_BarcodeHistory  set Description = '" + price_desc + "', serialno = '" + SerialNo + "', Weight = '" + price_weight + "', Karat = '" + price_karat + "', Carat = '" + price_carat + "',cost = '" + all_cost + "',price = '" + all_price + "',currency =  '" + currency + "' where refallbarcode ='" + refallbarcode + "' and COSTCENTER = 'PRICING'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new saveCellular1 { Respons = "DATA UPDATED", Result = "1" };
            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_saveCellular1" + Ex.Message);
                return new saveCellular1 { Respons = "Transaction Failed", Result = "0" };
            }

        }
        [WebMethod]
        public savewatch1 Pricing_savewatch1(string refqty, string all_cost, string all_price, string currency, string price_desc, string SerialNo, string price_karat, string price_weight, string price_carat, string watch_cost, string repair_cost, string cleaning_cost, string GoldKaratPrice, string MountedPrice, string costname, string refallbarcode)
        {
            log.Info("Pricing_savewatch1");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update ASYS_Pricing_Detail  set refqty = '" + refqty + "',all_cost ='" + all_cost + "',all_price = '" + all_price + "',currency =  '" + currency + "',"
                + "price_desc = '" + price_desc + "',SerialNo = '" + SerialNo + "',price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',"
                + "price_carat = '" + price_carat + "',watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost = '" + cleaning_cost + "',"
                + "GoldKaratPrice ='" + GoldKaratPrice + "', MountedPrice ='" + MountedPrice + "',costdate = getdate(),costname = '" + costname + "',"
                + "status='PRICED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECDISTRI')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_BarcodeHistory  set Description = '" + price_desc + "', serialno = '" + SerialNo + "', Weight = '" + price_weight + "', Karat = '" + price_karat + "', Carat = '" + price_carat + "',cost = '" + all_cost + "',price = '" + all_price + "',currency =  '" + currency + "' where refallbarcode ='" + refallbarcode + "' and COSTCENTER = 'PRICING'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new savewatch1 { Respons = "DATA UPDATED", Result = "1" };
            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_savewatch1:" + Ex.Message);
                return new savewatch1 { Respons = "Transaction Failed", Result = "0" };
            }

        }
        [WebMethod]
        public savewatch Pricing_savewatch(string all_desc, string SerialNo, string all_karat, string all_carat, string all_weight, string all_cost, string currency, string price_desc, string price_karat, string price_weight, string price_carat, string cellular_cost, string watch_cost, string repair_cost, string cleaning_cost, string gold_cost, string yg_cost, string wg_cost, string costname, string refallbarcode, string sDB)
        {
            log.Info("Pricing_savewatch");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {
                if (SerialNo == "" || SerialNo == "NULL" || SerialNo == "0")
                {
                    SerialNo = "0.00";
                }
                if (all_karat == "" || all_karat == "NULL" || all_karat == "0")
                {
                    all_karat = "0.00";
                }
                if (all_carat == "" || all_carat == "NULL" || all_carat == "0")
                {
                    all_carat = "0.00";
                }
                if (all_weight == "" || all_weight == "NULL" || all_weight == "0")
                {
                    all_weight = "0.00";
                }
                if (all_cost == "" || all_cost == "NULL" || all_cost == "0")
                {
                    all_cost = "0.00";
                }
                if (currency == "" || currency == "NULL" || currency == "0")
                {
                    currency = "0.00";
                }

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;

                com.CommandText = ("Update ASYS_REM_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
                + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update rems" + sDB + ".dbo.ASYS_REM_Detail set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
              + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
              + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
              + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
              + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
              + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_REMOutsource_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
                + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update rems" + sDB + ".dbo.ASYS_REMOutsource_Detail set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
             + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
             + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_carat + "',cellular_cost ='" + cellular_cost + "',"
             + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
             + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',"
             + "status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("update REMS.dbo.asys_barcodehistory  set description = '" + all_desc + "', karat = '" + all_karat + "', carat = '" + all_carat + "', weight = '" + all_weight + "' where refallbarcode = '" + refallbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new savewatch { Respons = "DATA UPDATED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_savewatch" + Ex.Message);
                return new savewatch { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public savewatch Pricing_savewatch_Else_Update(string all_desc, string SerialNo, string all_karat, string all_carat, string all_weight, string all_cost, string currency, string price_desc, string price_karat, string price_weight, string price_carat, string cellular_cost, string watch_cost, string repair_cost, string cleaning_cost, string gold_cost, string yg_cost, string wg_cost, string costname, string refallbarcode)
        {
            log.Info("Pricing_savewatch_Else_Update");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();

            try
            {


                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update rems.dbo.ASYS_REMOutsource_Detail  set all_desc = '" + all_desc + "',SerialNo = '" + SerialNo + "',all_karat ='" + all_karat + "',"
                + "all_carat = '" + all_carat + "',all_weight = '" + all_weight + "',all_cost = '" + all_cost + "',currency = '" + currency + "',price_desc ='" + price_desc + "',"
                + "price_karat = '" + price_karat + "',price_weight = '" + price_weight + "',price_carat = '" + price_weight + "',cellular_cost ='" + cellular_cost + "', "
                + "watch_cost = '" + watch_cost + "',repair_cost = '" + repair_cost + "',cleaning_cost ='" + cleaning_cost + "',gold_cost = '" + gold_cost + "',"
                + "yg_cost = '" + yg_cost + "',wg_cost = '" + wg_cost + "',costdate = getdate(),costname = '" + costname + "',status = 'COSTED' where refallbarcode = '" + refallbarcode + "' and status not in ('RELEASED','RECMLWB')");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("update REMS.dbo.asys_barcodehistory  set description = '" + all_desc + "', karat = '" + all_karat + "', carat = '" + all_carat + "', weight = '" + price_weight + "' where refallbarcode = '" + refallbarcode + "'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new savewatch { Respons = "DATA UPDATED", Result = "1" };


            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_savewatch_Else_Update" + Ex.Message);
                return new savewatch { Respons = "Transaction Failed", Result = "0" };
            }

        }
        [WebMethod]
        public RetrieveInfo2 Pricing_RetrieveInfo2(string ALLBar)
        {
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                log.Info("Pricing_RetrieveInfo2");
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT refallbarcode,RefLotno, RefallBarcode, PTN, ItemID, PTNBarcode, BranchCode, BranchName,"
                + "Loanvalue, RefItemcode, Itemcode, BranchItemDesc, RefQty, Qty,  KaratGrading, CaratSize, Weight,"
                + "Actionclass,Sortcode, ALL_desc, SerialNo, ALL_karat, ALL_carat, ALL_Cost, ALL_Weight,currency,"
                + "PhotoName,all_price, Cellular_cost, Watch_cost, Repair_cost,Cleaning_cost, Gold_cost, Mount_cost,"
                + "YG_cost, WG_cost, MaturityDate, ExpiryDate, LoanDate, Status"
                + " FROM dbo.ASYS_MLWB_detail where refallbarcode = '" + ALLBar + "' and status <> 'RECPRICING'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        Item = (string.IsNullOrEmpty(dr["RefLotno"].ToString()) ? "NULL" : dr["RefLotno"].ToString().Trim());// dr["RefLotno"].ToString().Trim();
                        Item2 = (string.IsNullOrEmpty(dr["RefallBarcode"].ToString()) ? "NULL" : dr["RefallBarcode"].ToString().Trim());//dr["RefallBarcode"].ToString().Trim();
                        Item3 = (string.IsNullOrEmpty(dr["PTN"].ToString()) ? "NULL" : dr["PTN"].ToString().Trim());//dr["PTN"].ToString().Trim();
                        Item4 = (string.IsNullOrEmpty(dr["ItemID"].ToString()) ? "NULL" : dr["ItemID"].ToString().Trim());//dr["ItemID"].ToString().Trim();
                        Item5 = (string.IsNullOrEmpty(dr["PTNBarcode"].ToString()) ? "NULL" : dr["PTNBarcode"].ToString().Trim());//dr["PTNBarcode"].ToString().Trim();
                        Item6 = (string.IsNullOrEmpty(dr["BranchCode"].ToString()) ? "NULL" : dr["BranchCode"].ToString().Trim());//dr["BranchCode"].ToString().Trim();
                        Item7 = (string.IsNullOrEmpty(dr["BranchName"].ToString()) ? "NULL" : dr["BranchName"].ToString().Trim());//dr["BranchName"].ToString().Trim();
                        Item8 = (string.IsNullOrEmpty(dr["Loanvalue"].ToString()) ? "0" : dr["Loanvalue"].ToString().Trim());//dr["Loanvalue"].ToString().Trim();
                        Item9 = (string.IsNullOrEmpty(dr["RefItemcode"].ToString()) ? "NULL" : dr["RefItemcode"].ToString().Trim());//dr["RefItemcode"].ToString().Trim();
                        Item10 = (string.IsNullOrEmpty(dr["Itemcode"].ToString()) ? "NULL" : dr["Itemcode"].ToString().Trim());//dr["Itemcode"].ToString().Trim();
                        Item11 = (string.IsNullOrEmpty(dr["BranchItemDesc"].ToString()) ? "NULL" : dr["BranchItemDesc"].ToString().Trim());//dr["BranchItemDesc"].ToString().Trim();
                        Item12 = (string.IsNullOrEmpty(dr["RefQty"].ToString()) ? "0" : dr["RefQty"].ToString().Trim());//dr["RefQty"].ToString().Trim();
                        Item13 = (string.IsNullOrEmpty(dr["Qty"].ToString()) ? "0" : dr["Qty"].ToString().Trim());//dr["Qty"].ToString().Trim();
                        Item14 = (string.IsNullOrEmpty(dr["KaratGrading"].ToString()) ? "0" : dr["KaratGrading"].ToString().Trim());//dr["KaratGrading"].ToString().Trim();
                        Item15 = (string.IsNullOrEmpty(dr["CaratSize"].ToString()) ? "0" : dr["CaratSize"].ToString().Trim());//dr["CaratSize"].ToString().Trim();
                        Item16 = (string.IsNullOrEmpty(dr["Weight"].ToString()) ? "0" : dr["Weight"].ToString().Trim());//dr["Weight"].ToString().Trim();
                        Item17 = (string.IsNullOrEmpty(dr["Actionclass"].ToString()) ? "NULL" : dr["Actionclass"].ToString().Trim());//dr["Actionclass"].ToString().Trim();
                        Item18 = (string.IsNullOrEmpty(dr["Sortcode"].ToString()) ? "NULL" : dr["Sortcode"].ToString().Trim());//dr["Sortcode"].ToString().Trim();
                        Item19 = (string.IsNullOrEmpty(dr["ALL_desc"].ToString()) ? "NULL" : dr["ALL_desc"].ToString().Trim());//dr["ALL_desc"].ToString().Trim();
                        Item20 = (string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "0" : dr["SerialNo"].ToString().Trim());//dr["SerialNo"].ToString().Trim();
                        Item21 = (string.IsNullOrEmpty(dr["ALL_karat"].ToString()) ? "0" : dr["ALL_karat"].ToString().Trim());//dr["ALL_karat"].ToString().Trim();
                        Item22 = (string.IsNullOrEmpty(dr["ALL_carat"].ToString()) ? "0" : dr["ALL_carat"].ToString().Trim());//dr["ALL_carat"].ToString().Trim();
                        Item23 = (string.IsNullOrEmpty(dr["ALL_Cost"].ToString()) ? "0" : dr["ALL_Cost"].ToString().Trim());//dr["ALL_Cost"].ToString().Trim();
                        Item24 = (string.IsNullOrEmpty(dr["ALL_Weight"].ToString()) ? "0" : dr["ALL_Weight"].ToString().Trim());//dr["ALL_Weight"].ToString().Trim();
                        Item25 = (string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString().Trim());//dr["currency"].ToString().Trim();
                        Item26 = (string.IsNullOrEmpty(dr["PhotoName"].ToString()) ? "NULL" : dr["PhotoName"].ToString().Trim());//dr["PhotoName"].ToString().Trim();
                        Item27 = (string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());//dr["all_price"].ToString().Trim();
                        Item28 = (string.IsNullOrEmpty(dr["Cellular_cost"].ToString()) ? "0" : dr["Cellular_cost"].ToString().Trim());//dr["Cellular_cost"].ToString().Trim();
                        Item29 = (string.IsNullOrEmpty(dr["Watch_cost"].ToString()) ? "0" : dr["Watch_cost"].ToString().Trim());//dr["Watch_cost"].ToString().Trim();
                        Item30 = (string.IsNullOrEmpty(dr["Repair_cost"].ToString()) ? "0" : dr["Repair_cost"].ToString().Trim());//dr["Repair_cost"].ToString().Trim();
                        Item31 = (string.IsNullOrEmpty(dr["Cleaning_cost"].ToString()) ? "0" : dr["Cleaning_cost"].ToString().Trim());//dr["Cleaning_cost"].ToString().Trim();
                        Item32 = (string.IsNullOrEmpty(dr["Gold_cost"].ToString()) ? "0" : dr["Gold_cost"].ToString().Trim());//dr["Gold_cost"].ToString().Trim();
                        Item33 = (string.IsNullOrEmpty(dr["Mount_cost"].ToString()) ? "0" : dr["Mount_cost"].ToString().Trim());//dr["Mount_cost"].ToString().Trim();
                        Item34 = (string.IsNullOrEmpty(dr["YG_cost"].ToString()) ? "0" : dr["YG_cost"].ToString().Trim());//dr["YG_cost"].ToString().Trim();
                        Item35 = (string.IsNullOrEmpty(dr["WG_cost"].ToString()) ? "0" : dr["WG_cost"].ToString().Trim());//dr["WG_cost"].ToString().Trim();
                        Item36 = (string.IsNullOrEmpty(dr["MaturityDate"].ToString()) ? "NULL" : dr["MaturityDate"].ToString().Trim());//dr["MaturityDate"].ToString().Trim();
                        Item37 = (string.IsNullOrEmpty(dr["ExpiryDate"].ToString()) ? "NULL" : dr["ExpiryDate"].ToString().Trim());//dr["ExpiryDate"].ToString().Trim();
                        Item38 = (string.IsNullOrEmpty(dr["LoanDate"].ToString()) ? "NULL" : dr["LoanDate"].ToString().Trim());//dr["LoanDate"].ToString().Trim();
                        Item39 = (string.IsNullOrEmpty(dr["Status"].ToString()) ? "NULL" : dr["Status"].ToString().Trim());//dr["Status"].ToString().Trim();
                        fetch = " DATA FOUND";
                        container = "1";
                    }
                }

                else
                {
                    fetch = "NO DATA FOUND";
                    container = "0";
                }
                return new RetrieveInfo2
                {
                    Respons = fetch,
                    Result = container,
                    RefLotno = Item,
                    RefallBarcode = Item2,
                    PTN = Item3,
                    ItemID = Item4,
                    PTNBarcode = Item5,
                    BranchCode = Item6,
                    BranchName = Item7,
                    Loanvalue = Item8,
                    RefItemcode = Item9,
                    Itemcode = Item10,
                    BranchItemDesc = Item11,
                    RefQty = Item12,
                    Qty = Item13,
                    KaratGrading = Item14,
                    CaratSize = Item15,
                    Weight = Item16,
                    Actionclass = Item17,
                    Sortcode = Item18,
                    ALL_desc = Item19,
                    SerialNo = Item20,
                    ALL_karat = Item21,
                    ALL_carat = Item22,
                    ALL_Cost = Item23,
                    ALL_Weight = Item24,
                    currency = Item25,
                    PhotoName = Item26,
                    all_price = Item27,
                    Cellular_cost = Item28,
                    Watch_cost = Item29,
                    Repair_cost = Item30,
                    Cleaning_cost = Item31,
                    Gold_cost = Item32,
                    Mount_cost = Item33,
                    YG_cost = Item34,
                    WG_cost = Item35,
                    MaturityDate = Item36,
                    ExpiryDate = Item37,
                    LoanDate = Item38,
                    Status = Item39,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_RetrieveInfo2:" + Ex.Message);
                return new RetrieveInfo2 { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public btnSaveMLWB_Click Pricing_btnSaveMLWB_Click(string all_desc, string SerialNo, string refqty, string all_weight, string all_karat, string all_carat, string price_desc, string price_weight, string price_karat, string price_carat, string cellular_cost, string watch_cost, string Repair_cost, string cleaning_cost, string gold_cost, string mount_cost, string yg_cost, string wg_cost, string all_cost, string refallbarcode)
        {
            log.Info("Pricing_btnSaveMLWB_Click");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction Trans;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            Trans = conn.BeginTransaction();
            try
            {

                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;


                com.CommandText = ("Update asys_MLWB_detail set all_desc = '" + all_desc + "', SerialNo = '" + SerialNo + "', refqty = " + refqty + ", all_weight = " + all_weight + ","
                + "all_karat = '" + all_karat + "', all_carat = '" + all_carat + "',price_desc = '" + price_desc + "',  price_weight = '" + price_weight + "', price_karat = '" + price_karat + "',"
                + "price_carat = '" + price_carat + "', cellular_cost = '" + cellular_cost + "', watch_cost = '" + watch_cost + "', "
                + "Repair_cost = '" + Repair_cost + "',cleaning_cost = '" + cleaning_cost + "',"
                + "gold_cost = '" + gold_cost + "', mount_cost = '" + mount_cost + "',yg_cost = '" + yg_cost + "',"
                + "wg_cost = '" + yg_cost + "',all_cost = '" + all_cost + "' where refallbarcode ='" + refallbarcode + "' and status = 'RECEIVED'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();

                com.CommandText = ("Update ASYS_BarcodeHistory set Description = '" + all_desc + "',  Weight = '" + all_weight + "', Karat = '" + all_karat + "',"
                + "Carat = '" + all_carat + "',cost = '" + all_cost + "' where refallbarcode ='" + refallbarcode + "' and COSTCENTER = 'MLWB' and status = 'RECEIVED'");
                com.Transaction = Trans;
                com.ExecuteNonQuery();


                com.Transaction = Trans;
                Trans.Commit();
                conn.Close();

                return new btnSaveMLWB_Click { Respons = "DATA UPDATED", Result = "1" };

            }
            catch (Exception Ex)
            {
                Trans.Rollback();
                conn.Close();
                log.Error("Pricing_btnSaveMLWB_Click" + Ex.Message);
                return new btnSaveMLWB_Click { Respons = "Transaction Failed", Result = "0" };
            }


        }
        # endregion


        #region FIFTH REGION
        [WebMethod]
        public ComboBox6_SelectedIndexChanged Pricing_ComboBox6_SelectedIndexChanged(string Gold_Karat, bool pricing)
        {
            log.Info("Pricing_ComboBox6_SelectedIndexChanged");
            x = "REMS";
            String query;
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                if (pricing == true)
                {
                    query = "SELECT Gold_Karat, Plain FROM ASYS_GOLDKARAT WHERE Gold_Karat = '" + Gold_Karat + "'";
                }
                else
                {
                    query = "SELECT Gold_Karat, Plain FROM ASYS_GOLDKARAT_SUBASTA WHERE Gold_Karat = '" + Gold_Karat + "'";
                }

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = query;
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item = (string.IsNullOrEmpty(dr["Gold_Karat"].ToString()) ? "0.00" : dr["Gold_Karat"].ToString().Trim());//dr["Gold_Karat"].ToString().Trim();
                        Item2 = (string.IsNullOrEmpty(dr["Plain"].ToString()) ? "0.00" : dr["Plain"].ToString().Trim());//dr["Plain"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new ComboBox6_SelectedIndexChanged
                {
                    Respons = "aha",
                    Result = fetch,
                    Gold_Karat = Item,
                    Plain = Item2


                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_ComboBox6_SelectedIndexChanged:" + Ex.Message);
                return new ComboBox6_SelectedIndexChanged { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_GenerateLot()
        {
            log.Info("Releasing_GenerateLot");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.Data = new GenerateLot();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select * from asys_lotno_gen ";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        container.Data.Lotnumber = (string.IsNullOrEmpty(dr["lotno"].ToString()) ? "NULL" : dr["lotno"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing { Data = container.Data, Result = fetch };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_GenerateLot:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public releaser Releasing_releaser()
        {

            log.Info("Releasing_releaser");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                var list = new Releasing();
                list.releaserlist = new releaser();
                list.releaserlist.fullname2 = new List<string>();
                list.releaserlist.ID = new List<string>();
                list.releaserlist.res_id = new List<string>();
                list.releaserlist.fullname = new List<string>();
                list.releaserlist.sur_name = new List<string>();


                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select fullname as fullname2, * from rems.dbo.vw_humresvismin where job_title like 'distri%'order by fullname asc ";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.releaserlist.fullname2.Add(string.IsNullOrEmpty(dr["fullname2"].ToString()) ? "NULL" : dr["fullname2"].ToString().Trim());//(dr["fullname2"].ToString().Trim());
                        list.releaserlist.ID.Add(string.IsNullOrEmpty(dr["ID"].ToString()) ? "NULL" : dr["ID"].ToString().Trim());//(dr["ID"].ToString().Trim());
                        list.releaserlist.res_id.Add(string.IsNullOrEmpty(dr["res_id"].ToString()) ? "NULL" : dr["res_id"].ToString().Trim());//(dr["res_id"].ToString().Trim());
                        list.releaserlist.fullname.Add(string.IsNullOrEmpty(dr["fullname"].ToString()) ? "NULL" : dr["fullname"].ToString().Trim());//(dr["fullname"].ToString().Trim());
                        list.releaserlist.sur_name.Add(string.IsNullOrEmpty(dr["sur_name"].ToString()) ? "NULL" : dr["sur_name"].ToString().Trim());//(dr["sur_name"].ToString().Trim());
                        fetch = "DATA FOUND";
                        value = "1";
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    value = "0";
                }
                return new releaser
                {
                    fullname2 = list.releaserlist.fullname2,
                    ID = list.releaserlist.ID,
                    res_id = list.releaserlist.res_id,
                    fullname = list.releaserlist.fullname,
                    sur_name = list.releaserlist.sur_name,
                    Respons = fetch,
                    Result = value
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_releaser:" + Ex.Message);
                return new releaser { Result = "0", Respons = Ex.Message };
            }

        }
        [WebMethod]
        public Lotno Releasing_Lotno()
        {
            log.Info("Releasing_Lotno");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var list = new Releasing();
                list.lotnolist = new Lotno();
                list.lotnolist.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select distinct LOTno from asys_PRICING_detail where status = 'PRICED' order by lotno desc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.lotnolist.lotnumber.Add(string.IsNullOrEmpty(dr["LOTno"].ToString()) ? "NULL" : dr["LOTno"].ToString().Trim());//(dr["LOTno"].ToString().Trim());

                        fetch = "DATA FOUND";
                        value = "1";
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    value = "0";
                }
                return new Lotno
                {
                    lotnumber = list.lotnolist.lotnumber,
                    Respons = fetch,
                    Result = value
                };
            }
            catch (Exception Ex)
            {
                log.Error("Releasing_Lotno:" + Ex.Message);
                conn.Close();
                return new Lotno { Result = "0", Respons = Ex.Message };
            }

        }
        [WebMethod]
        public Lotno Releasing_Lotno2()
        {
            log.Info("Releasing_Lotno2");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var list = new Releasing();
                list.lotnolist = new Lotno();
                list.lotnolist.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select distinct LOTno from asys_PRICING_detail where status = 'PRICED' order by lotno desc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.lotnolist.lotnumber.Add(dr["LOTno"].ToString().Trim());

                        fetch = "DATA FOUND";
                        value = "1";
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    value = "0";
                }
                return new Lotno
                {
                    lotnumber = list.lotnolist.lotnumber,
                    Respons = fetch,
                    Result = value
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_Lotno2:" + Ex.Message);
                return new Lotno { Result = "0", Respons = Ex.Message };
            }

        }
        [WebMethod]
        public Releasing Releasing_addrow(string lotno)
        {
            log.Info("Releasing_addrow");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.addrowref = new addrow();
                container.addrowref.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT * FROM ASYS_PRICING_detail WHERE lotno = '" + lotno + "' and status = 'PRICED' ";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.addrowref.lotnumber.Add(dr["RefLotno"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing { addrowref = container.addrowref, Result = fetch };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_addrow:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_addrow2(string lotno)
        {
            log.Info("Releasing_addrow2");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection cn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.addrowref = new addrow();
                DataSet ds1 = new DataSet();

                using (cn)
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT case when ptn is NULL then '0' else ptn end ptn, refALLBarcode,price_desc,price_weight,price_karat,price_carat,case when all_price is null then '0' else all_price end all_price FROM ASYS_PRICING_detail WHERE lotno= '" + lotno + "' AND status = 'priced'", cn))
                        da.Fill(ds1, "TableName1");
                    cn.Close();

                }

                return new Releasing { Result = "1", Respons = fetch, dataset = ds1 };
            }
            catch (Exception Ex)
            {
                cn.Close();
                log.Error("Releasing_addrow2:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_addrow3(string lotno)
        {
            log.Info("Releasing_addrow3");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.addrowref = new addrow();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT price_desc,price_weight,price_karat,price_carat,all_price as all_value FROM ASYS_PRICING_detail WHERE refallbarcode ='" + lotno + "' AND status = 'PRICED'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.addrowref.price_desc = (string.IsNullOrEmpty(dr["price_desc"].ToString()) ? "NULL" : dr["price_desc"].ToString().Trim());//dr["price_desc"].ToString().Trim();
                        container.addrowref.price_weight = (string.IsNullOrEmpty(dr["price_weight"].ToString()) ? "0" : dr["price_weight"].ToString().Trim());//dr["price_weight"].ToString().Trim();
                        container.addrowref.price_karat = (string.IsNullOrEmpty(dr["price_karat"].ToString()) ? "0" : dr["price_karat"].ToString().Trim());//dr["price_karat"].ToString().Trim();
                        container.addrowref.price_carat = (string.IsNullOrEmpty(dr["price_carat"].ToString()) ? "0" : dr["price_carat"].ToString().Trim());//dr["price_carat"].ToString().Trim();
                        container.addrowref.all_value = (string.IsNullOrEmpty(dr["all_value"].ToString()) ? "0" : dr["all_value"].ToString().Trim());//dr["all_value"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing { addrowref = container.addrowref, Result = fetch };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_addrow3:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_saveblehtrue(string templot, string[][] dgEntry, string user, string userLog, string cmbo)
        {
            log.Info("Releasing_saveblehtrue");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction tranCon;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            tranCon = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand("update asys_lotno_gen set lotno ='" + templot + "' WHERE BusinessCenter ='PRICING'", conn, tranCon))
                {
                    cmd.ExecuteNonQuery();
                    value = "Success!";
                }
                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    if (dgEntry[i] != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into asys_barcodehistory (lotno,refallbarcode, allbarcode, itemcode, " +
                        "itemid, [description], karat, carat, SerialNo, weight, currency, price, cost, empname, custodian,trandate," +
                        " costcenter, consignto, status) select '" + templot + "' as reflotno,'" + dgEntry[i][0] +
                        "' as refallbarcode,'" + dgEntry[i][0] + "' as  allbarcode,  asys_PRICING_detail.itemcode, asys_PRICING_detail.itemid," +
                        " asys_PRICING_detail.price_desc, asys_PRICING_detail.price_karat, asys_PRICING_detail.price_carat, " +
                        "asys_PRICING_detail.SerialNo, asys_PRICING_detail.price_weight, asys_PRICING_detail.currency, asys_PRICING_detail.all_price," +
                        " asys_PRICING_detail.all_cost, '" + user + "' as empname, asys_PRICING_detail.receiver as custodian, getdate()" +
                        " as trandate, 'PRICING', 'NULL', 'RELEASED' from asys_PRICING_detail where asys_PRICING_detail.refallbarcode" +
                        " = '" + dgEntry[i][0] + "' and asys_PRICING_detail.status = 'PRICED' ", conn, tranCon))
                        {
                            cmd.ExecuteNonQuery();
                            value = "Success!";
                        }
                        using (SqlCommand cmd = new SqlCommand("Update asys_PRICING_detail set reflotno ='" + templot + "', releasedate = " +
                            "getdate(), releaser = '" + userLog + "',custodian = '" + cmbo + "', status = 'RELEASED' where refallbarcode='" + dgEntry[i][0] +
                            "' and status = 'PRICED'  ", conn, tranCon))
                        {
                            cmd.ExecuteNonQuery();
                            fetch = "1";
                            value = "Success!";
                        }
                    }
                    else
                    {
                        fetch = "0";
                        value = "failed";
                        break;
                    }
                }
                tranCon.Commit();
                conn.Close();
                return new Releasing { Respons = value, Result = fetch };
            }
            catch (Exception Ex)
            {
                tranCon.Rollback();
                conn.Close();
                log.Error("Releasing_saveblehtrue:" + Ex.Message);
                return new Releasing { Respons = "Transaction Failed", Result = "0" };
            }


        }
        [WebMethod]
        public Releasing Releasing_btnSave_Click(string templot, string[][] dgEntry, string user, string userLog, string cmbo)
        {
            log.Info("Releasing_btnSave_Click");
            x = "REMS";
            Int32 tempIncrement;
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction tranCon;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            tranCon = conn.BeginTransaction();
            try
            {

                ASYS_Consign_Header(templot);
                if (consign_header == true && exit == false)
                {
                    consign_header = false;
                    tempIncrement = Convert.ToInt32(templot) + 1;
                    templot = tempIncrement.ToString();
                }
                else if (consign_header == false && exit == false)
                {

                }
                else
                {
                    tranCon.Rollback();
                    return new Releasing { Result = "0", Respons = "service error: connection was interupted" };
                }


                for (int i = 0; i <= dgEntry.Count() - 1; i++)
                {
                    if (dgEntry[i] != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO REMS.DBO.ASYS_Barcodehistory (lotno, refallbarcode,allbarcode,"
                            + "itemcode, itemid, [description], karat, carat, SerialNo, weight, currency, price, cost, empname, custodian,"
                            + "trandate, costcenter, consignto, status) SELECT '" + templot + "', '" + dgEntry[i][0] + "','" + dgEntry[i][0] + "',"
                            + "asys_PRICING_detail.refitemcode, asys_PRICING_detail.itemid, asys_PRICING_detail.price_desc,asys_PRICING_detail.price_karat, "
                            + "asys_PRICING_detail.price_carat, asys_PRICING_detail.SerialNo, asys_PRICING_detail.price_weight, asys_PRICING_detail.currency,"
                            + "asys_PRICING_detail.all_price, asys_PRICING_detail.all_price, '" + user + "' as empname, receiver as custodian, "
                            + "getdate(), 'PRICING', 'NULL', 'RELEASED' FROM ASYS_PRICING_Detail WHERE asys_PRICING_detail.refallbarcode = '" + dgEntry[i][0] + "' "
                            + "AND status = 'PRICED'", conn, tranCon))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("UPDATE ASYS_PRICING_detail SET reflotno ='" + templot + "',releaser = '" + userLog + "',"
                            + "releasedate = GETDATE(), custodian = '" + cmbo + "', status = 'RELEASED' WHERE refallbarcode ='" + dgEntry[i][0] + "' "
                            + "AND status = 'PRICED'", conn, tranCon))
                        {
                            cmd.ExecuteNonQuery();
                            fetch = "1";
                            value = "Success!";
                        }
                    }
                    else
                    {
                        fetch = "0";
                        value = "failed";
                        // break;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE asys_lotno_gen SET lotno ='" + templot + "' WHERE BusinessCenter ='PRICING'", conn, tranCon))
                {
                    cmd.ExecuteNonQuery();
                }
                tranCon.Commit();
                conn.Close();
                return new Releasing { Respons = value, Result = fetch };


            }
            catch (Exception Ex)
            {
                tranCon.Rollback();
                conn.Close();
                log.Error("Releasing_btnSave_Click:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }


        }
        public Boolean consign_header;
        public Boolean exit;
        public void ASYS_Consign_Header(string lot1234)
        {

            string constring = ConfigurationManager.ConnectionStrings["REMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    con.Open();
                    string query = "SELECT TOP 1 * FROM REMS.DBO.ASYS_PRICING_DETAIL WHERE REFLOTNO = '" + lot1234 + "' AND STATUS = 'PRICED'";//"select * from ASYS_Consign_Header where lotno = ''";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (rdr.HasRows)
                            {
                                consign_header = true;
                            }
                            else
                            {

                                consign_header = false;
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    log.Error("getLotNumDetails: " + ex.Message);
                    exit = true;
                }

            }

        }
        [WebMethod]
        public Releasing Releasing_lotnorefresh()
        {
            log.Info("Releasing_lotnorefresh");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.lotnumberlist = new lotnorefresh();
                container.lotnumberlist.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select distinct reflotno from ASYS_PRicing_Detail where status = 'PRICED' order by reflotno desc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.lotnumberlist.lotnumber.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());//(dr["reflotno"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = "aha",
                    Result = fetch,
                    lotnumberlist = container.lotnumberlist,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_lotnorefresh:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_cmbCostCenter_KeyPress()
        {
            log.Info("Releasing_cmbCostCenter_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                var container = new Releasing();
                container.lotnumberlist2 = new cmbCostCenter_KeyPress();
                container.lotnumberlist2.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select distinct reflotno from asys_PRICING_detail where status= 'PRICED'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.lotnumberlist2.lotnumber.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());//(dr["reflotno"].ToString().Trim());
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = "aha",
                    Result = fetch,
                    lotnumberlist2 = container.lotnumberlist2,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_cmbCostCenter_KeyPress:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_cmbbarcode_KeyPress(string ALLBar)
        {
            log.Info("Releasing_cmbbarcode_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.items = new cmbbarcode_KeyPress();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT RefallBarcode, PTN, RefItemcode, RefQty, price_desc as [desc],price_karat as karat, price_carat as carat, all_price as cost, price_Weight as wt,status FROM dbo.ASYS_PRICING_detail WHERE refallbarcode = '" + ALLBar + "' AND status = 'PRICED'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.items.RefallBarcode = (string.IsNullOrEmpty(dr["RefallBarcode"].ToString()) ? "NULL" : dr["RefallBarcode"].ToString().Trim());//dr["RefallBarcode"].ToString().Trim();
                        container.items.PTN = (string.IsNullOrEmpty(dr["PTN"].ToString()) ? "NULL" : dr["PTN"].ToString().Trim());//dr["PTN"].ToString().Trim();
                        container.items.RefItemcode = (string.IsNullOrEmpty(dr["RefItemcode"].ToString()) ? "NULL" : dr["RefItemcode"].ToString().Trim());//dr["RefItemcode"].ToString().Trim();
                        container.items.RefQty = (string.IsNullOrEmpty(dr["RefQty"].ToString()) ? "NULL" : dr["RefQty"].ToString().Trim());//dr["RefQty"].ToString().Trim();
                        container.items.desc = (string.IsNullOrEmpty(dr["desc"].ToString()) ? "NULL" : dr["desc"].ToString().Trim());//dr["desc"].ToString().Trim();
                        container.items.karat = (string.IsNullOrEmpty(dr["karat"].ToString()) ? "0" : dr["karat"].ToString().Trim());//dr["karat"].ToString().Trim();
                        container.items.carat = (string.IsNullOrEmpty(dr["carat"].ToString()) ? "0" : dr["carat"].ToString().Trim());//dr["carat"].ToString().Trim();
                        container.items.cost = (string.IsNullOrEmpty(dr["cost"].ToString()) ? "0" : dr["cost"].ToString().Trim());//dr["cost"].ToString().Trim();
                        container.items.wt = (string.IsNullOrEmpty(dr["wt"].ToString()) ? "NULL" : dr["wt"].ToString().Trim());//dr["wt"].ToString().Trim();
                        container.items.status = (string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());//dr["status"].ToString().Trim();
                    }
                    fetch = "2";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = "aha",
                    Result = fetch,
                    items = container.items
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_cmbbarcode_KeyPress:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_cb_SelectedIndexChanged(string humres, string fullname)
        {
            log.Info("Releasing_cb_SelectedIndexChanged");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.fullname = new cb_SelectedIndexChanged();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select rtrim(fullname) as fullname3 from  " + humres + " where fullname='" + fullname + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.fullname.fullname3 = (string.IsNullOrEmpty(dr["fullname3"].ToString()) ? "NULL" : dr["fullname3"].ToString().Trim());//dr["fullname3"].ToString().Trim();
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = "aha",
                    Result = fetch,
                    fullname = container.fullname
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_cb_SelectedIndexChanged:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_cb_SelectedIndexChanged2()
        {
            log.Info("Releasing_cb_SelectedIndexChanged2");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Releasing();
                container.lot = new cb_SelectedIndexChanged2();
                container.lot.lotnumber = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select distinct(asys_pricing_detail.lotno) as lotno from asys_pricing_header inner join asys_pricing_detail on asys_pricing_header.lotno = asys_pricing_detail.lotno where asys_pricing_detail.status = 'PRICED' order by asys_pricing_detail.lotno asc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.lot.lotnumber.Add(string.IsNullOrEmpty(dr["lotno"].ToString()) ? "NULL" : dr["lotno"].ToString().Trim());//(dr["lotno"].ToString().Trim());
                    }
                    fetch = "1";
                    dr.Close();
                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = "aha",
                    Result = fetch,
                    lot = container.lot
                };
            }
            catch (Exception Ex)
            {
                log.Error("Releasing_cb_SelectedIndexChanged2:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing Releasing_cmbItems_KeyPress(string txtCostCenter, string cmbbarcode)
        {
            log.Info("Releasing_cmbItems_KeyPress");
            var list = new PricingResult();
            list.Action_type = new RetrieveDatabyAction_Type();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                if (txtCostCenter == "MLWB")
                {
                    txtCostCenter = "select * from tbl_recrel_MLWB_dtail where  Allbarcode = '" + cmbbarcode + "' and Status_ID_dRec = 1";
                }
                if (txtCostCenter == "PRICING")
                {
                    txtCostCenter = "select * from tbl_recrel_PRICING_dtail where  Allbarcode = '" + cmbbarcode + "' and Status_ID_dRec = 1";
                }
                if (txtCostCenter == "DISTRI")
                {
                    txtCostCenter = "select * from tbl_recrel_DISTRI_dtail where  Allbarcode = '" + cmbbarcode + "' and Status_ID_dRec = 1";
                }
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = container;
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {

                    if (txtCostCenter == "MLWB")
                    {
                        while (dr.Read())
                        {

                        }
                        dr.Close();
                    }

                    if (txtCostCenter == "PRICING")
                    {
                        while (dr.Read())
                        {


                        }
                        dr.Close();
                    }

                    if (txtCostCenter == "DISTRI")
                    {
                        while (dr.Read())
                        {

                        }
                        dr.Close();
                    }
                    fetch = "1";

                }
                else
                {
                    fetch = "0";
                }
                conn.Close();
                return new Releasing
                {
                    Respons = txtCostCenter,
                    Result = fetch,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_cmbItems_KeyPress:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0" };
            }
        }
        [WebMethod]
        public ViewPricedItem ViewPricedItem_TextBox1_KeyPress(string reflotno)
        {
            log.Info("ViewPricedItem_TextBox1_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new ViewPricedItem();
                container.List = new TextBox1_KeyPress();
                container.List.allbarcode = new List<string>();
                container.List.price_desc = new List<string>();
                container.List.all_price = new List<string>();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select refallbarcode as allbarcode,price_desc ,all_price from ASYS_PRICING_Detail where reflotno = '" + reflotno + "' and status in ('RECEIVED','PRICED') order by refallbarcode asc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container.List.allbarcode.Add(string.IsNullOrEmpty(dr["allbarcode"].ToString()) ? "NULL" : dr["allbarcode"].ToString().Trim());//(dr["allbarcode"].ToString().Trim());
                        container.List.price_desc.Add(string.IsNullOrEmpty(dr["price_desc"].ToString()) ? "NULL" : dr["price_desc"].ToString().Trim());//(dr["price_desc"].ToString().Trim());
                        container.List.all_price.Add(string.IsNullOrEmpty(dr["all_price"].ToString()) ? "0" : dr["all_price"].ToString().Trim());//dr["all_price"].ToString().Trim());
                        fetch = "DATA FOUND";
                        identifier = "1";
                    }
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    identifier = "2";
                }
                conn.Close();
                return new ViewPricedItem
                {
                    Respons = fetch,
                    Result = identifier,
                    List = container.List
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("ViewPricedItem_TextBox1_KeyPress:" + Ex.Message);
                return new ViewPricedItem { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Pricing_Report Report_Method(string Plotno)
        {
            log.Info("Report_Method");
            var list = new Pricing_Report();
            list._ViewPricedItem = new List<ViewPricedItem_Report>();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "ASYS_ViewPricedItem";
                com.Parameters.AddWithValue("@Plotno", Plotno);

                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        var stored = new ViewPricedItem_Report();

                        stored.RefLotno = dr["RefLotno"].ToString().Trim();
                        stored.branchCode = dr["branchCode"].ToString().Trim();
                        stored.branchname = dr["branchname"].ToString().Trim();
                        stored.receivedate = Convert.ToDateTime(dr["receivedate"]).ToString("MMMM dd, yyyy").Trim();
                        stored.custodian = dr["custodian"].ToString().Trim();
                        stored.refallbarcode = dr["refallbarcode"].ToString().Trim();
                        stored.price_desc = dr["price_desc"].ToString().Trim();
                        stored.serialno = dr["serialno"].ToString().Trim();
                        stored.quantity = string.IsNullOrEmpty(dr["quantity"].ToString()) ? 0 : Convert.ToInt32(dr["quantity"]);
                        stored.price_karat = dr["price_karat"].ToString().Trim();
                        stored.price_weight = string.IsNullOrEmpty(dr["price_weight"].ToString()) ? 0 : Convert.ToDouble(dr["price_weight"]);
                        stored.price_carat = string.IsNullOrEmpty(dr["price_carat"].ToString()) ? 0 : Convert.ToDouble(dr["price_carat"]);
                        stored.all_price = string.IsNullOrEmpty(dr["all_price"].ToString()) ? 0 : Convert.ToDouble(dr["all_price"]);
                        list._ViewPricedItem.Add(stored);

                        fetch = "Data Found";
                        Item = "2";
                    }
                }
                else
                {
                    fetch = "No Data Found";
                    Item = "3";
                }
                conn.Close();
                return new Pricing_Report
                {
                    Respons = fetch,
                    Result = Item,
                    _ViewPricedItem = list._ViewPricedItem
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Report_Method:" + Ex.Message);
                return new Pricing_Report { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Receiving Receiving_ReadyPrintForm1(string LotNo)
        {
            log.Info("Receiving_ReadyPrintForm1");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Receiving();
                container._ReadyPrintForm1 = new ReadyPrintForm1();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT consigncode, consignname FROM ASYS_Consign_Header WHERE LotNo = '" + LotNo + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container._ReadyPrintForm1.consigncode = (string.IsNullOrEmpty(dr["consigncode"].ToString()) ? "NULL" : dr["consigncode"].ToString().Trim());//dr["consigncode"].ToString().Trim();
                        container._ReadyPrintForm1.consignname = (string.IsNullOrEmpty(dr["consignname"].ToString()) ? "NULL" : dr["consignname"].ToString().Trim());//dr["consignname"].ToString().Trim();
                        fetch = "DATA FOUND";
                        Item = "1";
                    }
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    Item = "0";
                }
                conn.Close();
                return new Receiving
                {
                    Respons = fetch,
                    Result = Item,
                    _ReadyPrintForm1 = container._ReadyPrintForm1,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_ReadyPrintForm1:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Receiving Receiving_ReadyPrintForm1_2(string consigncode, string consingname)
        {
            log.Info("Receiving_ReadyPrintForm1_2");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Receiving();
                container._ReadyPrintForm2 = new ReadyPrintForm2();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "IF EXISTS(SELECT BranchId FROM ASYS_BranchAddress WHERE BranchCode='" + consigncode + "' AND BranchName='" + consingname + "')SELECT TOP 1 'SHOWROOM' AS ConsignTo FROM ASYS_BranchAddress ELSE SELECT TOP 1 'VIP' AS ConsignTo FROM ASYS_BranchAddress";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container._ReadyPrintForm2.consignto = (string.IsNullOrEmpty(dr["consignto"].ToString()) ? "NULL" : dr["consignto"].ToString().Trim());//dr["consignto"].ToString().Trim();
                        fetch = "DATA FOUND";
                        Item = "1";
                    }
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    Item = "0";
                }
                conn.Close();
                return new Receiving
                {
                    Respons = fetch,
                    Result = Item,
                    _ReadyPrintForm2 = container._ReadyPrintForm2,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_ReadyPrintForm1_2:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Receiving Receiving_searchLotNumber()
        {
            log.Info("Receiving_searchLotNumber");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Receiving();
                container._searchLotNumber = new searchLotNumber();
                container._searchLotNumber.LotNum = new List<string>();
                container._searchLotNumber.EmpName1 = new List<string>();
                container._searchLotNumber.receiveDate = new List<string>();
                container._searchLotNumber.releasedate = new List<string>();
                searchLotNumber emt = new searchLotNumber();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT DISTINCT LotNo as LotNum, upper(receiver) as EmpName1, convert(nvarchar,receivedate,101) as receivedDate FROM asys_PRICING_detail where actionclass in ('return','outsource','goodstock', 'cellular', 'watch')  ORDER BY LotNum desc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container._searchLotNumber.LotNum.Add(string.IsNullOrEmpty(dr["LotNum"].ToString()) ? "NULL" : dr["LotNum"].ToString().Trim());//(dr["LotNum"].ToString().Trim());
                        container._searchLotNumber.EmpName1.Add(string.IsNullOrEmpty(dr["EmpName1"].ToString()) ? "NULL" : dr["EmpName1"].ToString().Trim());//(dr["EmpName1"].ToString().Trim());
                        container._searchLotNumber.receiveDate.Add(string.IsNullOrEmpty(dr["receivedDate"].ToString()) ? "NULL" : dr["receivedDate"].ToString().Trim());//(dr["receivedDate"].ToString().Trim());
                        fetch = "DATA FOUND";
                        Item = "1";
                    }
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    Item = "0";
                }
                conn.Close();
                return new Receiving
                {
                    Respons = fetch,
                    Result = Item,
                    _searchLotNumber = container._searchLotNumber
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_searchLotNumber:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Receiving Receiving_searchLotNumber2()
        {
            log.Info("Receiving_searchLotNumber2");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                var container = new Receiving();
                container._searchLotNumber = new searchLotNumber();
                container._searchLotNumber.LotNum = new List<string>();
                container._searchLotNumber.EmpName1 = new List<string>();
                container._searchLotNumber.receiveDate = new List<string>();
                container._searchLotNumber.releasedate = new List<string>();
                searchLotNumber emt = new searchLotNumber();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT DISTINCT releasedate,receiveDate FROM asys_PRICING_detail where actionclass in ('return','outsource','goodstock', 'cellular', 'watch')  ORDER BY releasedate desc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        container._searchLotNumber.releasedate.Add(string.IsNullOrEmpty(dr["releasedate"].ToString()) ? "NULL" : dr["releasedate"].ToString().Trim());//(dr["EmpName1"].ToString().Trim());
                        container._searchLotNumber.receiveDate.Add(string.IsNullOrEmpty(dr["receiveDate"].ToString()) ? "NULL" : dr["receiveDate"].ToString().Trim());//(dr["receivedDate"].ToString().Trim());
                        fetch = "DATA FOUND";
                        Item = "1";
                    }
                }
                else
                {
                    fetch = "NO DATA FOUND";
                    Item = "0";
                }
                conn.Close();
                return new Receiving
                {
                    Respons = fetch,
                    Result = Item,
                    _searchLotNumber = container._searchLotNumber
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_searchLotNumber2:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Pricing Pricing_SaveMounted_Data(string[][] DgEntry, string tmpLotNo, string cboALL)
        {
            log.Info("Pricing_SaveMounted_Data");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction tranCon;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            tranCon = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ASYS_Gold WHERE AllBarcode = '" + cboALL + "'", conn, tranCon))
                {
                    cmd.CommandTimeout = 400000;
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i <= DgEntry.Count() - 1; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ASYS_Gold WHERE AllBarcode = '" + cboALL + "'", conn, tranCon))
                    {
                        cmd.CommandTimeout = 400000;
                        cmd.ExecuteNonQuery();
                    }
                }
                tranCon.Commit();
                conn.Close();
                return new Pricing { Result = "Items are Saved!", Respons = "1" };
            }
            catch (Exception ex)
            {
                tranCon.Rollback();
                conn.Close();
                log.Error("Pricing_SaveMounted_Data:" + ex.Message);
                return new Pricing { Result = "Transaction Failed" + ex.Message, Respons = "0" };
            }


        }
        #endregion


        #region SIXTH REGION

        [WebMethod]
        public Pricing_Report Pricing_receiving_Report(string hlot)
        {
            log.Info("Pricing_receiving_Report");
            var list = new Pricing_Report();
            list._pricing_receiving_Report = new List<pricing_receiving_report>();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);

            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "ASYS_PRICINGSummaryGoodStocks_Rcv_rpt";
                com.Parameters.AddWithValue("@hlot", hlot);

                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var stored = new pricing_receiving_report();

                        stored.lotno = string.IsNullOrEmpty(dr["lotno"].ToString()) ? "NULL" : dr["lotno"].ToString().Trim();
                        stored.reflotno = string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim();
                        stored.receiver = string.IsNullOrEmpty(dr["receiver"].ToString()) ? "NULL" : dr["receiver"].ToString().Trim();
                        stored.employee = string.IsNullOrEmpty(dr["employee"].ToString()) ? "NULL" : dr["employee"].ToString().Trim();
                        stored.receivedate = string.IsNullOrEmpty(dr["receivedate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["receivedate"]).ToString("MMMM dd, yyyy").Trim();
                        stored.refallbarcode = string.IsNullOrEmpty(dr["refallbarcode"].ToString()) ? "NULL" : dr["refallbarcode"].ToString().Trim();
                        stored.allbarcode = string.IsNullOrEmpty(dr["allbarcode"].ToString()) ? "NULL" : dr["allbarcode"].ToString().Trim();
                        stored.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim();
                        stored.itemid = string.IsNullOrEmpty(dr["itemid"].ToString()) ? 0 : Convert.ToInt32(dr["itemid"]);
                        stored.ptnbarcode = string.IsNullOrEmpty(dr["ptnbarcode"].ToString()) ? "NULL" : dr["ptnbarcode"].ToString().Trim();
                        stored.branchcode = string.IsNullOrEmpty(dr["branchcode"].ToString()) ? "NULL" : dr["branchcode"].ToString().Trim();
                        stored.branchname = string.IsNullOrEmpty(dr["branchname"].ToString()) ? "NULL" : dr["branchname"].ToString().Trim();
                        stored.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                        stored.refitemcode = string.IsNullOrEmpty(dr["refitemcode"].ToString()) ? "NULL" : dr["refitemcode"].ToString().Trim();
                        stored.itemcode = string.IsNullOrEmpty(dr["itemcode"].ToString()) ? "NULL" : dr["itemcode"].ToString().Trim();
                        stored.branchitemdesc = string.IsNullOrEmpty(dr["branchitemdesc"].ToString()) ? "NULL" : dr["branchitemdesc"].ToString().Trim();
                        stored.SerialNo = string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "NULL" : dr["SerialNo"].ToString().Trim();
                        stored.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                        stored.qty = string.IsNullOrEmpty(dr["qty"].ToString()) ? 0 : Convert.ToInt32(dr["qty"]);
                        stored.karatgrading = string.IsNullOrEmpty(dr["karatgrading"].ToString()) ? "NULL" : dr["karatgrading"].ToString().Trim();
                        stored.caratsize = string.IsNullOrEmpty(dr["caratsize"].ToString()) ? 0 : Convert.ToDouble(dr["caratsize"]);
                        stored.weight = string.IsNullOrEmpty(dr["weight"].ToString()) ? 0 : Convert.ToDouble(dr["weight"]);
                        stored.actionclass = string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim();
                        stored.sortcode = string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim();
                        stored.all_desc = string.IsNullOrEmpty(dr["all_desc"].ToString()) ? "NULL" : dr["all_desc"].ToString().Trim();
                        stored.all_karat = string.IsNullOrEmpty(dr["all_karat"].ToString()) ? "NULL" : dr["all_karat"].ToString().Trim();
                        stored.all_carat = string.IsNullOrEmpty(dr["all_carat"].ToString()) ? 0 : Convert.ToDouble(dr["all_carat"]);
                        stored.all_cost = string.IsNullOrEmpty(dr["all_cost"].ToString()) ? 0 : Convert.ToDouble(dr["all_cost"]);
                        stored.all_weight = string.IsNullOrEmpty(dr["all_weight"].ToString()) ? 0 : Convert.ToDouble(dr["all_weight"]);
                        stored.appraisevalue = string.IsNullOrEmpty(dr["appraisevalue"].ToString()) ? 0 : Convert.ToDouble(dr["appraisevalue"]);
                        stored.currency = string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString();
                        stored.photoname = string.IsNullOrEmpty(dr["photoname"].ToString()) ? "NULL" : dr["photoname"].ToString().Trim();
                        stored.pricedesc = string.IsNullOrEmpty(dr["pricedesc"].ToString()) ? "NULL" : dr["pricedesc"].ToString().Trim();
                        stored.price_karat = string.IsNullOrEmpty(dr["price_karat"].ToString()) ? "NULL" : dr["price_karat"].ToString().Trim();
                        stored.price_weight = string.IsNullOrEmpty(dr["price_weight"].ToString()) ? 0 : Convert.ToDouble(dr["price_weight"]);
                        stored.price_carat = string.IsNullOrEmpty(dr["price_carat"].ToString()) ? 0 : Convert.ToDouble(dr["price_carat"]);
                        stored.all_price = string.IsNullOrEmpty(dr["all_price"].ToString()) ? 0 : Convert.ToDouble(dr["all_price"]);
                        stored.cellular_cost = string.IsNullOrEmpty(dr["cellular_cost"].ToString()) ? 0 : Convert.ToDouble(dr["cellular_cost"]);
                        stored.watch_cost = string.IsNullOrEmpty(dr["watch_cost"].ToString()) ? 0 : Convert.ToDouble(dr["watch_cost"]);
                        stored.repair_cost = string.IsNullOrEmpty(dr["repair_cost"].ToString()) ? 0 : Convert.ToDouble(dr["repair_cost"]);
                        stored.cleaning_cost = string.IsNullOrEmpty(dr["cleaning_cost"].ToString()) ? 0 : Convert.ToDouble(dr["cleaning_cost"]);
                        stored.gold_cost = string.IsNullOrEmpty(dr["gold_cost"].ToString()) ? 0 : Convert.ToDouble(dr["gold_cost"]);
                        stored.costdate = string.IsNullOrEmpty(dr["costdate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["costdate"]).ToString("MMM dd yyyy").Trim();
                        stored.costname = string.IsNullOrEmpty(dr["costname"].ToString()) ? "NULL" : dr["costname"].ToString().Trim();
                        stored.releasedate = string.IsNullOrEmpty(dr["releasedate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["releasedate"]).ToString("MMM dd yyyy").Trim();
                        stored.releaser = string.IsNullOrEmpty(dr["releaser"].ToString()) ? "NULL" : dr["releaser"].ToString().Trim();
                        stored.maturitydate = string.IsNullOrEmpty(dr["maturitydate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["maturitydate"]).ToString("MMM dd yyyy").Trim();
                        stored.expirydate = string.IsNullOrEmpty(dr["expirydate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["expirydate"]).ToString("MMM dd yyyy").Trim();
                        stored.loandate = string.IsNullOrEmpty(dr["loandate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["loandate"]).ToString("MMM dd yyyy").Trim();
                        stored.status = string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim();
                        stored.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "MONTH IS NOT IS SPECIFIED" : dr["month"].ToString().Trim();
                        stored.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "YEAR IS NOT IS SPECIFIED" : dr["year"].ToString().Trim();
                        fetch = "Data Found";
                        Item = "1";
                        list._pricing_receiving_Report.Add(stored);
                        f = string.IsNullOrEmpty(dr["receivedate"].ToString()) ? "NO DATE" : Convert.ToDateTime(dr["receivedate"]).ToString("MMMM dd, yyyy").Trim();
                        f.ToUpper();
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "No Data Found";
                    Item = "2";
                    dr.Close();

                }
                conn.Close();
                return new Pricing_Report
                {
                    Respons = fetch,
                    Result = Item,
                    _pricing_receiving_Report = list._pricing_receiving_Report,
                    receivedate = f
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_receiving_Report:" + Ex.Message);
                return new Pricing_Report { Respons = Ex.Message, Result = "0" };
            }
        }
        [WebMethod]
        public Pricing_Report Pricing_unreceive_items(string COSTCENTER)
        {
            log.Info("Pricing_unreceive_items");
            var list = new Pricing_Report();
            list._unreceive_items = new List<unreceive_items>();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "ASYS_GetDeptUnreceived_Items";
                com.Parameters.AddWithValue("@COSTCENTER", COSTCENTER);

                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var stored = new unreceive_items();
                        stored.COSTCENTER = string.IsNullOrEmpty(dr["COSTCENTER"].ToString()) ? "NULL" : dr["COSTCENTER"].ToString().Trim();
                        stored.LOTNO = string.IsNullOrEmpty(dr["LOTNO"].ToString()) ? "NULL" : dr["LOTNO"].ToString().Trim();
                        stored.REFALLBARCODE = string.IsNullOrEmpty(dr["REFALLBARCODE"].ToString()) ? "NULL" : dr["REFALLBARCODE"].ToString().Trim();
                        stored.QTY = string.IsNullOrEmpty(dr["QTY"].ToString()) ? 0 : Convert.ToInt32(dr["QTY"]);
                        stored.SERIALNO = string.IsNullOrEmpty(dr["SERIALNO"].ToString()) ? "NULL" : dr["SERIALNO"].ToString().Trim();
                        stored.DESCRIPTION = string.IsNullOrEmpty(dr["DESCRIPTION"].ToString()) ? "NULL" : dr["DESCRIPTION"].ToString().Trim();
                        stored.KARAT = string.IsNullOrEmpty(dr["KARAT"].ToString()) ? "NULL" : dr["KARAT"].ToString().Trim();
                        stored.WEIGHT = string.IsNullOrEmpty(dr["WEIGHT"].ToString()) ? 0 : Convert.ToDouble(dr["WEIGHT"]);
                        stored.CARAT = string.IsNullOrEmpty(dr["CARAT"].ToString()) ? 0 : Convert.ToDouble(dr["CARAT"]);
                        stored.ALL_PRICE = string.IsNullOrEmpty(dr["ALL_PRICE"].ToString()) ? 0 : Convert.ToDouble(dr["ALL_PRICE"]);

                        fetch = "Data Found";
                        Item = "1";
                        list._unreceive_items.Add(stored);
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "No Data Found";
                    Item = "2";
                    dr.Close();
                }
                conn.Close();
                return new Pricing_Report
                {
                    Respons = fetch,
                    Result = Item,
                    _unreceive_items = list._unreceive_items
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_unreceive_items:" + Ex.Message);
                return new Pricing_Report { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Releasing_Report Releasing_print(string hlot)
        {
            log.Info("Releasing_print");
            var list = new Releasing_Report();
            list._releasing = new List<_releasing>();
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {

                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "ASYS_PRICINGSummaryGoodStock_Rel_rpt";
                com.Parameters.AddWithValue("@hlot", hlot);

                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var stored = new _releasing();
                        stored.lotno = string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim();
                        stored.receiver = string.IsNullOrEmpty(dr["receiver"].ToString()) ? "NULL" : dr["receiver"].ToString().Trim();
                        stored.employee = string.IsNullOrEmpty(dr["employee"].ToString()) ? "NULL" : dr["employee"].ToString().Trim();
                        stored.receivedate = string.IsNullOrEmpty(dr["receivedate"].ToString()) ? "NULL" : Convert.ToDateTime(dr["receivedate"]).ToString("MMMM dd, yyyy").Trim();
                        stored.refallbarcode = string.IsNullOrEmpty(dr["refallbarcode"].ToString()) ? "NULL" : dr["refallbarcode"].ToString().Trim();
                        stored.allbarcode = string.IsNullOrEmpty(dr["allbarcode"].ToString()) ? "NULL" : dr["allbarcode"].ToString().Trim();
                        stored.ptn = string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim();
                        stored.itemid = string.IsNullOrEmpty(dr["itemid"].ToString()) ? 0 : Convert.ToInt32(dr["itemid"]);
                        stored.ptnbarcode = string.IsNullOrEmpty(dr["ptnbarcode"].ToString()) ? "NULL" : dr["ptnbarcode"].ToString().Trim();
                        stored.branchcode = string.IsNullOrEmpty(dr["branchcode"].ToString()) ? "NULL" : dr["branchcode"].ToString().Trim();
                        stored.branchname = string.IsNullOrEmpty(dr["branchname"].ToString()) ? "NULL" : dr["branchname"].ToString().Trim();
                        stored.loanvalue = string.IsNullOrEmpty(dr["loanvalue"].ToString()) ? 0 : Convert.ToDouble(dr["loanvalue"]);
                        stored.refitemcode = string.IsNullOrEmpty(dr["refitemcode"].ToString()) ? "NULL" : dr["refitemcode"].ToString().Trim();
                        stored.branchitemdesc = string.IsNullOrEmpty(dr["branchitemdesc"].ToString()) ? "NULL" : dr["branchitemdesc"].ToString().Trim();
                        stored.refqty = string.IsNullOrEmpty(dr["refqty"].ToString()) ? 0 : Convert.ToInt32(dr["refqty"]);
                        stored.quantity = string.IsNullOrEmpty(dr["quantity"].ToString()) ? 0 : Convert.ToInt32(dr["quantity"]);
                        stored.karatgrading = string.IsNullOrEmpty(dr["karatgrading"].ToString()) ? "NULL" : dr["karatgrading"].ToString().Trim();
                        stored.caratsize = string.IsNullOrEmpty(dr["caratsize"].ToString()) ? "NULL" : dr["caratsize"].ToString().Trim();
                        stored.actionclass = string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim();
                        stored.sortcode = string.IsNullOrEmpty(dr["sortcode"].ToString()) ? "NULL" : dr["sortcode"].ToString().Trim();
                        stored.appraisevalue = string.IsNullOrEmpty(dr["appraisevalue"].ToString()) ? "NULL" : dr["appraisevalue"].ToString().Trim();
                        stored.currency = string.IsNullOrEmpty(dr["currency"].ToString()) ? "NULL" : dr["currency"].ToString().Trim();
                        stored.photoname = string.IsNullOrEmpty(dr["photoname"].ToString()) ? "NULL" : dr["photoname"].ToString().Trim();
                        stored.pricedesc = string.IsNullOrEmpty(dr["pricedesc"].ToString()) ? "NULL" : dr["pricedesc"].ToString().Trim();
                        stored.pricekarat = string.IsNullOrEmpty(dr["pricekarat"].ToString()) ? "NULL" : dr["pricekarat"].ToString().Trim();
                        stored.priceweight = string.IsNullOrEmpty(dr["priceweight"].ToString()) ? 0 : Convert.ToDouble(dr["priceweight"]);
                        stored.pricecarat = string.IsNullOrEmpty(dr["pricecarat"].ToString()) ? 0 : Convert.ToDouble(dr["pricecarat"]);
                        stored.SerialNo = string.IsNullOrEmpty(dr["SerialNo"].ToString()) ? "NULL" : dr["SerialNo"].ToString().Trim();
                        stored.allprice = string.IsNullOrEmpty(dr["allprice"].ToString()) ? 0 : Convert.ToDouble(dr["allprice"]);
                        stored.cellcost = string.IsNullOrEmpty(dr["cellcost"].ToString()) ? 0 : Convert.ToDouble(dr["cellcost"]);
                        stored.watchcost = string.IsNullOrEmpty(dr["watchcost"].ToString()) ? 0 : Convert.ToDouble(dr["watchcost"]);
                        stored.repaircost = string.IsNullOrEmpty(dr["repaircost"].ToString()) ? 0 : Convert.ToDouble(dr["repaircost"]);
                        stored.cleaningcost = string.IsNullOrEmpty(dr["cleaningcost"].ToString()) ? 0 : Convert.ToDouble(dr["cleaningcost"]);
                        stored.goldcost = string.IsNullOrEmpty(dr["goldcost"].ToString()) ? 0 : Convert.ToDouble(dr["goldcost"]);
                        stored.mountcost = string.IsNullOrEmpty(dr["mountcost"].ToString()) ? 0 : Convert.ToDouble(dr["mountcost"]);
                        stored.releasedate = string.IsNullOrEmpty(dr["releasedate"].ToString()) ? "NULL" : Convert.ToDateTime(dr["releasedate"]).ToString("MMMM dd, yyyy").Trim();
                        stored.releaser = string.IsNullOrEmpty(dr["releaser"].ToString()) ? "NULL" : dr["releaser"].ToString().Trim();
                        stored.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();
                        stored.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim();
                        fetch = "Data Found";
                        Item = "1";
                        list._releasing.Add(stored);
                    }
                    dr.Close();
                }
                else
                {
                    fetch = "No Data Found";
                    Item = "2";
                    dr.Close();

                }
                conn.Close();
                return new Releasing_Report
                {
                    Respons = fetch,
                    Result = Item,
                    _releasing = list._releasing
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_print:" + Ex.Message);
                return new Releasing_Report { Respons = Ex.Message, Result = "0" };
            }

        }
        [WebMethod]
        public Receiving Receiving_Costcenter()
        {
            log.Info("Receiving_Costcenter");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var CostDept = new Receiving();
            SqlConnection conn = new SqlConnection(connection);
            CostDept._costcenter = new costcenters();
            try
            {
                var costcenter = new costcenters();
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select * from tbl_CostCenter";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        CostDept._costcenter.CostDept = string.IsNullOrEmpty(dr["CostDept"].ToString()) ? "NULL" : dr["CostDept"].ToString().Trim();
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _costcenter = CostDept._costcenter,
                    Respons = fetch,
                    Result = Item,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_Costcenter:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }
        }
        [WebMethod]
        public Receiving Receiving_generateLOTNO()
        {
            log.Info("Receiving_generateLOTNO");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Receiving();
            Reflotno._generate = new genetareLOTNO2();
            Reflotno._generate.reflotno = new List<string>();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select distinct reflotno from ASYS_MLWB_Detail where status = 'RELEASED'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._generate.reflotno.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _generate = Reflotno._generate,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_generateLOTNO:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_DetailList(string refallBarcode)
        {
            log.Info("Receiving_DetailList");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Receiving();
            SqlConnection conn = new SqlConnection(connection);
            Reflotno._DetailList = new DetailList();
            Reflotno._DetailList.reflotno = new List<string>();
            Reflotno._DetailList.division = new List<string>();
            Reflotno._DetailList.ptn = new List<string>();
            Reflotno._DetailList.itemcode = new List<string>();
            Reflotno._DetailList.ptnitemdesc = new List<string>();
            Reflotno._DetailList.quantity = new List<string>();
            Reflotno._DetailList.barcode = new List<string>();
            Reflotno._DetailList.all_desc = new List<string>();
            Reflotno._DetailList.actionclass = new List<string>();
            Reflotno._DetailList.status = new List<string>();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select distinct asys_MLWB_detail.refLOTNO AS reflotno, asys_MLWB_detail.branchname as division, asys_MLWB_detail.ptn as ptn, asys_MLWB_detail.itemcode as itemcode,asys_MLWB_detail.branchitemdesc as ptnitemdesc, asys_MLWB_detail.qty as quantity, asys_MLWB_detail.refallbarcode as barcode,all_desc, asys_MLWB_detail.actionclass as actionclass, asys_MLWB_detail.status as status from asys_MLWB_detail  where asys_MLWB_detail.refallBarcode = '" + refallBarcode + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._DetailList.reflotno.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());
                        Reflotno._DetailList.division.Add(string.IsNullOrEmpty(dr["division"].ToString()) ? "NULL" : dr["division"].ToString().Trim());
                        Reflotno._DetailList.ptn.Add(string.IsNullOrEmpty(dr["ptn"].ToString()) ? "NULL" : dr["ptn"].ToString().Trim());
                        Reflotno._DetailList.itemcode.Add(string.IsNullOrEmpty(dr["itemcode"].ToString()) ? "NULL" : dr["itemcode"].ToString().Trim());
                        Reflotno._DetailList.ptnitemdesc.Add(string.IsNullOrEmpty(dr["ptnitemdesc"].ToString()) ? "NULL" : dr["ptnitemdesc"].ToString().Trim());
                        Reflotno._DetailList.quantity.Add(string.IsNullOrEmpty(dr["quantity"].ToString()) ? "NULL" : dr["quantity"].ToString().Trim());
                        Reflotno._DetailList.barcode.Add(string.IsNullOrEmpty(dr["barcode"].ToString()) ? "NULL" : dr["barcode"].ToString().Trim());
                        Reflotno._DetailList.all_desc.Add(string.IsNullOrEmpty(dr["all_desc"].ToString()) ? "NULL" : dr["all_desc"].ToString().Trim());
                        Reflotno._DetailList.actionclass.Add(string.IsNullOrEmpty(dr["actionclass"].ToString()) ? "NULL" : dr["actionclass"].ToString().Trim());
                        Reflotno._DetailList.status.Add(string.IsNullOrEmpty(dr["status"].ToString()) ? "NULL" : dr["status"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _DetailList = Reflotno._DetailList,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_DetailList:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_frmPRICINGReceiving_rpt_Load(string searchflag, string lotno)
        {
            log.Info("Receiving_frmPRICINGReceiving_rpt_Load");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Receiving();
            SqlConnection conn = new SqlConnection(connection);
            Reflotno._frmPRICINGReceiving_rpt_Load = new frmPRICINGReceiving_rpt_Load();

            if (searchflag == "1")
            {
                Item = "select top 1 lotno, receivedate from ASYS_PRICING_detail where status in ('received','priced') and lotno = '" + lotno + "'";
            }
            else
            {
                Item = "select top 1 lotno, receivedate from ASYS_PRICING_detail where lotno = '" + lotno + "'";
            }
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = Item;
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._frmPRICINGReceiving_rpt_Load.lotno = string.IsNullOrEmpty(dr["lotno"].ToString()) ? "NULL" : dr["lotno"].ToString().Trim();
                        Reflotno._frmPRICINGReceiving_rpt_Load.receivedate = string.IsNullOrEmpty(dr["receivedate"].ToString()) ? "NULL" : dr["receivedate"].ToString().Trim();

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _frmPRICINGReceiving_rpt_Load = Reflotno._frmPRICINGReceiving_rpt_Load,
                    Respons = fetch,
                    Result = Item,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_frmPRICINGReceiving_rpt_Load:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_habwa_date()
        {
            log.Info("Receiving_habwa_date");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Receiving();
            SqlConnection conn = new SqlConnection(connection);
            Reflotno._habwa_date = new habwa_date2();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._habwa_date.month = string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim();
                        Reflotno._habwa_date.year = string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim();
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _habwa_date = Reflotno._habwa_date,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_habwa_date:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Pricing_txtlot_KeyPress(string lotno)
        {
            log.Info("Pricing_txtlot_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Receiving();
            SqlConnection conn = new SqlConnection(connection);
            Reflotno._txtlot_KeyPress1 = new txtlot_KeyPress();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "-";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._txtlot_KeyPress1.receivedate = string.IsNullOrEmpty(dr["receivedate"].ToString()) ? "NULL" : dr["receivedate"].ToString().Trim();

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _txtlot_KeyPress1 = Reflotno._txtlot_KeyPress1,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_txtlot_KeyPress:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving__Function_receiver()
        {
            log.Info("Receiving__Function_receiver");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Receiving();
            Reflotno._Function_receiver = new Function_receiver();
            Reflotno._Function_receiver.fullname2 = new List<string>();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select fullname as fullname2, * from rems.dbo.vw_humresvismin where job_title like 'distri%'order by fullname asc";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._Function_receiver.fullname2.Add(string.IsNullOrEmpty(dr["fullname2"].ToString()) ? "NULL" : dr["fullname2"].ToString().Trim());

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                return new Receiving
                {
                    _Function_receiver = Reflotno._Function_receiver,
                    Respons = fetch,
                    Result = Item,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving__Function_receiver:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_cmbEmployee_SelectedIndexChanged(string humres, string fullname)
        {
            log.Info("Receiving_cmbEmployee_SelectedIndexChanged");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Receiving();
            Reflotno._cmbEmployee_SelectedIndexChanged = new cmbEmployee_SelectedIndexChanged();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select res_id from " + humres + " where fullname='" + fullname + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._cmbEmployee_SelectedIndexChanged.res_id = string.IsNullOrEmpty(dr["res_id"].ToString()) ? "NULL" : dr["res_id"].ToString().Trim();

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _cmbEmployee_SelectedIndexChanged = Reflotno._cmbEmployee_SelectedIndexChanged,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_cmbEmployee_SelectedIndexChanged:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_cboreceive_SelectedIndexChanged(string humres2, string fullname)
        {
            log.Info("Receiving_cboreceive_SelectedIndexChanged");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Receiving();
            Reflotno._cboreceive_SelectedIndexChanged = new cboreceive_SelectedIndexChanged();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select rtrim(fullname) as fullname2 from " + humres2 + " where fullname='" + fullname + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._cboreceive_SelectedIndexChanged.fullname2 = string.IsNullOrEmpty(dr["fullname2"].ToString()) ? "NULL" : dr["fullname2"].ToString().Trim();

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _cboreceive_SelectedIndexChanged = Reflotno._cboreceive_SelectedIndexChanged,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_cboreceive_SelectedIndexChanged:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Receiving Receiving_cboreceive_KeyPress()
        {
            log.Info("Receiving_cboreceive_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Receiving();
            Reflotno._cboreceive_KeyPress = new cboreceive_KeyPress();
            Reflotno._cboreceive_KeyPress.reflotno = new List<string>();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select distinct reflotno from asys_mlwb_detail where status = 'RELEASED'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._cboreceive_KeyPress.reflotno.Add(string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Receiving
                {
                    _cboreceive_KeyPress = Reflotno._cboreceive_KeyPress,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Receiving_cboreceive_KeyPress:" + Ex.Message);
                return new Receiving { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing_CallCostCenter()
        {
            log.Info("Releasing_CallCostCenter");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Releasing();
            Reflotno._CallCostCenter = new CallCostCenter();
            Reflotno._CallCostCenter.CostDept = new List<string>();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select * from vw_CostCenter";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reflotno._CallCostCenter.CostDept.Add(string.IsNullOrEmpty(dr["CostDept"].ToString()) ? "NULL" : dr["CostDept"].ToString().Trim());

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Releasing
                {
                    _CallCostCenter = Reflotno._CallCostCenter,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_CallCostCenter:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing_frmDISTRIReleasing_rpt_Load(string reflotno)
        {
            log.Info("Releasing_frmDISTRIReleasing_rpt_Load");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Releasing();
            Reflotno._frmDISTRIReleasing_rpt_Load = new frmDISTRIReleasing_rpt_Load();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select releasedate from ASYS_DISTRI_detail where status = 'released' and reflotno = '" + reflotno + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._frmDISTRIReleasing_rpt_Load.releasedate = (string.IsNullOrEmpty(dr["releasedate"].ToString()) ? "NULL" : dr["releasedate"].ToString().Trim());

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Releasing
                {
                    _frmDISTRIReleasing_rpt_Load = Reflotno._frmDISTRIReleasing_rpt_Load,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_frmDISTRIReleasing_rpt_Load:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing_txtlot_KeyPress(string txtlot)
        {
            log.Info("Releasing_txtlot_KeyPress");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Releasing();
            SqlConnection conn = new SqlConnection(connection);
            Reflotno._txtlot_KeyPress1111 = new txtlot_KeyPress1111();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select month(releasedate)as month, day(releasedate) as day, year(releasedate) as year from asys_DISTRI_header inner join asys_distri_detail on asys_distri_header.lotno = asys_distri_detail.lotno where asys_distri_detail.reflotno = '" + txtlot + "'";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._txtlot_KeyPress1111.month = (string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim());
                        Reflotno._txtlot_KeyPress1111.day = (string.IsNullOrEmpty(dr["day"].ToString()) ? "NULL" : dr["day"].ToString().Trim());
                        Reflotno._txtlot_KeyPress1111.year = (string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim());

                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Releasing
                {
                    _txtlot_KeyPress1111 = Reflotno._txtlot_KeyPress1111,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_txtlot_KeyPress:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing_frmPRICINGReleasing_rpt_Load()
        {
            log.Info("Releasing_frmPRICINGReleasing_rpt_Load");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            var Reflotno = new Releasing();
            Reflotno._frmPRICINGReleasing_rpt_Load = new frmPRICINGReleasing_rpt_Load();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                //Releasing_frmPRICINGReleasing_rpt_Load
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 6000000;
                //com.CommandText = "select top 1 reflotno as lotno,releasedate from ASYS_PRICING_detail where status in ('released','recdistri') order by releasedate desc";
                com.CommandText = "SELECT TOP 1 reflotno as lotno, releasedate FROM ASYS_PRICING_detail WHERE status = 'released' ORDER BY releasedate DESC";
                com.ExecuteNonQuery();
                //SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._frmPRICINGReleasing_rpt_Load.lotno = (string.IsNullOrEmpty(dr["lotno"].ToString()) ? "NULL" : dr["lotno"].ToString().Trim());
                        Reflotno._frmPRICINGReleasing_rpt_Load.releasedate = (string.IsNullOrEmpty(dr["releasedate"].ToString()) ? "NULL" : dr["releasedate"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }
                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }

                conn.Close();
                return new Releasing
                {
                    _frmPRICINGReleasing_rpt_Load = Reflotno._frmPRICINGReleasing_rpt_Load,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_frmPRICINGReleasing_rpt_Load:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing__habwa_date3()
        {
            log.Info("Releasing__habwa_date3");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Releasing();
            Reflotno._habwa_date3 = new habwa_date3();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select cast(month(getdate()) as int) as month, cast(year(getdate()) as int) as year";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._habwa_date3.month = (string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim());
                        Reflotno._habwa_date3.year = (string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Releasing
                {
                    _habwa_date3 = Reflotno._habwa_date3,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing__habwa_date3:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Releasing Releasing_txtlot_KeyPress3(string txtlot)
        {
            log.Info("Releasing_txtlot_KeyPress3");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Reflotno = new Releasing(); Reflotno._txtlot_KeyPress3 = new txtlot_KeyPress3();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "Select month(releasedate)as month, day(releasedate) as day, year(releasedate) as year from asys_PRICING_header inner join asys_pricing_detail on asys_pricing_header.lotno = asys_pricing_detail.lotno where asys_pricing_detail.reflotno = '" + txtlot + "' and asys_pricing_detail.status in ('RELEASED','RECDISTRI')";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        Reflotno._txtlot_KeyPress3.month = (string.IsNullOrEmpty(dr["month"].ToString()) ? "NULL" : dr["month"].ToString().Trim());
                        Reflotno._txtlot_KeyPress3.day = (string.IsNullOrEmpty(dr["day"].ToString()) ? "NULL" : dr["day"].ToString().Trim());
                        Reflotno._txtlot_KeyPress3.year = (string.IsNullOrEmpty(dr["year"].ToString()) ? "NULL" : dr["year"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }

                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Releasing
                {
                    _txtlot_KeyPress3 = Reflotno._txtlot_KeyPress3,
                    Respons = fetch,
                    Result = Item,

                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Releasing_txtlot_KeyPress3:" + Ex.Message);
                return new Releasing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public Pricing Pricing_frmRec_Load()
        {
            log.Info("Pricing_frmRec_Load");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var List = new Pricing();
            List._frmRec_Loadp = new frmRec_Loadp();
            List._frmRec_Loadp.GOLD_KARAT = new List<string>();
            List._frmRec_Loadp.Plain = new List<string>();
            var Reflotno = new Releasing();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "SELECT GOLD_KARAT, Plain FROM ASYS_GOLDKARAT ORDER BY [ID]";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        List._frmRec_Loadp.GOLD_KARAT.Add(string.IsNullOrEmpty(dr["GOLD_KARAT"].ToString()) ? "NULL" : dr["GOLD_KARAT"].ToString().Trim());
                        List._frmRec_Loadp.Plain.Add(string.IsNullOrEmpty(dr["Plain"].ToString()) ? "NULL" : dr["Plain"].ToString().Trim());
                    }
                    Item = "1";
                    fetch = "DATA FOUND";
                    dr.Close();
                }
                else
                {
                    Item = "2";
                    fetch = "NO DATA FOUND";
                }
                conn.Close();
                return new Pricing
                {
                    _frmRec_Loadp = List._frmRec_Loadp,
                    Respons = fetch,
                    Result = Item,
                };
            }
            catch (Exception Ex)
            {
                conn.Close();
                log.Error("Pricing_frmRec_Load:" + Ex.Message);
                return new Pricing { Respons = Ex.Message, Result = "0", };
            }

        }
        [WebMethod]
        public frmPRICINGReleasing_Load Pricing_frmPRICINGReleasing_Load()
        {
            log.Info("Pricing_frmPRICINGReleasing_Load");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            var Data = new frmPRICINGReleasing_Load();
            Data._formax_lotno = new formax_lotno();
            try
            {
                conn.Open();
                SqlCommand com = conn.CreateCommand();
                com.CommandTimeout = 400000;
                com.CommandText = "select max (reflotno) as reflotno from asys_pricing_detail";
                com.ExecuteNonQuery();
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Data._formax_lotno.reflotno = (string.IsNullOrEmpty(dr["reflotno"].ToString()) ? "NULL" : dr["reflotno"].ToString().Trim());

                    }
                    Data.fetch = "DATA FOUND";
                    Data.result = "1";
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    Data.fetch = "NO DATA FOUND";
                    Data.result = "0";
                }
                conn.Close();
                return new frmPRICINGReleasing_Load { _formax_lotno = Data._formax_lotno, result = Data.result, fetch = Data.fetch };
            }
            catch (Exception ex)
            {
                conn.Close();
                return new frmPRICINGReleasing_Load { result = "0", fetch = ex.Message };
            }

        }
        #endregion

        [WebMethod]
        public Releasing Update_Status(int templot)
        {
            log.Info("Update_Status");
            x = "REMS";
            string connection = ConfigurationManager.ConnectionStrings[x].ConnectionString;
            SqlTransaction tranCon;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            tranCon = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE asys_lotno_gen SET lotno ='" + templot + "' WHERE BusinessCenter ='PRICING'", conn, tranCon))
                {
                    cmd.ExecuteNonQuery();
                }

                tranCon.Commit();
                conn.Close();
                return new Releasing { Respons = value, Result = fetch };


            }
            catch (Exception Ex)
            {
                tranCon.Rollback();
                conn.Close();
                log.Error("Update_Status:" + Ex.Message);
                return new Releasing { Respons = "Transaction Failed", Result = "0" };
            }


        }
        public bool flag = false;
        string constring = ConfigurationManager.ConnectionStrings["rems"].ConnectionString;
        [WebMethod]
        //DERE!
        public MLWB saveData(string userLog, string[][] DgEntry, string lotnum)//Receiving 
        {
            log.Info("Logging starts here: saveData");
            SqlTransaction tran;

            checker(lotnum);

            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                tran = con.BeginTransaction();
                try
                {


                    if (flag == true)
                    {

                    }
                    else
                    {
                        using (SqlCommand cmd1 = new SqlCommand("Insert into asys_MLWB_header (lotno) Values ('" + lotnum + "')", con, tran))
                        {

                            cmd1.ExecuteNonQuery();
                            flag = false;
                            log.Error(cmd1);

                        }
                    }


                    for (int i = 0; i <= DgEntry.Count() - 1; i++)
                    {

                        if (DgEntry[i][0] != null)
                        {

                            using (SqlCommand cmd = new SqlCommand("INSERT INTO [REMS].dbo.ASYS_BarcodeHistory (lotno,refallbarcode, allbarcode,itemcode," +
                               "[description],karat,carat,SerialNo,weight,currency,price,cost,custodian,trandate,status,costcenter," +
                               "empname) SELECT lotno,refallbarcode, allbarcode,itemcode,[description],karat,carat,SerialNo,weight," +
                               "currency,price,cost,custodian,trandate,status,costcenter,empname from (SELECT TOP 1 RefLotno as lotno," +
                               " RefALLBarcode, allbarcode, RefItemcode as itemcode, ALL_Desc as [description], ALL_Karat as karat," +
                               " ALL_Carat as carat, SerialNo, ALL_Weight as weight, Currency , ALL_price as price, ALL_Cost as cost," +
                               " SorterName as custodian,getdate() as trandate,'RECEIVED' as status,'MLWB' as costcenter,'" + userLog +
                               "' as empname FROM dbo.ASYS_REM_Detail WHERE refallbarcode = '" + DgEntry[i][0] + "' AND " +
                               "status = 'RELEASED' UNION ALL SELECT TOP 1 RefLotno as lotno, RefALLBarcode, allbarcode, RefItemcode as itemcode, " +
                               "ALL_Desc as [description], ALL_Karat as karat, ALL_Carat as carat, SerialNo, ALL_Weight as weight, " +
                               "Currency , ALL_price as price, ALL_Cost as cost ,'" + userLog + "' as custodian,getdate() as " +
                               "trandate,'RECEIVED' as status,'MLWB' as costcenter,'" + userLog + "' as empname FROM dbo.ASYS_REMOutsource_Detail WHERE" +
                               " refallbarcode = '" + DgEntry[i][0] + "' AND status = 'RELEASED' )a", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                                log.Error(i.ToString() + cmd);
                            }
                            string query = "INSERT INTO ASYS_MLWB_Detail (reflotno,lotno,refallbarcode, " +
                                "allbarcode,ptn,itemid,ptnbarcode,branchcode,branchname,loanvalue,refitemcode,itemcode,branchitemdesc, " +
                                "refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,all_desc,all_karat,all_carat,SerialNo, " +
                                "all_cost,all_weight,appraisevalue,currency,photoname,price_desc,price_karat,price_weight,price_carat, " +
                                "all_price,cellular_cost,watch_cost,cleaning_cost,gold_cost,mount_cost,yg_cost,wg_cost,costdate,costname, " +
                                "maturitydate,expirydate,loandate,receivedate,receiver,custodian,status)SELECT reflotno,lotno,refallbarcode, " +
                                "allbarcode,ptn,itemid,ptnbarcode,branchcode,branchname,loanvalue,refitemcode,itemcode,branchitemdesc, " +
                                "refqty,qty,karatgrading,caratsize,weight,actionclass,sortcode,all_desc,all_karat,all_carat,SerialNo, " +
                                "all_cost,all_weight,appraisevalue,currency,photoname,price_desc,price_karat,price_weight,price_carat, " +
                                "all_price,cellular_cost,watch_cost,cleaning_cost,gold_cost,mount_cost,yg_cost,wg_cost,costdate,costname, " +
                                "maturitydate,expirydate,loandate,receivedate,receiver,custodian,'RECEIVED' as status " +
                                "FROM ( SELECT TOP 1 dbo.ASYS_REM_Detail.ID, dbo.ASYS_REM_Detail.RefLotno, " +
                                "dbo.ASYS_REM_Detail.RefLotno as lotno,dbo.ASYS_REM_Detail.RefALLBarcode,dbo.ASYS_REM_Detail.RefALLBarcode as allbarcode, " +
                                " dbo.ASYS_REM_Detail.PTN, dbo.ASYS_REM_Detail.itemid, dbo.ASYS_REM_Header.PTNBarcode, " +
                                "dbo.ASYS_REM_Header.BranchCode, dbo.ASYS_REM_Header.BranchName, dbo.ASYS_REM_header.loanvalue, " +
                                "dbo.ASYS_REM_Detail.RefItemcode, dbo.ASYS_REM_Detail.RefItemcode as itemcode,dbo.ASYS_REM_Detail.BranchItemDesc," +
                                " dbo.ASYS_REM_Detail.RefQty,dbo.ASYS_REM_Detail.RefQty as qty, dbo.ASYS_REM_Detail.KaratGrading, " +
                                "dbo.ASYS_REM_Detail.CaratSize, dbo.ASYS_REM_Detail.Weight, dbo.ASYS_REM_Detail.ActionClass, dbo.ASYS_REM_Detail.SortCode, " +
                                " dbo.ASYS_REM_Detail.ALL_Desc,  dbo.ASYS_REM_Detail.ALL_Karat, dbo.ASYS_REM_Detail.ALL_Carat, " +
                                "dbo.ASYS_REM_Detail.SerialNo, dbo.ASYS_REM_Detail.ALL_Cost, dbo.ASYS_REM_Detail.ALL_Weight, " +
                                "dbo.ASYS_REM_detail.AppraiseValue, dbo.ASYS_REM_Detail.Currency, dbo.ASYS_REM_Detail.PhotoName, " +
                                "dbo.ASYS_REM_Detail.Price_Desc, dbo.ASYS_REM_Detail.Price_karat, dbo.ASYS_REM_Detail.Price_weight, " +
                                "dbo.ASYS_REM_Detail.Price_carat, dbo.ASYS_REM_Detail.all_price, dbo.ASYS_REM_Detail.Cellular_cost, " +
                                "dbo.ASYS_REM_Detail.Watch_cost, dbo.ASYS_REM_Detail.Repair_cost, dbo.ASYS_REM_Detail.Cleaning_cost, " +
                                "dbo.ASYS_REM_Detail.Gold_cost, dbo.ASYS_REM_Detail.Mount_cost, dbo.ASYS_REM_Detail.YG_cost, " +
                                "dbo.ASYS_REM_Detail.WG_cost, dbo.ASYS_REM_Detail.CostDate, dbo.ASYS_REM_Detail.CostName, " +
                                "dbo.ASYS_REM_Header.MaturityDate, dbo.ASYS_REM_Header.ExpiryDate,  dbo.ASYS_REM_Header.LoanDate,getdate() as receivedate," +
                                "'" + userLog + "' as receiver,'" + userLog + "' as custodian " +
                                "FROM dbo.ASYS_REM_Detail  INNER JOIN dbo.ASYS_REM_Header  ON dbo.ASYS_REM_Detail.PTN = dbo.ASYS_REM_Header.PTN and " +
                                "dbo.ASYS_REM_Detail.Lotno = dbo.ASYS_REM_Header.Lotno WHERE refallbarcode = '" + DgEntry[i][0] + "' and " +
                                "reflotno = '" + lotnum + "' AND status = 'RELEASED' ORDER BY dbo.ASYS_REM_Detail.ID DESC " +
                                "union all SELECT TOP 1 '' AS ID, dbo.ASYS_REMOutsource_Detail.RefLotno, dbo.ASYS_REMOutsource_Detail.RefLotno as lotno, " +
                                "dbo.ASYS_REMOutsource_Detail.RefALLBarcode,dbo.ASYS_REMOutsource_Detail.RefALLBarcode as allbarcode, " +
                                "'0' as PTN, dbo.ASYS_REMOutsource_Detail.itemid,dbo.ASYS_REMOutsource_detail.PTNBarcode, '0' as branchcode, " +
                                " 'NONE' as branchname,dbo.ASYS_REMOutsource_Detail.loanvalue, dbo.ASYS_REMOutsource_Detail.RefItemcode, " +
                                "dbo.ASYS_REMOutsource_Detail.RefItemcode as itemcode, dbo.ASYS_REMOutsource_Detail.BranchItemDesc, " +
                                "dbo.ASYS_REMOutsource_Detail.RefQty,dbo.ASYS_REMOutsource_Detail.RefQty as qty, dbo.ASYS_REMOutsource_Detail.KaratGrading, " +
                                "dbo.ASYS_REMOutsource_Detail.CaratSize, dbo.ASYS_REMOutsource_Detail.Weight, dbo.ASYS_REMOutsource_Detail.ActionClass, " +
                                " dbo.ASYS_REMOutsource_Detail.SortCode, dbo.ASYS_REMOutsource_Detail.ALL_Desc, dbo.ASYS_REMOutsource_Detail.ALL_Karat, " +
                                " dbo.ASYS_REMOutsource_Detail.ALL_Carat, dbo.ASYS_REMOutsource_Detail.SerialNo, dbo.ASYS_REMOutsource_Detail.ALL_Cost, " +
                                "dbo.ASYS_REMOutsource_Detail.ALL_Weight, dbo.ASYS_REMOutsource_detail.AppraiseValue, dbo.ASYS_REMOutsource_Detail.Currency, " +
                                "dbo.ASYS_REMOutsource_Detail.PhotoName, dbo.ASYS_REMOutsource_Detail.Price_Desc, dbo.ASYS_REMOutsource_Detail.Price_karat, " +
                                "dbo.ASYS_REMOutsource_Detail.Price_weight, dbo.ASYS_REMOutsource_Detail.Price_carat, dbo.ASYS_REMOutsource_Detail.all_price," +
                                "dbo.ASYS_REMOutsource_Detail.Cellular_cost, dbo.ASYS_REMOutsource_Detail.Watch_cost, dbo.ASYS_REMOutsource_Detail.Repair_cost," +
                                " dbo.ASYS_REMOutsource_Detail.Cleaning_cost, dbo.ASYS_REMOutsource_Detail.Gold_cost, dbo.ASYS_REMOutsource_Detail.Mount_cost, " +
                                " dbo.ASYS_REMOutsource_Detail.YG_cost, dbo.ASYS_REMOutsource_Detail.WG_cost, dbo.ASYS_REMOutsource_Detail.CostDate, " +
                                "dbo.ASYS_REMOutsource_Detail.CostName, dbo.ASYS_REMOutsource_detail.MaturityDate, dbo.ASYS_REMOutsource_detail.ExpiryDate,  " +
                                "dbo.ASYS_REMOutsource_detail.LoanDate, getdate() as receivedate,'" + userLog + "' as receiver," +
                                "'" + userLog + "' as custodian FROM dbo.ASYS_REMOutsource_Detail  WHERE " +
                                "refallbarcode = '" + DgEntry[i][0] + "' and reflotno = '" + lotnum + "' AND status = 'RELEASED')a";
                            using (SqlCommand cmd = new SqlCommand(query, con, tran))
                            {

                                log.Error(i.ToString() + query);
                                cmd.CommandTimeout = 1000000;
                                cmd.ExecuteNonQuery();

                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE ASYS_REMoutsource_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and  status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE ASYS_REM_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and  status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSLUZON.dbo.asys_remoutsource_detail SET status = 'RECMLWB' where refallbarcode = '" + DgEntry[i][0] + "' and  status = 'RELEASED' ", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSLUZON.dbo.asys_REM_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "'  and  status ='RELEASED' ", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSVISAYAS.dbo.asys_remoutsource_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSVISAYAS.dbo.asys_REM_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and  status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSMINDANAO.dbo.asys_remoutsource_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSMINDANAO.dbo.asys_REM_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and  status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSSHOWROOM.dbo.asys_remoutsource_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE REMSSHOWROOM.dbo.asys_REM_detail SET status = 'RECMLWB' where  refallbarcode = '" + DgEntry[i][0] + "' and  status ='RELEASED'", con, tran))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    //tran.Commit();
                    tran.Rollback();
                    con.Close();
                    return new MLWB { respCode = "1", respMsg = "Success" };
                }
                catch (Exception ex)
                {
                    log.Error("saveData: " + ex.Message);
                    tran.Rollback();
                    return new MLWB { respCode = "0", respMsg = "service error: " + ex.Message };
                }
            }
        }

        public void checker(string lotnum)
        {


            try
            {

                using (SqlConnection con = new SqlConnection(constring))
                {

                    con.Open();
                    SqlDataReader read;
                    using (SqlCommand cmd = new SqlCommand("select lotno from asys_MLWB_header where lotno='" + lotnum + "'", con))
                    {
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (read.HasRows)
                        {
                            read.Close();
                            flag = true;
                        }

                        else
                        {
                            flag = false;
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
