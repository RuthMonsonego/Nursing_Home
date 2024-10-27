using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
     public  class Types_Of_Medicines:GeneralRow
     {

        public int CodeMedicineType { get; set; }
        public string NameMedicineType { get; set; }
        public Types_Of_Medicines():base()
        {

        }
        public Types_Of_Medicines(DataRow row):base(row)
        {

        }

        public override void FillDataRow()
        {
            row["CodeMedicineType"] = this.CodeMedicineType;
            row["NameMedicineType"] = this.NameMedicineType;
        }
        public override void FillFields()
        {
            this.CodeMedicineType = Convert.ToInt32(row["CodeMedicineType"]);
            this.NameMedicineType = row["NameMedicineType"].ToString();
        }
        public override string ToString()
        {
            return "קוד סוג תרופה: " + CodeMedicineType + " הסוג: " + NameMedicineType;
        }
    }
}
