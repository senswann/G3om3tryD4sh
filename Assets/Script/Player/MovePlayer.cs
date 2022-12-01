using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    //variable de base du player
    Rigidbody2D rb;
    Animator animator;
    Transform transforms;

    //variable utile pour le déplacement du player
    public float speed = 5f;
    public static float runSpeed = 11f;
    public float verticalMovement;
    bool onJump = false;
    bool onAnimJump = false;
    private int current_timer = 0;
    int timer = 15;

    //variable utile pour check le sol
    public bool isGrounding = true;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    //varible sheep
    public GameObject sheep;
    public bool isSheep = false;
    bool isFall = false;

    //variable pour l'animation de rotation
    public Vector3 rotate;

    void Start()
    {
        //récupération du rigidBody du player
        rb = GetComponent<Rigidbody2D>();
        //récupération de l'Animator du player
        animator = GetComponent<Animator>();
        //récupération du Transform du player
        transforms = GetComponent<Transform>();
        sheep.SetActive(false);
    }

    //permet de gérer le jump avec un input System
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (isSheep)
            {
                current_timer = 0;
                isFall = true;
            }
            onJump = false;
        }
        if (!context.canceled)
        {
            if (isSheep)
            {
                current_timer = 0;
            }
            onJump = true;
        }
    }

    //fonction permettant de gérer le changement de gameplay
    public void OnSheep()
    {
        isSheep = !isSheep;
        Vector3 playerPos = gameObject.transform.position;
        if (isSheep)
        {
            sheep.SetActive(true);
            transform.rotation = Quaternion.identity;
            groundCheck.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
            transform.position += new Vector3(0,1f,0f);
            rb.gravityScale = 35;
            groundCheckRadius=1f;
        }
        else
        {
            sheep.SetActive(false);
            groundCheck.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
            transform.localScale = new Vector3(1f,1f, 1);
            rb.gravityScale = 62;
            groundCheckRadius=0.6f;
        }
    }

    private void FixedUpdate()
    {
        //création d'un collider qui nous renverra si le player est au sol
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (isSheep && onJump)
        {
            if (current_timer == 0)
            {
                rb.gravityScale = 35;
                verticalMovement = 1.5f;
                transform.rotation = Quaternion.identity;
            }

            current_timer++;

            // on réduit la vitesse du jump en fonction de la durée de l'animation
            if (current_timer > 20 && verticalMovement < 2.5f)
            {
                verticalMovement += 0.05f;
                transforms.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
            }
        }

        if(isSheep && !onJump && isFall)
        {
            if(current_timer == 0)
                transform.rotation = Quaternion.identity;

            // on réduit la vitesse du jump en fonction de la durée de l'animation
            if (current_timer > 20 && verticalMovement >= 0f)
            {
                rb.gravityScale = 55;
                verticalMovement -= 0.1f;
                transforms.Rotate(Vector3.back * 200 * Time.fixedDeltaTime);
            }
            current_timer++;
            // on désactive l'anim de jump ainsi que reset les différentes valeur laissant la gravité faire pour la déscante
            if (current_timer >= 50)
            {
                current_timer = 0;
                verticalMovement = 0;
                isFall = false;
            }
        }
        
        if(isSheep && isGrounding)
        {
            transform.rotation = Quaternion.identity;
        }

        //si le player est au sol
        if (!isSheep)
        {
            if (isGrounding)
            {
                //on repositionne le player comme il faut lors qu'il attéri avec l'aide d'un quaterion
                rotate = transforms.rotation.eulerAngles;
                rotate.z = Mathf.Round(rotate.z / 90) * 90;
                transforms.rotation = Quaternion.Euler(rotate);
            }
            else
            {
                // le player fait des backflip dans les aires
                transforms.Rotate(Vector3.back * 400 * Time.fixedDeltaTime);
            }
        }
        //si le player est au sol et qu'il veut jump on active l'anim de jump
        if (isGrounding && onJump && !isSheep)
        {
             onAnimJump = true;
        }


        //si l'anim de jump est activé on effectue le jump
        if (onAnimJump)
        {
            if (current_timer==0)
                verticalMovement = 3.5f;

            current_timer++;

            // on réduit la vitesse du jump en fonction de la durée de l'animation
            if (current_timer>10)
                verticalMovement -= 0.7f;

            // on désactive l'anim de jump ainsi que reset les différentes valeur laissant la gravité faire pour la déscante
            if (current_timer >= timer)
            {
                current_timer = 0;
                verticalMovement = 0;
                onAnimJump = false;
            }
        }

        //on gère le jump
        rb.velocity = new Vector2(runSpeed, verticalMovement * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb.gravityScale != 0)
            rb.gravityScale = 0;            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb.gravityScale != 64)
            rb.gravityScale = 64;
    }
}
