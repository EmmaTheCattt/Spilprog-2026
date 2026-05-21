using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerData : NetworkBehaviour
{
    public string playerName;
    public float rating;
    [SerializeField]
    Canvas loginCanvasHost;
    [SerializeField]
    Canvas loginCanvasClient;
    [SerializeField]
    TMPro.TMP_Text nameTextHost;
    [SerializeField]
    TMPro.TMP_Text ratingTextHost;
    [SerializeField]
    TMPro.TMP_Text nameTextClient;
    [SerializeField]
    TMPro.TMP_Text ratingTextClient;
    void Start()
    {
        if (IsHost)
        {
            loginCanvasHost.gameObject.SetActive(true);
            loginCanvasClient.gameObject.SetActive(false);
        }
        else
        {
            loginCanvasHost.gameObject.SetActive(false);
            loginCanvasClient.gameObject.SetActive(true);
        }
    }

    
    public void Login()
    {

    loginCanvasHost.gameObject.SetActive(false);
    loginCanvasClient.gameObject.SetActive(false);
    }
    public void SetName(string name)
    {
        if (IsHost)
        {
            playerName = name;
            nameTextHost.text = name;
        }
        else
        {
            playerName = name;
            nameTextClient.text = name;
        }
    }
    public void SetRating(float newRating)
    {
        if (IsHost)
        {
            rating = newRating;
            ratingTextHost.text = newRating.ToString();
        }
        else
        { 
            rating = newRating;
            ratingTextClient.text = newRating.ToString();
        }

    }
    public string GetName()
    {
        return playerName;
    }
    public float GetRating()
    {
        return rating;
    }

}
