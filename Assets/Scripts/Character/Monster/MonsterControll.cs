using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    public GameObject HurtPrefab;

    private Animator animator;

    private Transform hurtLocation;
    private GameObject hurtObject;

    public bool FacingRight = true;

    public void Attack(bool flag)
    {
        animator.SetBool("Attack", flag);
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void LookAround(bool flag)
    {
        
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();

        hurtLocation = transform.Find("HurtIcon");
        hurtObject = Instantiate(HurtPrefab, hurtLocation.position, hurtLocation.rotation) as GameObject;
        hurtObject.transform.parent = hurtLocation.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On trigger enter: " + other.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("On trigger exit: " + other.name);
    }
}
