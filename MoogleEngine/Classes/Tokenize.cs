
using System.Collections.Generic;


using System;
namespace MoogleEngine.Logic;

static class Tokenize {
    private static char[] token = "¿#$%&'(<>)+,-./:;=?@[]_`{|}  \"\n".ToCharArray();

    static public string[] GetTokenize(string text) {
        text = NormalizeWord(text);
        string s= "\n";
        
        return text.Split(token, System.StringSplitOptions.RemoveEmptyEntries);
    }
    
    // Normaliza una cadena de texto
    public static string NormalizeWord(string word) {
        string w = word.ToLower();
        string newWord = "";

        foreach (char item in w) {
            switch (item) {
                case 'á':
                    newWord += 'a';
                    break;
                case 'é':
                    newWord += 'e';
                    break;
                case 'í':
                    newWord += 'i';
                    break;
                case 'ó':
                    newWord += 'o';
                    break;
                case 'ú':
                    newWord += 'u';
                    break;         
                default: 
                    newWord += item;
                    break;

            }
        }

        return newWord;
    }

    // Modifica el tittle para quitarle la extension .txt al nombre
    static public string getTittle(string text) {
        string[] aux = text.Split(".");
        string response = "";

        for (int i = 0; i < aux.Length - 1; i++) {
            response = response + aux[i] + ".";
        }

        try{
            response = response.Remove(response.Length - 1, 1);
        }
        catch (System.Exception) { }

        return response;
    }


    // Obtiene los operadores de la
    static public Operators GetOperators(string[] text) {
        Operators op = new Operators();

        op.Text = text;

        for (int i = 0; i < text.Length; i++) {
            // revisa para los operadores ! * ^
            switch (text[i][0]) {
                case '!':
                    string none = text[i].Substring(1, text[i].Length - 1);
                    op.OpNone.Add(none);
                    op.Text[i] = none;

                    break;
                case '^':
                    string appear = text[i].Substring(1, text[i].Length - 1);
                    op.OpAppear.Add(appear);
                    op.Text[i] = appear;

                    break;
                case '*':
                    string[] aux = text[i].Split("*".ToCharArray());
                    OperatorImportance imp = new OperatorImportance(aux[aux.Length - 1], aux.Length - 1);
                    op.OpImportance.Add(imp);
                    op.Text[i] = aux[aux.Length - 1];

                    break;
            }

            // para el operador ~
            if (text[i] == "~") {
                try
                {
                    string first = text[i - 1];
                    string second = text[i + 1];
                    
                    op.OpNear.Add(new OperatorNear(first, second));
                }
                catch (System.Exception)    {
                }
            }
        }
        
        foreach (var item in op.OpNear) {
            //Console.WriteLine(item.Second);
        }
        
        return op;
    }
}