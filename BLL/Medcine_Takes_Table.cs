using Nursing_Home.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home
{
        public class Medicines_Take_Table : GeneralTable
        {
            public Medicines_Take_Table() : base("Medicines_Of_Elders")
            {
                foreach (DataRow item in base.table.Rows)
                {
                    list.Add(new Medicine_Take(item));
                }
            }
            public List<Medicine_Take> GetList()
            {
                return base.list.ConvertAll(x => (Medicine_Take)x);
            }
        }
}
