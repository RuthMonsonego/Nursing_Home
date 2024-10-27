using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class ShiftsTable:GeneralTable
    {
        public ShiftsTable():base("Shift")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Shift(item));
            }
        }
        public List<Shift> GetList()
        {
            return base.list.ConvertAll(x => (Shift)x);
        }
        public int ThisCode()
        {
            return GetList().Last().CodeShift + 1;
        }
    }
}
