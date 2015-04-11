using UnityEngine;

public class HeroShortState : MonoBehaviour
{
	public float Speed;
	
	private Animator playerAnimator;
	private PlayerControl player;
	
	private Rigidbody2D playerRigid2D;
	
	void OnEnable()
	{
	}

	void FixedUpdate()
	{
		playerRigid2D.velocity = new Vector2(Speed, 0);
	}
	
	void Awake()
	{
		playerAnimator = GetComponent<Animator>();
		player = GetComponent<PlayerControl>();
		playerRigid2D = GetComponent<Rigidbody2D>();
	}
}
