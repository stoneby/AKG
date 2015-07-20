using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Transform Left;
    public Transform Right;

    public float Speed;

    private MonsterControll monsterControll;
	private CharacterCommon characterCommon;

    void FixedUpdate()
    {
        Tuning();

        Moving();
    }

    private void Tuning()
    {
		if (characterCommon.FacingRight && Right.position.x < transform.position.x)
        {
			characterCommon.Flip();
        }

		if (!characterCommon.FacingRight && Left.position.x > transform.position.x)
        {
			characterCommon.Flip();
        }
    }

    private void Moving()
    {
		rigidbody2D.velocity = new Vector2(Speed * (characterCommon.FacingRight ? 1 : -1), 0);
    }

    void Awake()
    {
        monsterControll = GetComponent<MonsterControll>();
		characterCommon = GetComponent<CharacterCommon>();
    }
}
