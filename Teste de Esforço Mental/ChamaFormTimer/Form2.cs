using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace ChamaFormTimer
{
    public partial class Form2 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);        
        public Form2()
        {
            InitializeComponent();
        }
        int tempo = 3;
        int tempo2 = 0;
        Random rnd = new Random();
                
        private void Form2_Load(object sender, EventArgs e) //Começa gravãção no OpenBCI e inicia Teste
        {            
            timer1.Enabled = true;
            timer1.Start();
            SetForegroundWindow(this.Handle);
        }


        private void timer1_Tick(object sender, EventArgs e) // Contagem regressiva para iniciar teste
        {

            if (tempo == 0)
            {
                timer1.Stop();
                sample();
            }
            else
            {
                label1.Text = tempo.ToString(); //imprime o valor do tempo
                tempo--;
            }
        }
        string[] arraysample = new string[4];
        string[] arrayanswer = new string[4];
        public void sample() //Criação de amostra aleatória
        {            
            label1.Text = "";             
            arraysample[0] = rnd.Next(0, 10).ToString();
            arraysample[1] = rnd.Next(0, 10).ToString();
            arraysample[2] = rnd.Next(0, 10).ToString();
            arraysample[3] = rnd.Next(0, 10).ToString();
            label2.Text = arraysample[0];
            label3.Text = arraysample[1];
            label4.Text = arraysample[2];
            label5.Text = arraysample[3];
            this.timer2.Start();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arrayanswer[0] = textBox1.Text;
            textBox2.Select();
        }        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            arrayanswer[1] = textBox2.Text;
            textBox3.Select();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            arrayanswer[2] = textBox3.Text;
            textBox4.Select();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            arrayanswer[3] = textBox4.Text;
            StopOpen();
            SetForegroundWindow(this.Handle);
            resultado();
            button1.Visible = true;
            button1.Enabled = true;
            label6.Visible = true;
            label8.Visible = true;
            label7.Visible = true;
            label7.Text = hits.ToString(); // Imprime resultado na tela
            //Form1 retornahits = new Form1(hits.ToString());
            

                        
        }  
        private void progressBar1_Click(object sender, EventArgs e)
        {
            this.timer2.Start();
            
        }
        private void timer2_Tick(object sender, EventArgs e) //Barra de progresso e troca de label para textBox
        {
            //this.progressBar1.Increment(4); //4 * 50 milisgundos * 100 frames = 2 segundos

            if (tempo2 >= 2000)
            {
                timer2.Stop();
                progressBar1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;               
                textBox3.Visible = true;                               
                textBox4.Visible = true;                
                textBox1.Select();

            }
            else
            {
                progressBar1.Visible = true;
                this.progressBar1.Increment(1); //4 * 50 milisgundos * 100 frames = 2 segundos
                tempo2 = tempo2 + 25;
            }
        }
        void StopOpen()
        {
            Process p = Process.GetProcessesByName("javaw")[0];
            IntPtr pointer = p.MainWindowHandle;
            SetForegroundWindow(pointer);
            SendKeys.Send("s");
            SendKeys.Send("{enter}");
        }
        public int hits;
        void resultado()
        {
            hits = 0;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
                        
            if (Convert.ToInt32(arraysample[0]) + 1 == Convert.ToInt32(arrayanswer[0]) | (Convert.ToInt32(arraysample[0]) + 1 == 10 & Convert.ToInt32(arrayanswer[0]) == 0))
            //if (a == b | a == 10)
            {
                textBox1.BackColor = Color.Green;
                hits++;
            }
            else
            {
                textBox1.BackColor = Color.Red;
            }
            if (Convert.ToInt32(arraysample[1]) + 1 == Convert.ToInt32(arrayanswer[1]) | (Convert.ToInt32(arraysample[1]) + 1 == 10 & Convert.ToInt32(arrayanswer[1]) == 0))
            //if (a == b | a == 10)
            {
                textBox2.BackColor = Color.Green;
                hits++;
            }
            else
            {
                textBox2.BackColor = Color.Red;
            }
            if (Convert.ToInt32(arraysample[2]) + 1 == Convert.ToInt32(arrayanswer[2]) | (Convert.ToInt32(arraysample[2]) + 1 == 10 & Convert.ToInt32(arrayanswer[2]) == 0))
            //if (a == b | a == 10)
            {
                textBox3.BackColor = Color.Green;
                hits++;
            }
            else
            {
                textBox3.BackColor = Color.Red;
            }
            if (Convert.ToInt32(arraysample[3]) + 1 == Convert.ToInt32(arrayanswer[3]) | (Convert.ToInt32(arraysample[3]) + 1 == 10 & Convert.ToInt32(arrayanswer[3]) == 0))
            //if (a == b | a == 10)
            {
                textBox4.BackColor = Color.Green;
                hits++;
            }
            else
            {
                textBox4.BackColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();            
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
