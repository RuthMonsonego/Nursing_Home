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
using Nursing_Home.GUI;

namespace Nursing_Home.GUI
{
    public partial class UCshift : UserControl
    {
        Nurse n1;
        List<Employee_Shifts> lstempsh;
        List<Shift> lstsh;
        Shift s;
        Employee_Shifts emp;
        int thisShift;
        int shift;
        int dayN;
        public UCshift(int i)
        {
            InitializeComponent();
            this.label2.Text = MyDB.arrdays[i-1];
            lstsh = new List<Shift>();
            lstsh = MyDB.shifts.GetList().Where(x => x.Nnurses > 0 && x.DayInWeek == i).ToList();
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tx = item as TextBox;
                    tx.Click += Messege_Click;
                }
            }
            s = null;
            while (lstsh.Count != 0)
            {
                foreach (var item in this.Controls)
                {
                    if (s != null)
                    {
                        lstsh.Remove(s);
                        s = null;
                    }
                    if (item is TextBox)
                    {
                        TextBox tx = item as TextBox;
                        //לחפש את התג של הטקסטבוקס הנוכחי- איזה מס' משמרת ביום
                        shift = 10 * i + Convert.ToInt32(tx.Tag);

                        //בדוק אם האח עובד ביום זה ובשעה זו
                        s = lstsh.FirstOrDefault(x => x.CodeShift == shift);
                        if (s != null)
                        {
                            //לצבוע את המשבצת
                            tx.BackColor = Color.Yellow;
                        }
                    }

                }
            }
        }

        //פעולה בונה המקבלת יום בשבוע ואח ומציגה משמרות שלו
        public UCshift(Nurse n, int d, string named)
        {
            InitializeComponent();
            lstempsh = new List<Employee_Shifts>();
            n1 = n;
            dayN = d;
            this.label2.Text = named;

            //רשימת משמרות לאח זה
            lstempsh = MyDB.shiftsemp.GetList().Where(x => x.CodeEmployee == n.IdNurse&& x.CodeShift/10==d).ToList();
            emp = null;
            while(lstempsh.Count!=0)
            {
                foreach (var item in this.Controls)
                {
                    if (emp != null)
                    {
                        lstempsh.Remove(emp);
                        emp = null;
                    }
                    if (item is TextBox)
                    {
                        TextBox tx = item as TextBox;
                        shift = d * 10 + Convert.ToInt32(tx.Tag);
                        //בדוק אם האח עובד ביום זה ובשעה זו
                        emp = lstempsh.FirstOrDefault(x => x.CodeShift == shift);
                        if (emp != null)
                        {
                            //לצבוע את המשבצת
                            tx.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        //פעולה המציגה את המשמרות שניתן עדין לשבץ בהם אחים
        //ומציגה כמה אחים חסרים בכל משמרת בשביל שיבוץ מקסימלי
        public UCshift(Nurse n, int d, string named, bool t)
        {
            InitializeComponent();
            n1 = n;
            dayN = d;
            this.label2.Text = named;
            lstsh = new List<Shift>();
            lstsh = MyDB.shifts.GetList().Where(x => x.Nnurses > 0 && x.DayInWeek == d).ToList();
            s = null;
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tx = item as TextBox;
                    tx.Click += Tx_Click;
                }
            }
            while (lstsh.Count!=0)
            {
                foreach (var item in this.Controls)
                {
                    if(s!=null)
                    {
                        lstsh.Remove(s);
                        s = null;
                    }
                    if (item is TextBox)
                    {
                        TextBox tx = item as TextBox;
                        //לחפש את התג של הטקסטבוקס הנוכחי- איזה מס' משמרת ביום
                        shift =10*d+ Convert.ToInt32(tx.Tag);

                        //בדוק אם האח עובד ביום זה ובשעה זו
                        s = lstsh.FirstOrDefault(x => x.CodeShift == shift);
                        if (s != null)
                        {
                            //לצבוע את המשבצת
                            tx.BackColor = Color.Yellow;
                            tx.Text = s.Nnurses.ToString();
                        }
                    }

                }
            }
        }

        private void Tx_Click(object sender, EventArgs e)
        {
            TextBox tx = sender as TextBox;
            s = new Shift();
            s = MyDB.shifts.GetList().First(x => x.CodeShift == 10 * dayN + Convert.ToInt32(tx.Tag));
            emp = new Employee_Shifts();
            thisShift = s.CodeShift;
            emp = MyDB.shiftsemp.GetList().FirstOrDefault(x => x.CodeEmployee == n1.IdNurse && x.CodeShift == s.CodeShift);
            if (emp != null)
            {
                DialogResult d = MessageBox.Show("אח זה רשום כבר במשמרת שנבחרה, האם ברצונך למחוק אותו משם", "שאלה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    MyDB.shiftsemp.DeleteItem(emp);
                    MyDB.shiftsemp.SaveChanges();
                    MessageBox.Show(n1.NameNurse + " הוסר/ה מהמשמרת שנבחרה");
                    s.Nnurses++;
                    MyDB.shifts.UpDataItem(s);
                    MyDB.shifts.SaveChanges();
                    tx.Text = s.Nnurses.ToString();
                    if (s.Nnurses!=0)
                        tx.BackColor = Color.Yellow;
                }
            }
            else if (s.Nnurses > 0)
            {
                int shift1, shift2;
                if (s.CodeShift % 10 == 2)
                {
                    shift1 = s.CodeShift - 1;
                    shift2 = s.CodeShift + 1;
                }
                else if (s.CodeShift % 10 == 1)
                {
                    if (s.CodeShift / 10 == 1)
                        shift1 = 73;
                    else
                        shift1 = s.CodeShift - 8;
                    shift2 = s.CodeShift + 1;
                }
                else
                {
                    shift1 = s.CodeShift - 1;
                    if (s.CodeShift / 10 == 7)
                        shift2 = 11;
                    else
                        shift2 = s.CodeShift + 8;
                }
                if (MyDB.shiftsemp.GetList().FirstOrDefault(x => x.CodeEmployee == n1.IdNurse && x.CodeShift == shift1) != null)
                {
                    MessageBox.Show("אח זה משובץ כבר במשמרת הקודמת לזו שנבחרה. שיבוץ בשתי משמרות רצופות אינו חוקי");
                }
                else if (MyDB.shiftsemp.GetList().FirstOrDefault(x => x.CodeEmployee == n1.IdNurse && x.CodeShift == shift2) != null)
                {
                    MessageBox.Show("אח זה משובץ כבר במשמרת שאחרי המשמרת שנבחרה. שיבוץ בשתי משמרות רצופות אינו חוקי");
                }
                else
                {
                    DialogResult d = MessageBox.Show("האם תרצה להוסיף את ?" + n1.NameNurse + " למשמרת שנבחרה", "שאלה", MessageBoxButtons.YesNo);
                    if (d == DialogResult.Yes)
                    {
                        AddShiftForNurs();
                        s.Nnurses--;
                        MyDB.shifts.UpDataItem(s);
                        MyDB.shifts.SaveChanges();
                        tx.Text = s.Nnurses.ToString();
                        if (s.Nnurses == 0)
                            tx.BackColor = Color.White;
                    }
                }
            }
            else
            {
                emp = MyDB.shiftsemp.GetList().FirstOrDefault(x => x.CodeEmployee == n1.IdNurse && x.CodeShift == s.CodeShift);
                if (emp == null)
                    MessageBox.Show("משמרת זו מלאה, אין אפשרות להוסיף אח נוסף למשמרת זו.");
            }
        }

        private void AddShiftForNurs()
        {
            emp = new Employee_Shifts();
            emp.CodeShiftEm = MyDB.shiftsemp.EmployeeShiftCode();
            emp.CodeEmployee = n1.IdNurse;
            emp.CodeShift = thisShift;

            MyDB.shiftsemp.AddItem(emp);
            MyDB.shiftsemp.SaveChanges();
            MessageBox.Show("המשמרת נוספה בהצלחה, תוכלו להוסיף עכשיו עוד משמרות או לחזור לטופס הקודם");
        }
        private void Messege_Click(object sender, EventArgs e)
        {
            int day=0, shift;
            for (int i=0; i<7; i++)
            {
                if (MyDB.arrdays[i] == label2.Text)
                {
                    day = i + 1;
                }
            }
            TextBox tx = sender as TextBox;
            shift = Convert.ToInt32(tx.Tag);
            MyShifts myShifts = new MyShifts(day * 10 + shift);
            myShifts.Show();
        }
    }
}
