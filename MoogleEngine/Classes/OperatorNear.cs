namespace MoogleEngine.Logic;

class OperatorNear {
    private string first ;
    private string second;

    public string First {
        set { first = value; }
        get { return first; }
    }
    public string Second {
        set { second = value; }
        get { return second; }
    }

    public OperatorNear(string first, string second) {
        this.first = first;
        this.second = second;
    }
}