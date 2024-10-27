using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class EldersTable:GeneralTable
    {
        public EldersTable():base("Elder")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Elder(item));
            }
        }
        public List<Elder> GetList()
        {
            return base.list.ConvertAll(x => (Elder)x);
        }
    }
}
