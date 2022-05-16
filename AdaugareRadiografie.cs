using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_MTP
{
    public partial class AdaugareRadiografie : Form
    {
        string cnp, nume_pacient;
        public AdaugareRadiografie(string cnp_selectat, string nume_selectat)
        {
            InitializeComponent();
            cnp = cnp_selectat;
            nume_pacient = nume_selectat;
        }

        private void AdaugareRadiografie_Load(object sender, EventArgs e)
        {
            textBoxCNP.Text = cnp.ToString();
            textBoxNumePacient.Text = nume_pacient.ToString();
        }

        //button cancel
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }


        //---------------------------ERROR PROVIDERS-------------------------------
        //TEXTBOX DIAGNOSTIC
        private void textBoxDiagnostic_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnostic.Text))
            {
                textBoxDiagnostic.Focus();
                errorProviderDiagnostic.SetError(textBoxDiagnostic, "Introduceti un diagnostic!");
            }
            else
                errorProviderDiagnostic.SetError(this.textBoxDiagnostic, String.Empty);
        }
        //TEXTBOX COMENTARII
        private void textBoxComentarii_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxComentarii.Text))
            {
                textBoxComentarii.Focus();
                errorProviderComentarii.SetError(textBoxComentarii, "Introduceti comentarii!");
            }
            else
                errorProviderComentarii.SetError(this.textBoxComentarii, String.Empty);
        }


        //---------------------------BUTON SALVARE RADIOGRAFII->SALVARE IN MSQL-------------------------------
        private void buttonAdaugareRadiografie_Click(object sender, EventArgs e)
        {
            try
            {
                string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
                Initial Catalog=DogzillaPaws;
                Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Radiografii ([Nume_pacient], [CNP_proprietar], [Data], [Nume_imagine], [Diagnostic],[Comentarii]) values (@npacient,@cnp,@data,@nume_imagine,@diagnostic,@comentarii)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@npacient", textBoxNumePacient.Text);
                sc.Parameters.AddWithValue("@cnp", textBoxCNP.Text);
                sc.Parameters.AddWithValue("@data", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@nume_imagine", textBoxImage.Text);
                sc.Parameters.AddWithValue("@diagnostic", textBoxDiagnostic.Text);
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

        //button brows images
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string numeImg = Path.GetFileName(dlg.FileName);
                    textBoxImage.Text = numeImg;
                    string cale = Path.GetFullPath(dlg.FileName);
                    string dest = Application.StartupPath + "\\Radiografii\\" + numeImg;
                    File.Copy(cale, dest);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare " + ex.Message);
            }
        }

      

       
    }
}
