using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{

    public class Nurse:GeneralRow
    {
        public string IdNurse { get; set; }
        public string NameNurse { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Nurse() : base() 
        {
        
        }
        public Nurse(DataRow row):base(row)
        {

        }
        public override void FillDataRow()
        {
            row["IdNurse"] = this.IdNurse;
            row["NameNurse"] = this.NameNurse;
            row["PhoneNumber"] = this.PhoneNumber;
            row["Address"] = this.Address;
            row["DateOfBirth"] = this.DateOfBirth;
        }
        public override void FillFields()
        {
            this.IdNurse = row["IdNurse"].ToString();
            this.NameNurse = row["NameNurse"].ToString();
            this.PhoneNumber = row["PhoneNumber"].ToString();
            this.Address = row["Address"].ToString();
            this.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
        }
        public override string ToString()
        {
            return " ת.ז. אחות:" + IdNurse + " שם האחות: " + " מספר טלפון: " + PhoneNumber + " כתובת: " + Address + "תאריך לידה: " + DateOfBirth.ToShortDateString();
        }
    }
}
