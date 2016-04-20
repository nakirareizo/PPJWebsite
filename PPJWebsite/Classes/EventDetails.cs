using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PPJWebsite.Classes
{
    public class EventDetails
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int TotalNumber { get; set; }

        internal static List<EventDetails> getAll(DataTable dtJalanBooked, DataTable jalanInfo, int Month, int Year)
        {
            List<EventDetails> eventList = new List<EventDetails>();
            int Available = Convert.ToInt32(jalanInfo.Rows[0]["Tersedia"].ToString());
            int daysInMonths = System.DateTime.DaysInMonth(Year, Month);
            int Total = 0;
            int OverallTotal = 0;
            for (int i = 1; i <= daysInMonths; i++)
            {
                Total = 0;
                EventDetails tempahan = new EventDetails();
                foreach (DataRow dr in dtJalanBooked.Rows)
                {
                    int StartDate = Convert.ToDateTime(dr["TarikhMula"].ToString()).Day;
                    int EndDate = Convert.ToDateTime(dr["TarikhTamat"].ToString()).Day;
                    int Booked = Convert.ToInt32(dr["JumlahTiangTempah"].ToString());
                    if (IsWithin(i, StartDate, EndDate))
                    {
                        Total += Booked;
                    }
                }
                if (Total == 0)
                {
                    OverallTotal = Available;
                    tempahan.Day = i;
                    tempahan.Month = Month;
                    tempahan.Year = Year;
                    tempahan.TotalNumber = OverallTotal;
                }
                else
                {
                    tempahan.Day = i;
                    tempahan.Month = Month;
                    tempahan.Year = Year;
                    OverallTotal = Available - Total;
                    tempahan.TotalNumber = OverallTotal;
                }

                eventList.Add(tempahan);
            }
            //ClearSession();
            return eventList;
        }

        internal static void getAvailableByIDAndDates(out List<EventDetails> eventList, DataTable dtJalanBooked, int Available, DateTime tarikhMula, DateTime tarikhTamat)
        {
            eventList = new List<EventDetails>();
        }

        private static bool IsWithin(int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

        private static void ClearSession()
        {
            HttpContext.Current.Session["JalanInfo"] = null;
            HttpContext.Current.Session["JalanBooked"] = null;
            HttpContext.Current.Session["SelectedYear"] = null;
            HttpContext.Current.Session["SelectedMonth"] = null;
        }

        public static IEnumerable<Tuple<string, int>> MonthsBetween(DateTime startDate, DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return Tuple.Create(
                    dateTimeFormat.GetMonthName(iterator.Month),
                    iterator.Year);
                iterator = iterator.AddMonths(1);
            }
        }

        public static List<Tuple<int, int>> year_month_Between(DateTime d0, DateTime d1)
        {
            List<DateTime> datemonth = Enumerable.Range(0, (d1.Year - d0.Year) * 12 + (d1.Month - d0.Month + 1))
                             .Select(m => new DateTime(d0.Year, d0.Month, 1).AddMonths(m)).ToList();
            List<Tuple<int, int>> yearmonth = new List<Tuple<int, int>>();

            foreach (DateTime x in datemonth)
            {
                yearmonth.Add(new Tuple<int, int>(x.Year, x.Month));
            }
            return yearmonth;
        }

        public static List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date.Date);
            }

            return allDates;
        }
    }
}