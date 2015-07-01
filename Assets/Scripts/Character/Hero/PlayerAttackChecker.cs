using System.Linq;
using UnityEngine;

public class PlayerAttackChecker : MonoBehaviour
{
	public enum SideMode
	{
		OneSide,
		Both,
	};

    public float CheckerDistance;
	public SideMode Side;
	public int HurtHealth;

    private PlayerControl player;
    private CharacterCommon characterCommon;

    public void Check()
    {
		RaycastHit2D[] hits;

		if (Side == SideMode.OneSide)
		{
        	hits = Physics2D.RaycastAll(player.transform.position,
            							characterCommon.FacingRight ? Vector2.right : -Vector2.right, 
			                            CheckerDistance,
			                            LayerMask.GetMask("Monster"));
		}
		else
		{
			hits = Physics2D.RaycastAll(player.transform.position - new Vector3(CheckerDistance, 0, 0),
			                            Vector2.right, CheckerDistance * 2, LayerMask.GetMask("Monster"));
		}

        if (hits != null && hits.Count() != 0)
        {
			foreach (var hit in hits)
			{
				HurtMonster(hit.collider);
			}
        }
    }

    void HurtMonster(Collider2D other)
    {
		var monsterHealth = other.GetComponent<CharacterHealth>();
		if (monsterHealth.Dead)
		{
			return;
		}

		// health update.
		monsterHealth.HurtHealth = HurtHealth;
		monsterHealth.Hurt();

        // monster ui update.
		var monster = other.GetComponent<MonsterControll>();
		monster.Hurt();

        // player ui update.
        var playerInfor = player.GetComponent<CharacterInformation>();
		playerInfor.Damage = HurtHealth;
        playerInfor.Show(true);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        characterCommon = player.GetComponent<CharacterCommon>();
    }
}
