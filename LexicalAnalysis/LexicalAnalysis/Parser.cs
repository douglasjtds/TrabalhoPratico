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
            token = Lexer.performsAutomaton(entrada, readText, outputSet, ST);
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


        public void prog()
        {
            if (eat(Tag.KW_PROGRAM))
            {
                if (!eat(Tag.ID))
                {
                    flagSyntaxError("(prog) Era esperado um ID , mas foi encontrado: " + token.Lexema);
                }

                body();

                if (!eat(Tag.EOF))
                {
                    flagSyntaxError("(prog) Era esperado o fim de arquivo, mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("(prog) Era esperado: \"program\", mas foi encontrado: " + token.Lexema); //"Era esperado: \" O QUE ERA ESPERADO \", mas foi encontrado: " + token.Lexema
            }
        }

        public void body()
        {

            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl_list();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("(body) Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("(body) Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("(body) Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void decl_list()
        {
            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl();

                if (!eat(Tag.SMB_SEM))
                {
                    flagSyntaxError("(decl list) Era esperado: \" ; \", mas foi encontrado: " + token.Lexema);
                }

                decl_list();


            }
            else if (token.Classe.Equals(Tag.SMB_OBC)) return;
            else
            {
                flagSyntaxError("(decl list) Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void decl()
        {
            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                type();

                id_list1();
            }
            else
            {
                flagSyntaxError("(decl) Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void type()
        {
            if (!eat(Tag.KW_NUM) && !eat(Tag.KW_CHAR))
            {
                flagSyntaxError("(type) Era esperado: \" num ou char \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void id_list1()
        {
            if (eat(Tag.ID))
                id_list2();
            else
            {
                flagSyntaxError("(id_list1) Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void id_list2()
        {
            if (eat(Tag.SMB_COM))
                id_list1();

            else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("(id_list2) Era esperado: \" , ou um ;  \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                    flagSyntaxError("(stmt list) Era esperado: \" ; \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

            }
            else if (token.Classe.Equals(Tag.SMB_CBC)) return;
            else
            {
                flagSyntaxError("(stmt list) Era esperado: \"if, ID, while, read, while ou }\", mas foi encontrado: " + token.Lexema);
            }

        }

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

        public void assign_stmt()
        {
            if (eat(Tag.ID))
            {
                if (!eat(Tag.OP_ASS))
                {
                    flagSyntaxError("(assing stmt) Era esperado: \" = \", mas foi encontrado: " + token.Lexema);
                }

                simple_expr1();
            }
            else
            {
                flagSyntaxError("(assign stmt) Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void If_stmt1()
        {
            if (eat(Tag.KW_IF))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("(If_stmt1) Era esperado: \" ( \", mas foi encontrado: " + token.Lexema);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("(If_stmt1) Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
                }

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("(If_stmt1) Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("(If_stmt1) Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }

                If_stmt2();
            }
            else
            {
                flagSyntaxError("(If_stmt1) Era esperado: \" if \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void If_stmt2()
        {
            if (eat(Tag.KW_ELSE))
            {
                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("(If_stmt2) Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("(If_stmt2) Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }

            }
            else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("(If_stmt2) Era esperado: \" else \", mas foi encontrado: " + token.Lexema);
            }
        }


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
                flagSyntaxError("(condition) Era esperado: \" ID, const_char, const_num, not ou ( \", mas foi encontrado: " + token.Lexema);
            }

        }

        public void While_stmt()
        {

            if (token.Classe.Equals(Tag.KW_WHILE))
            {
                stmt_prefix();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("(while stmt) Era esperado: \" { \", mas foi encontrado: " + token.Lexema);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("(while stmt) Era esperado: \" } \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("(while stmt) Era esperado: \" while \", mas foi encontrado: " + token.Lexema);
            }

        }

        public void stmt_prefix()
        {
            if (eat(Tag.KW_WHILE))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("(stmt prefix) Era esperado: \" ( \", mas foi encontrado: " + token.Lexema);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("(stmt prefix) Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("(stmt prefix) Era esperado: \" while \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void read_stmt()
        {

            if (eat(Tag.KW_READ))
            {
                if (!eat(Tag.ID))
                {
                    flagSyntaxError("(read stmt) Era esperado: \" ID \", mas foi encontrado: " + token.Lexema);
                }
            }
            else
            {
                flagSyntaxError("(read stmt) Era esperado: \" read \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void write_stmt()
        {
            if (eat(Tag.KW_WRITE))
            {
                writable();
            }
            else
            {
                flagSyntaxError("(write stmt) Era esperado: \" write \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                    flagSyntaxError("(writable) Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
                }
            }
        }

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
                flagSyntaxError("(expression1) Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                flagSyntaxError("(expression 2) Era esperado: \" ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                flagSyntaxError("(simple expr 1) Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                flagSyntaxError("(simple expr 2) Era esperado: \" +, -, or, ;, ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                flagSyntaxError("(term1) Era esperado: \" ID, const_num, const_char, not ou ( \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                flagSyntaxError("(term2) Era esperado: \" *, /, and, +, -, or, ;, ==, >, >=, <, <=, != ou ) \", mas foi encontrado: " + token.Lexema);
            }


        }

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
                flagSyntaxError("(factor a) Era esperado: \" ID, (, const_num ou const_char \", mas foi encontrado: " + token.Lexema);
            }
        }

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
                    flagSyntaxError("(factor) Era esperado: \" ) \", mas foi encontrado: " + token.Lexema);
            }
            else
            {
                flagSyntaxError("(factor) Era esperado: \" ID, const_char ou const_num \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void relop()  //“==”43 | “>” 44 | “>=” 45 | “<” 46 | “<=” 47 | “!=” 48
        {

            if (!eat(Tag.OP_EQ) &&
                !eat(Tag.OP_GT) &&
                !eat(Tag.OP_GE) &&
                !eat(Tag.OP_LT) &&
                !eat(Tag.OP_LE) &&
                !eat(Tag.OP_NE))
            {
                flagSyntaxError("(relop) Era esperado: \" ==, >, >=, <, <= ou != \", mas foi encontrado: " + token.Lexema);
            }

        }

        public void addop()
        {
            if (!eat(Tag.OP_AD) && !eat(Tag.OP_MIN) && !eat(Tag.KW_OR))
            {
                flagSyntaxError("(addop) Era esperado: \" or, + ou - \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void mulop()
        {
            if (!eat(Tag.OP_MUL) && !eat(Tag.OP_DIV) && !eat(Tag.KW_AND))
            {
                flagSyntaxError("(mulop) Era esperado: \" and, * ou / \", mas foi encontrado: " + token.Lexema);
            }
        }

        public void constant()
        {
            if (!eat(Tag.CON_NUM) && !eat(Tag.CON_CHAR))
            {
                flagSyntaxError("(constant) Era esperado: \" const_num ou const_char \", mas foi encontrado: " + token.Lexema);
            }
        }

    }
}
