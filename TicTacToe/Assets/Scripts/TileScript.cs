using UnityEngine;
using Unity.Netcode;

public class TileScript : NetworkBehaviour
{
    public int x; // X coordinate of the tile
    public int y; // Y coordinate of the tile
    public char status = ' '; // ' ' = empty, 'X' = X, 'O' = O
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = ' '; // Initialize tile status to empty
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider entered");
        if (other.gameObject.CompareTag("X") && status == ' ')
        {
            status = 'X'; // Update tile status to 'X' when an 'X' piece enters the tile
        }
        else if (other.gameObject.CompareTag("O") && status == ' ')
        {
            status = 'O'; // Update tile status to 'O' when an 'O' piece enters the tile
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collider exited");
        if (other.gameObject.CompareTag("X") && status == 'X')
        {
            status = ' '; // Reset tile status to empty when an 'X' piece exits the tile
        }
        else if (other.gameObject.CompareTag("O") && status == 'O')
        {
            status = ' '; // Reset tile status to empty when an 'O' piece exits the tile
        }
    }
    
}
