using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class DoctorsTable:GeneralTable
    {
        public DoctorsTable() : base("Doctor")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Doctor(item));
            }
        }
        public List<Doctor> GetList()
        {
            return base.list.ConvertAll(x => (Doctor)x);
        }
    }
}
