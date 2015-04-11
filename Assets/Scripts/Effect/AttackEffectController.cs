using UnityEngine;

public class AttackEffectController : MonoBehaviour
{
    public float LifeTime;
    public float Speed;
    public bool FacingRight;

	public GameObject BoomEffectPrefab;

	private Transform boomLocation;
	private OneShotEffectController effectController;
	private GameObject boomEffect;
	
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
		GenerateEffect();
		effectController.Play();

        Destroy(gameObject);
    }

	private void GenerateEffect()
	{
		boomEffect = Instantiate(BoomEffectPrefab, boomLocation.position, boomLocation.rotation) as GameObject;
		//boomEffect.transform.parent = boomLocation;
		
		effectController = boomEffect.GetComponent<OneShotEffectController>();
	}

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

		boomLocation = transform.Find("BoomLocation");
    }
}
