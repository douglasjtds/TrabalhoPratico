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

        public void flagSyntaxError(string message)
        {

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

            }
        }

        public void body()
        {
        }

        public void decl_list()
        {
        }

        public void decl()
        {
        }

        public void type()
        {
        }

        public void id_list1()
        {
        }

        public void id_list2()
        {
        }

        public void stmt_list()
        {
        }

        public void stmt()
        {
        }

        public void assign_stmt()
        {
        }

        public void If_stmt1()
        {
        }

        public void If_stmt2()
        {
        }
        public void condition()
        {
        }

        public void While_stmt()
        {
        }

        public void stmt_prefix()
        {
        }

        public void read_stmt()
        {
        }

        public void write_stmt()
        {
        }

        public void writable()
        {
        }

        public void expression1()
        {
        }

        public void expression2()
        {
        }

        public void simple_expr1()
        {
        }

        public void simple_expr2()
        {
        }

        public void term1()
        {
        }

        public void term2()
        {
        }

        public void factor_a()
        {
        }
        
        public void factor(){
        }
        
        public void relop()
        {
        }

        public void addop()
        {
        }

        public void mulop()
        {
        }

        public void constant()
        {
        }


    }
}
