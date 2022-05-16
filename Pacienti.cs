using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proiect_MTP
{
    public partial class Pacienti : Form
    {
        public Pacienti()
        {
            InitializeComponent();
        }

        //incarcarea datelor in baza de date SQL
        private void Pacienti_Load(object sender, EventArgs e)
        {
            string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
            Initial Catalog=DogzillaPaws;
            Integrated Security=True"; 
            SqlConnection cnn = new SqlConnection(connect); 
            cnn.Open();
            string tabel_date = "select * from Pacienti"; 
            SqlDataAdapter da = new SqlDataAdapter(tabel_date, connect);
            DataSet ds = new DataSet(); 
            da.Fill(ds, "Pacienti");
            dataGridView1.DataSource = ds.Tables["Pacienti"].DefaultView;
            cnn.Close();
        }


        //Butonul de Search
        private void button1_Click(object sender, EventArgs e)
        {
            string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
            Initial Catalog=DogzillaPaws;
            Integrated Security=True";
            SqlConnection con = new SqlConnection(connect); 
            con.Open();
            string stmt = "select * from pacienti where Nume_pacient='" + cautaNumeTB.Text + "'"; 
            SqlDataAdapter da = new SqlDataAdapter(stmt, con); 
            DataSet ds = new DataSet(); 
            da.Fill(ds,"Pacienti"); 
            dataGridView1.DataSource = ds.Tables["Pacienti"].DefaultView; 
            con.Close();
            da.Dispose();
            ds.Dispose();
        }

        //Buton -> forma de inregistrare a unui Pacient nou
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f=new AdaugarePacient();
            f.ShowDialog();
        }

        //Butonul Cancel
        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        string cnp_selectat = "";
        string nume_selectat = "";

        //button  -> forma adaugare consultatie noua
        private void button3_Click(object sender, EventArgs e)
        {
            if ((cnp_selectat != "")&& (nume_selectat != ""))
            {
                this.Hide();
                Form f = new Consultatie(cnp_selectat,nume_selectat);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Selectati un pacient");  
        }


        //button  -> forma adaugare radiografie noua
        private void buttonAdaugareRadiografie_Click(object sender, EventArgs e)
        {
            if ((cnp_selectat != "") && (nume_selectat != ""))
            {
                this.Hide();
                Form f = new AdaugareRadiografie(cnp_selectat, nume_selectat);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Selectati un pacient");
        }


        //button  -> forma adaugare programare salon noua
        private void buttonAdaugarePrSalon_Click(object sender, EventArgs e)
        {
            if ((cnp_selectat != "") && (nume_selectat != ""))
            {
                this.Hide();
                Form f = new AdaugarePrSalon(cnp_selectat, nume_selectat);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Selectati un pacient");
        }

        //button  -> forma vizualizare fisa medicala
        private void buttonVizualizareFisa_Click(object sender, EventArgs e)
        {
            if ((cnp_selectat != "") && (nume_selectat != ""))
            {
                this.Hide();
                Form f = new VizualizareFisaMedicala(cnp_selectat, nume_selectat);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Selectati un pacient");
        }


        private void buttonVizoalizareFisaSalon_Click(object sender, EventArgs e)
        {
            if ((cnp_selectat != "") && (nume_selectat != ""))
            {
                this.Hide();
                Form f = new VizualizareFisaSalon(cnp_selectat, nume_selectat);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Selectati un pacient");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cnp_selectat = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            nume_selectat = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            button3.Enabled = true;
            buttonAdaugareRadiografie.Enabled = true;
            buttonAdaugarePrSalon.Enabled = true;
            buttonVizualizareFisa.Enabled = true;
            buttonVizoalizareFisaSalon.Enabled = true;
        }

        //deleting pacient from data base 

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
                Initial Catalog=DogzillaPaws;
                Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Pacienti where  Nume_pacient='" + nume_selectat + "'";              
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Pacientul a fost sters cu succes!");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private void cautaNumeTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
