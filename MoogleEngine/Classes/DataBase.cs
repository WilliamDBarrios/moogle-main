
using System.Collections.Generic;
using System;
using System.IO;
namespace MoogleEngine.Logic;

class DataBase {

    private List<Document> documents = new List<Document>();
    private Dictionary<string, double> matrizIDF;
    private Dictionary<string, double> matrizTFIDF;

    public List<Document> Documents {
        get { return documents; }
        set { documents = value; }
    }
    public Dictionary<string, double> MatrizIDF {
        get { return matrizIDF; }
        set { matrizIDF = value; }
    }

    public Dictionary<string, double> MatrizTFIDF {
        get { return matrizTFIDF; }
        set { matrizTFIDF = value; }
    }

    public DataBase() {
        getFiles(Config.StartPath);
    }

    // carga los archivos y los agrega a la List de Documents
    private void getFiles(string path) {
        if (Directory.Exists(path) == false) {
            Console.WriteLine(1);
            //return;
        }

        DirectoryInfo di = new DirectoryInfo(path);
        foreach (var fi in di.GetFiles()) {
            string text = File.ReadAllText(fi.FullName);
            Document doc = new Document(fi.Name, text);
            doc.SetText();
            
            documents.Add(doc);
        }
        
        foreach (var doc in documents) {
        }
    }
}