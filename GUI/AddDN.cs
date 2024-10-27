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
    public partial class AddDN : Form
    {
        FormStatus f;
        bool isboctor;
        Nurse n;
        Doctor d;
        bool flag = true;
        public AddDN(Doctor d)
        {
            InitializeComponent();
            f = FormStatus.update;
            isboctor = true;
            this.d = d;
            label1.Text = "עדכון פרטי הרופא/ה " + d.NameDoctor;
            this.Text= " עדכון פרטי הרופא/ה" + d.NameDoctor;
            textBox1.Text = d.IdDoctor;
            textBox2.Text = d.NameDoctor;
            textBox3.Text = d.LicenseNumber;
            textBox4.Text = d.PoneNumber;
            textBox5.Text = d.Address;
            dateTimePicker1.Value = d.DateOfBirth;
        }
        public AddDN(Nurse n)
        {
            InitializeComponent();
            f = FormStatus.update;
            isboctor = false;
            this.n = n;
            label1.Text = "עדכון פרטי האח/ות " + n.NameNurse;
            this.Text = "עדכון פרטי האח/ות " + n.NameNurse;
            textBox3.Visible = false;
            label4.Visible = false;
            btnAddShift.Visible = true;
            textBox1.Text = n.IdNurse;
            textBox2.Text = n.NameNurse;
            textBox4.Text = n.PhoneNumber;
            textBox5.Text = n.Address;
            dateTimePicker1.Value = n.DateOfBirth;

           
        }
        public AddDN(bool isdoctor)
        {
            InitializeComponent();
            f = FormStatus.add;
            if (isdoctor)
            {
                label1.Text = "אנא הכנס את פרטי הרופא/ה";
                this.Text = "הוספת רופא חדש למאגר ";
                button1.Text = "לאישור פרטי הרופא/ה";
                this.isboctor = true;
                d = new Doctor();
            }
            else
            {
                label1.Text = "אנא הכנס את פרטי האח/ות";
                this.Text = "הוספת אח חדש למאגר ";
                button1.Text = "לאישור פרטי האח/ות";
                label4.Visible = false;
                textBox3.Visible = false;
                this.isboctor = false;
                n = new Nurse();
                btnAddShift.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            flag = true;
            FillDitails();
            if (flag)
            {
                if (f == FormStatus.add)
                {
                    if (isboctor)
                    {
                        MyDB.doctors.AddItem(d);
                        MyDB.doctors.SaveChanges();
                        MessageBox.Show("הרופא/ה התקבל/ה בהצלחה!");
                        this.Close();
                    }
                    if (!isboctor)
                    {
                        if (f == FormStatus.add)
                        {
                            MyDB.nurses.AddItem(n);
                            MyDB.nurses.SaveChanges();
                            MessageBox.Show("האח/ות התקבל/ה בהצלחה");
                        }
                        DialogResult r = MessageBox.Show("האם ברצונך להגדיר כעת משמרות עבור אח זה?","הוספת אח למאגר", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(r==DialogResult.Yes)
                        {
                            MyShifts frmshift = new MyShifts(n, true);
                            this.Hide();
                            frmshift.ShowDialog();
                            this.Show();
                            button1.Text = "לשמירת השינויים";
                            f = FormStatus.update;
                            btnAddShift.Visible = true;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
                else if (f == FormStatus.update)
                {
                    if (isboctor)
                    {
                        MyDB.doctors.UpDataItem(d);
                        MyDB.doctors.SaveChanges();
                        MessageBox.Show("השינויים נשמרו בהצלחה!");
                        this.Close();
                    }
                    if (!isboctor)
                    {
                        MyDB.nurses.UpDataItem(n);
                        MyDB.nurses.SaveChanges();
                        MessageBox.Show("השינויים נשמרו בהצלחה!");
                        this.Close();
                    }
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsHebrew(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if(e.KeyChar.ToString()==" ")
            {
                e.Handled = false;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void FillDitails()
        {
            errorProvider1.Clear();
            flag = true;
            if (isboctor)
            {
                if (f == FormStatus.add)
                {
                    if (textBox1.Text == "")
                    {
                        flag = false;
                        errorProvider1.SetError(textBox1, "לא הוכנס ערך");
                    }
                    else
                    {
                        Doctor doctor = MyDB.doctors.GetList().FirstOrDefault(x => x.IdDoctor == textBox1.Text);
                        if (doctor != null)
                        {
                            flag = false;
                            errorProvider1.SetError(textBox1, ".ת.ז. זו קימת כבר במאגר, אין אפשרות להכניס רופא נוסף עם מספר זהות זה,");
                        }
                        else if (!Validation.CheckId(textBox1.Text))
                        {
                            flag = false;
                            errorProvider1.SetError(textBox1, "תעודת הזהות אינה תקינה");
                        }
                        else
                        {
                            d.IdDoctor = textBox1.Text;
                        }
                    }
                }
                if (textBox2.Text == "")
                {
                    flag = false;
                    errorProvider1.SetError(textBox2, "לא הוכנס ערך");
                }
                else if(textBox1.Text.Length==1)
                {
                    flag = false;
                    errorProvider1.SetError(textBox2, "הערך קצר מידי");
                }
                else
                {
                    d.NameDoctor = textBox2.Text;
                }
                if(textBox3.Text=="")
                {
                    flag = false;
                    errorProvider1.SetError(textBox3, "לא הוכנס ערך");
                }
                else if (textBox3.Text.Length == 7)
                {
                    d.LicenseNumber = textBox3.Text;
                }
                else
                {
                    flag = false;
                    errorProvider1.SetError(textBox3, "מספר רישיון לא חוקי");
                }
                if(textBox4.Text=="")
                {
                    flag = false;
                    errorProvider1.SetError(textBox4, "לא הוכנס ערך");
                }
                else if (Validation.IsTel(textBox4.Text) || Validation.IsPelepon(textBox4.Text))
                {
                    d.PoneNumber = textBox4.Text;
                }
                else
                {
                    flag = false;
                    errorProvider1.SetError(textBox4, "מספר הטלפון לא תקין");
                }
                if (textBox5.Text == "")
                {
                    flag = false;
                    errorProvider1.SetError(textBox5, "לא הוכנס ערך");
                }
                else
                {
                    d.Address = textBox5.Text;
                }
                if (dateTimePicker1.Value.Date >= DateTime.Today)
                {
                    flag = false;
                    errorProvider1.SetError(dateTimePicker1, "הערך אינו תקין");
                }
                else
                {
                    d.DateOfBirth = dateTimePicker1.Value;
                }
            }
            if (!isboctor)
            {
                if (f == FormStatus.add)
                {
                    if (textBox1.Text == "")
                    {
                        flag = false;
                        errorProvider1.SetError(textBox1, "לא הוכנס ערך");
                    }
                    else
                    {
                        Nurse nurse = MyDB.nurses.GetList().FirstOrDefault(x => x.IdNurse == textBox1.Text);
                        if (nurse != null)
                        {
                            flag = false;
                            errorProvider1.SetError(textBox1, ".ת.ז. זו קימת כבר במאגר, אין אפשרות להכניס אח נוסף עם מספר זהות זה,");
                        }
                        else if (Validation.CheckId(textBox1.Text))
                        {
                            n.IdNurse = textBox1.Text;
                        }
                        else
                        {
                            flag = false;
                            errorProvider1.SetError(textBox1, "תעודת הזהות אינה תקינה");
                        }
                    }
                }
                if (textBox2.Text == "")
                {
                    flag = false;
                    errorProvider1.SetError(textBox2, "לא הוכנס ערך");
                }
                else
                {
                    n.NameNurse = textBox2.Text;
                }
                if(textBox4.Text=="")
                {
                    flag = false;
                    errorProvider1.SetError(textBox4, "לא הוכנס ערך");
                }
                else if (Validation.IsTel(textBox4.Text) || Validation.IsPelepon(textBox4.Text))
                {
                    n.PhoneNumber = textBox4.Text;
                }
                else
                {
                    flag = false;
                    errorProvider1.SetError(textBox4, "מספר הטלפון לא תקין");
                }
                if (textBox5.Text == "")
                {
                    flag = false;
                    errorProvider1.SetError(textBox5, "לא הוכנס ערך");
                }
                else
                {
                    n.Address = textBox5.Text;
                }
                if (dateTimePicker1.Value.Date >= DateTime.Today)
                {
                    flag = false;
                    errorProvider1.SetError(dateTimePicker1, "הערך אינו תקין");
                }
                else
                {
                    n.DateOfBirth = dateTimePicker1.Value;
                }
            }
        }

        private void btnAddShift_Click(object sender, EventArgs e)
        {
            MyShifts frmshift = new MyShifts(n, true);
            this.Hide();
            frmshift.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (Validation.IsHebrew(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = false;
            }
            if (e.KeyChar.ToString() == " ")
            {
                e.Handled = false;
            }
        }
    }
}
