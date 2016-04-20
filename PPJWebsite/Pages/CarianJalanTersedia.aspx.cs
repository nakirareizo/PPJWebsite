using PPJWebsite.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPJWebsite.Pages
{
    public partial class CarianJalanTersedia : System.Web.UI.Page
    {
        private DataTable dtJalanInfo = new DataTable();
        private DataTable dtJalanBooked = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //calendar1.ShowOtherMonthsDays = false;
                Session["EventList"] = null;
                Session["UserControl"] = null;
                DataTable dtJalanList = JalanInfomation.getAllRoadInfo();
                ddlJalan.DataValueField = "NoRujukanJalan";
                ddlJalan.DataTextField = "NamaJalan";
                ddlJalan.DataSource = dtJalanList;
                ddlJalan.DataBind();
                gvRoadList.DataSource = null;
                gvRoadList.DataBind();
            }
            if (IsPostBack)
            {
                if (Session["UserControl"] != null)
                {
                    List<Control> UscList = CreateControls();
                    Session["UserControl"] = UscList;
                }
            }
        }

        private List<Control> CreateControls()
        {
            int counted = Convert.ToInt32(1);
            List<Control> UscList = new List<Control>();
            for (int i = 1; i <= counted; i++)
            {
                Control ctrl = LoadControl("../UserControls/ucCalendarMonth.ascx");
                //Label Title = (Label)ctrl.FindControl("lblMiddle");
                //if (i == 1)
                //    Title.Text = "";
                //else
                //    Title = "";
                //HiddenField hdSelectedDate = (HiddenField)ctrl.FindControl("hdSelectedDate");
                //if (i == 1)
                //    hdSelectedDate.Value = DateTime.Now.ToString();
                //else
                //    hdSelectedDate.Value = DateTime.Now.AddMonths(1).ToString();
                //ctrl.ID = i.ToString();
                //plc.Controls.Add(ctrl);
                //UscList.Add(ctrl);
            }
            Session["UserControl"] = UscList;
            return UscList;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["EventList"] = null;
            //Session["UserControl"] = null;
            //plc.Controls.Clear();
            string[] SelectedMonthYear = txtBulan.Text.Split(' ');
            string Month = SelectedMonthYear[0].ToString();
            string Year = SelectedMonthYear[1].ToString();
            string SelectedRoad = ddlJalan.SelectedValue;
            //BIND GRIDVIEW
            BindGridView(SelectedRoad);
            //BIND CALENDAR
            BindCalendar(Month, Year, SelectedRoad);
            //SetCalendar();
        }

        private void BindGridView(string SelectedRoad)
        {
            dtJalanInfo = JalanInfomation.getRoadByID(Convert.ToInt32(SelectedRoad));
            gvRoadList.DataSource = dtJalanInfo;
            gvRoadList.DataBind();
        }

        private void BindCalendar(string Month, string Year, string SelectedRoad)
        {
            dtJalanBooked = JalanInfomation.getBookedByIDAndMonth(SelectedRoad, Month, Year);
            int SelectedMonth = AppsCont.getMonthValue(Month.ToUpper());
            List<EventDetails> EventList = EventDetails.getAll(dtJalanBooked, dtJalanInfo, SelectedMonth, Convert.ToInt16(Year));
            Session["EventList"] = EventList;
            trCldr1.Visible = true;
            trCldr2.Visible = true;
            Calendar1.SelectedDates.Clear();
            Calendar1.SelectedDate = new DateTime(EventList[0].Year, EventList[0].Month, EventList[0].Day);
            Calendar1.VisibleDate = new DateTime(EventList[0].Year, EventList[0].Month, EventList[0].Day);
            Calendar1.SelectedDayStyle.BackColor = System.Drawing.Color.White;
            Calendar1.SelectedDayStyle.ForeColor = System.Drawing.Color.Black;
            this.Calendar1.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar1_DayRender);
        }

        private void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            List<EventDetails> EventList = (List<EventDetails>)Session["EventList"];
            e.Day.IsSelectable = false;
            if (e.Day.IsOtherMonth)
            {
                e.Cell.Controls.Clear();
                e.Cell.Text = string.Empty;
            }
            List<DateTime> dates = new List<DateTime>();
            if (Session["SelectedDates"] != null)
            {
                dates = (List<DateTime>)Session["SelectedDates"];
            }
            for (int i = 0; i <= EventList.Count - 1; i++)
            {
                if (e.Day.Date.Day == EventList[i].Day && e.Day.Date.Month == EventList[i].Month)
                {
                    Label lbl = new Label();
                    lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[i].TotalNumber;
                    e.Cell.Controls.Add(lbl);
                }
            }
        }

        private void SetCalendar()
        {
            int counted = Convert.ToInt32(1);
            List<Control> UscList = new List<Control>();
            for (int i = 1; i <= counted; i++)
            {
                Control ctrl = LoadControl("../UserControls/ucCalendarMonth.ascx");
                ctrl.ID = i.ToString();
                //plc.Controls.Add(ctrl);
                UscList.Add(ctrl);
            }
            Session["UserControl"] = UscList;
        }
    }
}