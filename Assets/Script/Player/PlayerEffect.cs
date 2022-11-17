using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private Animator animator;
    int index=0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Death()
    {
        index = Random.Range(1, 12);
        animator.SetInteger("Death", 1);
    }

    public void Restart()
    {
        index = 0;
        animator.SetInteger("Death", index);
    }
}
