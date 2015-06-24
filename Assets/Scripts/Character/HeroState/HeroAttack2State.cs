using UnityEngine;
using System.Collections;

public class HeroAttack2State : MonoBehaviour 
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
