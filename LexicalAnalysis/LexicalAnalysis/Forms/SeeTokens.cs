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

        List<String> ListaToken = new List<String>();

        public SeeTokens(List<String> ListaToken, Form Form1)
        {
            this.ListaToken = ListaToken;

            InitializeComponent();

            foreach (String auxToken in ListaToken)
            {
                logBox.AppendText("\r\n");
                logBox.AppendText("\r\n" + auxToken);
            }
        }

        private void SeeTokens_Load(object sender, EventArgs e)
        {

        }
    }
}
