using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using LexicalAnalysis;
using System.Collections.Generic;
using myExtension;

namespace SyntaxAnalysis
{
    /// <summary>
    /// Classe que contém todos os métodos utilizados durante a execução do analisador sintático
    /// </summary>
    public class Parser
    {

        Lexer lexer;
        Token token;
        Stream entrada;
        StreamReader readText;
        List<String> outputSet;
        SymbolTable ST;
        int countLine;
        int countColumn;

        /// <summary>
        /// Método construtor do Parser 
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="readText"></param>
        /// <param name="outputSet"></param>
        /// <param name="ST"></param>
        public Parser(Stream entrada, StreamReader readText, List<String> outputSet, SymbolTable ST)
        {
            this.ST = ST;
            this.entrada = entrada;
            this.readText = readText;
            this.outputSet = outputSet;
            countLine = 1;
            countColumn = 1;
            token = Lexer.performsAutomaton(entrada, readText, outputSet, ST, countLine, countColumn);
        }

        /// <summary>
        /// Método que retorna a mensagem dos erros sintáticos encontrados
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string flagSyntaxError(string message)
        {
            outputSet.Add("[ERRO SINTÁTICO]: Na linha: " + token.Linha + ", coluna: " + token.Coluna + ". " + message);
            return "[ERRO SINTÁTICO]: Na linha: " + token.Linha + ", coluna: " + token.Coluna + ". " + message;
        }


        /// <summary>
        /// Método que avança a entrada do caracter
        /// </summary>
        public void advance()
        {

            do
            {
                token = Lexer.performsAutomaton(entrada, readText, outputSet, ST);
            }
            while (token == null);

        }

        /// <summary>
        /// Método que consome o caracter
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool eat(Tag tag)
        {
            if (token.Classe.Equals(tag))
            {
                advance();
                return true;
            }

            return false;
        }

        public void CloseFiles()
        {
            Lexer.CloseFile(entrada, readText);
        }

