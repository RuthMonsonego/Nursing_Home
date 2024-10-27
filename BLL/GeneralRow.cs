
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Nursing_Home.BLL
{
        public abstract class GeneralRow
        {
            public DataRow row;
            public GeneralRow()
            {
            }
            public GeneralRow(DataRow row)
            {
                this.row = row;
                FillFields();
            }
        public void UpdateItem(GeneralRow item)
        {
            item.FillDataRow();
        }

        // פונקציה שמקבלת נתונים מהאקסס ושומרת אותם בתוך משתני המחלקה
        public abstract void FillFields();
            // פונקציה שממלאה את הנתונים בטבלה באקסס
            public abstract void FillDataRow();
        }
}
