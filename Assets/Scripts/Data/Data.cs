// Assets/Scripts/Juego/Example.cs
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private Root root;
    private List<BirdInfo> birdInfos = new List<BirdInfo>();
    private List<ModelQuestion> allModelQuestions = new List<ModelQuestion>();

    private JsonReader jsonReader;

    void Start()
    {
        jsonReader = GetComponent<JsonReader>();

        if (jsonReader != null)
        {
            root = jsonReader.ReadJson();
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
        if (root != null && root.BirdTypes != null)
        {
            birdInfos = new List<BirdInfo>();
            allModelQuestions = new List<ModelQuestion>();

            foreach (var birdType in root.BirdTypes)
            {
                if (birdType.AllModelQuestions != null)
                {
                    allModelQuestions.AddRange(birdType.AllModelQuestions);

                    foreach (var modelQuestion in birdType.AllModelQuestions)
                    {
                        birdInfos.Add(modelQuestion.BirdInfo);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Invalid JSON data!");
        }
    }

    // Método actualizado para obtener preguntas por birdTypeId
    public List<ModelQuestion> GetModelQuestions(int birdTypeId)
    {
        // Usa el root deserializado para buscar el birdType con el birdTypeId especificado
        BirdType birdType = root.BirdTypes.Find(bt => bt.BirdTypeId == birdTypeId);
        if (birdType == null)
        {
            Debug.LogWarning($"BirdType with birdTypeId {birdTypeId} not found.");
            return null;
        }

        // Devuelve las preguntas asociadas con el birdTypeId
        return birdType.AllModelQuestions;
    }

    public BirdInfo GetBirdInfo(int modelIndex)
    {
        var birdInfo = birdInfos.Find(bi => bi.ModelIndex == modelIndex);
   
        return birdInfo;
    }
}
