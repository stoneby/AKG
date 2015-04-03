using UnityEngine;

public class AttackState : MonoBehaviour
{
    private MonsterControll monster;
    private PlayerControl player;

    void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.zero;

        if (monster.FacingRight && player.transform.position.x < monster.transform.position.x)
        {
            monster.Flip();
        }

        if (!monster.FacingRight && player.transform.position.x > monster.transform.position.x)
        {
            monster.Flip();
        }
    }

    void Awake()
    {
        monster = GetComponent<MonsterControll>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
