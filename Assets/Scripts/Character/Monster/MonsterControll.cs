using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    private Animator animator;

    public bool FacingRight = true;

    public void Attack(bool flag)
    {
        animator.SetBool("Attack", flag);
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void LookAround(bool flag)
    {
        
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
