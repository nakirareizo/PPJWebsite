using PPJWebsite.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPJWebsite.Pages
{
    public partial class DaftarJalan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = JalanInfomation.getAllRoadInfo();
                gvRoadList.DataSource = dt;
                gvRoadList.DataBind();
            }
        }
    }
}