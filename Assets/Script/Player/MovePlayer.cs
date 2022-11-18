using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    //variable de base du player
    Rigidbody2D rb;
    Animator animator;
    Transform transforms;

    //variable utile pour le d�placement du player
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
        //r�cup�ration du rigidBody du player
        rb = GetComponent<Rigidbody2D>();
        //r�cup�ration de l'Animator du player
        animator = GetComponent<Animator>();
        //r�cup�ration du Transform du player
        transforms = GetComponent<Transform>();
    }

    //permet de g�rer le jump avec un input System
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
        //cr�ation d'un collider qui nous renverra si le player est au sol
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        //si le player est au sol
        if (isGrounding)
        {
            //on repositionne le player comme il faut lors qu'il att�ri avec l'aide d'un quaterion
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

        //si l'anim de jump est activ� on effectue le jump
        if (onAnimJump)
        {
            if(current_timer==0)
                verticalMovement = 3.5f;

            current_timer++;

            // on r�duit la vitesse du jump en fonction de la dur�e de l'animation
            if (current_timer>10)
                verticalMovement -= 0.7f;

            // on d�sactive l'anim de jump ainsi que reset les diff�rentes valeur laissant la gravit� faire pour la d�scante
            if (current_timer >= timer)
            {
                current_timer = 0;
                verticalMovement = 0;
                onAnimJump = false;
            }
        }

        //on g�re le jump
        rb.velocity = new Vector2(0, verticalMovement * speed);
    }
}
