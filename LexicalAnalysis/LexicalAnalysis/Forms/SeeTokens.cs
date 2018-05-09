using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LexicalAnalysis
{
    partial class SeeTokens : Form
    {

        List<Token> ListaToken = new List<Token>();

        public SeeTokens(List<Token> ListaToken, Form Form1)
        {
            InitializeComponent();

            this.ListaToken = ListaToken;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
