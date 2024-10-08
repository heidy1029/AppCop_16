using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    public string currentLanguage = "es"; // Idioma actual
    public Dictionary<string, Dictionary<string, string>> languages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Para que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        LoadLanguages();
    }

    void LoadLanguages()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("languages");

        if (jsonTextAsset != null)
        {
            languages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonTextAsset.text);
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo JSON desde Resources.");
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (languages.ContainsKey(currentLanguage))
        {
            if (languages[currentLanguage].ContainsKey(key))
            {
                return languages[currentLanguage][key];
            }
            else
            {
                Debug.LogWarning($"Clave '{key}' no encontrada en el idioma '{currentLanguage}'");
                return key;
            }
        }
        else
        {
            Debug.LogError($"El idioma '{currentLanguage}' no está presente en el diccionario.");
            return key;
        }
    }

    public void ChangeLanguage(string newLanguage)
    {
        if (languages.ContainsKey(newLanguage))
        {
            currentLanguage = newLanguage;

            // Actualiza los textos localizados
            UpdateAllLocalizedTexts();

            // Recarga el JSON con la información del nuevo idioma
            JsonReader jsonReader = FindObjectOfType<JsonReader>();
            if (jsonReader != null)
            {
                Root birdData = jsonReader.ReadJson(); // Cargar el nuevo archivo JSON en el idioma seleccionado

                if (birdData != null)
                {
                    // Actualizar los datos de las aves en la clase Data
                    Data dataManager = FindObjectOfType<Data>();
                    if (dataManager != null)
                    {
                        dataManager.LoadData(); // Asegúrate de que este método está disponible en la clase Data
                    }
                    else
                    {
                        Debug.LogError("Data manager not found!");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"El idioma '{newLanguage}' no está disponible.");
        }
    }



    private void UpdateAllLocalizedTexts()
    {
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();

        foreach (LocalizedText localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }
}
