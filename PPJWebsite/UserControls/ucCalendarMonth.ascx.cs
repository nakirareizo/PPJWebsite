using PPJWebsite.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PPJWebsite.UserControls
{
    public partial class ucCalendarMonth : System.Web.UI.UserControl
    {
        public DateTime SelectedDate { get; set; }
        public DateTime SelectedMonth { get; set; }
        private DateTime dDate = new DateTime();

        override protected void OnInit(EventArgs e)
        {
            if (Session["EventOverall"] != null && Session["CurrMonth"] != null)
            {
                int CurrMonth = (int)Session["CurrMonth"];
                Calendar1.SelectedDates.Clear();
                List<List<EventDetails>> EventList = (List<List<EventDetails>>)Session["EventOverall"];
                Calendar1.SelectedDate = new DateTime(EventList[0][0].Year, CurrMonth, EventList[0][0].Day);
                Calendar1.VisibleDate = new DateTime(EventList[0][0].Year, CurrMonth, EventList[0][0].Day);
                Calendar1.SelectedDayStyle.BackColor = System.Drawing.Color.White;
                //this.Calendar1.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar1_DayRender);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.ShowNextPrevMonth = false;
            }
        }

        private void FillHolidayDataset()
        {
            DateTime firstDate = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
        }

        private void Calendar1_DayRender(Object source, DayRenderEventArgs e)
        {
            //if (Session["EventOverall"] != null && Session["CurrMonth"] != null)
            //{
            int CurrMonth = (int)Session["CurrMonth"];
            List<List<EventDetails>> EventList = (List<List<EventDetails>>)Session["EventOverall"];
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
            //else
            //{
            for (int i = 0; i <= EventList[0].Count - 1; i++)
            {
                if (e.Day.Date.Day == EventList[0][i].Day && e.Day.Date.Month == CurrMonth)
                {
                    if (dates.Count > 0)
                    {
                        bool exists = dates.Exists(element => element == new DateTime(e.Day.Date.Year, e.Day.Date.Month, e.Day.Date.Day));
                        if (exists)
                        {
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[0][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                        }
                        else
                        {
                            e.Cell.BackColor = System.Drawing.Color.LightGray;
                            e.Cell.ForeColor = System.Drawing.Color.LightGray;
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:dimgrey; font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[0][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                        }
                    }
                    else
                    {
                        Label lbl = new Label();
                        lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[0][i].TotalNumber;
                        e.Cell.Controls.Add(lbl);
                    }
                }
            }
        }

        //}
    }
}