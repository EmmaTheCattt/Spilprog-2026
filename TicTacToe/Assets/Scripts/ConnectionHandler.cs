
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ConnectionHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Button _buttonStartHost;
        [SerializeField]
        private Button _buttonStartClient;

        private void Start()
        {
            _buttonStartHost.onClick.AddListener(OnButtonStartHost);
            _buttonStartClient.onClick.AddListener(OnButtonStartClient);
        }

        private void OnDestroy()
        {
            _buttonStartHost.onClick.RemoveAllListeners();
            _buttonStartClient.onClick.RemoveAllListeners();
        }

        public void OnButtonStartHost()
        {
            Debug.Log("OnButtonStartHost");
            NetworkManager.Singleton.StartHost();
            SceneManager.LoadScene("GAME_SCENE");
        }

        public void OnButtonStartClient()
        {
            Debug.Log("OnButtonStartClient");
            NetworkManager.Singleton.StartClient();
            SceneManager.LoadScene("GAME_SCENE");
        }
    }

