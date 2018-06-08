using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LexicalAnalysis
{
    partial class SeeTokens : Form
    {
        
        IEnumerable ListaToken;
        
        public SeeTokens(IEnumerable ListaToken, Form Form1)
        {
            this.ListaToken = ListaToken;

            InitializeComponent();

            if (ListaToken is IList)
            {
                foreach (String auxToken in ListaToken)
                {
                    logBox.AppendText("\r\n");
                    logBox.AppendText("\r\n" + auxToken);
                }
            }
            else if(ListaToken is IDictionary)
            {
                var NewListaToken = (Dictionary<Token, string>)ListaToken;

                foreach(Token token in NewListaToken.Keys)
                {
                    logBox.AppendText("\r\n");
                    logBox.AppendText("\r\n" + token.ToString());
                }
            }

        } 


        private void SeeTokens_Load(object sender, EventArgs e)
        {

        }
    }
}
