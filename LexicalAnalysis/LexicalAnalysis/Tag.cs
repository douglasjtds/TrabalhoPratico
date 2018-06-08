using System;

namespace LexicalAnalysis
{
    public enum Tag
    {
        /// <summary>
        /// Fim do arquivo
        /// </summary>
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

        #region [PALAVRAS CHAVES]
        KW_PROGRAM,
        KW_IF,
        KW_ELSE,
        KW_WHILE,
        KW_WRITE,
        KW_READ,
        KW_NUM,
        KW_CHAR,
        KW_NOT,
        KW_OR,
        KW_AND,
        #endregion

        /// <summary>
        /// Identificador
        /// </summary>
        ID,

        /// <summary>
        /// Literal
        /// </summary>
        LIT,

        #region [CONSTANTES]
        CON_NUM,
        CON_CHAR,
        #endregion
    }
}
