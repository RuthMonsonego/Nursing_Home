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
    public partial class DoctorsPage : Form
    {
        Doctor d;
        public DoctorsPage(Doctor d)
        {
            InitializeComponent();
            this.d = new Doctor();
            this.d = d;
            label1.Text = d.NameDoctor;
            dataGridView1.DataSource = MyDB.elders.GetList().Where(x => x.IdDoctor == d.IdDoctor).Select(x=>new { x.IdElder,x.NameElder,x.DateOfBirth.Date,x.HospitalizationRoom,x.IdDoctor,x.PhoneContact,x.NameContact,x.ProximityContact}).ToList();
            dataGridView1.Columns[0].HeaderText = "מספר ת.ז. ";
            dataGridView1.Columns[1].HeaderText = "שם המטופל";
            dataGridView1.Columns[2].HeaderText = "תאריך לידה";
            dataGridView1.Columns[3].HeaderText = "חדש איפוז";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
            dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
            dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
            this.Text = "עמוד הרופא " + d.NameDoctor;
        }

        private void CellClickElder(object sender, DataGridViewCellEventArgs e)
        {
            Elder eld = new Elder();
            foreach (var item in MyDB.elders.GetList())
            {
                if (item.IdElder == dataGridView1.CurrentRow.Cells[0].Value.ToString())
                    eld = item;
            }
            ElderPage f = new ElderPage(eld,d);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
