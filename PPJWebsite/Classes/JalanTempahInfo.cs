using System;
using System.Data;
using System.Linq;

namespace PPJWebsite.Classes
{
    public class JalanTempahInfo
    {
        public int No { get; set; }
        public int NoRujukanTempah { get; set; }
        public string NoRujukanPermohonan { get; set; }
        public int NoRujukanJalan { get; set; }
        public int JumlahTiangTempah { get; set; }
        public DateTime TarikhMula { get; set; }
        public DateTime TarikhTamat { get; set; }
        public double? HargaSeunit { get; set; }
        public double? Jumlah { get; set; }

        protected static DataTable getBookedByIDAndMonth(string selectedRoad, string month, string year)
        {
            int SelectedMonth = AppsCont.getMonthValue(month.ToUpper());
            DataTable dt = getTableStructure();
            PPJDBDataContext PPJdb = new PPJDBDataContext();
            //var jalanTempah= PPJdb.JalanTempahs.Select(row => row).Where(rec=>rec.NoRujukanJalan==Convert.ToInt32(selectedRoad)
            //&& Convert.ToDateTime(rec.TarikhMula).Month==Convert.ToInt32(month) && Convert.ToDateTime(rec.TarikhTamat).Month == Convert.ToInt32(month));
            var queryJalanTempah =
                   from a in PPJdb.GetTable<JalanTempah>()
                   where Convert.ToInt32(Convert.ToDateTime(a.TarikhMula).Month) == SelectedMonth &&
                   Convert.ToInt32(Convert.ToDateTime(a.TarikhTamat).Month) == SelectedMonth &&
                   a.NoRujukanJalan == Convert.ToInt32(selectedRoad)
                   select a;
            int Counter = 0;
            foreach (var c in queryJalanTempah)
            {
                DataRow dr = dt.NewRow();
                dr["No"] = Counter++;
                dr["NoRujukanTempah"] = c.NoRujukanTempah;
                dr["NoRujukanPermohonan"] = c.NoRujukanPermohonan;
                dr["NoRujukanJalan"] = c.NoRujukanJalan;
                dr["JumlahTiangTempah"] = c.JumlahTiangTempah;
                dr["TarikhMula"] = c.TarikhMula;
                dr["TarikhTamat"] = c.TarikhTamat;
                dr["HargaSeunit"] = c.HargaSeunit;
                dr["Jumlah"] = c.Jumlah;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private static DataTable getTableStructure()
        {
            DataTable output = new DataTable();
            output.Columns.Add("No", typeof(string));
            output.Columns.Add("NoRujukanTempah", typeof(string));
            output.Columns.Add("NoRujukanPermohonan", typeof(string));
            output.Columns.Add("NoRujukanJalan", typeof(string));
            output.Columns.Add("JumlahTiangTempah", typeof(string));
            output.Columns.Add("TarikhMula", typeof(string));
            output.Columns.Add("TarikhTamat", typeof(string));
            output.Columns.Add("HargaSeunit", typeof(string));
            output.Columns.Add("Jumlah", typeof(string));
            return output;
        }

        protected static DataTable getBookedByIDAndDates(int jalanID, DateTime tarikhMula, DateTime tarikhTamat)
        {
            DataTable dt = getTableStructure();
            PPJDBDataContext PPJdb = new PPJDBDataContext();
            //var queryJalanTempah = from s in PPJdb.JalanTempahs
            //                       where (s.TarikhMula >= tarikhMula && s.TarikhTamat <= tarikhTamat && s.NoRujukanJalan==jalanID)
            //                       select s;

            //var queryJalanTempah = (from t1 in PPJdb.JalanTempahs
            //                        where (tarikhMula >= t1.TarikhMula && tarikhTamat <= t1.TarikhTamat && t1.NoRujukanJalan==jalanID)
            //                        select t1);
            var queryJalanTempah = PPJdb.JalanTempahs.Where(i => i.TarikhMula >= tarikhMula && i.TarikhTamat <= tarikhTamat && i.NoRujukanJalan == jalanID)
                            .Select(row => row);

            int Counter = 0;
            foreach (var c in queryJalanTempah)
            {
                DataRow dr = dt.NewRow();
                dr["No"] = Counter++;
                dr["NoRujukanTempah"] = c.NoRujukanTempah;
                dr["NoRujukanPermohonan"] = c.NoRujukanPermohonan;
                dr["NoRujukanJalan"] = c.NoRujukanJalan;
                dr["JumlahTiangTempah"] = c.JumlahTiangTempah;
                dr["TarikhMula"] = c.TarikhMula;
                dr["TarikhTamat"] = c.TarikhTamat;
                dr["HargaSeunit"] = c.HargaSeunit;
                dr["Jumlah"] = c.Jumlah;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        internal static void AddTempah(DataTable jalan, JalanTempahInfo tempah, out string NoRujukanPermohonan)
        {
            NoRujukanPermohonan = getNoRujukanTempah();
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                JalanTempah tempahInfo = new JalanTempah();
                tempahInfo.NoRujukanPermohonan = NoRujukanPermohonan;
                tempahInfo.NoRujukanJalan = Convert.ToInt32(jalan.Rows[0]["NoRujukanJalan"].ToString());
                tempahInfo.JumlahTiangTempah = tempah.JumlahTiangTempah;
                tempahInfo.HargaSeunit = tempah.HargaSeunit;
                tempahInfo.Jumlah = tempah.Jumlah;
                tempahInfo.TarikhMula = tempah.TarikhMula;
                tempahInfo.TarikhTamat = tempah.TarikhTamat;
                tempahInfo.TarikhData = DateTime.Now;
                PPJdb.JalanTempahs.InsertOnSubmit(tempahInfo);
                PPJdb.SubmitChanges();
            }
        }

        private static string getNoRujukanTempah()
        {
            string NoRujukan = "";
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                //var NoRujukanQuery = (from t in PPJdb.JalanTempahs
                //              orderby t.TarikhData descending
                //              select t.NoRujukanPermohonan).First();
                //int LastDigit = 0;
                //int NewDigit = 0;
                //foreach (var item in NoRujukanQuery)
                //{
                //     LastDigit = Convert.ToInt32(item.ToString().Substring(item.ToString().Length - 1, 1));
                //     NewDigit = LastDigit + 1;
                //}

                //return NoRujukan = NoRujukan.Substring(0, NoRujukan.Length - 1) + NewDigit.ToString();
                var q = (from a in PPJdb.GetTable<JalanTempah>()
                         orderby a.NoRujukanTempah descending
                         select a).First();
                int LastDigit = 0;
                int NewDigit = 0;
                q.NoRujukanPermohonan = q.NoRujukanPermohonan.Replace("\r\n4", "");
                NoRujukan = q.NoRujukanPermohonan;
                string temp = q.NoRujukanPermohonan.Substring(q.NoRujukanPermohonan.Length - 1, 1);
                LastDigit = Convert.ToInt32(temp);
                NewDigit = LastDigit + 1;
                return NoRujukan = NoRujukan.Substring(0, NoRujukan.Length - 1) + NewDigit.ToString();
            }
        }
    }
}