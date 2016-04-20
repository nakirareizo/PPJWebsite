using System;
using System.Data;
using System.Linq;

namespace PPJWebsite.Classes
{
    public class JalanInfomation : JalanTempahInfo
    {
        public int No { get; set; }
        public int NoRujukanJalan { get; set; }
        public string NamaJalan { get; set; }
        public int? JumlahTiang { get; set; }
        public int? Rosak { get; set; }
        public int? Tersedia { get; set; }
        public int? JumlahArm { get; set; }
        public string SaizGegantung { get; set; }
        public double? HargaKosSeunit { get; set; }
        public double? HargaSeunit { get; set; }
        public bool Aktif { get; set; }

        internal static DataTable getAllRoadInfo()
        {
            DataTable output = getTableStructture();
            PPJDBDataContext PPJdb = new PPJDBDataContext();
            var jalanInfo = PPJdb.JalanInfos.Select(row => row).Where(rec => rec.Aktif == true);
            int counter = 0;
            foreach (var item in jalanInfo)
            {
                counter++;
                DataRow dr = output.NewRow();
                dr["No"] = counter;
                dr["NoRujukanJalan"] = item.NoRujukanJalan;
                dr["NamaJalan"] = item.NamaJalan;
                dr["JumlahTiang"] = item.JumlahTiang;
                dr["Rosak"] = item.Rosak;
                dr["Tersedia"] = item.Tersedia;
                dr["JumlahArm"] = item.JumlahArm;
                dr["SaizGegantung"] = item.SaizGegantung;
                dr["HargaKosSeunit"] = item.HargaKosSeunit;
                dr["HargaSeunit"] = item.HargaSeunit;
                output.Rows.Add(dr);
            }
            //output = null;
            return output;
        }

        internal static DataTable getRoadByID(int RoadID)
        {
            DataTable output = getTableStructture();
            //output.Columns.Add("NoRujukanJalan", typeof(int));
            //output.Columns.Add("NamaJalan", typeof(string));
            PPJDBDataContext PPJdb = new PPJDBDataContext();
            JalanInfo jalanInfo = PPJdb.JalanInfos.SingleOrDefault(rec => rec.NoRujukanJalan == RoadID);
            DataRow dr = output.NewRow();
            dr["No"] = 1;
            dr["NoRujukanJalan"] = jalanInfo.NoRujukanJalan;
            dr["NamaJalan"] = jalanInfo.NamaJalan;
            dr["JumlahTiang"] = jalanInfo.JumlahTiang;
            dr["Rosak"] = jalanInfo.Rosak;
            dr["Tersedia"] = jalanInfo.Tersedia;
            dr["JumlahArm"] = jalanInfo.JumlahArm;
            dr["SaizGegantung"] = jalanInfo.SaizGegantung;
            dr["HargaKosSeunit"] = jalanInfo.HargaKosSeunit;
            dr["HargaSeunit"] = jalanInfo.HargaSeunit;
            output.Rows.Add(dr);
            //output = null;
            return output;
        }

        internal static DataTable getBookedByIDAndDates(int jalanID, DateTime tarikhMula, DateTime tarikhTamat)
        {
            DataTable dt = JalanTempahInfo.getBookedByIDAndDates(jalanID, tarikhMula, tarikhTamat);
            return dt;
        }

        private static DataTable getTableStructture()
        {
            DataTable output = new DataTable();
            output.Columns.Add("No", typeof(string));
            output.Columns.Add("NoRujukanJalan", typeof(string));
            output.Columns.Add("NamaJalan", typeof(string));
            output.Columns.Add("JumlahTiang", typeof(string));
            output.Columns.Add("Rosak", typeof(string));
            output.Columns.Add("Tersedia", typeof(string));
            output.Columns.Add("JumlahArm", typeof(string));
            output.Columns.Add("SaizGegantung", typeof(string));
            output.Columns.Add("HargaKosSeunit", typeof(string));
            output.Columns.Add("HargaSeunit", typeof(string));
            return output;
        }

        internal static DataTable getBookedByIDAndMonth(string selectedRoad, string month, string year)
        {
            DataTable dt = JalanTempahInfo.getBookedByIDAndMonth(selectedRoad, month, year);
            return dt;
        }

        #region CRUD

        internal static void InsertNewJalan(JalanInfomation JalanInfo)
        {
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                JalanInfo jalanInfo = new JalanInfo();
                jalanInfo.Aktif = true;
                jalanInfo.HargaKosSeunit = JalanInfo.HargaKosSeunit;
                jalanInfo.JumlahTiang = JalanInfo.JumlahTiang;
                jalanInfo.HargaSeunit = JalanInfo.HargaSeunit;
                jalanInfo.NamaJalan = JalanInfo.NamaJalan;
                jalanInfo.JumlahArm = JalanInfo.JumlahArm;
                jalanInfo.Rosak = JalanInfo.Rosak;
                jalanInfo.Tersedia = JalanInfo.Tersedia;
                jalanInfo.SaizGegantung = JalanInfo.SaizGegantung;
                PPJdb.JalanInfos.InsertOnSubmit(jalanInfo);
                PPJdb.SubmitChanges();
            }
        }

        internal static int getAllTiangTersediaByDates(DateTime tarikhMula, DateTime tarikhTamat, string jalanID)
        {
            int AvailableCounted = 0;
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                var queryJalanTempah = PPJdb.JalanTempahs.Where(i => i.TarikhMula >= tarikhMula
                && i.TarikhTamat <= tarikhTamat && i.NoRujukanJalan == Convert.ToInt32(jalanID)).Select(rec=>rec.JumlahTiangTempah);
            }
            return AvailableCounted;
        }
        internal static void UpdateJalan(JalanInfomation JalanInfo, int RoadID)
        {
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                JalanInfo jalanInfo = PPJdb.JalanInfos.SingleOrDefault(rec => rec.NoRujukanJalan == RoadID);
                if (jalanInfo != null)
                {
                    jalanInfo.HargaKosSeunit = JalanInfo.HargaKosSeunit;
                    jalanInfo.JumlahTiang = JalanInfo.JumlahTiang;
                    jalanInfo.HargaSeunit = JalanInfo.HargaSeunit;
                    jalanInfo.NamaJalan = JalanInfo.NamaJalan;
                    jalanInfo.JumlahArm = JalanInfo.JumlahArm;
                    jalanInfo.Rosak = JalanInfo.Rosak;
                    jalanInfo.Tersedia = JalanInfo.Tersedia;
                    jalanInfo.SaizGegantung = JalanInfo.SaizGegantung;
                    PPJdb.SubmitChanges();
                }
            }
        }

        internal static void InactiveJalanByID(int RoadID)
        {
            using (PPJDBDataContext PPJdb = new PPJDBDataContext())
            {
                JalanInfo jalanInfo = PPJdb.JalanInfos.SingleOrDefault(rec => rec.NoRujukanJalan == RoadID);
                if (jalanInfo != null)
                {
                    jalanInfo.Aktif = false;
                    PPJdb.SubmitChanges();
                }
            }
        }

        #endregion CRUD
    }
}