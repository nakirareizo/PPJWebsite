using PPJWebsite.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPJWebsite.Pages
{
    public partial class TambahJalanTersedia : System.Web.UI.Page
    {
        int Available = 0;
        DataTable dtJalanInfo = new DataTable();
        int SelectedRoad = 0;
        List<Control> UscList = new List<Control>();
        //public void Page_Init(object sender, EventArgs e)
        //{
        //        btnTempah.Click += new EventHandler(this.btnTempah_Click);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(IsPostBack)
            // {
            //     btnTempah.Click += new EventHandler(this.btnTempah_Click);
            // }
            if (!IsPostBack)
            {

                DataTable dtJalanList = JalanInfomation.getAllRoadInfo();
                ddlJalan.DataValueField = "NoRujukanJalan";
                ddlJalan.DataTextField = "NamaJalan";
                ddlJalan.DataSource = dtJalanList;
                ddlJalan.DataBind();
            }
        }
        private void GetAvailableDate(DateTime tarikhMula, DateTime tarikhTamat, int SelectedMonth, int SelectedYear, out List<EventDetails> EventDates)
        {
            EventDates = new List<EventDetails>();
            dtJalanInfo = JalanInfomation.getRoadByID(Convert.ToInt32(ddlJalan.SelectedValue));
            DataTable dtJalanBooked = JalanInfomation.getBookedByIDAndDates(SelectedRoad, tarikhMula, tarikhTamat);
            EventDates = EventDetails.getAll(dtJalanBooked, dtJalanInfo, SelectedMonth, SelectedYear);
            //Session["EventList"] = EventList;
        }
        private int getTotalMonths(DateTime tarikhMula, DateTime tarikhTamat)
        {
            int intReturn = 0;

            tarikhMula = tarikhMula.Date.AddDays(-(tarikhMula.Day - 1));
            tarikhTamat = tarikhTamat.Date.AddDays(-(tarikhTamat.Day - 1));

            while (tarikhTamat.Date > tarikhMula.Date)
            {
                intReturn++;
                tarikhMula = tarikhMula.AddMonths(1);
            }

            return intReturn;
        }
        protected void ddlJalan_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvRoadList.DataSource = null;
            gvRoadList.DataBind();
            SelectedRoad = Convert.ToInt32(ddlJalan.SelectedValue);
            //BIND GRIDVIEW
            BindGridView(SelectedRoad);
        }
        private void BindGridView(int SelectedRoad)
        {
            ClearParameters();
            dtJalanInfo = JalanInfomation.getRoadByID(SelectedRoad);
            gvRoadList.DataSource = dtJalanInfo;
            gvRoadList.DataBind();
            Available = Convert.ToInt32(dtJalanInfo.Rows[0]["Tersedia"].ToString());
            hdHargaSeunit.Value = dtJalanInfo.Rows[0]["HargaSeunit"].ToString();
        }
        private void ClearParameters()
        {
            hdHargaSeunit.Value = "";
            txtJumHarga.Text = "";
            txtJumTiang.Text = "";
        }
        protected void txtTarikhTamat_TextChanged(object sender, EventArgs e)
        {
            DateTime TarikhMula = Convert.ToDateTime(txtTarikhMula.Text);
            DateTime TarikhTamat = Convert.ToDateTime(txtTarikhTamat.Text);
            var months = EventDetails.year_month_Between(TarikhMula, TarikhTamat);
            if (months.Count <= 3)
            {
                SelectedRoad = Convert.ToInt32(ddlJalan.SelectedValue);
                SetCalendar(TarikhMula, TarikhTamat);
            }
            else
            {
                string sScript0 = "window.alert('Anda hanya dibenarkan memilih tarikh antara 3 bulan dari hari ni.');";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "SaveRecord", sScript0, true);
            }
        }
        private void SetCalendar(DateTime tarikhMula, DateTime tarikhTamat)
        {
            UscList = new List<Control>();
            DateTime OriTarikkhMula = tarikhMula;
            DateTime OriTarikhTamat = tarikhTamat;

            List<DateTime> dates = EventDetails.GetDatesBetween(OriTarikkhMula, OriTarikhTamat);
            Session["SelectedDates"] = dates;
            var months = EventDetails.year_month_Between(OriTarikkhMula, OriTarikhTamat);
            //var Years = new int[] { tarikhMula.Year, tarikhTamat.Year };
            //int MaxYear = Years.Max();
            //ArrayList DateList = new ArrayList();
            DataTable dtDates = new DataTable();
            dtDates.Columns.Add("TarikhMula", typeof(string));
            dtDates.Columns.Add("TarikhTamat", typeof(string));
            foreach (var item in months)
            {
                DataRow dr = dtDates.NewRow();
                dr[0] = new DateTime(item.Item1, item.Item2, 1);
                int Days = System.DateTime.DaysInMonth(item.Item1, item.Item2);
                dr[1] = new DateTime(item.Item1, item.Item2, Days);
                dtDates.Rows.Add(dr);
            }
            List<List<EventDetails>> EventOverall = new List<List<EventDetails>>();
            for (int i = 0; i <= dtDates.Rows.Count - 1; i++)
            {
                List<EventDetails> EventDates = new List<EventDetails>();
                var Years = new int[] { Convert.ToDateTime(dtDates.Rows[i][0]).Year, Convert.ToDateTime(dtDates.Rows[i][1]).Year };
                int MaxYear = Years.Max();
                int year = Convert.ToDateTime(dtDates.Rows[i][1]).Year;
                int month = Convert.ToDateTime(dtDates.Rows[i][0]).Month;
                GetAvailableDate(Convert.ToDateTime(dtDates.Rows[i][0]), Convert.ToDateTime(dtDates.Rows[i][1]), month, MaxYear, out EventDates);
                EventOverall.Add(EventDates);
                Session["EventOverall"] = EventOverall;
                if (i == 0)
                {
                    cldr1.Visible = true;
                    Calendar1.SelectedDates.Clear();
                    Calendar1.SelectedDate = new DateTime(EventOverall[0][0].Year, EventOverall[0][0].Month, EventOverall[0][0].Day);
                    Calendar1.VisibleDate = new DateTime(EventOverall[0][0].Year, EventOverall[0][0].Month, EventOverall[0][0].Day);
                    Calendar1.SelectedDayStyle.BackColor = System.Drawing.Color.White;
                    Calendar1.SelectedDayStyle.ForeColor = System.Drawing.Color.Black;
                    this.Calendar1.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar1_DayRender);
                }
                if (i == 1)
                {
                    cldr2.Visible = true;
                    Calendar2.SelectedDates.Clear();
                    Calendar2.SelectedDate = new DateTime(EventOverall[1][0].Year, EventOverall[1][0].Month, EventOverall[1][0].Day);
                    Calendar2.VisibleDate = new DateTime(EventOverall[1][0].Year, EventOverall[1][0].Month, EventOverall[1][0].Day);
                    Calendar2.SelectedDayStyle.BackColor = System.Drawing.Color.White;
                    Calendar2.SelectedDayStyle.ForeColor = System.Drawing.Color.Black;
                    this.Calendar2.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar2_DayRender);
                }
                if (i == 2)
                {
                    cldr3.Visible = true;
                    Calendar3.SelectedDates.Clear();
                    Calendar3.SelectedDate = new DateTime(EventOverall[2][0].Year, EventOverall[2][0].Month, EventOverall[2][0].Day);
                    Calendar3.VisibleDate = new DateTime(EventOverall[2][0].Year, EventOverall[2][0].Month, EventOverall[2][0].Day);
                    Calendar3.SelectedDayStyle.BackColor = System.Drawing.Color.White;
                    Calendar3.SelectedDayStyle.ForeColor = System.Drawing.Color.Black;
                    this.Calendar3.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar3_DayRender);
                }
            }
            if (UscList.Count > 0)
                trTitle.Visible = true;
        }
        protected void btnCarian_Click(object sender, EventArgs e)
        {
            Session["EventOverall"] = null;
            cldr1.Visible = false;
            cldr2.Visible = false;
            cldr3.Visible = false;
            if (txtTarikhTamat.Text != "")
            {
                //Session["EventList"] = null;
                Session["UserControl"] = null;
                //plc.Controls.Clear();
                DateTime TarikhMula = Convert.ToDateTime(txtTarikhMula.Text);
                DateTime TarikhTamat = Convert.ToDateTime(txtTarikhTamat.Text);
                var months = EventDetails.year_month_Between(TarikhMula, TarikhTamat);
                if (months.Count <= 3)
                {
                    SelectedRoad = Convert.ToInt32(ddlJalan.SelectedValue);
                    SetCalendar(TarikhMula, TarikhTamat);
                }
                else
                {
                    string sScript0 = "window.alert('Anda hanya dibenarkan memilih tarikh antara 3 bulan dari hari ni.');";
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "SaveRecord", sScript0, true);
                }
            }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
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
            for (int i = 0; i <= EventList[0].Count - 1; i++)
            {
                if (e.Day.Date.Day == EventList[0][i].Day && e.Day.Date.Month == EventList[0][i].Month)
                {
                    if (dates.Count > 0)
                    {
                        bool exists = dates.Exists(element => element == new DateTime(e.Day.Date.Year, e.Day.Date.Month, e.Day.Date.Day));
                        if (exists)
                        {
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[0][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                            int Counted = Convert.ToInt32(EventList[0][i].TotalNumber);
                            int hdCounted = 0;
                            if (hdAccumulateAvailableCounted.Value != "")
                            {
                                hdCounted = Convert.ToInt32(hdAccumulateAvailableCounted.Value);
                                hdAccumulateAvailableCounted.Value = (Counted + hdCounted).ToString();
                            }
                            else
                            {
                                hdAccumulateAvailableCounted.Value = Counted.ToString();
                            }
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
                }
            }
        }
        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
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
                if (e.Day.Date.Day == EventList[1][i].Day && e.Day.Date.Month == EventList[1][i].Month)
                {
                    if (dates.Count > 0)
                    {
                        bool exists = dates.Exists(element => element == new DateTime(e.Day.Date.Year, e.Day.Date.Month, e.Day.Date.Day));
                        if (exists)
                        {
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[1][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                            int Counted = Convert.ToInt32(EventList[1][i].TotalNumber);
                            int hdCounted = 0;
                            if (hdAccumulateAvailableCounted.Value != "")
                            {
                                hdCounted = Convert.ToInt32(hdAccumulateAvailableCounted.Value);
                                hdAccumulateAvailableCounted.Value = (Counted + hdCounted).ToString();
                            }
                        }
                        else
                        {
                            e.Cell.BackColor = System.Drawing.Color.LightGray;
                            e.Cell.ForeColor = System.Drawing.Color.LightGray;
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:dimgrey; font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[1][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                        }
                    }
                }
            }
        }
        protected void Calendar3_DayRender(object sender, DayRenderEventArgs e)
        {
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
                if (e.Day.Date.Day == EventList[2][i].Day && e.Day.Date.Month == EventList[2][i].Month)
                {
                    if (dates.Count > 0)
                    {
                        bool exists = dates.Exists(element => element == new DateTime(e.Day.Date.Year, e.Day.Date.Month, e.Day.Date.Day));
                        if (exists)
                        {
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:Black;font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[2][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                            int Counted = Convert.ToInt32(EventList[2][i].TotalNumber);
                            int hdCounted = 0;
                            if (hdAccumulateAvailableCounted.Value != "")
                            {
                                hdCounted = Convert.ToInt32(hdAccumulateAvailableCounted.Value);
                                hdAccumulateAvailableCounted.Value = (Counted + hdCounted).ToString();
                            }
                        }
                        else
                        {
                            e.Cell.BackColor = System.Drawing.Color.LightGray;
                            e.Cell.ForeColor = System.Drawing.Color.LightGray;
                            Label lbl = new Label();
                            lbl.Text = "<div style='color:dimgrey; font-size:20pt;height:30px;vertical-align:middle;text-align:center'>" + EventList[2][i].TotalNumber;
                            e.Cell.Controls.Add(lbl);
                        }
                    }
                }
            }
        }
        protected void btnAddRoad_Click(object sender, EventArgs e)
        {
            string NoRujukanTempah = "";
            DataTable jalan = JalanInfomation.getRoadByID(Convert.ToInt32(ddlJalan.SelectedValue));
            JalanTempahInfo tempah = new JalanTempahInfo();
            if (!string.IsNullOrEmpty(txtJumHarga.Text))
                tempah.Jumlah = Convert.ToDouble(txtJumHarga.Text);
            if (!string.IsNullOrEmpty(txtJumTiang.Text))
                tempah.JumlahTiangTempah = Convert.ToInt32(txtJumTiang.Text);
            if (!string.IsNullOrEmpty(txtTarikhMula.Text))
                tempah.TarikhMula = Convert.ToDateTime(txtTarikhMula.Text);
            if (!string.IsNullOrEmpty(txtTarikhTamat.Text))
                tempah.TarikhTamat = Convert.ToDateTime(txtTarikhTamat.Text);
            JalanTempahInfo.AddTempah(jalan, tempah, out NoRujukanTempah);
            Session["EventOverall"] = null;
            Session["SelectedDates"] = null;
            SetCalendar(tempah.TarikhMula, tempah.TarikhTamat);
            string sScript0 = "window.alert('Jalan telah ditambah.');";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Page_Load", sScript0, true);
        }
        protected void btnTempah_Click(object sender, EventArgs e)
        {
            DataTable jalan = JalanInfomation.getRoadByID(Convert.ToInt32(ddlJalan.SelectedValue));
            JalanTempahInfo tempah = new JalanTempahInfo();
            if (Convert.ToInt32(txtJumTiang.Text) <= Convert.ToInt32(hdAccumulateAvailableCounted.Value))
            {
                string NoRujukanTempah = "";
             
                if (!string.IsNullOrEmpty(jalan.Rows[0]["HargaSeunit"].ToString()))
                    tempah.HargaSeunit = Convert.ToDouble(jalan.Rows[0]["HargaSeunit"].ToString());
                if (!string.IsNullOrEmpty(txtJumHarga.Text))
                    tempah.Jumlah = Convert.ToDouble(txtJumHarga.Text);
                if (!string.IsNullOrEmpty(txtJumTiang.Text))
                    tempah.JumlahTiangTempah = Convert.ToInt32(txtJumTiang.Text);
                if (!string.IsNullOrEmpty(txtTarikhMula.Text))
                    tempah.TarikhMula = Convert.ToDateTime(txtTarikhMula.Text);
                if (!string.IsNullOrEmpty(txtTarikhTamat.Text))
                    tempah.TarikhTamat = Convert.ToDateTime(txtTarikhTamat.Text);
                JalanTempahInfo.AddTempah(jalan, tempah, out NoRujukanTempah);
                Session["EventOverall"] = null;
                Session["SelectedDates"] = null;
                SetCalendar(tempah.TarikhMula, tempah.TarikhTamat);
                string sScript0 = "window.alert('Jalan telah ditambah.');";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Page_Load", sScript0, true);
            }
            else
            {
                Session["EventOverall"] = null;
                Session["SelectedDates"] = null;
                SetCalendar(Convert.ToDateTime(txtTarikhMula.Text), Convert.ToDateTime(txtTarikhTamat.Text));
            }
        }
    }
}