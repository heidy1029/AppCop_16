using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DataController : MonoBehaviour, UserData
{
    private static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataController>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "DataController";
                    _instance = go.AddComponent<DataController>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private string _accessToken;
    private long _tokenExpiryTime;
    private string _userId;
    private int _currentLevel = 1;
    private int _currentProgressInDatabase = 1;

    private const string API_URL_LEVEL_PROGRESS = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/levels_progress";
    private const string PROGRESS_ID_KEY = "CurrentProgressID";

    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;

    public string GetAccessToken()
    {
        return _accessToken;
    }

    public long GetTokenExpiryTime()
    {
        return _tokenExpiryTime;
    }

    public string GetUserId()
    {
        return _userId;
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void GetCurrentLevelOnStart()
    {
        if (_isInitialized)
        {
            return;
        }
        GetProgress();
    }

    public void SaveAccessToken(string accessToken)
    {
        _accessToken = accessToken;
    }

    public void SaveTokenExpiryTime(long expiryTime)
    {
        _tokenExpiryTime = expiryTime;
    }

    public void SaveUserId(string userId)
    {
        _userId = userId;
    }

    public void SaveCurrentLevel(int currenLevel, bool isCompleted)
    {
        _currentLevel = currenLevel;
        if (_currentLevel < _currentProgressInDatabase)
        {
            return;
        }

        StartCoroutine(SaveProgressCorroutine(currenLevel, isCompleted));
    }

    private IEnumerator SaveProgressCorroutine(int currentLevel, bool completed)
    {
        Task<string> task = SaveProgress(currentLevel, completed);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            Debug.LogError($"Error creating progress: {task.Exception.Message}");
        }
        else
        {
            string progressId = task.Result;
            Debug.Log($"Progress created with ID: {progressId}");
            // Aquí puedes hacer algo con el progressId si es necesario
        }
    }

    private async Task<string> SaveProgress(int currentLevel, bool isCompleted)
    {
        string userId = GetUserId();
        string jsonBody = $"{{ \"user_id\": \"{userId}\", \"currentLevel\": \"{currentLevel}\" }}";
        Debug.LogFormat("JSON enviado: {0}", jsonBody);

        // Verificar si el usuario ya tiene un progreso guardado
        string checkUrl = $"{API_URL_LEVEL_PROGRESS}?user_id=eq.{userId}&select=*";
        string checkResponse = await Request.SendRequest(checkUrl, "GET");

        if (checkResponse != null)
        {
            var responseList = JsonConvert.DeserializeObject<List<ProgressResponse>>(checkResponse);
            if (responseList != null && responseList.Count > 0)
            {
                // El usuario ya tiene un progreso guardado, actualizar el progreso existente
                string updateUrl = $"{API_URL_LEVEL_PROGRESS}?user_id=eq.{userId}";
                Debug.Log("Updating progress with URL: " + updateUrl);
                string updateResponse = await Request.SendRequest(updateUrl, "PATCH", jsonBody, true, true);

                if (updateResponse != null)
                {
                    Debug.Log("Progress updated successfully. Response: " + updateResponse);

                    string progressId = ParseProgressId(updateResponse);
                    if (!string.IsNullOrEmpty(progressId))
                    {
                        // Guardamos el ID en PlayerPrefs
                        PlayerPrefs.SetString(PROGRESS_ID_KEY, progressId);
                        PlayerPrefs.Save();
                        return progressId;
                    }
                }
            }
            else
            {
                // El usuario no tiene un progreso guardado, crear un nuevo registro
                string createResponse = await Request.SendRequest(API_URL_LEVEL_PROGRESS, "POST", jsonBody, true, true);

                if (createResponse != null)
                {
                    Debug.Log("Progress created successfully. Response: " + createResponse);

                    string progressId = ParseProgressId(createResponse);
                    if (!string.IsNullOrEmpty(progressId))
                    {
                        // Guardamos el ID en PlayerPrefs
                        PlayerPrefs.SetString(PROGRESS_ID_KEY, progressId);
                        PlayerPrefs.Save();
                        return progressId;
                    }
                }
            }
        }

        Debug.LogError("Failed to create or update progress");
        return null;
    }

    private string ParseProgressId(string response)
    {
        try
        {
            var responseObj = JsonUtility.FromJson<ProgressResponse>(response);
            return responseObj.currentLevel.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing progress ID: {e.Message}");
            return null;
        }
    }

    public async void GetProgress()
    {
        string url = $"{API_URL_LEVEL_PROGRESS}?user_id=eq.{GetUserId()}&select=*";
        string response = await Request.SendRequest(url, "GET");

        if (response != null)
        {
            Debug.Log("Levels Response: " + response);
            var responseList = JsonConvert.DeserializeObject<List<ProgressResponse>>(response);
            if (responseList != null && responseList.Count > 0)
            {
                var responseObj = responseList[0];
                _currentLevel = responseObj.currentLevel;
                _currentProgressInDatabase = _currentLevel;
                _isInitialized = true;
            }
            else
            {
                //Debug.LogError("No progress data found");
                SaveCurrentLevel(1, false);
                _isInitialized = true;
            }
        }
        else
        {
            Debug.LogError("Failed to fetch progress");
        }
    }

    [Serializable]
    private class ProgressResponse
    {
        public int id;
        public string created_at;
        public string user_id;
        public int currentLevel;
    }
}