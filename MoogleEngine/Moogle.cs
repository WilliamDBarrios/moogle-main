using MoogleEngine.Logic;
namespace MoogleEngine;

// todos los metodos y archivos relacionados con el algoritmo de busqueda estan en el directorio Classes/
// Search clase donde se realiza todo el algoritmo de busqueda 


public static class Moogle
{
    
    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda
        Search search = new Search(query);
        search.InitSearch(); // inicia la busqueda

        SearchItem[] items = GetSearchItem(search.GetDocument()); // metodo GetDocument devuelve la lista de documentos encontrados
        
        return new SearchResult(items, search.Suggestion);
    }

    // Obtiene los items a enviar al usarios de los documentos buscados
    private static SearchItem[] GetSearchItem(List<Document> documents) {
        List<SearchItem> list = new List<SearchItem>();

        foreach (var item in documents)
        {
            if (item.Score == 0) {
                continue;
            }

            list.Add(new SearchItem(item.Title, item.Snippet, (float)(item.Score)));
        }

        SearchItem[] items = new SearchItem[list.Count];
        int pos = 0;

        foreach (var item in list)
        {
            items[pos] = item;
            pos++;
        }

        return items;
    }
}
