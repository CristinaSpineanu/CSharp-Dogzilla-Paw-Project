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
    public partial class AdaugarePacient : Form
    {
        public AdaugarePacient()
        {
            InitializeComponent();
        }

        //Validare pentru numele pacientului(Error provider)
        private void textBox10_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox10.Text))
            {
                textBox10.Focus();
                errorProviderNume.SetError(textBox10, "Va rog introduceti un nume valid!");
            }
            else
                errorProviderNume.SetError(this.textBox10, String.Empty);
        }

        //Validare sexului pacientului(Error provider)
        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                comboBox1.Focus();
                errorProviderSex.SetError(comboBox1, "Va rog selectati sexul pacientului!");
            }
            else
                errorProviderSex.SetError(this.comboBox1, String.Empty);

        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.Focus();
                errorProviderVarsta.SetError(textBox7, "Selectati data nasterii,varsta va fi completata automat!");
            }
            else
                errorProviderVarsta.SetError(this.textBox7, String.Empty);
        }

        //Validare rasa pacientului(Error provider)
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProviderRasa.SetError(textBox1, "Va rog introduceti rasa pacientului!");
            }
            else
                errorProviderRasa.SetError(this.textBox1, String.Empty);
        }

        //Validare camp vaccinare_zi (Error provider)
        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                textBox6.Focus();
                errorProviderVaccinareZi.SetError(textBox6, "Va rog complectati acest camp!");
            }
            else
                errorProviderVaccinareZi.SetError(this.textBox6, String.Empty);
        }

        //Validare CNP proprietar (Error provider)
        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProviderCNP.SetError(textBox2, "Va rog introduceti un CNP valid!");
            }
            else
                errorProviderCNP.SetError(this.textBox2,String.Empty);
        }

        //Validare prenume proprietar(Error provider)
        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.Focus();
                errorProviderPrenumeProprietar.SetError(textBox4, "Va rog introduceti prenumele proprietarului!");
            }
            else
                errorProviderPrenumeProprietar.SetError(this.textBox4, String.Empty);
        }

        //Validare nume proprietar(Error provider)
        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox4.Focus();
                errorProviderNumeProprietar.SetError(textBox3, "Va rog introduceti numele proprietarului!");
            }
            else
                errorProviderNumeProprietar.SetError(this.textBox3, String.Empty);
        }

        //Validare camp antecedente(Error provider)
        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.Focus();
                errorProviderAntecedente.SetError(textBox5, "Va rog complectati acest camp!");
            }
            else
                errorProviderAntecedente.SetError (this.textBox5, String.Empty);
        }

        //calcul varsta pacient 
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            var age = today.Year - dateTimePicker1.Value.Year;
            if (dateTimePicker1.Value.Date > today.AddYears(-age)) age--;
            textBox7.Text = Convert.ToString(age);
        }

        //functie verificare CNP
        public static bool verificCNP(string cnp)
        {
            int s, a1, a2, l1, l2, z1, z2, j1, j2, n1, n2, n3, cifc, u;
            if (cnp.Trim().Length != 13) return false;
            else
            {
                s = Convert.ToInt16(cnp.Substring(0, 1));
                a1 = Convert.ToInt16(cnp.Substring(1, 1));
                a2 = Convert.ToInt16(cnp.Substring(2, 1));
                l1 = Convert.ToInt16(cnp.Substring(3, 1));
                l2 = Convert.ToInt16(cnp.Substring(4, 1));
                z1 = Convert.ToInt16(cnp.Substring(5, 1));
                z2 = Convert.ToInt16(cnp.Substring(6, 1));
                j1 = Convert.ToInt16(cnp.Substring(7, 1));
                j2 = Convert.ToInt16(cnp.Substring(8, 1));
                n1 = Convert.ToInt16(cnp.Substring(9, 1));
                n2 = Convert.ToInt16(cnp.Substring(10, 1));
                n3 = Convert.ToInt16(cnp.Substring(11, 1));
                cifc = Convert.ToInt16(((s * 2 + a1 * 7 + a2 * 9 + l1 * 1 + l2 * 4 + z1 * 6 + z2 * 3 + j1 * 5 + j2 * 8 + n1 * 2 + n2 * 7 + n3 * 9) % 11));
                if (cifc == 10) { cifc = 1; }
                u = Convert.ToInt16(cnp.Substring(12, 1));
                if (cifc == u) return true;
                else return false;
            }
        }

        //Butonul de salvare pacient
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                string connect = @"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;
                Initial Catalog=DogzillaPaws;
                Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Pacienti ([Nume_pacient], [Sex], [Data_nasterii], [Varsta], [Rasa],[Vaccinare_zi],[CNP_proprietar],[Prenume_proprietar], [Nume_proprietar],[Antecedente]) values (@npacient,@s,@data,@varsta,@rasa,@vaccinare,@cnp,@prenumeP,@numeP,@antecedent)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@npacient", textBox10.Text);
                sc.Parameters.AddWithValue("@s", comboBox1.Text);
                sc.Parameters.AddWithValue("@data", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@varsta", textBox7.Text);
                sc.Parameters.AddWithValue("@rasa", textBox1.Text);
                sc.Parameters.AddWithValue("@vaccinare", textBox6.Text);
                sc.Parameters.AddWithValue("@cnp", textBox2.Text);
                sc.Parameters.AddWithValue("@prenumeP", textBox4.Text);
                sc.Parameters.AddWithValue("@numeP", textBox3.Text);
                sc.Parameters.AddWithValue("@antecedent", textBox5.Text);
                sc.ExecuteNonQuery();
                cnn.Close();
                this.DialogResult = DialogResult.OK;
                this.Hide();
                MessageBox.Show("Pacientul a fost adaugat cu succes!");
                Form f = new Pacienti();
                f.ShowDialog();

            }
            else
            {
                MessageBox.Show("Nu ati introdus CNP-ul!");
            }
        }


        //button Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AdaugarePacient_Load(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
