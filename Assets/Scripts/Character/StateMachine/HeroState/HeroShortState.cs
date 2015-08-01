using UnityEngine;

public class HeroShortState : MonoBehaviour
{
	public AudioClip Clip;
	public float Speed;

    private Rigidbody2D playerRigid2D;
	
	void OnEnable()
	{
		audio.clip = Clip;
		audio.Play();
	}

	void FixedUpdate()
	{
		playerRigid2D.velocity = Speed * playerRigid2D.velocity;
	}
	
	void Awake()
	{
		GetComponent<Animator>();
		GetComponent<PlayerControl>();
		playerRigid2D = GetComponent<Rigidbody2D>();
	}
}
