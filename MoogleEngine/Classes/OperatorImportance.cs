namespace MoogleEngine.Logic;

class OperatorImportance{
    private string word;
    private double value;
    public string Word {
        set { word = value; }
        get { return word; }
    }

    public double Value {
        set { value = value; }
        get { return value; }
    }

    public OperatorImportance(string word, double value) {
        this.word = word;
        this.value = value;
    }
}