using System.Linq;
using UnityEngine;

public class CharacterAttackChecker : MonoBehaviour
{
	public enum SideMode
	{
		OneSide,
		Both,
	};

	public GameObject SourceCharacter;
	public string TargetLayer;

    public float CheckerDistance;
	public SideMode Side;
	public int HurtHealth;
	
    private CharacterCommon characterCommon;
	private CharacterInformation characterInfor;

    public void Check()
    {
		RaycastHit2D[] hits;

		if (Side == SideMode.OneSide)
		{
        	hits = Physics2D.RaycastAll(SourceCharacter.transform.position,
            							characterCommon.FacingRight ? Vector2.right : -Vector2.right, 
			                            CheckerDistance,
			                            LayerMask.GetMask(TargetLayer));
		}
		else
		{
			hits = Physics2D.RaycastAll(SourceCharacter.transform.position - new Vector3(CheckerDistance, 0, 0),
			                            Vector2.right, CheckerDistance * 2, LayerMask.GetMask(TargetLayer));
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

        // character target update.
		var characterCommon = other.GetComponent<CharacterCommon>();
		characterCommon.Hurt();

        // character source ui update.
		characterInfor.Damage = HurtHealth;
		characterInfor.Show(true);
    }

    void Awake()
    {
        characterCommon = SourceCharacter.GetComponent<CharacterCommon>();
		characterInfor = SourceCharacter.GetComponent<CharacterInformation>();
    }
}
