using UnityEngine;

public class CameraS : MonoBehaviour
{
    public float timeOffset;
    public Vector3 posOffset;

    Vector3 startPosOffset;

    private Vector3 velocity;
    //variable d'instance du AudioManager
    public static CameraS instance;

    //permet de créer une instance unique de AudioManager appelable avec CameraS.instance
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
        startPosOffset = posOffset;
    }

    public void CamUp()
    {
        posOffset += new Vector3(0,1.0f,0);
        transform.position = Vector3.SmoothDamp(transform.position, posOffset, ref velocity, timeOffset);
    }
    public void CamDown()
    {
        posOffset -= new Vector3(0,1.0f,0);
        transform.position = Vector3.SmoothDamp(transform.position, posOffset, ref velocity, timeOffset);
    }

    public void ResetCam()
    {
        posOffset = startPosOffset;
        transform.position = Vector3.SmoothDamp(transform.position, posOffset, ref velocity, timeOffset);
    }

}