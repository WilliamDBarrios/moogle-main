

using System.Collections.Generic;

using System;
namespace MoogleEngine.Logic;

class Document
{
    private string title;
    private string content;
    private double score;
    private string snippet;
    private string[] text;
    private int CantWords;
    private Dictionary<string, double> matrizTF;
    private Dictionary<string, double> matrizTFIDF;
    private Dictionary<string, List<int> > near;

    public string Title {
        get { return title; }
        set { title = value; }
    }
    public string Content {
        get { return content; }
        set {
            content = value;
            SetText();
        }
    }
    public string Snippet {
        get { return snippet; } 
        set { snippet = value; }
    }
    public double Score {
        get { return score; } 
        set { score = value; } 
    }
    public string[] Text {
        get { return text; }
    }
    public Dictionary<string, double> MatrizTF {
        get { return matrizTF; }
        set { matrizTF = value; }
    }
    public Dictionary<string, double> MatrizTFIDF {
        get { return matrizTFIDF; }
        set { matrizTFIDF = value; }
    }
    public Dictionary<string, List<int> > Near {
        get { return near; }
        set { near = value; }
    }
    
    public Document(string title, string content) {
        this.title = Tokenize.getTittle(title);
        this.content = content;

        matrizTF = new Dictionary<string, double>();
        matrizTFIDF = new Dictionary<string, double>();
        near = new Dictionary<string, List<int>>(); 
    }
    public Document(string content) {
        this.content = content;

        matrizTF = new Dictionary<string, double>();
        matrizTFIDF = new Dictionary<string, double>();
    }
    public Document() {
        this.title = "";
        this.content = "";
    }


    // Tokeniza el texto y le halla el TF
    public void SetText() {
        text = Tokenize.GetTokenize(content);
        CantWords = text.Length;
        
        matrizTF = TFIDF.TermFrecuency(text);
    }
}