using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    public    class Medicines_Of_Elder:GeneralRow
    {
        public int AutomaticCode { get; set; }
        public string IdElder { get; set; }
        public string IdDoctor { get; set; }
        public int CodeMedicine { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesADay { get; set; }
        public int NumberOfMilimetersAtADay { get; set; }
        public Medicines_Of_Elder():base()
        {

        }
        public Medicines_Of_Elder(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["AutomaticCode"] = this.AutomaticCode;
            row["IdElder"] = this.IdElder;
            row["IdDoctor"] = this.IdDoctor;
            row["CodeMedicine"] = this.CodeMedicine;
            row["StartDate"] = this.StartDate;
            row["EndDate"] = this.EndDate;
            row["TimesADay"] = this.TimesADay;
            row["NumberOfMilimetersAtADay"] = this.NumberOfMilimetersAtADay;
        }
        public override void FillFields()
        {
            this.AutomaticCode = Convert.ToInt32(row["AutomaticCode"]);
            this.IdElder= row["IdElder"].ToString();
            this.IdDoctor = row["IdDoctor"].ToString();
            this.CodeMedicine = Convert.ToInt32(row["CodeMedicine"]);
            this.StartDate = Convert.ToDateTime(row["StartDate"]);
            this.EndDate = Convert.ToDateTime(row["EndDate"]);
            this.TimesADay = Convert.ToInt32(row["TimesADay"]);
            this.NumberOfMilimetersAtADay = Convert.ToInt32(row["NumberOfMilimetersAtADay"]);
        }
        public string NameMedicine(int code)
        {
            Medicine m = MyDB.medicines.GetList().First(x => x.CodeMedicine == code);
            return m.NameMedicine;
        }
        public string NameElder(string id)
        {
            Elder e = MyDB.elders.GetList().First(x => x.IdElder == id);
            return e.NameElder;
        }
        public string NameType(int code)
        {
            Types_Of_Medicines t = MyDB.types.GetList().First(x => x.CodeMedicineType == code);
            return t.NameMedicineType;
        }
        public override string ToString()
        {
            return "ת.ז. זקן: " + IdElder + " ת.ז. של הרופא הרושם: " + IdDoctor + " קוד התרופה: " + CodeMedicine + " תאריך תחילת נטילתה: " + StartDate.ToShortDateString() + " תאריך סיום נטילתה +" + EndDate.ToShortDateString() + " מספר הפעמים ביום: " + TimesADay + " מספר המילימטרים בכל פעם: " + NumberOfMilimetersAtADay;
        }
    }
}