using UnityEngine;
using Unity.Netcode;

public class DragSystem : NetworkBehaviour
{

    private void OnMouseDrag()
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
    private void OnMouseUp()
    {
       transform.localScale = new Vector3(1f, 1f, 1f);
       SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sortingOrder = 1; // Sætter sorting order tilbage til 0, når musen slippes
       BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
       boxCollider.enabled = true; // Aktiverer BoxCollider2D igen, når musen slippes
    }
}


