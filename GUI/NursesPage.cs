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
    public partial class NursesPage : Form
    {
        Nurse n;
        int shift = 0;
        List<Treat> t;
        public NursesPage(Nurse n)
        {
            InitializeComponent();
            this.n = n;
            label1.Text = "שלום ל" + n.NameNurse;
            this.Text = "כניסה למשמרת";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyShifts msh = new MyShifts(n);
            this.Hide();
            msh.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "1" && textBox1.Text != "2" && textBox1.Text != "3")
            {
                errorProvider1.SetError(textBox1, "מספר קומה שגוי");
                textBox1.Text = "";
            }
            else
            {
                errorProvider1.Clear();
                button1.Visible = true;
                tub.Visible = true;
                dataGridViewElders.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().First(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).Where(x => x.HospitalizationRoom.ToString()[0] == textBox1.Text[0]).ToList();
                dataGridViewElders.Columns[0].HeaderText = "תעודת זהות";
                dataGridViewElders.Columns[1].HeaderText = "שם";
                dataGridViewElders.Columns[2].HeaderText = "תאריך לידה";
                dataGridViewElders.Columns[3].HeaderText = "חדר אישפוז";
                dataGridViewElders.Columns[4].HeaderText = "הרופא המטפל";
                dataGridViewElders.Columns[5].HeaderText = "מספר הקשר";
                dataGridViewElders.Columns[6].HeaderText = "שם איש קשר";
                dataGridViewElders.Columns[7].HeaderText = "קרבה משפחתית";
                List<Elder> el = MyDB.elders.GetList().Where(x => x.HospitalizationRoom.ToString()[0] == textBox1.Text[0]).ToList();
                if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour < 14)
                    shift = 1;
                else if (DateTime.Now.Hour >= 14 && DateTime.Now.Hour < 21)
                    shift = 2;
                else
                    shift = 3;
                List<Medicine_Take> mt = MyDB.medicinestake.GetList().Where(x => x.Status == false && x.InShift == shift&& x.InDate.Date.ToShortDateString() == DateTime.Today.ToShortDateString() && MyDB.elders.GetList().First(z => z.IdElder == MyDB.medicineelder.GetList().First(y => y.AutomaticCode == x.AutomaticCode).IdElder).HospitalizationRoom.ToString()[0] == textBox1.Text[0]).ToList();
                t = new List<Treat>();
                t.Clear();
                Treat treat = new Treat();
                foreach (var item in mt)
                {
                    treat = new Treat();
                    treat.c = item.Code;
                    treat.code = MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).CodeMedicine;
                    treat.namem = MyDB.medicines.GetList().First(z => z.CodeMedicine == MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).CodeMedicine).NameMedicine;
                    treat.nummili = MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).NumberOfMilimetersAtADay;
                    treat.namee = MyDB.elders.GetList().First(a => a.IdElder == MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).IdElder).NameElder;
                    treat.id = MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).IdElder;
                    treat.room = MyDB.elders.GetList().First(a => a.IdElder == MyDB.medicineelder.GetList().First(y => y.AutomaticCode == item.AutomaticCode).IdElder).HospitalizationRoom;
                    t.Add(treat);
                    treat = null;
                }
                dataGridViewMedicines.DataSource = t;
                dataGridViewMedicines.Columns[0].HeaderText = "קוד הטיפול";
                dataGridViewMedicines.Columns[1].HeaderText = "קוד תרופה";
                dataGridViewMedicines.Columns[2].HeaderText = "שם תרופה";
                dataGridViewMedicines.Columns[3].HeaderText = "הכמות במילימטרים";
                dataGridViewMedicines.Columns[4].HeaderText = "שם המטופל";
                dataGridViewMedicines.Columns[5].HeaderText = "תעודת זהות";
                dataGridViewMedicines.Columns[6].HeaderText = "חדר אישפוז";
                button2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Text = n.NameNurse;
                this.Text = "משמרת ";
                if (shift == 1)
                    this.Text += "בוקר";
                else if (shift == 2)
                    this.Text += "צהוריים";
                else
                    this.Text += "ערב";
                this.Text += " של: " + n.NameNurse;
            }
        }

        private void dataGridViewMedicines_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult r = MessageBox.Show("האם נתת את התרופה " + dataGridViewMedicines.CurrentRow.Cells[2].Value + " לזקן " + dataGridViewMedicines.CurrentRow.Cells[4].Value, "נטילת תרופה", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                //שינוי הסטטוס והסרה מהרשימה
                Medicine_Take m = MyDB.medicinestake.GetList().First(x => x.Code == Convert.ToInt32(dataGridViewMedicines.CurrentRow.Cells[0].Value));
                m.Status = true;
                MyDB.medicinestake.UpDataItem(m);
                MyDB.medicinestake.SaveChanges();
                Treat treat = new Treat();
                treat = t.First(x => x.c == m.Code);
                t.Remove(treat);
                dataGridViewMedicines.DataSource = null;
                dataGridViewMedicines.DataSource = t;
                dataGridViewMedicines.Columns[0].HeaderText = "קוד הטיפול";
                dataGridViewMedicines.Columns[1].HeaderText = "קוד תרופה";
                dataGridViewMedicines.Columns[2].HeaderText = "שם תרופה";
                dataGridViewMedicines.Columns[3].HeaderText = "הכמות במילימטרים";
                dataGridViewMedicines.Columns[4].HeaderText = "שם המטופל";
                dataGridViewMedicines.Columns[5].HeaderText = "תעודת זהות";
                dataGridViewMedicines.Columns[6].HeaderText = "חדר אישפוז";
                if (t.Count == 0)
                {
                    label3.Text = "אין תרופות לנטילה";
                }

                //הפחתת התרופה מהמלאי
                Medicine medicine = MyDB.medicines.GetList().First(x => x.CodeMedicine == treat.code);
                if (Convert.ToInt32(medicine.UnitsInStock) < Convert.ToInt32(treat.nummili))
                {
                    MessageBox.Show(" לא ניתן לבצע פעולה זו כיון שתרופה זו לא קיימת מספיק במאגר, יש לפנות בדחיפות למנהל");
                    t.Add(treat);
                    dataGridViewMedicines.DataSource = null;
                    dataGridViewMedicines.DataSource = t;
                    dataGridViewMedicines.Columns[0].HeaderText = "קוד הטיפול";
                    dataGridViewMedicines.Columns[1].HeaderText = "קוד תרופה";
                    dataGridViewMedicines.Columns[2].HeaderText = "שם תרופה";
                    dataGridViewMedicines.Columns[3].HeaderText = "הכמות במילימטרים";
                    dataGridViewMedicines.Columns[4].HeaderText = "שם המטופל";
                    dataGridViewMedicines.Columns[5].HeaderText = "תעודת זהות";
                    dataGridViewMedicines.Columns[6].HeaderText = "חדר אישפוז";
                }
                else
                {
                    medicine.UnitsInStock = medicine.UnitsInStock - treat.nummili;
                    MyDB.medicines.UpDataItem(medicine);
                    MyDB.medicines.SaveChanges();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
