using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
     public  class Medicine:GeneralRow
    {
        public int CodeMedicine { get; set; }
        public string NameMedicine { get; set; }
        public int CodeMedicineType { get; set; }
        public int TheIdealAmauntInStock { get; set; }
        public int UnitsInStock { get; set; }
        public Medicine():base()
        {

        }
        public Medicine(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["CodeMedicine"] = this.CodeMedicine;
            row["NameMedicine"] = this.NameMedicine;
            row["CodeMedicineType"] = this.CodeMedicineType;
            row["TheIdealAmauntInStock"] = this.TheIdealAmauntInStock;
            row["UnitsInStock"] = this.UnitsInStock;
        }
        public override void FillFields()
        {
            this.CodeMedicine = Convert.ToInt32(row["CodeMedicine"]);
            this.NameMedicine = (row["NameMedicine"]).ToString();
            this.CodeMedicineType = Convert.ToInt32(row["CodeMedicineType"]);
            this.TheIdealAmauntInStock = Convert.ToInt32(row["TheIdealAmauntInStock"]);
            this.UnitsInStock = Convert.ToInt32(row["UnitsInStock"]);
        }
        public override string ToString()
        {
            return "קוד התרופה: " + CodeMedicine + " שם התרופה: " + NameMedicine + " קוד סוג התרופה: " + CodeMedicineType + " הכמות האידיאלית במלאי: " + TheIdealAmauntInStock + " הכמות הנוכחית במלאי: " + UnitsInStock;
        }
    }
}
