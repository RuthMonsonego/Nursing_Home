using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nursing_Home.BLL;
using Nursing_Home.DAL;

namespace Nursing_Home.BLL
{
    public abstract class GeneralTable
    {
        private static Connect connect = new Connect();
        protected DataTable table;
        protected List<GeneralRow> list;
        public GeneralTable(string NameTable)
        {
            table = connect.GetTable(NameTable);
            list = new List<GeneralRow>();
        }
        public void AddItem(GeneralRow item)
        {
            list.Add(item);
            item.row = table.NewRow();
            item.FillDataRow();
            table.Rows.Add(item.row);
        }
        public void UpDataItem(GeneralRow item)
        {
            item.FillDataRow();
        }

        public void DeleteItem(GeneralRow item)
        {
            item.row.Delete();
            list.Remove(item);
        }
        public void SaveChanges()
        {
            connect.UpdateTable(this.table.TableName);
        }
    }
}
