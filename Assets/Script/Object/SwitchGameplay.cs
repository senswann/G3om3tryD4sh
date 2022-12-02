using UnityEngine;

public class SwitchGameplay : MonoBehaviour
{
    public Vector3 camOffset;
    public float profondeur;

    //si le joueur s'approche du portail on change son mode de jeu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraS.instance.setCamera(camOffset,profondeur);
            collision.GetComponent<MovePlayer>().OnSheep();
        }
    }
}
