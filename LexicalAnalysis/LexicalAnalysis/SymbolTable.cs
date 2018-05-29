using LexicalAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    /// <summary>
    /// Classe que contem as funcionalidades para a Tabela de Símbolos
    /// </summary>
    public class SymbolTable
    {
        public static Dictionary<Token, string> ST;


        public SymbolTable()
        {
            #region [INSERE KW NA TABELA DE SIMBOLOS]
            ST = new Dictionary<Token, string>();

            Token word;
            word = new Token(Tag.KW_PROGRAM, "program", 0, 0);
            ST.Add(word, "program");

            word = new Token(Tag.KW_IF, "if", 0, 0);
            ST.Add(word, "if");

            word = new Token(Tag.KW_ELSE, "else", 0, 0);
            ST.Add(word, "else");

            word = new Token(Tag.KW_WHILE, "while", 0, 0);
            ST.Add(word, "while");

            word = new Token(Tag.KW_WRITE, "write", 0, 0);
            ST.Add(word, "write");

            word = new Token(Tag.KW_READ, "read", 0, 0);
            ST.Add(word, "read");

            word = new Token(Tag.KW_NUM, "num", 0, 0);
            ST.Add(word, "num");

            word = new Token(Tag.KW_CHAR, "char", 0, 0);
            ST.Add(word, "char");

            word = new Token(Tag.KW_NOT, "not", 0, 0);
            ST.Add(word, "not");

            word = new Token(Tag.KW_OR, "or", 0, 0);
            ST.Add(word, "or");

            word = new Token(Tag.KW_AND, "and", 0, 0);
            ST.Add(word, "and");

            #endregion
        }


        /// <summary>
        /// Verifica se já tem esse lexema na Tabela de símbolos. Se tiver, retorna ele. Se não tiver, adiciona e retorna ele.
        /// </summary>
        /// <param name="tag">O tipo do lexema encontrado</param>
        /// <param name="completeWord">A StringBiulder construida </param>
        /// <param name="countColumn">Posição atual da coluna</param>
        /// <param name="countLine">Posição atual da linha</param>
        /// <returns></returns>
        public Token isLexemaOnSymbolTable(Tag tag, string completeWord, int countLine, int countColumn)
        {
            foreach (Token token in ST.Keys)
            {
                if (token.Lexema.ToUpper().Equals(completeWord.ToUpper()))
                {
                    return token;
                }
            }

            Token NovoToken = new Token(tag, completeWord, countLine, countColumn);
            ST.Add(NovoToken, completeWord);                                            //Se sair do foreach e não achar nenhum outro ID na TS, insere na tabela um novo símbolo
            return NovoToken;    
        }

        public String showSymbolTable()
        {
            foreach (Token token in ST.Keys)
            {
                //TO-DO: terminar implementacao utilizar o metodo pra printar no componente da interface 
            }
            return "aff Zzzz";
        }

    }
}
