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
    // Este método alterna entre inglés y español cada vez que se hace clic
    public void ToggleLanguage()
    {
        // Si el idioma actual es español (es), cambia a inglés (en), de lo contrario cambia a español
        string newLanguage = currentLanguage == "es" ? "en" : "es";

        // Cambia el idioma usando el método ChangeLanguage
        ChangeLanguage(newLanguage);
    }

    public void ChangeLanguage(string newLanguage)
    {
        // Verifica si el idioma seleccionado está disponible
        if (languages.ContainsKey(newLanguage))
        {
            // Actualiza el idioma actual a newLanguage
            currentLanguage = newLanguage;

            // Actualiza todos los textos localizados en la interfaz
            UpdateAllLocalizedTexts();
            Debug.Log("Idioma cambiado a: " + currentLanguage);

            // Recarga el archivo JSON con la información del nuevo idioma
            JsonReader jsonReader = FindObjectOfType<JsonReader>();
            if (jsonReader != null)
            {
                Root birdData = jsonReader.ReadJson(); // Cargar el archivo JSON en el idioma seleccionado

                if (birdData != null)
                {
                    // Actualiza los datos de las aves en la clase Data
                    Data dataManager = FindObjectOfType<Data>();
                    if (dataManager != null)
                    {
                        dataManager.LoadData(); // Asegúrate de que este método existe en la clase Data
                    }
                    else
                    {
                        Debug.LogError("¡No se encontró el administrador de datos (Data manager)!");
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