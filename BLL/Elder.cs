using Nursing_Home.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
     public  class Elder:GeneralRow
     {
        public string IdElder { get; set; }
        public string NameElder { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HospitalizationRoom { get; set; }
        public string IdDoctor { get; set; }
        public string PhoneContact { get; set; }
        public string NameContact { get; set; }
        public string ProximityContact { get; set; }
        public Elder():base()
        {

        }
        public Elder(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["IdElder"] = this.IdElder;
            row["NameElder"] = this.NameElder;
            row["DateOfBirth"] = this.DateOfBirth;
            row["HospitalizationRoom"] = this.HospitalizationRoom;
            row["IdDoctor"] = this.IdDoctor;
            row["PhoneContact"] = this.PhoneContact;
            row["NameContact"] = this.NameContact;
            row["ProximityContact"] = this.ProximityContact;
        }
        public override void FillFields()
        {
            this.IdElder = row["IdElder"].ToString();
            this.NameElder = row["NameElder"].ToString();
            this.DateOfBirth =Convert.ToDateTime( row["DateOfBirth"]);
            this.HospitalizationRoom = Convert.ToInt32(row["HospitalizationRoom"]);
            this.IdDoctor = row["IdDoctor"].ToString();
            this.PhoneContact = row["PhoneContact"].ToString();
            this.NameContact = row["NameContact"].ToString();
            this.ProximityContact = row["ProximityContact"].ToString();
        }
        public override string ToString()
        {
            return " ת.ז. הזקן: " + IdElder + " שם הזקן: " + NameElder + " תאריך לידה: " + DateOfBirth.ToShortDateString() + " חדר אישפוז: " + HospitalizationRoom + " קוד הרופא המטפל: " + IdDoctor + " שם איש הקשר: " + NameContact + " מספרו: " + PhoneContact + " קרבה: " + ProximityContact;
        }
    }
}
