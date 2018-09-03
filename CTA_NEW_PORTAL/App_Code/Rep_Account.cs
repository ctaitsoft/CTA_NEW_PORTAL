using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTA_NEW_PORTAL.EB
{
    public class Rep_Account
    {
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        //===========================================================================================================================================

        #region Account_Add

        public DataTable AccInfoTypeDetails(int userId, string accountType)
        {
            DataTable accInfoTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCode_AccInfo_Type", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@AccCategory", accountType));

                accInfoTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //    string errText = error.Replace("\'", "");

            //    string AlertMSG = "";
            //    AlertMSG += errText;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);

            //    _connection.Close();
            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return accInfoTable;
        }

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
                    ddlAdapter.SelectCommand.Parameters.AddWithValue("@AccCategory", param);
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

        public DataTable Acc_Save(int userId, string accName, string accName1, string accName2, string accName3, string accName4, byte accCategory, byte accTitle, byte accGender)
        {
            DataTable accountTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Account_Add", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccName", accName);
                cmd.Parameters.AddWithValue("@AccName1", accName1);
                cmd.Parameters.AddWithValue("@AccName2", accName2);
                cmd.Parameters.AddWithValue("@AccName3", accName3);
                cmd.Parameters.AddWithValue("@AccName4", accName4);
                cmd.Parameters.AddWithValue("@AccCategory", accCategory);
                cmd.Parameters.AddWithValue("@AccTitle", accTitle);
                cmd.Parameters.AddWithValue("@AccGender", accGender);
                cmd.Parameters.AddWithValue("@RtrnAccID", DBNull.Value);

                accountTable.Load(cmd.ExecuteReader());

                //cmd.ExecuteScalar();
            }
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //    string errText = error.Replace("\'", "");

            //    return accountTable;
            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return accountTable;
        }

        public DataTable Contact_Save(int userId, string conName, string conName1, string conName2, string conName3, string conName4, byte conCategory, byte conTitle, byte conGender, byte conType, int accId)
        {
            DataTable contactTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountContact_Add", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccName", conName);
                cmd.Parameters.AddWithValue("@AccName1", conName1);
                cmd.Parameters.AddWithValue("@AccName2", conName2);
                cmd.Parameters.AddWithValue("@AccName3", conName3);
                cmd.Parameters.AddWithValue("@AccName4", conName4);
                cmd.Parameters.AddWithValue("@AccCategory", conCategory);
                cmd.Parameters.AddWithValue("@AccTitle", conTitle);
                cmd.Parameters.AddWithValue("@AccGender", conGender);
                cmd.Parameters.AddWithValue("@ContactType", conType);
                cmd.Parameters.AddWithValue("@MainAccID", accId);

                contactTable.Load(cmd.ExecuteReader());

                //cmd.ExecuteScalar();
            }
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //    string errText = error.Replace("\'", "");

            //    return accountTable;
            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return contactTable;
        }

        public void Acc_InfoType_Save(int userId,string accId,string infoType, string infoData)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountInfo_Add", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccID", accId);
                cmd.Parameters.AddWithValue("@InfoType", infoType);
                cmd.Parameters.AddWithValue("@InfoData", infoData);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{
               
            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }  
        }

        public void Acc_Title_Save(int userId, byte accCategory, string accTitle)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCodeAdd_Acc_Title", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccCategory", accCategory);
                cmd.Parameters.AddWithValue("@Title", accTitle);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{

            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Contact_Type_Save(int userId, byte accCategory, string conType)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCodeAdd_AccContact_Type", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccCategory", accCategory);
                cmd.Parameters.AddWithValue("@ContactType", conType);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{

            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        #endregion

        //===========================================================================================================================================

        #region AccountUdt

        public DataTable SelectAcc(int userId, int accId)
        {
            DataTable accCrdTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Account", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@AccID", accId));

                accCrdTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return accCrdTable;
        }

        public DataTable SelectAccInfo(int userId, int accId)
        {
            DataTable accCrdTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountInfo", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@AccID", accId));

                accCrdTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return accCrdTable;
        }

        public string UpdateUdt(
            int userId,
            int accId,
            string accName,
            string accName1,
            string accName2,
            string accName3,
            string accName4,
            string accTitle,
            string accGender)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Account_Udt", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccID", accId);
                cmd.Parameters.AddWithValue("@AccName", accName);
                cmd.Parameters.AddWithValue("@AccName1", accName1);
                cmd.Parameters.AddWithValue("@AccName2", accName2);
                cmd.Parameters.AddWithValue("@AccName3", accName3);
                cmd.Parameters.AddWithValue("@AccName4", accName4);
                cmd.Parameters.AddWithValue("@AccTitle", accTitle);
                cmd.Parameters.AddWithValue("@AccGender", accGender);
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                return "Update Failed " + errText;
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();

                }
            }

            return "Success";

        }

        #endregion

        //===========================================================================================================================================

        #region Account_Search

        public DataTable SelectSearch(int userId, string searchType, string searchInfo, string moduleName)
        {
            DataTable CustomersDT = new DataTable();

            try
            {
                _connection.Open();
                SqlDataAdapter CustomersDA = new SqlDataAdapter("SP_Account_Search", _connection);
                CustomersDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                CustomersDA.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                CustomersDA.SelectCommand.Parameters.AddWithValue("@SearchType", searchType);
                CustomersDA.SelectCommand.Parameters.AddWithValue("@SearchInfo", searchInfo);
                CustomersDA.SelectCommand.Parameters.AddWithValue("@ModuleName", moduleName);

                CustomersDA.Fill(CustomersDT);
                _connection.Close();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return CustomersDT;
        }

        #endregion

        //===========================================================================================================================================

        #region Account_Crdt

        public DataTable SelectCrdt(int userId, int accId)
        {
            DataTable accCrdTable = new DataTable();

            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountCrdt", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@AccID", accId));

                accCrdTable.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            //catch (Exception ex)
            //{
            //    //string error = ex.Message;
            //    //string errText = error.Replace("\'", "");
            //    //return accCrdTable;
            //    _connection.Close();
            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return accCrdTable;
        }

        public string UpdateCrdt(
            int userId,
            int accId,
            int accCrdtNo,
            byte crdtCategory1,
            byte crdtCategory2,
            byte crdtType,
            double crdtMax,
            double crdtExtra,
            byte crdtStop,
            byte crdtResp,
            string crdtMemo)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountCrdt_Udt", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccID", accId);
                cmd.Parameters.AddWithValue("@AccCrdtNo", accCrdtNo);
                cmd.Parameters.AddWithValue("@AccCrdtGategory1", crdtCategory1);
                cmd.Parameters.AddWithValue("@AccCrdtGategory2", crdtCategory2);
                cmd.Parameters.AddWithValue("@AccCrdtType", crdtType);
                cmd.Parameters.AddWithValue("@AccCrdtMax", crdtMax);
                cmd.Parameters.AddWithValue("@AccCrdtExtra", crdtExtra);
                cmd.Parameters.AddWithValue("@AccCrdtStop", crdtStop);
                cmd.Parameters.AddWithValue("@AccCrdtResp", crdtResp);
                cmd.Parameters.AddWithValue("@AccCrdtMemo", crdtMemo);
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                return "Update Failed " + errText;
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();

                }
            }

            return "Success";

        }

        public void Crdt_Category1_Save(int userId, string category)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCodeAdd_AccCrdt_Category1", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CrdtCategory1", category);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{

            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Crdt_Category2_Save(int userId, string category)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCodeAdd_AccCrdt_Category2", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CrdtCategory2", category);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{

            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        #endregion

        //===========================================================================================================================================

        #region Account_Contact

        public DataTable SelectContactSearch(int userId, string accId)
        {
            DataTable contactsDt = new DataTable();

            try
            {
                _connection.Open();
                SqlDataAdapter customersDa = new SqlDataAdapter("SP_AccountContact_View", _connection);
                customersDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                customersDa.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                customersDa.SelectCommand.Parameters.AddWithValue("@AccID", accId);

                customersDa.Fill(contactsDt);
                _connection.Close();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return contactsDt;
        }

        public void Contact_Link(int userId, int mainAccId, int contactId, byte contactType)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountContact_Link", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@MainAccID", mainAccId);
                cmd.Parameters.AddWithValue("@ContactID", contactId);
                cmd.Parameters.AddWithValue("@ContactType", contactType);
                cmd.ExecuteScalar();
            }
            //catch (Exception e)
            //{

            //}

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        #endregion

        //===========================================================================================================================================

        #region AccountSales

        public void Acc_Currency_Save(int userId, string currency)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCodeAdd_All_Currency", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Currency", currency);
                cmd.ExecuteScalar();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Acc_Parts_Save(int userId, string accId, float parts)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountSales_SPDis", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccID", accId);
                cmd.Parameters.AddWithValue("@SPDis", parts);
                cmd.ExecuteScalar();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Acc_Service_Save(int userId, string accId, float service)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountSales_SCDis", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@AccID", accId);
                cmd.Parameters.AddWithValue("@SCDis", service);
                cmd.ExecuteScalar();
            }

            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        #endregion
    }
}