using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour 
{
	public float AttackInterval;

	private PlayerControl player;
	private CharacterCommon characterCommon;
	private CharacterInformation playerInfor;

	private bool monsterInRange;

	void OnTriggerEnter2D(Collider2D other)
	{
        Debug.LogWarning("PlayerAttackController OnTriggerEnter2D: " + other.name);
        
        if (other.tag.Equals("Monster"))
		{
            StartCoroutine("CheckAttack", other);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.LogWarning("PlayerAttackController OnTriggerExit2D: " + other.name);

		if(other.tag.Equals("Monster"))
		{
            StopCoroutine("CheckAttack");
		}
	}

	IEnumerator CheckAttack(Collider2D other)
	{
		while (true)
		{
			yield return null;

			// in case monster is dead.
			if (other == null)
			{
				break;
			}

            var facingRight = (other.transform.position.x > player.transform.position.x);
		    if (characterCommon.FacingRight == facingRight)
		    {
                if (player.fire)
                {
                    HurtMonster(other);

                    yield return new WaitForSeconds(AttackInterval);
                }
            }
		}
	}

    void HurtMonster(Collider2D other)
    {
        // health update.
        var monster = other.GetComponent<MonsterControll>();
		monster.Hurt();

        // monster ui update.
        var monsterHealth = other.GetComponent<CharacterHealth>();
        monsterHealth.Hurt();
        
        // player ui update.
        var playerInfor = player.GetComponent<CharacterInformation>();
		playerInfor.Show(true);
	}

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		characterCommon = player.GetComponent<CharacterCommon>();
		playerInfor = player.GetComponent<CharacterInformation>();
	}
}
