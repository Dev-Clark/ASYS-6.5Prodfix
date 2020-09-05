using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ASYSOutsourceService
{
    public class edit_
    {
        public String BranchCode;
        public String BranchName;
        public String Ptn;
        public String PtnBarcode;
        public String LoanValue;
        public String transdate;
        public String pulloutdate;
        public String custId;
        public String custName;
        public String custAddress;
        public String custCity;
        public String custGender;
        public String custtel;
        public String maturitydate;
        public String expirydate;
        public String loandate;
    }
    public class ptn
    {
        public List<string> reflotno;
        public List<string> refallbarcode;
        public List<string> refitemcode;
        public List<string> id;
        public List<string> branchitemdesc;
        public List<string> refqty;
        public List<string> karatgrading;
        public List<string> caratsize;
        public List<string> weight;
        public List<string> actionclass;
        public List<string> sortcode;
        public List<string> all_karat;
        public List<string> all_cost;
        public List<string> all_weight;
        public List<string> appraisevalue;
        public List<string> status;
        public List<string> appraiser;
    }
    public class Connection
    {
        //----------------------------------------------------------Here-----------------------------------------//
        #region Module
        #region Declaration
        //--------------------------
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
        public SqlDataReader x;

        // private SqlDataAdapter da;
        // private SqlDataAdapter da1;
        // private SqlDataAdapter da2;
        private SqlDataAdapter da3;
        //private SqlDataAdapter da4;
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

        public static string sDatasource1 { get; set; }
        public static string sDatabase1 { get; set; }
        public static string sUsername1 { get; set; }
        public static string sPassword1 { get; set; }
        public static string sDatasource2 { get; set; }
        public static string sDatabase2 { get; set; }
        public static string sUsername2 { get; set; }
        public static string sPassword2 { get; set; }
        public static string sDatasource3 { get; set; }
        public static string sDatabase3 { get; set; }
        public static string sUsername3 { get; set; }
        public static string sPassword3 { get; set; }

        public string conString { get; set; }
        public string conString1 { get; set; }
        public string conString2 { get; set; }
        public string conString3 { get; set; }
        public string conString4 { get; set; }
        //------------------------------------Module---------------------------------//
        public static string sDB { get; set; }
        public static string dserver { get; set; }
        public static string humres2 { get; set; }
        public static string bedryf2 { get; set; }
        public static string bedryfLuzon { get; set; }
        public static string bedryfVisayas { get; set; }
        public static string bedryfShowroom { get; set; }
        public static string bedryfMindanao { get; set; }
        public static string bedryfLNCR { get; set; }

        public static string myregion { get; set; }

        public static string photodes { get; set; }
        public static string LImageDestination { get; set; }
        public static string XMLSource { get; set; }
        public static string SPassword { get; set; }
        public static string LImageSource { get; set; }
        public static string SUsername { get; set; }
        public static string XMLDestination { get; set; }
        public static string JewelrySDestination { get; set; }
        public static string GoodstockSDestination { get; set; }
        public static string ImportFolderWC { get; set; }
        public static string ImportFolderJW { get; set; }
        public static string ARJDestination { get; set; }
        public static string ImportExportServer { get; set; }
        public static string ExportFolderBackup { get; set; }
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
                    sPassword1 =  line.Replace(Key_Pass1, "");
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
                case "LNCR"://made some modification this part here!
                    Key_Server1 = "[ServerLNCR]=";
                    Key_DataBase1 = "[DataBaseLNCR]=";
                    Key_User1 = "[UserLNCR]=";
                    Key_Pass1 = "[PasswordLNCR]=";
                    break;
                case "NCR"://made some modification this part here!
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
                    sPassword1 =  line.Replace(Key_Pass1, "");
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
                    sPassword1 = line.Replace(Key_Pass1, "");
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
                    sPassword1 =  line.Replace(Key_Pass1, "");
                    
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
                    sPassword1 = line.Replace(Key_Pass1, ""); //decryptPass(line.Replace(Key_Pass1, ""));
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
            dr1 = command1.ExecuteReader(CommandBehavior.CloseConnection);

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
        //---------Transaction
        public void BeginTransax1()
        {
            tranCon1 = connection1.BeginTransaction();
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

         public SqlDataReader ExecuteDr33()
        {
            dr3 = command3.ExecuteReader();//(CommandBehavior.CloseConnection);

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
        public void commandExeParam3(string query)
        {
            command3 = connection3.CreateCommand();
            command3.CommandTimeout = 600;
            command3.CommandText = query;
        }
        public void commandExeStoredParam3(string query)
        {
            command3 = connection3.CreateCommand();
            command3.CommandType = CommandType.StoredProcedure;
            command3.CommandTimeout = 600;
            command3.CommandText = query;

        }


        //---------Transaction
        public void BeginTransax3()
        {
            tranCon3 = connection3.BeginTransaction();
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
            command4.CommandTimeout = 60;
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
        //--------Command Paramaters
        #endregion

        #region Parameters
        public void RetrieveInfoParam(string param, string param2, string data, string data2)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParams1(string param, string param2, string data, string data2)
        {
            command1.Parameters.AddWithValue(param, data);
            command1.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParams(string param, string param2, string data, string data2)
        {
            command3.Parameters.AddWithValue(param, data);
            command3.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParamsss(string param, string param2, string data, DateTime data2)
        {
            command3.Parameters.AddWithValue(param, data);
            command3.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParam3(string param, string param2, string data, int data2)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParams3(string param, string param2, string data, int data2)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParamss3(string param, string param2, string data, int data2)
        {
            command3.Parameters.AddWithValue(param, data);
            command3.Parameters.AddWithValue(param2, data2);
        }
        public void RetrieveInfoParam2(string param, string data)
        {
            command.Parameters.AddWithValue(param, data);
        }
        public void RetrieveInfoParam1(string param, string data)
        {
            command1.Parameters.AddWithValue(param, data);
        }
        public void RetrieveInfoParams2(string param, string data)
        {
            command3.Parameters.AddWithValue(param, data);
        }
        public void RetrieveInfoParamss2(string param, string data)
        {
            command4.Parameters.AddWithValue(param, data);
        }
        public void SaveOutSourceParam(string param, string param2, string param3, string param4, string param5, string param6,
            string param7, string param8, string param9, string param10, string param11, string param12, string param13, string param14,
            string param15, string param16, string param17, string param18, string param19, string param20, string param21, string param22,
            string param23, string param24, string param25, string data, string data2, string data3, string data4, string data5, string data6,
            string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16,
            string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
            command.Parameters.AddWithValue(param11, data11);
            command.Parameters.AddWithValue(param12, data12);
            command.Parameters.AddWithValue(param13, data13);
            command.Parameters.AddWithValue(param14, data14);
            command.Parameters.AddWithValue(param15, data15);
            command.Parameters.AddWithValue(param16, data16);
            command.Parameters.AddWithValue(param17, data17);
            command.Parameters.AddWithValue(param18, data18);
            command.Parameters.AddWithValue(param19, data19);
            command.Parameters.AddWithValue(param20, data20);
            command.Parameters.AddWithValue(param21, data21);
            command.Parameters.AddWithValue(param22, data22);
            command.Parameters.AddWithValue(param23, data23);
            command.Parameters.AddWithValue(param24, data24);
            command.Parameters.AddWithValue(param25, data25);

        }
        public void SaveOutSourceParam2(string param, string param2, string param3, string param4, string param5, string param6, string param7,
            string param8, string param9, string param10, string param11, string data, string data2, string data3, string data4, string data5, string data6,
            string data7, string data8, string data9, string data10, string data11)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
            command.Parameters.AddWithValue(param11, data11);
        }
        public void SaveReturnsParam(string param, string param2, string param3, string param4, string param5, string param6, string param7,
            string data, string data2, string data3, string data4, string data5, string data6, string data7)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
        }
        public void SaveReturnParam2(string param, string param2, string param3, string data, string data2, string data3)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
        }
        public void SaveReturnParam21(string param, string param2, string param3, int data, int data2, string data3)
        {
            command1.Parameters.AddWithValue(param, data);
            command1.Parameters.AddWithValue(param2, data2);
            command1.Parameters.AddWithValue(param3, data3);
        }
        public void SaveReturnParam213(string param, string param2, string param3, int data, int data2, string data3)
        {
            command3.Parameters.AddWithValue(param, data);
            command3.Parameters.AddWithValue(param2, data2);
            command3.Parameters.AddWithValue(param3, data3);
        }
        public void SaveReturnParam212(string param, string param2, string param3, string data, int data2, string data3)
        {
            command1.Parameters.AddWithValue(param, data);
            command1.Parameters.AddWithValue(param2, data2);
            command1.Parameters.AddWithValue(param3, data3);
        }
        public void SaveReturnParams(string param, string param2, string param3, string data, string data2, string data3)
        {
            command3.Parameters.AddWithValue(param, data);
            command3.Parameters.AddWithValue(param2, data2);
            command3.Parameters.AddWithValue(param3, data3);
        }
        public void SaveReturnByControlParam(string param, string param2, string param3, string param4, string param5, string param6, string param7,
            string param8, string param9, string param10, string data, string data2, string data3, string data4, string data5, string data6, string data7,
            string data8, string data9, string data10)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
        }
        public void SaveReturnByControlParam2(string param, string param2, string param3, string param4, string data, string data2, string data3, string data4)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
        }
        public void SaveReturnByControlParam21(string param, string param2, string param3, string param4, int data, int data2, string data3, string data4)
        {
            command1.Parameters.AddWithValue(param, data);
            command1.Parameters.AddWithValue(param2, data2);
            command1.Parameters.AddWithValue(param3, data3);
            command1.Parameters.AddWithValue(param4, data4);
        }
        public void SaveOutSourceCostingGoodStock(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
            string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
            int data, string data2, string data3, string data4, double data5, double data6, double data7, string data8, string data9, string data10,
            double data11, double data12, double data13, double data14, double data15, double data16, string data17, string data18)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
            command.Parameters.AddWithValue(param11, data11);
            command.Parameters.AddWithValue(param12, data12);
            command.Parameters.AddWithValue(param13, data13);
            command.Parameters.AddWithValue(param14, data14);
            command.Parameters.AddWithValue(param15, data15);
            command.Parameters.AddWithValue(param16, data16);
            command.Parameters.AddWithValue(param17, data17);
            command.Parameters.AddWithValue(param18, data18);
        }
        public void SaveOutSourceCostingGoodStock4(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
            string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
            int data, string data2, string data3, string data4, double data5, double data6, double data7, string data8, string data9, string data10,
            double data11, double data12, double data13, double data14, double data15, double data16, string data17, string data18)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
            command4.Parameters.AddWithValue(param3, data3);
            command4.Parameters.AddWithValue(param4, data4);
            command4.Parameters.AddWithValue(param5, data5);
            command4.Parameters.AddWithValue(param6, data6);
            command4.Parameters.AddWithValue(param7, data7);
            command4.Parameters.AddWithValue(param8, data8);
            command4.Parameters.AddWithValue(param9, data9);
            command4.Parameters.AddWithValue(param10, data10);
            command4.Parameters.AddWithValue(param11, data11);
            command4.Parameters.AddWithValue(param12, data12);
            command4.Parameters.AddWithValue(param13, data13);
            command4.Parameters.AddWithValue(param14, data14);
            command4.Parameters.AddWithValue(param15, data15);
            command4.Parameters.AddWithValue(param16, data16);
            command4.Parameters.AddWithValue(param17, data17);
            command4.Parameters.AddWithValue(param18, data18);
        }
        public void SaveCostingUpdateParam(string param, string param2, string param3, string param4, string param5, string param6, string data,
            string data2, string data3, double data4, double data5, string data6)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
        }
        public void SaveCostingUpdateParams4(string param, string param2, string param3, string param4, string param5, string param6, string data,
          string data2, string data3, double data4, double data5, string data6)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
            command4.Parameters.AddWithValue(param3, data3);
            command4.Parameters.AddWithValue(param4, data4);
            command4.Parameters.AddWithValue(param5, data5);
            command4.Parameters.AddWithValue(param6, data6);
        }
        public void SaveCostingUpdateParam4(string param, string param2, string param3, string param4, string param5, string param6, string data,
            string data2, string data3, double data4, double data5, string data6)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
            command4.Parameters.AddWithValue(param3, data3);
            command4.Parameters.AddWithValue(param4, data4);
            command4.Parameters.AddWithValue(param5, data5);
            command4.Parameters.AddWithValue(param6, data6);
        }
        public void saveCostingParam(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
            string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
            string param19, string param20, string param21, int data, string data2, string data3, string data4, double data5, double data6, double data7,
            string data8, string data9, string data10, double data11, double data12, double data13, double data14, double data15, double data16, string data17,
            string data18, string data19, string data20, string data21)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
            command.Parameters.AddWithValue(param11, data11);
            command.Parameters.AddWithValue(param12, data12);
            command.Parameters.AddWithValue(param13, data13);
            command.Parameters.AddWithValue(param14, data14);
            command.Parameters.AddWithValue(param15, data15);
            command.Parameters.AddWithValue(param16, data16);
            command.Parameters.AddWithValue(param17, data17);
            command.Parameters.AddWithValue(param18, data18);
            command.Parameters.AddWithValue(param19, data19);
            command.Parameters.AddWithValue(param20, data20);
            command.Parameters.AddWithValue(param21, data21);
        }
        public void saveCostingParam4(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
            string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
            string param19, string param20, string param21, int data, string data2, string data3, string data4, double data5, double data6, double data7,
            string data8, string data9, string data10, double data11, double data12, double data13, double data14, double data15, double data16, string data17,
            string data18, string data19, string data20, string data21)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
            command4.Parameters.AddWithValue(param3, data3);
            command4.Parameters.AddWithValue(param4, data4);
            command4.Parameters.AddWithValue(param5, data5);
            command4.Parameters.AddWithValue(param6, data6);
            command4.Parameters.AddWithValue(param7, data7);
            command4.Parameters.AddWithValue(param8, data8);
            command4.Parameters.AddWithValue(param9, data9);
            command4.Parameters.AddWithValue(param10, data10);
            command4.Parameters.AddWithValue(param11, data11);
            command4.Parameters.AddWithValue(param12, data12);
            command4.Parameters.AddWithValue(param13, data13);
            command4.Parameters.AddWithValue(param14, data14);
            command4.Parameters.AddWithValue(param15, data15);
            command4.Parameters.AddWithValue(param16, data16);
            command4.Parameters.AddWithValue(param17, data17);
            command4.Parameters.AddWithValue(param18, data18);
            command4.Parameters.AddWithValue(param19, data19);
            command4.Parameters.AddWithValue(param20, data20);
            command4.Parameters.AddWithValue(param21, data21);
        }
        public void saveCostingParam2(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
           string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
           string param19, string param20, string param21, int data, string data2, string data3, string data4, double data5, double data6, double data7,
           string data8, string data9, string data10, double data11, double data12, double data13, double data14, double data15, double data16, double data17,
           double data18, double data19, string data20, string data21)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
            command.Parameters.AddWithValue(param7, data7);
            command.Parameters.AddWithValue(param8, data8);
            command.Parameters.AddWithValue(param9, data9);
            command.Parameters.AddWithValue(param10, data10);
            command.Parameters.AddWithValue(param11, data11);
            command.Parameters.AddWithValue(param12, data12);
            command.Parameters.AddWithValue(param13, data13);
            command.Parameters.AddWithValue(param14, data14);
            command.Parameters.AddWithValue(param15, data15);
            command.Parameters.AddWithValue(param16, data16);
            command.Parameters.AddWithValue(param17, data17);
            command.Parameters.AddWithValue(param18, data18);
            command.Parameters.AddWithValue(param19, data19);
            command.Parameters.AddWithValue(param20, data20);
            command.Parameters.AddWithValue(param21, data21);
        }
        public void saveCostingParams4(string param, string param2, string param3, string param4, string param5, string param6, string param7, string param8,
           string param9, string param10, string param11, string param12, string param13, string param14, string param15, string param16, string param17, string param18,
           string param19, string param20, string param21, int data, string data2, string data3, string data4, double data5, double data6, double data7,
           string data8, string data9, string data10, double data11, double data12, double data13, double data14, double data15, double data16, double data17,
           double data18, double data19, string data20, string data21)
        {
            command4.Parameters.AddWithValue(param, data);
            command4.Parameters.AddWithValue(param2, data2);
            command4.Parameters.AddWithValue(param3, data3);
            command4.Parameters.AddWithValue(param4, data4);
            command4.Parameters.AddWithValue(param5, data5);
            command4.Parameters.AddWithValue(param6, data6);
            command4.Parameters.AddWithValue(param7, data7);
            command4.Parameters.AddWithValue(param8, data8);
            command4.Parameters.AddWithValue(param9, data9);
            command4.Parameters.AddWithValue(param10, data10);
            command4.Parameters.AddWithValue(param11, data11);
            command4.Parameters.AddWithValue(param12, data12);
            command4.Parameters.AddWithValue(param13, data13);
            command4.Parameters.AddWithValue(param14, data14);
            command4.Parameters.AddWithValue(param15, data15);
            command4.Parameters.AddWithValue(param16, data16);
            command4.Parameters.AddWithValue(param17, data17);
            command4.Parameters.AddWithValue(param18, data18);
            command4.Parameters.AddWithValue(param19, data19);
            command4.Parameters.AddWithValue(param20, data20);
            command4.Parameters.AddWithValue(param21, data21);
        }
        public void SaveCostingUpdateParam(string param, string param2, string param3, string param4, string param5, string param6, string data,
           string data2, string data3, string data4, string data5, string data6)
        {
            command.Parameters.AddWithValue(param, data);
            command.Parameters.AddWithValue(param2, data2);
            command.Parameters.AddWithValue(param3, data3);
            command.Parameters.AddWithValue(param4, data4);
            command.Parameters.AddWithValue(param5, data5);
            command.Parameters.AddWithValue(param6, data6);
        }
        public void SaveCostingUpdateParam1(string param, string param2, string param3, string param4, string param5, string param6, string data,
           string data2, string data3, string data4, string data5, string data6)
        {
            command1.Parameters.AddWithValue(param, data);
            command1.Parameters.AddWithValue(param2, data2);
            command1.Parameters.AddWithValue(param3, data3);
            command1.Parameters.AddWithValue(param4, data4);
            command1.Parameters.AddWithValue(param5, data5);
            command1.Parameters.AddWithValue(param6, data6);
        }
        #endregion
        #endregion
        //----------------------------------------------------------End-------------------------------------------//
    }
    public class entries
    {
        public String barcode;
        public String fetch ;
        public String query;
        public String code;
    }

    public class LoginInfo
    {

        public string jobTitle { get; set; }

        public string fullName { get; set; }

        public int idNumber { get; set; }
        public bool userFlag { get; set; }
        public bool FlagLoggedIn { get; set; }
        public string vw_bedryf { get; set; }
        public string vw_humresall { get; set; }
        public string vw_bedryfLuzon { get; set; }
        public string vw_bedryfVisayas { get; set; }
        public string vw_bedryfVismin { get; set; }
        public string vw_bedryfshowroom { get; set; }
        public string vw_bedryfMindanao { get; set; }
        public string vw_bedryfLNCR { get; set; }
        public string photodes { get; set; }


    }

    public class LoginResult
    {

        public string responseMsg { get; set; }
        public string responseCode { get; set; }
        public LoginInfo data { get; set; }
    }
    public class RespALLResult
    {
       // public <list>
        public edit_ edit;
        public ptn results;
        public List<string> month { get; set; }
        public List<int> count;
        public String lotno;
        public String prendaMonth;
        public int prenda;
        public String TradeMonth;

        public List<string> action_type;



        public String bcode;
        public string responseMsg { get; set; }
        public string responseCode { get; set; }
        public OutSourceModels OutSourcedata { get; set; }
        public checkBarcodeIfExistModels checkdata2 { get; set; }
        public saveReturnSelect_ASYS_ConsignHeaderModels saveReturnSelect_ASYS_ConsignHeaderdata { get; set; }
        public saveReturnEXEC_ASYS_Barcode_GeneratorModels saveReturnEXEC_ASYS_Barcode_Generatordata { get; set; }
        public saveReturnSelectTop1_bedrnr_bedrnmModels saveReturnSelectTop1_bedrnr_bedrnmdata { get; set; }
        public saveReturnSelect_CustomerDetailsModels saveReturnSelect_CustomerDetailsdata { get; set; }
        public saveReturnSelect_bedrnr_bedrnmModels saveReturnSelect_bedrnr_bedrnmdata { get; set; }
        public saveReturnSelect_CustomerDetails2Models saveReturnSelect_CustomerDetails2data { get; set; }
        public saveReturnSelect_ItemDetailsModels saveReturnSelect_ItemDetailsdata { get; set; }
        public saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderModels saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderdata { get; set; }
        public saveOutSourceAddEditSelect_ASYS_REM_DetailModels saveOutSourceAddEditSelect_ASYS_REM_Detaildata { get; set; }
        public saveOutSourceAddEditSelect_ASYS_REMOutSource_HeaderModels saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata { get; set; }
        public saveOutSourceAddEditSelect_ASYS_REMOutSource_HeaderModels saveOutSourceAddEditSelect_ASYS_REMOutSource_Headerdata2 { get; set; }
        public saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtailModels saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtaildata { get; set; }
        public saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_DetailModels saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_Detaildata { get; set; }
        public OutSourceAddEditReturn_Select_ASYS_Distri_DetailModels OutSourceAddEditReturn_Select_ASYS_Distri_Detaildata { get; set; }
        public OutSourceSearch_BedryfModels OutSourceSearch_Bedryfdata { get; set; }
        public OutSourceSearch_BedryfModels2 OutSourceSearch_Bedryf2data { get; set; }
        public OutSourceSearch_BedryfModels3 OutSourceSearch_Bedryf3data { get; set; }
        public OutSourceSearch_CustDetails_ASYS_CreateCustInfoModels OutSourceSearch_CustDetails_ASYS_CreateCustInfodata { get; set; }
        public OutSourceSearch_CustDetails_ASYS_CreateCustInfo2Models OutSourceSearch_CustDetails_ASYS_CreateCustInfo2data { get; set; }
        public OutSourceSearch_CustDetails_ASYS_CreateCustInfo3Models OutSourceSearch_CustDetails_ASYS_CreateCustInfo3data { get; set; }
        public OutSourceDisplay_BedryfModels OutSourceDisplay_Bedryfdata { get; set; }
        public OutSourceDisplay_BedryfModels2 OutSourceDisplay_Bedryf2data { get; set; }
        public Search_LotNumberModels Search_LotNumberdata { get; set; }
        public UnReceivedReportByBranchCodeModels UnReceivedReportByBranchCodedata { get; set; }
        public UnReceivedReportByBranchNameModels UnReceivedReportByBranchNamedata { get; set; }
        public OutSourceCostEditModels OutSourceCostEditdata { get; set; }
        public getOutSourceCostEditModels getOutSourceCostEditdata { get; set; }
        public getREMCostEditModels getREMCostEditdata { get; set; }
        public getREMDataCostEdit2Models getREMCostEditData2 { get; set; }
        public getREMDataCostEditActionClassModels getREMDataCostEditActionClassdata { get; set; }
        public getREMDataCostEditCostDetailsModels getREMDataCostEditCostDetailsdata { get; set; }
        public saveCostingForCellularModels saveCostingForCellulardata { get; set; }
        public selectWatchCostingModels selectWatchCostingdata { get; set; }
        public LoadRecodeActionTypeModels LoadRecodeActionTypedata { get; set; }
        public RecodeRetrieveInfoModels RecodeRetrieveInfodata { get; set; }
        public RecodeRetrieveInfo2Models RecodeRetrieveInfo2data { get; set; }
        public RecodeRetrieveInfo3Models RecodeRetrieveInfo3data { get; set; }
        public RecodeRetrieveInfo4Models RecodeRetrieveInfo4data { get; set; }
        public RecodeRetrieveInfo5Models RecodeRetrieveInfo5data { get; set; }
        public DisplayActionClassModels DisplayActionClassdata { get; set; }
        public DisplayActionClass2Modesl DisplayActionClass2data { get; set; }
        public ExeMaxGenModels ExeMaxGendata { get; set; }
        public ReleasingActClassModels ReleasingActClassdata { get; set; }
        public RefreshLotModels RefreshLotdata { get; set; }
        public CountBarcodeModels CountBarcodedata { get; set; }
        public DisplayToReleaseBarcodeModels DisplayToReleaseBarcodedata { get; set; }
        public DisplayToReleaseALLBarcodeModels DisplayToReleaseALLBarcodedata { get; set; }
        public RPTLoadModels RPTLoaddata { get; set; }
        public RPTCheckBranchCodeModels RPTCheckBranchCodedata { get; set; }
        public ReleasingRPTLoadModels ReleasingRPTLoaddata { get; set; }
        public ReleasingRPTLoadEMPModels ReleasingRPTLoadEMPdata { get; set; }
        public CheckBranchCodeREPORTModels CheckBranchCodeREPORTdata { get; set; }
        public CheckBranchNameREPORTModels CheckBranchNameREPORTdata { get; set; }
        public RelLoadDataByYearModels RelLoadDataByYeardata { get; set; }
        public LoadTradeInOrMissingItemModels LoadTradeInOrMissingItemdata { get; set; }
        public comboChangeModels comboChangedata { get; set; }
        public MainLoadModels MainLoaddata { get; set; }
        public OnLoadREMItemModels OnLoadREMItemdata { get; set; }
        public ReportDataModels ReportData { get; set; }
        public LoadGoldItemModels LoadGoldItemdata { get; set; }
        public DiamondPriceItemModels DiamondPriceItemdata { get; set; }
        public LoadPearlPriceItemModels LoadPearlPriceItemdata { get; set; }
        public PearlPriceItemModels LoadSonePriceItem { get; set; }
        public REMCOStingModels REMCOStingdata { get; set; }
    }
    public class ReportDataModels
    {
        public List<OutSourceRPTList> sp { get; set; }
        public List<ReturnsRPTList> sp2 { get; set; }
        public List<OutSourceUnreceivedList> sp3 { get; set; }
        public List<OutSourceALLUnReceivedItems> sp4 { get; set; }
        public List<TradeInList> TList { get; set; }
        public List<JewelryList> JList { get; set; }
        public List<REMREVReportSmmry> RRRSList { get; set; }
        public List<REMReLRPT> RRRPTList { get; set; }
        public List<ASYSREMROSRPT> ASYSREMROSRPTList { get; set; }
    }
    public class ASYSREMROSRPT
    {
        public string lotno { get; set; }
        public string allbarcode { get; set; }
        public string PTN { get; set; }
        public int QTY { get; set; }
        public string ActionClass { get; set; }
        public string ALL_Desc { get; set; }
        public string ALL_Karat { get; set; }
        public double ALL_Carat { get; set; }
        public string SerialNo { get; set; }
        public double ALL_Weight { get; set; }
        public double ALL_Cost { get; set; }
        public double ALL_price { get; set; }
        public string ReleaseDate { get; set; }
        public string Releaser { get; set; }
        public string Status { get; set; }
    }
    public class REMReLRPT
    {
        public string lotno { get; set; }
        public string allbarcode { get; set; }
        public string PTN { get; set; }
        public int QTY { get; set; }
        public string ActionClass { get; set; }
        public string ALL_Desc { get; set; }
        public string ALL_Karat { get; set; }
        public double ALL_Carat { get; set; }
        public double ALL_Weight { get; set; }
        public double ALL_Cost { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string Releaser { get; set; }
        public string releasedate { get; set; }
        public string Status { get; set; }
    }
    public class REMREVReportSmmry
    {
        public string Lotno { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PTN { get; set; }
        public string Itemcode { get; set; }
        public string ItemDesc { get; set; }
        public int QTY { get; set; }
        public string Karat { get; set; }
        public string ActionClass { get; set; }
        public string SortCode { get; set; }
        public double Weight { get; set; }
        public double AppraiseValue { get; set; }
        public double LoanValue { get; set; }
        public string Receiver { get; set; }
        public string ReceiveDate { get; set; }
        public string TransDate { get; set; }
       
    }
    public class JewelryList
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string PTN { get; set; }
        public string BranchItemDesc { get; set; }
        public int Qty { get; set; }
        public string KaratGrading { get; set; }
        public double CaratSize { get; set; }
        public double Weight { get; set; }
        public double ALL_Cost { get; set; }
        public double LoanValue { get; set; }
        public string Sortername { get; set; }
        public string ActionClass { get; set; }
        public string SortCode { get; set; }
    }
    public class TradeInList
    {
        public String TradeMonth;
        public string Division { get; set; }
        public string Divisionname { get; set; }
        public string Transaction_No { get; set; }
        public double Appraisal_Amount { get; set; }
        public string Reflotno { get; set; }
        public string Itemcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Karat { get; set; }
        public double Carat { get; set; }
        public double Weight { get; set; }
        public string ReceiveDate { get; set; }
        public string Receiver { get; set; }
        public string ReleaseDate { get; set; }
        public string Releaser { get; set; }
        public string Status { get; set; }


    }
    public class OutSourceALLUnReceivedItems
    {
        public int ID { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PTN { get; set; }
        public string Descri { get; set; }
        public string Karat { get; set; }
        public int Qty { get; set; }
        public double Carat { get; set; }
        public double Weight { get; set; }
        public double LoanValue { get; set; }

    }

    public class OutSourceUnreceivedList
    {
        public string StockReturnNumber { get; set; }
        public string Returndate { get; set; }
        public string bedrnr { get; set; }
        public string Itemcode { get; set; }
        public string Consigncode { get; set; }
        public string bedrnm { get; set; }
        public string Description { get; set; }
        public string Karat { get; set; }
        public double Carat { get; set; }
        public double Weight { get; set; }
        public double SalesPrice { get; set; }
        public int Qty { get; set; }
        public string SerialNo { get; set; }
    }
    public class OutSourceRPTList
    {
        public string Lotno { get; set; }
        public string Receiver { get; set; }
        public string TYPE { get; set; }
        public string ReceiveDate { get; set; }
        public string RefallBarcode { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public string RefQty { get; set; }
        public string karatgrading { get; set; }
        public double caratsize { get; set; }
        public string SerialNo { get; set; }
        public double weight { get; set; }
        public double ALL_Cost { get; set; }

    }
    public class ReturnsRPTList
    {
        public string Lotno { get; set; }
        public string Receiver { get; set; }
        public string BranchName { get; set; }
        public string ReceiveDate { get; set; }
        public string RefallBarcode { get; set; }
        public string Price_desc { get; set; }
        public string RefItemcode { get; set; }
        public string Price_karat { get; set; }
        public double Price_carat { get; set; }
        public string SerialNo { get; set; }
        public double Price_weight { get; set; }
        public double ALL_price { get; set; }

    }
    public class OutSourceModels
    {
        public int getNewLot { get; set; }
        public int lastlot;
    }

    public class checkBarcodeIfExistModels
    {
        public bool exists { get; set; }
    }

    public class saveOutsourceReturnsModels
    {
        public string Dept { get; set; }
        public string status { get; set; }
        public string sortCode { get; set; }
    }
    public class saveReturnSelect_ASYS_ConsignHeaderModels
    {
        public int itemCode { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public double weight { get; set; }
        public int karat { get; set; }
        public double carat { get; set; }
        public double cost { get; set; }
        public string consignName { get; set; }
    }

    public class saveReturnEXEC_ASYS_Barcode_GeneratorModels
    {
        public string maxbarcode { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }

    public class saveReturnSelectTop1_bedrnr_bedrnmModels
    {
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }

    }

    public class saveReturnSelect_CustomerDetailsModels
    {
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }

    public class saveReturnSelect_bedrnr_bedrnmModels
    {
        public string branchName { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }

    public class saveReturnSelect_CustomerDetails2Models
    {
        public string branchName { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }

    public class saveReturnSelect_ItemDetailsModels
    {
        public List<string> allbarcode { get; set; }
        public List<int> itemCode { get; set; }
        public List<string> description { get; set; }
        public List<int> quantity { get; set; }
        public List<double> weight { get; set; }
        public List<int> karat { get; set; }
        public List<double> carat { get; set; }
        public List<double> price { get; set; }

    }

    public class saveOutSourceAddEditSelect_SelectReturn_ASYS_ConsignHeaderModels
    {
        public bool allreturns { get; set; }
    }

    public class saveOutSourceAddEditSelect_ASYS_REM_DetailModels
    {
        public bool allOutsource { get; set; }
    }

    public class saveOutSourceAddEditSelect_ASYS_REMOutSource_HeaderModels
    {
        public string receiver { get; set; }
        public string date { get; set; }
        public string branchCode { get; set; }
        public string branchName { get; set; }

        public List<string> allbarcode { get; set; }
        public List<int> itemCode { get; set; }
        public List<string> description { get; set; }
        public List<int> quantity { get; set; }
        public List<double> weight { get; set; }
        public List<int> karat { get; set; }
        public List<double> carat { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }


    }

    public class saveOutSourceReturnAddEdit_Select_ASYS_REM_DEtailModels
    {
        public string message { get; set; }
    }

    public class saveOutSourceReturnAddEdit_Select_ASYS_DISTRI_DetailModels
    {
        public string message { get; set; }
    }

    public class OutSourceAddEditReturn_Select_ASYS_Distri_DetailModels
    {
        public string priceDescription { get; set; }
        public string itemCode { get; set; }
        public string priceKarat { get; set; }
        public string priceCarat { get; set; }
        public string priceWeight { get; set; }
    }

    public class OutSourceSearch_BedryfModels
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }

    }

    public class OutSourceSearch_BedryfModels2
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }
    }

    public class OutSourceSearch_BedryfModels3
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }
    }

    public class OutSourceSearch_CustDetails_ASYS_CreateCustInfoModels
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }
    }

    public class OutSourceSearch_CustDetails_ASYS_CreateCustInfo2Models
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }
    }

    public class OutSourceSearch_CustDetails_ASYS_CreateCustInfo3Models
    {
        public List<string> customerCode { get; set; }
        public List<string> customerName { get; set; }
    }

    public class OutSourceDisplay_BedryfModels
    {
        public List<string> bedrnr { get; set; }
        public List<string> bedrnm { get; set; }
    }

    public class OutSourceDisplay_BedryfModels2
    {
        public List<string> custID { get; set; }
        public List<string> custName { get; set; }
    }

    public class Search_LotNumberModels
    {
        public List<string> employeeNames { get; set; }
        public List<string> lotNumbers { get; set; }
        public List<string> receivedDates { get; set; }
        public List<string> releasedDates { get; set; }

    }

    public class UnReceivedReportByBranchCodeModels
    {
        public string branchName { get; set; }
    }

    public class UnReceivedReportByBranchNameModels
    {
        public string branchName { get; set; }
        public string branchCode { get; set; }
    }

    public class OutSourceCostEditModels
    {
        public bool exists { get; set; }
    }
    public class getOutSourceCostEditModels
    {
        public string imagePath { get; set; }
        public string barcodestatus { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Action { get; set; }
        public string PTN { get; set; }
        public string ptnBarcode { get; set; }
        public string AppraisedValue { get; set; }
        public string LoanValue { get; set; }
        public string sortcode { get; set; }
        public string currency { get; set; }
        public string itemid { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public int qty { get; set; }
        public string karatgrading { get; set; }
        public double caratsize { get; set; }
        public double weight { get; set; }
        public double all_cost { get; set; }
        public string all_desc { get; set; }
        public string SerialNo { get; set; }
        public string ALL_Karat { get; set; }
        public double ALL_Carat { get; set; }
        public double ALL_Weight { get; set; }
        public double ALL_price { get; set; }
        public double cellular_cost { get; set; }
        public double watch_cost { get; set; }
        public double repair_cost { get; set; }
        public double cleaning_cost { get; set; }
        public double gold_cost { get; set; }
        public double mount_cost { get; set; }
        public double YG_cost { get; set; }
        public double WG_cost { get; set; }
        //public string ALL_cost2 { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }
    public class getREMCostEditModels
    {
        public string imagePath { get; set; }
        public string barcodestatus { get; set; }
        public string Action { get; set; }
        public string PTN { get; set; }
        public string AppraisedValue { get; set; }
        public string LoanValue { get; set; }
        public string sortcode { get; set; }
        public string currency { get; set; }
        public string itemid { get; set; }
        public string itemcode { get; set; }
        public string branchitemdesc { get; set; }
        public string qty { get; set; }
        public string karatgrading { get; set; }
        public string caratsize { get; set; }
        public string weight { get; set; }
        public string all_cost { get; set; }
        public string all_desc { get; set; }
        public string SerialNo { get; set; }
        public string ALL_Karat { get; set; }
        public string ALL_Carat { get; set; }
        public string ALL_Weight { get; set; }
        public string ALL_price { get; set; }
        public string cellular_cost { get; set; }
        public string watch_cost { get; set; }
        public string repair_cost { get; set; }
        public string cleaning_cost { get; set; }
        public string gold_cost { get; set; }
        public string mount_cost { get; set; }
        public string YG_cost { get; set; }
        public string WG_cost { get; set; }
        public string ALL_cost2 { get; set; }
        public string stat { get; set; }
        public string IsNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }
    public class getREMDataCostEdit2Models
    {
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string loanValue { get; set; }
        public string loanDate { get; set; }
        public string MaturityDate { get; set; }
        public string ExpiryDate { get; set; }
        public string ptnBarcode { get; set; }
        public string isNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }

    }
    public class getREMDataCostEditActionClassModels
    {
        public double costA { get; set; }
        public double costB { get; set; }
        public double costC { get; set; }
        public double costD { get; set; }

    }
    public class getREMDataCostEditCostDetailsModels
    {
        public string Cost1 { get; set; }
        public string Cost2 { get; set; }
        public string Cost3 { get; set; }
        public string Cost4 { get; set; }
        public string Cost5 { get; set; }
        public string isNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }
    public class saveCostingForCellularModels
    {
        public string Cost1 { get; set; }
        public string Cost2 { get; set; }
        public string Cost3 { get; set; }
        public string Cost4 { get; set; }
        public string isNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }
    public class selectWatchCostingModels
    {
        public string Cost1 { get; set; }
        public string Cost2 { get; set; }
        public string Cost3 { get; set; }
        public string Cost4 { get; set; }
        public string isNull(object odata)
        {
            if (Convert.IsDBNull(odata))
            {
                return "0";
            }
            else
            {
                return odata.ToString();
            }
        }
    }
    public class LoadRecodeActionTypeModels
    {
        public List<string> actionType { get; set; }
        public List<string> actionID { get; set; }
        public List<string> description { get; set; }
        public List<string> code { get; set; }
    }
    public class RecodeRetrieveInfoModels
    {
        public string st { get; set; }
        public string costID { get; set; }
        public string itemID { get; set; }
        public string photo { get; set; }
        public string status { get; set; }
        public string ptn { get; set; }
        public string reflotno { get; set; }
        public string actionClass { get; set; }
        public string sortCode { get; set; }
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
    public class RecodeRetrieveInfo2Models
    {
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string loanValue { get; set; }
    }
    public class RecodeRetrieveInfo3Models
    {
        public List<string> itemid { get; set; }
        public List<string> itemcode { get; set; }
        public List<string> branchitemdesc { get; set; }
        public List<string> qty { get; set; }
        public List<string> karatgrading { get; set; }
        public List<string> caratsize { get; set; }
        public List<string> weight { get; set; }
        public List<string> all_cost { get; set; }
        public List<string> price_desc { get; set; }
        public List<string> serialno { get; set; }
        public List<string> refqty { get; set; }
        public List<string> price_karat { get; set; }
        public List<string> price_carat { get; set; }
        public List<string> price_weight { get; set; }
        //public List<string> ALL_Cost { get; set; }
        public List<string> ALL_price { get; set; }
        public List<string> appraisevalue { get; set; }
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
    public class RecodeRetrieveInfo4Models
    {
        public string itemcode { get; set; }
        public string loanvalue { get; set; }
        public string itemid { get; set; }
        public string branchcode { get; set; }
        public string branchitemdesc { get; set; }
        public string qty { get; set; }
        public string karatgrading { get; set; }
        public string caratsize { get; set; }
        public string weight { get; set; }
        public string all_cost { get; set; }
        public string price_desc { get; set; }
        public string serialno { get; set; }
        public string refqty { get; set; }
        public string price_karat { get; set; }
        public string price_carat { get; set; }
        public string price_weight { get; set; }
        //public string ALL_Cost { get; set; }
        public string ALL_price { get; set; }
        public string appraisevalue { get; set; }
        public string photoname { get; set; }
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
    public class RecodeRetrieveInfo5Models
    {
        public string bedrnm { get; set; }
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
    public class DisplayActionClassModels
    {
        public string CostA { get; set; }
        public string CostB { get; set; }
        public string CostC { get; set; }
        public string CostD { get; set; }
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
    public class DisplayActionClass2Modesl
    {
        public string ALL_Wt { get; set; }
        public string ALL_Karat { get; set; }
        public string Gold_Cost { get; set; }
        public string Mount_Cost { get; set; }
        public string YG_Cost { get; set; }
        public string WG_Cost { get; set; }
        public string ALL_Cost { get; set; }
        public string Cellular_Cost { get; set; }
        public string Watch_Cost { get; set; }
        public string Repair_Cost { get; set; }
        public string Cleaning_Cost { get; set; }
        public string isNUll(object data)
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
    public class ExeMaxGenModels
    {
        public int barcode { get; set; }
        public string bcode;
        public long refallbarcode;
    }
    public class ReleasingActClassModels
    {
        public List<string> actionType { get; set; }
    }
    public class RefreshLotModels
    {
        public List<string> lotNumber { get; set; }
    }
    public class CountBarcodeModels
    {
        public string photoname { get; set; }
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
    public class DisplayToReleaseBarcodeModels
    {
        public List<string> ptn { get; set; }
        public List<string> refallbarcode { get; set; }
        public List<string> barcodeMid { get; set; }
        public List<string> all_desc { get; set; }
        public List<string> alL_weight { get; set; }
        public List<string> all_karat { get; set; }
        public List<string> all_carat { get; set; }
        public List<string> all_price { get; set; }
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
    public class DisplayToReleaseALLBarcodeModels
    {
        public string status { get; set; }
        public string Photoname { get; set; }
        public string ptn { get; set; }
        public string ALL_DESC { get; set; }
        public string ALL_weight { get; set; }
        public string ALL_karat { get; set; }
        public string all_carat { get; set; }
        public string all_price { get; set; }
        public string Price_desc { get; set; }
        public string Price_weight { get; set; }
        public string Price_karat { get; set; }
        public string Price_carat { get; set; }
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
    public class RPTLoadModels
    {
        public List<string> actionType { get; set; }
    }
    public class RPTCheckBranchCodeModels
    {
        public string bedrnm { get; set; }
    }
    public class ReleasingRPTLoadModels
    {
        public string reflotno { get; set; }
    }
    public class ReleasingRPTLoadEMPModels
    {
        public List<string> fullname { get; set; }
    }
    public class CheckBranchCodeREPORTModels
    {
        public string bedrnm { get; set; }
    }
    public class CheckBranchNameREPORTModels
    {
        public string bedrnr { get; set; }
    }
    public class RelLoadDataByYearModels
    {
        public List<string> ptn { get; set; }
        public List<string> refallbarcode { get; set; }
        public List<string> itemcode { get; set; }
        public List<string> all_desc { get; set; }
        public List<string> alL_weight { get; set; }
        public List<string> all_karat { get; set; }
        public List<string> all_carat { get; set; }
        public List<string> all_price { get; set; }
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
    public class LoadTradeInOrMissingItemModels
    {
        public List<string> PTN { get; set; }
        public List<string> refitemcode { get; set; }
        public List<string> all_desc { get; set; }
        public List<string> Refqty { get; set; }
        public List<string> all_karat { get; set; }
        public List<string> all_carat { get; set; }
        public List<string> sortcode { get; set; }
        public List<string> all_weight { get; set; }
        public List<string> loanvalue { get; set; }
        public List<string> all_cost { get; set; }
        public List<string> itemid { get; set; }
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
    public class comboChangeModels
    {
        public string res_id { get; set; }
    }
    public class MainLoadModels
    {
        public string job_title { get; set; }
    }
    public class OnLoadREMItemModels
    {
        public List<string> action_type { get; set; }
        public List<string> action_id { get; set; }
    }
    public class LoadGoldItemModels
    {
        public List<string> gold_karat { get; set; }
        public List<string> plain { get; set; }
        public List<string> mounted { get; set; }
        public List<string> id { get; set; }
        public string isNULL(object data)
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
    public class DiamondPriceItemModels
    {
        public List<string> color_Type { get; set; }
        public List<string> vs { get; set; }
        public List<string> si { get; set; }
        public List<string> i { get; set; }
        public List<string> carat_wt_from { get; set; }
        public List<string> carat_wt_to { get; set; }
        public List<string> id { get; set; }
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
    public class LoadPearlPriceItemModels
    {
        public List<string> pearl_type { get; set; }
        public List<string> verygood { get; set; }
        public List<string> good { get; set; }
        public List<string> poor { get; set; }
        public List<string> id { get; set; }
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
    public class PearlPriceItemModels
    {
        public List<string> colstone_type { get; set; }
        public List<string> colstoneverygood { get; set; }
        public List<string> colstonegood { get; set; }
        public List<string> colstonepoor { get; set; }
        public List<string> id { get; set; }
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
    public class REMCOStingModels
    {
        public double ALL_Weight { get; set; }
        public double ALL_Karat { get; set; }
        public double ALL_Cost { get; set; }
        public double Gold_Cost { get; set; }
        public double Mount_Cost { get; set; }
        public double YG_Cost { get; set; }
        public double WG_Cost { get; set; }
        public double Cellular_Cost { get; set; }
        public double Cleaning_Cost { get; set; }
        public double Repair_Cost { get; set; }
        public double Watch_Cost { get; set; }
    }

    public class Create_Brancha_And_Customer
    {

        public List<string> code { get; set; }
        public List<string> name { get; set; }
        public List<string> address { get; set; }
        public String resCode;
        public String resMsg;
        public String query;
    }
    public class _ClickInfo
    {
        public String branchCode;
        public String branchName;
        public String branchAddress;
        public String xmlCode;
        public String CustId;
        public String firstName;
        public String mi;
        public String resCode;
        public String resMsg;
        public String query;
        public String lastName;
        public String custAddress;


    }
}