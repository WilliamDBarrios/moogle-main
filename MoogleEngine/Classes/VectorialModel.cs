using System;
using System.Collections.Generic;
namespace MoogleEngine.Logic;

class VectorialModel {

    // comprobacion para el operador !( no este en el documento ) y ^ ( este en el documento )
    static private bool isValidDocument(List<string> none, List<string> appear, Dictionary<string, double> doc) {
        foreach (var item in none) {
            if(doc.ContainsKey(item) == true) {
                return false;
            }
        }
        
        foreach (var item in appear) {
            if(doc.ContainsKey(item) == false) {
                return false;
            }
        }
        return true;
    }

    // Calcula el score usando modelo vectorial
    static public double GetScore(Dictionary<string, double> document, Dictionary<string, double> query, Operators operators) {
        if(isValidDocument(operators.OpNone, operators.OpAppear, document) == false) {
            return 0;
        }

        double dotProduct = DotProduct(document, query);

        double normaDocument = GetNorma(document);
        double normaQuery = GetNorma(query);

        if (normaDocument * normaQuery == 0) { // si no ay palabras comunes el score vale 0
            return 0;
        }

        Console.WriteLine((dotProduct / (normaDocument * normaQuery)));
        return (dotProduct / (normaDocument * normaQuery));
    }

    // Calcula el dotProduct
    static double DotProduct(Dictionary<string, double> document, Dictionary<string, double> query) {
        double dotProduct = 0;

        foreach (var item in query) {
            if (document.ContainsKey(item.Key) == true) {
                dotProduct += document[item.Key] * item.Value;
            }
        }
        return dotProduct;
    }

    // Calcula la norma
    static double GetNorma(Dictionary<string, double> d) {
        double norma = 0;

        foreach (var item in d) {
            try
            {
                double value = item.Value;
                norma += value * value;
            }
            catch (System.Exception) {
            }
        }

        return Math.Sqrt(norma);
    }
}