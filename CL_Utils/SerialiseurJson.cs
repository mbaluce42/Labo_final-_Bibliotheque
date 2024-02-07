using CL_bibliotheque;
using Newtonsoft.Json;

namespace CL_Utils
{
    public class SerialiseurJson
    {
        public static void Serialiser(Bibliotheque bibli, string fileName)
        {
            var json = JsonConvert.SerializeObject(bibli, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public static Bibliotheque? Deserialiser(string fileName)
        {
            var json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Bibliotheque>(json);
        }
    }
}

/*

Ce code est écrit en langage C# et utilise deux bibliothèques externes : "CL_bibliotheque" et "Newtonsoft.Json". Voici les lignes de code et leur fonctionnalité :

1. `using CL_bibliotheque;` : Importe la bibliothèque CL_bibliotheque. Cette ligne permet d'accéder aux classes et fonctionnalités de cette bibliothèque.

2. `using Newtonsoft.Json;` : Importe la bibliothèque Newtonsoft.Json. Cette ligne permet d'utiliser les fonctionnalités de cette bibliothèque pour la sérialisation et la désérialisation d'objets JSON.

3. `namespace CL_Utils` : Déclare un espace de noms appelé "CL_Utils". Un espace de noms est utilisé pour organiser et regrouper des classes et des fonctions apparentées.

4. `public class SerialiseurJson` : Définit une classe publique appelée "SerialiseurJson". Cette classe contient des méthodes pour la sérialisation et la désérialisation d'objets en format JSON.

5. `public static void Serialiser(Bibliotheque bibli, string fileName)` : Déclare une méthode statique publique appelée "Serialiser" qui prend en paramètres un objet de type "Bibliotheque" et une chaîne de caractères "fileName". Cette méthode sérialise l'objet "bibli" en format JSON en utilisant la bibliothèque Newtonsoft.Json et écrit le résultat dans un fichier spécifié par "fileName".

6. `var json = JsonConvert.SerializeObject(bibli, Formatting.Indented);` : Sérialise l'objet "bibli" en format JSON en utilisant la méthode "SerializeObject" de la bibliothèque Newtonsoft.Json. Le paramètre "Formatting.Indented" spécifie que le JSON sera formaté de manière lisible avec des indentations.

7. `File.WriteAllText(fileName, json);` : Écrit le contenu de la variable "json" dans un fichier spécifié par "fileName" en utilisant la méthode "WriteAllText" de la classe "File". Cette ligne enregistre le JSON sérialisé dans un fichier.

8. `public static Bibliotheque? Deserialiser(string fileName)` : Déclare une méthode statique publique appelée "Deserialiser" qui prend en paramètre une chaîne de caractères "fileName". Cette méthode lit le contenu d'un fichier spécifié par "fileName" et désérialise le JSON en un objet de type "Bibliotheque".

9. `var json = File.ReadAllText(fileName);` : Lit le contenu d'un fichier spécifié par "fileName" en utilisant la méthode "ReadAllText" de la classe "File". Le contenu du fichier est stocké dans la variable "json".

10. `return JsonConvert.DeserializeObject<Bibliotheque>(json);` : Désérialise le contenu de la variable "json" en utilisant la méthode "DeserializeObject" de la bibliothèque Newtonsoft.Json. Le type spécifié entre les chevrons "<>" indique le type d'objet dans lequel le JSON sera désérialisé, dans ce cas, un objet de type "Bibliotheque". La méthode renvoie l'objet désérialisé.

*/