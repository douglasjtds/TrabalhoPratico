using System;
using LexicalAnalysis;

/// <summary>
/// Classe para as regras do Token
/// </summary>
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

    /// <summary>
    /// Get/Set do atributo Coluna
    /// </summary>
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

    /// <summary>
    /// Construtor da classe Token 
    /// </summary>
    /// <param name="classe">Tag que define o lexema</param>
    /// <param name="lexema">Lexema formado ao ler o algoritmo PasC</param>
    /// <param name="linha">Linha que o token foi encontrado</param>
    /// <param name="coluna">Coluna que o token foi encontrado</param>
    public Token(Tag classe, String lexema, int linha, int coluna)
    {
        this.Classe = classe;
        this.Lexema = lexema;
        this.Linha = linha;
        this.Coluna = coluna;
    }

    /// <summary>
    /// Método de print da classe Token
    /// </summary>
    /// <returns></returns>
    public override String ToString()
    {
        return "<" + Classe + ", \"" + Lexema + "\">" + " - " + "Linha: " + Linha + " / " + "Coluna: " + Coluna;
    }
}