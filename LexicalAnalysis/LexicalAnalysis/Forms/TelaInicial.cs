using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using myExtension;
using SyntaxAnalysis;

namespace LexicalAnalysis
{
    public partial class TelaInicial : Form
    {
        string CodePath;                 //String que contem o caminho do arquivo
        Stream Entrada;                  
        StreamReader ReadText;           
        Token tokenAux;
        List<Token> TokenList = new List<Token>();
        List<String> OutputSet = new List<String>();
        SymbolTable ST = new SymbolTable();
        Parser parser;
        Lexer lexer;

        int column;
        int line;

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
            column = 1; line = 1;


            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            } 

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("ErrorCase2.txt");
            column = 1; line = 1;


            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("ErrorCase3.txt");
            column = 1; line = 1;

            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase1.txt");
            column = 1; line = 1;

            /*
            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            }*/

            if (File.Exists(CodePath))
            {

                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);

                do
                {
                    tokenAux = lexer.performsAutomaton();

                    if (tokenAux != null)
                    {
                        TokenList.Add(tokenAux);
                    }
                }
                while (tokenAux != null && tokenAux.Classe != LexicalAnalysis.Tag.EOF);
            }

            SeeTokens seeTokens = new SeeTokens(OutputSet, this);
            seeTokens.Show();

            lexer.CloseFile();
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase2.txt");
            column = 1; line = 1;


            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            } 

        }

        private void button6_Click(object sender, EventArgs e)
        { 
            TokenList.Clear();
            OutputSet.Clear();
            CodePath = Lexer.readFile("SuccessCase3.txt");
            column = 1; line = 1;


            if (File.Exists(CodePath))
            {
                Entrada = File.Open(CodePath, FileMode.Open);
                ReadText = new StreamReader(Entrada);
                ST = new SymbolTable();

                lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                parser = new Parser(lexer, OutputSet);

                parser.prog();

                SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                seeTokens.Show();
                parser.CloseFiles();
            }

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
                textBox1.Text = "Ex: C:\\Users\\Gustavo\\Desktop\\AlgoritmoPasC.txt";


                TokenList.Clear();
                OutputSet.Clear();
                CodePath = textBox1.Text;
                column = 1; line = 1;

                if (File.Exists(CodePath))
                {
                    Entrada = File.Open(CodePath, FileMode.Open);
                    ReadText = new StreamReader(Entrada);
                    ST = new SymbolTable();

                    lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                    parser = new Parser(lexer, OutputSet);

                    parser.prog();

                    SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                    seeTokens.Show();
                    parser.CloseFiles();
                }
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
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|pasc files (*.pasc)|*.pasc"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                CodePath = fileName;
                TokenList.Clear();
                OutputSet.Clear();
                column = 1; line = 1;

                if (File.Exists(CodePath))
                {
                    Entrada = File.Open(CodePath, FileMode.Open);
                    ReadText = new StreamReader(Entrada);
                    ST = new SymbolTable();

                    lexer = new Lexer(Entrada, ReadText, OutputSet, ST, line, column);
                    parser = new Parser(lexer, OutputSet);

                    parser.prog();

                    SeeTokens seeTokens = new SeeTokens(OutputSet, this);
                    seeTokens.Show();
                    parser.CloseFiles();
                }

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
            //Tem que mudar isso pra mostrar a ST ao inves do conjunto de saída


            //SeeTokens seeTokens = new SeeTokens(ST.ST, this);
            SeeTokens seeTokens = new SeeTokens(ST.ST, this);
            seeTokens.Show();
        }
    }
}
