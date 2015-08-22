using UnityEngine;

public class MudMasterAttackState : MonoBehaviour
{
	public AudioClip Clip;

	private CharacterAttackChecker attackChecker;

	private Transform playerTrans;
	private CharacterCommon monsterCommon;

	public void OnAttack()
	{
		attackChecker.Check();
	}

    void OnEnable()
    {
		audio.clip = Clip;
		audio.Play();
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
		attackChecker = transform.Find("Sensors/Attack").GetComponent<CharacterAttackChecker>();

		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		monsterCommon = GetComponent<CharacterCommon>();
    }
}
