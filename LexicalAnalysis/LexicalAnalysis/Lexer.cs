using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using LexicalAnalysis;
using System.Collections.Generic;

namespace myExtension
{
    /// <summary>
    /// Classe que contém todos os métodos utilizados durante a execução do analisador léxico
    /// </summary>
    public class Lexer
    {

        /// <summary>
        /// Método usado para ler o arquivo que seria o programa em PasC de acordo com o botão pressionado.      
        /// </summary>
        /// <param name="nomeArquivo">string com o nome do arquivo que será lido</param>
        /// <returns>Mensagem de teste com o conteúdo do arquivo.</returns>
        /// <remarks>Deve ser implementado quando for necessário ler um novo arquivo de programa PasC.</remarks>
        public static string readFile(String nomeArquivo)
        {
            string codePath = Path.Combine(@Environment.CurrentDirectory, nomeArquivo);
            return codePath;
        }


        /// <summary>
        /// Método utilizado para executar o autômato analisará o lexico da linguagem PasC
        /// </summary>
        /// <param name="readText"></param>
        /// <param name="entrada"></param>
        /// <param name="ST"></param>
        /// <param name="outputSet"></param>
        /// <returns>Token</returns>
        /// <remarks>Deve ser chamado para iniciar a execução do autômato</remarks>
        public static Token performsAutomaton(Stream entrada, StreamReader readText, List<String> outputSet, SymbolTable ST)
        {
            const int END_OF_FILE = -1;
            int lookahead = 0;

            try
            {
                Token auxToken;
                char AuxChar;
                StringBuilder StringPanicMode = new StringBuilder();
                char CharPanicMode = 'a';
                bool PanicError = false;

                int currentState = 1;                               //Nosso estado inicial é o 1
                int countLine = 1, countColumn = 0;                 //Contadores de linha e coluna
                StringBuilder completeWord = new StringBuilder();   //String que será incrementada com os caracteres lidos
                char currentCharacter = '\u0000';

                do
                {
                    try
                    {
                        lookahead = readText.Peek();                //Pega o proximo caracter sem comsumir ele
                        currentCharacter = (char)lookahead;         //Se não for o final do arquivo, a variável recebe o próximo caracter
                    }
                    catch (IOException e)
                    {
                        outputSet.Add("Erro na leitura do arquivo. Mensagem: " + e.StackTrace);
                        return null;
                    }

                    switch (currentState)
                    {

                        case 1:     //É o estado inicial

                            if (lookahead == END_OF_FILE)
                            {
                                completeWord.Append((char)readText.Read());
                                auxToken = ST.isLexemaOnSymbolTable(Tag.EOF, "END_OF_FILE", countLine, countColumn);
                                outputSet.Add(auxToken.ToString());
                                return auxToken;
                            }
                            else if (currentCharacter.Equals('\n') || char.IsWhiteSpace(currentCharacter) || currentCharacter.Equals('\t') || currentCharacter.Equals('\r'))
                            {
                                if (currentCharacter.Equals('\n'))
                                {
                                    countColumn = 1;
                                    countLine++;
                                    readText.Read();
                                }
                                else if (currentCharacter.Equals('\r'))
                                {
                                    readText.Read();
                                }
                                else if (currentCharacter.Equals('\t'))
                                {
                                    countColumn = countColumn + 3;
                                    readText.Read();
                                }
                                else if (char.IsWhiteSpace(currentCharacter))
                                {
                                    countColumn++;
                                    readText.Read();
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
                                CharPanicMode = currentCharacter;
                                readText.Read();
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
                                PanicError = false;
                            }
                            else if (char.IsDigit((currentCharacter)))
                            {
                                countColumn++;
                                currentState = 31;
                                completeWord.Append((int)readText.Read());

                            }
                            else if (currentCharacter == '\'')
                            {
                                countColumn++;
                                currentState = 36;
                                completeWord.Append((char)readText.Read());
                                PanicError = false;
                            }
                            else
                            {
                                completeWord.Append((char)readText.Read()); countColumn++;
                                outputSet.Add(flagError(completeWord.ToString(), countLine, countColumn));
                                return null;
                            }

                            break;

                        case 2:

                            if (char.IsLetterOrDigit(currentCharacter))
                            {
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
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;

                        case 4:         //ACHOU {         
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_OBC, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            return auxToken;


                        case 5:         //ACHOU }
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_CBC, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            return auxToken;


                        case 6:        //ACHOU UM =
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
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            return auxToken;


                        case 8:         //ACHOU =
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_ASS, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            return auxToken;


                        case 9:         //ACHOU !
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (lookahead == -1)
                            {
                                outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar um =");

                                readText.Read();
                                currentState = 1;
                            }
                            else if (AuxChar.Equals('='))
                            {
                                completeWord.Append(CharPanicMode);
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                currentState = 10;
                            }

                            else
                            {
                                if (AuxChar.Equals(' ') || AuxChar.Equals('\t'))
                                {
                                    outputSet.Add("Erro na " + currentLineAndColumn(countLine, countColumn) + " - Nao pode haver espaços entre o simbolo de diferenca. Era esperado um =");

                                    readText.Read();
                                    countColumn++;
                                    completeWord.Clear();
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    outputSet.Add("Erro na " + currentLineAndColumn(countLine, countColumn) + " - Nao pode haver quebra de linha entre o token de diferença. Era esperado um =");

                                    readText.Read();
                                    countColumn = 1;
                                    countLine++;
                                    completeWord.Clear();
                                }
                                else
                                {
                                    outputSet.Add(flagError(AuxChar.ToString(), countLine, countColumn) + "  - Era esperado um =");

                                    readText.Read();
                                    countColumn++;
                                    completeWord.Clear();
                                }

                            }

                            break;

                        case 10:        //ACHOU !=
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_NE, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            PanicError = false;

                            return auxToken;


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
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 13:        //ACHOU >
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_GT, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


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
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 16:        //ACHOU <
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_LT, completeWord.ToString(), countLine, countColumn);
                            //MessageBox.Show(auxToken.ToString() + currentLineAndColumn(countLine, countColumn));

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 17:        //ACHOU +
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_AD, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 18:        //ACHOU -
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_MIN, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 19:        //ACHOU *
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_MUL, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 20:        //ACHOU (
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_OPA, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 21:        //ACHOU )
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_CPA, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 22:        //ACHOU ,
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_COM, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 23:        //ACHOU ;
                            auxToken = ST.isLexemaOnSymbolTable(Tag.SMB_SEM, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 24:        //ACHOU / (SIMBOLO DIVISAO)
                            AuxChar = (char)readText.Peek();
                            if (AuxChar.Equals('/'))
                            {
                                currentState = 28;
                            }
                            else if (AuxChar.Equals('*'))
                            {
                                currentState = 26;
                                countColumn++;
                                completeWord.Append((char)readText.Read());     //Salvando comentário pra nada mesmo
                            }
                            else
                            {
                                currentState = 25;
                            }

                            break;

                        case 25:        //ACHOU /  (SIMBOLO DIVISÃO)
                            auxToken = ST.isLexemaOnSymbolTable(Tag.OP_DIV, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 26:        //Entrou na regra de comentário de múltiplas linhas
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (lookahead == -1)
                            {
                                outputSet.Add("Erro encontrada na " + currentLineAndColumn(countLine, countColumn) + "  - O comentário de múltipla linha não foi fechado.");

                                currentState = 1;
                            }
                            else if (AuxChar.Equals('*'))
                            {
                                currentState = 27;
                                completeWord.Append((char)readText.Read());
                                countColumn++;

                            }
                            else if (AuxChar.Equals('\n'))
                            {
                                readText.Read();
                                countLine++;
                                countColumn = 1;
                            }
                            else
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                            }

                            break;

                        case 27:
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (lookahead == -1)
                            {
                                outputSet.Add("Erro encontrada na " + currentLineAndColumn(countLine, countColumn) + "  - O comentário de múltipla linha não foi fechado.");

                                currentState = 1;
                            }
                            else if (AuxChar.Equals('/'))
                            {
                                completeWord.Append((char)readText.Read());
                                countColumn++;
                                completeWord.Clear();
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

                            do
                            {
                                lookahead = readText.Peek();

                                if (lookahead == -1)
                                {
                                    break;
                                }

                                AuxChar = (char)readText.Read();

                            } while (AuxChar != '\n');

                            countLine++;
                            countColumn = 1;
                            currentState = 1;
                            completeWord.Clear();

                            break;

                        case 29:        //ACHOU " 
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (PanicError == false)
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar uma aspas");

                                    currentState = 1;
                                }
                                else if (AuxChar.Equals('"'))
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                    StringPanicMode.Append(completeWord.ToString());
                                    currentState = 30;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    StringPanicMode.Append(completeWord.ToString());
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn) + " Era esperado uma aspas");

                                    completeWord.Clear();
                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                    PanicError = true;
                                }
                                else
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                }
                            }

                            else
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar uma aspas");

                                    currentState = 1;
                                }
                                else if (AuxChar.Equals('"'))
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                    StringPanicMode.Append(completeWord.ToString());
                                    currentState = 30;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn) + " Era esperado uma aspas");

                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                }
                                else if (AuxChar.Equals('\r') || AuxChar.Equals(' ') || AuxChar.Equals('\t'))
                                {
                                    readText.Read();
                                }
                                else
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                    //MessageBox.Show(flagError(AuxChar.ToString(), countLine, countColumn) + "Era esperado uma aspas");
                                    outputSet.Add(flagError(AuxChar.ToString(), countLine, countColumn) + "Era esperado uma aspas");
                                }
                            }

                            break;

                        case 30:        //ACHOU "" (STRING) 

                            if (completeWord.ToString().Length == 2)     //Diz o Gustavo que se criar uma string "" tem que dar erro. Mas se eu só tiver viajando, é só tirar esse if e deixar só o que tá dentro do else
                            {
                                outputSet.Add(flagError(completeWord.ToString(), countLine, countColumn));

                                completeWord.Clear();
                                currentState = 1;
                            }
                            else
                            {
                                auxToken = ST.isLexemaOnSymbolTable(Tag.LIT, StringPanicMode.ToString(), countLine, countColumn);
                                outputSet.Add(auxToken.ToString());
                                return auxToken;
                            }

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            StringPanicMode.Clear();
                            PanicError = false;

                            break;

                        case 31:       //ACHOU DIGITO
                            AuxChar = (char)readText.Peek();

                            if (char.IsDigit(AuxChar))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                            }
                            else if (AuxChar.Equals('.'))
                            {
                                countColumn++;
                                completeWord.Append((char)readText.Read());
                                currentState = 32;
                            }
                            else
                            {
                                currentState = 35;
                            }

                            break;

                        case 32:        //ACHOU .
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (PanicError == false)
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar um numero");

                                    currentState = 1;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    StringPanicMode.Append(completeWord.ToString());
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn) + " Era esperado encontrar um numero");

                                    completeWord.Clear();
                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                    PanicError = true;
                                }

                                else if (char.IsDigit(AuxChar))
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                    StringPanicMode.Append(completeWord.ToString());
                                    currentState = 33;
                                }
                                else
                                {
                                    StringPanicMode.Append(completeWord.ToString());
                                    AuxChar = (char)readText.Read();
                                    outputSet.Add(flagError(AuxChar.ToString(), countLine, countColumn));

                                    completeWord.Clear();
                                    PanicError = true;
                                }
                            }

                            else
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar um numero");

                                    currentState = 1;
                                }
                                else if (char.IsDigit(AuxChar))
                                {
                                    countColumn++;
                                    StringPanicMode.Append((char)readText.Read());
                                    currentState = 33;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn) + " Era esperado um numero");

                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                }
                                else if (AuxChar.Equals('\r') || AuxChar.Equals(' ') || AuxChar.Equals('\t'))
                                {
                                    readText.Read();
                                }
                                else
                                {
                                    countColumn++;
                                    AuxChar = ((char)readText.Read());
                                    outputSet.Add(flagError(AuxChar.ToString(), countLine, countColumn) + " Era esperado um numero");
                                }
                            }


                            break;

                        case 33:        //ACHOU FLOAT

                            AuxChar = (char)readText.Peek();

                            if (char.IsDigit(AuxChar))
                            {
                                countColumn++;
                                StringPanicMode.Append((char)readText.Read());
                            }
                            else
                            {
                                countColumn++;
                                currentState = 34;
                            }

                            break;

                        case 34:   // ACHOU UM FLOAT (estado final)    
                            auxToken = ST.isLexemaOnSymbolTable(Tag.CON_NUM, StringPanicMode.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder
                            StringPanicMode.Clear();
                            PanicError = false;

                            return auxToken;


                        case 35:         //ACHOU INT (estado final)
                            auxToken = ST.isLexemaOnSymbolTable(Tag.CON_NUM, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        case 36:  //ACHOU ASPAS SIMPLES
                            AuxChar = (char)readText.Peek();
                            lookahead = readText.Peek();

                            if (PanicError == false)
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar uma aspas. ");

                                    currentState = 1;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn));

                                    StringPanicMode.Append(completeWord.ToString());
                                    completeWord.Clear();
                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                    PanicError = true;
                                }
                                else if (AuxChar.Equals('\''))                   //PEGOU UM EMPTY CHAR
                                {
                                    completeWord.Append((char)readText.Read()); countColumn++;
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  -  Foi encontrado um Char vazio. ");

                                    completeWord.Clear();
                                    currentState = 1;
                                }
                                else
                                {
                                    completeWord.Append((char)(readText.Read()));       //Simplesmente pega o proximo caracter
                                    countColumn++;

                                    currentState = 37;
                                }

                            }
                            else
                            {
                                if (lookahead == -1)
                                {
                                    outputSet.Add("Erro na linha " + currentLineAndColumn(countLine, countColumn) + "  - Fim do arquivo antes de encontar uma aspas");

                                    currentState = 1;
                                }
                                else if (AuxChar.Equals('\n'))
                                {
                                    outputSet.Add("Quebra de linha inesperada na " + currentLineAndColumn(countLine, countColumn));

                                    completeWord.Clear();
                                    readText.Read();
                                    countLine++;
                                    countColumn = 1;
                                }
                                else if (AuxChar.Equals('\''))                   //PEGOU UM EMPTY CHAR
                                {
                                    completeWord.Append((char)readText.Read()); countColumn++;
                                    outputSet.Add("Erro na " + currentLineAndColumn(countLine, countColumn) + " - Um Char deve ser construído todo na mesma linha. ");

                                    completeWord.Clear();
                                    currentState = 1;
                                }
                                else
                                {
                                    countColumn++;
                                    completeWord.Append((char)readText.Read());
                                    outputSet.Add(flagError(completeWord.ToString(), countLine, countColumn) + " -  Era esperado uma aspas");
                                }
                            }

                            break;

                        case 37:
                            AuxChar = (char)readText.Peek();

                            if (AuxChar.Equals('\''))
                            {
                                completeWord.Append((char)readText.Read()); countColumn++;
                                currentState = 38;
                            }
                            else
                            {
                                completeWord.Append((char)readText.Read()); countColumn++;
                                outputSet.Add(flagError(completeWord.ToString(), countLine, countColumn));

                                completeWord.Clear();
                                currentState = 1;
                            }

                            break;

                        case 38:

                            auxToken = ST.isLexemaOnSymbolTable(Tag.CON_CHAR, completeWord.ToString(), countLine, countColumn);
                            outputSet.Add(auxToken.ToString());

                            currentState = 1;       //Reseta a execução do automato
                            completeWord.Clear();   //Reseta a StringBiulder

                            return auxToken;


                        default:
                            outputSet.Add(flagError(completeWord.ToString(), countLine, countColumn));

                            return null;

                    }

                } while (true);

            }

            catch (ArgumentException)
            {
                //MessageBox.Show("Caminho informado inválido");
                outputSet.Add("Caminho informado inválido!");
                return null;
            }
            catch (NotSupportedException)
            {
                //MessageBox.Show("Caminho informado inválido");
                outputSet.Add("Caminho informado inválido!");
                return null;
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
            return " Linha: " + line + ", Coluna: " + column;
        }


        /// <summary>
        /// Método para sinalizar um erro e o lugar onde ele foi encontrado
        /// </summary>
        public static string flagError(string lexema, int line, int column)
        {
            return "[ERRO LÉXICO]: Caracter '" + lexema + "' inesperado na " + currentLineAndColumn(line, column) + " ";
        }


        /// <summary>
        /// Método usado para fechar o arquivo após a leitura
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="readText"></param>
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