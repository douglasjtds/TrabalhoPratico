using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using LexicalAnalysis;

namespace myExtension
{
    public class OurMethods
    {

        /// <summary>
        /// Método usado para ler o arquivo que seria o programa em PasC de acordo com o botão pressionado.      
        /// </summary>
        /// <param name="nomeArquivo">string com o nome do arquivo que será lido</param>
        /// <returns>Mensagem de teste com o conteúdo do arquivo.</returns>
        /// <remarks>Deve ser implementado quando for necessário ler um novo arquivo de programa PasC.</remarks>
        public static string lerArquivo(String nomeArquivo, Stream entrada, StreamReader readText)
        {
            string codePath = Path.Combine(@Environment.CurrentDirectory, nomeArquivo);
            return codePath;
        }


        /// <summary>
        /// Método utilizado para executar o autômato analisará o lexico da linguagem PasC
        /// </summary>
        /// <param name="codePath"></param>
        /// <param name="readText"></param>
        /// <param name="entrada"></param>
        /// <param name="completeWord"></param>
        /// <returns>Void</returns>
        /// <remarks>Deve ser chamado para iniciar a execução do autômato</remarks>
        public static void performsAutomaton(String codePath, Stream entrada, StreamReader readText)
        {
            const int END_OF_FILE = -1;
            int lookahead = 0;
            entrada = File.Open(codePath, FileMode.Open);
            readText = new StreamReader(entrada);
            SymbolTable ST = new SymbolTable();
            Token auxToken;
            char AuxChar;

            int currentState = 1;                               //Nosso estado inicial é o 1
            int countLine = 1, countColumn = 0;                 //Contadores de linha e coluna
            StringBuilder completeWord = new StringBuilder();   //String que será incrementada com os caracteres lidos
            char currentCharacter = '\u0000';

            if (File.Exists(codePath))
            {
                do
                {

                    // avança caractere
                    try
                    {
                        lookahead = readText.Peek();              //Pega o proximo caracter sem comsumir ele
                        if (lookahead != END_OF_FILE)
                        {
                            currentCharacter = (char)lookahead;
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Erro na leitura do arquivo. Mensagem: " + e);
                    }

                    switch (currentState)
                    {

                        case 1:     //É o estado inicial

                            if (lookahead == END_OF_FILE)
                            {
                                auxToken = ST.isLexemaOnSymbolTable(Tag.EOF, completeWord.ToString(), countLine, countColumn);
                                MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            }
                            else if (isLineBreak(currentCharacter, countColumn, countLine))
                            {
                                if (char.IsWhiteSpace(currentCharacter))
                                {
                                    countColumn++;
                                }
                                else if (currentCharacter.Equals('\n'))
                                {
                                    countColumn = 1;
                                    countLine++;

                                }
                                else if (currentCharacter.Equals('\t'))
                                {
                                    countColumn = +3;
                                    completeWord.Clear();
                                }

                            }

                            else if (char.IsLetter(currentCharacter))
                            {
                                countColumn++;
                                currentState = 2;
                                completeWord.Append((char)readText.Read());    //Consome o caracter que somente foi lido pelo .Peek() e já adiciona no StringBiulder
                            }
                            else if (char.IsDigit(currentCharacter))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                currentState = 31;
                            }
                            else if (currentCharacter.Equals('{'))
                            {
                                countColumn++;
                                currentState = 4;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('}'))
                            {
                                countColumn++;
                                currentState = 5;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('='))
                            {
                                countColumn++;
                                currentState = 6;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('!'))
                            {
                                countColumn++;
                                currentState = 9;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('>'))
                            {
                                countColumn++;
                                currentState = 11;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('<'))
                            {
                                countColumn++;
                                currentState = 14;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('+'))
                            {
                                countColumn++;
                                currentState = 17;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('-'))
                            {
                                countColumn++;
                                currentState = 18;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('*'))
                            {
                                countColumn++;
                                currentState = 19;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('('))
                            {
                                countColumn++;
                                currentState = 20;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(')'))
                            {
                                countColumn++;
                                currentState = 21;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(','))
                            {
                                countColumn++;
                                currentState = 22;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(';'))
                            {
                                countColumn++;
                                currentState = 23;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('/'))
                            {
                                countColumn++;
                                currentState = 24;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('"'))
                            {
                                countColumn++;
                                currentState = 29;
                                completeWord.Append((char)readText.Read());
                            }
                            else
                            {
                                flagError(completeWord.ToString(), countLine, countColumn);
                            }

                            break;

                        case 2:

                            if (char.IsLetterOrDigit(currentCharacter))
                            {
                                //currentState = 2; //não precisa pq ele já está no estado 2
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                            }
                            else
                            {
                                currentState = 3;
                            }

                            break;

                        case 3:         //ACHOU ID

                            auxToken = ST.isLexemaOnSymbolTable(Tag.ID, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));               //PRINTA!

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 4:         //ACHOU {         
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_OBC, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 5:         //ACHOU }
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_CBC, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 6:
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('='))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 7;
                            }
                            else
                            {
                                currentState = 8;
                            }

                            break;

                        case 7:         //ACHOU ==
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_EQ, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 8:         //ACHOU =
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_ASS, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 9:
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('='))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 10;
                            }
                            else
                            {
                                flagError(completeWord.ToString(), countLine, countColumn);
                            }
                            break;

