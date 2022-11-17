using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLayerSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer first;
    [SerializeField] private SpriteRenderer second;
    [SerializeField] private SpriteRenderer third;

    [SerializeField] private int index;
    [SerializeField] private Sprite[] skinFirst;
    [SerializeField] private Sprite[] skinSecond;
    [SerializeField] private Sprite[] skinThird;

    private Animator animator;
    int indexAnim = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeSprites(index);
        animator = GetComponent<Animator>();
    }

    public void ChangeSprites(int _index)
    {
        first.sprite = skinFirst[_index];
        second.sprite = skinSecond[_index-1<0?0:_index-1];
        third.sprite = skinThird[(_index-11<0?0:_index-11)];
    }

    public void Death()
    {
        second.enabled = false;
        third.enabled = false;
        indexAnim = Random.Range(1, 12);
        animator.SetInteger("Death", 1);
    }

    public void Restart()
    {
        second.enabled = true;
        third.enabled = true;
        indexAnim = 0;
        animator.SetInteger("Death", indexAnim);
    }
}
