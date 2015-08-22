using UnityEngine;
using System.Collections;

public class InterfaceController : Singleton<InterfaceController> 
{
	public Transform HeroContainer;
	public Transform GoldContainer;
	public Transform SkillQContainer;
	public Transform SkillWContainer;
	public Transform SkillEContainer;

	private Transform SkillContainer;

	void Awake()
	{
		HeroContainer = transform.Find("HeroInforContainer");
		GoldContainer = transform.Find("Gold");
		SkillContainer = transform.Find("SkillContainer");
		SkillQContainer = SkillContainer.Find("Q");
		SkillEContainer = SkillContainer.Find("E");
		SkillWContainer = SkillContainer.Find("W");
	}
}
