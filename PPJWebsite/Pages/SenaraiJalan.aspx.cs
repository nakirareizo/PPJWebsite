using PPJWebsite.Classes;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace PPJWebsite.Pages
{
    public partial class SenaraiJalan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtJalanList = JalanInfomation.getAllRoadInfo();
                ddlJalan.DataValueField = "NoRujukanJalan";
                ddlJalan.DataTextField = "NamaJalan";
                ddlJalan.DataSource = dtJalanList;
                ddlJalan.DataBind();
                BindGridView();
            }
        }

        private void BindGridView()
        {
            DataTable dt = JalanInfomation.getAllRoadInfo();
            gvRoadList.DataSource = dt;
            gvRoadList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int RoadID = Convert.ToInt32(ddlJalan.SelectedValue);
            DataTable dtJalanList = JalanInfomation.getRoadByID(RoadID);
            gvRoadList.DataSource = dtJalanList;
            gvRoadList.DataBind();
            lblRecord.Text = dtJalanList.Rows.Count.ToString();
        }

        protected void gvRoadList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandSource.GetType().Name.ToString().ToUpper() == "LINKBUTTON")
                {
                    if (((LinkButton)e.CommandSource).ID.ToUpper() == "LBTNVIEW")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);     // Convert the row index stored in the CommandArgument to integer
                        GridViewRow row = gvRoadList.Rows[index];    // Retrieve the row that contains the button clicked by the user from the Rows collection.
                        string RoadID = gvRoadList.DataKeys[index].Value.ToString();
                        Response.Redirect("~/Pages/DaftarJalan.aspx?RoadID=" + RoadID);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvRoadList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the LinkButton control from the first column.
                LinkButton lbtnView = (LinkButton)e.Row.Cells[9].Controls[1];
                // Set the LinkButton's CommandArgument property with the row's index.
                lbtnView.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
}