using UnityEngine;

public class HeroSkillQState : MonoBehaviour
{
	public float AttackSpeed;

	private PlayerAttackChecker checker;
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
		checker.Check();
	}
	
	void Awake()
	{
		checker = transform.Find("Sensors/SkillQAttack").GetComponent<PlayerAttackChecker>();
		characterCommon = GetComponent<CharacterCommon>();
		rigid2D = GetComponent<Rigidbody2D>();
	}
}
