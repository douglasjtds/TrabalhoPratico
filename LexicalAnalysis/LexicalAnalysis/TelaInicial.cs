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
using myExtension;

namespace LexicalAnalysis
{
    public partial class TelaInicial : Form
    {
        public string CodePath;                 //String que contem o caminho do arquivo
        public Stream Entrada;                  //Stream de abertura do arquivo
        public StreamReader ReadText;           //StreamReader de leitura do arquivo

        public TelaInicial()
        {

            InitializeComponent();
        }


        private void TelaInicial_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("ErrorCase1.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("ErrorCase2.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("ErrorCase3.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("SuccessCase1.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("SuccessCase2.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CodePath = OurMethods.readFile("SuccessCase3.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Criadores: " + "\n" + "Douglas Tertuliano" + "\n" + "Matheus Pires");
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/douglasjtds/TrabalhoPraticoCompiladores");
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text.Equals("Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt"))
            {
                textBox1.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CodePath = textBox1.Text;
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = @"C:\Users\Douglas Tertuliano\Desktop";
            openFileDialog1.Title = "Arquivo do programa em PasC para ser executado:";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
            }


        }
    }
}
