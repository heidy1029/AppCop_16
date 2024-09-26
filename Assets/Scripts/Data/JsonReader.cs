using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReader : MonoBehaviour
{
    public string fileName = "data";

    public Root ReadJson()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(fileName);
        if (jsonTextAsset == null)
        {
            Debug.LogError($"No se pudo cargar el archivo JSON desde Resources/{fileName}.json");
            return null;
        }

        Root root = JsonConvert.DeserializeObject<Root>(jsonTextAsset.text);
        return root;
    }
}