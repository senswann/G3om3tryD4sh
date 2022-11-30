using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public bool isCamUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCamUp)
            {
                CameraS.instance.CamUp();
            }else
            {
                CameraS.instance.CamDown();
            }
        }
    }
}
