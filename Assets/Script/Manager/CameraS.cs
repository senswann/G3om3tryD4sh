using UnityEngine;

public class CameraS : MonoBehaviour
{
    //variable permettant de modifier l'Offset de la cam
    public float timeOffset;
    public Vector3 posOffset;

    //variable stockant l'offset de départ
    Vector3 startPosOffset;
    private Vector3 velocity;

    //variable d'instance de la CameraS
    public static CameraS instance;

    GameObject player;

    //permet de créer une instance unique de CameraS appelable avec CameraS.instance
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de CameraS");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosOffset = posOffset;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x + posOffset.x, posOffset.y, posOffset.z), ref velocity, timeOffset);
    }

    //function permettant d elever la camera
    public void CamUp()
    {
        posOffset += new Vector3(0,1.0f,0);
    }

    //fonction permettant de descendre la camera
    public void CamDown()
    {
        posOffset -= new Vector3(0,1.0f,0);
    }

    //fonction permettant de remttre l'offset de depart
    public void ResetCam()
    {
        posOffset = startPosOffset;
    }

}