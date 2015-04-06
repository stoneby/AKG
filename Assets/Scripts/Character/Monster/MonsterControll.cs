using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    private Animator animator;
	private CharacterInformation characterInfor;
	private PlayerControl player;

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
	
    void Awake()
    {
        animator = GetComponent<Animator>();
		characterInfor = GetComponent<CharacterInformation>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Monster On trigger enter: " + other.name);

		if (other.tag.Equals("Player"))
		{

		}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Monster On trigger exit: " + other.name);
    }
}
