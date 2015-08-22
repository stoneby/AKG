using UnityEngine;

public class HeroSkillEState : MonoBehaviour
{
	public AudioClip Clip;
	private CharacterAttackChecker checker;

    void OnEnable()
    {
	}

	public void CheckAttackE()
	{
		GetComponent<AudioSource>().clip = Clip;
		GetComponent<AudioSource>().Play();

		checker.Check();
	}

    void Awake()
    {
		checker = transform.Find("Sensors/SkillEAttack").GetComponent<CharacterAttackChecker>();
	}
}
