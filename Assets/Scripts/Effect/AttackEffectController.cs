using UnityEngine;

public class AttackEffectController : MonoBehaviour
{
    public float LifeTime;
    public float Speed;
    public bool FacingRight;

	/// <summary>
	/// Owner game object that send attack effect.
	/// </summary>
	/// <value>The owner.</value>
	public GameObject Owner { get; set; }

	private DynamicSpawner spawner;
	
	private OneShotEffectController effectController;
	
    private Animator animator;
    private Rigidbody2D rigid;

    public void Play()
    {
        animator.Play("Attack");

        Destroy(gameObject, LifeTime);
    }

    public void Flying()
    {
        rigid.velocity = new Vector2(FacingRight ? Speed : -Speed, 0);
    }

    public void Hit()
    {
		spawner.Generate();

		effectController = spawner.SpawnInstance.GetComponent<OneShotEffectController>();
		effectController.Play();

        Destroy(gameObject);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

		spawner = transform.Find("BoomLocation").GetComponent<DynamicSpawner>();
    }
}
