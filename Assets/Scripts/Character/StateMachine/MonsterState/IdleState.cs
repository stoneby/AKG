using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Transform Left;
    public Transform Right;

    public float Speed;

	private CharacterCommon characterCommon;
    private Rigidbody2D rigid2D;

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
        rigid2D.velocity = new Vector2(Speed * (characterCommon.FacingRight ? 1 : -1), 0);
    }

    void Awake()
    {
        var root = transform.parent.parent;

        characterCommon = root.GetComponent<CharacterCommon>();
        rigid2D = root.GetComponent<Rigidbody2D>();
    }
}
