using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro


public class DatabaseUploader : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI responseText;
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", "test@gmail.com");
        form.AddField("password", "111111");


        UnityWebRequest www = UnityWebRequest.Post("https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/signup", form);


        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al subir el formulario: " + www.error);
        }
        else
        {
            string serverResponse = www.downloadHandler.text;
            Debug.Log("¡Subida del formulario completa! Respuesta: " + www.downloadHandler.text);
            responseText.text = serverResponse;

        }
    }
    public void StartUpload()
    {
        StartCoroutine(Upload());
    }
}
