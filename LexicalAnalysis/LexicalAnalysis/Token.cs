using System;
using LexicalAnalysis;

public class Token
{

    private String lexema;
    private LexicalAnalysis.Tag classe;
    private int linha;
    private int coluna;

    public string Lexema
    {
        get
        {
            return lexema;
        }

        set
        {
            lexema = value;
        }
    }

    public Tag Classe
    {
        get
        {
            return classe;
        }

        set
        {
            classe = value;
        }
    }

    public int Linha
    {
        get
        {
            return linha;
        }

        set
        {
            linha = value;
        }
    }

    public int Coluna
    {
        get
        {
            return coluna;
        }

        set
        {
            coluna = value;
        }
    }

    public Token(Tag classe, String lexema, int linha, int coluna)
    {
        this.Classe = classe;
        this.Lexema = lexema;
        this.Linha = linha;
        this.Coluna = coluna;
    }

    
    public override String ToString()
    {
        return "<" + Classe + ", \"" + Lexema + "\">";
    }
}