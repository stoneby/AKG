using UnityEngine;

public class HurtState : MonoBehaviour
{
    private PlayerControl player;
    private MonsterHUDController hudController;
    private CharacterHealth health;

    private Animator anim;
	private DynamicSpawner hurtSpawner;
	private OneShotEffectController hurtEffectController;

    void OnEnable()
    {
		hurtEffectController.gameObject.SetActive(true);
		hurtEffectController.Play();
	}

	void OnDisable()
	{
		hurtEffectController.gameObject.SetActive(false);
	}
   
    /// <summary>
    /// Decision point.
    /// </summary>
    /// <remarks>To idle state or duang state.</remarks>
    public void OnDecision()
    {
        if (player.LastAttack || health.Dead)
        {
            anim.SetTrigger("Duang");
        }
    }

    void Awake()
    {
        hudController = GetComponent<MonsterHUDController>();
        health = GetComponent<CharacterHealth>();

        anim = GetComponent<Animator>();

		hurtSpawner = transform.Find("Effect/Hurt").GetComponent<DynamicSpawner>();
		hurtSpawner.Generate();

		hurtEffectController = hurtSpawner.SpawnInstance.GetComponent<OneShotEffectController>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
