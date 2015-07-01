using UnityEngine;

public class HeroSkillQState : MonoBehaviour
{
	public float AttackSpeed;

	private PlayerAttackChecker normalChecker;
	private PlayerAttackChecker powerChecker;
	private CharacterCommon characterCommon;
	private Rigidbody2D rigid2D;

	void OnEnable()
	{
	}

	void FixedUpdate()
	{
		rigid2D.velocity += new Vector2(characterCommon.FacingRight ? AttackSpeed : -AttackSpeed, 0);
	}

	public void CheckAttackQ()
	{
		normalChecker.Check();
	}

	public void CheckPowerAttackQ()
	{
		powerChecker.Check();
	}
	
	void Awake()
	{
		normalChecker = transform.Find("Sensors/SkillQNormalAttack").GetComponent<PlayerAttackChecker>();
		powerChecker = transform.Find("Sensors/SkillQPowerAttack").GetComponent<PlayerAttackChecker>();
		characterCommon = GetComponent<CharacterCommon>();
		rigid2D = GetComponent<Rigidbody2D>();
	}
}
