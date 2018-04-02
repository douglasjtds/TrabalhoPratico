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
            string CodePath = OurMethods.lerArquivo("ErrorCase1.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string CodePath = OurMethods.lerArquivo("ErrorCase2.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string CodePath = OurMethods.lerArquivo("ErrorCase3.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string CodePath = OurMethods.lerArquivo("SuccessCase1.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string CodePath = OurMethods.lerArquivo("SuccessCase2.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string CodePath = OurMethods.lerArquivo("SuccessCase3.txt", Entrada, ReadText);
            OurMethods.performsAutomaton(CodePath, Entrada, ReadText);
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
