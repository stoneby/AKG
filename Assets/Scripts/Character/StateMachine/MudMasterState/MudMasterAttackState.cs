using UnityEngine;

public class MudMasterAttackState : MonoBehaviour
{
	private CharacterAttackChecker attackChecker;

	private Transform playerTrans;
	private CharacterCommon monsterCommon;

	public void OnAttack()
	{
		attackChecker.Check();
	}

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

	void FixedUpdate()
	{	
		if (monsterCommon.FacingRight && playerTrans.position.x < monsterCommon.transform.position.x)
		{
			monsterCommon.Flip();
		}
		
		if (!monsterCommon.FacingRight && playerTrans.position.x > monsterCommon.transform.position.x)
		{
			monsterCommon.Flip();
		}
	}

    void Awake()
    {
        var root = transform.parent.parent;
        attackChecker = root.Find("Sensors/Attack").GetComponent<CharacterAttackChecker>();

		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		monsterCommon = GetComponent<CharacterCommon>();
    }
}
