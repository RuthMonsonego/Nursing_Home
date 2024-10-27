using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Nursing_Home.BLL;


namespace Nursing_Home
{
    public class Medicine_Take : GeneralRow
    {
        public int Code { get; set; }
        public int AutomaticCode { get; set; }
        public DateTime InDate { get; set; }
        public int InShift { get; set; }
        public bool Status { get; set; }
        public Medicine_Take() : base()
        {

        }
        public Medicine_Take(DataRow row) : base(row)
        {

        }
        public override void FillDataRow()
        {
            row["Code"] = this.Code;
            row["AutomaticCode"] = this.AutomaticCode;
            row["InDate"] = this.InDate;
            row["InShift"] = this.InShift;
            row["Status"] = this.Status;
        }
        public override void FillFields()
        {
            this.Code = Convert.ToInt32(row["Code"]);
            this.AutomaticCode =Convert.ToInt32(row["AutomaticCode"]);
            this.InDate =Convert.ToDateTime(row["InDate"]);
            this.InShift =Convert.ToInt32(row["InShift"]);
            this.Status = Convert.ToBoolean(row["Status"]);
        }
        public override string ToString()
        {
            return "קוד נטילת תרופה: " + Code + "תאריך: " + InDate + "משמרת: " + InShift + "האם ניתן? " + Status;
        }
    }
}
