using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ASYSPricingService
{
    #region
    public class frmPRICINGReleasing_Load
    {
        public formax_lotno _formax_lotno { get; set; }
        public string fetch { get; set; }
        public string result { get; set; }
    }
    public class PricingResult
    {
        public string respCode { get; set; }
        public string respMsg { get; set; }
        public pricingReceiving data { get; set; }
        public RetrieveGoldKaratById Gold { get; set; }
        public RetrieveDatabyAction_Type Action_type { get; set; }
        public RetrievePTN_Barcode Action { get; set; }


    }
    public class PricingResult2
    {
        public receiving data2 { get; set; }
        public string respond { get; set; }

    }
    public class PricingResult3
    {
        public Listofitems data3 { get; set; }
        public string result { get; set; }
        public string fetch { get; set; }
        public DataSet container2 { get; set; }


    }
    public class PricingResult5
    {
        public string LotNumber { get; set; }
        public string AllBarcode { get; set; }
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class pricingReceiving
    {
        public List<string> lotNumber { get; set; }
        public List<string> ptn { get; set; }
        public List<string> result { get; set; }
        public List<string> barcode { get; set; }
        public List<string> alldesc { get; set; }
        public List<string> weight { get; set; }
        public List<string> karat { get; set; }
        public List<string> carat { get; set; }
        public List<string> price { get; set; }
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
    public class Pricing
    {
        public string Respons { get; set; }
        public string Result { get; set; }
        public SaveMounted insert { get; set; }
        public habwa_date _habwa_date { get; set; }
        public frmRec_Loadp _frmRec_Loadp { get; set; }
    }
    public class receiving
    {
        public List<string> items { get; set; }
    }
    public class Listofitems
    {
        public List<string> itemslist { get; set; }
        public List<string> itemslist2 { get; set; }


    }
    public class InsertLotnumberPHeader
    {
        public string LotNumber { get; set; }
        public string respCode { get; set; }
    }
    public class ViewReport
    {
        public string Report { get; set; }
        public string Respons { get; set; }
    }
    public class RetrieveDatabyLotNumber
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string AllBarcode { get; set; }

    }
    public class RetrieveDataByStatus
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string GoldkaratPrice { get; set; }
        public string MountedPrice { get; set; }
        public string Gold_Karat { get; set; }
    }
    public class RetrieveCostedData
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string Reflotno { get; set; }
        public string RefallBarcode { get; set; }
        public string PTN { get; set; }
        public string ItemID { get; set; }
        public string PTNBarCode { get; set; }
        public string BranchName { get; set; }
        public string LoanValue { get; set; }
        public string RefItemcode { get; set; }
        public string Itemcode { get; set; }
        public string BranchItemDesc { get; set; }
        public string RefQty { get; set; }
        public string Qty { get; set; }
        public string KaratGrading { get; set; }
        public string CaratSize { get; set; }
        public string Weight { get; set; }
        public string Actionclass { get; set; }
        public string Sortcode { get; set; }
        public string ALL_desc { get; set; }
        public string SerialNo { get; set; }
        public string ALL_karat { get; set; }
        public string ALL_carat { get; set; }
        public string ALL_cost { get; set; }
        public string ALL_Weight { get; set; }
        public string currency { get; set; }
        public string PhotoName { get; set; }
        public string all_price { get; set; }
        public string price_DESC { get; set; }
        public string price_carat { get; set; }
        public string price_weight { get; set; }
        public string Cellular_cost { get; set; }
        public string Watch_cost { get; set; }
        public string Repair_cost { get; set; }
        public string Cleaning_cost { get; set; }
        public string Gold_cost { get; set; }
        public string Mount_cost { get; set; }
        public string YG_cost { get; set; }
        public string WG_cost { get; set; }
        public string MaturityDate { get; set; }
        public string ExpiryDate { get; set; }
        public string LoanDate { get; set; }
        public string Status { get; set; }
        public string GoldKaratPrice { get; set; }
        public string MountedPrice { get; set; }
        public string Gold_Karat { get; set; }
        public string BranchCode { get; set; }
        public string Price_karat { get; set; }
    }
    public class RetrieveGoldKaratById
    {
        public List<string> gold_karat { get; set; }
        public string Respons { set; get; }
        public string Result { get; set; }
    }
    public class RetrieveCostbyAction_Type
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string CostA { get; set; }
        public string CostB { get; set; }
        public string CostC { get; set; }
        public string CostD { get; set; }

    }
    public class RetrieveDatabyAction_Type
    {
        public List<string> ALL_kartlist { get; set; }
        public List<string> ALL_costlist { get; set; }
        public List<string> ALL_weightlist { get; set; }
        public List<string> Gold_CostList { get; set; }
        public List<string> Mount_Costlist { get; set; }
        public List<string> YG_Costlist { get; set; }
        public List<string> WG_Costllist { get; set; }
        public List<string> ALL_Costllist { get; set; }
        public List<string> Cellular_Costlist { get; set; }
        public List<string> Repair_Costlist { get; set; }
        public List<string> Cleanign_Costlist { get; set; }
        public List<string> Watch_Costlist { get; set; }
        public string Respons { set; get; }
        public string Result { get; set; }
        public string ID { get; set; }
        public string RefLotno { get; set; }
        public string Lotno { get; set; }
        public string RefallBarcode { get; set; }
        public string ALLbarcode { get; set; }
        public string PTN { get; set; }
        public string ItemID { get; set; }
        public string PTNBarcode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string LoanValue { get; set; }
        public string RefItemcode { get; set; }
        public string Itemcode { get; set; }
        public string BranchitemDesc { get; set; }
        public string SerialNo { get; set; }
        public string RefQty { get; set; }
        public string Qty { get; set; }
        public string KaratGrading { get; set; }
        public string CaratSize { get; set; }
        public string Weight { get; set; }
        public string ActionClass { get; set; }
        public string SortCode { get; set; }
        public string ALL_desc { get; set; }
        public string ALL_karat { get; set; }
        public string ALL_carat { get; set; }
        public string ALL_cost { get; set; }
        public string ALL_weight { get; set; }
        public string AppraiseValue { get; set; }
        public string Currency { get; set; }
        public string PhotoName { get; set; }
        public string Price_desc { get; set; }
        public string Price_karat { get; set; }
        public string Price_weight { get; set; }
        public string Price_carat { get; set; }
        public string ALL_price { get; set; }
        public string Cellular_cost { get; set; }
        public string Watch_cost { get; set; }
        public string Repair_cost { get; set; }
        public string Cleaning_cost { get; set; }
        public string Gold_cost { get; set; }
        public string Mount_cost { get; set; }
        public string YG_cost { get; set; }
        public string WG_cost { get; set; }
        public string CostDate { get; set; }
        public string CostName { get; set; }
        public string Receivedate { get; set; }
        public string Receiver { get; set; }
        public string ReleaseDate { get; set; }
        public string Releaser { get; set; }
        public string Custodian { get; set; }
        public string MaturityDate { get; set; }
        public string ExpiryDate { get; set; }
        public string LoanDate { get; set; }
        public string Status { get; set; }
        public string DetailStamp { get; set; }
        public string GoldKaratPrice { get; set; }
        public string MountedPrice { get; set; }
        public string Gold_Karat { get; set; }
    }
    public class MLWBDisplayAction
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string CostA { get; set; }
        public string CostB { get; set; }
        public string CostC { get; set; }
        public string CostD { get; set; }

        public string Gold_Cost { get; set; }
        public string Mount_Cost { get; set; }
        public string YG_Cost { get; set; }
        public string WG_Cost { get; set; }
        public string ALL_Cost { get; set; }

        public string Cellular_Cost { get; set; }
        public string Repair_Cost { get; set; }
        public string Cleaning_Cost { get; set; }

        public string Watch_Cost { get; set; }
        public string Gold { get; set; }
        public string Cellular { get; set; }
        public string Watch { get; set; }
    }
    public class DisplayAction
    {
        public string Respons { set; get; }
        public string Result { get; set; }

        public string CostA { get; set; }
        public string CostB { get; set; }
        public string CostC { get; set; }
        public string CostD { get; set; }

        public string ALL_Weight { get; set; }
        public string ALL_Karat { get; set; }
        public string ALL_Cost { get; set; }

        public string Gold_Cost { get; set; }
        public string Mount_Cost { get; set; }
        public string YG_Cost { get; set; }
        public string WG_Cost { get; set; }

        public string Cellular_Cost { get; set; }
        public string Repair_Cost { get; set; }
        public string Cleaning_Cost { get; set; }

        public string Watch_Cost { get; set; }




    }
    public class RetrieveInfo
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string st { get; set; }
        public string photo { get; set; }
        public string itemIDs { get; set; }
        public string cost_id { get; set; }
        public string actionclass { get; set; }
        public string sortcode { get; set; }
        public string status { get; set; }


        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ptn { get; set; }
        public string ptnbarcode { get; set; }
        public string loanvalue { get; set; }
        public string maturitydate { get; set; }
        public string expirydate { get; set; }
        public string loandate { get; set; }

        public string itemid { get; set; }
        public string refallbarcode { get; set; }
        public string refitemcode { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public string refqty { get; set; }
        public string qty { get; set; }
        public string karatgrading { get; set; }
        public string caratsize { get; set; }
        public string weight { get; set; }
        public string all_desc { get; set; }
        public string SerialNo { get; set; }
        public string all_karat { get; set; }
        public string all_carat { get; set; }
        public string all_weight { get; set; }
        public string currency { get; set; }
        public string all_cost { get; set; }
        public string photoname { get; set; }
        public string all_price { get; set; }
        public string cellular_cost { get; set; }
        public string watch_cost { get; set; }
        public string repair_cost { get; set; }
        public string cleaning_cost { get; set; }
        public string Gold_cost { get; set; }
        public string mount_cost { get; set; }
        public string yg_cost { get; set; }
        public string wg_cost { get; set; }
        public string appraisevalue { get; set; }
        public string bedrnm { get; set; }

    }
    public class RetrieveInfoRet
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string st { get; set; }
        public string photo { get; set; }
        public string itemid { get; set; }
        public string cost_id { get; set; }
        public string actionclass { get; set; }
        public string sortcode { get; set; }
        public string status { get; set; }

        public string ptn { get; set; }
        public string ptnbarcode { get; set; }
        public string branchcode { get; set; }
        public string branchname { get; set; }
        public string loanvalue { get; set; }
        public string refitemcode { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public string refqty { get; set; }
        public string qty { get; set; }
        public string karatgrading { get; set; }
        public string caratsize { get; set; }
        public string weight { get; set; }
        public string all_desc { get; set; }
        public string SerialNo { get; set; }
        public string all_karat { get; set; }
        public string all_carat { get; set; }
        public string all_weight { get; set; }
        public string currency { get; set; }
        public string all_cost { get; set; }
        public string photoname { get; set; }
        public string all_price { get; set; }
        public string appraisevalue { get; set; }
        public string cellular_cost { get; set; }
        public string watch_cost { get; set; }
        public string repair_cost { get; set; }
        public string cleaning_cost { get; set; }
        public string gold_cost { get; set; }
        public string mount_cost { get; set; }
        public string yg_cost { get; set; }
        public string wg_cost { get; set; }




    }
    public class RetrievePTN_Barcode
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public List<string> BranchCode { get; set; }
        public List<string> BranchName { get; set; }
        public List<string> PTN { get; set; }
        public List<string> PTNBarcode { get; set; }
        public List<string> RefALLBarcode { get; set; }


    }
    public class NewCostingSave
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public Pricing_NewCostingSave_Param Pricing_NewCostingSave_Param { get; set; }
    }
    public class Pricing_NewCostingSave_Param
    {
        public string refqty { get; set; }
    }
    public class SaveMounted
    {
        public string Respons { set; get; }
        public string Result { get; set; }
        public string Lotno { get; set; }
    }
    public class saveCellular
    {
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class saveCellular1
    {
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class savewatch1
    {
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class savewatch
    {
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class RetrieveInfo2
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public string RefLotno { get; set; }
        public string RefallBarcode { get; set; }
        public string PTN { get; set; }
        public string ItemID { get; set; }
        public string PTNBarcode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Loanvalue { get; set; }
        public string RefItemcode { get; set; }
        public string Itemcode { get; set; }
        public string BranchItemDesc { get; set; }
        public string RefQty { get; set; }
        public string Qty { get; set; }
        public string KaratGrading { get; set; }
        public string CaratSize { get; set; }
        public string Weight { get; set; }
        public string Actionclass { get; set; }
        public string Sortcode { get; set; }
        public string ALL_desc { get; set; }
        public string SerialNo { get; set; }
        public string ALL_karat { get; set; }
        public string ALL_carat { get; set; }
        public string ALL_Cost { get; set; }
        public string ALL_Weight { get; set; }
        public string currency { get; set; }
        public string PhotoName { get; set; }
        public string all_price { get; set; }
        public string Cellular_cost { get; set; }
        public string Watch_cost { get; set; }
        public string Repair_cost { get; set; }
        public string Cleaning_cost { get; set; }
        public string Gold_cost { get; set; }
        public string Mount_cost { get; set; }
        public string YG_cost { get; set; }
        public string WG_cost { get; set; }
        public string MaturityDate { get; set; }
        public string ExpiryDate { get; set; }
        public string LoanDate { get; set; }
        public string Status { get; set; }





    }
    public class btnSaveMLWB_Click
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public string all_desc { get; set; }
        public string SerialNo { get; set; }
        public string refqty { get; set; }
        public string all_weight { get; set; }
        public string all_karat { get; set; }
        public string all_carat { get; set; }
        public string price_desc { get; set; }
        public string price_weight { get; set; }
        public string price_karat { get; set; }
        public string price_carat { get; set; }
        public string cellular_cost { get; set; }
        public string watch_cost { get; set; }
        public string Repair_cost { get; set; }
        public string cleaning_cost { get; set; }
        public string gold_cost { get; set; }
        public string mount_cost { get; set; }
        public string yg_cost { get; set; }
        public string wg_cost { get; set; }
        public string all_cost { get; set; }
        public string refallbarcode { get; set; }
    }
    public class ComboBox6_SelectedIndexChanged
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public string Gold_Karat { get; set; }
        public string Plain { get; set; }
    }
    public class Releasing
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public GenerateLot Data { get; set; }
        public releaser releaserlist { get; set; }
        public Lotno lotnolist { get; set; }
        public addrow addrowref { get; set; }
        public DataSet dataset { get; set; }
        public lotnorefresh lotnumberlist { get; set; }
        public cmbCostCenter_KeyPress lotnumberlist2 { get; set; }
        public cmbbarcode_KeyPress items { get; set; }
        public cb_SelectedIndexChanged fullname { get; set; }
        public cb_SelectedIndexChanged2 lot { get; set; }
        public cmbItems_KeyPress datas { get; set; }
        public CallCostCenter _CallCostCenter { get; set; }
        public frmDISTRIReleasing_rpt_Load _frmDISTRIReleasing_rpt_Load { get; set; }

        public txtlot_KeyPress _txtlot_KeyPress { get; set; }
        public txtlot_KeyPress1111 _txtlot_KeyPress1111 { get; set; }
        public frmPRICINGReleasing_rpt_Load _frmPRICINGReleasing_rpt_Load { get; set; }
        public habwa_date3 _habwa_date3 { get; set; }
        public txtlot_KeyPress3 _txtlot_KeyPress3 { get; set; }
    }
    public class GenerateLot
    {
        public string Lotnumber { get; set; }

    }
    public class releaser
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public List<string> fullname2 { get; set; }

        public List<string> ID { get; set; }

        public List<string> res_id { get; set; }

        public List<string> fullname { get; set; }

        public List<string> sur_name { get; set; }
    }
    public class Lotno
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public List<string> lotnumber { get; set; }

    }
    public class addrow
    {
        public List<string> lotnumber { get; set; }
        public string ptn { get; set; }
        public string refALLBarcode { get; set; }
        public string price_desc { get; set; }
        public string price_weight { get; set; }
        public string price_karat { get; set; }
        public string price_carat { get; set; }
        public string all_value { get; set; }

    }
    public class lotnorefresh
    {
        public List<string> lotnumber { get; set; }
    }
    public class cmbCostCenter_KeyPress
    {
        public List<string> lotnumber { get; set; }
    }
    public class cmbbarcode_KeyPress
    {
        public string RefallBarcode { get; set; }
        public string PTN { get; set; }
        public string RefItemcode { get; set; }
        public string RefQty { get; set; }
        public string desc { get; set; }
        public string karat { get; set; }
        public string carat { get; set; }
        public string cost { get; set; }
        public string wt { get; set; }
        public string status { get; set; }

    }
    public class cb_SelectedIndexChanged
    {
        public string fullname3 { get; set; }

    }
    public class cb_SelectedIndexChanged2
    {
        public List<string> lotnumber { get; set; }

    }
    public class cmbItems_KeyPress
    {
        public string DBLWB_ID { get; set; }
        public string Lotno_Drec { get; set; }
        public string RefLotno_DRec { get; set; }
        public string Division_DRec { get; set; }
        public string PTN_Drec { get; set; }
        public string MPTN_Drec { get; set; }
        public string ItemCode_Drec { get; set; }
        public string PTNItemDesc_Drec { get; set; }
        public string Quantity_Drec { get; set; }
        public string ALLBarcode { get; set; }
        public string ItemSource_Drec { get; set; }
        public string Action_ID_Drec { get; set; }
        public string Status_ID_Drec { get; set; }
        public string MLWBDetailStamp { get; set; }


    }
    public class ViewPricedItem
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public TextBox1_KeyPress List { get; set; }
    }
    public class TextBox1_KeyPress
    {
        public List<string> allbarcode { get; set; }
        public List<string> price_desc { get; set; }
        public List<string> all_price { get; set; }
    }
    public class View_ALL_Barcode_Details
    {
        public string Result { get; set; }
        public string Respons { get; set; }
    }
    public class PRicingDisplayAction
    {
        public string dCostA { get; set; }
        public string dCostB { get; set; }
        public string dCostC { get; set; }
        public string dCostD { get; set; }
    }
    public class Pricing_Report
    {
        public string receivedate { get; set; }
        public string Result { get; set; }
        public string Respons { get; set; }
        public List<ViewPricedItem_Report> _ViewPricedItem { get; set; }
        public List<pricing_receiving_report> _pricing_receiving_Report { get; set; }
        public List<unreceive_items> _unreceive_items { get; set; }
        public List<_releasing> _releasing { get; set; }
    }
    public class Releasing_Report
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public List<_releasing> _releasing { get; set; }
    }
    public class ViewPricedItem_Report
    {
        public string RefLotno { get; set; }
        public string branchCode { get; set; }
        public string branchname { get; set; }
        public string Receiver { get; set; }
        public string custodian { get; set; }
        public string receivedate { get; set; }
        public string refallbarcode { get; set; }
        public string price_desc { get; set; }
        public string serialno { get; set; }
        public int quantity { get; set; }
        public string price_karat { get; set; }
        public double price_weight { get; set; }
        public double price_carat { get; set; }
        public double all_price { get; set; }



    }
    public class Receiving
    {
        public string Result { get; set; }
        public string Respons { get; set; }
        public ReadyPrintForm1 _ReadyPrintForm1 { get; set; }
        public ReadyPrintForm2 _ReadyPrintForm2 { get; set; }
        public searchLotNumber _searchLotNumber { get; set; }
        public costcenters _costcenter { get; set; }
        public genetareLOTNO2 _generate { get; set; }
        public DetailList _DetailList { get; set; }
        public frmPRICINGReceiving_rpt_Load _frmPRICINGReceiving_rpt_Load { get; set; }
        public habwa_date2 _habwa_date { get; set; }
        public Function_receiver _Function_receiver { get; set; }
        public cmbEmployee_SelectedIndexChanged _cmbEmployee_SelectedIndexChanged { get; set; }
        public cboreceive_SelectedIndexChanged _cboreceive_SelectedIndexChanged { get; set; }
        public cboreceive_KeyPress _cboreceive_KeyPress { get; set; }
        public txtlot_KeyPress _txtlot_KeyPress1 { get; set; }

    }
    public class ReadyPrintForm1
    {
        public string consigncode { get; set; }
        public string consignname { get; set; }
    }
    public class ReadyPrintForm2
    {
        public string consignto { get; set; }
    }
    public class searchLotNumber
    {
        public List<string> LotNum { get; set; }
        public List<string> EmpName1 { get; set; }
        public List<string> receiverDate { get; set; }
        public List<string> releasedate { get; set; }
        public List<string> receiveDate { get; set; }
        public double Empty { get; set; }
        public string None { get; set; }
        public string converted { get; set; }

    }
    public class searchLotNumber2
    {

        public List<string> receiverDate { get; set; }
        public List<string> releasedate { get; set; }


    }
    public class habwa_date
    {
        public string iMonth { get; set; }
        public string iYear { get; set; }
    }
    public class pricing_receiving_report
    {
        public string lotno { get; set; }
        public string reflotno { get; set; }
        public string receiver { get; set; }
        public string employee { get; set; }
        public string receivedate { get; set; }
        public string refallbarcode { get; set; }
        public string allbarcode { get; set; }
        public string ptn { get; set; }
        public Int32 itemid { get; set; }
        public string ptnbarcode { get; set; }
        public string branchcode { get; set; }
        public string branchname { get; set; }
        public double loanvalue { get; set; }
        public string refitemcode { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public string SerialNo { get; set; }
        public Int32 refqty { get; set; }
        public Int32 qty { get; set; }
        public string karatgrading { get; set; }
        public double caratsize { get; set; }
        public double weight { get; set; }
        public string actionclass { get; set; }
        public string sortcode { get; set; }
        public string all_desc { get; set; }
        public string all_karat { get; set; }
        public double all_carat { get; set; }
        public double all_cost { get; set; }
        public double all_weight { get; set; }
        public double appraisevalue { get; set; }
        public string currency { get; set; }
        public string photoname { get; set; }
        public string pricedesc { get; set; }
        public string price_karat { get; set; }
        public double price_weight { get; set; }
        public double price_carat { get; set; }
        public double all_price { get; set; }
        public double cellular_cost { get; set; }
        public double watch_cost { get; set; }
        public double repair_cost { get; set; }
        public double cleaning_cost { get; set; }
        public double gold_cost { get; set; }
        public string costdate { get; set; }
        public string costname { get; set; }
        public string releasedate { get; set; }
        public string releaser { get; set; }
        public string maturitydate { get; set; }
        public string expirydate { get; set; }
        public string loandate { get; set; }
        public string status { get; set; }
        public string month { get; set; }
        public string year { get; set; }



    }
    public class unreceive_items
    {
        public string COSTCENTER { get; set; }
        public string LOTNO { get; set; }
        public string REFALLBARCODE { get; set; }
        public Int32 QTY { get; set; }
        public string SERIALNO { get; set; }
        public string DESCRIPTION { get; set; }
        public string KARAT { get; set; }
        public Double WEIGHT { get; set; }
        public Double CARAT { get; set; }
        public Double ALL_PRICE { get; set; }
    }
    public class _releasing
    {
        public string reflotno { get; set; }
        public string lotno { get; set; }
        public string receiver { get; set; }
        public string employee { get; set; }
        public string receivedate { get; set; }
        public string refallbarcode { get; set; }
        public string allbarcode { get; set; }
        public string ptn { get; set; }
        public Int32 itemid { get; set; }
        public string ptnbarcode { get; set; }
        public string branchcode { get; set; }
        public string branchname { get; set; }
        public double loanvalue { get; set; }
        public string refitemcode { get; set; }
        public string branchitemdesc { get; set; }
        public Int32 refqty { get; set; }
        public Int32 quantity { get; set; }
        public string karatgrading { get; set; }
        public string caratsize { get; set; }
        public string actionclass { get; set; }
        public string sortcode { get; set; }
        public string appraisevalue { get; set; }
        public string currency { get; set; }
        public string photoname { get; set; }
        public string pricedesc { get; set; }
        public string pricekarat { get; set; }
        public double priceweight { get; set; }
        public double pricecarat { get; set; }
        public string SerialNo { get; set; }
        public double allprice { get; set; }
        public double cellcost { get; set; }
        public double watchcost { get; set; }
        public double repaircost { get; set; }
        public double cleaningcost { get; set; }
        public double goldcost { get; set; }
        public double mountcost { get; set; }
        public string releasedate { get; set; }
        public string releaser { get; set; }
        public string status { get; set; }
        public string month { get; set; }
        public string year { get; set; }

    }
    public class frmPRICINGReceiving_rpt_Load
    {
        public string lotno { get; set; }
        public string receivedate { get; set; }
    }
    public class costcenters
    {
        public string CostDept { get; set; }
    }
    public class genetareLOTNO2
    {
        public List<string> reflotno { get; set; }
    }
    public class DetailList
    {
        public List<string> reflotno { get; set; }
        public List<string> division { get; set; }
        public List<string> ptn { get; set; }
        public List<string> itemcode { get; set; }
        public List<string> ptnitemdesc { get; set; }
        public List<string> quantity { get; set; }
        public List<string> barcode { get; set; }
        public List<string> all_desc { get; set; }
        public List<string> actionclass { get; set; }
        public List<string> status { get; set; }
    }
    public class habwa_date2
    {
        public string month { get; set; }
        public string year { get; set; }
    }
    public class txtlot_KeyPress
    {
        public string receivedate { get; set; }
    }
    public class Function_receiver
    {
        public List<string> fullname2 { get; set; }
    }
    public class cmbEmployee_SelectedIndexChanged
    {
        public string res_id { get; set; }
    }
    public class cboreceive_SelectedIndexChanged
    {
        public string fullname2 { get; set; }
    }
    public class cboreceive_KeyPress
    {
        public List<string> reflotno { get; set; }
    }
    public class CallCostCenter
    {
        public List<string> CostDept { get; set; }
    }
    public class frmDISTRIReleasing_rpt_Load
    {
        public string releasedate { get; set; }
    }
    public class txtlot_KeyPress1111
    {
        public string month { get; set; }
        public string day { get; set; }
        public string year { get; set; }
    }
    public class frmPRICINGReleasing_rpt_Load
    {
        public string lotno { get; set; }
        public string releasedate { get; set; }

    }
    public class habwa_date3
    {
        public string month { get; set; }
        public string year { get; set; }
    }
    public class txtlot_KeyPress3
    {
        public string month { get; set; }
        public string day { get; set; }
        public string year { get; set; }
    }
    public class frmRec_Loadp
    {
        public List<string> GOLD_KARAT { get; set; }
        public List<string> Plain { get; set; }
    }
    public class formax_lotno 
    {
        public string reflotno { get; set; }
    }
      #endregion

    public class MLWB
    {
        public string respCode { get; set; }
        public string respMsg { get; set; }
      
    }

}