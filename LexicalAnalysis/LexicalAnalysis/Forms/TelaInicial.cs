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
        string CodePath;                 //String que contem o caminho do arquivo
        Stream Entrada;                  //Stream de abertura do arquivo
        StreamReader ReadText;           //StreamReader de leitura do arquivo
        Token tokenAux;
        List<Token> TokenList = new List<Token>();
        List<String> OutputSet = new List<String>();
        SymbolTable ST = new SymbolTable();

        public TelaInicial()
        {
            InitializeComponent();
        }


        private void TelaInicial_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();

            CodePath = Lexer.readFile("ErrorCase1.txt");
            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                    if (tokenAux != null)
                    {
                        TokenList.Add(tokenAux);
                        //MessageBox.Show(tokenAux.ToString());
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("ErrorCase2.txt");

            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    if (tokenAux != null)
                    {
                        tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("ErrorCase3.txt");

            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    if (tokenAux != null)
                    {
                        tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase1.txt");

            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    if (tokenAux != null)
                    {
                        tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase2.txt");

            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    if (tokenAux != null)
                    {
                        tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase3.txt");

            Entrada = File.Open(CodePath, FileMode.Open);
            ReadText = new StreamReader(Entrada);
            SymbolTable ST = new SymbolTable();

            if (File.Exists(CodePath))
            {
                do
                {
                    if (tokenAux != null)
                    {
                        tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            Lexer.CloseFile(Entrada, ReadText);

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
                textBox1.Text = "";
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            textBox1.Text = "Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                TokenList.Clear();

                CodePath = textBox1.Text;
                textBox1.Text = "Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt";

                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                SymbolTable ST = new SymbolTable();

                if (File.Exists(CodePath))
                {
                    do
                    {
                        if (tokenAux != null)
                        {
                            tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                            TokenList.Add(tokenAux);
                        }
                    }
                    while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
                }

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();

                Lexer.CloseFile(Entrada, ReadText);

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Caminho informado inválido");

            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Caminho informado inválido");

            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Arquivo do programa em PasC para ser executado:",
                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                CodePath = fileName;
                TokenList.Clear();

                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                SymbolTable ST = new SymbolTable();

                if (File.Exists(CodePath))
                {
                    do
                    {
                        if (tokenAux != null)
                        {
                            tokenAux = Lexer.performsAutomaton(Entrada, ReadText, OutputSet, ST);
                            TokenList.Add(tokenAux);
                        }
                    }
                    while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
                }

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();

                Lexer.CloseFile(Entrada, ReadText);

            }
        }

        private void textBox1_ModifiedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt";
        }

        private void TelaInicial_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();
        }
    }
}
