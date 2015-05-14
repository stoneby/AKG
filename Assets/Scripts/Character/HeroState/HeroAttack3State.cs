using UnityEngine;

public class HeroAttack3State : MonoBehaviour
{
	public HeroAttackMove AttackMove;

    private PlayerControl player;

    void OnEnable()
    {
        //player.BoomFight = true;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
