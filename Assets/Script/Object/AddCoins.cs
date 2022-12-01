using UnityEngine;

public class AddCoins : MonoBehaviour
{
    //song jouer lorsqu'on ramasse une piece
    public AudioClip sound;
    public GameObject selfObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si le joueur trigger la piece on start la coroutine d'ajout de piece
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sound, collision.transform.position);
            selfObj.SetActive(false);
            GameManager.instance.AddCoins();
        }
    }
}
