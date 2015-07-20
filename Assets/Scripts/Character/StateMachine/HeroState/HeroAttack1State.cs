using UnityEngine;

public class HeroAttack1State : MonoBehaviour
{
    private CharacterAttackChecker checker;

	void OnEnable()
	{
        checker.Check();
	}
	
	void Awake()
	{
        checker = transform.parent.parent.Find("Sensors/NormalAttack").GetComponent<CharacterAttackChecker>();
	}
}
