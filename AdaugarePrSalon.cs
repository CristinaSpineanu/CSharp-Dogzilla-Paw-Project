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
    public partial class AdaugarePrSalon : Form
    {
        //-----------------COPIERE DATE(CNP,nume) din pacient form---------------------------------

        string cnp, nume_pacient;
        public AdaugarePrSalon(string cnp_selectat, string nume_selectat)
        {
            InitializeComponent();
            cnp = cnp_selectat;
            nume_pacient = nume_selectat;
        }

        private void AdaugarePrSalon_Load(object sender, EventArgs e)
        {
            textBoxCNP.Text = cnp.ToString();
            textBoxNumePacient.Text = nume_pacient.ToString();
        }


        //------------------ERROR PROVIDERS IF TEXTBOXEX EMPTY----------------------------------


        //errorProvider Spalat in case NULL
        private void comboBoxSpalat_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxSpalat.Text))
            {
                comboBoxSpalat.Focus();
                errorProviderSpalat.SetError(comboBoxSpalat, "Alegeti un raspuns!");
            }
            else
                errorProviderSpalat.SetError(this.comboBoxSpalat, String.Empty);
        }

        //errorProvider Tuns in case NULL
        private void comboBoxTuns_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTuns.Text))
            {
                comboBoxTuns.Focus();
                errorProviderTuns.SetError(comboBoxTuns, "Alegeti un raspuns!");
            }
            else
                errorProviderTuns.SetError(this.comboBoxTuns, String.Empty);
        }


        //errorProvider Tratament in case NULL
        private void comboBoxTratament_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTratament.Text))
            {
                comboBoxTratament.Focus();
                errorProviderTratament.SetError(comboBoxTratament, "Alegeti un raspuns!");
            }
            else
                errorProviderTratament.SetError(this.comboBoxTratament, String.Empty);
        }


        //errorProvider comentarii in case NULL
        private void textBoxComentarii_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxComentarii.Text))
            {
                textBoxComentarii.Focus();
                errorProviderComentarii.SetError(textBoxComentarii, "Introduceti mai multe detalii!");
            }
            else
                errorProviderComentarii.SetError(this.textBoxComentarii, String.Empty);
        }


        //------------------IMPREMENTARE BUTON ANULARE----------------------------------

        //button ->Cancel->Trimitere forma Pacienti
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }

    

        //------------------IMPREMENTARE BUTON SALVARE----------------------------------


        //button ->salvare programare ->trimitere catre forma Pacienti
        private void buttonAdaugarePrSalon_Click(object sender, EventArgs e)
        {

            try
            {
                string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
                Initial Catalog=DogzillaPaws;
                Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Cosmetica ([Nume_pacient], [CNP_proprietar], [Data], [Spalat], [Tuns],[Tratamente],[Cometarii]) values (@npacient,@cnp,@data,@spalat,@tuns,@tratamente,@comentarii)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@npacient", textBoxNumePacient.Text);
                sc.Parameters.AddWithValue("@cnp", textBoxCNP.Text);
                sc.Parameters.AddWithValue("@data", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@spalat", comboBoxSpalat.Text);
                sc.Parameters.AddWithValue("@tuns", comboBoxTuns.Text);
                sc.Parameters.AddWithValue("@tratamente", comboBoxTratament.Text);
                sc.Parameters.AddWithValue("@comentarii", textBoxComentarii.Text);
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
