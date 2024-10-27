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
    public partial class ElderPage : Form
    {
        Elder el;
        Doctor d;
        public ElderPage(Elder e,Doctor d)
        {
            InitializeComponent();
            label1.Text = e.NameElder;
            label2.Text = "ת.ז.: " + e.IdElder;
            label3.Text = "תאריך לידה: " + e.DateOfBirth.ToShortDateString();
            label4.Text = "חדר אישפוז: " + e.HospitalizationRoom;
            this.el = e;
            this.d = d;
            dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == e.IdElder&&x.EndDate.Date>=DateTime.Today).Select(x=> new {x.AutomaticCode,MyDB.medicines.GetList().First(y=>y.CodeMedicine==x.CodeMedicine).NameMedicine,x.StartDate.Date,x.EndDate,x.TimesADay,x.NumberOfMilimetersAtADay }).ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "שם התרופה";
            dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
            dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
            dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
            dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
            this.Text = "תיק רפואי: " + e.NameElder;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Medicines_Of_Elder m = MyDB.medicineelder.GetList().FirstOrDefault(x => x.AutomaticCode == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            DialogResult r = MessageBox.Show("האם ברצונך למחוק את  " + m.NameMedicine(m.CodeMedicine) + "לזקן: "+ m.NameElder(m.IdElder),"מחיקת תרופה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//איך מסדרים את זה
            if (r == DialogResult.Yes)
            {
                MyDB.medicineelder.DeleteItem(m);
                MyDB.medicineelder.SaveChanges();
                dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == el.IdElder && x.EndDate.Date >= DateTime.Today).Select(x => new { x.AutomaticCode, MyDB.medicines.GetList().First(y => y.CodeMedicine == x.CodeMedicine).NameMedicine, x.StartDate.Date, x.EndDate, x.TimesADay, x.NumberOfMilimetersAtADay }).ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "שם התרופה";
                dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
                dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
                dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
                dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
            }
        }

        private void הוספה_Click(object sender, EventArgs e)
        {
            AddMedicineOfElder a = new AddMedicineOfElder(d,el);
            this.Hide();
            a.ShowDialog();
            this.Show();
            dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == el.IdElder && x.EndDate.Date >= DateTime.Today).Select(x => new { x.AutomaticCode, MyDB.medicines.GetList().First(y => y.CodeMedicine == x.CodeMedicine).NameMedicine, x.StartDate.Date, x.EndDate, x.TimesADay, x.NumberOfMilimetersAtADay }).ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "שם התרופה";
            dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
            dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
            dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
            dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (button3.Text == "לצפיה בכל ההיסטוריה הרפואית")
            {
                dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == el.IdElder).Select(x => new { x.AutomaticCode, MyDB.medicines.GetList().First(y => y.CodeMedicine == x.CodeMedicine).NameMedicine, x.StartDate.Date, x.EndDate, x.TimesADay, x.NumberOfMilimetersAtADay }).ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "שם התרופה";
                dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
                dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
                dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
                dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
                button3.Text = "לחזרה";
            }
            else
            {
                dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == el.IdElder && x.EndDate.Date >= DateTime.Today).Select(x => new { x.AutomaticCode, MyDB.medicines.GetList().First(y => y.CodeMedicine == x.CodeMedicine).NameMedicine, x.StartDate.Date, x.EndDate, x.TimesADay, x.NumberOfMilimetersAtADay }).ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "שם התרופה";
                dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
                dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
                dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
                dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
                button3.Text = "לצפיה בכל ההיסטוריה הרפואית";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Medicines_Of_Elder c = MyDB.medicineelder.GetList().FirstOrDefault(x => x.AutomaticCode == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            if (c != null)
            {
                AddMedicineOfElder f = new AddMedicineOfElder(c);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            dataGridView1.DataSource = MyDB.medicineelder.GetList().Where(x => x.IdElder == el.IdElder && x.EndDate.Date >= DateTime.Today).Select(x => new { x.AutomaticCode, MyDB.medicines.GetList().First(y => y.CodeMedicine == x.CodeMedicine).NameMedicine, x.StartDate.Date, x.EndDate, x.TimesADay, x.NumberOfMilimetersAtADay }).ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "שם התרופה";
            dataGridView1.Columns[2].HeaderText = "תאריך הנטילה הראשונה";
            dataGridView1.Columns[3].HeaderText = "תאריך הנטילה האחרונה";
            dataGridView1.Columns[4].HeaderText = "מספר הפעמים ביום";
            dataGridView1.Columns[5].HeaderText = "מספר המילימטרים בפעם";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
