using UnityEngine;

public class DuangState : MonoBehaviour
{
    public float DuangSpeed;

    private CharacterHealth monsterHealth;
    private Animator monsterAnimator;
    private Rigidbody2D rigid2D;

    private CharacterCommon playerCommon;

    private bool onTheGround;

    void OnEnable()
    {
        onTheGround = false;
    }

    void FixedUpdate()
    {
        rigid2D.velocity = (onTheGround) ? Vector2.zero : new Vector2(playerCommon.FacingRight ? DuangSpeed : -DuangSpeed, 0);
    }

    /// <summary>
    /// Event call back when on the ground.
    /// </summary>
    public void OnTheGround()
    {
        onTheGround = true;

        if (monsterHealth.Dead)
        {
            monsterAnimator.SetTrigger("Die");
        }
    }

    void Awake()
    {
        monsterHealth = GetComponent<CharacterHealth>();
        monsterAnimator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

        playerCommon = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterCommon>();
    }
}
