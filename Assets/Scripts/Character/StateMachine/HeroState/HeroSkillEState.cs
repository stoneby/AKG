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
        checker = transform.parent.parent.Find("Sensors/SkillEAttack").GetComponent<CharacterAttackChecker>();
	}
}
