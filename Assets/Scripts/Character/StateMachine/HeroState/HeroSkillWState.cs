using UnityEngine;

public class HeroSkillWState : MonoBehaviour
{
	private DynamicSpawner spawner;
	private OneShotEffectController heatEffect;


	void OnEnable()
	{
		heatEffect.gameObject.SetActive(true);
		heatEffect.Play();
	}

	void OnDisable()
	{
		heatEffect.gameObject.SetActive(false);
	}
	
	void Awake()
	{
		spawner = transform.parent.parent.Find("Effect/HeatLocation").GetComponent<DynamicSpawner>();
		spawner.Generate();
		
		heatEffect = spawner.SpawnInstance.GetComponent<OneShotEffectController>();
	}
}
