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


        public bool isFinalState(int currentState)
        {
            if(currentState == 3 || currentState == 4 || 
               currentState == 5 || currentState == 7 || 
               currentState == 8 || currentState == 10 || 
               currentState == 12 || currentState == 13 || 
               currentState == 15 || currentState == 16 || 
               currentState == 17 || currentState == 18 || 
               currentState == 19 || currentState == 20 ||
               currentState == 21 || currentState == 22 ||
               currentState == 23 || currentState == 25)
            {
                return true;
            }
            return false;
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
            int currentState = 1;   //Nosso estado inicial é o 1
            StringBuilder completeWord = new StringBuilder();

            if (File.Exists(codePath))
            {
                do
                {
                    char currentCharacter = (char)readText.Peek();

                    switch (currentState)
                    {

                        case 1:     //Seria o estado inicial

                            if (char.IsLetter(currentCharacter))
                            {
                                currentState = 2;
                                readText.Read();    //Consome o caracter que somente foi lido pelo .Peek();
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
                                //Printa o token encontrado
                                //Não dá o .Read(), pois é necessário para ver qual caracter é o próximo
                            }

                            break;

                        case 3:

                            break;

                        case 4:

                            break;

                        default:

                            if (!char.IsWhiteSpace(currentCharacter))
                            {  //Senão for um espaço em branco, adiciona na StringBiulder
                                completeWord.Append(currentCharacter);
                            }
                            else
                            {
                                MessageBox.Show(completeWord.ToString());
                                completeWord.Clear();
                            }
                            break;


                    }
                } while (!readText.EndOfStream);


                readText.Close();
                entrada.Close();

            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Current Character">Caracter atual que foi lido pelo arquivo</param>
        /// <returns>True se a caracter atual for quebra de linha</returns>
        public bool isLineBreak(char c)
        {
            if (c.Equals("\n"))
            {
                return true;
            }

            return false;
        }

        public bool isComment()
        {
            return false;
        }

    }
}

