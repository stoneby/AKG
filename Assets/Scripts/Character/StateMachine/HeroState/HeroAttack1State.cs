using UnityEngine;

public class HeroAttack1State : MonoBehaviour
{
	public AudioClip Clip;

    private CharacterAttackChecker checker;

	void OnEnable()
	{
		audio.clip = Clip;
		audio.Play();

        checker.Check();
	}
	
	void Awake()
	{
	    checker = transform.Find("Sensors/NormalAttack").GetComponent<CharacterAttackChecker>();
	}
}
