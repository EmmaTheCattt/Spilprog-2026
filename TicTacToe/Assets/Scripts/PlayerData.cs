using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class PlayerData : NetworkBehaviour
{
   

    [SerializeField]
    Canvas loginCanvasHost;
    [SerializeField]
    Canvas loginCanvasClient;
    [SerializeField]
    Canvas HostInfo;
    [SerializeField]
    Canvas ClientInfo;
    [SerializeField]
    TMPro.TMP_Text nameTextHost;
    [SerializeField]
    TMPro.TMP_Text ratingTextHost;
    [SerializeField]
    TMPro.TMP_Text nameTextClient;
    [SerializeField]
    TMPro.TMP_Text ratingTextClient;
    public override void OnNetworkSpawn()
    {
        /*
        NetworkData.ND.playerName1.OnValueChanged += (FixedString32Bytes oldValue, FixedString32Bytes newValue) =>
        {
            nameTextHost.text = newValue.ToString();
        };
        NetworkData.ND.rating1.OnValueChanged += (float oldValue, float newValue) =>
        {
            ratingTextHost.text = newValue.ToString();
        };
        NetworkData.ND.playerName2.OnValueChanged += (FixedString32Bytes oldValue, FixedString32Bytes newValue) =>
        {
            nameTextClient.text = newValue.ToString();
        };
        NetworkData.ND.rating2.OnValueChanged += (float oldValue, float newValue) =>
        {
            ratingTextClient.text = newValue.ToString();
        };
        */

        if (IsOwner)
        {
            Debug.Log("Owner Client ID:" + OwnerClientId);
            Debug.Log("IsHost:" + IsHost);
            Debug.Log("IsOwner:" + IsOwner);
            Debug.Log("IsClient:" + IsClient);

            if (IsHost)
            {
                Debug.Log("I hate my life");
                loginCanvasHost.gameObject.SetActive(true);
                loginCanvasClient.gameObject.SetActive(false);
                HostInfo.gameObject.SetActive(true);
                ClientInfo.gameObject.SetActive(false);
            }
            else if (IsClient)
            {
                Debug.Log("I love my life");
                loginCanvasHost.gameObject.SetActive(false);
                loginCanvasClient.gameObject.SetActive(true);
                HostInfo.gameObject.SetActive(false);
                ClientInfo.gameObject.SetActive(true);
            }
        } else
        {
            loginCanvasHost.gameObject.SetActive(false);
            loginCanvasClient.gameObject.SetActive(false);
            HostInfo.gameObject.SetActive(false);
            ClientInfo.gameObject.SetActive(false);
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
            NetworkData.ND.playerName1.Value = name;
            //nameTextHost.text = name;
        }
        else
        {
            SetClientNameRPC(name);
            //playerName2.Value = name;
            //nameTextClient.text = name;
        }
    }
    [Rpc(SendTo.Server)]
    public void SetClientNameRPC(string name)
    {
        NetworkData.ND.playerName2.Value = name;
    }
    [Rpc(SendTo.Server)]
    public void SetClientRatingRPC(float newRating)
    {
        NetworkData.ND.rating2.Value = newRating;
    }
    public void SetRating(float newRating)
    {
        if (IsHost)
        {
            NetworkData.ND.rating1.Value = newRating;
            //ratingTextHost.text = newRating.ToString();
        }
        else
        { 
            SetClientRatingRPC(newRating);
            //rating2.Value = newRating;
            //ratingTextClient.text = newRating.ToString();
        }
    }

    


    public string GetName()
    {
        if (IsHost)
        {
            return NetworkData.ND.playerName1.Value.ToString();
        }
        else
        {
            return NetworkData.ND.playerName2.Value.ToString();
        }
    }
    public float GetRating()
    {
        if (IsHost)
        {
            return NetworkData.ND.rating1.Value;
        }
        else
        {
            return NetworkData.ND.rating2.Value;
        }
    }

}
