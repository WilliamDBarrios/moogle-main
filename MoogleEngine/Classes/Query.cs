namespace MoogleEngine.Logic;

class Query : Document{
    public Query(string content) {
        this.Content = content;
        this.Title = "query";


        this.MatrizTF = new Dictionary<string, double>();
        this.MatrizTFIDF = new Dictionary<string, double>();
    }
}