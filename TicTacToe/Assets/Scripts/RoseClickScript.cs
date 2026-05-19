using UnityEngine;

public class RoseClickScript : MonoBehaviour
{
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        Debug.Log("CLICK");
        Destroy(this.gameObject);
    }

    public void OnMouseUp()
    {
        Debug.Log("I DO EXIT");
    }

    public void OnMouseDrag()
    {
        Debug.Log("WORKS?!?");
        transform.localScale = new Vector3(2, 2, 2);
    }
}
