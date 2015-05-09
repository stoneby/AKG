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
		playerRigid2D.velocity = Speed * playerRigid2D.velocity;
	}
	
	void Awake()
	{
		playerAnimator = GetComponent<Animator>();
		player = GetComponent<PlayerControl>();
		playerRigid2D = GetComponent<Rigidbody2D>();
	}
}
