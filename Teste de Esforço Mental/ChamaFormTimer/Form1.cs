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
using System.Drawing.Drawing2D;
using System.IO;

namespace ChamaFormTimer
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private string nomearquivo = "";
        public Form1()
        {
            InitializeComponent();
        }

        /*public Form1(string texto)
        {
            InitializeComponent();
            hots = texto;
        }

            string hots;
        */
        int contador = 1;
                                      
        private void button2_Click(object sender, EventArgs e)
        {
            Comecar();
        }

        private void Comecar()
        {
            if (string.IsNullOrEmpty(textBox1.Text)) //verifica se existe algo digitado na caixa de texto
            {
                MessageBox.Show("Informe seu nome antes de começar!");
                return;
            }

            saveFileDialog1.Title = "TESTE DE ESFORÇO MENTAL"; //define o titulo
            saveFileDialog1.Filter = "Text File|.txt"; //Define as extensões permitidas            
            saveFileDialog1.FilterIndex = 0; //define o indice do filtro            
            saveFileDialog1.FileName = textBox1.Text + DateTime.Now.ToString("ddMMyyyy_HHmmss"); //Atribui um valor vazio ao nome do arquivo
            saveFileDialog1.DefaultExt = ".txt"; //Define a extensão padrão como .txt
            saveFileDialog1.InitialDirectory = @"C:\Teste_Mental"; //define o diretório padrão
            saveFileDialog1.RestoreDirectory = true; //restaura o diretorio atual antes de fechar a janela


            nomearquivo = @"C:\Teste_Mental\" + textBox1.Text + DateTime.Now.ToString(" dd-MM-yyyy_HH-mm-ss") + ".txt";
            FileStream fs = new FileStream(nomearquivo, FileMode.Create); //Cria um stream usando o nome do arquivo
            StreamWriter writer = new StreamWriter(fs); //Cria um escrito que irá escrever no stream
            writer.WriteLine("Nome do participante: " + textBox1.Text);
            //writer.WriteLine("------------------------------------------------------------------------");
            writer.WriteLine("TipeSample, N, StartSample, StopSample, Test Time, Hits"); //Cria o cabeçalho
            //writer.WriteLine("------------------------------------------------------------------------");
            writer.Close(); //fecha o escrito e o stream

            button2.Enabled = false;
            textBox1.Enabled = false;

            button1.Visible = true;
            button3.Visible = true;
            //pictureBox6.Visible = true;
            label4.Visible = true;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            f2();                  
        }
        private void button3_Click(object sender, EventArgs e)
        {
            f3();
        }
        //public string[] arrayhits = new string[5];
        public void f2()
        {
            int loop = 0;            
            while (loop < 5)
            {                
                StartOpen();
                Form2 chamaprox = new Form2();
                FileStream fs = new FileStream(nomearquivo, FileMode.Append); //Cria um stream usando o nome do arquivo                                
                StreamWriter writer = new StreamWriter(fs); //Cria um escrito que irá escrever no stream                
                DateTime ini = DateTime.Now;
                writer.Write("Adicione-1, " + contador + ", " + ini.ToString("HH:mm:ss:mm"));// Escreve o conteúdo na caixa de texto do stream                
                loop++;
                chamaprox.ShowDialog();
                DateTime fim = DateTime.Now;
                writer.WriteLine(", " + fim.ToString("HH:mm:ss:mmm") + ", " + fim.Subtract(ini).TotalMilliseconds.ToString() + ", " + chamaprox.hits.ToString());// Escreve o conteúdo na caixa de texto do stream                
                contador++;
                writer.Close();
            }
            SetForegroundWindow(this.Handle);
            MessageBox.Show("Parabéns, você concluiu o teste ADICIONE-1 com sucesso!");
            return;       
            
        }

        public void f3()
        {
            int loop = 0;
            while (loop < 5)
            {
                StartOpen();
                Form3 chamaprox = new Form3();
                FileStream fs = new FileStream(nomearquivo, FileMode.Append); //Cria um stream usando o nome do arquivo                                
                StreamWriter writer = new StreamWriter(fs); //Cria um escrito que irá escrever no stream                
                DateTime ini = DateTime.Now;
                writer.Write("Adicione-3, " + contador + ", " + ini.ToString("HH:mm:ss:mm"));// Escreve o conteúdo na caixa de texto do stream                
                loop++;
                chamaprox.ShowDialog();
                DateTime fim = DateTime.Now;
                writer.WriteLine(", " + fim.ToString("HH:mm:ss:mmm") +", " + fim.Subtract(ini).TotalMilliseconds.ToString() + ", " + chamaprox.hits.ToString());// Escreve o conteúdo na caixa de texto do stream                
                contador++;
                writer.Close();

            }
            SetForegroundWindow(this.Handle);
            MessageBox.Show("Parabéns, você concluiu o teste ADICIONE-3 com sucesso!");
            return;

        }

        

        public void StartOpen()
        {
            Process p = Process.GetProcessesByName("javaw")[0];
            IntPtr pointer = p.MainWindowHandle;
            SetForegroundWindow(pointer);
            SendKeys.Send("b");
            SendKeys.Send("{enter}");                       
        }
        public void StopOpen() 
        {
            Process p = Process.GetProcessesByName("javaw")[0];
            IntPtr pointer = p.MainWindowHandle;
            SetForegroundWindow(pointer);
            SendKeys.Send("s");
            SendKeys.Send("{enter}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateFolder();                        
        }
        
        public void CreateFolder()
        {
            string folderName = @"C:\Teste_Mental";            
            string pathString = System.IO.Path.Combine(folderName);            
            System.IO.Directory.CreateDirectory(pathString);
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }                                 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Comecar();
            }
        }
    }
}
