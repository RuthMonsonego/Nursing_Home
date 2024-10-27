using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Nursing_Home.BLL;

namespace Nursing_Home.BLL
{
    public class Doctor: GeneralRow
    {
        public string IdDoctor { get; set; }
        public string NameDoctor { get; set; }
        public string LicenseNumber { get; set; }
        public string PoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Doctor():base()
        {

        }
        public Doctor(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["IdDoctor"] = this.IdDoctor;
            row["NameDoctor"] = this.NameDoctor;
            row["LicenseNumber"] = this.LicenseNumber;
            row["PoneNumber"] = this.PoneNumber;
            row["Address"] = this.Address;
            row["DateOfBirth"] = this.DateOfBirth;
        }
        public override void FillFields()
        {
            this.IdDoctor = row["IdDoctor"].ToString();
            this.NameDoctor = row["NameDoctor"].ToString();
            this.LicenseNumber = row["LicenseNumber"].ToString();
            this.PoneNumber = row["PoneNumber"].ToString();
            this.Address = row["Address"].ToString();
            this.DateOfBirth =Convert.ToDateTime( row["DateOfBirth"]);
        }
        public override string ToString()
        {
            return " ת.ז. של הרופא: " + IdDoctor + " שם הדוקטור: " + NameDoctor + " מספר רישיו רופא: " + LicenseNumber + " מספר הטלפון: " + PoneNumber + " כתובת:" + Address + " תאריך לידה: " + DateOfBirth.ToShortDateString();
        }
    }
}
