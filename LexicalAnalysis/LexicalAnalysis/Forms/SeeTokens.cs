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

        /*  TRYING IMPLEMENT FOR A GENERIC LIST
        IEnumerable ListaToken;
        
        public SeeTokens(IEnumerable ListaToken, Form Form1)
        {
            this.ListaToken = ListaToken;

            InitializeComponent();

            if (ListaToken.GetType() is IList)
            {
                List<String> Lista = ListaToken.ToList();

                foreach (String auxToken in ListaToken)
                {
                    logBox.AppendText("\r\n");
                    logBox.AppendText("\r\n" + auxToken);
                }
            }
            else if(ListaToken.GetType() is IDictionary)
            {
                foreach(Token token in ListaToken)
                {
                    logBox.AppendText("\r\n");
                    logBox.AppendText("\r\n" + token.ToString());
                }
            }

        } 
             
             
        */

        private void SeeTokens_Load(object sender, EventArgs e)
        {

        }
    }
}