        /// <summary>
        /// Método para o não terminal "prog"
        /// </summary>
        public void prog()
        {
            if (eat(Tag.KW_PROGRAM))
            {
                if (!eat(Tag.ID))
                {
                    flagSyntaxError("Era esperado um ID , mas foi encontrado: " + token.Lexema);
                }

                body();

                if (!eat(Tag.EOF))
                {
                    flagSyntaxError("Era esperado o fim de arquivo, mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \"program\", mas foi encontrado: " + token.Lexema); //"Era esperado: \" O QUE ERA ESPERADO \", mas foi encontrado: " + token.Lexema
            }
        }

        /// <summary>
        /// Método para o não terminal "body"
        /// </summary>
        public void body()
        {

            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl_list();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "decl-list"
        /// </summary>
        public void decl_list()
        {
            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl();

                if (!eat(Tag.SMB_SEM))
                {
                    flagSyntaxError("Era esperado: \" ; \", mas foi encontrado: " + token.Lexema);
                }

                decl_list();


            }
            else if (token.Classe.Equals(Tag.SMB_OBC)) return;
            else
            {
                flagSyntaxError("Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "decl"
        /// </summary>
        public void decl()
        {
            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                type();

                id_list1();
            }
            else
            {
                flagSyntaxError("Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "type"
        /// </summary>
        public void type()
        {
            if (!eat(Tag.KW_NUM) && !eat(Tag.KW_CHAR))
            {
                flagSyntaxError("Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "id-list"
        /// </summary>
        public void id_list1()
        {
            if (eat(Tag.ID))
                id_list2();
            else
            {
                flagSyntaxError("Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal " id-list' "
        /// </summary>
        public void id_list2()
        {
            if (eat(Tag.SMB_COM))
                id_list1();

            else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("Era esperado: \" , ou um ;  \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "stmt-list"
        /// </summary>
        public void stmt_list()
        {

            if (token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.KW_IF) ||
                token.Classe.Equals(Tag.KW_WHILE) ||
                token.Classe.Equals(Tag.KW_READ) ||
                token.Classe.Equals(Tag.KW_WRITE))
            {

                stmt();

                if (!eat(Tag.SMB_SEM))
                {
                    flagSyntaxError("Era esperado: \" ; \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

            }
            else if (token.Classe.Equals(Tag.SMB_CBC)) return;
            else
            {
                flagSyntaxError("Era esperado: \"if, ID, while, read, while ou }\", mas foi encontrado: " + token.Lexema);
            }

        }

        /// <summary>
        /// Método para o não terminal "stmt"
        /// </summary>
        public void stmt()
        {
            if (token.Classe.Equals(Tag.ID))
                assign_stmt();
            else if (token.Classe.Equals(Tag.KW_IF))
                If_stmt1();
            else if (token.Classe.Equals(Tag.KW_WHILE))
                While_stmt();
            else if (token.Classe.Equals(Tag.KW_READ))
                read_stmt();
            else if (token.Classe.Equals(Tag.KW_WRITE))
                write_stmt();
        }

        /// <summary>
        /// Método para o não terminal "assign-stmt"
        /// </summary>
        public void assign_stmt()
        {
            if (eat(Tag.ID))
            {
                if (!eat(Tag.OP_ASS))
                {
                    flagSyntaxError("Era esperado: \" = \", mas foi encontrado: " + token.Lexema);
                }

                simple_expr1();
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "if-stmt"
        /// </summary>
        public void If_stmt1()
        {
            if (eat(Tag.KW_IF))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("Era esperado: \" ( \", mas foi encontrado: " + token.Lexema);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
                }

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }

                If_stmt2();
            }
            else
            {
                flagSyntaxError("Era esperado: \" if \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal " if-stmt' "
        /// </summary>
        public void If_stmt2()
        {
            if (eat(Tag.KW_ELSE))
            {
                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }

            }
            else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("Era esperado: \" else \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "condition"
        /// </summary>
        public void condition()
        {
            if (token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.KW_NOT) ||
                token.Classe.Equals(Tag.SMB_OPA))

                expression1();
            else
            {
                flagSyntaxError("Era esperado: \" ID, const_char, const_num, not ou ( \", mas foi encontrado: " + token.Lexema);
            }

        }

        /// <summary>
        /// Método para o não terminal "while-stmt"
        /// </summary>
        public void While_stmt()
        {

            if (token.Classe.Equals(Tag.KW_WHILE))
            {
                stmt_prefix();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \" while \", mas foi encontrado: " + token.Lexema);
            }

        }

        /// <summary>
        /// Método para o não terminal "stmt-prefix"
        /// </summary>
        public void stmt_prefix()
        {
            if (eat(Tag.KW_WHILE))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("Era esperado: \" ( \", mas foi encontrado: " + token.Lexema);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \" while \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "read-stmt"
        /// </summary>
        public void read_stmt()
        {

            if (eat(Tag.KW_READ))
            {
                if (!eat(Tag.ID))
                {
                    flagSyntaxError("Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \" read \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "write-stmt"
        /// </summary>
        public void write_stmt()
        {
            if (eat(Tag.KW_WRITE))
            {
                writable();
            }
            else
            {
                flagSyntaxError("Era esperado: \" write \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "writable"
        /// </summary>
        public void writable()
        {
            if (!eat(Tag.LIT))
            {
                if (token.Classe.Equals(Tag.ID) ||
                    token.Classe.Equals(Tag.CON_NUM) ||
                    token.Classe.Equals(Tag.CON_CHAR) ||
                    token.Classe.Equals(Tag.KW_NOT) ||
                    token.Classe.Equals(Tag.SMB_OPA))

                    simple_expr1();
                else
                {
                    flagSyntaxError("Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
                }
            }
        }

        /// <summary>
        /// Método para o não terminal "expression"
        /// </summary>
        public void expression1()
        {
            if (token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.KW_NOT) ||
                token.Classe.Equals(Tag.SMB_OPA))
            {
                simple_expr1();
                expression2();
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal " expression' "
        /// </summary>
        public void expression2()
        {
            if (token.Classe.Equals(Tag.OP_EQ) ||
                token.Classe.Equals(Tag.OP_GT) ||
                token.Classe.Equals(Tag.OP_GE) ||
                token.Classe.Equals(Tag.OP_LT) ||
                token.Classe.Equals(Tag.OP_LE) ||
                token.Classe.Equals(Tag.OP_NE))
            {
                relop();
                simple_expr1();
            }

            else if (token.Classe.Equals(Tag.SMB_CPA)) return;
            else
            {
                flagSyntaxError("Era esperado: \" ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "simple-expr"
        /// </summary>
        public void simple_expr1()
        {
            if (token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.KW_NOT) ||
                token.Classe.Equals(Tag.SMB_OPA))
            {
                term1();
                simple_expr2();
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal " simple-expr' "
        /// </summary>
        public void simple_expr2()
        {
            if (token.Classe.Equals(Tag.OP_AD) ||
                token.Classe.Equals(Tag.OP_MIN) ||
                token.Classe.Equals(Tag.KW_OR))
            {
                addop();

                term1();

                simple_expr2();
            }

            else if (token.Classe.Equals(Tag.SMB_SEM) ||      //“;”, “==”, “>”, “>=”, “<”, “<=”, “!=”, “)”
                    token.Classe.Equals(Tag.OP_EQ) ||
                    token.Classe.Equals(Tag.OP_GT) ||
                    token.Classe.Equals(Tag.OP_GE) ||
                    token.Classe.Equals(Tag.OP_LT) ||
                    token.Classe.Equals(Tag.OP_LE) ||
                    token.Classe.Equals(Tag.SMB_CPA) ||
                    token.Classe.Equals(Tag.OP_NE)) return;
            else
            {
                flagSyntaxError("Era esperado: \" +, -, or, ;, ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "term"
        /// </summary>
        public void term1()
        {
            if (token.Classe.Equals(Tag.ID) ||       //First FactorA “id”, “num_const”, “char_const”, “(“, “not”
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.SMB_OPA) ||
                token.Classe.Equals(Tag.KW_NOT) ||
                token.Classe.Equals(Tag.CON_CHAR))
            {
                factor_a();
                term2();
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal " term' "
        /// </summary>
        public void term2()
        {
            if (token.Classe.Equals(Tag.OP_MUL) ||
                token.Classe.Equals(Tag.OP_DIV) ||
                token.Classe.Equals(Tag.KW_AND))
            {
                mulop();

                factor_a();

                term2();
            }

            else if (token.Classe.Equals(Tag.OP_AD) ||
                    token.Classe.Equals(Tag.OP_MIN) ||
                    token.Classe.Equals(Tag.KW_OR) ||
                    token.Classe.Equals(Tag.SMB_SEM) ||
                    token.Classe.Equals(Tag.SMB_CPA) ||
                    token.Classe.Equals(Tag.OP_EQ) ||
                    token.Classe.Equals(Tag.OP_GT) ||
                    token.Classe.Equals(Tag.OP_GE) ||
                    token.Classe.Equals(Tag.OP_LT) ||
                    token.Classe.Equals(Tag.OP_LE) ||
                    token.Classe.Equals(Tag.OP_NE)) return; //“+”, “-”, “or”, “;”, “==”, “>”, “>=”, “<”, “<=”, “!=”, “)”
            else
            {
                flagSyntaxError("Era esperado: \" *, /, and, +, -, or, ;, ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }


        }

        /// <summary>
        /// Método para o não terminal "factor-a"
        /// </summary>
        public void factor_a()
        {
            if (token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.SMB_OPA))

                factor();

            else if (eat(Tag.KW_NOT))
            {
                factor();
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID, (, const_num ou const_char \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "factor"
        /// </summary>
        public void factor()
        {

            if (eat(Tag.ID))
                return;

            else if (token.Classe.Equals(Tag.CON_NUM) || token.Classe.Equals(Tag.CON_CHAR))
                constant();

            else if (eat(Tag.SMB_OPA))
            {
                expression1();
                if (!eat(Tag.SMB_CPA))
                    flagSyntaxError("Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
            }
            else
            {
                flagSyntaxError("Era esperado: \" ID, const_char ou const_num \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "relop"
        /// </summary>
        public void relop()  //“==”43 | “>” 44 | “>=” 45 | “<” 46 | “<=” 47 | “!=” 48
        {

            if (!eat(Tag.OP_EQ) &&
                !eat(Tag.OP_GT) &&
                !eat(Tag.OP_GE) &&
                !eat(Tag.OP_LT) &&
                !eat(Tag.OP_LE) &&
                !eat(Tag.OP_NE))
            {
                flagSyntaxError("Era esperado: \" ==, >, >=, <, <= ou != \", mas foi encontrado: " + token.Lexema);
            }

        }

        /// <summary>
        /// Método para o não terminal "addop"
        /// </summary>
        public void addop()
        {
            if (!eat(Tag.OP_AD) && !eat(Tag.OP_MIN) && !eat(Tag.KW_OR))
            {
                flagSyntaxError("Era esperado: \" or, + ou - \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "mulop"
        /// </summary>
        public void mulop()
        {
            if (!eat(Tag.OP_MUL) && !eat(Tag.OP_DIV) && !eat(Tag.KW_AND))
            {
                flagSyntaxError("Era esperado: \" and, * ou / \", mas foi encontrado: " + token.Lexema);
            }
        }

        /// <summary>
        /// Método para o não terminal "constant"
        /// </summary>
        public void constant()
        {
            if (!eat(Tag.CON_NUM) && !eat(Tag.CON_CHAR))
            {
                flagSyntaxError("Era esperado: \" const_num ou const_char \", mas foi encontrado: " + token.Lexema);
            }
        }

    }
}
