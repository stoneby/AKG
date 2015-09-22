using UnityEngine;

public class HeroSkillQState : MonoBehaviour
{
    public AudioClip Clip;
    public float AttackSpeed;
    public float JumpSpeed;

    private CharacterAttackChecker normalChecker;
    private CharacterAttackChecker powerChecker;
    private CharacterCommon characterCommon;
    private Rigidbody2D rigid2D;
    private bool jump;
    private bool jumpEnd;

    void OnEnable()
    {
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().Play();

        jump = false;
        jumpEnd = false;
    }

    void FixedUpdate()
    {
        rigid2D.velocity += new Vector2(characterCommon.FacingRight ? AttackSpeed : -AttackSpeed, 0);

        if (jump)
        {
            rigid2D.velocity += new Vector2(0, JumpSpeed);
            jump = false;
        }

        if (jumpEnd)
        {
            rigid2D.velocity += new Vector2(0, -JumpSpeed);
            jumpEnd = false;
        }
    }

    public void CheckAttackQ()
    {
        normalChecker.Check();
    }

    public void CheckPowerAttackQ()
    {
        powerChecker.Check();
    }

    public void Jump()
    {
        jump = true;
    }

    public void JumpEnd()
    {
        jumpEnd = true;
    }

    void Awake()
    {
        normalChecker = transform.Find("Sensors/SkillQNormalAttack").GetComponent<CharacterAttackChecker>();
        powerChecker = transform.Find("Sensors/SkillQPowerAttack").GetComponent<CharacterAttackChecker>();
        characterCommon = GetComponent<CharacterCommon>();
        rigid2D = GetComponent<Rigidbody2D>();
    }
}
