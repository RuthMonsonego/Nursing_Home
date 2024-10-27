using Nursing_Home.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nursing_Home.GUI
{
    public partial class MainPage : Form
    {
        Nurse n;
        Doctor d;

        public MainPage()
        {
            InitializeComponent();
            this.Text = "כניסה";
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            textBox1.Visible = true;
            label1.Visible = true;
            button5.Visible = false;
            button6.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            label2.Visible = true;
            textBox2.Visible = true;
            button4.Visible = false;
            button6.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Visible = true;
            button6.Visible = true;
            label3.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button5.Visible = false;
            button4.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            textBox3.Text = "";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "2010")
            {
                DirectorPage x = new DirectorPage();
                this.Hide();
                x.ShowDialog();
                this.Close();
            }
            else
            {
                textBox1.Text = "";
                errorProvider1.SetError(textBox1, "סיסמאת המנהל שגויה");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            d = MyDB.doctors.GetList().FirstOrDefault(x => x.IdDoctor == textBox2.Text);
            if (!Validation.CheckId(textBox2.Text))
            {
                textBox2.Text = "";
                errorProvider1.SetError(textBox2, "תעודת הזהות שגויה");
            }

            else if (d == null)
            {
                textBox2.Text = "";
                errorProvider1.SetError(textBox2, "תעודת זהות זו אינה מופיעה במאגר הרופאים, אנא פנה למנהל");
            }
            else
            {
                DoctorsPage dp = new DoctorsPage(d);
                this.Hide();
                dp.ShowDialog();
                this.Close();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = MyDB.nurses.GetList().FirstOrDefault(x => x.IdNurse == textBox3.Text);
            if (!Validation.CheckId(textBox3.Text))
            {
                textBox3.Text = "";
                errorProvider1.SetError(textBox3, "תעודת הזהות שגויה");
            }

            else if (n == null)
            {
                textBox3.Text = "";
                errorProvider1.SetError(textBox3, "תעודת זהות זו אינה מופיעה במאגר האחים, אנא פנה למנהל");
            }
            else
            {
                NursesPage np = new NursesPage(n);
                this.Hide();
                np.ShowDialog();
                this.Close();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.IsNum(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
