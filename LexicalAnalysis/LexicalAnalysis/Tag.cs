using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    public enum Tag
    {
        //Fim do arquivo
        EOF,

        #region [OPERADORES]
        OP_EQ,
        OP_GE,
        OP_MUL,
        OP_NE,
        OP_LE,
        OP_DIV,
        OP_GT,
        OP_AD,
        OP_ASS,
        OP_LT,
        OP_MIN,
        #endregion

        #region [SIMBOLOS]
        SMB_OBC,
        SMB_COM,
        SMB_CBC,
        SMB_SEM,
        SMB_OPA,
        SMB_CPA,
        #endregion

        // palavra reservada
        KW, 

        //Identificador
        ID, 

        //Literal
        LIT,

        #region [CONSTANTES]
        CON_NUM,
        CON_CHAR,
        #endregion
    }
}
