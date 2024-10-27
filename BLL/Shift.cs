using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    public  class Shift:GeneralRow
    {
        public int CodeShift { get; set; }
        public int NameShift { get; set; }
        public int DayInWeek { get; set; }
        public int PaymentForHour { get; set; }
        public DateTime BeginningTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Nnurses { get; set; }
        public Shift():base()
        {

        }
        public Shift(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["CodeShift"] = this.CodeShift;
            row["NameShift"] = this.NameShift;
            row["DayInWeek"] = this.DayInWeek;
            row["PaymentForHour"] = this.PaymentForHour;
            row["BeginningTime"] = this.BeginningTime;
            row["EndTime"] = this.EndTime;
            row["Nnurses"] = this.Nnurses;
        }
        public override void FillFields()
        {
            this.CodeShift = Convert.ToInt32(row["CodeShift"]);
            this.NameShift =Convert.ToInt32(row["NameShift"]);
            this.DayInWeek =Convert.ToInt32(row["DayInWeek"]);
            this.PaymentForHour = Convert.ToInt32(row["PaymentForHour"]);
            this.BeginningTime = Convert.ToDateTime(row["BeginningTime"]);
            this.EndTime = Convert.ToDateTime(row["EndTime"]);
            this.Nnurses = Convert.ToInt32(row["Nnurses"]);
        }
        public override string ToString()
        {
            return "משמרת: " + NameShift + " מתחילה בשעה: " + BeginningTime + " מסתיימת בשעה: " + EndTime+" מספר האחיות הנחוץ למשמרת:"+ Nnurses;
        }
    }
}
