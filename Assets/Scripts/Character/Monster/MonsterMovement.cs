using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour
{
    public Transform LeftRange;
    public Transform RightRange;
    public float Speed;

    private Transform player;
    private Animator animator;
    private bool facingRight = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Tuning();

        Moving();
    }

    private void Tuning()
    {
        if (facingRight && RightRange.position.x < transform.position.x)
        {
            Flip();
        }

        if (!facingRight && LeftRange.position.x > transform.position.x)
        {
            Flip();
        }
    }

    private void Moving()
    {
        rigidbody2D.velocity = new Vector2(Speed * (facingRight ? 1 : -1), 0);        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
