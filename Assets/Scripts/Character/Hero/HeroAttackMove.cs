using UnityEngine;
using System.Collections;

public class HeroAttackMove : MonoBehaviour
{
	[Range(0, 10f)]
	public float SpeedRatio;
	
	private PlayerControl player;
	private CharacterCommon characterCommon;

	private bool init;
	private bool stopped;
	
	public void MoveUpdate()
	{
		if (!init)
		{
			return;
		}

		if (stopped)
		{
			player.rigidbody2D.velocity = Vector2.zero;
		}
		else
		{
			player.rigidbody2D.velocity = new Vector2(SpeedRatio * player.horizontalSpeed * (characterCommon.FacingRight ? 1 : -1), player.rigidbody2D.velocity.y);
		}
	}
	
	public void MoveStart()
	{
		init = true;
		stopped = false;
	}
	
	public void MoveStop()
	{
		init = false;
		stopped = true;
	}
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		characterCommon = player.GetComponent<CharacterCommon>();
	}
}
