using UnityEngine;
using System.Collections;

public class HeroAttack2State : MonoBehaviour 
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
