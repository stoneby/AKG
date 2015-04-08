using UnityEngine;

public class AttackState : MonoBehaviour
{
    private MonsterControll monster;
    private PlayerControl player;
	private CharacterInformation monsterInfor;
	private CharacterCommon monsterCommon;
	private CharacterCommon playerCommon;

    void OnEnable()
    {
		playerCommon.Hurt();
		monsterInfor.Show(true);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.zero;

		if (monsterCommon.FacingRight && player.transform.position.x < monster.transform.position.x)
        {
			monsterCommon.Flip();
        }

		if (!monsterCommon.FacingRight && player.transform.position.x > monster.transform.position.x)
        {
			monsterCommon.Flip();
        }
    }

    public void AttackRightPlace()
    {
        
    }

    void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		playerCommon = player.GetComponent<CharacterCommon>();
		monster = GetComponent<MonsterControll>();
		monsterCommon = GetComponent<CharacterCommon>();
		monsterInfor = GetComponent<CharacterInformation>();
    }
}
