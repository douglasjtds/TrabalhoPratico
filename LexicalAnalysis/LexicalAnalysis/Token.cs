using LexicalAnalysis;
using System;


public class Token
{

    public Tag tag;
    private string lexema;

    public Token(Tag tag, String lexema)
    {
        this.tag = tag;
        this.lexema = lexema;
    }

    public string getLexema
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

    public Tag getTag{
        get
        {
            return tag;
        }
    }

    public string returnToken()
    {
        return "< " + tag + ", " + lexema + " > / ";
    }
}
