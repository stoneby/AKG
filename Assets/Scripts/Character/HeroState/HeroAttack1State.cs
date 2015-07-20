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
	    checker = transform.Find("Sensors/NormalAttack").GetComponent<CharacterAttackChecker>();
	}
}
