using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public TMP_InputField name;
    public TMP_InputField code;
    public int baseRating = 1000;

    public SQL db;
    public GameObject sqldb;

    public PlayerData playerData;
    
    public void SignUp()
    {
        string username = name.text;
        string password = code.text;
        db.CreatePlayer(username, password, baseRating);

    }

    public void PlayerLogIn()
    {
        string username = name.text;
        string password = code.text;
        playerData.SetName(username);
        playerData.SetRating(db.GetRating(username, password));
        playerData.Login();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        sqldb = GameObject.Find("DatabaseTest");
        db = sqldb.GetComponent<SQL>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
