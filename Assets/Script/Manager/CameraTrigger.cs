using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    //boolean permettant de savoir si l'on veux monté ou descendre la camera lors du trigger
    public bool isCamUp;
    public bool isSecret;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCamUp)
            {
                CameraS.instance.CamUp();
                if (isSecret)
                {
                    CameraS.instance.CamUp();
                    CameraS.instance.CamUp();
                    CameraS.instance.CamUp();
                }
            }
            else
            {
                CameraS.instance.CamDown();
                if (isSecret)
                {
                    CameraS.instance.CamDown();
                    CameraS.instance.CamDown();
                    CameraS.instance.CamDown();
                }
            }
        }
    }
}
