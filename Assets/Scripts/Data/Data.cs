// Assets/Scripts/Juego/Data.cs
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data : MonoBehaviour
{
    private Root root;

    private Dictionary<int, Dictionary<int, ModelQuestion>> modelQuestionsByBirdType = new Dictionary<int, Dictionary<int, ModelQuestion>>();

    private Dictionary<int, Dictionary<int, BirdInfo>> birdInfosByBirdType = new Dictionary<int, Dictionary<int, BirdInfo>>();

    private JsonReader jsonReader;

    void Start()
    {
        jsonReader = GetComponent<JsonReader>();

        if (jsonReader != null)
        {
            root = jsonReader.ReadJson();
            InitializeData(root);

            //PrintAllBirdInfoAsJson();
        }
        else
        {
            Debug.LogError("JsonReader component not found!");
        }
    }

    private void InitializeData(Root root)
    {
        if (root != null && root.BirdTypes != null)
        {
            modelQuestionsByBirdType = new Dictionary<int, Dictionary<int, ModelQuestion>>();
            birdInfosByBirdType = new Dictionary<int, Dictionary<int, BirdInfo>>();

            foreach (var birdType in root.BirdTypes)
            {
                if (birdType.AllModelQuestions != null)
                {
                    modelQuestionsByBirdType[birdType.BirdTypeId] = new Dictionary<int, ModelQuestion>();
                    birdInfosByBirdType[birdType.BirdTypeId] = new Dictionary<int, BirdInfo>();

                    foreach (var modelQuestion in birdType.AllModelQuestions)
                    {
                        modelQuestionsByBirdType[birdType.BirdTypeId][modelQuestion.ModelIndex] = modelQuestion;

                        if (modelQuestion.BirdInfo != null)
                        {
                            modelQuestion.BirdInfo.ModelIndex = modelQuestion.ModelIndex;
                            birdInfosByBirdType[birdType.BirdTypeId][modelQuestion.ModelIndex] = modelQuestion.BirdInfo;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Invalid JSON data!");
        }
    }

    public List<ModelQuestion> GetModelQuestions(int birdTypeId)
    {
        if (modelQuestionsByBirdType.TryGetValue(birdTypeId, out var questionsByIndex))
        {
            return new List<ModelQuestion>(questionsByIndex.Values);
        }
        Debug.LogWarning($"No ModelQuestions found for BirdTypeId {birdTypeId}.");
        return new List<ModelQuestion>(); 
    }

    public BirdInfo GetBirdInfo(int birdTypeId, int modelIndex)
    {
        if (birdInfosByBirdType.TryGetValue(birdTypeId, out var birdInfoByIndex) && birdInfoByIndex.TryGetValue(modelIndex, out var birdInfo))
        {
            return birdInfo;
        }
        Debug.LogWarning($"BirdInfo not found for BirdTypeId {birdTypeId} and ModelIndex {modelIndex}.");
        return null;
    }

    public List<BirdInfo> GetBirdInfos(int birdTypeId)
    {
        if (birdInfosByBirdType.TryGetValue(birdTypeId, out var birdInfoByIndex))
        {
            return new List<BirdInfo>(birdInfoByIndex.Values);
        }
        return new List<BirdInfo>();
    }

    public void PrintAllBirdInfoAsJson()
    {
        foreach (var entry in birdInfosByBirdType)
        {
            int birdTypeId = entry.Key;
            Dictionary<int, BirdInfo> birdInfoDict = entry.Value;

            string birdInfoJson = JsonConvert.SerializeObject(birdInfoDict, Formatting.Indented);

            Debug.Log($"BirdTypeId: {birdTypeId} - BirdInfo JSON: {birdInfoJson}");
        }
    }
}
