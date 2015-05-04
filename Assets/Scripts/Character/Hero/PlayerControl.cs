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

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private bool grounded;			        // Whether or not the player is grounded.
    private Animator anim;					// Reference to the player's animator component.
    private bool bordered;                  // Whether or not the player is in bordered.

	private CharacterCommon characterCommon;

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
		characterCommon = GetComponent<CharacterCommon>();

        if (inputDevice == InputDevice.HUD)
        {
            inputManager = GetComponent<HUDInput>();
        }
        else
        {
            inputManager = GetComponent<VirtualInput>();
        }
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        var hit = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        var lastgrounded = (hit.collider != null);

        if (lastgrounded != grounded)
        {
            grounded = lastgrounded;

            collider2D.isTrigger = !grounded && !bordered;
        }

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
        rigidbody2D.velocity = new Vector2(x * h, rigidbody2D.velocity.y);

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

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalSpeed);

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
        Debug.LogWarning("On trigger enter." + other.name);

        if (other.tag.Equals("Borders"))
        {
            bordered = true;
            collider2D.isTrigger = false;
        }

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

            characterCommon.Hurt();

			var monsterCommon = attackEffectController.Owner.GetComponent<CharacterCommon>();
			HurtFront = (characterCommon.FacingRight != monsterCommon.FacingRight);
        }

		if (other.tag.Equals("CaveEnd"))
		{
			var levelManager = GameData.Instance.LevelManager;
			var levelInit = PresentData.Instance.LevelInit;
			if (!levelManager.IsLastLevel)
			{
				levelInit.Load();
			}
		}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.LogWarning("On trigger exit." + other.name);

        if (other.tag.Equals("Borders"))
        {
            bordered = false;
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
        Debug.LogWarning("On collision enter." + other.transform.name);

        if (other.collider.tag.Equals("Borders"))
        {
            bordered = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.LogWarning("On collision exit." + other.transform.name);

        if (other.collider.tag.Equals("Borders"))
        {
            bordered = false;
        }
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
