using UnityEngine;

public class LightFollow : MonoBehaviour
{
    //variable permettant le follow d'une target par la lumière
    public GameObject target;
    public float timeOffset;
    public Vector3 posOffset;
    private Vector3 velocity;

    // on set la position de la light a celle du joueur
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + posOffset, ref velocity, timeOffset);
    }
}
