using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    public enum Tags
    {
        EOF,

        #region (OPERADORES)
        RELOP_LT,
        RELOP_LE,
        RELOP_GT,
        RELOP_GE,
        RELOP_EQ,
        RELOP_NE,
        RELOP_ASSIGN,
        RELOP_PLUS,
        RELOP_MINUS,
        RELOP_MULT,
        RELOP_DIV,
        #endregion

        #region (SIMBOLOS)
        SMB_OP, 
        SMB_CP,
        SMB_SEMICOLON,
        #endregion

        ID,

        KW,

    }
}
