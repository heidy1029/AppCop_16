// Assets/Scripts/Juego/Example.cs
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public List<BirdInfo> birdInfos = new List<BirdInfo>();
    public List<ModelQuestion> allModelQuestions = new List<ModelQuestion>();

    private JsonReader jsonReader;

    void Start()
    {
        jsonReader = GetComponent<JsonReader>();

        if (jsonReader != null)
        {
            Root root = jsonReader.ReadJson();
            InitializeData(root);
        }
        else
        {
            Debug.LogError("JsonReader component not found!");
        }
    }

    /// <summary>
    /// Inicializa los datos a partir del objeto Root deserializado.
    /// </summary>
    private void InitializeData(Root root)
    {
        if (root != null && root.BirdType != null)
        {
            birdInfos = new List<BirdInfo>();
            allModelQuestions = root.BirdType.AllModelQuestions;

            foreach (var modelQuestion in root.BirdType.AllModelQuestions)
            {
                birdInfos.Add(modelQuestion.BirdInfo);
            }
        }
        else
        {
            Debug.LogError("Invalid JSON data!");
        }
    }

    /// <summary>
    /// Obtiene la información del ave basada en el modelIndex.
    /// </summary>
    /// <param name="modelIndex">Índice del modelo.</param>
    /// <returns>Objeto BirdInfo correspondiente.</returns>
    public BirdInfo GetBirdInfo(int modelIndex)
    {
        BirdInfo birdInfo = birdInfos.Find(b => b.ModelIndex == modelIndex);
        if (birdInfo == null)
        {
            Debug.LogWarning($"BirdInfo with modelIndex {modelIndex} not found.");
        }
        return birdInfo;
    }
}
