using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Question
{
    [JsonProperty("questionText")]
    public string QuestionText { get; set; }

    [JsonProperty("answers")]
    public List<string> Answers { get; set; }

    [JsonProperty("correctAnswerIndex")]
    public int CorrectAnswerIndex { get; set; }
}

[Serializable]
public class ModelQuestion
{
    [JsonProperty("modelIndex")]
    public int ModelIndex { get; set; }

    [JsonProperty("questions")]
    public List<Question> Questions { get; set; }

    [JsonProperty("birdInfo")]
    public BirdInfo BirdInfo { get; set; }
}

[Serializable]
public class BirdInfo
{
    [JsonProperty("modelIndex")]
    public int ModelIndex { get; set; }

    [JsonProperty("birdName")]
    public string BirdName { get; set; }

    [JsonProperty("species")]
    public string Species { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("mainImage")]
    public string MainImage { get; set; }

    [JsonProperty("habitat")]
    public string Habitat { get; set; }

    [JsonProperty("diet")]
    public string Diet { get; set; }

    [JsonProperty("reproduction")]
    public string Reproduction { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("funFact1")]
    public string FunFact1 { get; set; }

    [JsonProperty("funFact2")]
    public string FunFact2 { get; set; }

    [JsonProperty("secondaryImage")]
    public string SecondaryImage { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; }

    [JsonProperty("bibliography")]
    public string Bibliography { get; set; }
}

[Serializable]
public class BirdType
{
    [JsonProperty("birdTypeId")]
    public int BirdTypeId { get; set; }

    [JsonProperty("allModelQuestions")]
    public List<ModelQuestion> AllModelQuestions { get; set; }
}

[Serializable]
public class Root
{
    [JsonProperty("birdTypes")]
    public List<BirdType> BirdTypes { get; set; }
}