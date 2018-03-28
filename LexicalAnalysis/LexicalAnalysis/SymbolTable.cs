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

            //ST.Add(Tag.KW, "program");
            //ST.Add(Tag.KW, "if");
            //ST.Add(Tag.KW, "else");
            //ST.Add(Tag.KW, "while");
            //ST.Add(Tag.KW, "write");
            //ST.Add(Tag.KW, "read");
            //ST.Add(Tag.KW, "num");
            //ST.Add(Tag.KW, "char");
            //ST.Add(Tag.KW, "not");
            //ST.Add(Tag.KW, "or");
            //ST.Add(Tag.KW, "and");
            #endregion

            #region [INSERE OPERADORES NA TABELA DE SIMBOLOS]
            //ST.Add(Tag.OP_EQ, "==");
            //ST.Add(Tag.OP_NE, "!=");
            //ST.Add(Tag.OP_GT, ">");
            //ST.Add(Tag.OP_LT, "<");
            //ST.Add(Tag.OP_GE, ">=");
            //ST.Add(Tag.OP_LE, "<=");
            //ST.Add(Tag.OP_AD, "+");
            //ST.Add(Tag.OP_MIN, "-");
            //ST.Add(Tag.OP_MUL, "*");
            //ST.Add(Tag.OP_DIV, "/");
            //ST.Add(Tag.OP_ASS, "=");
            #endregion

            #region [INSERE SIMBOLOS NA TABELA DE SIMBOLOS]
            //ST.Add(Tag.SMB_OBC, "{");
            //ST.Add(Tag.SMB_CBC, "}");
            //ST.Add(Tag.SMB_OPA, "(");
            //ST.Add(Tag.SMB_CPA, ")");
            //ST.Add(Tag.SMB_COM, ",");
            //ST.Add(Tag.SMB_SEM, ";");
            #endregion

            //ST.Add(Tag.ID, "");

            //ST.Add(Tag.LIT, "");

            //ST.Add(Tag.CON_CHAR, "");
            //ST.Add(Tag.CON_NUM, "");
        }


        /// <summary>
        /// Retorna um identificador de um determinado token
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        

        /// <summary>
        /// Verifica se possui aquele lexema na tabela de símbolo. Utilizado para símbolo e operador, não para ID
        /// </summary>
        /// <param name="token">A StringBiulder construída</param>
        /// <returns></returns>
        public Token isLexemaOnTableSymbol(string completeWord)
        {
            foreach (Token token in ST.Keys)
            {
                if (token.Lexema.ToUpper().Equals(completeWord.ToUpper()))
                {
                    return token;
                }
            }
            //return "aaaaaaaaaaaaaaaaaaaaaaa";
            return null;
        }


        /// <summary>
        /// Método para inserir um novo token na tabela de símbolos. 
        /// Será verificado se o simbolo já está contido na tabela de simbolos
        /// </summary>
        /// <param name="token"></param>
        //public void insertOnSymbolTable(Token token)
        //{
        //    foreach (string lexema in ST.Values)
        //    {
        //        if (token.Lexema.ToUpper().Equals(lexema.ToUpper()))         //Se o lexema (em maiusculo) encontrado for igual a qualquer item da tabela símbolo, não insere
        //        {
        //            return;
        //        }
        //        ST.Add(token.Tag, token.Lexema);
        //    }

        //}
    }
}
