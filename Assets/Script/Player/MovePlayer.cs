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
    private float verticalMovement;
    bool onJump = false;
    bool onAnimJump = false;
    private int current_timer = 0;
    int timer = 15;

    //variable utile pour check le sol
    public bool isGrounding = true;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    //variable pour l'animation de rotation
    Vector3 rotate;

    void Start()
    {
        //récupération du rigidBody du player
        rb = GetComponent<Rigidbody2D>();
        //récupération de l'Animator du player
        animator = GetComponent<Animator>();
        //récupération du Transform du player
        transforms = GetComponent<Transform>();
    }

    //permet de gérer le jump avec un input System
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
        //création d'un collider qui nous renverra si le player est au sol
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        //si le player est au sol
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
            transforms.Rotate(Vector3.forward * 425 * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        //si le player est au sol et qu'il veut jump on active l'anim de jump
        if (isGrounding && onJump)
        {
             onAnimJump = true;
        }

        //si l'anim de jump est activé on effectue le jump
        if (onAnimJump)
        {
            if(current_timer==0)
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
        rb.velocity = new Vector2(0, verticalMovement * speed);
    }
}
