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
    public partial class UpdateOrderingMedicine : Form
    {
        Medicine m;
        public UpdateOrderingMedicine(Medicine m)
        {
            InitializeComponent();
            this.m = m;
            label1.Text += m.NameMedicine + " ואח'כ לחץ על אישור";
            this.Text = "עדכון כמות התרופה " + m.NameMedicine + " במלאי";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m.UnitsInStock = m.UnitsInStock + Convert.ToInt32(textBox1.Text);
            MyDB.medicines.UpDataItem(m);
            MyDB.medicines.SaveChanges();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
