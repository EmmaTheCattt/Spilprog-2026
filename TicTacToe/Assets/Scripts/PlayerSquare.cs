using System.Globalization;
using UnityEngine;
using Unity.Netcode;

public class PlayerSquare : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Debug.Log($"I am client ID: {OwnerClientId}");
        }
    }
}
