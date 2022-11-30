using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //variable du sound effect a jouer
    public AudioClip sound;

    //si le joueur rentre dans le collider de l'objet on start la coroutine de mort
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    //avec cette coroutine on arrete de bouger le Monde, on lance le SFX ainsi que l'animation de mort, puis on replace tout comme au début et on recommence.
    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        PlatformScript.speed = 0.0f;
        Scroller.speed = 0.0f;
        collision.transform.rotation = Quaternion.identity;
        collision.GetComponent<BoxCollider2D>().enabled = false;
        collision.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        collision.GetComponent<MultipleLayerSprite>().Death();
        AudioManager.instance.PlayClipAt(sound, collision.transform.position);
        yield return new WaitForSeconds(1f);
        if(collision.GetComponent<MovePlayer>().isSheep)
            collision.GetComponent<MovePlayer>().OnSheep();
        collision.GetComponent<BoxCollider2D>().enabled = true;
        collision.GetComponent<MultipleLayerSprite>().Restart();
        GameManager.instance.ReplaceWord();
        //collision.transform.position = new Vector3(-4.4f, -2.9f,0f);
        collision.GetComponent<Rigidbody2D>().gravityScale = 64f;
        PlatformScript.speed = 11.0f;
        Scroller.speed = 0.15f;
    }
}
