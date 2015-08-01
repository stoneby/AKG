using UnityEngine;

public class HeroHurtState : MonoBehaviour
{
	public AudioClip Clip;

	private Animator playerAnimator;
	private PlayerControl player;

	private DynamicSpawner hurtFrontSpawner;
	private DynamicSpawner hurtBackSpawner;

	private OneShotEffectController hurtFrontEffect;
	private OneShotEffectController hurtBackEffect;

    void OnEnable()
    {
		audio.clip = Clip;
		audio.Play();

		if (player.HurtFront)
		{
			hurtFrontEffect.gameObject.SetActive(true);
			hurtFrontEffect.Play();
		}
		else
		{
			hurtBackEffect.gameObject.SetActive(true);
			hurtBackEffect.Play();
		}
    }

	void OnDisable()
	{
		hurtFrontEffect.gameObject.SetActive(false);
		hurtBackEffect.gameObject.SetActive(false);
	}

    void Awake()
    {
		playerAnimator = GetComponent<Animator>();
		player = GetComponent<PlayerControl>();

		hurtFrontSpawner = transform.Find("Effect/HurtFrontLocation").GetComponent<DynamicSpawner>();
		hurtBackSpawner = transform.Find("Effect/HurtBackLocation").GetComponent<DynamicSpawner>();

		hurtFrontSpawner.Generate();
		hurtBackSpawner.Generate();

		hurtFrontEffect = hurtFrontSpawner.SpawnInstance.GetComponent<OneShotEffectController>();
		hurtBackEffect = hurtBackSpawner.SpawnInstance.GetComponent<OneShotEffectController>();
	}
}
