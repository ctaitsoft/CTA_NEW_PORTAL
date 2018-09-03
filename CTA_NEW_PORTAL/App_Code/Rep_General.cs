using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTA_NEW_PORTAL.EB
{
    public class Rep_General
    {
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        //=================================================================Change Password==========================================================================
        public string Password_Save(int userId, string oldPass, string newPass)
        {
            string passMsg = "";

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysUser_PassChange", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@OldPass", oldPass);
                cmd.Parameters.AddWithValue("@NewPass", newPass);

                cmd.Parameters.Add("@MSG", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                passMsg = cmd.Parameters["@MSG"].Value.ToString();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return passMsg;
        }

        //================================================================= Navegation Panel ==========================================================================

        public DataTable NavegationDetails(int userId, string menuHeader)
        {
            DataTable navInfoTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysRole_MenuSub", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@ModuleHeader", menuHeader));

                navInfoTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }


            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return navInfoTable;
        }

        //=================================================================Modules==========================================================================
        public string SelectModule(int userId, string moduleName)
        {
            DataTable moduleTable = new DataTable();

            try
            {
                _connection.Open();
                SqlDataAdapter moduleDA = new SqlDataAdapter("SP_SysRole_Module", _connection);
                moduleDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                moduleDA.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                moduleDA.SelectCommand.Parameters.AddWithValue("@ModuleName", moduleName);
                moduleDA.Fill(moduleTable);
                _connection.Close();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                return "Failed " + errText;
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();

                }
            }

            if (moduleTable.Rows.Count > 0)
            {
                return moduleTable.Rows[0]["ModulePath"].ToString();
            }
            else
            {
                return "Stop";
            }

        }

        //====================================================================Buttons list==================================================================
        public DataTable BtnsDataTable()
        {
            //Creating dummy datatable for testing
            DataTable btnTable = new DataTable();
            DataColumn dc = new DataColumn("Text", typeof(String));
            btnTable.Columns.Add(dc);

            dc = new DataColumn("Title", typeof(String));
            btnTable.Columns.Add(dc);

            //dc = new DataColumn("ID", typeof(String));
            //btnTable.Columns.Add(dc);

            DataRow dr1 = btnTable.NewRow();
            //dr1[0] = "B_11";
            dr1[0] = "<i class='fa fa-file-text-o' aria-hidden='true'></i>";
            dr1[1] = "Inquiry";
            //this will add the row at the end of the datatable
            btnTable.Rows.Add(dr1); //Inquiry(1)

            DataRow dr2 = btnTable.NewRow();
            //dr2[0] = "B_12";
            dr2[0] = "<i class='fa fa-eye' aria-hidden='true'></i>";
            dr2[1] = "View";

            btnTable.Rows.Add(dr2); //View(2)

            DataRow dr3 = btnTable.NewRow();
            //dr3[0] = "B_13";
            dr3[0] = "<i class='fa fa-print' aria-hidden='true'></i>";
            dr3[1] = "Print";

            btnTable.Rows.Add(dr3); //Print(3)

            DataRow dr4 = btnTable.NewRow();
            //dr4[0] = "B_21";
            dr4[0] = "<i class='fa fa-plus' aria-hidden='true'></i>";
            dr4[1] = "New";

            btnTable.Rows.Add(dr4); //New(4)

            DataRow dr5 = btnTable.NewRow();
            //dr5[0] = "B_22";
            dr5[0] = "<i class='fa fa-pencil' aria-hidden='true'></i>";
            dr5[1] = "Update";

            btnTable.Rows.Add(dr5); //Update(5)

            DataRow dr6 = btnTable.NewRow();
            //dr6[0] = "B_23";
            dr6[0] = "<i class='fa fa-trash-o' aria-hidden='true'></i>";
            dr6[1] = "Delete";

            btnTable.Rows.Add(dr6); //Cancel(6)

            DataRow dr7 = btnTable.NewRow();
            //dr7[0] = "B_29";
            dr7[0] = "<i class='fa fa-floppy-o' aria-hidden='true'></i>";
            dr7[1] = "Save";

            btnTable.Rows.Add(dr7); //Save(7)

            DataRow dr8 = btnTable.NewRow();
            //dr8[0] = "B_18";
            dr8[0] = "<i class='fa fa-refresh' aria-hidden='true'></i>";
            dr8[1] = "Refresh";

            btnTable.Rows.Add(dr8); //Refresh(8)

            DataRow dr9 = btnTable.NewRow();
            //dr9[0] = "B_19";
            dr9[0] = "<i class='fa fa-undo' aria-hidden='true'></i>";
            dr9[1] = "Clear";

            btnTable.Rows.Add(dr9); //Clear(10)

            DataRow dr10 = btnTable.NewRow();
            //dr10[0] = "B_31";
            dr10[0] = "<i class='fa fa-clipboard' aria-hidden='true'></i>&nbsp;&nbsp;Copy";
            dr10[1] = "Copy";

            btnTable.Rows.Add(dr10); //Copy(11)

            DataRow dr11 = btnTable.NewRow();
            //dr11[0] = "B_32";
            dr11[0] = "<i class='fa fa-clone' aria-hidden='true'></i>&nbsp;&nbsp;Paste";
            dr11[1] = "Paste";

            btnTable.Rows.Add(dr11); //Paste(12)

            DataRow dr12 = btnTable.NewRow();
            //dr12[0] = "B_41";
            dr12[0] = "<i class='fa fa-upload' aria-hidden='true'></i>&nbsp;&nbsp;Import";
            dr12[1] = "Import";

            btnTable.Rows.Add(dr12); //Import(13)

            DataRow dr13 = btnTable.NewRow();
            //dr13[0] = "B_42";
            dr13[0] = "<i class='fa fa-download' aria-hidden='true'></i>&nbsp;&nbsp;Export";
            dr13[1] = "Export";

            btnTable.Rows.Add(dr13); //Export(14)

            DataRow dr14 = btnTable.NewRow();
            //dr14[0] = "B_99";
            dr14[0] = "<i class='fa fa-star-o' aria-hidden='true'></i>&nbsp;&nbsp;Special Fields";
            dr14[1] = "Special Fields";

            btnTable.Rows.Add(dr14); //Special Fields(15)


            return btnTable;
        }

        //====================================================================Buttons list permission=======================================================
        public DataTable Permitted_buttons(int userId, string moduleName)
        {
            DataTable btnInfoTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysRole_Option", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@ModuleName", moduleName));

                btnInfoTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return btnInfoTable;
        }

        //====================================================================Drop Down lists Data==========================================================
        public DataTable SelectddlDataTable(int userId, string proceduer, string param)
        {
            DataTable ddlTable = new DataTable();

            try
            {
                SqlDataAdapter ddlAdapter = new SqlDataAdapter(proceduer, _connection);
                ddlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                ddlAdapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                if (param == "")
                { }
                else
                {
                    //ddlAdapter.SelectCommand.Parameters.AddWithValue("@paramName", param);
                }

                ddlAdapter.Fill(ddlTable);
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return ddlTable;
        }

        //====================================================================Query Strings Names===========================================================
        public string ObfuscateQueryString(string qString)
        {
            switch (qString)
            {
                case "accID":
                    return "ARxDmBzn1oQYUTxO";
                case "accNo":
                    return "NZdWB12fqrtyVNMz";
                case "view":
                    return "VyzX1rE";
            }

            return "";
        }

        //==================================================================== IDs Encryption & Decryption ==============================================================
        public int EncryptIds(int num)
        {
            int obfuscate = (num * 22 + 7) * 7;

            return obfuscate;
        }

        public int DecryptIds(int num)
        {
            int obfuscate = (num / 7 - 7) / 22;

            return obfuscate;
        }
    }
}