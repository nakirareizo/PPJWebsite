using PPJWebsite.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace PPJWebsite.UserControls
{
    public partial class CalendarDetails : System.Web.UI.UserControl
    {
        public DateTime SelectedDate { get; set; }
        public DateTime SelectedMonth { get; set; }
        private DateTime dDate = new DateTime();

        protected override void OnInit(EventArgs e)
        {
            #region "CALENDAR"

            if (Session["EventList"] != null)
            {
                List<EventDetails> EventList = (List<EventDetails>)Session["EventList"];
                if (Session["SelectedDates"] != null)
                {
                    SetAvailabeDisableCalendar(EventList);
                }
                else
                {
                    dDate = new DateTime(EventList[0].Year, EventList[0].Month, EventList[0].Day);
                    SelectedDate = dDate;
                    // Here you need to made the datastructure in such a way that you can bind the data in your datalist easily.
                    // So give concentration on database query to produce data like below:
                    // You can display each event, description if you can make the data row appropriately.

                    DataTable dtEvents = new DataTable("DailyEvents");
                    dtEvents.Columns.Add(new DataColumn("EventDate", System.Type.GetType("System.DateTime")));
                    dtEvents.Columns.Add(new DataColumn("AvailableBooked"));
                    DataTable datatable = new DataTable();
                    datatable.Columns.Add("Day"); //day contains day of the month
                    datatable.Columns.Add("Data"); //run time produce html & just place on it.
                    for (int i = 0; i <= EventList.Count - 1; i++)
                    {
                        //dtEvents.Rows.Add(dDate.AddDays(i), EventList[i].TotalNumber.ToString());
                        DateTime dCalendarDay = new DateTime(EventList[i].Year, EventList[i].Month, EventList[i].Day);
                        DataRow oRow = datatable.NewRow(); // here i am preparing data for a specific day.
                                                           // bworkingday method return: does the day is off or not.
                                                           // You can apply you business logic in this function to reurn data.
                                                           // by getEvents method i collect this day events from my all data.
                                                           // you can add control & event handler in runtime for more interactive calendar.
                        oRow["Day"] = EventList[i].Day.ToString();
                        if (!bWorkingDay(dCalendarDay))
                            oRow["Data"] = "<div style='color:Blue;font-size:15pt'>" + EventList[i].Day.ToString() + " <br/><div style='color:Blue;font-size:15pt'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                        else
                            oRow["Data"] = "<div style='color:Red;font-size:15pt;opacity:unset'>" + EventList[i].Day.ToString() + "<br/><div style='color:Red;font-size:15pt;opacity:unset'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                        datatable.Rows.Add(oRow);
                    }

                    dlCalendar.DataSource = datatable;
                    dlCalendar.DataBind();

                    #region ""TODAY BLOCK

                    // here just i am making current date block.
                    if (dDate.Year == DateTime.Now.Year && dDate.Month == DateTime.Now.Month)
                    {
                        foreach (DataListItem oItem in dlCalendar.Items)
                        {
                            if (oItem.ItemIndex == DateTime.Now.Day - 1)
                            {
                                oItem.BorderStyle = BorderStyle.Solid;
                                oItem.BorderColor = System.Drawing.Color.DeepSkyBlue; oItem.BorderWidth = 2;
                            }
                        }
                    }

                    #endregion ""TODAY BLOCK

                    Session["EventList"] = null;
                }
            }

            #endregion "CALENDAR"
        }

        private void SetAvailabeDisableCalendar(List<EventDetails> EventList)
        {
            List<DateTime> dates = (List<DateTime>)Session["SelectedDates"];
            //dDate = new DateTime(EventList[0].Year, EventList[0].Month, EventList[0].Day);
            DataTable dtEvents = new DataTable("DailyEvents");
            dtEvents.Columns.Add(new DataColumn("EventDate", System.Type.GetType("System.DateTime")));
            dtEvents.Columns.Add(new DataColumn("AvailableBooked"));
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Day"); //day contains day of the month
            datatable.Columns.Add("Data"); //run time produce html & just place on it.
            for (int i = 0; i <= EventList.Count - 1; i++)
            {
                DataRow oRow = datatable.NewRow();
                DateTime dCalendarDay = new DateTime(EventList[i].Year, EventList[i].Month, EventList[i].Day);
                SelectedDate = dCalendarDay;
                bool exists = dates.Exists(element => element == dCalendarDay);
                //Available
                if (exists)
                {
                    oRow["Day"] = EventList[i].Day.ToString();
                    if (!bWorkingDay(dCalendarDay))
                        oRow["Data"] = "<div style='color:Blue;font-size:15pt'>" + EventList[i].Day.ToString() + " <br/><div style='color:Blue;font-size:15pt'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                    else
                        oRow["Data"] = "<div style='color:Red;font-size:15pt;opacity:unset'>" + EventList[i].Day.ToString() + "<br/><div style='color:Red;font-size:15pt;opacity:unset'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                    datatable.Rows.Add(oRow);
                }
                //Grey-Out
                else
                {
                    oRow["Day"] = EventList[i].Day.ToString();
                    if (!bWorkingDay(dCalendarDay))
                        oRow["Data"] = "<div style='filter:alpha(opacity=50);opacity: 0.5;-moz-opacity:0.50;z-index: 20'>" + EventList[i].Day.ToString() + " <br/><div style='color:Blue;font-size:15pt'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                    else
                        oRow["Data"] = "<div style='filter:alpha(opacity=50);opacity: 0.5;-moz-opacity:0.50;z-index: 20'>" + EventList[i].Day.ToString() + "<br/><div style='color:Red;font-size:15pt;opacity:unset'>" + dCalendarDay.ToString("ddd") + " <br/><div style='color:Black;font-size:30pt'>" + EventList[i].TotalNumber.ToString() + "</div>";
                    datatable.Rows.Add(oRow);
                }
            }

            dlCalendar.DataSource = datatable;
            dlCalendar.DataBind();

            #region ""TODAY BLOCK

            // here just i am making current date block.
            if (dDate.Year == DateTime.Now.Year && dDate.Month == DateTime.Now.Month)
            {
                foreach (DataListItem oItem in dlCalendar.Items)
                {
                    if (oItem.ItemIndex == DateTime.Now.Day - 1)
                    {
                        oItem.BorderStyle = BorderStyle.Solid;
                        oItem.BorderColor = System.Drawing.Color.DeepSkyBlue; oItem.BorderWidth = 2;
                    }
                }
            }

            #endregion ""TODAY BLOCK

            Session["EventList"] = null;
            Session["SelectedDates"] = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void dlCalendar_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            // Just populationg my header section
            if (e.Item.ItemType == ListItemType.Header)
            {
                //((Label)e.Item.FindControl("lblLeft")).Text = "<a style=color:Black href=CarianJalanTersedia.aspx?SpecificMonth=" + dDate.AddMonths(-1).ToString("dd-MMMM-yyyy") + ">" + dDate.AddMonths(-1).ToString("MMMM yyyy") + "</a>";
                ((Label)e.Item.FindControl("lblMiddle")).Text = SelectedDate.ToString("MMMM yyyy");
                //((Label)e.Item.FindControl("lblRight")).Text = "<a style=color:Black href=CarianJalanTersedia.aspx?SpecificMonth=" + dDate.AddMonths(+1).ToString("dd-MMMM-yyyy") + ">" + dDate.AddMonths(+1).ToString("MMMM yyyy") + "</a>";
            }
        }

        public string getEvents(DateTime dDate, DataTable dTable)
        {
            string str = "<br/><div style='color:Black;font-size:30pt'>";
            foreach (DataRow oItem in dTable.Rows)
            {
                str = str + " " + oItem["AvailableBooked"];
            }
            return str;
        }

        public bool bWorkingDay(DateTime bDate)
        {
            // here i am assuming the sunday as holiday but you can make it more efficiently
            // by using a databse if you want to generate country based calendar.
            // In different country they have different holidays schedule.
            if (bDate.ToString("ddd") == "Sun" || bDate.ToString("ddd") == "Sat")
                return true;
            return false;
        }
    }
}