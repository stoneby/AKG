using UnityEngine;

public class AttackState : MonoBehaviour
{
	public AudioClip Clip;

    public GameObject AttackPrefab;

    private MonsterControll monster;
    private PlayerControl player;
	private CharacterInformation monsterInfor;
	private CharacterCommon monsterCommon;
	private CharacterCommon playerCommon;

    private Transform attackLocation;
    private AttackEffectController attackEffectController;

    void OnEnable()
    {
		audio.clip = Clip;
		audio.Play();
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

    /// <summary>
    /// Attack right place occurs.
    /// </summary>
    /// <remarks>Refers to attack animation right place.</remarks>
    public void AttackRightPlace()
    {
        GenerateEffect();

        attackEffectController.FacingRight = monsterCommon.FacingRight;
        attackEffectController.Play();
    }


    void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		playerCommon = player.GetComponent<CharacterCommon>();
		monster = GetComponent<MonsterControll>();
		monsterCommon = GetComponent<CharacterCommon>();
		monsterInfor = GetComponent<CharacterInformation>();

        attackLocation = transform.Find("Effect/Attack");
    }

    private void GenerateEffect()
    {
        var attackObject = Instantiate(AttackPrefab, attackLocation.position, attackLocation.rotation) as GameObject;
        attackEffectController = attackObject.GetComponent<AttackEffectController>();
		attackEffectController.Owner = gameObject;

        //attackObject.transform.parent = attackLocation;
        attackObject.transform.localScale =
            new Vector3(
                monsterCommon.FacingRight ? attackObject.transform.localScale.x : -attackObject.transform.localScale.x,
                attackObject.transform.localScale.y, attackObject.transform.localScale.z);
    }
}
