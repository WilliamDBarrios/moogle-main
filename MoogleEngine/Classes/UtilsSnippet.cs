using System;

namespace MoogleEngine.Logic;

class UtilsSnippet {
    // calcula el snippet
    static public string CalcSnippet(string[] text, string[] query, int window) {
        int pos = CalcSnippetValue(text, query, window);
        int posA = pos - window;
        int posB = pos + window;
        string snippet = "";

        if (posA < 0) {
            posA = 0;
        }

        if (posB >= text.Length) {
            posB = text.Length - 1;
        }


        // Construye la el snippet
        for (int i = posA ; i < posB; i++) {
            try
            {
                snippet = snippet + text[i] + " ";
            }
            catch (System.Exception) {}
        }
        
        return snippet;
    }

    // calcula donde esta la posicion del mejor snippet
    static public int CalcSnippetValue(string[] text, string[] query, int window) {
        int max = 0;
        int posMax = 0;

        for (int i = 0; i < text.Length; i++) {
            foreach (var item in query) {
                if (text[i] == item) {
                    int cant = CantOcurrence(text, i, query, window);

                    if (max < cant) {
                        max = cant;
                        posMax = i;
                    }
                    break;
                }
            }
        }

        return posMax;
    }

    // cantidad de veces que aparece una palabra en un texto
    static private int CantOcurrence(string[] text, int pos, string[] query, int window) {
        int cant = 1;

        for(int i = 1; i < window; i++) {
            foreach (var item in query) {
                try
                {    
                    if(text[pos + i] == item)cant++;
                }
                catch (System.Exception) {}
                
                try
                {
                    
                    if(text[pos - i] == item) {
                        cant++;
                    }
                }
                catch (System.Exception) {}
            }
        }
        return cant;
    }
}