using UnityEngine;

public class HeroSkillQState : MonoBehaviour
{
    public AudioClip Clip;

    private CharacterAttackChecker normalChecker;
    private CharacterAttackChecker powerChecker;
    private CharacterCommon characterCommon;
	private AudioSource source;

	private PlayerControl player;

    void OnEnable()
    {
        source.clip = Clip;
        source.Play();
    }

    public void CheckAttackQ()
    {
        normalChecker.Check();
    }

    public void CheckPowerAttackQ()
    {
        powerChecker.Check();
    }

    public void Jump()
    {
		player.SkillQJump = true;
    }

    public void JumpEnd()
    {
		player.SkillQJumpEnd = true;
    }

	public void EarthQuake()
	{
		CameraEffect.Instance.EarthQuake();
	}

    void Awake()
    {
        normalChecker = transform.Find("Sensors/SkillQNormalAttack").GetComponent<CharacterAttackChecker>();
        powerChecker = transform.Find("Sensors/SkillQPowerAttack").GetComponent<CharacterAttackChecker>();
        characterCommon = GetComponent<CharacterCommon>();
		source = GetComponent<AudioSource>();
		player = GetComponent<PlayerControl>();
    }
}
