using LexicalAnalysis;
using System;
using System.Collections;


public class Token
{

    private string lexema;
    public Tag tag;
    private int linha;
    private int coluna;

    public Token(Tag tag, String lexema, int linha, int coluna)
    {
        this.tag = tag;
        this.lexema = lexema;
        this.linha = linha;
        this.coluna = coluna;
    }

    public String returnToken()
    {
        return "<" + tag + ", \"" + lexema + "\">";
    }
}
