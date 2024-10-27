using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    public class Medicines_Of_Elders_Table:GeneralTable
    {
        public Medicines_Of_Elders_Table():base("Medicines_Of_Elders")
        { 
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Medicines_Of_Elder(item));
            }
        }
        public List<Medicines_Of_Elder> GetList()
        {
            return base.list.ConvertAll(x => (Medicines_Of_Elder)x);
        }
        public int ThisCode()
        {
            return GetList().Max(x=>x.AutomaticCode) + 1;
        }
    }
}