                        case 10:        //ACHOU !=
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_NE, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 11:
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('='))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 12;
                            }
                            else
                            {
                                currentState = 13;
                            }

                            break;

                        case 12:        //ACHOU >=
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_GE, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 13:        //ACHOU >
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_GT, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 14:            // ACHOU < 
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('='))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 15;
                            }
                            else
                            {
                                currentState = 16;
                            }

                            break;

                        case 15:        //ACHOU <=
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_LE, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 16:        //ACHOU <
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_LT, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 17:        //ACHOU +
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_AD, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 18:        //ACHOU -
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_MIN, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 19:        //ACHOU *
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_MUL, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 20:        //ACHOU (
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_OPA, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 21:        //ACHOU )
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_CPA, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 22:        //ACHOU ,
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_COM, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 23:        //ACHOU ;
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_SEM, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            countColumn++;
                            break;

                        case 24:
                            AuxChar = (char)readText.Peek();
                            if (AuxChar.Equals('/'))
                            {
                                currentState = 28;
                            }
                            else if (AuxChar.Equals('*'))
                            {
                                currentState = 26;
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                //tem que fazer mais coisa aqui?
                            }
                            else
                            {
                                currentState = 25;
                            }

                            break;

                        case 25:        //ACHOU /  (SIMBOLO DIVISÃO)
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_DIV, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 26:
                            //regra de comentário de múltiplas linhas
                            AuxChar = (char)readText.Peek();
                            if (AuxChar.Equals('*'))
                            {
                                currentState = 27;
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                            }
                            else
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                            }
                            break;

                        case 27:
                            AuxChar = (char)readText.Peek();
                            if (AuxChar.Equals('/'))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 1;
                            }
                            else
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 26;
                            }
                            break;

                        case 28:        //ACHOU //  (COMENTARIO)
                            readText.ReadLine();
                            countLine++;
                            completeWord.Clear();
                            currentState = 1;
                            break;

                        case 29:
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('"'))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                currentState = 30;
                            }
                            else
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                //faltou a opção se vier outro
                            }

                            break;

                        case 30:        //ACHOU "" (STRING)

                            auxToken = ST.isLexemaOnSymbolTable(Tag.LIT, completeWord.ToString(), countLine, countColumn);
                            MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            break;

                        case 31:
                            //pode vir outro dígito ou '.'
                            if (char.IsDigit(currentCharacter))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                //e o que mais?
                            }
                            else if (currentCharacter.Equals('.'))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                currentState = 32;
                            }
                            else
                            {
                                //flagError();
                            }

                            break;

                        case 32:   //digito ou outro
                            if (char.IsDigit(currentCharacter))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                //e o que mais?
                            }
                            else
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                currentState = 33;
                            }

                            break;

                        case 33:

                            break;

                        case 34:

                            break;

                        default:

                            //if (char.IsWhiteSpace(currentCharacter))
                            //    completeWord.Clear();
                            flagError(completeWord.ToString(), countLine, countColumn);    // fiquei na dúvida se dá esse completeWord aqui   --------------------?????????

                            break;


                    }
                } while (true);

                CloseFile(entrada, readText);

            }
        }


        /// <summary>
        /// Método que verifica se o proximo caracter é uma quebra de linha 
        /// </summary>
        /// <param name="c">Caracter atual que foi lido pelo arquivo</param>
        /// <returns>True se o caracter atual for quebra de linha</returns>
        public static bool isLineBreak(char currentCharacter, int countColumn, int countLine)
        {
            if (currentCharacter.Equals('\n') || char.IsWhiteSpace(currentCharacter) || currentCharacter.Equals('\t'))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Método para verificar se o estado atual é um estado final. Caso for, printa qual foi o token encontrado
        /// </summary>
        /// <param name="currentState">Inteiro que representa o estado atual</param>
        public static void isFinalState(int currentState, int currentLine, int currentColumn)
        {
            if (currentState == 3 || currentState == 4 ||
               currentState == 5 || currentState == 7 ||
               currentState == 8 || currentState == 10 ||
               currentState == 12 || currentState == 13 ||
               currentState == 15 || currentState == 16 ||
               currentState == 17 || currentState == 18 ||
               currentState == 19 || currentState == 20 ||
               currentState == 21 || currentState == 22 ||
               currentState == 23 || currentState == 25)
            {
                //returnToken();                                    //Método que está dentro da classe Token e vai printar o token encontrado
                currentLineAndColumn(currentLine, currentColumn);   //Printa aonde esse token foi encontrado
                currentState = 1;                                   //Retorna para o estado inicial
            }
        }


        /// <summary>
        /// Método para sinalizar um erro e o lugar onde ele foi encontrado
        /// </summary>
        /// <param name="column">Representa a coluna atual que o leitor está</param>
        /// <param name="line">Representa a linha atual que o leitor está</param>
        /// <returns>Retorna a posição atual da linha e da coluna</returns>
        public static string currentLineAndColumn(int line, int column)
        {
            return " Linha " + line + ", Coluna " + column;
        }


        /// <summary>
        /// Método para sinalizar um erro e o lugar onde ele foi encontrado
        /// </summary>
        public static string flagError(string lexema, int line, int column)
        {
            return "Caracter " + lexema + "inesperado na " + currentLineAndColumn(line, column);
        }


        public static void CloseFile(Stream entrada, StreamReader readText)
        {
            try
            {
                readText.Close();
                entrada.Close();
            }
            catch (IOException errorFile)
            {
                Console.WriteLine("Erro ao fechar arquivo\n" + errorFile);
            }
        }

    }
}

