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
   
    public partial class Consultatie : Form
    {
        string cnp,nume_pacient;
        public Consultatie(string cnp_selectat, string nume_selectat)
        {
            InitializeComponent();
            cnp = cnp_selectat;
            nume_pacient = nume_selectat;
        }

        private void Consultatie_Load(object sender, EventArgs e)
        {
            textBoxCNP.Text = cnp.ToString();
            textBoxNumePacient.Text = nume_pacient.ToString();
        }

        //button Cancel
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }


        //---------------------------ERROR PROVIDERS-------------------------------
        //TEXTBOX SIMPTOME

        private void textBoxSimptome_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSimptome.Text))
            {
                textBoxSimptome.Focus();
                errorProviderSimptome.SetError(textBoxSimptome, "Introduceti un raspuns!");
            }
            else
                errorProviderSimptome.SetError(this.textBoxSimptome, String.Empty);
        }

        //TEXTBOX DIAGNOSTIC
        private void textBoxDiagnostic_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnostic.Text))
            {
                textBoxDiagnostic.Focus();
                errorProviderDiagnostic.SetError(textBoxDiagnostic, "Introduceti un raspuns!");
            }
            else
                errorProviderDiagnostic.SetError(this.textBoxDiagnostic, String.Empty);
        }

        //TEXTBOX TRATAMENT
        private void textBoxTratament_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTratament.Text))
            {
                textBoxSimptome.Focus();
                errorProviderSimptome.SetError(textBoxTratament, "Introduceti un raspuns!");
            }
            else
                errorProviderTratament.SetError(this.textBoxTratament, String.Empty);
        }

        //--------------------------BUTTON SALVARE CONSULATIE->SALVARE IN MSQL------------------------------
        private void buttonAdaugareConsultatie_Click(object sender, EventArgs e)
        {

            //---------------INTRODUCERE DATE IN MSQL------------------------
            try
            {
                string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
                Initial Catalog=DogzillaPaws;
                Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Consultatii ([Nume_pacient], [CNP_proprietar], [Data], [Simptome], [Diagnostic],[Tratament]) values (@npacient,@cnp,@data,@simptome,@diagnostic,@tratament)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@npacient", textBoxNumePacient.Text);
                sc.Parameters.AddWithValue("@cnp", textBoxCNP.Text);
                sc.Parameters.AddWithValue("@data", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@simptome", textBoxSimptome.Text);
                sc.Parameters.AddWithValue("@diagnostic", textBoxDiagnostic.Text);
                sc.Parameters.AddWithValue("@tratament", textBoxTratament.Text);
                sc.ExecuteNonQuery();
                cnn.Close();
                this.Hide();
                Form f = new Pacienti();
                f.ShowDialog();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
           
        }
    }
}
