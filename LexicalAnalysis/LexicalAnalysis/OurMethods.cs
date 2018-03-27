using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

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
            entrada = File.Open(codePath, FileMode.Open);
            readText = new StreamReader(entrada);

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
            int currentState = 1;                               //Nosso estado inicial é o 1
            int countLine = 1, countColumn = 1;                 //Contadores de linha e coluna
            StringBuilder completeWord = new StringBuilder();   //String que será incrementada com os caracteres lidos

            if (File.Exists(codePath))
            {
                do
                {
                    char currentCharacter = (char)readText.Peek();

                    switch (currentState)
                    {

                        case 1:     //É o estado inicial

                            if (isLineBreak(currentCharacter))              //Se for uma quebra de linha...
                            {
                                countColumn++;
                                completeWord.Clear();
                            }

                            else if (char.IsWhiteSpace(currentCharacter))  //Se for espaço em branco 
                            {
                                countLine++;
                                completeWord.Clear();
                            }

                            else if (currentCharacter.Equals("\t"))
                            {
                                countColumn = countLine + 3;
                                completeWord.Clear();
                            }
                            else if (char.IsLetter(currentCharacter))
                            {
                                currentState = 2;
                                completeWord.Append((char)readText.Read());    //Consome o caracter que somente foi lido pelo .Peek() e já adiciona no StringBiulder
                            }
                            else if(currentCharacter.Equals('{'))
                            {
                                currentState = 4;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('}'))
                            {
                                currentState = 5;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('='))
                            {
                                currentState = 6;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('!'))
                            {
                                currentState = 9;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('>'))
                            {
                                currentState = 11;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('<'))
                            {
                                currentState = 14;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('+'))
                            {
                                currentState = 17;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('-'))
                            {
                                currentState = 18;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('*'))
                            {
                                currentState = 19;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('('))
                            {
                                currentState = 20;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals(')'))
                            {
                                currentState = 21;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals(','))
                            {
                                currentState = 22;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals(';'))
                            {
                                currentState = 23;
                                readText.Read();
                            }
                            else if (currentCharacter.Equals('/'))
                            {
                                currentState = 24;
                                readText.Read();
                            }

                                break;

                        case 2:

                            if (char.IsLetterOrDigit(currentCharacter))
                            {
                                currentState = 2;
                                readText.Read();
                            } else
                            {
                                currentState = 3;
                                //Printar o token encontrado
                                //Não dá o .Read(), pois é necessário para ver qual caracter é o próximo
                            }

                            break;

                        case 3:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 4:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 5:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 6:

                            break;

                        case 7:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 8:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 9:

                            break;

                        case 10:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 11:

                            break;

                        case 12:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 13:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 14:

                            break;

                        case 15:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 16:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 17:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 18:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 19:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 20:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 21:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 22:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 23:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 24:

                            break;

                        case 25:
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a esecução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 26:

                            break;

                        case 27:

                            break;

                        default:

                            //if (char.IsWhiteSpace(currentCharacter))
                            //    completeWord.Clear();
        
                            break;


                    }
                } while (!readText.EndOfStream);


                readText.Close();
                entrada.Close();

            }
        }


        /// <summary>
        /// Método que verifica se o proximo caracter é uma quebra de linha 
        /// </summary>
        /// <param name="c">Caracter atual que foi lido pelo arquivo</param>
        /// <returns>True se o caracter atual for quebra de linha</returns>
        public static bool isLineBreak(char c)
        {
            if (c.Equals("\n") || c.Equals("\b"))
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
        public static string flagError(int line, int column)
        {
            return "Erro encontrado na " + currentLineAndColumn(line, column);
        }
    }
}

