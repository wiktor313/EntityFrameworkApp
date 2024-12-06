using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework
{ 
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        HurtowniaEntities db = new HurtowniaEntities();

        private void Form2_Load(object sender, EventArgs e)
        {
            db.Znajomi.Load();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = db.Znajomi.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                var w_dane = row.Cells[2].Value as string;

                if (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();
                }
                var wynik = db.ProceduraMieszkanie(w_dane);
                dataGridView2.DataSource = wynik.ToList();
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnego wiersza");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int licznik = 0;
            foreach (var dd in db.Znajomi)
            {
                if (db.Entry(dd).State != EntityState.Unchanged)
                {
                    licznik++;
                }
            }
            foreach (var ddd in db.Mieszkania_domy)
            {
                if (db.Entry(ddd).State != EntityState.Unchanged)
                {
                    licznik++;
                }
            }
            MessageBox.Show("Ilość zmian: " + licznik);
        }
    }
}
