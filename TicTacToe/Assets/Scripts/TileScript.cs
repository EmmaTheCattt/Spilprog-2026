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

    // Update is called once per frame
    void FixedUpdate()
    {
        //update tile status based on meeple placement
    }
}
