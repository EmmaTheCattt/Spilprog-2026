using UnityEngine;
using Unity.Netcode;
using Unity.Mathematics;

public class DragSystem : NetworkBehaviour
{
    private float dx;
    private float dy;
    private void Update()
    {
        if (dx != 0 || dy != 0)
        {
                MoveServerRpc(dx, dy);
        }
            
    }
    [Rpc(SendTo.Server)]
    private void MoveServerRpc(float dx, float dy)
    {
        transform.position += new Vector3(dx, dy, 0);
    }
    private void OnMouseDrag()
    {
        DO_STUFFRpc();

        /*

       Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
       //transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
       transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
       Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
       dx = worldMousePosition.x - transform.position.x;
       dy = worldMousePosition.y - transform.position.y;
       transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);

       SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sortingOrder = 2; // Sætter sorting order til 1 for at sikre, at det trækker over andre objekter
       BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
       boxCollider.enabled = false; // Deaktiverer BoxCollider2D for at undgå kollisioner under træk
        //Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(vector3.x, vector3.y, 0);

        */
    }
    private void OnMouseUp()
    {
        MAKE_STUFFRpc();

        /*

       transform.localScale = new Vector3(1f, 1f, 1f);
       SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sortingOrder = 1; // Sætter sorting order tilbage til 0, når musen slippes
       BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
       boxCollider.enabled = true; // Aktiverer BoxCollider2D igen, når musen slippes

        */
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    public void DO_STUFFRpc()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 2; // Sætter sorting order til 1 for at sikre, at det trækker over andre objekter
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false; // Deaktiverer BoxCollider2D for at undgå kollisioner under træk
        //Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(vector3.x, vector3.y, 0);
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    public void MAKE_STUFFRpc()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1; // Sætter sorting order tilbage til 0, når musen slippes
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true; // Aktiverer BoxCollider2D igen, når musen slippes
    }
}


