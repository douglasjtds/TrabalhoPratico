﻿using System;
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

            entrada = File.Open(codePath, FileMode.Open);
            readText = new StreamReader(entrada);
            SymbolTable ST = new SymbolTable();
            Token aux;

            int currentState = 1;                               //Nosso estado inicial é o 1
            int countLine = 1, countColumn = 1;                 //Contadores de linha e coluna
            StringBuilder completeWord = new StringBuilder();   //String que será incrementada com os caracteres lidos

            if (File.Exists(codePath))
            {
                do
                {
                    char currentCharacter = (char)readText.Peek();              //Pega o proximo caracter sem comsumir ele

                    switch (currentState)
                    {

                        case 1:     //É o estado inicial

                            if (isLineBreak(currentCharacter, countColumn, countLine))                  //Se for uma quebra de linha...
                            {
                                completeWord.Clear();
                            }

                            else if (char.IsWhiteSpace(currentCharacter))       //Se for espaço em branco...
                            {
                                countColumn++;
                                completeWord.Clear();
                            }

                            else if (currentCharacter.Equals("\t"))             //Se for tabulação...
                            {
                                countColumn = countColumn + 3;
                                completeWord.Clear();
                            }
                            else if (char.IsLetter(currentCharacter))
                            {
                                countColumn++;
                                currentState = 2;
                                completeWord.Append((char)readText.Read());    //Consome o caracter que somente foi lido pelo .Peek() e já adiciona no StringBiulder
                            }
                            else if(currentCharacter.Equals('{'))
                            {
                                currentState = 4;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('}'))
                            {
                                currentState = 5;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('='))
                            {
                                currentState = 6;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('!'))
                            {
                                currentState = 9;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('>'))
                            {
                                currentState = 11;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('<'))
                            {
                                currentState = 14;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('+'))
                            {
                                currentState = 17;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('-'))
                            {
                                currentState = 18;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('*'))
                            {
                                currentState = 19;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('('))
                            {
                                currentState = 20;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(')'))
                            {
                                currentState = 21;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(','))
                            {
                                currentState = 22;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals(';'))
                            {
                                currentState = 23;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (currentCharacter.Equals('/'))
                            {
                                currentState = 24;
                                completeWord.Append((char)readText.Read());
                            }

                                break;

                        case 2:

                            if (char.IsLetterOrDigit(currentCharacter))
                            {
                                //currentState = 2; //não precisa pq ele já está no estado 2
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                            } else
                            {
                                currentState = 3;
                            }

                            break;

                        case 3:         //ACHOU ID

                            aux = ST.isLexemaOnSymbolTable(Tag.ID, completeWord.ToString() , countLine, countColumn);     //Verifica se já tem esse lexema na ST. Se tiver, retorna ele. Se não tiver, adiciona ele.
                            MessageBox.Show(aux.ToString() + currentLineAndColumn(countLine, countColumn));               //PRINTA!
                                
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 4:         //ACHOU {         
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 5:         //ACHOU }
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 6:

                            break;

                        case 7:         //ACHOU ==
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 8:         //ACHOU =
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 9:

                            break;

                        case 10:        //ACHOU !=
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 11:

                            break;

                        case 12:        //ACHOU >=
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 13:        //ACHOU >
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 14:

                            break;

                        case 15:        //ACHOU <=
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 16:        //ACHOU <
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 17:        //ACHOU +
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 18:        //ACHOU -        
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 19:        //ACHOU *
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 20:        //ACHOU (
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 21:        //ACHOU )
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 22:        //ACHOU ,
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 23:        //ACHOU ;
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 24:

                            break;

                        case 25:        //ACHOU /  (SIMBOLO DIVISÃO)
                            //Printar o token encontrado com isFinalState
                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            break;

                        case 26:

                            break;

                        case 27:

                            break;

                        case 28:        //ACHOU //  (COMENTARIO)

                            break;

                        case 29:        

                            break;

                        case 30:        //ACHOU "" (STRING)

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
        public static bool isLineBreak(char c, int countColoumn, int countLine)
        {
            if (c.Equals("\n") || c.Equals("\b"))
            {
                countColoumn = 0;
                countLine++; 
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

