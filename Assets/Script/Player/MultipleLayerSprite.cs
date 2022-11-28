using UnityEngine;
using UnityEngine.InputSystem;


public class MultipleLayerSprite : MonoBehaviour
{
    //variable stockant les 3 layer du joueur
    [SerializeField] private SpriteRenderer first;
    [SerializeField] private SpriteRenderer second;
    [SerializeField] private SpriteRenderer third;
    [SerializeField] private SpriteRenderer sheep;

    //variable utile pour le changement de skin
    [SerializeField] public int index;
    [SerializeField] private Sprite[] skinFirst;
    [SerializeField] private Sprite[] skinSecond;
    [SerializeField] private Sprite[] skinThird;

    //variable utilisé pour le restart
    public GameObject lights;
    public PlayerInput player;
    private Color currentColor;

    //variable pour l'animation du player
    private Animator animator;
    int indexAnim = 0;
    int currentIndex;

    void Start()
    {
        // on commence avec un skin par default
        ChangeSprites(index);
        currentIndex = index;
        //on récupère l'animator
        animator = GetComponent<Animator>();
    }

    // permet de changer le skin du player
    public void ChangeSprites(int _index)
    {
        first.sprite = skinFirst[_index];
        second.sprite = skinSecond[_index-1<0?0:_index-1];
        third.sprite = skinThird[(_index-11<0?0:_index-11)];
    }

    private void FixedUpdate()
    {
        if (index != currentIndex)
        {
            Debug.Log("test");
            ChangeSprites(index);
            currentIndex = index;
        }
    }

    //permet de gérer l'animation de mort du player
    public void Death()
    {
        currentColor = first.color;
        first.color = Color.white;
        lights.SetActive(false);
        player.enabled = false;
        second.enabled = false;
        third.enabled = false;
        sheep.enabled = false;
        indexAnim = Random.Range(1, 10);
        animator.SetInteger("Death", indexAnim);
    }

    // permet de gérer l'animation de revive du player
    public void Restart()
    {
        first.color = currentColor;
        lights.SetActive(true);
        player.enabled = true;
        second.enabled = true;
        third.enabled = true;
        sheep.enabled = true;
        indexAnim = 0;
        animator.SetInteger("Death", indexAnim);
    }

    public Sprite getFirstLayer(int _index)
    {
        return skinFirst[_index];
    }
}
