using UnityEngine;

public class HeroSkillEState : MonoBehaviour
{
	private CharacterAttackChecker checker;

    void OnEnable()
    {
    }

	public void CheckAttackE()
	{
		checker.Check();
	}

    void Awake()
    {
		checker = transform.Find("Sensors/SkillEAttack").GetComponent<CharacterAttackChecker>();
	}
}
