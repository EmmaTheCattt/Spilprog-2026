using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkData : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> playerName1 = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<float> rating1 = new NetworkVariable<float>();
    public NetworkVariable<FixedString32Bytes> playerName2 = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<float> rating2 = new NetworkVariable<float>();

    public static NetworkData ND;
    private void Start()
    {
        if (ND != null) Destroy(this);
        else
        {
            ND = this;
            DontDestroyOnLoad(this);

        }
        
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
