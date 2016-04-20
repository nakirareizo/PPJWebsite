using System;

namespace PPJWebsite.Pages
{
    public partial class Landing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lnkbtnDaftarJalan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/DaftarJalan.aspx", true);
        }

        protected void lnkbtnJalanCarianTersedia_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/CarianJalanTersedia.aspx", true);
        }

        protected void lnkbtnTambahJalanTersedia_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/TambahJalanTersedia.aspx", true);
        }

        protected void lnkbtnSenaraiJalan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SenaraiJalan.aspx", true);
        }
    }
}