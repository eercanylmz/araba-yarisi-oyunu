using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Web;

namespace araba_yarisi_oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int kazanılanPuan = 0;
        int yolHızı = 30;
        int arabaHızı = 30;


        bool sagYon = false;
        bool solYon = false;

        int digerarabahızı = 5;

        Random rnd = new Random();


        public void ooyunuBaslat()
        {
            button1.Enabled = false;
            carpma.Visible = false;

            label2.Text = Settings1.Default.YüksekSkor.ToString();
            kazanılanPuan = 0;
            arabaHızı = 10;
            digerarabahızı = 10;

            araba3.Left = 160;
            araba3.Top = 300;

            araba1.Left = 30;
            araba1.Top = 50;

            araba2.Left = 320;
            araba2.Top = 50;

            sagYon = false;
            solYon = false;

            timer1.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ooyunuBaslat();
            sesac();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ooyunuBaslat();
        }

        private void sesac()
        {
            SoundPlayer ses = new SoundPlayer();

            string sesyol = Application.StartupPath + "\\Ahmet-Kaya.wav";
            ses.SoundLocation = sesyol;
            ses.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            kazanılanPuan++;
            lbl_puan.Text = kazanılanPuan.ToString();

            yol.Top += yolHızı;
            if (yol.Top > 400)
            {
                yol.Top = -100;
            }

            if (solYon)
            {
                araba3.Left -= arabaHızı;
            }
            if (sagYon)
            {
                araba3.Left += arabaHızı; 
            }

            if (araba3.Left < 1)
            {
                solYon = false;
            }
            if (araba3.Left + araba3.Width > 450)
            {
                sagYon = false;
            }

            araba1 .Top+= arabaHızı;
            araba2.Top += arabaHızı;

            if (araba1.Top > panel1.Height)
            {
                arabaDegıstır();
                araba1.Left = rnd.Next(20, 50);
                araba1.Top = rnd.Next(40, 140) * -1;

            }
            if (araba2.Top > panel1.Height)
            {
                araba2Degıstır();
                araba2.Left = rnd.Next(100, 200);
                araba2.Top = rnd.Next(40, 140) * -1;
            }

            if (araba3.Bounds.IntersectsWith(araba2.Bounds) || araba3.Bounds.IntersectsWith(araba2.Bounds))
            {
                oyunuBıtır();
            }
        }
        private void arabaDegıstır()
        {
            int sıra = rnd.Next(1, 7);
            switch (sıra)
            {
                case 1:
                    araba1.Image = Properties.Resources.araba5;
                    break;
                case 2:
                    araba1.Image = Properties.Resources.araba7;
                    break;
                case 3:
                    araba1.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba1.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba1.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba1.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba1.Image = Properties.Resources.araba7;
                    break;
            }
        }
        private void araba2Degıstır()
        {
            int sıra = rnd.Next(1, 7);
            switch (sıra)
            {
                case 1:
                    araba2.Image = Properties.Resources.araba5;
                    break;
                case 2:
                    araba2.Image = Properties.Resources.araba7;
                    break;
                case 3:
                    araba2.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba2.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba2.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba2.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba2.Image = Properties.Resources.araba7;
                    break;
            }
        }
        private void oyunuBıtır()
        {
            timer1.Stop();
            if (Convert.ToInt32(lbl_puan.Text) > Convert.ToInt32(Settings1.Default.YüksekSkor.ToString()))
            {
                Settings1.Default.YüksekSkor =Convert.ToInt32( lbl_puan.Text);
            }
            button1.Enabled = true;
            carpma.Visible = true;
            araba3.Controls.Add(carpma);
            carpma.Location = new Point(7, -5);
            carpma.BringToFront();
            carpma.BackColor = Color.Transparent;
            MessageBox.Show("TEBRİKLER KAZANDIĞINIZ PUAN: "+lbl_puan,"BİLGİLENDİRME ",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && araba3.Left > 0)
            {
                solYon = true;
            }
            if (e.KeyCode == Keys.Right && araba3.Left + araba3.Width < panel1.Width)
            {
                sagYon = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { solYon = false; }
            if(e.KeyCode == Keys.Right) { sagYon = false; }
        }

        private void carpma_Click(object sender, EventArgs e)
        {

        }
    }
}
