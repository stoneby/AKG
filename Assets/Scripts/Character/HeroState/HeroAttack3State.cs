using UnityEngine;

public class HeroAttack3State : MonoBehaviour
{
	public HeroAttackMove AttackMove;

    private PlayerControl player;
	private CharacterAttackChecker checker;

    void OnEnable()
    {
		checker.Check();
    }

	void FixedUpdate()
	{
		AttackMove.MoveUpdate();
	}
	
	public void OnAttack3Start()
	{
		AttackMove.MoveStart();
	}
	
	public void OnAttack3Stop()
	{
		AttackMove.MoveStop();
	}

    void Awake()
    {
		checker = transform.Find("Sensors/NormalAttack").GetComponent<CharacterAttackChecker>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
