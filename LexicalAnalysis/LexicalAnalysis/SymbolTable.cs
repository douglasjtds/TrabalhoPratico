using LexicalAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    public class SymbolTable
    {
        public static Dictionary<Token, string> ST;


        public SymbolTable()
        {
            #region [INSERE KW NA TABELA DE SIMBOLOS]
            ST = new Dictionary<Token, string>();

            Token word;
            word = new Token(Tag.KW, "program", 0, 0);
            ST.Add(word, "program");

            word = new Token(Tag.KW, "if", 0, 0);
            ST.Add(word, "if");

            word = new Token(Tag.KW, "else", 0, 0);
            ST.Add(word, "else");

            word = new Token(Tag.KW, "while", 0, 0);
            ST.Add(word, "while");

            word = new Token(Tag.KW, "write", 0, 0);
            ST.Add(word, "write");

            word = new Token(Tag.KW, "read", 0, 0);
            ST.Add(word, "read");

            word = new Token(Tag.KW, "num", 0, 0);
            ST.Add(word, "num");

            word = new Token(Tag.KW, "char", 0, 0);
            ST.Add(word, "char");

            word = new Token(Tag.KW, "not", 0, 0);
            ST.Add(word, "not");

            word = new Token(Tag.KW, "or", 0, 0);
            ST.Add(word, "or");

            word = new Token(Tag.KW, "and", 0, 0);
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

    }
}
