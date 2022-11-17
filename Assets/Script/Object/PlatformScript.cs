using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    static public float speed = 5.0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0);
    }
}
