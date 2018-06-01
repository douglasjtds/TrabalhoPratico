using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using LexicalAnalysis;
using System.Collections.Generic;
using myExtension;

namespace SyntaxAnalysis
{
    public class Parser
    {

        Lexer lexer;
        Token token;
        Stream entrada;
        StreamReader readText; 
        List<String> outputSet; 
        SymbolTable ST; 

        public Parser(Stream entrada, StreamReader readText, List<String> outputSet, SymbolTable ST, Lexer lexer)
        {
            this.lexer = lexer;
            this.ST = ST;
            this.entrada = entrada;
            this.readText = readText;
            this.outputSet = outputSet;
            token = Lexer.performsAutomaton(entrada, readText, outputSet, ST);
        }

        public string flagSyntaxError(string message)
        {
            return "[ERRO SINTÁTICO]: Na linha: " + token.Linha + ", coluna: " + token.Coluna + ". " + message;
        }

        public void advance()
        {
            token = Lexer.performsAutomaton(entrada, readText, outputSet, ST);
            outputSet.Add(token.ToString());
        }

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
                    flagSyntaxError("Era esperado um ID , mas o encontrado foi: " + token.Lexema);
                    Environment.Exit(666);
                }

                body();

                if (!eat(Tag.EOF))
                {
                    flagSyntaxError("Era esperado o fim de arquivo, mas o encontrado foi: " + token.Lexema);
                    Environment.Exit(666);
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \"program\", mas o encontrado foi: " + token.Lexema);
            }
        }

        public void body()
        {

            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl_list();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void decl_list()
        {
            if (token.Classe.Equals(Tag.KW_NUM) || token.Classe.Equals(Tag.KW_CHAR))
            {
                decl();

                if (!eat(Tag.SMB_SEM))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                decl_list();
                

            } else if (token.Classe.Equals(Tag.SMB_OBC)) return;
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
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
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void type()
        {
            if(!eat(Tag.KW_NUM) || !eat(Tag.KW_CHAR))
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void id_list1()
        {
            if (eat(Tag.ID))
                id_list2();
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void id_list2()
        {
            if (eat(Tag.SMB_COM))
                id_list1();

            else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
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
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                stmt_list();

            } else if (token.Classe.Equals(Tag.SMB_CBC)) return;
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
                
        }

        public void stmt()
        {
            if(token.Classe.Equals(Tag.ID))
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
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                simple_expr1();
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void If_stmt1()
        {
            if (eat(Tag.KW_IF))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                If_stmt2();
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void If_stmt2()
        {
            if (eat(Tag.KW_ELSE))
            {
                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

            } else if (token.Classe.Equals(Tag.SMB_SEM)) return;
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }


        public void condition()
        {
            if(token.Classe.Equals(Tag.ID) ||
                token.Classe.Equals(Tag.CON_NUM) ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.KW_NOT) ||
                token.Classe.Equals(Tag.SMB_OPA))

                    expression1();
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }

        }

        public void While_stmt()
        {

            if (token.Classe.Equals(Tag.KW_WHILE))
            {
                stmt_prefix();

                if (!eat(Tag.SMB_OBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                stmt_list();

                if (!eat(Tag.SMB_CBC))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }

        }

        public void stmt_prefix()
        {
            if (eat(Tag.KW_WHILE))
            {
                if (!eat(Tag.SMB_OPA))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }

                condition();

                if (!eat(Tag.SMB_CPA))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void read_stmt()
        {

            if (eat(Tag.KW_READ))
            {
                if (!eat(Tag.ID))
                {
                    flagSyntaxError("");
                    Environment.Exit(666);
                }
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
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
                flagSyntaxError("");
                Environment.Exit(666);
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
                    flagSyntaxError("");
                    Environment.Exit(666);
                }
            }
        }

        public void expression1()
        {
            if (token.Classe.Equals(Tag.ID)      ||
                token.Classe.Equals(Tag.CON_NUM)  ||
                token.Classe.Equals(Tag.CON_CHAR) ||
                token.Classe.Equals(Tag.KW_NOT)  ||
                token.Classe.Equals(Tag.SMB_OPA))
            {
                simple_expr1();
                expression2();
            }
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
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
                flagSyntaxError("");
                Environment.Exit(666);
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
                flagSyntaxError("");
                Environment.Exit(666);
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

            else if(token.Classe.Equals(Tag.SMB_SEM)    ||      //“;”, “==”, “>”, “>=”, “<”, “<=”, “!=”, “)”
                    token.Classe.Equals(Tag.OP_EQ)      ||
                    token.Classe.Equals(Tag.OP_GT)      ||
                    token.Classe.Equals(Tag.OP_GE)      ||
                    token.Classe.Equals(Tag.OP_LT)      ||
                    token.Classe.Equals(Tag.OP_LE)      ||
                    token.Classe.Equals(Tag.SMB_OPA)    ||
                    token.Classe.Equals(Tag.OP_NE)) return; 
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
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
                flagSyntaxError("");
                Environment.Exit(666);
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

            else if(token.Classe.Equals(Tag.OP_AD)      ||
                    token.Classe.Equals(Tag.OP_MIN)     ||
                    token.Classe.Equals(Tag.KW_OR)      ||
                    token.Classe.Equals(Tag.SMB_SEM)    ||
                    token.Classe.Equals(Tag.SMB_CPA)    ||
                    token.Classe.Equals(Tag.OP_EQ)      ||
                    token.Classe.Equals(Tag.OP_GT)      ||
                    token.Classe.Equals(Tag.OP_GE)      ||
                    token.Classe.Equals(Tag.OP_LT)      ||
                    token.Classe.Equals(Tag.OP_LE)      ||
                    token.Classe.Equals(Tag.OP_NE)) return; //“+”, “-”, “or”, “;”, “==”, “>”, “>=”, “<”, “<=”, “!=”, “)”
            else
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }


            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
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
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void factor(){   //BUGUEI!

            if (!eat(Tag.ID))
            {
                //erro
            }

            //         ||       OU

            constant();

            //         ||       OU

            if (!eat(Tag.SMB_OPA))
            {
                //erro
            }

            expression1();

            if (!eat(Tag.SMB_CPA))
            {
                //erro
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
                flagSyntaxError("");
                Environment.Exit(666);
            }

        }

        public void addop()
        {
            if (!eat(Tag.OP_AD) && !eat(Tag.OP_MIN) && !eat(Tag.KW_OR))
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void mulop()
        {
            if (!eat(Tag.OP_MUL) && !eat(Tag.OP_DIV) && !eat(Tag.KW_AND))
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

        public void constant()
        {
            if (!eat(Tag.CON_NUM) && !eat(Tag.CON_CHAR))
            {
                flagSyntaxError("");
                Environment.Exit(666);
            }
        }

    }
}
