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
    [SerializeField]
    TMPro.TMP_Text nameTextHost;
    [SerializeField]
    TMPro.TMP_Text ratingTextHost;
    [SerializeField]
    TMPro.TMP_Text nameTextClient;
    [SerializeField]
    TMPro.TMP_Text ratingTextClient;
    private void Start()
    {
        if (ND != null) Destroy(this);
        else
        {
            ND = this;
            DontDestroyOnLoad(this);

        }
        NetworkData.ND.playerName1.OnValueChanged += (FixedString32Bytes oldValue, FixedString32Bytes newValue) =>
        {
            nameTextHost.text = newValue.ToString();
            nameTextClient.text = NetworkData.ND.playerName2.Value.ToString();
        };
        NetworkData.ND.rating1.OnValueChanged += (float oldValue, float newValue) =>
        {
            ratingTextHost.text = newValue.ToString();
            ratingTextClient.text = NetworkData.ND.rating2.Value.ToString();
        };
        NetworkData.ND.playerName2.OnValueChanged += (FixedString32Bytes oldValue, FixedString32Bytes newValue) =>
        {
            nameTextClient.text = newValue.ToString();
            nameTextHost.text = NetworkData.ND.playerName1.Value.ToString();
        };
        NetworkData.ND.rating2.OnValueChanged += (float oldValue, float newValue) =>
        {
            ratingTextClient.text = newValue.ToString();
            ratingTextHost.text = NetworkData.ND.rating1.Value.ToString();
        };

    }
}
