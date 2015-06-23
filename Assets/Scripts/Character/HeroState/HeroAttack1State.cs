using UnityEngine;

public class HeroAttack1State : MonoBehaviour
{
    private PlayerAttackChecker checker;

	void OnEnable()
	{
        checker.Check();
	}
	
	void Awake()
	{
	    checker = GetComponent<PlayerAttackChecker>();
	}
}
