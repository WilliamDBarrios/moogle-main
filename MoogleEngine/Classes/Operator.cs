using System.Collections.Generic;
namespace MoogleEngine.Logic;


class Operators {
    private List<string> opNone;
    private List<string> opAppear;
    private List<OperatorNear> opNear;
    private List<OperatorImportance> opImportance;
    private double cantImportance;
    private string[] text;

    public List<string> OpNone {
        set { opNone = value; }
        get { return opNone; }
    }
    public List<string> OpAppear {
        set { opAppear = value; }
        get { return opAppear; }
    }
    public List<OperatorNear> OpNear {
        set { opNear = value; }
        get { return opNear; }
    }
    public List<OperatorImportance> OpImportance {
        set { opImportance = value; }
        get { return opImportance; }
    }
    public double CantImportance  {
        set { cantImportance = value; }
        get { return cantImportance; }
    }
    public string[] Text{
        set { text = value; }
        get { return text; }
    }

    public Operators() {
        opNone = new List<string>();
        opAppear = new List<string>();
        opNear = new List<OperatorNear>();
        opImportance = new List<OperatorImportance>();
    }
}