using System.Linq;
using UnityEngine;

public class PlayerAttackChecker : MonoBehaviour
{
    public float CheckerDistance;

    private PlayerControl player;
    private CharacterCommon characterCommon;

    public void Check()
    {
        var hits = Physics2D.RaycastAll(player.transform.position,
            characterCommon.FacingRight ? Vector2.right : -Vector2.right, CheckerDistance,
            LayerMask.GetMask("Monster"));
        if (hits != null && hits.Count() != 0)
        {
            Debug.LogWarning("------------------------- i am hitting sth." + hits[0].transform.name);
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
        player = transform.GetComponent<PlayerControl>();
        characterCommon = player.GetComponent<CharacterCommon>();
    }
}
