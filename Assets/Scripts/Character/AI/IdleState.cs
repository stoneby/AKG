using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Transform Left;
    public Transform Right;

    public float Speed;

    private MonsterControll monsterControll;

    void FixedUpdate()
    {
        Tuning();

        Moving();
    }

    private void Tuning()
    {
        if (monsterControll.FacingRight && Right.position.x < transform.position.x)
        {
            monsterControll.Flip();
        }

        if (!monsterControll.FacingRight && Left.position.x > transform.position.x)
        {
            monsterControll.Flip();
        }
    }

    private void Moving()
    {
        rigidbody2D.velocity = new Vector2(Speed * (monsterControll.FacingRight ? 1 : -1), 0);
    }

    void Awake()
    {
        monsterControll = GetComponent<MonsterControll>();
    }
}
