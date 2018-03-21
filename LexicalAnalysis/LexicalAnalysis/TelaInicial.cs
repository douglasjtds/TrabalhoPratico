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
        public TelaInicial()
        {

            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("ErrorCase1.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("ErrorCase2.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("ErrorCase3.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("SuccessCase1.txt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("SuccessCase2.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OurMethods.lerArquivo("SuccessCase3.txt");
        }
    }
}
