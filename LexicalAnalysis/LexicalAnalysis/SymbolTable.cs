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
        Dictionary<Tag, string> ST;


        public SymbolTable()
        {
            #region [INSERE KW NA TABELA DE SIMBOLOS]
            this.ST = new Dictionary<Tag, string>();
            ST.Add(Tag.KW, "program");
            ST.Add(Tag.KW, "if");
            ST.Add(Tag.KW, "else");
            ST.Add(Tag.KW, "while");
            ST.Add(Tag.KW, "write");
            ST.Add(Tag.KW, "read");
            ST.Add(Tag.KW, "num");
            ST.Add(Tag.KW, "char");
            ST.Add(Tag.KW, "not");
            ST.Add(Tag.KW, "or");
            ST.Add(Tag.KW, "and");
            #endregion

            #region [INSERE OPERADORES NA TABELA DE SIMBOLOS]
            ST.Add(Tag.OP_EQ, "==");
            ST.Add(Tag.OP_NE, "!=");
            ST.Add(Tag.OP_GT, ">");
            ST.Add(Tag.OP_LT, "<");
            ST.Add(Tag.OP_GE, ">=");
            ST.Add(Tag.OP_LE, "<=");
            ST.Add(Tag.OP_AD, "+");
            ST.Add(Tag.OP_MIN, "-");
            ST.Add(Tag.OP_MUL, "*");
            ST.Add(Tag.OP_DIV, "/");
            ST.Add(Tag.OP_ASS, "=");
            #endregion

            #region [INSERE SIMBOLOS NA TABELA DE SIMBOLOS]
            ST.Add(Tag.SMB_OBC, "{");
            ST.Add(Tag.SMB_CBC, "}");
            ST.Add(Tag.SMB_OPA, "(");
            ST.Add(Tag.SMB_CPA, ")");
            ST.Add(Tag.SMB_COM, ",");
            ST.Add(Tag.SMB_SEM, ";");
            #endregion

            ST.Add(Tag.ID, "");

            ST.Add(Tag.LIT, "");

            ST.Add(Tag.CON_CHAR, "");
            ST.Add(Tag.CON_NUM, "");
        }

        /// <summary>
        /// Método para inserir um novo token na tabela de símbolos. 
        /// Será verificado se o simbolo já está contido na tabela de simbolos
        /// </summary>
        /// <param name="token"></param>
        public void insertOnSymbolTable(Token token)
        {
            ST.Add(token.tag, token.getLexema);
        }

        //public void isOnTableSymbol(Token token)
        //{
        //    foreach (var token in ST)
        //    {

        //    }
        //}

    }
}
