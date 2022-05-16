using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proiect_MTP
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        //functie citire date de autentificare din fisierul utilizatori.txt si face verificarea parolei introduse cu cea din .txt ,
        ////trimitand utilizatorul la forma Pacienti in cazul indeplinirii celor 2

        private void Autentificare_Load(object sender, EventArgs e)
        {
            string[] utilizatori = File.ReadAllLines("utilizatori.txt");
            foreach (var line in utilizatori)
            {
                string[] inregistrare = line.Split(',');
                comboBox1.Items.Add(inregistrare[0]);
            }
        }
        private int incercari = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string[] utilizatori = File.ReadAllLines("utilizatori.txt");
            foreach (var line in utilizatori)
            {
                string[] inregistrare = line.Split(',');
                if ((comboBox1.Text).Equals(inregistrare[0]))
                {
                    if ((textBox1.Text.Trim()).Equals(inregistrare[1].Trim()))
                    {
                        this.Hide();
                        Form f = new Pacienti();
                        f.ShowDialog();
                    }
                    else
                    {
                        incercari++;
                        MessageBox.Show("Parola incorecta! Mai aveti " + (3 - incercari).ToString() + " incercari.");
                    }
                }
                if (incercari == 3)
                Application.Exit();
            }
        }
        //butonul de cancel
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //butonul care face trm la forma de Inregistrare
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Inregistrare();
            f.ShowDialog();
        }
    }
}
