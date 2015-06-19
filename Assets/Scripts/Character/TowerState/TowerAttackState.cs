using UnityEngine;

public class TowerAttackState : MonoBehaviour
{
    public PowerEffectController PowerEffectController;
    public GameObject AttackPrefab;

    public float ShootBeginTime;
    public float ShootInterval;

    private PlayerControl player;
    private CharacterCommon monsterCommon;

    private Transform attackLocation;
    private AttackEffectController attackEffectController;
    private Animator powerAnimator;

    /// <summary>
    /// Attack right place occurs.
    /// </summary>
    /// <remarks>Refers to attack animation right place.</remarks>
    public void OnShoot(GameObject go)
    {
        GenerateEffect();

        attackEffectController.FacingRight = monsterCommon.FacingRight;
        attackEffectController.Play();
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

    void PrepareToShoot()
    {
        powerAnimator.Play("TowerPower");
    }

    void OnEnable()
    {
        PowerEffectController.gameObject.SetActive(true);
        InvokeRepeating("PrepareToShoot", ShootBeginTime, ShootInterval);
    }

    void OnDisable()
    {
        CancelInvoke("PrepareToShoot");
        PowerEffectController.gameObject.SetActive(false);
    }

    void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		player.GetComponent<CharacterCommon>();
		GetComponent<MonsterControll>();
		monsterCommon = GetComponent<CharacterCommon>();
		GetComponent<CharacterInformation>();

        attackLocation = transform.Find("Effect/Attack");

        PowerEffectController.OnShoot += OnShoot;
        powerAnimator = PowerEffectController.GetComponent<Animator>();
    }
}
