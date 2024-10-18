using UnityEngine;

public interface UserData
{
    public string GetUserId();
    public string GetAccessToken();
    public long GetTokenExpiryTime();
    public int GetCurrentLevel();

    public void SaveAccessToken(string accessToken);
    public void SaveUserId(string userId);
    public void SaveTokenExpiryTime(long expiryTime);
    public void SaveCurrentLevel(int currenLevel, bool isCompleted);
}