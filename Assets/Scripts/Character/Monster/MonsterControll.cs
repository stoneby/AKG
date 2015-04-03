using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    private Animator animator;
	private CharacterInformation characterInfor;

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
