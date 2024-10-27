using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
   public  class MedicinesTable:GeneralTable
    {
        public MedicinesTable() : base("Medicine")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Medicine(item));
            }
        }
        public List<Medicine> GetList()
        {
            return base.list.ConvertAll(x => (Medicine)x);
        }
        public int ThisCode()
        {
            return GetList().Last().CodeMedicine + 1;
        }
    }
}
