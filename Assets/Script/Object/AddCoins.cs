using System.Collections;
using UnityEngine;

public class AddCoins : MonoBehaviour
{
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(addCoins(collision));
        }
    }
    private IEnumerator addCoins(Collider2D collision)
    {
        AudioManager.instance.PlayClipAt(sound , collision.transform.position);
        GetComponent<SpriteRenderer>().enabled = false;
        GameManager.instance.AddCoins();
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().enabled = true;
    }

}
