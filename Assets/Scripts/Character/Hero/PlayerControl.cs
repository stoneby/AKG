using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    [HideInInspector]
    public bool fire = false;

	public bool SkillQ;
	public bool SkillW;
	public bool SkillE;

    public float h { get; set; }

    public enum InputDevice
    {
        HUD,
        Keyboard,
    };

    public InputDevice inputDevice;
    public AbstractInput inputManager;

    public float horizontalSpeed = 5f;		// The fastest the player can travel in the x axis when running.
    public float horizontalSpeedAttack;     // The fastest the player can travel in the x axis when attacking.
    public float verticalSpeed = 5f;

    public float fireStopTime;              // Continue firing util not any fire key pressed during the time.
	
    public AudioClip[] jumpClips;			// Array of clips for when the player jumps.

	public PlayerUIController PlayerUI;

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

    void Awake()
    {
        // Setting up references.
        groundChecker = transform.Find("Checkers/Ground");

        anim = GetComponent<Animator>();
		anim.enabled = false;

		characterCommon = GetComponent<CharacterCommon>();
        characterHealth = GetComponent<CharacterHealth>();

        if (inputDevice == InputDevice.HUD)
        {
            inputManager = GetComponent<HUDInput>();
        }
        else
        {
            inputManager = GetComponent<VirtualInput>();
        }
    }

	void Start()
	{
		PresentData.Instance.LevelInit.OnLoadComplete += OnLoadComplete;
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

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (inputManager.DoesJump() && grounded && !fire)
        {
            jump = true;
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(x * h, GetComponent<Rigidbody2D>().velocity.y);

        //rigidbody2D.AddForce(new Vector2(x *h, 0));

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
            // Set the Jump animator trigger parameter.
            anim.SetTrigger("Jump");

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, verticalSpeed);

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
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
            GetComponent<Rigidbody2D>().isKinematic = true;
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

    void OnCollisionExit2D(Collision2D other)
    {
    }

    void StopFiring()
    {
        fire = false;
    }

	void UpdateHealth(float value)
	{
		PlayerUI.UpdateSlider(value);
	}
}
