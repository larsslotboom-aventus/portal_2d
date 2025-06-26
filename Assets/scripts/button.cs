using UnityEngine;

public class button : MonoBehaviour
{
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool Pressed = isPressed();
        if (Pressed == true)
        {
            Debug.Log("button is pressed");
        }
    }
    public bool isPressed()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
        
    }
}
