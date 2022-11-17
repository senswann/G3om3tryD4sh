using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        PlatformScript.speed = 0.0f;
        Scroller.speed = 0.0f;
        collision.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        collision.GetComponent<MultipleLayerSprite>().Death();
        yield return new WaitForSeconds(1f);
        collision.GetComponent<MultipleLayerSprite>().Restart();
        collision.transform.position = new Vector3(-4.4f, -2.9f,0f);
        collision.GetComponent<Rigidbody2D>().gravityScale = 30.0f;
        PlatformScript.speed = 5.0f;
        Scroller.speed = 0.15f;
    }
}
