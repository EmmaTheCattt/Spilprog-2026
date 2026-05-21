using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_handler : MonoBehaviour
{
    public GameObject UI_title;
    public GameObject GAME;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        UI_title.SetActive(false);
        GAME.SetActive(true);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        UI_title.SetActive(false);
        GAME.SetActive(true);
    }

    public void OnServerClick()
    {
        NetworkManager.Singleton.StartServer();
    }
}
