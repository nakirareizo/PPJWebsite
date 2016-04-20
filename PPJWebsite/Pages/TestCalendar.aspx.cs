using System;
using System.Collections.Generic;
using System.Web.UI;

namespace PPJWebsite.Pages
{
    public partial class TestCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int counted = Convert.ToInt32(1);
                List<Control> UscList = new List<Control>();
                for (int i = 1; i <= counted; i++)
                {
                    Control ctrl = LoadControl("../UserControls/ucCalendarMonth.ascx");
                    ctrl.ID = i.ToString();
                    plc.Controls.Add(ctrl);
                    UscList.Add(ctrl);
                }
                Session["UserControl"] = UscList;
            }
        }
    }
}