using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Transform Left;
    public Transform Right;

    public float Speed;

    private bool facingRight = true;

    void FixedUpdate()
    {
        Tuning();

        Moving();
    }

    private void Tuning()
    {
        if (facingRight && Right.position.x < transform.position.x)
        {
            Flip();
        }

        if (!facingRight && Left.position.x > transform.position.x)
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
