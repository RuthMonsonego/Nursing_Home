using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class Employee_Shifts_Table:GeneralTable
    {
        public Employee_Shifts_Table():base("Employee_Shifts")
        {
                foreach (DataRow item in base.table.Rows)
                {
                    list.Add(new Employee_Shifts(item));
                }
        }
       public List<Employee_Shifts> GetList()
       {
           return base.list.ConvertAll(x => (Employee_Shifts)x);
       }
        public int EmployeeShiftCode()
        {
            return GetList().Max(x=>x.CodeShiftEm)+1;
        }
    }
}
