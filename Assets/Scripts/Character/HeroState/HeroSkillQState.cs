using UnityEngine;

public class HeroSkillQState : MonoBehaviour
{
	private PlayerAttackChecker checker;
	
	void OnEnable()
	{
		checker.Check();
	}
	
	void Awake()
	{
		checker = transform.Find("Sensors/SkillQAttack").GetComponent<PlayerAttackChecker>();
	}
}
