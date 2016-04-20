namespace PPJWebsite.Classes
{
    public class AppsCont
    {
        public const int JAN = 1;
        public const int FEB = 2;
        public const int MAR = 3;
        public const int APR = 4;
        public const int MAY = 5;
        public const int JUN = 6;
        public const int JUL = 7;
        public const int AUG = 8;
        public const int SEP = 9;
        public const int OCT = 10;
        public const int NOV = 11;
        public const int DEC = 12;

        internal static int getMonthValue(string Month)
        {
            int MonthValue = 0;
            switch (Month)
            {
                case "JAN":
                    MonthValue = AppsCont.JAN;
                    break;

                case "FEB":
                    MonthValue = AppsCont.FEB;
                    break;

                case "MAR":
                    MonthValue = AppsCont.MAR;
                    break;

                case "APR":
                    MonthValue = AppsCont.APR;
                    break;

                case "MAY":
                    MonthValue = AppsCont.MAY;
                    break;

                case "JUN":
                    MonthValue = AppsCont.JUN;
                    break;

                case "JUL":
                    MonthValue = AppsCont.JUL;
                    break;

                case "AUG":
                    MonthValue = AppsCont.AUG;
                    break;

                case "SEP":
                    MonthValue = AppsCont.SEP;
                    break;

                case "OCT":
                    MonthValue = AppsCont.OCT;
                    break;

                case "NOV":
                    MonthValue = AppsCont.NOV;
                    break;

                case "DEC":
                    MonthValue = AppsCont.DEC;
                    break;
            }
            return MonthValue;
        }

        internal static string getMonthDesc(int month)
        {
            string Month = "";
            switch (month)
            {
                case 1:
                    Month = "January";
                    break;
            }
            return Month;
        }
    }
}