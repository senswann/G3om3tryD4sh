using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Transform transforms;

    public float speed = 5f;
    private float verticalMovement;
    bool onJump = false;
    bool onAnimJump = false;
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
        transforms = GetComponent<Transform>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            onJump = false;
        }
        if (!context.canceled)
        {
            onJump = true;
        }
    }

    private void Update()
    {
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (isGrounding)
        {
            rotate = transforms.rotation.eulerAngles;
            rotate.z = Mathf.Round(rotate.z / 90) * 90;
            transforms.rotation = Quaternion.Euler(rotate);
        }
        else
        {
            transforms.Rotate(Vector3.forward * 425 * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (isGrounding && onJump)
        {
             onAnimJump = true;
        }


        if (onAnimJump)
        {
            if(current_timer==0)
                verticalMovement = 3.5f;

            current_timer++;

            if (current_timer>10)
                verticalMovement -= 0.7f;

            if (current_timer >= timer)
            {
                current_timer = 0;
                verticalMovement = 0;
                onAnimJump = false;
            }
        }

        rb.velocity = new Vector2(0, verticalMovement * speed);
    }
}
