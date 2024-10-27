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
    public partial class AddElder : Form
    {
        FormStatus f;
        Elder el;
        bool flag = true;
        public AddElder()
        {
            InitializeComponent();
            f = FormStatus.add;
            comboBox1.DataSource = MyDB.doctors.GetList().Select(x => x.NameDoctor).ToList();
            el = new Elder();
            this.Text = "הוספת דייר למאגר";
        }
        public AddElder(Elder el)
        {
            InitializeComponent();
            f = FormStatus.update;
            comboBox1.DataSource = MyDB.doctors.GetList().Select(x => x.NameDoctor).ToList();
            button1.Text = "שמור שינויים";
            this.el = el;
            this.Text = "עדכון פרטי הדייר " + el.NameElder;
            textBox1.Text = el.IdElder;
            textBox2.Text = el.NameElder;
            comboBox1.SelectedItem = MyDB.doctors.GetList().First(x => x.IdDoctor == el.IdDoctor).NameDoctor;
            textBox3.Text = el.HospitalizationRoom.ToString();
            dateTimePicker1.Value = el.DateOfBirth;
            textBox4.Text = el.NameContact;
            textBox5.Text = el.PhoneContact;
            textBox6.Text = el.ProximityContact;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDitailies();
            if (f==FormStatus.add)
            {
                if (flag)
                {
                    MyDB.elders.AddItem(el);
                    MyDB.elders.SaveChanges();
                    MessageBox.Show("הדייר/ת התקבל/ה בהצלחה");
                    this.Close();
                }
            }
            if(f==FormStatus.update)
            {
                if (flag)
                {
                    MyDB.elders.UpDataItem(el);
                    MyDB.elders.SaveChanges();
                    MessageBox.Show("השינויים נשמרו בהצלחה!");
                    this.Close();
                }
            }
        }
        public void FillDitailies()
        {
            flag = true;
            errorProvider1.Clear();
            if (f == FormStatus.add)
            {
                if (textBox1.Text == "")
                {
                    flag = false;
                    errorProvider1.SetError(textBox1, "לא הוכנס ערך");
                }
                else
                {
                    Elder elder = MyDB.elders.GetList().FirstOrDefault(x => x.IdElder == textBox1.Text);
                    if (elder != null)
                    {
                        flag = false;
                        errorProvider1.SetError(textBox1, ".ת.ז. זו קימת כבר במאגר, אין אפשרות להכניס דייר נוסף עם מספר זהות זה,");
                    }
                    else if (Validation.CheckId(textBox1.Text))
                    {
                        el.IdElder = textBox1.Text;
                    }
                    else
                    {
                        errorProvider1.SetError(textBox1, "ת.ז. אינה תקינה");
                        flag = false;
                    }
                }
            }
            if (textBox2.Text == "")
            {
                flag = false;
                errorProvider1.SetError(textBox2, "לא הוכנס ערך");
            }
            else if(textBox2.Text.Length==1)
            {
                flag = false;
                errorProvider1.SetError(textBox2, "הערך שהוכנס קצר מידי");
            }
            else
            {
                el.NameElder = textBox2.Text;
            }
            if (dateTimePicker1.Value.Date >= DateTime.Today)
            {
                flag = false;
                errorProvider1.SetError(dateTimePicker1, "הערך אינו תקין");
            }
            else
            {
                el.DateOfBirth = dateTimePicker1.Value;
            }
            if (textBox3.Text == "")
            {
                flag = false;
                errorProvider1.SetError(textBox3, "לא הוכנס ערך");
            }
            else if (Convert.ToInt32(textBox3.Text) != el.HospitalizationRoom)
            {
                if (Convert.ToInt32(textBox3.Text) < 99 || Convert.ToInt32(textBox3.Text) > 400)
                {
                    errorProvider1.SetError(textBox3, "מספר החדר לא תקין");
                    flag = false;
                }
                else
                {
                    Elder e;
                    e = MyDB.elders.GetList().FirstOrDefault(x => x.HospitalizationRoom == Convert.ToInt32(textBox3.Text));
                    if (e != null)
                    {
                        errorProvider1.SetError(textBox3, "החדר מלא כבר, אנא בחר חדר אישפוז אחר");
                        flag = false;
                    }
                    else
                    {
                        el.HospitalizationRoom = Convert.ToInt32(textBox3.Text);
                    }
                }
            }
            el.IdDoctor = MyDB.doctors.GetList().First(x => x.NameDoctor == comboBox1.SelectedItem.ToString()).IdDoctor;
            if (textBox4.Text == "")
            {
                flag = false;
                errorProvider1.SetError(textBox4, "לא הוכנס ערך");
            }
            else if (textBox4.Text.Length==1)
            {
                errorProvider1.SetError(textBox4, "הערך שהוכנס קצר מידי");
                flag = false;
            }
            else
            {
                el.NameContact = textBox4.Text;
            }
            if (textBox5.Text == "")
            {
                flag = false;
                errorProvider1.SetError(textBox5, "לא הוכנס ערך");
            }
            else
            {
                if (Validation.IsPelepon(textBox5.Text) || Validation.IsTel(textBox5.Text))
                {
                    el.PhoneContact = textBox5.Text;
                }
                else
                {
                    errorProvider1.SetError(textBox5, "מספר הטלפון אינו תקין");
                    flag = false;
                }
            }
            if(textBox6.Text=="")
            {
                flag = false;
                errorProvider1.SetError(textBox6, "לא הוכנס ערך");
            }
            else
                el.ProximityContact = textBox6.Text;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //שם
            if (!Validation.IsHebrew(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == " ")
            {
                e.Handled = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //שם איש קשר
            if (!Validation.IsHebrew(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == " ")
            {
                e.Handled = false;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //קרבת איש קשר
            if (!Validation.IsHebrew(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == " ")
            {
                e.Handled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //תעודת זהות
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //מספר חדר
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //טלפון איש קשר
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
