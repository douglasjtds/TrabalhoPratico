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
            lerArquivo("ErrorCase1.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lerArquivo("ErrorCase2.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lerArquivo("ErrorCase3.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lerArquivo("SuccessCase1.txt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lerArquivo("SuccessCase2.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lerArquivo("SuccessCase3.txt");
        }


        #region [Métodos]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lerArquivo"></param>
        /// <param name="nomeArquivo">Método usado para ler o arquivo de acordo com o </param>
        public void lerArquivo(String nomeArquivo)
        {
            string codePath = Path.Combine(@Environment.CurrentDirectory, nomeArquivo);

            if (File.Exists(codePath))
            {
                Stream entrada = File.Open(codePath, FileMode.Open);
                StreamReader readText = new StreamReader(entrada);
                string str = readText.ReadToEnd();

                MessageBox.Show(str);

                readText.Close();
                entrada.Close();
            }
        }

        #endregion

    }
}
