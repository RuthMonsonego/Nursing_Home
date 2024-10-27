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
    public partial class AddMedicine : Form
    {
        FormStatus f;
        Medicine m;
        bool flag;
        public AddMedicine()
        {
            InitializeComponent();
            f = FormStatus.add;
            m = new Medicine();
            this.Text = "הוספת תרופה למאגר";
            comboBox1.DataSource = MyDB.types.GetList().Select(x => x.NameMedicineType).ToList();
        }
        public AddMedicine(Medicine m)
        {
            InitializeComponent();
            f = FormStatus.update;
            this.m = m;
            button1.Text = "שמור שינויים";
            this.Text = "עדכון פרטי התרופה";
            textBox1.Text = m.NameMedicine;
            comboBox1.SelectedItem = MyDB.types.GetList().First(d=>d.CodeMedicineType==m.CodeMedicineType).NameMedicineType.ToList();
            numericUpDown1.Value = m.TheIdealAmauntInStock;
            numericUpDown2.Value = m.UnitsInStock;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDitails();
            if (flag)
            {
                if (f == FormStatus.add)
                {
                    MyDB.medicines.AddItem(m);
                    MyDB.medicines.SaveChanges();
                    MessageBox.Show("התרופה נשמרה בהצלחה");
                    this.Close();
                }
                if (f == FormStatus.update)
                { 
                    MyDB.medicines.UpDataItem(m);
                    MyDB.medicines.SaveChanges();
                    MessageBox.Show("השינויים נשמרו בהצלחה!");
                    this.Close();
                }
            }
        }
        private void FillDitails()
        {
            errorProvider1.Clear();
            flag = true;
            if (f == FormStatus.add)
                m.CodeMedicine = MyDB.medicines.ThisCode();
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "לא הוכנס ערך");
                flag = false;
            }
            else if(textBox1.Text.Length==1)
            {
                flag = false;
                errorProvider1.SetError(textBox1,"הערך קצר מידי");
            }
            else
                m.NameMedicine = textBox1.Text;
            m.CodeMedicineType = MyDB.types.GetList().First(x => x.NameMedicineType == comboBox1.SelectedItem.ToString()).CodeMedicineType;
            if(numericUpDown1.Value==0)
            {
                flag = false;
                errorProvider1.SetError(numericUpDown1, "לא הוגדרה כמות");
            }
            else
                m.TheIdealAmauntInStock = Convert.ToInt32(numericUpDown1.Value);
            m.UnitsInStock = Convert.ToInt32(numericUpDown2.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
