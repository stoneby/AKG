using UnityEngine;

public class HeroSkillEState : MonoBehaviour
{
	private PlayerAttackChecker checker;

    void OnEnable()
    {
		checker.Check();
    }

    void Awake()
    {
		checker = transform.Find("Sensors/SkillEAttack").GetComponent<PlayerAttackChecker>();
	}
}
