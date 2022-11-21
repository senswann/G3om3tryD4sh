using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //variable du déplacement de la platforme
    public static float speed = 11.0f;
    Rigidbody2D rb;

    void Start()
    {
        //récupération du RigidBody
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //ajout du déplacement a l'objet
        rb.velocity = new Vector2(-speed, 0);
    }
}
