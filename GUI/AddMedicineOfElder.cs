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
    public partial class AddMedicineOfElder : Form
    {
        FormStatus f =new FormStatus();
        Medicines_Of_Elder med;
        bool flag = true;
        string d;
        string e;
        int donttouch = 3;

        public AddMedicineOfElder(Doctor d,Elder e)
        {
            InitializeComponent();
            f = FormStatus.add;
            comboBox1.DataSource = MyDB.types.GetList().Select(x => x.NameMedicineType).ToList();
            label2.Visible = false;
            comboBox2.Visible = false;
            med = new Medicines_Of_Elder();
            this.d = d.IdDoctor;
            this.e = e.IdElder;
            this.Text = "רישום תרופה למטופל " + e.NameElder;
        }
        public AddMedicineOfElder(Medicines_Of_Elder m)
        {
            InitializeComponent();
            f = FormStatus.update;
            med = m;
            d = m.IdDoctor;
            e = m.IdElder;
            button1.Text = "עדכן";
            donttouch = 1;
            comboBox1.DataSource = MyDB.types.GetList().Select(x => x.NameMedicineType).ToList();
            numericUpDown1.Value = m.TimesADay;
            textBox1.Text = m.NumberOfMilimetersAtADay.ToString();
            dateTimePicker1.Value = m.StartDate;
            string p= (med.EndDate.Date - med.StartDate.Date).ToString();
            string q = "";
            bool b = true;
            for (int i = 0; i <p.Length&&b; i++)
            {
                if (p[i] != '.')
                    q += p[i];
                else
                    b = false;
            }
            
            txtnumdays.Text =q;
            label2.Visible = true;
            comboBox2.Visible = true;
            comboBox2.SelectedItem = MyDB.medicines.GetList().First(z => z.CodeMedicine == m.CodeMedicine).NameMedicine;
            comboBox1.SelectedItem = MyDB.types.GetList().First(x => x.CodeMedicineType == (MyDB.medicines.GetList().First(y => y.CodeMedicine == m.CodeMedicine)).CodeMedicineType).NameMedicineType;
            this.Text = "עדכון תרופה למטופל " + MyDB.elders.GetList().First(x => x.IdElder == m.IdElder).NameElder;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (donttouch == 3)
            {
                Types_Of_Medicines t;
                t = MyDB.types.GetList().First(x => x.NameMedicineType == comboBox1.SelectedItem.ToString());
                comboBox2.Visible = true;
                comboBox2.DataSource = MyDB.medicines.GetList().Where(x => x.CodeMedicineType == t.CodeMedicineType).Select(x => x.NameMedicine).ToList();
                label2.Visible = true;
            }
            else
            {
                donttouch++;
                comboBox2.DataSource = MyDB.medicines.GetList().Where(x => x.CodeMedicineType == MyDB.medicines.GetList().First(z => z.CodeMedicine == med.CodeMedicine).CodeMedicineType).Select(x => x.NameMedicine).ToList();
                comboBox2.SelectedItem = MyDB.medicines.GetList().First(x => x.CodeMedicine == med.CodeMedicine).NameMedicine;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (f == FormStatus.add)
            {
                FillMedicineOfElder();
                if (flag)
                {
                    MyDB.medicineelder.AddItem(med);
                    MyDB.medicineelder.SaveChanges();
                    AddDaysForMedicine();
                    MessageBox.Show("רישום התרופה התבצע בהצלחה!");
                    this.Close();
                }
            }
            if(f==FormStatus.update)
            {
                FillMedicineOfElder();
                if (flag)
                {
                    foreach (var item in MyDB.medicinestake.GetList())
                    {
                        if(item.AutomaticCode==med.AutomaticCode)
                        {
                            MyDB.medicinestake.DeleteItem(item);
                            MyDB.medicinestake.SaveChanges();
                        }
                    }
                    MyDB.medicineelder.UpDataItem(med);
                    MyDB.medicineelder.SaveChanges();
                    AddDaysForMedicine();
                    MessageBox.Show("השינויים נשמרו בהצלחה!");
                    this.Close();
                }
            }
        }
        private void AddDaysForMedicine()
        {
            int numd =Convert.ToInt32(txtnumdays.Text);
            DateTime dt = med.StartDate;
            Medicine_Take mt = new Medicine_Take();
            mt.AutomaticCode = med.AutomaticCode;
            mt.Status = false;
            for (int i = 0; i < numd; i++)
            {
                mt.InDate = dt;
                if (med.TimesADay==1)
                {
                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 1;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                }
                if(med.TimesADay==2)
                {
                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 1;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 3;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                }
                if(med.TimesADay==3)
                {

                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 1;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 2;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                    mt.Code = MyDB.medicinestake.ThisCode();
                    mt.InShift = 3;
                    MyDB.medicinestake.AddItem(mt);
                    MyDB.medicinestake.SaveChanges();
                }
                dt=dt.AddDays(1.0);
            }
        }
        public void FillMedicineOfElder()
        {
            flag = true;
            errorProvider1.Clear();
            if(f==FormStatus.add)
               med.AutomaticCode = MyDB.medicineelder.ThisCode();
            med.CodeMedicine = MyDB.medicines.GetList().First(x => x.NameMedicine == comboBox2.SelectedItem.ToString()).CodeMedicine;
            med.IdElder = e;
            med.IdDoctor = d;
            if (dateTimePicker1.Value.Date < DateTime.Today)
            {
                errorProvider1.SetError(dateTimePicker1, "אין אפשרות לרשום תרופה לתאריך שהיה בעבר");
                flag = false;
            }
            else
            {
                med.StartDate = dateTimePicker1.Value;
            }
            if (txtnumdays.Text!="")
            {
                med.EndDate = dateTimePicker1.Value.AddDays(Convert.ToInt32(txtnumdays.Text));
            }
            else
            {
                errorProvider1.SetError(txtnumdays, "מספר הימים לנטילת התרופה לא הוגדר");
                flag = false;
            }
            med.TimesADay = Convert.ToInt32(numericUpDown1.Value);
            if (textBox1.Text != "")
            {
                med.NumberOfMilimetersAtADay = Convert.ToInt32(textBox1.Text);
            }
            else
            {
                errorProvider1.SetError(textBox1, "מספר המיליליטרים לא הוגדר");
                flag = false;
            }
        }

        private void txtnumdays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
