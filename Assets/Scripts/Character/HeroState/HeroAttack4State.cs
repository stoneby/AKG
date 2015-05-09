using UnityEngine;

public class HeroAttack4State : MonoBehaviour
{
	[Range(0, 10f)]
	public float SpeedRatio;

    private PlayerControl player;
	private CharacterCommon characterCommon;

	private bool stopped;

    void OnEnable()
    {
		stopped = false;
        player.LastAttack = true;
	}

	void OnDisable()
	{
		player.LastAttack = false;
	}

	void FixedUpdate()
	{
		if (stopped)
		{
			player.rigidbody2D.velocity = Vector2.zero;
		}
		else
		{
			player.rigidbody2D.velocity = new Vector2(SpeedRatio * player.horizontalSpeed * (characterCommon.FacingRight ? 1 : -1), player.rigidbody2D.velocity.y);
		}
	}

	public void OnStop()
	{
		stopped = true;
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		characterCommon = player.GetComponent<CharacterCommon>();
    }
}
