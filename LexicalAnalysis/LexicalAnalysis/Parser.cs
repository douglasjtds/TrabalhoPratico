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
                    return; //Makesense?
                }

                body();

                if (!eat(Tag.EOF))
                {
                    flagSyntaxError("Era esperado o fim de arquivo, mas o encontrado foi: " + token.Lexema);
                    return; //Makesense?
                }
            }
            else
            {
                flagSyntaxError("Era esperado: \"program\", mas o encontrado foi: " + token.Lexema);
            }
        }

        public void body()
        {
            decl_list();

            if (!eat(Tag.SMB_OBC))
            {
                //erro
            }

            stmt_list();

            if (!eat(Tag.SMB_CBC))
            {

            }

        }

        public void decl_list()
        {
            decl();

            if (!eat(Tag.SMB_SEM))
            {
                //erro
            }

            decl_list();

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void decl()
        {
            type();

            id_list1();
        }

        public void type()
        {
            if(!eat(Tag.KW_NUM) || !eat(Tag.KW_CHAR))
            {
                //erro
            }
        }

        public void id_list1()
        {
            if (!eat(Tag.ID))
            {
                //erro
            }

            id_list2();
        }

        public void id_list2()
        {
            if (!eat(Tag.SMB_COM))
            {
                //erro
            }

            id_list1();

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void stmt_list()
        {
            stmt();

            if (!eat(Tag.SMB_SEM))
            {
                //erro
            }

            stmt_list();

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void stmt()
        {
            assign_stmt();
            If_stmt1();
            While_stmt();
            read_stmt();
            write_stmt();
        }

        public void assign_stmt()
        {
            if (!eat(Tag.ID))
            {
                //erro
            }

            if (!eat(Tag.OP_ASS))
            {
                //erro
            }

            simple_expr1();
        }

        public void If_stmt1()
        {
            if (!eat(Tag.KW_IF))
            {
                //erro
            }

            if (!eat(Tag.SMB_OPA))
            {
                //erro
            }

            condition();

            if (!eat(Tag.SMB_CPA))
            {
                //erro
            }

            if (!eat(Tag.SMB_OBC))
            {
                //erro
            }

            stmt_list();

            if (!eat(Tag.SMB_CBC))
            {
                //erro
            }

            If_stmt2();

        }

        public void If_stmt2()
        {
            if (!eat(Tag.KW_ELSE))
            {
                //erro
            }

            if (!eat(Tag.SMB_OBC))
            {
                //erro
            }

            stmt_list();

            if (!eat(Tag.SMB_CBC))
            {
                //erro
            }

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }


        public void condition()
        {
            expression1();
        }

        public void While_stmt()
        {
            stmt_prefix();

            if (!eat(Tag.SMB_OBC))
            {
                //erro
            }

            stmt_list();

            if (!eat(Tag.SMB_CBC))
            {
                //erro
            }

        }

        public void stmt_prefix()
        {
            if (!eat(Tag.KW_WHILE))
            {
                //erro
            }

            if (!eat(Tag.SMB_OPA))
            {
                //erro
            }

            condition();

            if (!eat(Tag.SMB_CPA))
            {
                //erro
            }

        }

        public void read_stmt()
        {

            if (!eat(Tag.KW_READ))
            {
                //erro
            }

            if (!eat(Tag.ID))
            {
                //erro
            }

        }

        public void write_stmt()
        {
            if (!eat(Tag.KW_WRITE))
            {
                //erro
            }

            writable();
        }

        public void writable()
        {
            simple_expr1();

            //         ||       OU

            if (!eat(Tag.LIT))
            {
                //erro
            }

        }

        public void expression1()
        {
            simple_expr1();

            expression2();
        }

        public void expression2()
        {
            relop();

            simple_expr1();

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void simple_expr1()
        {
            term1();

            simple_expr2();
        }

        public void simple_expr2()
        {
            addop();

            term1();

            simple_expr2();
            
            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void term1()
        {
            factor_a();

            term2();
        }

        public void term2()
        {
            mulop();

            factor_a();

            term2();

            //TUDO QUE ESTÁ ACIMA OU VAZIO! 
        }

        public void factor_a()
        {
            factor();

            //         ||       OU

            if (!eat(Tag.KW_NOT))
            {
                //erro
            }

            factor();

        }

        public void factor(){

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

        public void relop()
        {

            if (token.Classe.Equals(Tag.OP_EQ) || token.Classe.Equals(Tag.OP_GT) || token.Classe.Equals(Tag.OP_GE) || token.Classe.Equals(Tag.OP_LT) || token.Classe.Equals(Tag.OP_LE) || token.Classe.Equals(Tag.OP_NE))
            {
                //erro
            }

        }

        public void addop()
        {
            if (!eat(Tag.OP_AD) && !eat(Tag.OP_MIN) && !eat(Tag.KW_OR))
            {
                //erro
            }
        }

        public void mulop()
        {
            if (!eat(Tag.OP_MUL) && !eat(Tag.OP_DIV) && !eat(Tag.KW_AND))
            {
                //erro
            }
        }

        public void constant()
        {
            if (!eat(Tag.KW_NUM) && !eat(Tag.KW_CHAR))
            {
                //erro
            }
        }

    }
}
