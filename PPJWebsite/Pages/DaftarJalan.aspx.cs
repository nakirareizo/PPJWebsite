using PPJWebsite.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPJWebsite.Pages
{
    public partial class DaftarJalan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["RoadID"] != null)
                {
                    int RoadID = Convert.ToInt32(Request.QueryString["RoadID"]);
                    lblNoRujJalanVal.Text = RoadID.ToString();
                    lblNoRujJalan.Visible = true;
                    BindToControls(RoadID);
                }
                BindGridView();
            }
            if (lblNoRujJalanVal.Text != "")
                lblNoRujJalan.Visible = true;
        }

        private void BindGridView()
        {
            DataTable dt = JalanInfomation.getAllRoadInfo();
            gvRoadList.DataSource = dt;
            gvRoadList.DataBind();
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
                        int RoadID = Convert.ToInt32(gvRoadList.DataKeys[index].Value.ToString());
                        BindToControls(RoadID);
                        lblNoRujJalanVal.Text = RoadID.ToString();
                        lblNoRujJalan.Visible = true;
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

        private void BindToControls(int roadID)
        {
            DataTable dtJalan = JalanInfomation.getRoadByID(roadID);
            if (dtJalan != null)
            {
                txtNamaJalan.Text = dtJalan.Rows[0]["NamaJalan"].ToString();
                txtJumlahTiang.Text = dtJalan.Rows[0]["JumlahTiang"].ToString();
                txtJumTiangRosak.Text = dtJalan.Rows[0]["Rosak"].ToString();
                txtJumTiangTersedia.Text = dtJalan.Rows[0]["Tersedia"].ToString();
                txtKosSeunit.Text = dtJalan.Rows[0]["HargaKosSeunit"].ToString();
                txtHargaSeuinit.Text = dtJalan.Rows[0]["HargaSeunit"].ToString();
                txtJumArm.Text = dtJalan.Rows[0]["JumlahArm"].ToString();
                txtSaizGegantung.Text = dtJalan.Rows[0]["SaizGegantung"].ToString();
            }
        }

        protected void btnTamJalan_Click(object sender, EventArgs e)
        {
            JalanInfomation jalanInfo = new JalanInfomation();
            if (!string.IsNullOrEmpty(txtNamaJalan.Text))
                jalanInfo.NamaJalan = txtNamaJalan.Text.Trim();
            if (!string.IsNullOrEmpty(txtJumlahTiang.Text))
                jalanInfo.JumlahTiang = Convert.ToInt32(txtJumlahTiang.Text);
            if (!string.IsNullOrEmpty(txtJumTiangTersedia.Text))
                jalanInfo.Tersedia = Convert.ToInt32(txtJumTiangTersedia.Text);
            if (!string.IsNullOrEmpty(txtJumTiangRosak.Text))
                jalanInfo.Rosak = Convert.ToInt32(txtJumTiangRosak.Text);
            if (!string.IsNullOrEmpty(txtJumArm.Text))
                jalanInfo.JumlahArm = Convert.ToInt32(txtJumArm.Text);
            if (!string.IsNullOrEmpty(txtSaizGegantung.Text))
                jalanInfo.SaizGegantung = txtSaizGegantung.Text;
            if (!string.IsNullOrEmpty(txtHargaSeuinit.Text))
                jalanInfo.HargaSeunit = Convert.ToDouble(txtHargaSeuinit.Text.Trim());
            if (!string.IsNullOrEmpty(txtKosSeunit.Text))
                jalanInfo.HargaKosSeunit = Convert.ToDouble(txtKosSeunit.Text.Trim());
            JalanInfomation.InsertNewJalan(jalanInfo);
            BindGridView();
            string sScript0 = "window.alert('Jalan baru telah dimasukan dalam database.');";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Page_Load", sScript0, true);
        }

        protected void btnKemaskini_Click(object sender, EventArgs e)
        {
            JalanInfomation jalanInfo = new JalanInfomation();
            if (!string.IsNullOrEmpty(txtNamaJalan.Text))
                jalanInfo.NamaJalan = txtNamaJalan.Text.Trim();
            if (!string.IsNullOrEmpty(txtJumlahTiang.Text))
                jalanInfo.JumlahTiang = Convert.ToInt32(txtJumlahTiang.Text);
            if (!string.IsNullOrEmpty(txtJumTiangTersedia.Text))
                jalanInfo.Tersedia = Convert.ToInt32(txtJumTiangTersedia.Text);
            if (!string.IsNullOrEmpty(txtJumTiangRosak.Text))
                jalanInfo.Rosak = Convert.ToInt32(txtJumTiangRosak.Text);
            if (!string.IsNullOrEmpty(txtJumArm.Text))
                jalanInfo.JumlahArm = Convert.ToInt32(txtJumArm.Text);
            if (!string.IsNullOrEmpty(txtSaizGegantung.Text))
                jalanInfo.SaizGegantung = txtSaizGegantung.Text;
            if (!string.IsNullOrEmpty(txtHargaSeuinit.Text))
                jalanInfo.HargaSeunit = Convert.ToDouble(txtHargaSeuinit.Text.Trim());
            if (!string.IsNullOrEmpty(txtKosSeunit.Text))
                jalanInfo.HargaKosSeunit = Convert.ToDouble(txtKosSeunit.Text.Trim());
            int RoadID = 0;
            if (!string.IsNullOrEmpty(lblNoRujJalanVal.Text))
                RoadID = Convert.ToInt32(lblNoRujJalanVal.Text);
            JalanInfomation.UpdateJalan(jalanInfo, RoadID);
            BindGridView();
            string sScript0 = "window.alert('Jalan telah dikemaskini.');";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Page_Load", sScript0, true);
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            if (lblNoRujJalanVal.Text != null)
            {
                JalanInfomation.InactiveJalanByID(Convert.ToInt32(lblNoRujJalanVal.Text));
                BindGridView();
                ClearTextBox();
                string sScript0 = "window.alert('Jalan telah di batalkan.');";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Page_Load", sScript0, true);
            }
        }

        private void ClearTextBox()
        {
            txtNamaJalan.Text = "";
            txtJumlahTiang.Text = "";
            txtJumArm.Text = "";
            txtHargaSeuinit.Text = "";
            txtKosSeunit.Text = "";
            txtJumArm.Text = "";
            txtJumTiangRosak.Text = "";
            txtJumTiangTersedia.Text = "";
            txtSaizGegantung.Text = "";
        }
    }
}