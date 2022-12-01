using UnityEngine;

public class SwitchGameplay : MonoBehaviour
{
    //si le joueur s'approche du portail on change son mode de jeu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<MovePlayer>().OnSheep();
    }
}
