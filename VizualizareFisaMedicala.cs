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
    public partial class VizualizareFisaMedicala : Form
    {
        string cnp, nume_pacient;
        public VizualizareFisaMedicala(string cnp_selectat, string nume_selectat)
        {
            InitializeComponent();
            cnp = cnp_selectat;
            nume_pacient = nume_selectat;
        }


        private void VizualizareFisaMedicala_Load(object sender, EventArgs e)
        {
            textBoxCNP.Text = cnp;
            textBoxNumePacient.Text = nume_pacient;
            flowLayoutPanel1.Controls.Clear();
            //+ "where CNP_proprietar=' " + cnp + "'"
            // where Nume_pacient = ',CNP_proprietar='" + nume_pacient + "'" + cnp + "'";
            try
            {
                //----------------------------------------------------
                //Introducere date din database Pacienti
                SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-UFFDJDC\SQLEXPRESS;Initial Catalog=DogzillaPaws;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("select * from Pacienti where  CNP_proprietar='" + cnp+ "'" , cnn);
                cnn.Open();
                SqlDataReader dgz = cmd.ExecuteReader();
                while (dgz.Read())
                {
                    textBoxNumePacient.Text = dgz["Nume_pacient"].ToString();
                    textBoxSex.Text = dgz["Sex"].ToString();
                    dateTimePicker1.Text = dgz["Data_nasterii"].ToString();
                    textBoxVarsta.Text = dgz["Varsta"].ToString();
                    textBoxRasa.Text = dgz["Rasa"].ToString();
                    textBoxVaccinare.Text= dgz["Vaccinare_zi"].ToString() ;
                    textBoxPrenume.Text = dgz["Prenume_proprietar"].ToString();
                    textBoxNume.Text = dgz["Nume_proprietar"].ToString();
                    textBoxAntecedente.Text = dgz["Antecedente"].ToString();

                }
                dgz.Close();

                //----------------------------------------------------
                //Introducere date din database Consultatii
                SqlDataAdapter da = new SqlDataAdapter("select * from Consultatii where CNP_proprietar='" + cnp + "'", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Consultatii");
                dataGridConsultatii.DataSource = ds.Tables["Consultatii"].DefaultView;

                //---------------------------------------------------------
                //Introducere date din database Radiografii 
                int index = 0;
                SqlCommand sc = new SqlCommand("select * from Radiografii where CNP_proprietar='" + cnp + "'", cnn);
                  dgz = sc.ExecuteReader();
                  while (dgz.Read())
                  {
                      string poza = dgz["nume_imagine"].ToString();
                      string cale_poza = Application.StartupPath + "\\Radiografii\\" + poza;

                      PictureBox pb = new PictureBox(); //genereare poza

                      //setare proprietatie poze
                      pb.Name = "Picture" + index.ToString();
                      pb.SetBounds(0, 0, 90, 70);
                      pb.BackColor = Color.Black;
                      pb.SizeMode = PictureBoxSizeMode.Zoom;
                      pb.Image = Bitmap.FromFile(cale_poza);
                      pb.Tag = index++;

                      flowLayoutPanel1.Controls.Add(pb);
                      pb.Click += Pb_Click;
                  }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pictureBoxRadiografii.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRadiografii.Image = pic.Image;
            pic.BorderStyle = BorderStyle.Fixed3D;
        }

        //button OK->back form Pacienti
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Pacienti();
            f.ShowDialog();
        }

     
    }
}
