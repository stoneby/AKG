using UnityEngine;

public class MudMasterHurtState : MonoBehaviour
{
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

	void Awake()
	{
		hurtSpawner = transform.Find("Effect/Hurt").GetComponent<DynamicSpawner>();
		hurtSpawner.Generate();
		
		hurtEffectController = hurtSpawner.SpawnInstance.GetComponent<OneShotEffectController>();
	}
}
