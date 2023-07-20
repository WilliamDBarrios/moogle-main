using System;
namespace MoogleEngine.Logic;


class Utils {
    static private int Min(int a, int b, int c) {
        return Math.Min(a, Math.Min(b, c));
    }
    
    // calcula la cercania de dos palabras en un documento
    public static int CalcNear(string[] text, string wordSearched, string word) {
        int value = int.MaxValue;
        bool mk = false;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == word){

                int posLeft = posWordLeft(text, wordSearched, i);
                int posRight = posWordRight(text, wordSearched, i);
                
                if (posLeft == -1 && posRight == -1) { // si no encontro coincidencia a la izquierda y a la derecha
                    return -1;
                }
                else {
                    int min = 0;

                    if ((posLeft == -1 && posRight != -1 ) || (posLeft != -1 && posRight == -1)) { // si uno de los dos lados no encontro coincidencia pero no los dos
                        min = Math.Max(posLeft, posRight);
                    }
                    else { // si los dos lados devuelven una coincidencia
                        min = Math.Min(posLeft, posRight);
                    }

                    value = Math.Min(value, min);
                    mk = true;
                }

            }
        }

        return mk == true ? value : -1; // si no encontro ninguna coincidencia devuelve -1
    }

    // revisa desde la posicion hasta el inicio del array la primera aparicion que encuentra de la palabra buscada
    private static int posWordLeft(string[] text, string wordSearched, int position) {
        for (int i = position - 1 ; i >= 0; i--)
        {
            if (text[i] == wordSearched) {
                return i;
            }
        }

        return -1;
    }

    // revisa desde la posicion hasta el final del array la primera aparicion que encuentra de la palabra buscada
    private static int posWordRight(string[] text, string wordSearched, int position) {
        for (int i = position; i < text.Length; i++)
        {
            if (text[i] == wordSearched) {
                return i;
            }
        }

        return -1;
    }

    public static int EditDistance(string query, string synonym, int n, int m) {
        if (n == 0) return m;

        if (m == 0) return n;

        if (query[n - 1] == synonym[m - 1]) {
            return EditDistance(query, synonym, n - 1, m - 1);
        }

        return 1 + Min( EditDistance(query, synonym, n, m - 1), // Insertar
                    EditDistance(query, synonym, n - 1, m), // Remover
                    EditDistance(query, synonym, n - 1, m - 1)  // Remplazar
                    );
    }
    public static int EditDistanceDP(string query, string synonym) {
        int n = query.Length;
        int m = synonym.Length;
        int[,] dp = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) {
            for (int j = 0; j <= m; j++) {
                if (i == 0) {
                    dp[i, j] = j;
                }

                else if (j == 0) {
                    dp[i, j] = i;
                }
                
                else if (query[i - 1] == synonym[j - 1]){
                     dp[i, j] = dp[i - 1, j - 1];
                }

                else {
                    dp[i, j] = 1 + Min(dp[i, j - 1], 
                        dp[i - 1, j], 
                        dp[i - 1, j - 1]);
                }                        
            }
        }

        return dp[n, m];
    }
}