using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class CardsProgress : MonoBehaviour
{
    private const string URL_COLLECTED_CARDS = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/collected_cards";
    private const string PROGRESS_ID_KEY = "CurrentProgressID";

    public async Task CreateCard(string jsonCards)
    {
        string userId = DataController.Instance.GetUserId();
        string jsonBody = $"{{ \"cards\": {jsonCards}, \"user_id\": \"{userId}\" }}";

        string response = await Request.SendRequest(URL_COLLECTED_CARDS, "POST", jsonBody, true, true);

        if (response != null)
        {
            Debug.Log("Card created successfully. Response: " + response);
        }
        else
        {
            Debug.LogError("Failed to create card");
        }
    }

    public async Task<List<Card>> GetCards()
    {
        var userId = DataController.Instance.GetUserId();

        string url = $"{URL_COLLECTED_CARDS}?user_id=eq.{userId}&select=*";
        Debug.Log(url);

        string response = await Request.SendRequest(url, "GET", null);

        if (response != null)
        {
            Debug.Log("Cards retrieved successfully. Response: " + response);
            return JsonConvert.DeserializeObject<List<Card>>(response);
        }
        else
        {
            Debug.LogError("Failed to retrieve cards");
            return null;
        }
    }

    public async Task UpdateCards(List<Card> updatedCards)
    {
        string userId = DataController.Instance.GetUserId();
        string jsonBody = JsonConvert.SerializeObject(updatedCards);

        string url = $"{URL_COLLECTED_CARDS}?user_id=eq.{userId}";
        string response = await Request.SendRequest(url, "PATCH", jsonBody, true, true);

        if (response != null)
        {
            Debug.Log("Cards updated successfully. Response: " + response);
        }
        else
        {
            Debug.LogError("Failed to update cards");
        }
    }
}
