using System;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class WinManager : NetworkBehaviour
{
    public bool gameOver = false;
    public static WinManager WM;
    public SQL DB;
    public GameObject sqldb;
    [SerializeField]
    public GameObject tile00; //Prefab for spilbrættets felter, skal sættes i Unity editoren
    [SerializeField]
    public GameObject tile01;
    [SerializeField]
    public GameObject tile02;
    [SerializeField]
    public GameObject tile10;
    [SerializeField]
    public GameObject tile11;
    [SerializeField]
    public GameObject tile12;
    [SerializeField]
    public GameObject tile20;
    [SerializeField]
    public GameObject tile21;
    [SerializeField]
    public GameObject tile22;

    //Eksempel på et spilbræt, hvor 'X' har vundet. Skal erstattes med det faktiske spilbræt i din implementation
    char[,] board = new char[3, 3];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (WM != null) Destroy(this);
        else
        {
            WM = this;
            DontDestroyOnLoad(this);
        }
        sqldb = GameObject.Find("DatabaseTest");
        DB = sqldb.GetComponent<SQL>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver) GameOver();
        Updateboard();
        CheckWin(board);
    }
    void Updateboard()
    {
        board[0, 0] = tile00.GetComponent<TileScript>().status;
        board[0, 1] = tile01.GetComponent<TileScript>().status;
        board[0, 2] = tile02.GetComponent<TileScript>().status;
        board[1, 0] = tile10.GetComponent<TileScript>().status;
        board[1, 1] = tile11.GetComponent<TileScript>().status;
        board[1, 2] = tile12.GetComponent<TileScript>().status;
        board[2, 0] = tile20.GetComponent<TileScript>().status;
        board[2, 1] = tile21.GetComponent<TileScript>().status;
        board[2, 2] = tile22.GetComponent<TileScript>().status;
    }
    //Metoder til at tjekke for vinder
    
    //Metode til at udskrive hvem der har vundet
    void OnWin(char player)// X eller O
    {
        string playerO = NetworkData.ND.playerName1.Value.ToString();
        string playerX = NetworkData.ND.playerName2.Value.ToString();
        float ratingO = NetworkData.ND.rating1.Value;
        float ratingX = NetworkData.ND.rating2.Value;
        //Læs ratingen for begge spillere fra en fil eller database
        //float ratingX = 1500; //Eksempelværdi, skal erstattes med faktisk værdi
        //float ratingO = 1500; //Eksempelværdi, skal erstattes med faktisk værdi
       
        if (player == 'X')
        {
            Debug.Log(playerX + " wins!");
            NetworkData.ND.rating2.Value = UpdateRatings(ratingX, ratingO, player).x;
            NetworkData.ND.rating1.Value = UpdateRatings(ratingX, ratingO, player).y;
            DB.UpdateRating(playerX,NetworkData.ND.rating2.Value);
            DB.UpdateRating(playerO,NetworkData.ND.rating1.Value);
        }
        else if (player == 'O')
        {
            Debug.Log(playerO + " wins!");
            NetworkData.ND.rating2.Value = UpdateRatings(ratingX, ratingO, player).x;
            NetworkData.ND.rating1.Value = UpdateRatings(ratingX, ratingO, player).y;
            DB.UpdateRating(playerX,NetworkData.ND.rating2.Value);
            DB.UpdateRating(playerO,NetworkData.ND.rating1.Value);
        }
        //Debug.Log("New ratings:");
        //Vector2 newRatings = UpdateRatings(ratingX, ratingO, player);
        //Debug.Log("X: " + newRatings.x+ "(" + (newRatings.x - ratingX) + ")");
        //Debug.Log("O: " + newRatings.y+ "(" + (newRatings.y - ratingO) + ")");
        gameOver = true;
    }
    public void GameOver()
    {
        //Game over somehow
        
    }
    void CheckWin(char[,] board)
    {
        RowCheck(board, 'X');
        ColumnCheck(board, 'X');
        DiagonalCheck(board, 'X');
        RowCheck(board, 'O');
        ColumnCheck(board, 'O');
        DiagonalCheck(board, 'O');
    }
    void RowCheck(char[,] board, char player)
    {
        if (board[0, 0] == player && board[0, 1] == player && board[0, 2] == player)
        {
            OnWin(player);
        }
        else if (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player)
        {
            OnWin(player);
        }
        else if (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player)
        {
            OnWin(player);
        }
    }
    void ColumnCheck(char[,] board, char player)
    {
        if (board[0, 0] == player && board[1, 0] == player && board[2, 0] == player)
        {
            OnWin(player);
        }
        else if (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player)
        {
            OnWin(player);
        }
        else if (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player)
        {
            OnWin(player);
        }
    }
    void DiagonalCheck(char[,] board, char player)
    {
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
        {
            OnWin(player);
        }
        else if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
        {
            OnWin(player);
        }
    }
    //Elo rating system, først beregnes sandsynligheden for at vinde, og derefter opdateres ratingen
    float CalculateProbabilityWinning(float ratingX, float ratingO)
    {
        return 1 / (1 + (float)Math.Pow(10, (ratingO - ratingX) / 400));
    }
    Vector2 UpdateRatings(float ratingX, float ratingO, char winner)
    {
        float probabilityX = CalculateProbabilityWinning(ratingX, ratingO);
        float probabilityO = CalculateProbabilityWinning(ratingO, ratingX);
        if (winner == 'X')
        {
            ratingX += 20 * (1 - probabilityX);
            ratingO += 20 * (0 - probabilityO);
        }
        else if (winner == 'O')
        {
            ratingX += 20 * (0 - probabilityX);
            ratingO += 20 * (1 - probabilityO);
        }
        return new Vector2(ratingX, ratingO);
    }
}
