using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nursing_Home.BLL;

namespace Nursing_Home.GUI
{
    public partial class MyShifts : Form
    {
       
        Nurse n;
        public MyShifts(int shift)
        {
            InitializeComponent();
            label1.Text = "מפת משמרות האחים";
            this.Text = "דף משמרות, מנהל";
            label5.Visible = true;
            label6.Visible = true;
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 7; i++)
            {
                UCshift shiftControl = new UCshift(i + 1);
                flowLayoutPanel1.Controls.Add(shiftControl);
            }
            groupBox1.Visible = true;
            string ss;
            if (shift % 10 == 1)
                ss = "בוקר";
            else if (shift % 10 == 2)
                ss = "צהוריים";
            else
                ss = "לילה";
            groupBox1.Text = "משמרת " + ss + " ביום " + MyDB.arrdays[shift / 10 - 1];
            Shift s = MyDB.shifts.GetList().First(x => x.CodeShift == shift);
            if (s.Nnurses != 0)
            {
                label7.Visible = true;
                label7.Text += s.Nnurses.ToString() + " אחים/ות";
            }
            List<Employee_Shifts> lst = new List<Employee_Shifts>();
            lst = MyDB.shiftsemp.GetList().Where(x => x.CodeShift == shift).ToList();
            if(lst.Count!=0)
            {
                label9.Visible = true;
                label9.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
            if (lst.Count != 0)
            {
                label10.Visible = true;
                label10.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
            if (lst.Count != 0)
            {
                label11.Visible = true;
                label11.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
            if (lst.Count != 0)
            {
                label12.Visible = true;
                label12.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
            if (lst.Count != 0)
            {
                label13.Visible = true;
                label13.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
            if (lst.Count != 0)
            {
                label14.Visible = true;
                label14.Text = MyDB.nurses.GetList().First(x => x.IdNurse == lst[0].CodeEmployee).NameNurse;
                lst.Remove(lst[0]);
            }
        }
        public MyShifts()
        {
            InitializeComponent();
            label1.Text = "מפת משמרות האחים";
            this.Text = "דף משמרות, מנהל";
            label5.Visible = true;
            label6.Visible = true;
            flowLayoutPanel1.Controls.Clear();
            label15.Visible = false;
            for (int i = 0; i < 7; i++)
            {
                UCshift shiftControl = new UCshift(i+1);
                flowLayoutPanel1.Controls.Add(shiftControl);
            }
        }
        public MyShifts(Nurse n)
        {
            InitializeComponent();
            this.n = new Nurse();
            this.n = n;
            label1.Text = "משמרות עבור: " + n.NameNurse;
            this.Text="דף המשמרות של "+n.NameNurse;
            ShowYoman();
            double sum = 0;
            foreach (var item in MyDB.shiftsemp.GetList().Where(x=>x.CodeEmployee==n.IdNurse))
            {
                sum += 8 * MyDB.shifts.GetList().First(y => y.CodeShift == item.CodeShift).PaymentForHour;
            }
            label15.Visible = true;
            label15.Text = "שכר לחודש(ארבעה שבועות) הוא " + sum+" שקלים";
        }
        public MyShifts(Nurse n, bool f)
        {
            InitializeComponent();
            this.n = new Nurse();
            button1.Visible = true;
            this.n = n;
            label1.Text = "משמרות אופציונליות עבור: " + n.NameNurse;
            this.Text = "דף המשמרות החסירות";
            AddShiftForNurs();
        }

        private void ShowYoman()
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 7; i++)
            {
                //להצגת משמרות שולח לפעולה בונה
                UCshift shiftControl = new UCshift(n,Convert.ToInt32(i+1), MyDB.arrdays[i]);
                flowLayoutPanel1.Controls.Add(shiftControl);
            }
        }

        private void AddShiftForNurs()
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 7; i++)
            {
                //שולח לפעולה בונה  להוספת משמרות
                UCshift shiftControl = new UCshift(n, i+1, MyDB.arrdays[i], true);
                flowLayoutPanel1.Controls.Add(shiftControl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
