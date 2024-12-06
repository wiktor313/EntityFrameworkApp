using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework
{
    public partial class Form1 : Form
    {
        HurtowniaEntities db = new HurtowniaEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            db.Znajomi.Load();
            dataGridView1.DataSource = db.Znajomi.Local.ToBindingList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int changesCount = 0;
            StringBuilder changes = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is Znajomi znajomy && (db.Entry(znajomy).State == EntityState.Added || db.Entry(znajomy).State == EntityState.Modified))
                {
                    changesCount++;
                    changes.AppendLine($"Nazwisko: {znajomy.Nazwisko}, Imie: {znajomy.Imie}, Pesel: {znajomy.Pesel}, Data urodzenia: {znajomy.Data_urodzenia}, Majatek: {znajomy.Majatek}, Miasto: {znajomy.Miasto}");
                }
            }
            if (changesCount == 0) MessageBox.Show("Brak zmian", "Zmiany znajomi");
            else MessageBox.Show($"Ilość zmian: {changesCount}\n\nZmiany:\n{changes.ToString()}", "Zmiany znajomi");

            
            changes.Clear();
            changesCount = 0;
            var entries = db.ChangeTracker.Entries<Mieszkania_domy>().ToList();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var mieszkanie = entry.Entity;
                    changes.AppendLine($"Pesel_w: {mieszkanie.Pesel_w}, ID: {mieszkanie.ID}, Metraz: {mieszkanie.Metraz}, Miejscowosc: {mieszkanie.Miejscowosc}, Kod: {mieszkanie.Kod}, Adres: {mieszkanie.Adres}");
                    changesCount++;
                }
            }
            if (changesCount == 0) MessageBox.Show("Brak zmian", "Zmiany mieszkania");
            else MessageBox.Show($"Ilość zmian: {changesCount}\n\nZmiany:\n{changes.ToString()}", "Zmiany mieszkania");
        }

        private string selectedPesel;

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.DataSource == null) return;
            if (e.RowIndex >= dataGridView1.Rows.Count || dataGridView1.Rows[e.RowIndex].Cells[2].Value == null) return;

            selectedPesel = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            var entries = db.ChangeTracker.Entries<Mieszkania_domy>().ToList(); //zwraca wszystkie obiekty w kontekście
            foreach (var entry in entries)
            {
                db.Entry(entry.Entity).State = EntityState.Detached; //odłącza obiekt od kontekstu
            }

            var wybrane = db.Mieszkania_domy.Where(z => z.Pesel_w == selectedPesel);
            wybrane.Load();

            dataGridView2.DataSource = db.Mieszkania_domy.Local.ToBindingList();
        }

        private void dataGridView2_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Cancel = true;
            }
            else dataGridView2.Rows[e.RowIndex].Cells[0].Value = selectedPesel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Czy zapisać zmiany?", "Zapis", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.SaveChanges();
                    MessageBox.Show("Zmiany zostały zapisane do bazy danych.");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisywania zmian: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}


