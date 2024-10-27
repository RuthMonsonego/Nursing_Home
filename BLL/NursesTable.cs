using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class NursesTable:GeneralTable
    {
        public NursesTable():base("Nurse")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Nurse(item));
            }
        }
        public List<Nurse> GetList()
        {
            return base.list.ConvertAll(x => (Nurse)x);
        }
    }
}
