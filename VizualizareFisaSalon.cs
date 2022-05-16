using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_MTP
{
    public partial class VizualizareFisaSalon : Form
    {
        string cnp, nume_pacient;
        public VizualizareFisaSalon(string cnp_selectat, string nume_selectat)
        {
            InitializeComponent();
            cnp = cnp_selectat;
            nume_pacient = nume_selectat;
        }

        private void VizualizareFisaSalon_Load(object sender, EventArgs e)
        {
            textBoxCNP.Text = cnp;
            textBoxNumePacient.Text = nume_pacient;
            try
            {
                //----------------------------------------------------
                //Introducere date din database Cosmetica
                SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;Initial Catalog=DogzillaPaws;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("select * from Cosmetica where  CNP_proprietar='" + cnp + "'", cnn);
                cnn.Open();
                SqlDataReader dgz = cmd.ExecuteReader();
                while (dgz.Read())
                {
                    textBoxNumePacient.Text = dgz["Nume_pacient"].ToString();
                    dateTimePicker1.Text = dgz["Data"].ToString();
                    textBoxSpalat.Text = dgz["Spalat"].ToString();
                    textBoxTuns.Text = dgz["Tuns"].ToString();
                    textBoxTratament.Text = dgz["Tratamente"].ToString();
                    textBoxComentarii.Text = dgz["Cometarii"].ToString();
                }
                dgz.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        //button OK->trimitere forma Pacienti
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }

    }
}
