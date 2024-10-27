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
    public partial class DeleteDoctor : Form
    {
        List<Elder> elderlist = new List<Elder>();
        Doctor d;
        public DeleteDoctor(Doctor d)
        {
            InitializeComponent();
            this.d = d;
            label2.Text += d.NameDoctor + " מרשימת הרופאים ";
            elderlist = MyDB.elders.GetList().Where(x => x.IdDoctor == d.IdDoctor).ToList();
            comboBox1.DataSource = MyDB.doctors.GetList().Where(x => x.IdDoctor != d.IdDoctor).Select(x=>x.NameDoctor).ToList();
            comboBox1.SelectedItem = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (elderlist.Count != 0)
                {
                    label3.Text = elderlist.First().NameElder;
                    elderlist.First().IdDoctor = MyDB.doctors.GetList().FirstOrDefault(x => x.NameDoctor == comboBox1.SelectedItem.ToString()).IdDoctor;
                    MyDB.elders.UpDataItem(elderlist.First());
                    MyDB.elders.SaveChanges();
                    elderlist.Remove(elderlist.First());
                }
                else
                {
                    MyDB.doctors.DeleteItem(d);
                    MyDB.doctors.SaveChanges();
                    MessageBox.Show("מחיקת הרופא מהמאגר נשלמה");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
