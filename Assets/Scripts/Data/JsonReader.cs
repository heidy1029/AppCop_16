using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReader : MonoBehaviour
{
    private string fileName; // Nombre del archivo se decide dinámicamente

    public Root ReadJson()
    {
        // Cambia el nombre del archivo según el idioma actual
        if (LanguageManager.instance.currentLanguage == "en")
        {
            fileName = "dataIngles"; // Carga el archivo en inglés
        }
        else
        {
            fileName = "data"; // Carga el archivo en español u otro idioma por defecto
        }

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
