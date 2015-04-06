using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	public int TotalHealth;
    public int CurrentHealth;
    public int HurtHealth;

    private Animator animator;

    public void Hurt()
    {
        CurrentHealth -= HurtHealth;
        animator.SetTrigger("Hurt");

		SendMessage("UpdateHealth", 1f * CurrentHealth / TotalHealth, SendMessageOptions.DontRequireReceiver);

        if (CurrentHealth < 0)
        {
            animator.SetTrigger("Die");
			SendMessage("Dead", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
		CurrentHealth = TotalHealth;
    }
}
