using UnityEngine;

public class AttackEffectController : MonoBehaviour
{
    public float LifeTime;
    public float Speed;
    public bool FacingRight;

    private Animator animator;
    private Rigidbody2D rigid;

    public void Play()
    {
        animator.Play("Attack");

        Destroy(gameObject, LifeTime);
    }

    public void Flying()
    {
        rigid.velocity = new Vector2(FacingRight ? Speed : -Speed, 0);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
}
