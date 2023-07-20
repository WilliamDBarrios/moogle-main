using System.Collections.Generic;
using System;

namespace MoogleEngine.Logic;

class TFIDF {
    // Calcula el TF a los documentos
    static public Dictionary<string, double> TermFrecuency(string[] text) {
        Dictionary<string, double> matrizTF = new Dictionary<string, double>();

        foreach (var item in text) {
            if (matrizTF.ContainsKey(item) == false) {
                matrizTF.Add(item, 1);
            }

            else {
                matrizTF[item]++;
            }
        }

        return matrizTF;
    }

    // Calcula el IDF a los documentos
    static public Dictionary<string, double> InverseDocumentFrecuency(int cantDocuments, List<Document> list) {
        Dictionary<string, double> aux = new Dictionary<string, double>();
        foreach (var item in list) {
            foreach (var i in item.MatrizTF) {
                if (aux.ContainsKey(i.Key) == false) {
                    aux.Add(i.Key, 1);
                }

                else { 
                    aux[i.Key]++;
                }
            }
        }

        Dictionary<string, double> matrizIDF = new Dictionary<string, double>();

        foreach (var item in aux) {
            matrizIDF.Add(item.Key, Math.Log((cantDocuments + 1) / item.Value));
        }

        foreach (var item in matrizIDF) {
            //if(item.Value != 0.8450980400142568) {}
            //Console.WriteLine(item);
        }
        return matrizIDF;
    }

    // Calcula el TFIDF a los documentos
    static public Dictionary<string, double> TFIDFDocuments(Dictionary<string, double> matrizTF, Dictionary<string, double> matrizIDF) {
        Dictionary<string, double> aux = new Dictionary<string, double>();

        foreach (var item in matrizTF) {
            if(aux.ContainsKey(item.Key) == false) {
                aux.Add(item.Key, item.Value * matrizIDF[item.Key]);
            }
        }

        return aux;
    }


    // Calcula el TFidf para la query
    static public Dictionary<string, double> TFIDFQuery(string[] query, Dictionary<string, double> matrizIDF, List<OperatorImportance> imp) {
        // Calcula el tf para la query
        Dictionary<string, double> matrizTF = TermFrecuency(query);

        foreach (var item in matrizTF) {
            //Console.WriteLine(item);
        }
        Dictionary<string, double> sol = new Dictionary<string, double>();

        foreach (var item in matrizTF) {
            if (sol.ContainsKey(item.Key) == false && matrizIDF.ContainsKey(item.Key)) {
                sol.Add(item.Key, item.Value * matrizIDF[item.Key]);
            }
        }

        // le pasa la importancia de archivos de la query
        foreach (var item in imp) {
            try
            {
                sol[item.Word] *= 2 * item.Value;
            }
            catch (System.Exception) {
            }
        }
        return sol;
    }
}
