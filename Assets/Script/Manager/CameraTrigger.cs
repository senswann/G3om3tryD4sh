using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    //boolean permettant de savoir si l'on veux monté ou descendre la camera lors du trigger
    public bool isCamUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCamUp)
                CameraS.instance.CamUp();
            else
                CameraS.instance.CamDown();
        }
    }
}
