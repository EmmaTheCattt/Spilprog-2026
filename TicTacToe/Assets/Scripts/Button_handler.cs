using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_handler : MonoBehaviour
{
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
        SceneManager.LoadScene("GAME_SCENE");
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene("GAME_SCENE");
    }
}
