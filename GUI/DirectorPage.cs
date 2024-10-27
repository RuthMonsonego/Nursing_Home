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
    public partial class DirectorPage : Form
    {
        int status,z;
        Medicine medicine;
        public DirectorPage()
        {
            InitializeComponent();
            this.Text = "עמוד המנהל";
            comboBox1.DataSource = MyDB.medicines.GetList().Select(x => x.NameMedicine).ToList();
            comboBox1.SelectedItem = null;
            medicine = null;
        }

        private void צפיהבנתוניהרופאיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ למחוק אחים מהמאגר יש לבחור את האח המבוקש וללחוץ על מחק";
            dataGridView1.DataSource = MyDB.doctors.GetList().Select(x=> new { x.IdDoctor,x.NameDoctor,x.LicenseNumber,x.PoneNumber,x.Address,x.DateOfBirth.Date}).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הרופא";
            dataGridView1.Columns[2].HeaderText = "מספר רישיון";
            dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[5].HeaderText = "תאריך לידה";
            button1.Visible = true;
            status=1;
        }


        private void הוספתרופאיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status=1;
            AddDN a = new AddDN(true);
            this.Hide();
            a.ShowDialog();
            this.Show();
            dataGridView1.DataSource = MyDB.doctors.GetList().Select(x => new { x.IdDoctor, x.NameDoctor, x.LicenseNumber, x.PoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הרופא";
            dataGridView1.Columns[2].HeaderText = "מספר רישיון";
            dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[5].HeaderText = "תאריך לידה";
        }

        private void עדכוןפרטיהרופאיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text="ע'מ לעדכן את נתוני הרופאים יש ללחוץ פעמיים על הרופא/ה המבוקש/ת";
            dataGridView1.DataSource = MyDB.doctors.GetList().Select(x => new { x.IdDoctor, x.NameDoctor, x.LicenseNumber, x.PoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הרופא";
            dataGridView1.Columns[2].HeaderText = "מספר רישיון";
            dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[5].HeaderText = "תאריך לידה";
            button1.Visible = false;
            status=1;
        }

        private void עדכוןפרטיהאחיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ לעדכן את נתוני האחים יש ללחוץ פעמיים על האח/ות המבוקש/ת";
            dataGridView1.DataSource = MyDB.nurses.GetList().Select(x=> new { x.IdNurse,x.NameNurse,x.PhoneNumber,x.Address,x.DateOfBirth.Date}).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם האח";
            dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[4].HeaderText = "תאריך לידה";
            button1.Visible = false;
            status=2;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(status==1)
            {
                Doctor d = new Doctor();
                foreach (var item in MyDB.doctors.GetList())
                {
                    if (item.IdDoctor == dataGridView1.CurrentRow.Cells[0].Value.ToString())
                        d = item;
                }
                AddDN a = new AddDN(d);
                this.Hide();
                a.ShowDialog();
                this.Show();
                dataGridView1.DataSource = MyDB.doctors.GetList().Select(x => new { x.IdDoctor, x.NameDoctor, x.LicenseNumber, x.PoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
                dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                dataGridView1.Columns[1].HeaderText = "שם הרופא";
                dataGridView1.Columns[2].HeaderText = "מספר רישיון";
                dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
                dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
                dataGridView1.Columns[5].HeaderText = "תאריך לידה";
            }
            if(status==2)
            {
                Nurse n = new Nurse();
                foreach (var item in MyDB.nurses.GetList())
                {
                    if (item.IdNurse == dataGridView1.CurrentRow.Cells[0].Value.ToString())
                        n = item;
                }
                AddDN a = new AddDN(n);
                this.Hide();
                a.ShowDialog();
                this.Show();
                dataGridView1.DataSource = MyDB.nurses.GetList().Select(x => new { x.IdNurse, x.NameNurse, x.PhoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
                dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                dataGridView1.Columns[1].HeaderText = "שם האח";
                dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
                dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
                dataGridView1.Columns[4].HeaderText = "תאריך לידה";
            }
            if (status == 3)
            {
                Elder el = new Elder();
                foreach (var item in MyDB.elders.GetList())
                {
                    if (item.IdElder == dataGridView1.CurrentRow.Cells[0].Value.ToString())
                        el = item;
                }
                AddElder a = new AddElder(el);
                this.Hide();
                a.ShowDialog();
                this.Show();
                dataGridView1.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().FirstOrDefault(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).ToList();
                dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                dataGridView1.Columns[1].HeaderText = "שם הדייר";
                dataGridView1.Columns[2].HeaderText = "תאריך לידה";
                dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
                dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
                dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
                dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
                dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
            }
            if (status == 4)
            {
                Medicine m = new Medicine();
                foreach (var item in MyDB.medicines.GetList())
                {
                    if (item.CodeMedicine == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value))
                        m = item;
                }
                AddMedicine a = new AddMedicine(m);
                this.Hide();
                a.ShowDialog();
                this.Show();
                dataGridView1.DataSource = MyDB.medicines.GetList().Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
                dataGridView1.Columns[0].HeaderText = "קוד תרופה";
                dataGridView1.Columns[1].HeaderText = "שם תרופה";
                dataGridView1.Columns[2].HeaderText = "סוג תרופה";
                dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
                dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            }
            if(status==44)
            {
                Medicine m = MyDB.medicines.GetList().First(x => x.CodeMedicine == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                UpdateOrderingMedicine u = new UpdateOrderingMedicine(m);
                this.Hide();
                u.ShowDialog();
                this.Show();
                dataGridView1.DataSource = MyDB.medicines.GetList().Where(x => x.UnitsInStock < x.TheIdealAmauntInStock / 2).Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
                dataGridView1.Columns[0].HeaderText = "קוד תרופה";
                dataGridView1.Columns[1].HeaderText = "שם תרופה";
                dataGridView1.Columns[2].HeaderText = "סוג תרופה";
                dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
                dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            }
        }

        private void הוספתאחיםלמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status=2;
            AddDN a = new AddDN(false);
            this.Hide();
            a.ShowDialog();
            this.Show();
            dataGridView1.DataSource = MyDB.nurses.GetList().Select(x => new { x.IdNurse, x.NameNurse, x.PhoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם האח";
            dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[4].HeaderText = "תאריך לידה";
        }

        private void צפיהבנתוניהאחיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "רשימת האחים";
            status =2;
            dataGridView1.DataSource = MyDB.nurses.GetList().Select(x => new { x.IdNurse, x.NameNurse, x.PhoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם האח";
            dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[4].HeaderText = "תאריך לידה";
            button1.Visible = false;
        }

        private void צפיהבנתוניהרופאיםToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            label1.Text = "רשימת הרופאים"; 
            status =1;
            dataGridView1.DataSource = MyDB.doctors.GetList().Select(x => new { x.IdDoctor, x.NameDoctor, x.LicenseNumber, x.PoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הרופא";
            dataGridView1.Columns[2].HeaderText = "מספר רישיון";
            dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[5].HeaderText = "תאריך לידה";
            button1.Visible = false;
        }

        private void מחיקתאחיםמהמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ למחוק אחים מהמאגר יש לבחור את האח המבוקש וללחוץ על מחק";
            dataGridView1.DataSource = MyDB.nurses.GetList().Select(x => new { x.IdNurse, x.NameNurse, x.PhoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם האח";
            dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
            dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
            dataGridView1.Columns[4].HeaderText = "תאריך לידה";
            button1.Visible = true;
            status=2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (status==1)
            {
                Doctor d = MyDB.doctors.GetList().FirstOrDefault(x => x.IdDoctor == dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DialogResult r = MessageBox.Show("האם ברצונך למחוק את הרופא   " + d.NameDoctor , "מחיקת רופא מהמאגר", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//איך מסדרים את זה
                if (r == DialogResult.Yes)
                {
                    Elder elder = MyDB.elders.GetList().FirstOrDefault(x => x.IdDoctor == d.IdDoctor);
                    if (elder != null)
                    {
                        DeleteDoctor deleteDoctor = new DeleteDoctor(d);
                        this.Hide();
                        deleteDoctor.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MyDB.doctors.DeleteItem(d);
                        MyDB.doctors.SaveChanges();
                        MessageBox.Show("מחיקת הרופא מהמאגר נשלמה");
                    }
                    //MyDB.doctors.DeleteItem(d);
                    //MyDB.doctors.SaveChanges();
                    dataGridView1.DataSource = MyDB.doctors.GetList().Select(x => new { x.IdDoctor, x.NameDoctor, x.LicenseNumber, x.PoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
                    dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                    dataGridView1.Columns[1].HeaderText = "שם הרופא";
                    dataGridView1.Columns[2].HeaderText = "מספר רישיון";
                    dataGridView1.Columns[3].HeaderText = "מספר פלאפון";
                    dataGridView1.Columns[4].HeaderText = "כתובת מגורים";
                    dataGridView1.Columns[5].HeaderText = "תאריך לידה";
                }
            }
            if (status==2)
            {
                Nurse n = MyDB.nurses.GetList().FirstOrDefault(x => x.IdNurse == dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DialogResult r = MessageBox.Show("האם ברצונך למחוק את האח   " + n.NameNurse, "מחיקת אח מהמאגר", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//איך מסדרים את זה
                if (r == DialogResult.Yes)
                {
                    int i = 0;
                    foreach (var item in MyDB.shiftsemp.GetList())
                    {
                        if (item.CodeEmployee == n.IdNurse)
                        {
                            i++;
                            Shift shift = MyDB.shifts.GetList().First(q => q.CodeShift == item.CodeShift);
                            shift.Nnurses++;
                            MyDB.shifts.UpDataItem(shift);
                            MyDB.shifts.SaveChanges();
                            MyDB.shiftsemp.DeleteItem(item);
                            MyDB.shiftsemp.SaveChanges();
                        }
                    }
                    MessageBox.Show("לתשומת ליבך, במהלך הסרת האח מהמאגר נמחקו גם כן  " + i + " משמרות שלו. יש לדאוג לקבוע בהקדם אחים אחרים למשמרות אלו");
                    MyDB.nurses.DeleteItem(n);
                    MyDB.nurses.SaveChanges();
                    dataGridView1.DataSource = MyDB.nurses.GetList().Select(x => new { x.IdNurse, x.NameNurse, x.PhoneNumber, x.Address, x.DateOfBirth.Date }).ToList();
                    dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                    dataGridView1.Columns[1].HeaderText = "שם האח";
                    dataGridView1.Columns[2].HeaderText = "מספר פלאפון";
                    dataGridView1.Columns[3].HeaderText = "כתובת מגורים";
                    dataGridView1.Columns[4].HeaderText = "תאריך לידה";
                }
            }
            if(status==3)
            {
                Elder el = MyDB.elders.GetList().First(x => x.IdElder == dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DialogResult r = MessageBox.Show("האם ברצונך למחוק את הדייר   " + el.NameElder, "מחיקת דייר מהמאגר", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    MyDB.elders.DeleteItem(el);
                    MyDB.elders.SaveChanges();
                    dataGridView1.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().FirstOrDefault(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).ToList();
                    dataGridView1.Columns[0].HeaderText = "תעודת זהות";
                    dataGridView1.Columns[1].HeaderText = "שם הדייר";
                    dataGridView1.Columns[2].HeaderText = "תאריך לידה";
                    dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
                    dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
                    dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
                    dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
                    dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
                }
            }
            if (status == 4)
            {
                Medicine m = MyDB.medicines.GetList().FirstOrDefault(x => x.CodeMedicine ==Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value));
                medicine = m;
                DialogResult r = MessageBox.Show("האם ברצונך למחוק מהמאגר את התרופה   " + m.NameMedicine, "מחיקת תרופה מהמאגר", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    comboBox1.Visible = true;
                    label2.Visible = true;
                    label1.Visible = false;
                    dataGridView1.Visible = false;
                    menuStrip1.Visible = false;
                    button1.Visible = false;
                    comboBox1.SelectedItem = null;
                    z = 5;
                    comboBox1.DataSource = MyDB.medicines.GetList().Where(x => x.NameMedicine != medicine.NameMedicine).Select(x => x.NameMedicine).ToList();

                }
            }
        }

        private void מחיקתדייריםמהמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ למחוק דיירים מהמאגר יש לבחור את הדייר המבוקש וללחוץ על מחק";
            status = 3;
            dataGridView1.DataSource = MyDB.elders.GetList().Select(x=> new {x.IdElder,x.NameElder,x.DateOfBirth.Date,x.HospitalizationRoom,MyDB.doctors.GetList().FirstOrDefault(y=>y.IdDoctor==x.IdDoctor).NameDoctor,x.PhoneContact,x.NameContact,x.ProximityContact }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הדייר";
            dataGridView1.Columns[2].HeaderText = "תאריך לידה";
            dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
            dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
            dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
            dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
            dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
            button1.Visible = true;
        }

        private void צפיהבנתוניהדייריםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "רשימת הדיירים";
           status = 3;
            dataGridView1.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().FirstOrDefault(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הדייר";
            dataGridView1.Columns[2].HeaderText = "תאריך לידה";
            dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
            dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
            dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
            dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
            dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
            button1.Visible = false;
        }

        private void עדכוןפרטיהדייריםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ לעדכן את נתוני הדיירים יש ללחוץ פעמיים על הדייר/ת המבוקש/ת";
            button1.Visible = false;
            dataGridView1.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().FirstOrDefault(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הדייר";
            dataGridView1.Columns[2].HeaderText = "תאריך לידה";
            dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
            dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
            dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
            dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
            dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
            status = 3;
        }

        private void הוספתדייריםלמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = 3;
            AddElder a = new AddElder();
            this.Hide();
            a.ShowDialog();
            this.Show();
            dataGridView1.DataSource = MyDB.elders.GetList().Select(x => new { x.IdElder, x.NameElder, x.DateOfBirth.Date, x.HospitalizationRoom, MyDB.doctors.GetList().FirstOrDefault(y => y.IdDoctor == x.IdDoctor).NameDoctor, x.PhoneContact, x.NameContact, x.ProximityContact }).ToList();
            dataGridView1.Columns[0].HeaderText = "תעודת זהות";
            dataGridView1.Columns[1].HeaderText = "שם הדייר";
            dataGridView1.Columns[2].HeaderText = "תאריך לידה";
            dataGridView1.Columns[3].HeaderText = "חדר אישפוז";
            dataGridView1.Columns[4].HeaderText = "הרופא המטפל";
            dataGridView1.Columns[5].HeaderText = "מספר איש הקשר";
            dataGridView1.Columns[6].HeaderText = "שם איש הקשר";
            dataGridView1.Columns[7].HeaderText = "קרבה משפחתית";
        }

        private void הוספתתרופותלמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = 4;
            AddMedicine a = new AddMedicine();
            this.Hide();
            a.ShowDialog();
            this.Show();
            dataGridView1.DataSource = MyDB.medicines.GetList().Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
            dataGridView1.Columns[0].HeaderText = "קוד תרופה";
            dataGridView1.Columns[1].HeaderText = "שם תרופה";
            dataGridView1.Columns[2].HeaderText = "סוג תרופה";
            dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
            dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
        }

        private void מחיקתתרופותמהמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ למחוק תרופות מהמאגר יש לבחור את התרופה המבוקשת וללחוץ על מחק";
            status = 4;
            dataGridView1.DataSource = MyDB.medicines.GetList().Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
            dataGridView1.Columns[0].HeaderText = "קוד תרופה";
            dataGridView1.Columns[1].HeaderText = "שם תרופה";
            dataGridView1.Columns[2].HeaderText = "סוג תרופה";
            dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
            dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            button1.Visible = true;
        }

        private void צפיהבנתוניכמותהתרופותשבמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "רשימת התרופות החסרות";
            status = 44;
            dataGridView1.DataSource = MyDB.medicines.GetList().Where(x => x.UnitsInStock < x.TheIdealAmauntInStock / 2).Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
            dataGridView1.Columns[0].HeaderText = "קוד תרופה";
            dataGridView1.Columns[1].HeaderText = "שם תרופה";
            dataGridView1.Columns[2].HeaderText = "סוג תרופה";
            dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
            dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            button1.Visible = false;
        }

        private void עדכוןכמותהתרופותבמאגרToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ע'מ לעדכן הזמנת תרופה יש ללחוץ פעמיים על השם של התרופה המבוקשת";
            status = 44;
            dataGridView1.DataSource = MyDB.medicines.GetList().Where(x => x.UnitsInStock < x.TheIdealAmauntInStock / 2).Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
            dataGridView1.Columns[0].HeaderText = "קוד תרופה";
            dataGridView1.Columns[1].HeaderText = "שם תרופה";
            dataGridView1.Columns[2].HeaderText = "סוג תרופה";
            dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
            dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            button1.Visible = false;
        }

        private void צפיהברשימתהתרופותהכוללתToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "רשימת התרופות";
            status = 4;
            dataGridView1.DataSource = MyDB.medicines.GetList().Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
            dataGridView1.Columns[0].HeaderText = "קוד תרופה";
            dataGridView1.Columns[1].HeaderText = "שם תרופה";
            dataGridView1.Columns[2].HeaderText = "סוג תרופה";
            dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
            dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
            button1.Visible = false;
        }

        private void צפיהבמשמרותהאחיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyShifts shifts = new MyShifts();
            this.Hide();
            shifts.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medicine != null)
            {
                if (z == 5)
                    z++;
                else
                {
                    DialogResult r = MessageBox.Show("האם ברצונך להגדיר את התרופה " + comboBox1.SelectedItem + " כתחליף לתרופה " + medicine.NameMedicine, "הגדרת תרופה חלופית", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        Medicine mm = MyDB.medicines.GetList().First(x => x.NameMedicine == comboBox1.SelectedItem.ToString());
                        foreach (var item in MyDB.medicineelder.GetList())
                        {
                            if (medicine.CodeMedicine == item.CodeMedicine)
                                item.CodeMedicine = mm.CodeMedicine;
                        }
                        MyDB.medicineelder.SaveChanges();
                        MyDB.medicines.DeleteItem(medicine);
                        MyDB.medicines.SaveChanges();
                        MessageBox.Show("המחיקה נגמרה בהצלחה");
                        comboBox1.Visible = false;
                        label2.Visible = false;
                        label1.Visible = true;
                        dataGridView1.Visible = true;
                        menuStrip1.Visible = true;
                        label1.Text = "רשימת התרופות";
                        status = 4;
                        dataGridView1.DataSource = MyDB.medicines.GetList().Select(x => new { x.CodeMedicine, x.NameMedicine, MyDB.types.GetList().First(y => y.CodeMedicineType == x.CodeMedicineType).NameMedicineType, x.TheIdealAmauntInStock, x.UnitsInStock }).ToList();
                        dataGridView1.Columns[0].HeaderText = "קוד תרופה";
                        dataGridView1.Columns[1].HeaderText = "שם תרופה";
                        dataGridView1.Columns[2].HeaderText = "סוג תרופה";
                        dataGridView1.Columns[3].HeaderText = "כמות אידיאלית";
                        dataGridView1.Columns[4].HeaderText = "כמות במלאי כרגע";
                        button1.Visible = false;
                    }
                }
            }
        }
    }
}