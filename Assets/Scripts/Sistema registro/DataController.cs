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
    private string _currentLanguage = "es";

    private const string API_URL = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1";
    private const string URL_LEVEL_PROGRESS = API_URL + "/levels_progress";
    private const string URL_USER_SETTINGS = API_URL + "/user_settings";
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

    public string GetCurrentLanguage()
    {
        return _currentLanguage;
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

    public void SaveCurrentLanguage(string language)
    {
        _currentLanguage = language;
        StartCoroutine(SaveSettingCoroutine());
    }

    public void SaveCurrentLevel(int currenLevel, bool isCompleted)
    {
        _currentLevel = currenLevel;
        if (_currentLevel < _currentProgressInDatabase)
        {
            return;
        }

        StartCoroutine(SaveProgressCoroutine(currenLevel, isCompleted));
    }

    private IEnumerator SaveProgressCoroutine(int currentLevel, bool completed)
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
        string checkUrl = $"{URL_LEVEL_PROGRESS}?user_id=eq.{userId}&select=*";
        string checkResponse = await Request.SendRequest(checkUrl, "GET");

        if (checkResponse != null)
        {
            var responseList = JsonConvert.DeserializeObject<List<ProgressResponse>>(checkResponse);
            if (responseList != null && responseList.Count > 0)
            {
                // El usuario ya tiene un progreso guardado, actualizar el progreso existente
                string updateUrl = $"{URL_LEVEL_PROGRESS}?user_id=eq.{userId}";
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
                string createResponse = await Request.SendRequest(URL_LEVEL_PROGRESS, "POST", jsonBody, true, true);

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
        string url = $"{URL_LEVEL_PROGRESS}?user_id=eq.{GetUserId()}&select=*";
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

    private IEnumerator SaveSettingCoroutine()
    {
        Task task = RetrieveOrCreateUserSettings(true);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            Debug.LogError($"Error managing settings: {task.Exception.Message}");
        }
    }

    public async Task RetrieveOrCreateUserSettings(bool isUpdate)
    {
        string userId = GetUserId();
        string getUrl = $"{URL_USER_SETTINGS}?user_id=eq.{userId}&select=*";
        string response = await Request.SendRequest(getUrl, "GET", null);

        if (string.IsNullOrEmpty(response) || response == "[]")
        {
            await CreateUserSettings();
        }
        else
        {
            if (isUpdate)
            {
                await UpdateUserSettings(response);
            }
            else
            {
                var settings = JsonConvert.DeserializeObject<UserSettings[]>(response);
                if (settings != null && settings.Length > 0)
                {
                    string language = settings[0].currentLanguage;
                    if (PlayerPrefs.HasKey("LanguageID"))
                    {
                        if (PlayerPrefs.GetString("LanguageID") != language)
                        {
                            _currentLanguage = LanguageManager.instance.currentLanguage;
                            await UpdateUserSettings(response);
                        }
                    }
                }
            }
        }
    }

    private async Task CreateUserSettings()
    {
        string userId = GetUserId();
        string jsonBody = $"{{\"currentLanguage\": \"{GetCurrentLanguage()}\",\"user_id\":\"{userId}\"}}";
        string response = await Request.SendRequest(URL_USER_SETTINGS, "POST", jsonBody);

        if (response != null)
        {
            Debug.Log("User settings created successfully.");
        }
        else
        {
            Debug.LogError("Failed to create user settings.");
        }
    }

    private async Task UpdateUserSettings(string getResponse)
    {
        UserSettings[] settings = JsonHelper.FromJson<UserSettings>(getResponse);
        if (settings != null && settings.Length > 0)
        {
            string userId = GetUserId();
            string updateUrl = $"{URL_USER_SETTINGS}?user_id=eq.{userId}";
            string jsonBody = $"{{\"currentLanguage\":\"{GetCurrentLanguage()}\",\"user_id\":\"{userId}\"}}";

            string response = await Request.SendRequest(updateUrl, "PATCH", jsonBody);

            if (response != null)
            {
                Debug.Log("User settings updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update user settings.");
            }
        }
        else
        {
            Debug.LogError("No settings found to update.");
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

    [Serializable]
    private class UserSettings
    {
        public int id;
        public string currentLanguage;
        public string user_id;
    }
}