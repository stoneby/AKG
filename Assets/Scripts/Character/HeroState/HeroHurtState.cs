using UnityEngine;

public class HeroHurtState : MonoBehaviour
{
	private Animator playerAnimator;
	private PlayerControl player;

    void OnEnable()
    {
    }

    void Awake()
    {
		playerAnimator = GetComponent<Animator>();
		player = GetComponent<PlayerControl>();
    }
}
