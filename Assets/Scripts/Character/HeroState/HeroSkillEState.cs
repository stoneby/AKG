using UnityEngine;

public class HeroSkillEState : MonoBehaviour
{
	private PlayerAttackChecker checker;

    void OnEnable()
    {
    }

	public void CheckAttack()
	{
		checker.Check();
	}

    void Awake()
    {
		checker = transform.Find("Sensors/SkillEAttack").GetComponent<PlayerAttackChecker>();
	}
}
