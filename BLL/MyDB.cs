using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    enum FormStatus { add, update };

    public static  class MyDB
    {
        public static DoctorsTable doctors = new DoctorsTable();
        public static EldersTable elders = new EldersTable();
        public static Employee_Shifts_Table shiftsemp = new Employee_Shifts_Table();
        public static Medicines_Of_Elders_Table medicineelder = new Medicines_Of_Elders_Table();
        public static MedicinesTable medicines = new MedicinesTable();
        public static NursesTable nurses = new NursesTable();
        public static ShiftsTable shifts = new ShiftsTable();
        public static Types_Of_Medicines_Table types = new Types_Of_Medicines_Table();
        public static Medicines_Take_Table medicinestake = new Medicines_Take_Table();
        public static string[] arrdays = new string[] { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" };
    }
}
