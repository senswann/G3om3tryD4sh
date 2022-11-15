using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer graphics;
    Animator animator;
    Transform transform;

    public float speed = 5f;
    private float verticalMovement;
    bool onJump = false;
    private int current_timer = 0;
    int timer = 15;

    //check sol
    public bool isGrounding = true;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    //rotation
    Vector3 rotate;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        graphics = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (isGrounding)
        {
            onJump = true;
        }
    }

    private void Update()
    {
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (isGrounding)
        {
            rotate = transform.rotation.eulerAngles;
            rotate.z = Mathf.Round(rotate.z / 90) * 90;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else
        {
            transform.Rotate(Vector3.forward * 425 * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (onJump)
        {
            verticalMovement = 3.5f;
            current_timer++;
            if (current_timer>10)
            {   
                Debug.Log(verticalMovement);
                verticalMovement -= 0.7f;
            }
            if (current_timer >= timer)
            {
                current_timer = 0;
                verticalMovement = 0;
                onJump = false;
            }
        }

        rb.velocity = new Vector2(0, verticalMovement * speed);
    }
}
