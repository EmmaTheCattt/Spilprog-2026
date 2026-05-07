using System;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    //Eksempel på et spilbræt, hvor 'X' har vundet. Skal erstattes med det faktiske spilbræt i din implementation
    char[,] board = new char[3, 3]
    {
            { 'X', 'X', 'X' },
            { 'O', 'O', ' ' },
            { ' ', ' ', 'O' }
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckWin(board);
    }
    
    //Metoder til at tjekke for vinder
    void CheckWin(char[,] board)
    {
        RowCheck(board, 'X');
        ColumnCheck(board, 'X');
        DiagonalCheck(board, 'X');
        RowCheck(board, 'O');
        ColumnCheck(board, 'O');
        DiagonalCheck(board, 'O');
    }
    //Metode til at udskrive hvem der har vundet
    void OnWin(char player)
    {
        //Læs ratingen for begge spillere fra en fil eller database
        float ratingX = 1500; //Eksempelværdi, skal erstattes med faktisk værdi
        float ratingO = 1500; //Eksempelværdi, skal erstattes med faktisk værdi
        Debug.Log(player + " wins!");
        Debug.Log("New ratings:");
        Vector2 newRatings = UpdateRatings(ratingX, ratingO, player);
        Debug.Log("X: " + newRatings.x+ "(" + (newRatings.x - ratingX) + ")");
        Debug.Log("O: " + newRatings.y+ "(" + (newRatings.y - ratingO) + ")");
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
