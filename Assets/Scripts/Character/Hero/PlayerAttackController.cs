using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour 
{
	public float AttackInterval;

	private PlayerControl player;
	private CharacterCommon characterCommon;
	private CharacterInformation playerInfor;

	private bool monsterInRange;

	private MonsterControll monster;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Monster"))
		{
			var facingRight = (other.transform.position.x > player.transform.position.x);
			if (characterCommon.FacingRight && facingRight)
			{
				monsterInRange = true;

				StartCoroutine("CheckAttack", other);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.LogWarning("PlayerAttackController OnTriggerExit2D: " + other.name);

		if(other.tag.Equals("Monster"))
		{
			var facingRight = (other.transform.position.x > player.transform.position.x);
			if (characterCommon.FacingRight && facingRight)
			{
				monsterInRange = false;

				StopCoroutine("CheckAttack");
			}
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

			monster = other.GetComponent<MonsterControll>();

			if (player.fire)
			{
				HurtMonster();

				yield return new WaitForSeconds(AttackInterval);
			}
		}
	}
	
	void HurtMonster()
	{
		var monsterHealth = monster.GetComponent<CharacterHealth>();
		monsterHealth.Hurt();

		monster.Hurt();

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
