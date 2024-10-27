using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    public  class Employee_Shifts:GeneralRow
    {
        public int CodeShiftEm { get; set; }
        public string CodeEmployee { get; set; }
        public int CodeShift { get; set; }

        public Employee_Shifts():base()
        {

        }
        public Employee_Shifts(DataRow row) : base(row)
        {

        }
        public override void FillDataRow()
        {
            row["CodeShiftEm"] = this.CodeShiftEm;
            row["CodeEmployee"] = this.CodeEmployee;
            row["CodeShift"] = this.CodeShift;

        }
        public override void FillFields()
        {
            this.CodeShiftEm = Convert.ToInt32(row["CodeShiftEm"]);
            this.CodeEmployee = row["CodeEmployee"].ToString();
            this.CodeShift = Convert.ToInt32(row["CodeShift"]);

        }
        public override string ToString()
        {
            return "קוד עובד: " + CodeEmployee + " קוד משמרת: " + CodeShift;
        }
    }
}