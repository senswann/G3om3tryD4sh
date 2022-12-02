using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //variable du sound effect a jouer
    public AudioClip sound;

    //si le joueur rentre dans le collider de l'objet on start la coroutine de mort
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Sheep"))
            StartCoroutine(GameManager.instance.ReplaceWorld(sound));
    }
}
