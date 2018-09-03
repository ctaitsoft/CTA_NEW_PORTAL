using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using CTA_NEW_PORTAL.EB;
using System.Web.Script.Serialization;

namespace CTA_NEW_PORTAL.Services.Account
{
    /// <summary>
    /// Summary description for Srvc_CheckName
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Srvc_CheckName : System.Web.Services.WebService
    {

        [WebMethod]
        public void UserNameExist(string suario, string searchType, string accName)
        {
            DataTable accNameTable = new DataTable();
            string nameInUse = "0";

            Rep_Account accountSearch = new Rep_Account();

            //DataTable User = (DataTable)Session["user"];
            //userId = "4";
            accName = Regex.Replace(accName, "[ ]{2,}", " ");
            accNameTable = accountSearch.SelectSearch(int.Parse(suario), searchType, accName, "AccountSearch");

            if (accNameTable.Rows.Count > 0)
            {
                nameInUse = accNameTable.Rows[0]["AccID"].ToString();
            }
            else
            {
                nameInUse = "0";
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(nameInUse));
        }
    }
}
