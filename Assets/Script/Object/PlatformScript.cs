using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //variable du d�placement de la platforme
    static public float speed = 5.0f;
    Rigidbody2D rb;

    void Start()
    {
        //r�cup�ration du RigidBody
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //ajout du d�placement a l'objet
        rb.velocity = new Vector2(-speed, 0);
    }
}
