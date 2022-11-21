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

    //variable stockant l'UI
    GameObject ui;
    bool isPause = false;

    //varible sheep
    public GameObject sheep;
    bool isSheep = false;
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
        ui = GameObject.FindGameObjectWithTag("UI");
        ui.SetActive(isPause);
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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPause = !isPause;
            ui.SetActive(isPause);
            AudioManager.instance.PauseMusic();
            GameManager.instance.PauseGame(isPause);
        }
    }

    public void OnSheep()
    {
        isSheep = !isSheep;
        Vector3 playerPos = gameObject.transform.position;
        if (isSheep)
        {
            sheep.SetActive(true);
            transform.rotation = Quaternion.identity;
            groundCheck.position = new Vector3(playerPos.x, playerPos.y-0.5f, playerPos.z);
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
            transform.position += new Vector3(0,1f,0f);
            rb.gravityScale = 35;
        }
        else
        {
            sheep.SetActive(false);
            groundCheck.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
            transform.localScale = new Vector3(1f,1f, 1);
            rb.gravityScale = 55;
        }
    }

    private void Update()
    {
        if (isSheep && onJump)
        {
            if (current_timer == 0)
            {
                rb.gravityScale = 35;
                verticalMovement = 1.5f;
            }

            current_timer++;
            if(current_timer <= 20)
                transforms.Rotate(Vector3.forward * 100 * Time.fixedDeltaTime);

            // on réduit la vitesse du jump en fonction de la durée de l'animation
            if (current_timer > 20 && verticalMovement < 2.5f)
            {
                verticalMovement += 0.05f;
            }
        }

        if(isSheep && !onJump && isFall)
        {
            if(current_timer <= 20)
                transforms.Rotate(Vector3.back * 100 * Time.fixedDeltaTime);

            // on réduit la vitesse du jump en fonction de la durée de l'animation
            if (current_timer > 20 && verticalMovement >= 0f)
            {
                rb.gravityScale = 55;
                verticalMovement -= 0.1f;
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
    }

    private void FixedUpdate()
    {
        //création d'un collider qui nous renverra si le player est au sol
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

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
                transforms.Rotate(Vector3.back * 100 * Time.fixedDeltaTime);
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
        rb.velocity = new Vector2(0, verticalMovement * speed);
    }
}