using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    [HideInInspector]
    public bool fire = false;

	[HideInInspector]
    public bool SkillQ;
	[HideInInspector]
    public bool SkillW;
	[HideInInspector]
    public bool SkillE;

	/// <summary>
	/// Flag indicates if jump in skill Q.
	/// </summary>
	[HideInInspector]
	public bool SkillQJump;
	/// <summary>
	/// Flag indicates if jump end in skill Q.
	/// </summary>
	[HideInInspector]
	public bool SkillQJumpEnd;

    public float horizontalSpeed = 5f;		// The fastest the player can travel in the x axis when running.
    public float horizontalSpeedAttack;     // The fastest the player can travel in the x axis when attacking.
    public float verticalSpeed = 5f;

	public float SkillQAttackSpeed;
	public float SkillQJumpSpeed;

    public float fireStopTime;              // Continue firing util not any fire key pressed during the time.

    public PlayerUIController PlayerUI;

    public HashIDs Hash;

    public int MaxJumpStep;

	/// <summary>
	/// Horizontal movement.
	/// </summary>
	/// <value>The h.</value>
	public float h { get; set; }

    /// <summary>
    /// Flag indicates whether this attack is booming.
    /// </summary>
    public bool BoomFight { get; set; }

    /// <summary>
    /// Flag indicates if the attack is final attack, eg, attack4.
    /// </summary>
    /// <value><c>true</c> if last attack; otherwise, <c>false</c>.</value>
    public bool LastAttack { get; set; }

    public bool Running { get { return h > 0f; } }

    /// <summary>
    /// Flag indicates if player is hurt front or back.
    /// </summary>
    /// <value><c>true</c> if hurt front; otherwise, <c>false</c>.</value>
    public bool HurtFront { get; set; }

    private Transform groundChecker;			// A position marking where to check if the player is grounded.
    private Animator anim;					// Reference to the player's animator component.

    private CharacterCommon characterCommon;
    private CharacterHealth characterHealth;
    private AbstractInput inputManager;
    private Rigidbody2D rigid2D;
    private int jumpCounter;

    void Awake()
    {
        // Setting up references.
        groundChecker = transform.Find("Checkers/Ground");

        anim = GetComponent<Animator>();
        anim.enabled = false;

        rigid2D = GetComponent<Rigidbody2D>();

        characterCommon = GetComponent<CharacterCommon>();
        characterHealth = GetComponent<CharacterHealth>();
    }

    void Start()
    {
        PresentData.Instance.LevelInit.OnLoadComplete += OnLoadComplete;
        inputManager = GameData.Instance.InputManager;
    }

    private void OnLoadComplete()
    {
        PresentData.Instance.LevelInit.OnLoadComplete -= OnLoadComplete;
        anim.enabled = true;
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        var grounded = Physics2D.Linecast(transform.position, groundChecker.position, LayerMask.GetMask("Ground"));

        if (grounded)
        {
            jumpCounter = 0;
        }

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (inputManager.DoesJump() && !fire && (jumpCounter < MaxJumpStep))
        {
            jump = true;
            ++jumpCounter;
        }

        if (inputManager.DoesFire() && grounded)
        {
            fire = true;

            CancelInvoke("StopFiring");
            Invoke("StopFiring", fireStopTime);
        }

        if (inputManager.DoesSkillE() && grounded)
        {
            SkillE = true;
        }

        if (inputManager.DoesSkillQ() && grounded)
        {
            SkillQ = true;
        }

        if (inputManager.DoesSkillW() && grounded)
        {
            SkillW = true;
        }
    }

    void FixedUpdate()
    {
        // Cache the horizontal input.
        h = inputManager.GetHorizontal();

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        h = Mathf.Clamp(h, -1, 1);
        var x = (fire) ? horizontalSpeedAttack : horizontalSpeed;
        rigid2D.velocity = new Vector2(x * h, rigid2D.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !characterCommon.FacingRight)
        {
            // ... flip the player.
            characterCommon.Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && characterCommon.FacingRight)
        {
            // ... flip the player.
            characterCommon.Flip();
        }

        // If the player should jump...
        if (jump)
        {
            if (CouldJumpState())
            {
                // Set the Jump animator trigger parameter.
                anim.SetTrigger("Jump");
            }

            rigid2D.velocity = new Vector2(rigid2D.velocity.x, verticalSpeed);

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }

		if (IsSkillQState())
		{
			rigid2D.velocity += new Vector2(characterCommon.FacingRight ? SkillQAttackSpeed : -SkillQAttackSpeed, 0);
		}

		if (SkillQJump)
		{
			rigid2D.velocity += new Vector2(0, SkillQJumpSpeed);
			SkillQJump = false;
		}

		if (SkillQJumpEnd)
		{
			rigid2D.velocity += new Vector2(0, -SkillQJumpSpeed);
			SkillQJumpEnd = false;
		}

        if (SkillE)
        {
            anim.SetTrigger("SkillE");
            SkillE = false;
        }

        if (SkillQ)
        {
            anim.SetTrigger("SkillQ");
            SkillQ = false;
        }

        if (SkillW)
        {
            anim.SetTrigger("SkillW");
            SkillW = false;
        }

        anim.SetBool("Attack", fire);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("AttackSensor"))
        {
            var monster = other.attachedRigidbody.GetComponent<MonsterControll>();
            monster.Attack(true);
        }

        if (other.tag.Equals("WarningSensor"))
        {
            var monster = other.attachedRigidbody.GetComponent<MonsterControll>();
            monster.LookAround(true);
        }

        if (other.tag.Equals("MonsterBullet"))
        {
            var attackEffectController = other.attachedRigidbody.GetComponent<AttackEffectController>();
            attackEffectController.Hit();

            HurtFront = (characterCommon.FacingRight != attackEffectController.FacingRight);

            characterCommon.Hurt();
        }

        if (other.tag.Equals("CaveEnd"))
        {
            var levelManager = GameData.Instance.LevelManager;
            if (levelManager.IsLastLevel)
            {
                levelManager.Reset();
            }
            rigid2D.isKinematic = true;
            var levelInit = PresentData.Instance.LevelInit;
            levelInit.Load();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Borders"))
        {
        }

        if (other.tag.Equals("AttackSensor"))
        {
            var monster = other.attachedRigidbody.GetComponent<MonsterControll>();
            monster.Attack(false);
        }

        if (other.tag.Equals("WarningSensor"))
        {
            var monster = other.attachedRigidbody.GetComponent<MonsterControll>();
            monster.LookAround(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("DeathBorder"))
        {
            characterCommon.HurtHealth = characterHealth.CurrentHealth;
            characterCommon.Hurt();
        }
    }

    void StopFiring()
    {
        fire = false;
    }

	private bool IsSkillQState()
	{
		return IsState(Hash.SkillQState);
	}

	private bool IsJumpState()
	{
		return IsState(Hash.JumpState);
	}

    private bool CouldJumpState()
    {
        return (IsState(Hash.IdleState)) || (IsState(Hash.RunState)) || (IsState(Hash.JumpState));
    }

	private bool IsState(int hash)
	{
		var result = anim.GetCurrentAnimatorStateInfo(0)
			.fullPathHash.Equals(hash);
		return result;
	}
}
