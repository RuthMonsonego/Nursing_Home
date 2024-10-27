using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Nursing_Home.BLL
{
    public class Types_Of_Medicines_Table : GeneralTable
    {
        public Types_Of_Medicines_Table() : base("Types_Of_Medicines")
        {
            foreach (DataRow item in base.table.Rows)
            {
                list.Add(new Types_Of_Medicines(item));
            }
        }
        public List<Types_Of_Medicines> GetList()
        {
            return base.list.ConvertAll(x => (Types_Of_Medicines)x);
        }
        public int ThisCode()
        {
            return GetList().Last().CodeMedicineType + 1;
        }
    }
}
