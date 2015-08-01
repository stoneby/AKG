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
		audio.clip = Clip;
		audio.Play();

		checker.Check();
	}

    void Awake()
    {
		checker = transform.Find("Sensors/SkillEAttack").GetComponent<CharacterAttackChecker>();
	}
}
