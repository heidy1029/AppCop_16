using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private Text uiText;
    private TextMeshProUGUI tmpText;

    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();

        if (uiText == null && tmpText == null)
        {
            //Debug.LogError("No se encontró componente de texto en el objeto " + gameObject.name);
            return;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        string localizedValue = LanguageManager.instance.GetLocalizedValue(key);
        //Debug.Log($"Actualizando texto con la clave '{key}', valor obtenido: '{localizedValue}'");

        if (uiText != null)
        {
            uiText.text = localizedValue;
        }
        else if (tmpText != null)
        {
            tmpText.text = localizedValue;
        }
        else
        {
            //Debug.LogWarning("No se encontró componente de texto para actualizar.");
        }
    }
}