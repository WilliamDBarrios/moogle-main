

using System.Collections.Generic;
using System;
namespace MoogleEngine.Logic;

class Search {
    private string[] query;
    private string suggestion;
    private Operators operators;
    private string[] opNone;
    private double normaQuery;
    private Dictionary<string, double> dictQuery;
    private DataBase db;

    public string[] Query
    {
        get { return query; }
        set { query = value; }
    }
    public string Suggestion {
        get { return suggestion;}
    }

    public Search(string query) {
        this.query = Tokenize.GetTokenize(query);
        this.operators = Tokenize.GetOperators(this.query);
        this.query = operators.Text;

        dictQuery = new Dictionary<string, double>();

        LoadDB();
    }

    private void LoadDB() {
        db = new DataBase();
    }

    // Inicia el algoritmo de busqueda
    public void InitSearch() {
        PreCalcDocuments();

        CalcTFIDFQuery();

        CalcOperaratorNear();

        CalcScore();

        SortDocuments();

        GetSnippets();

        getSuggestion();
    }

    // Calcula el tfidf de la query
    private void CalcTFIDFQuery() {
        dictQuery = TFIDF.TFIDFQuery(query, db.MatrizIDF, operators.OpImportance);

        foreach (var item in dictQuery) {

            //Console.WriteLine(item);
        }
    }

    // Realiza todo los calculos para los documentos
    private void PreCalcDocuments() {
        // obtiene el idf de los documentos
        db.MatrizIDF = TFIDF.InverseDocumentFrecuency(db.Documents.Count, db.Documents);

        foreach (var item in db.Documents) {
            item.MatrizTFIDF = TFIDF.TFIDFDocuments(item.MatrizTF, db.MatrizIDF);

            foreach (var i in item.MatrizTFIDF) {
                foreach (var j in query)    {
                    //if(j == i.Key)Console.WriteLine(i);
                }
            }
        }
        //Console.WriteLine("Hola hay un intermedio aqui");
    }

    private void CalcOperaratorNear() {
        foreach (var item in db.Documents)
        {
            foreach (var op in this.operators.OpNear)
            {
                int value = Utils.CalcNear(item.Text, op.First, op.Second);

                //Console.WriteLine(value + " " +  item.Title); // C# 1205 -03071097734205798 - historia de la computacion 5284 - 022760760485995853

                if ( value != -1) {
                    if (value <= 50) {
                        value = (1 / value) + 5;
                    }
                    if (value > 50  && value <= 300) {
                        value = (1 / value) + 4;
                    }
                    if (value > 300  && value <= 1000) {
                        value = (1 / value) + 3;
                    }
                    if (value > 1000  && value <= 10000) {
                        value = (1 / value) + 2;
                    }
                    if (value > 10000) {
                        value = (1 / value) + 1;
                    }

                    item.MatrizTFIDF[op.First] *= value;
                }
            }
        }
    }

    // Calcula los score de los ducumentos usando modelo vectorial
    private void CalcScore() {
        foreach (var item in db.Documents) {
            item.Score = VectorialModel.GetScore(item.MatrizTFIDF, dictQuery, operators);
        }
    }

    private void SortDocuments() {
        db.Documents.Sort(delegate (Document a, Document b) {
            if (a.Score < b.Score) return 1;
            return -1;
        });
    }

    private void GetSnippets() {
        foreach (var item in db.Documents) {
            item.Snippet = UtilsSnippet.CalcSnippet(item.Text, query, 25);
        }

        foreach (var item in db.Documents) {

            //if(item.Score != 0)Console.WriteLine(item.Title + " : _-----" + item.Snippet + " " + item.Score);
        }
    }

    private void getSuggestion() {
        Suggestion suggestion = new Suggestion(Config.SynonymPath);

        this.suggestion = suggestion.getSuggestion(query);

        foreach (var item in this.suggestion)
        {
            //Console.WriteLine(item);
        }
    }

    public List<Document> GetDocument() {
        return db.Documents;
    }

}

