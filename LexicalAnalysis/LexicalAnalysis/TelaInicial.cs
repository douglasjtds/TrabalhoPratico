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
        public TelaInicial() { 
        
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

            string codePath = Path.Combine(Environment.CurrentDirectory, "ErrorCase1.txt");

            //MessageBox.Show(codePath);
            Console.WriteLine(codePath);

            if (File.Exists(codePath))
            {
                Stream entrada = File.Open(codePath, FileMode.Open);
                StreamReader readText = new StreamReader(entrada);
                readText.ReadToEnd();
               
                MessageBox.Show(readText.ReadLine());

                readText.Close();
                entrada.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string codePath = Path.Combine(Environment.CurrentDirectory, "ErrorCase2.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string codePath = Path.Combine(Environment.CurrentDirectory, "ErrorCase3.txt"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string codePath = Path.Combine(Environment.CurrentDirectory, "SuccessCase1.txt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string codePath = Path.Combine(Environment.CurrentDirectory, "SuccessCase2.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string codePath = Path.Combine(Environment.CurrentDirectory, "SuccessCase3.txt");
        }
    }
}
